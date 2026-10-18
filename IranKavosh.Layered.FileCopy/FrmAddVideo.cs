using DevExpress.Utils;
using DevExpress.XtraEditors;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MyCom.Class.ClassFileManage;

namespace IranKavosh.Layered.FileCopy
{
    public partial class FrmAddVideo : DevExpress.XtraEditors.XtraForm
    {
        private string _addressFlash;
        public List<ClassFileManage.ModelFile> _modelFiles = new List<ClassFileManage.ModelFile>();

        #region http

        private static HttpClient _httpClient = new HttpClient();
        private MultiAgeHttpService _multiAge = new MultiAgeHttpService(_httpClient);
        private MultiActorHttpService _multiActor = new MultiActorHttpService(_httpClient);
        private MultiGenreHttpService _multiGenre = new MultiGenreHttpService(_httpClient);
        private MultiCountryHttpService _multiCountry = new MultiCountryHttpService(_httpClient);
        private MultiPriceHttpService _multiPrice = new MultiPriceHttpService(_httpClient);
        private MultimediaHeaderHttpService _multiHeader = new MultimediaHeaderHttpService(_httpClient);
        private MultimediaHeaderChapterHttpService _multiHeaderChapter = new MultimediaHeaderChapterHttpService(_httpClient);
        private MultimediaChapterDetailHttpService _multiChapterDetail = new MultimediaChapterDetailHttpService(_httpClient);
        private MultiLocationHttpService _multiLocation = new MultiLocationHttpService(_httpClient);
        //  private PersonalGroupHttpService _personalGroup = new PersonalGroupHttpService(_httpClient);

        #endregion
        private ClsFont _clsFont = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        public FrmAddVideo(string addressFlash = "ANDROIDDATA")
        {
            _addressFlash = addressFlash;
            InitializeComponent();
            this.Shown += FrmAddVideo_Shown;
        }

        private async void FrmAddVideo_Shown(object? sender, EventArgs e)
        {
            SetFonts();
            await Task.WhenAll(
                LoadHeader(),
                SetFieldDgvSelect()
            );
        }
        private void FrmAddVideo_Load(object sender, EventArgs e)
        {

        }
        public void SetFonts()
        {
            dgvSelect.RightToLeft = RightToLeft.Yes;
            _clsFont.ChangeFont(dgvSelect);
            _clsFont.ChangeFont(lueHeader, 14);
            dgvSelect.SetStyle();
        }

        class ModelLstHeader
        {
            public Guid Id { get; set; }
            public long Num { get; set; }
            public string NamePersian { get; set; }
            public string Created { get; set; }
        }
        class ModelForCopyFile
        {
            public string FullName { get; set; }
            public string FileName { get; set; }
            public string Exten { get; set; }

        }
        public async Task LoadHeader()
        {
            var getHeader = await _multiHeader.GetByModel();
            lueHeader.Properties.DataSource = getHeader.Select(s => new ModelLstHeader
            {
                Id = s.Id,
                Num = s.Num,
                NamePersian = s.NamePersian,
                Created = s.Created
            }).ToList();

            lueHeader.Properties.PopulateViewColumns();
            lueHeader.Properties.ValueMember = "Id";
            lueHeader.Properties.DisplayMember = "NamePersian";
            lueHeader.Properties.View.Columns["Id"].Visible = false;
            lueHeader.Properties.View.Columns["Num"].AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            lueHeader.Properties.View.Columns["Num"].AppearanceCell.TextOptions.VAlignment = VertAlignment.Center;
            lueHeader.Properties.View.BestFitColumns();
            lueHeader.EditValueChanged += async (s1, e1) =>
            {
                var getId = lueHeader.EditValue.ToGuid();
                await LoadHeaderChapter(getId);
            };

            lstHeaderChapters.SelectedValueChanged += async (s1, e1) =>
            {
                //var getHeaderChapterId = Guid.Parse(lstHeaderChapters.SelectedValue.ToString());
                var getHeaderChapter = lstHeaderChapters.SelectedValue as ModelLstHeaderChapter;
                if (getHeaderChapter != null)
                    await LoadChapterDetails(getHeaderChapter.Id);
                else
                {
                    if (lstHeaderChapters.SelectedValue != null)
                    {
                        var getHeaderChapterId2 = lstHeaderChapters.SelectedValue.ToGuid();
                        if (getHeaderChapterId2 != null)
                        {
                            await LoadChapterDetails(getHeaderChapterId2);
                        }
                    }
                }
            };
            lstChapterDetails.DoubleClick += (s1, e1) =>
            {
                string addressForCopy = "";
                string lang = "";
                var getId = lstChapterDetails.SelectedValue.ToGuid();
                var getFind = _getFullName.First(f => f.ChapterDetailsId == getId);


                var getDir = Path.GetDirectoryName(getFind.AddressFile);
                var getFile = Directory.GetFiles(getDir);
                var getFile1 = getFile.Select(s => new ModelForCopyFile
                {
                    FullName = s,
                    FileName = Path.GetFileNameWithoutExtension(s),
                    Exten = Path.GetExtension(s),

                }).ToList();

                if (getFile1.Any(a => a.FileName == "Fa-" + getFind.ChapterCode))
                    addressForCopy = getFile1.First(a => a.FileName == "Fa-" + getFind.ChapterCode).FullName;



                var to1 = getFind.HeaderName;
                var to2 = getFind.HeaderChapterCode.ToString();
                var to3 = Path.GetFileName(addressForCopy);

                var session = getFind.ChapterCode;

                string to4;
                string join;
                if (to2 != "1")
                {
                    to4 = Path.Combine(_addressFlash, to1, to2, to3);
                    join = string.Join(" - ", to1, to2, session);
                }
                else
                {
                    to4 = Path.Combine(_addressFlash, to1, to3);
                    join = string.Join(" - ", to1,  session);
                }

              
                
                _modelFiles.Add(new ModelFile
                {
                    Name = join,
                    ChapterHeaderId = getId,
                    From = addressForCopy,
                    To = to4
                });
                _dtSelect.Rows.Add("", "", getId, getFind.HeaderName + " - " + getFind.HeaderChapterCode + " - " + getFind.ChapterCode);
            };
        }
        List<ModelChapterDetailsFull> _getFullName;
        class ModelLstHeaderChapter
        {
            public Guid Id { get; set; }
            public string Full { get; set; }
        }

