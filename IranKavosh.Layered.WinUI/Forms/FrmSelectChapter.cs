using DevExpress.Data;
using DevExpress.Data.Helpers;
using DevExpress.Utils.Extensions;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DocumentFormat.OpenXml.Office2013.Drawing.ChartStyle;
using DocumentFormat.OpenXml.Presentation;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using IranKavosh.Layered.WinUI.Class;
using Microsoft.Win32;
using MyCom.Class;
using MyCom.Object;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class FrmSelectChapter : DevExpress.XtraEditors.XtraForm
    {
        public List<ModelSelectFiles> _selFilms;
        private readonly Guid _headerId;
        private Guid _headerChapterId;
        private readonly string _persianName;
        private ClsFont _clsFont = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private static HttpClient _httpClient = new HttpClient();
        private PersonalHttpService _personal = new PersonalHttpService(_httpClient);
        private PersonalGroupHttpService _personalGroup = new PersonalGroupHttpService(_httpClient);
        private MultimediaHeaderHttpService _multiHeader = new MultimediaHeaderHttpService(_httpClient);
        private MultimediaHeaderChapterHttpService _multiHeaderChapter = new MultimediaHeaderChapterHttpService(_httpClient);
        private MultimediaChapterDetailHttpService _multiChapterDetail = new MultimediaChapterDetailHttpService(_httpClient);
        private MultiLocationHttpService _multiLocation = new MultiLocationHttpService(_httpClient);
        private MultiCountryHttpService _multiCountry = new MultiCountryHttpService(_httpClient);
        private MultiGenreHttpService _multiGenre = new MultiGenreHttpService(_httpClient);

        public FrmSelectChapter(Guid headerId, string persianName, List<ModelSelectFiles> selFilms)
        {
            _headerId = headerId;
            _persianName = persianName;
            _selFilms = selFilms;

            InitializeComponent();
            this.Shown += FrmSelectChapter_Shown;

        }
        private void FrmSelectChapter_Load(object sender, EventArgs e)
        {

        }
        private async void FrmSelectChapter_Shown(object sender, EventArgs e)
        {
            SetFonts();

            await SetFieldLoadHeaderChapter(_headerId);
            LoadBeforeSelectFilm();
            await SetFieldDgvAllSelect();
            lblCountFilm.Text = _selFilms.Count.ToString();
        }

        public void SetFonts()
        {
            _clsFont.ChangeFont(PnlChapterEnFa, 12);

            _clsFont.ChangeFont(dgvChapter);
            _clsFont.ChangeFont(dgvChapterEn);
            _clsFont.ChangeFont(dgvHeaderChapter);
            _clsFont.ChangeFont(dgvAllSelect, 12);

            dgvChapter.SetStyle();
            dgvChapterEn.SetStyle();
            dgvHeaderChapter.SetStyle();
            dgvAllSelect.SetStyle();

            viewChapter.IndicatorWidth = 30;
            viewChapterEn.IndicatorWidth = 30;

            //viewChapter.OptionsSelection.MultiSelect=true;
            //viewChapter.OptionsSelection.UseIndicatorForSelection = true;
            //viewChapter.OptionsSelection.CheckBoxSelectorField = "انتخاب";
            //viewChapter.OptionsSelection.EnableAppearanceFocusedRow = true;
            //viewChapter.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

        }

        public class ModelSelectFiles
        {
            //public Guid UniqueId { get; set; }
            public Guid Id { get; set; }
            /// <summary>
            /// Fa-1.mp4  |  En-1.mp4
            /// </summary>
            public string FileName { get; set; }

            public ModelSelectFiles()
            {
                // UniqueId = Guid.NewGuid();
            }
        }
        DataTable _dtAllSelect;
        public async Task SetFieldDgvAllSelect()
        {
            if (dgvAllSelect.ColumnCount() == 0)
            {
                _dtAllSelect = dgvAllSelect.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new KavoshGrid.modelColumn{Name = "حذف",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.delete},
                    new KavoshGrid.modelColumn{Name = "Id",Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "فصل",Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "قسمت",Type = typeof(int)},
                    new KavoshGrid.modelColumn{Name = "زبان",Type = typeof(string)},
                }, false, false, false);
                dgvAllSelect.MaxMinWidth("حذف", 50, 50);
                dgvAllSelect.HiddenColumn("Id");

                #region Relation

                var getHeaderChapter = await _multiHeaderChapter.GetByHeader(_headerId);
                var getDataList = getHeaderChapter.Select(s => new { s.Id, s.Code }).ToList();
                var getcmb = dgvAllSelect.AddGridToGrid(getDataList, "فصل", "Id", "Code");
                getcmb.HiddenColumn("Id");
                dgvAllSelect.GetViewBase.Columns["فصل"].SortOrder = ColumnSortOrder.Ascending;
                dgvAllSelect.GetViewBase.Columns["قسمت"].SortOrder = ColumnSortOrder.Ascending;

                #endregion

                #region Event

                dgvAllSelect.AddEventRowCellClick<Guid>(async obj =>
                {
                    // dgvAllSelect.DeleteRow(false, async () =>
                    //  {

                    //var getUniqueId = dgvAllSelect.GetValue<Guid>(dgvAllSelect.GetViewBase.FocusedRowHandle, "UniqueId");
                    try
                    {
                        var forRemoveFa = _selFilms.First(f => f.Id == obj && f.FileName.Contains("Fa"));
                        _selFilms.Remove(forRemoveFa);
                    }
                    catch (Exception e)
                    {
                        var forRemoveEn = _selFilms.First(f => f.Id == obj && f.FileName.Contains("En"));
                        _selFilms.Remove(forRemoveEn);
                    }

                    lblCountFilm.Text = _selFilms.Count.ToString();
                    await SetFieldDgvAllSelect();
                    LoadBeforeSelectFilm();
                    //if (_dtChapter != null) 
                    _dtChapter.Clear();
                    _dtChapterEn.Clear();

                    //  });

                }, "Id", "حذف");

                #endregion

            }

            #region Load Data

            //var getData = await _multiChapterDetail.GetAsync();
            var getData = await _multiChapterDetail.GetByHeader(_headerId);
            _dtAllSelect.Rows.Clear();
            foreach (var item in _selFilms)
            {
                if (getData.Any(f => f.Id == item.Id))
                {
                    var hc = getData.First(f => f.Id == item.Id);
                    _dtAllSelect.Rows.Add("", hc.Id, hc.HeaderChapterId, hc.Code, item.FileName);
                }
            }

            #endregion
        }
        public void LoadBeforeSelectFilm()
        {
            for (int i = 0; i < dgvChapter.RowCount(); i++)
            {
                var getId = dgvChapter.GetValue<Guid>(i, "Id");
                //var getUniqueId = dgvChapter.GetValue<Guid>(i, "UniqueId");
                if (_selFilms.Any(f => f.Id == getId) && _selFilms.Any(a => a.FileName.Contains("Fa")))
                {
                    dgvChapter.SetValue(i, "انتخاب", true);
                    // dgvChapter.SetValue(i, "UniqueId", true);
                }
            }
            for (int i = 0; i < dgvChapterEn.RowCount(); i++)
            {
                var getId = dgvChapterEn.GetValue<Guid>(i, "Id");
                //var getUniqueId = dgvChapterEn.GetValue<Guid>(i, "UniqueId");
                if (_selFilms.Any(f => f.Id == getId) && _selFilms.Any(a => a.FileName.Contains("En")))
                {
                    dgvChapterEn.SetValue(i, "انتخاب", true);
                    //  dgvChapterEn.SetValue(i, "UniqueId", true);
                }
            }
        }
        private DataTable _dtHeaderChapter;
        public async Task SetFieldLoadHeaderChapter(Guid headerId)
        {
            if (dgvHeaderChapter.ColumnCount() == 0)
            {
                _dtHeaderChapter = dgvHeaderChapter.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new KavoshGrid.modelColumn{Name = "Id",Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "فصل",Type = typeof(string)},
                    new KavoshGrid.modelColumn{Name = "Session",Type = typeof(long)},
                }, false, false, false);
                dgvHeaderChapter.HiddenColumn(new List<string>{"Id", "Session" });

                #region Event

                dgvHeaderChapter.AddEventRowCellClick<Guid>(async headerChapterId =>
                {
                    await SelectChapter(headerChapterId);
                }, "Id", "فصل");

                #endregion
            }

            #region Load Data

            _dtHeaderChapter.Rows.Clear();
            var getHeaderChapter = await _multiHeaderChapter.GetByHeader(_headerId);
            foreach (var item in getHeaderChapter.OrderByDescending(o => o.Code))
            {
                string title = "";
                if (!string.IsNullOrEmpty(item.Title))
                    title = $"  ({item.Title})";
                _dtHeaderChapter.Rows.Add(item.Id, item.Code + title, item.Code);
            }
            if (getHeaderChapter.Any())
            {
                if (getHeaderChapter.Count() > 1)
                    lblName.Text = _persianName + @" - " + $"(دارای {getHeaderChapter.Count()} فصل)";
                else
                    lblName.Text = _persianName;


                await SelectChapter(getHeaderChapter.OrderByDescending(o => o.Code).First().Id);
            }

            #endregion
        }
        long getChapterCode;
        private async Task SelectChapter(Guid headerChapterId)
        {
            _headerChapterId = headerChapterId;

            getChapterCode = dgvHeaderChapter.GetValue<long>("Session");
            await SetFieldLoadChapter(_headerChapterId);
            await SetFieldLoadChapterEn(_headerChapterId);
            PageFa.Caption = $"دوبله - فصل {getChapterCode}";
            PageEn.Caption = $"زیرنویس - فصل {getChapterCode}";
            LoadBeforeSelectFilm();
        }

        private DataTable _dtChapter, _dtChapterEn;
        public async Task SetFieldLoadChapter(Guid headerChapterId)
        {
            if (dgvChapter.ColumnCount() == 0)
            {
                _dtChapter = dgvChapter.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new KavoshGrid.modelColumn{Name = "پخش",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.gettingstarted},
                    new KavoshGrid.modelColumn{Name = "انتخاب",Type = typeof(bool),Object = KavoshGrid.enumObject.Checked},
                    new KavoshGrid.modelColumn{Name = "Id",Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "قسمت",Type = typeof(int)},
                }, false, false, false);
                dgvChapter.HiddenColumn("Id");
                dgvChapter.MaxMinWidth("پخش", 50, 50);
                dgvChapter.MaxMinWidth("انتخاب", 70, 70);

                #region Event


                dgvChapter.GetViewBase.SelectionChanged += async (s1, e1) =>
                {
                    var getSelected = dgvChapter.GetViewBase.GetSelectedRows().ToList();

                    if (getSelected.Count > 1)
                        foreach (int i in getSelected)
                        {
                            dgvChapter.SetValue(i, "انتخاب", true);
                        }
                    await GetSelectedForBackFa();
                };

                dgvChapterEn.AddEventRowCellClick<Guid>(obj =>
                {

                }, "Id", "عنوان فصل");

                dgvChapter.AddEventRowCellClick<bool>(async obj =>
                {
                    dgvChapter.SetValue("انتخاب", !obj);
                    await GetSelectedForBackFa();

                }, "انتخاب", "انتخاب");

                dgvChapter.AddEventRowCellClick<Guid>(async obj =>
                {
                    var getChapterNumber = dgvChapter.GetValue<long>("قسمت");
                    var getPart = (await _multiChapterDetail.GetByHeaderTranslate(new List<ModelGuidFaEn>
                    {
                        new ModelGuidFaEn { ChapterDetailId = obj, Type = "Fa" }
                    })).First();

                    var getFindFile = Directory.GetFiles(Path.GetDirectoryName(getPart.AddressFile)).ToList();
                    var getMp4 = getFindFile.First(w => w.Contains($"Fa-{getChapterNumber}"));

                    var getPotPlayer = GetPotPlayerPathFromRegistry();
                    var fullscreen = $"\"{getPotPlayer}\"";

                    ProcessStartInfo startInfo = new ProcessStartInfo
                    {
                        FileName = getMp4,
                        Arguments = fullscreen,  // مسیر ویدیو در کوتیشن
                        UseShellExecute = true
                    };
                    Process process = Process.Start(startInfo);
                    //Process.Start(startInfo);


                    await Task.Delay(2500);


                    // --- 4. کلید Enter رو به پنجره فعال بفرست ---

                    SendKeys.SendWait("{ENTER}");
                }, "Id", "پخش");

                #endregion

            }

            #region Load Data


            _dtChapter.Rows.Clear();

            var getDataChapterDetail =
                await _multiChapterDetail
                    .GetByHeaderChapter(headerChapterId); // فرض بر این است که متد GetAll وجود دارد

            var getDataChapterDetailFull =
                await _multiChapterDetail.GetByHeaderTranslate(getDataChapterDetail.Select(s => new ModelGuidFaEn
                {
                    ChapterDetailId = s.Id,
                    Type = "Fa"
                }).ToList());

            // _dtChapter.BeginInit();
            foreach (var item in getDataChapterDetailFull.OrderBy(o => o.ChapterCode))
            {
                var getDirFiles = Path.GetDirectoryName(item.AddressFile);
                var getFiles = Directory.GetFiles(getDirFiles).ToList();
                // مردگان جذف شد

                var getFind = getFiles.Where(w => w.Contains("Fa")).ToList();
                var getCreate = $"{"Fa"}-{item.ChapterCode}";

                if (getFind.Any(w => w.Contains(getCreate)))
                    _dtChapter.Rows.Add("", false, item.ChapterDetailsId, item.ChapterCode);


            }
            //_dtChapter.EndInit();

            #endregion
        }


        private string GetPotPlayerPathFromRegistry()
        {
            // باز کردن مستقیم Registry 64-bit
            using (RegistryKey baseKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (RegistryKey key = baseKey.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\App Paths\PotPlayerMini64.exe"))
            {
                if (key != null)
                {
                    // اول مقدار Path رو بگیر
                    //var pathValue1 = key.GetValue("Path").ToString() ;

                    string pathValue = key.GetValue("Path") as string;


                    if (!string.IsNullOrEmpty(pathValue))
                    {
                        string fullPath = pathValue.Trim().Trim('"');
                        //string fullPath = pathValue.Trim('"'); // حذف کوتیشن از اول و آخرpathValue;
                        fullPath = fullPath.Trim('"'); // حذف کوتیشن از اول و آخرpathValue;
                        if (System.IO.File.Exists(fullPath))
                            return fullPath;
                    }

                    // اگه Path نبود، مقدار پیش‌فرض رو بگیر
                    string defaultValue = key.GetValue("") as string;
                    if (!string.IsNullOrEmpty(defaultValue) && System.IO.File.Exists(defaultValue))
                        return defaultValue;
                }
            }

            return null;
        }
        public async Task SetFieldLoadChapterEn(Guid headerChapterId)
        {
            if (dgvChapterEn.ColumnCount() == 0)
            {
                _dtChapterEn = dgvChapterEn.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new KavoshGrid.modelColumn{Name = "پخش",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.gettingstarted},
                    new KavoshGrid.modelColumn{Name = "انتخاب",Type = typeof(bool),Object = KavoshGrid.enumObject.Checked},
                    new KavoshGrid.modelColumn{Name = "Id",Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "قسمت",Type = typeof(int)},
                }, false, false, false);
                dgvChapterEn.HiddenColumn("Id");
                dgvChapterEn.MaxMinWidth("پخش", 50, 50);
                dgvChapterEn.MaxMinWidth("انتخاب", 70, 70);

                #region Event

                dgvChapterEn.GetViewBase.SelectionChanged += async (s1, e1) =>
                {
                    //dgvChapter.MultiCheckedRow();

                    var getSelected = dgvChapterEn.GetViewBase.GetSelectedRows().ToList();
                    //if (e1.Action==CollectionChangeAction.Add)
                    //for (var i = 0; i < dgvChapter.ColumnCount(); i++)
                    //{
                    //    dgvChapter.SetValue(i, "انتخاب", false);
                    //}
                    if (getSelected.Count > 1)
                        foreach (int i in getSelected)
                        {
                            dgvChapterEn.SetValue(i, "انتخاب", true);
                        }
                    // viewChapter.RefreshData();

                    await GetSelectedForBackEn();
                };

                dgvChapterEn.AddEventRowCellClick<Guid>(obj =>
                {

                }, "Id", "عنوان فصل");

                dgvChapterEn.AddEventRowCellClick<bool>(async obj =>
                {
                    dgvChapterEn.SetValue("انتخاب", !obj);
                    await GetSelectedForBackEn();

                }, "انتخاب", "انتخاب");
                #endregion

            }

            #region Load Data


            _dtChapterEn.Rows.Clear();

            var getDataChapterDetail =
                await _multiChapterDetail
                    .GetByHeaderChapter(headerChapterId); // فرض بر این است که متد GetAll وجود دارد

            var getDataChapterDetailFull =
                await _multiChapterDetail.GetByHeaderTranslate(getDataChapterDetail.Select(s => new ModelGuidFaEn
                {
                    ChapterDetailId = s.Id,
                    Type = "En"
                }).ToList());

            foreach (var item in getDataChapterDetailFull.OrderBy(o => o.ChapterCode))
            {
                var getDirFiles = Path.GetDirectoryName(item.AddressFile);
                var getFiles = Directory.GetFiles(getDirFiles).ToList();

                var getFind = getFiles.Where(w => w.Contains("En")).ToList();
                var getCreate = $"{"En"}-{item.ChapterCode}";

                if (getFind.Any(w => w.Contains(getCreate)))
                    _dtChapterEn.Rows.Add("", false, item.ChapterDetailsId, item.ChapterCode);
            }
            PageEn.PageEnabled = _dtChapterEn.Rows.Count > 0;

            #endregion
        }
        private async void btnAllSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvChapter.RowCount(); i++)
            {
                dgvChapter.SetValue(i, "انتخاب", true);
            }
            await GetSelectedForBackFa();
        }

        private async void btnRemlSelect_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < dgvChapter.RowCount(); i++)
            {
                dgvChapter.SetValue(i, "انتخاب", false);
            }
            for (int i = 0; i < dgvChapterEn.RowCount(); i++)
            {
                dgvChapterEn.SetValue(i, "انتخاب", false);
            }
            await GetSelectedForBackFa();
            await GetSelectedForBackEn();
        }

        public async Task<List<ModelSelectFiles>> GetSelectedForBackFa()
        {
            //  List<Guid> lstSelectedGuids = [];
            for (int i = 0; i < dgvChapter.RowCount(); i++)
            {
                var getId = dgvChapter.GetValue<Guid>(i, "Id");
                //var getUniqueId = dgvChapter.GetValue<Guid>(i, "UniqueId");
                var getChapterCode = dgvChapter.GetValue<long>(i, "قسمت");
                if (dgvChapter.GetValue<bool>(i, "انتخاب"))
                {
                    if (!_selFilms.Any(a => a.Id == getId && a.FileName.Contains("Fa")))
                    {
                        var uniqueId = Guid.NewGuid();
                        _selFilms.Add(new ModelSelectFiles
                        {
                            Id = getId,
                            //UniqueId = uniqueId,
                            FileName = $"Fa-{getChapterCode}"
                        });
                        //dgvChapter.SetValue(i, "UniqueId", uniqueId);
                    }
                }
                else
                {
                    if (_selFilms.Any(a => a.Id == getId && a.FileName.Contains("Fa")))
                        _selFilms.Remove(_selFilms.First(f => f.Id == getId && f.FileName.Contains("Fa")));
                }
            }

            lblCountFilm.Text = _selFilms.Count.ToString();
            await SetFieldDgvAllSelect();

            return _selFilms;
        }
        public async Task<List<ModelSelectFiles>> GetSelectedForBackEn()
        {
            //  List<Guid> lstSelectedGuids = [];
            for (int i = 0; i < dgvChapterEn.RowCount(); i++)
            {
                var getId = dgvChapterEn.GetValue<Guid>(i, "Id");
                //var getUniqueId = dgvChapterEn.GetValue<Guid>(i, "UniqueId");
                var getChapterCode = dgvChapterEn.GetValue<long>(i, "قسمت");
                if (dgvChapterEn.GetValue<bool>(i, "انتخاب"))
                {
                    if (!_selFilms.Any(a => a.Id == getId && a.FileName.Contains("En")))
                    {
                        // var uniqueId = Guid.NewGuid();
                        _selFilms.Add(new ModelSelectFiles
                        {
                            Id = getId,
                            //UniqueId = uniqueId,
                            FileName = $"En-{getChapterCode}"
                        });
                        // dgvChapterEn.SetValue(i, "UniqueId", uniqueId);
                    }
                }
                else
                {
                    if (_selFilms.Any(a => a.Id == getId && a.FileName.Contains("En")))
                        _selFilms.Remove(_selFilms.First(f => f.Id == getId && f.FileName.Contains("En")));
                }
            }

            lblCountFilm.Text = _selFilms.Count.ToString();
            await SetFieldDgvAllSelect();

            return _selFilms;
        }



        private void btnRemlSelectAll_Click(object sender, EventArgs e)
        {
            foreach (DataRow r in _dtAllSelect.Rows)
            {
                var getId = r["Id"].ToGuid();
                _selFilms.Remove(re => re.Id == getId);
                //r.Delete();
            }
            //_selFilms.Clear();
            lblCountFilm.Text = _selFilms.Count.ToString();



            _dtAllSelect.Rows.Clear();
            if (_dtChapter != null) _dtChapter.Rows.Clear();
            if (_dtChapterEn != null) _dtChapterEn.Rows.Clear();
        }

        private string dropFile = "";
        private async void dgvChapter_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files != null && files.Length != 1)
            {
                ClassMessageBox.ShowMSG("فقط یک ویدئو ارسال نمایید", Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار);
                return;
            }

            if (Path.GetExtension(files[0]).ToLower() != ".mp4")
            {
                ClassMessageBox.ShowMSG("فقط فایل mp4 قابل قبول است", Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار);
                return;
            }

            dropFile = files[0];


            var getMaxCode = await _multiChapterDetail.GetMaxCode(_headerChapterId);
            var getHeader = await _multiHeader.GetById(_headerId);
            var getFolder = await _multiHeader.LocationForImage(_headerId);

            var getFileModel = new List<ClassFileManage.ModelFile>();

            var getCodeFilm = getHeader.Num;

            //getChapterCode = dgvHeaderChapter.GetValue<long>("فصل");
            var getCodeHeaderChapter = getChapterCode;

            var getFileInfo = Path.GetExtension(dropFile);

            var getAddressTo = Path.Combine(getFolder, getCodeFilm.ToString(), getCodeHeaderChapter.ToString(), "Fa-" + getMaxCode + getFileInfo);

            var headerNamePersian = getHeader.NamePersian + "-" + "Fa-" + getMaxCode;
            getFileModel.Add(new ClassFileManage.ModelFile
            {
                ChapterHeaderId = _headerChapterId,
                Name = headerNamePersian,
                From = dropFile,
                To = getAddressTo,
            });

            var getMultiArg = ClassFillToCopy.FillToCopy(getFileModel) + " " + Guid.Empty + " " + getCodeFilm + " " + "AddFast";
            var getQues = ClassMessageBox.ShowMSGQues(headerNamePersian + Environment.NewLine + "اضافه شود", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);
            if (getQues)
            {
                RunExecutable(getMultiArg);
            }

        }
        public void RunExecutable(string arguments)
        {
            var getAddress = Application.StartupPath;
            var process = new Process();
            process.StartInfo.FileName = getAddress + @"IranKavosh.Layered.FileCopy.exe";
            //process.StartInfo.FileName = @"C:\Users\Mojtaba\source\repos\IranKavosh\IranKavosh.Layered.FileCopy\bin\Debug\net9.0-windows\IranKavosh.Layered.FileCopy.exe";
            process.StartInfo.Arguments = arguments;
            process.StartInfo.UseShellExecute = false;
            //process.StartInfo.CreateNoWindow = true;
            process.Start();
            //process.WaitForExit();
        }
        private void dgvChapter_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
    }
}