        public async Task LoadHeaderChapter(Guid headerId)
        {
            var getHeaderChapter = await _multiHeaderChapter.GetByHeader(headerId);
            lstHeaderChapters.DataSource = getHeaderChapter.OrderBy(o => o.Code).Select(s => new ModelLstHeaderChapter { Id = s.Id, Full = s.Code + " - " + s.Title }).ToList();
            lstHeaderChapters.ValueMember = "Id";
            lstHeaderChapters.DisplayMember = "Full";
        }
        public async Task LoadChapterDetails(Guid headerId)
        {
            //123
            //var getChapterDetail = await _multiChapterDetail.GetByHeader(headerId);
            var getChapterDetail = (await _multiChapterDetail.GetByHeaderChapter(headerId));
            //var modelGuidFaEns = new List<ModelGuidFaEn>();


            var _modelGuidFaEns = getChapterDetail.Select(s => new ModelGuidFaEn { ChapterDetailId = s.Id, Type = "Fa" }).ToList();

            _getFullName = (await _multiChapterDetail.GetByHeaderTranslate(_modelGuidFaEns)).ToList();

            //lstChapterDetails.DataSource = getChapterDetail.OrderBy(o => o.Code).Select(s => new { s.Id, Full = s.Code + " - " }).ToList();
            lstChapterDetails.DataSource = _getFullName.OrderBy(o => o.ChapterCode).Select(s => new
            {
                s.ChapterDetailsId,
                Full = "قسمت: " + s.ChapterCode
            }).ToList();
            lstChapterDetails.ValueMember = "ChapterDetailsId";
            lstChapterDetails.DisplayMember = "Full";
        }

        private DataTable _dtSelect;
        public async Task SetFieldDgvSelect()
        {
            if (dgvSelect.ColumnCount() == 0)
            {
                _dtSelect = dgvSelect.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new KavoshGrid.modelColumn{Name = "ردیف"},
                    new KavoshGrid.modelColumn{Name = "حذف",ImageValue = MyCom.Properties.Resources.delete4},
                    new KavoshGrid.modelColumn{Name = "Id",Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "فیلم",},
                }, false, false, false);

                dgvSelect.HiddenColumn("Id");
                dgvSelect.MaxMinWidth("ردیف", 25, 25);
                dgvSelect.MaxMinWidth("حذف", 30, 30);

                #region Event   

                dgvSelect.AddEventRowCellClick<Guid>(id =>
                {
                    dgvSelect.DeleteRow(false);
                }, "Id", "حذف");

                #endregion
            }

     
        }
    }
}