using DevExpress.Data;
using DevExpress.Xpf.Editors;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraGrid.Views.Tile.ViewInfo;
using DevExpress.XtraReports.UI;
using DocumentFormat.OpenXml;
using EF_Model.Table;
using IranKavosh.Layered.FileCopy;
using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using IranKavosh.Layered.WinUI.Class;
using IranKavosh.Layered.WinUI.ClassReport;
using IranKavosh.Layered.WinUI.Report;
using Microsoft.IdentityModel.Tokens;
using MyCom.Class;
using MyCom.Object;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MyCom.Class.ClassFileManage;
using ClassEncryptDecrypt = MyCom.Class.ClassEncryptDecrypt;

namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class FrmShowVideo : DevExpress.XtraEditors.XtraForm
    {
        private readonly ClsFont _clsFont = new(ClsFont.enumFont.samimBoldFD, true);
        private readonly ClsFont _clsFontEn = new(ClsFont.enumFont.samimBold, true);

        private static readonly HttpClient HttpClient = new();
        private readonly PersonalHttpService _personal = new(HttpClient);
        private PersonalGroupHttpService _personalGroup = new(HttpClient);

        private readonly MultimediaHeaderHttpService _multiHeader = new(HttpClient);
        private readonly MultimediaHeaderChapterHttpService _multimediaHeaderChapter = new(HttpClient);
        private readonly MultimediaChapterDetailHttpService _multiChapterDetail = new(HttpClient);
        private readonly MultiActorHttpService _multiActor = new(HttpClient);
        private readonly MultiLocationHttpService _multiLocation = new(HttpClient);
        private readonly MultiCountryHttpService _multiCountry = new(HttpClient);
        private readonly MultiGenreHttpService _multiGenre = new(HttpClient);
        private readonly HddMemoryHttpService _hddMemoryHttp = new(HttpClient);
        private readonly HddMemoryPersonHttpService _hddMemoryPerson = new(HttpClient);
        private readonly DefinitiveAccountHttpService _definitiveAccount = new(HttpClient);

        private Guid _selMemoryId;
        public FrmShowVideo()
        {
            InitializeComponent();
            ClassUsbDriveInfoProvider.ConnectedDisconnected(this, SetFieldDgvFlash, SetFieldDgvFlash);
            grpSelectFlash.CustomButtonClick += (s1, e1) =>
            {
                if (e1.Button.Properties.Caption == "بارگذاری")
                {
                    SetFieldDgvFlash();
                }
            };
            grpSelectFilms.CustomButtonClick += (s1, e1) =>
            {
                if (e1.Button.Properties.Caption == "خالی کردن لیست")
                {
                    lstSelectFilm.Clear();
                    _dtFilms.Rows.Clear();
                    btnFindMobile.Enabled = btnCopyToFlash.Enabled = false;
                    lblUSBDrive.Text = @"محل ذخیره";

                }
                else if (e1.Button.Properties.Caption == "حذف انتخاب شده")
                {
                    List<Guid> lstId = new List<Guid>();
                    for (int i = 0; i < dgvFilms.RowCount(); i++)
                    {
                        var getValue = dgvFilms.GetValue<bool>(i, "انتخاب");
                        if (getValue)
                        {

                            var getId = dgvFilms.GetValue<Guid>(i, "Id");
                            lstId.Add(getId);
                            // dgvFilms.DeleteRow(false, i);
                        }
                    }

                    foreach (Guid id in lstId)
                    {
                        lstSelectFilm.RemoveAll(r => r.Id == id);
                        DataRow row = _dtFilms.AsEnumerable()
                            .First(row => row.Field<Guid>("Id") == id);
                        _dtFilms.Rows.Remove(row);

                    }

                    btnCopyToFlash.Text = @$"انتقال ({0}) به =>";
                    btnCopyToFlash.Enabled = false;

                    btnFindMobile.Text = @$"موبایل ({0})";
                    btnFindMobile.Enabled = false;
                }

            };


        }
        IEnumerable<ModelChapterDetailsFull> _getSelectOnFilmByFilm;
        private async void FrmShowVideo_Load(object sender, EventArgs e)
        {
            SetFont();
            _getCntOldFilms = ClassIkOld.GetGroupedDataOld();

            getHeader = (await _multiHeader.GetByModel());
            //getHeader = (await _multiHeader.GetByModel()).Where(w=>w.IsAnimated).ToList();

            await GetHeader();
            await FunAddressAllImage();

            new Task(async () =>
            {
                while (true)
                {
                    try
                    {
                        await lblUSBDrive.InvokeAsync(() =>
                        {
                            if (lblUSBDrive.Text != @"محل ذخیره")
                            {
                                var enabled = Directory.Exists(lblUSBDrive.Text);
                                btnCopyToFlash.Enabled = enabled;
                            }
                            else
                                btnCopyToFlash.Enabled = false;

                        });
                    }
                    catch (Exception exception)
                    {

                    }

                    await Task.Delay(1000);
                }

            }).Start();

            barOpenDir.ItemClick += async (s1, e1) =>
            {
                var getData = barOpenDir.Tag as ModelTileView;
                var getDir1 = await _multiLocation.GetById(getData.LocationId.Value);
                var getDir = getDir1.DirShare_PCName + "\\" + getData.Num;
                //var getDir11 = getDir1.DirShare_PCIP+"\\"+ getData.Num.ToString();
                Process.Start("explorer.exe", getDir);
            };

            barCreatePoster.ItemClick += async (s1, e1) =>
            {
                this.WaitDownPage(() =>
                {
                    _ = CreatePoster();
                });
            };

            tileView1.ItemCustomize += (s1, e1) =>
            {
                var rowData = tileView1.GetRow(e1.RowHandle) as ModelTileView;
                if (!rowData.IsAnimated && !rowData.IsAnime)
                {
                    var animElements = e1.HtmlElement?.FindElementsByClass("text-anim");
                    if (animElements != null)
                    {
                        foreach (var element in animElements)
                        {
                            element.Hidden = true;
                        }
                    }
                }
                else
                {
                    rowData.AnimOrAnimation = rowData switch
                    {
                        { IsAnimated: true, IsAnime: false } => "انیمیشن",
                        { IsAnimated: false, IsAnime: true } => "انیمه",
                        _ => rowData.AnimOrAnimation
                    };
                }

                //new Task(() =>
                //{
                if (rowData.Num == 5316)
                {

                }
                if (!string.IsNullOrEmpty(rowData.LocationFull))
                    {
                        
                        var getCount = Directory
                            .GetFiles(rowData.LocationFull, "*.*", SearchOption.AllDirectories)
                            .Any(f => f.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase)
                                      || f.EndsWith(".mkv", StringComparison.OrdinalIgnoreCase));
                       
                        if (!getCount)
                        {
                            if (!rowData.NamePersian.Contains("فعلا موجود نیست"))
                            {
                                rowData.NamePersian += " (فعلا موجود نیست)";
                            }

                            var bottomText = e1.HtmlElement?
                                .FindElementsByClass("bottom-text")
                                .FirstOrDefault();

                            if (bottomText != null)
                                bottomText.ClassName = "bottom-text-no-video";
                        }
                    }
                //}).Start();
            

            };

            searchControl.QueryIsSearchColumn += (s1, e1) =>
            {
                if (e1.FieldName == "Num")
                {
                    List<ModelTileView> modelTileViews = new List<ModelTileView>();

                    var itemSize = tileView1.OptionsTiles.ItemSize;
                    var pageSize = tileView1.GridControl.ClientSize;

                    int horizontalCount = pageSize.Width / itemSize.Width;
                    int verticalCount = pageSize.Height / itemSize.Height;
                    int totalVisible = horizontalCount * verticalCount;

                    int topIndex = (tileView1 as IDataControllerVisualClient)?.TopRowIndex ?? 0;

                    for (int i = topIndex; i <= topIndex + totalVisible + 5 && i < tileView1.RowCount; i++)
                    {
                        if (tileView1.GetRow(i) is ModelTileView item)
                            modelTileViews.Add(item);
                    }

                    RefreshTileAfterNewTile(modelTileViews);
                }
            };
            dgvVideo.SizeChanged += (s1, e1) =>
            {
                List<ModelTileView> modelTileViews = new List<ModelTileView>();

                var itemSize = tileView1.OptionsTiles.ItemSize;
                var pageSize = tileView1.GridControl.ClientSize;

                int horizontalCount = pageSize.Width / itemSize.Width;
                int verticalCount = pageSize.Height / itemSize.Height;
                int totalVisible = horizontalCount * verticalCount;

                int topIndex = (tileView1 as IDataControllerVisualClient)?.TopRowIndex ?? 0;

                for (int i = topIndex; i <= topIndex + totalVisible + 5 && i < tileView1.RowCount; i++)
                {
                    if (tileView1.GetRow(i) is ModelTileView item)
                        modelTileViews.Add(item);
                }
                RefreshTileAfterNewTile(modelTileViews);
            };

            dgvVideo.AddEventRowCellClickTileView<ModelTileView>(async item =>
            {
                var getNamePersian = tileView1.GetRowCellValue(tileView1.FocusedRowHandle, "NamePersian").ToString();

                new FrmSelectChapter(item.Id, $"(  {getNamePersian}  )", lstSelectFilm).ShowDialog();
                //var get = lstSelectFilm.Count;

                //IEnumerable<ModelChapterDetailsFull> getTest;
                _getSelectOnFilmByFilm = await _multiChapterDetail.GetByHeaderTranslate(lstSelectFilm.Select(s => new ModelGuidFaEn
                {
                    ChapterDetailId = s.Id,
                    Type = s.FileName
                }).ToList());
                SetFielddgvFilms(_getSelectOnFilmByFilm.OrderBy(o => o.HeaderCode).ThenBy(t => t.ChapterCode).ThenBy(t => t.HeaderChapterCode).ToList());
            });

            dgvVideo.AddEventRowCellRightClickTileView<ModelTileView>(async item =>
            {
                barOpenDir.Tag = item;
                barCreatePoster.Tag = item;
                mnuVideo.ShowPopup(MousePosition);
            });

            dgvVideo.AddEventGetViewTile<ModelTileView>(obj =>
            {
                // listBox1.Items.Clear();
                //  tileView1.BeginDataUpdate();
                foreach (ModelTileView view in loadAllVideo)
                {
                    // view.Image?.Dispose();
                    view.Image = null;
                }
                // tileView1.RefreshData();

                RefreshTileAfterNewTile(obj);
                //  tileView1.EndDataUpdate();
            }, 20);

            await GetHeader();
            //this.Activated += (s1, e1) =>
            //{
            //    this.WindowState = FormWindowState.Maximized;
            //};

        }
        //class ModelHtml
        //{
        //    public int Code { get; set; }
        //    public string Type { get; set; }
        //}
        public class ModelTileView
        {
            public Guid Id { get; set; }
            public string NamePersian { get; set; }
            public Guid? LocationId { get; set; }
            public string Location { get; set; }
            public string LocationFull { get; set; }
            public int LocationCountVideo { get; set; }
            public string Country { get; set; }
            public long Num { get; set; }
            public string MultimediaGroup { get; set; } // سینمایی | سریال
            public string Gener { get; set; }
            public Image Image { get; set; }
            public bool IsAnimated { get; set; }
            public bool IsAnime { get; set; }
            public string AnimOrAnimation { get; set; }
        }

        List<ModelTileView> getAllImage;

        public void SetFont()
        {
            if (dgvFlash != null)
            {
                dgvFlash.SetStyle();
                dgvFlash.GetViewBase.OptionsView.RowAutoHeight = false;
                dgvFlash.GetViewBase.RowHeight = 25;
                dgvFilms.SetStyle();
                dgvFilms.GetViewBase.OptionsView.RowAutoHeight = false;
                dgvFilms.GetViewBase.RowHeight = 25;
                dgvFilms.GetViewBase.IndicatorWidth = 25;
                _clsFontEn.ChangeFont(dgvFlash, 12);
                _clsFont.ChangeFont(dgvFilms, 12);
                _clsFont.ChangeFont(mnuVideo, 10);
                _clsFont.ChangeFont(btnRefreshList);
                _clsFont.ChangeFont(btnNewFilm);
                _clsFont.ChangeFont(btnIraniNewSerial);

                dgvFlash.GetViewBase.OptionsView.ColumnAutoWidth = false;
            }
        }
        private static bool IsWithinTolerance(long actualSize, long expectedSize, double tolerancePercent)
        {
            if (actualSize == expectedSize)
                return true;
            if (expectedSize == 0)
                return actualSize == 0;

            var difference = Math.Abs(actualSize - expectedSize);
            var percentDifference = (double)difference / expectedSize * 100;

            return percentDifference <= tolerancePercent;
        }
        private DataTable _dtFlash;
        public void SetFieldDgvFlash()
        {
            lblUSBDrive.Text = "محل ذخیره";
            if (dgvFlash.ColumnCount() == 0)
            {
                _dtFlash = dgvFlash.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new() {Name = "Id",},
                    new() {Name = "باز کردن",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.Save2},
                    new() {Name = "بررسی",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.Refresh2},
                    new() {Name = "نام"},
                    new() {Name = "درایو"},
                    new() {Name = "حجم کل",RightToLeft = false},
                    new() {Name = "فضای آزاد",RightToLeft = false},
                    new() {Name = "FreeSpace",Type = typeof(ulong)},
                    new() {Name = "Size",Type = typeof(ulong)},
                    new() {Name = "Status",Type = typeof(bool)},
                    new() {Name = "GUID"},
                    new() {Name = "مشتری"}, // 12
                    new() {Name = "حساب",Type = typeof(long),PriceActive = true}, // 13
                    new() {Name = "تخصیص",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.Add_25},

                }, false, false, false);
                //dgvFlash.HiddenColumn([ "GUID"]);
                dgvFlash.HiddenColumn(["Id", "FreeSpace", "Size", "Status", "GUID"]);
                dgvFlash.MaxMinWidth("بررسی", 35, 35);
                dgvFlash.MaxMinWidth("باز کردن", 35, 35);
                dgvFlash.MaxMinWidth("تخصیص", 35, 35);

                #region Event

                dgvFlash.z_RowCellDoubleClick += (s1, e1) =>
                {
                    var obj = dgvFlash.GetValue<string>("درایو");
                    _selMemoryId = dgvFlash.GetValue<Guid>("Id");

                    lblUSBDrive.Text = obj + ":\\KavoshVideo";
                    btnFindMobile.Enabled = btnCopyToFlash.Enabled = true;
                    Directory.CreateDirectory(obj + ":\\KavoshVideo");
                };

                dgvFlash.AddEventRowCellClick<Guid>(obj =>
                {
                    FrmPerson frmPerson = new FrmPerson(obj);
                    frmPerson.ShowDialog();
                    SetFieldDgvFlash();
                }, "Id", "تخصیص");

                //dgvFlash.AddEventRowCellClick<string>(obj =>
                //{
                //    lblUSBDrive.Text = obj + ":\\KavoshVideo";
                //    Directory.CreateDirectory(obj + ":\\KavoshVideo");
                //}, "درایو", "درایو");

                dgvFlash.GetViewBase.RowCellStyle += (s1, e1) =>
                {
                    var getFreeSpace = dgvFlash.GetValue<long>(e1.RowHandle, "FreeSpace");
                    //var getStatus = dgvFlash.GetValue<bool>(e1.RowHandle, "Status");
                    var getSize = dgvFlash.GetValue<long>(e1.RowHandle, "Size");
                    if (getSize > 0)
                        if (e1.Column.FieldName is "درایو" or "فضای آزاد")
                        {
                            if (getSize < getFreeSpace)
                                e1.Appearance.BackColor = ClsUI.Ok;
                            else
                                e1.Appearance.BackColor = ClsUI.NotOk;
                        }

                };

                dgvFlash.GetViewBase.RowCellClick += (s1, e1) =>
                {
                    if (e1.Column.FieldName == "باز کردن")
                    {
                        var getDir = dgvFlash.GetValue<string>("درایو") + ":\\";
                        Process.Start("explorer.exe", getDir);
                    }
                };

                #endregion
            }
            #region Load Data
            _dtFlash.Rows.Clear();
            Task.Run(() =>
            {
                var getUsb = ClassUsbDriveInfoProvider.GetUsbDrives();

                dgvFlash.Invoke((MethodInvoker)(async () => _dtFlash.Rows.Clear()));
                dgvFlash.Invoke((MethodInvoker)(async () =>
                 {
                     foreach (var usb in getUsb)
                     {
                         HddMemory hddMemory = new HddMemory
                         {
                             StableSerial = usb.StableSerial,
                             TotalSize = Convert.ToInt64(usb.TotalSizeUse)
                         };

                         #region بررسی وجود درایوها

                         //var getOldFlash = (await _hddMemoryHttp.GetAsync()).Any(a => a.StableSerial == usb.StableSerial);
                         var getOldFlash = (await _hddMemoryHttp.GetAsync()).Any(a => a.StableSerial == usb.StableSerial && a.TotalSize == Convert.ToInt64(usb.TotalSizeUse));

                         #endregion

                         if (!getOldFlash)
                             await _hddMemoryHttp.AddByStableSerialAsync(hddMemory);

                         var getMemoryId = await _hddMemoryHttp.GetAsync();
                         //var memoryId = getMemoryId.First(f => f.StableSerial == usb.StableSerial).Id;
                         var memoryId = getMemoryId.First(f => f.StableSerial == usb.StableSerial).Id;

                         var getForStatus = usb.FreeSpaceUse - _sumSelFilms;

                         //bool allowAddUSBNew = true;
                         //foreach (DataRow row in _dtFlash.Rows)
                         //{
                         //    var g2 = row[2].ToString().ToLower();
                         //    var g3 = row[3].ToString().ToLower();
                         //    if (usb.VolumeLabel.ToLower() == g2 && usb.DriveLetter.ToString().ToLower() == g3)
                         //    {
                         //        allowAddUSBNew = false;

                         //    }
                         //}
                         //if (allowAddUSBNew)
                         //if (getUsb.Count > _dtFlash.Rows.Count)
                         if (getUsb.Count > _dtFlash.Rows.Count)
                         {
                             if (usb.TotalSizeUse < 499161600)
                             {
                                 continue;
                             }
                             var getPerson = (await _hddMemoryPerson.GetAsync()).ToList();
                             var getPerson2 = "";
                             long getDebtor = 0;

                             var checkFlash = getMemoryId.Any(f =>
                                     f.StableSerial == usb.StableSerial &&
                                     IsWithinTolerance(f.TotalSize, Convert.ToInt64(usb.TotalSizeUse), tolerancePercent: 1) // 1% تحمل
                             );
                             //if (getPerson.Any(a => a.HddMemoryId == memoryId))
                             if (getPerson.Any(a => a.HddMemoryId == memoryId) && checkFlash)
                             {
                                 var personalId = getPerson.First(a => a.HddMemoryId == memoryId).PersonalId;
                                 getPerson2 = (await _personal.GetById(personalId)).FullName;
                                 getDebtor = (await _definitiveAccount.GetByPersonCalc(personalId));

                             }

                             _dtFlash.Rows.Add(
                                 memoryId,
                                 "",
                                 "",
                                 usb.VolumeLabel, //2
                                 usb.DriveLetter.ToString().Replace(":", ""), //3
                                 usb.TotalSize,
                                 usb.FreeSpace,
                                 usb.FreeSpaceUse,
                                 _sumSelFilms,
                                 getForStatus >= 1000,
                                 usb.StableSerial, // اضافه کردن سریال پایدار,
                                 getPerson2, // 12
                                 getDebtor,
                                 ""
                             );
                         }


                     }

                     await dgvFlash.InvokeAsync(() => { dgvFlash.SetFieldSizeColumn(); });

                     //SetFieldDgvFlash(); //Test
                 }));
            });

            #endregion
        }

        class ModelForCopyFile
        {
            public string FullName { get; set; }
            public string FileName { get; set; }
            public string Exten { get; set; }

        }
        private DataTable _dtFilms;
        ulong _sumSelFilms;
        public void SetFielddgvFilms(List<ModelChapterDetailsFull> chapterDetailsFulls)
        {
            if (dgvFilms.ColumnCount() == 0)
            {
                _dtFilms = dgvFilms.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new() { Name = "انتخاب",Object = KavoshGrid.enumObject.Checked},
                    new() { Name = "Id",Type = typeof(Guid)},
                    new() { Name = "نام" },
                    new() { Name = "فصل",Type = typeof(int)  },
                    new() { Name = "قسمت", Type = typeof(int) },
                    new() { Name = "آدرس", Type = typeof(string) },
                    new() { Name = "Address", Type = typeof(string) },
                    new() { Name = "Size", Type = typeof(long) },
                    new() { Name = "سایز", Type = typeof(string) },
                    new() { Name = "زبان", Type = typeof(string) },
                }, false, false, true);
                dgvFilms.EnableMultiSelectColumn();

                dgvFilms.GetViewBase.OptionsSelection.MultiSelect = true;
                dgvFilms.GetViewBase.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

                dgvFilms.HiddenColumn(["Id", "آدرس", "Address", "Size"]);
                dgvFilms.MaxMinWidth("انتخاب", 40, 40);
                dgvFilms.MaxMinWidth("فصل", 40, 40);
                dgvFilms.MaxMinWidth("قسمت", 40, 40);
                dgvFilms.MaxMinWidth("سایز", 60, 60);
                dgvFilms.MaxMinWidth("نام", 110, 110);
                dgvFilms.MaxMinWidth("زبان", 60, 60);

                dgvFilms.AddSummaryItem("Size", "نام", "{0}", SummaryItemType.Custom);
                dgvFilms.AddSummaryItem("Size", "سایز", "{0}", SummaryItemType.Custom, false);

                #region Event

                // dgvFilms.AddSummaryItem("Size","سایز","0",SummaryItemType.Sum,true);
                // double sumSize = 0;

                dgvFilms.GetViewBase.SelectionChanged += async (s1, e1) =>
                {
                    var getSelected = dgvFilms.GetViewBase.GetSelectedRows().ToList();

                    if (getSelected.Count > 1)
                    {
                        int i;
                        for (i = 0; i < dgvFilms.RowCount(); i++)
                        {
                            dgvFilms.SetValue(i, "انتخاب", false);
                        }
                        foreach (int i2 in getSelected)
                        {
                            dgvFilms.SetValue(i2, "انتخاب", true);
                        }

                        var getISel = getSelected.Count;
                        btnCopyToFlash.Text = @$"انتقال ({getISel}) به =>";
                        btnCopyToFlash.Enabled = getISel != 0;

                        btnFindMobile.Text = @$"موبایل ({getISel})";
                        btnFindMobile.Enabled = getISel != 0;
                    }
                    // await GetSelectedForBackFa();
                };
                dgvFilms.AddEventRowCellClick<bool>(obj =>
                {
                    int totalSelected = 0;
                    dgvFilms.SetValue("انتخاب", !obj);
                    dgvFilms.GetViewBase.UpdateSummary();

                    for (int i = 0; i < dgvFilms.RowCount(); i++)
                    {
                        var getValue = dgvFilms.GetValue<bool>(i, "انتخاب");
                        if (getValue)
                            totalSelected++;
                    }

                    btnCopyToFlash.Text = $"انتقال ({totalSelected}) به =>";
                    btnCopyToFlash.Enabled = totalSelected != 0;

                    btnFindMobile.Text = $"موبایل ({totalSelected})";
                    btnFindMobile.Enabled = totalSelected != 0;

                }, "انتخاب", "انتخاب");
                ulong sumAllSizeFilms = 0;
                dgvFilms.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
                {

                    var conE = e1.ConvertItemSummary();
                    var getFieldName = e1.Row as DataRowView;
                    BandedGridView gridView = (BandedGridView)s1;


                    _sumSelFilms = (ulong)dgvFilms.GetAllCheckedRow("انتخاب", "Size");

                    var allSel = gridView.GetSelectedRows();
                    var gR = e1.RowHandle;
                    switch (e1.SummaryProcess)
                    {
                        case CustomSummaryProcess.Start:
                            sumAllSizeFilms = 0;
                            break;
                        case CustomSummaryProcess.Calculate:
                            if (e1.FieldValue != null)
                            {
                                if (conE.FieldName.Equals("Size"))
                                    // if (allSel.Contains(gR))
                                    sumAllSizeFilms += (ulong)Convert.ToInt64(e1.FieldValue.ToString());
                            }
                            break;
                        case CustomSummaryProcess.Finalize:
                            if (conE.Tag.Equals("سایز"))
                            {
                                e1.TotalValue = _sumSelFilms.FormatSize();
                            }
                            else if (conE.Tag.Equals("نام"))
                            {
                                e1.TotalValue = sumAllSizeFilms.FormatSize();
                            }

                            #region Send Size To Flash

                            if (_dtFlash != null)
                                foreach (DataRow row in _dtFlash.Rows)
                                {
                                    row["Size"] = _sumSelFilms;
                                }

                            #endregion
                            break;

                    }

                };
                dgvFilms.GetViewBase.CustomDrawFooterCell += (s1, e1) =>
                {
                    if (e1.Column.FieldName == "نام")
                        e1.Appearance.ForeColor = Color.FromArgb(36, 88, 235);
                    else if (e1.Column.FieldName == "سایز")
                        e1.Appearance.ForeColor = Color.FromArgb(255, 0, 253);
                };

                #endregion
            }
            _dtFilms.Rows.Clear();

            #region Load Data

            foreach (var ch in chapterDetailsFulls)
            {
                string addressForCopy = "";
                string lang = "";
                var getDir = Path.GetDirectoryName(ch.AddressFile);
                var getFile = Directory.GetFiles(getDir);
                var getFile1 = getFile.Select(s => new ModelForCopyFile
                {
                    FullName = s,
                    FileName = Path.GetFileNameWithoutExtension(s),
                    Exten = Path.GetExtension(s),

                }).ToList();

                if (getFile1.Any(a => a.FileName == "Fa-" + ch.ChapterCode.ToString()))
                    addressForCopy = getFile1.First(a => a.FileName == "Fa-" + ch.ChapterCode.ToString()).FullName;
                else if (getFile1.Any(a => a.FileName == "En-" + ch.ChapterCode.ToString()))
                    addressForCopy = getFile1.First(a => a.FileName == "En-" + ch.ChapterCode.ToString()).FullName;


                if (File.Exists(addressForCopy))
                {
                    var getSize = ((ulong)new FileInfo(addressForCopy).Length).FormatSize();
                    var getSizeForCopy = (ulong)new FileInfo(addressForCopy).Length;
                    _dtFilms.Rows.Add(false, ch.ChapterDetailsId, ch.HeaderName, ch.HeaderChapterCode, ch.ChapterCode, ch.AddressFile, addressForCopy, getSizeForCopy, getSize, ch.Type);
                }
                else
                {
                    // عدم وجود فیلم
                }
            }

            #endregion

        }

        public async Task FunAddressAllImage()
        {
            //var getHeader= await _multiHeader.GetAsync();
            var getlocation = await _multiLocation.GetAsync();

            getAllImage = getHeader.Select(s =>
            {
                var locationSmall = Path.Combine(getlocation.First(f => f.Id == s.LocationId).DirShare_PCIP, s.Num.ToString(), "ImageSmall.jpg");

                var location = Path.Combine(getlocation.First(f => f.Id == s.LocationId).DirShare_PCIP, s.Num.ToString(), "Image.jpg");
                if (!File.Exists(locationSmall))
                {
                    //location.LoadBitmapSize().Save(locationSmall);
                    var loadBitmapSizeLow = location.LoadBitmapSizeLow();
                    loadBitmapSizeLow.Save(locationSmall);
                }
                return new ModelTileView
                {
                    Id = s.Id,
                    Location = locationSmall
                    //  Location = location
                };
            }).ToList();
        }
        List<ModelTileView> loadAllVideo;
        IEnumerable<ModelMultiHeader> getHeader;
        public async Task GetHeader()
        {
            try
            {
                rdoFiltering.Invoke(() => rdoFiltering.Enabled = false);

                rdoFiltering.SelectedIndex = -1;
                rdoReport.SelectedIndex = -1;

                //getHeader = await _multiHeader.GetAsync();
                var getlocation = await _multiLocation.GetAsync();
                getCountry = await _multiCountry.GetAsync();
                getGenre = await _multiGenre.GetAsync();

                loadAllVideo = getHeader.Select(s =>
                {
                    var location = Path.Combine(getlocation.First(f => f.Id == s.LocationId).DirShare_PCIP, s.Num.ToString());
                    var getgenerList = s.MultiGenreLists.Split(',');
                    var genreTitles = getGenre
                        .Where(g => getgenerList.Contains(g.Id.ToString()))
                        //.Select(g => $"<div>{g.Title}</div>").Take(4);
                        .Select(g => g.Title).Take(4);

                    var getGener = string.Join(" , ", genreTitles);
                    return new ModelTileView
                    {
                        Id = s.Id,
                        NamePersian = s.NamePersian,
                        LocationId = s.LocationId,
                        LocationFull = location,
                        // Location = location,
                        Country = getCountry.First(f => f.Id == s.CountryId).Title,
                        Num = s.Num,
                        MultimediaGroup = s.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef")
                            ? "سریال"
                            : "سینمایی",
                        Gener = getGener,
                        //Gener = string.Join(',', genreTitles),


                        IsAnimated = s.IsAnimated,
                        IsAnime = s.IsAnime,

                        AnimOrAnimation = "",

                        //  Image = Image.FromFile(location)

                    };
                }).OrderByDescending(o => o.Num).ToList();

                dgvVideo.DataSource = loadAllVideo;
                this.WindowState = FormWindowState.Maximized;
                btnRefreshList.Enabled = true;
                rdoFiltering.Invoke(() => rdoFiltering.Enabled = true);
            }
            catch (Exception e)
            {

            }
        }

        public async void GetVideoCountLocation()
        {

        }

        readonly List<FrmSelectChapter.ModelSelectFiles> lstSelectFilm = new() { };


        private string _timeWeek = "";
        private async Task CreatePoster()
        {
            var getStr = "سینمایی";
            string forChapterNumber = "";
            string generes = "";
            var getData = barCreatePoster.Tag as ModelTileView;
            var getDir1 = await _multiLocation.GetById(getData.LocationId.Value);
            var getDir = getDir1.DirShare_PCName + "\\" + getData.Num;
            var getDirImage = Path.Combine(getDir, "Image.jpg").LoadBitmap().ImageToBase64();

            var poster = new ClassReport.ClassReport.ModelPoster
            {
                Name = getData.NamePersian,
                Genere = getData.Gener,
                Image = getDirImage,
                MultimediaHeader = null,
                Serial = false
            };

            generes = getData.Gener.Replace(",", " / ");
            // ModelMultimediaHeader getCount = null;

            if (getData.MultimediaGroup == "سریال")
            {
                getStr = "سریال";
                poster.Serial = true;
                var modelMultimediaHeader = await _multiHeader.GetChapterCount(getData.Id);
                poster.MultimediaHeader = modelMultimediaHeader;
                forChapterNumber = $"( قسمت {modelMultimediaHeader.EndChapterDetailsCount})";

                var getHeader = await _multiHeader.GetById(getData.Id);

                var modelWeekOfDays = ClassDateTime.FillDayOfWeek(new ClassDateTime.ModelWeekOfDay
                {
                    Code = 7,
                    Fa = "پایان",
                    En = "Finish"
                });
                if (getHeader.DayOfWeek.Value == 7)
                {
                    _timeWeek = $"";
                }
                else
                {
                    var getNameWeek = modelWeekOfDays.First(f => f.Code == getHeader.DayOfWeek.Value).Fa;
                    _timeWeek = $"📅 زمان: {getNameWeek}‌ها در ایران کاوش";
                }
            }

            if (getData.IsAnimated)
            {
                getStr = "انیمیشن";
            }
            else if (getData.IsAnime)
            {
                getStr = "انیمه";
            }

            var poster1 = new Poster1();
            poster1.Tag = poster;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var getDesk = Path.Combine(desktopPath, "پوستر فیلم");
            Directory.CreateDirectory(getDesk);

            string addressPoster = Path.Combine(getDesk, getData.NamePersian + ".jpg");

            await poster1.ExportToImageAsync(addressPoster);
            poster1.Dispose();
            //await Task.Delay(1000);


            var getQues = ClassMessageBox.ShowMSGQues("ارسال تصویر به گروه ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);
            if (getQues)
            {
                byte[] imageBytes = await File.ReadAllBytesAsync(addressPoster);

                using (var ms = new MemoryStream(imageBytes))
                using (var originalImage = Image.FromStream(ms))
                {
                    using (var resizedImage = ClassImage.ResizeByPercent(originalImage, 5))
                    {
                        // حذف فایل اصلی
                        File.Delete(addressPoster);

                        // ذخیره تصویر جدید
                        resizedImage.Save(addressPoster, ImageFormat.Jpeg);
                        resizedImage.Dispose();
                    }

                }

                //ClassBale bale = new ClassBale("1723187126:DU8S4HAsbBK2II1RGcFFPbygzwUrh8BWMvQ", "@caffenetik");
                //ClassBale bale = new ClassBale("1723187126:DU8S4HAsbBK2II1RGcFFPbygzwUrh8BWMvQ", "@iktest");
                ClassBale bale = new ClassBale("1723187126:DU8S4HAsbBK2II1RGcFFPbygzwUrh8BWMvQ", "@caffenetik");



                string msgNew = $@"🎬 {getStr}: {getData.NamePersian} {forChapterNumber}

📌 ژانر: ({generes})
{_timeWeek}

📢 آدرس کانال:
🆔 https://ble.ir/@caffenetik

🎁 ربات جایزه:
🆔 *https://ble.ir/@IK2026bot*

🏆 با عضویت در ربات و کانال ایران کاوش فیلم رایگان هدیه بگیرید !!!

💼 ارتباط با ادمین:
🆔 https://ble.ir/@Mojtaba7090

🔥 منتظر نظرات شما هستیم.";

                await bale.SendPhoto(new ClassBale.ModelMsg
                {
                    Msg = msgNew,
                    AddressImage = addressPoster
                });
            }

            _timeWeek = "";
        }

        private void RefreshTileAfterNewTile(List<ModelTileView> modelTileViews)
        {

            foreach (var item in modelTileViews)
            {
                // اجرای async بدون بلاک کردن فرم

                Task.Run(() =>
                {
                    var getLocImage = getAllImage.FirstOrDefault(f => f.Id == item.Id)?.Location;
                    if (getLocImage != null && File.Exists(getLocImage))
                    {
                        //var image = Image.FromFile(getLocImage);
                        var image = ClassGraphic.OpenImageFrom(getLocImage);

                        // آپدیت مدل در UI thread
                        tileView1.GridControl.Invoke((MethodInvoker)(() =>
                        {
                            var index = loadAllVideo.FindIndex(f => f.Id == item.Id);
                            if (index >= 0)
                            {
                                loadAllVideo[index].Image = image;
                                tileView1.RefreshRow(index); // فقط همون ردیف رو رفرش کن
                            }
                        }));
                    }
                });
            }

        }

        private void btnCopyToFlash_Click(object sender, EventArgs e)
        {
            grpSelectFilms.WaitDownPage(async () =>
            {
                if (Directory.Exists(lblUSBDrive.Text))
                {
                    var getfileModel = await GetSelectFilmForCopyToMemory(false);

                    // if (_sumSelFilms >= _sizeFlash)
                    {
                        if (getfileModel.Key.Count > 0)
                        {
                            btnCopyToFlash.Enabled = false;
                            var listFile = getfileModel.Key;
                            var getMultiArg = ClassFillToCopy.FillToCopy(listFile) + " " + _selMemoryId + " " +
                                              lblUSBDrive.Text;
                            RunExecutable(getMultiArg);
                        }
                    }
                }
            });
        }


        private async Task<KeyValuePair<List<ModelFile>, ulong>> GetSelectFilmForCopyToMemory(bool android)
        {
            ulong SizeSum = 0;
            List<ModelFile> getfileModel = [];

            for (int i = 0; i < dgvFilms.RowCount(); i++)
            {
                var getSelect = dgvFilms.GetValue<bool>(i, "انتخاب");
                if (!getSelect)
                {
                    continue;
                }

                var chapterId = dgvFilms.GetValue<Guid>(i, "Id");
                var size = dgvFilms.GetValue<long>(i, "Size");
                var from = dgvFilms.GetValue<string>(i, "Address");

                var to1 = dgvFilms.GetValue<string>(i, "نام");
                var to2 = dgvFilms.GetValue<string>(i, "فصل");

                var to3 = Path.GetFileName(from);

                var session = dgvFilms.GetValue<string>(i, "قسمت");

                string to4;
                string forCreate;

                if (to2 != "1")
                {
                    to4 = Path.Combine(lblUSBDrive.Text, to1, to2, to3);
                    forCreate = Path.Combine(lblUSBDrive.Text, to1, to2);
                }
                else
                {
                    to4 = Path.Combine(lblUSBDrive.Text, to1, to3);
                    forCreate = Path.Combine(lblUSBDrive.Text, to1);
                }


                if (android)
                {
                    if (to2 != "1")
                        to4 = Path.Combine("ANDROIDDATA", to1, to2, to3);
                    else
                        to4 = Path.Combine("ANDROIDDATA", to1, to3);
                }


                Directory.CreateDirectory(forCreate);
                string join;
                if (to2 != "1")
                    join = string.Join(" - ", to1, to2, session);
                else
                    join = string.Join(" - ", to1, session);



                var modelFile = new ModelFile
                {
                    ChapterHeaderId = chapterId,
                    Name = join,
                    From = from,
                    To = to4
                };
                getfileModel.Add(modelFile);
                SizeSum += (ulong)size;
            }

            return new KeyValuePair<List<ModelFile>, ulong>(getfileModel, SizeSum);
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
        }

        private async void btnFindMobile_Click(object sender, EventArgs e)
        {
            #region Get Films

            lblUSBDrive.Text = "محل ذخیره";
            KeyValuePair<List<ModelFile>, ulong> getFileModel = await GetSelectFilmForCopyToMemory(true);

            if (getFileModel.Key.Count > 0)
            {
                // 0 => data
                // 1 => MemoryId
                // 2 => 1
                // 3 => AddFast
                // 4 => android

                var getMultiArg = ClassFillToCopy.FillToCopy(getFileModel) + " " + "MemoryId" + " " + "1" + " " + "nullTest" + " " + "android";

                RunExecutable(getMultiArg);
            }

            #endregion

        }

        private void navigationPane1_Click(object sender, EventArgs e)
        {

        }
        private async Task<KeyValuePair<List<ModelFile>, ulong>> GetSelectFilmForDownload()
        {
            var getLocation = (await _multiLocation.GetAsync()).ToList();
            ulong SizeSum = 0;
            List<ModelFile> getfileModel = [];

            for (int i = 0; i < dgvFilms.RowCount(); i++)
            {
                var getSelect = dgvFilms.GetValue<bool>(i, "انتخاب");
                if (!getSelect)
                {
                    continue;
                }
                var chapterId = dgvFilms.GetValue<Guid>(i, "Id");
                var size = dgvFilms.GetValue<long>(i, "Size");
                var from = dgvFilms.GetValue<string>(i, "Address");

                var getExten = Path.GetExtension(from);

                var getFromSplit = from.Split("\\");
                var getRoot1 = "\\\\" + Path.Combine(getFromSplit[2], getFromSplit[3]);

                var getRoot = getLocation.First(f => f.DirShare_PCIP.Contains(getRoot1)).Root;

                var getFromFinal = from.Replace(getRoot1, getRoot);

                var to1 = dgvFilms.GetValue<string>(i, "نام");
                var to2 = dgvFilms.GetValue<string>(i, "فصل");
                var to3 = Path.GetFileName(getFromFinal);

                //var forCreate = Path.Combine(lblUSBDrive.Text, to1, to2);

                var session = dgvFilms.GetValue<string>(i, "قسمت");
                var to4 = Path.Combine(to1, to2, to3);

                var modelFile = new ModelFile
                {
                    ChapterHeaderId = chapterId,
                    Name = string.Join(" - ", to1, to2, session) + getExten,
                    From = getFromFinal,
                    To = to4
                };
                getfileModel.Add(modelFile);
                SizeSum += (ulong)size;
            }

            //var data = new Dictionary<List<ModelFile>, ulong> { { getfileModel, SizeSum } };
            return new KeyValuePair<List<ModelFile>, ulong>(getfileModel, SizeSum);

            //return data.First();
            //return data;
        }
        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            //KeyValuePair<List<ModelFile>, ulong> getfileModel = GetSelectFilmForDownload();
            var getfileModel = await GetSelectFilmForDownload();

            if (getfileModel.Key.Count == 1)
            {
                MessageBox.Show("فیلمی انتخاب نشده است !!!");

            }

            var selectedMovies = new List<ModelFile>();

            foreach (var item in getfileModel.Key)
            {
                selectedMovies.Add(new ModelFile
                {
                    From = item.From,
                    Name = item.Name,
                    To = item.To,
                    ChapterHeaderId = item.ChapterHeaderId
                });
            }

            #region Mobile Link

            using (var client = new HttpClient())
            {
                var json = JsonConvert.SerializeObject(selectedMovies);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.PostAsync($"http://192.168.1.160:1212/api/Download/create-download-page", content);
                var result = JsonConvert.DeserializeObject<dynamic>(await response.Content.ReadAsStringAsync());

                string downloadPageUrl = result.link;     // لینک منحصربه‌فرد برای این مشتری
                string guid = result.guid;                 // شناسه یکتا
                int movieCount = result.movieCount;        // تعداد فیلم‌ها
                DateTime expiry = result.expiry;           // زمان انقضا

                // نمایش لینک به کاربر
                //txtLink.Text = downloadPageUrl;
                //lblInfo.Text = $"تعداد فیلم‌ها: {movieCount} | معتبر تا: {expiry:HH:mm yyyy/MM/dd}";

                // ساخت QR Code از لینک

                var getImg = downloadPageUrl.QRCode();
                Form form = new Form { StartPosition = FormStartPosition.CenterScreen, WindowState = FormWindowState.Normal, Width = 440, Height = 520 };
                PictureBox pictureBox = new PictureBox { Dock = DockStyle.Fill, SizeMode = PictureBoxSizeMode.StretchImage };
                form.Controls.Add(pictureBox);
                pictureBox.Image = getImg;

                form.ShowDialog();


                //  MessageBox.Show($"لینک یکتا برای این مشتری ساخته شد!\n\n{downloadPageUrl}\n\nQR Code آماده اسکن است.");
            }

            #endregion




        }

        private async void btnRefreshList_Click(object sender, EventArgs e)
        {
            btnRefreshList.Enabled = false;
            getHeader = await _multiHeader.GetByModel();
            await GetHeader();
        }

        private void rdoFiltering_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rdoReport.SelectedIndex = -1;
            var getRdo = rdoFiltering.SelectedIndex;
            this.WindowState = FormWindowState.Normal;
            if (getRdo == 0)//"سریال های ایرانی"
            {
                _ = GetHeaderFilter(w => w.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef")// سریال
                                         && w.CountryId == Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") // ایرانی
                                         && w.IsAnimated == false
                                         && w.IsAnime == false);
                //filterCriteria = "[MultimediaGroup] = 'سریال' AND [Country] = 'ایرانی'";
            }
            else if (getRdo == 1)//"سینمایی ایرانی"
            {
                _ = GetHeaderFilter(w => w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5") // سینمایی
                                         && w.CountryId == Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") // ایرانی
                                         && w.IsAnimated == false
                                         && w.IsAnime == false);
                //filterCriteria = "[MultimediaGroup] = 'سینمایی' AND [Country] = 'ایرانی'";
            }
            else if (getRdo == 2)//"سریال های خارجی"
            {
                _ = GetHeaderFilter(w => w.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef")// سریال
                                         && w.CountryId != Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") // غیر ایرانی
                                         && w.CountryId != Guid.Parse("c219de1c-00cd-4195-aad0-fff47b44401e") // غیر هندی
                                         && w.CountryId != Guid.Parse("68824967-89a1-491d-a68c-ae8451d6c389") // غیر ترکی
                                         && w.IsAnimated == false
                                         && w.IsAnime == false);
                // filterCriteria = "[MultimediaGroup] = 'سریال' AND [Country] != 'ایرانی' And [AnimOrAnimation] != 'انیمیشن' And [AnimOrAnimation] != 'انیمه'";
            }
            else if (getRdo == 3)//"سینمایی خارجی"
            {
                _ = GetHeaderFilter(w => w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5")// سینمایی
                                         && w.CountryId != Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") // غیر ایرانی
                                         && w.CountryId != Guid.Parse("c219de1c-00cd-4195-aad0-fff47b44401e") // غیر هندی
                                         && w.CountryId != Guid.Parse("68824967-89a1-491d-a68c-ae8451d6c389") // غیر ترکی
                                         && w.IsAnimated == false
                                         && w.IsAnime == false);
                // filterCriteria = "[MultimediaGroup] = 'سینمایی' AND [Country] != 'ایرانی' And [AnimOrAnimation] != 'انیمیشن' And [AnimOrAnimation] != 'انیمه'";
            }
            else if (getRdo == 4)//"هندی"
            {
                _ = GetHeaderFilter(w => w.CountryId == Guid.Parse("c219de1c-00cd-4195-aad0-fff47b44401e") // هندی
                                          );
                //filterCriteria = "[Country] = 'هندی'";
            }
            else if (getRdo == 5)//"ترکی"
            {
                _ = GetHeaderFilter(w => w.CountryId == Guid.Parse("68824967-89a1-491d-a68c-ae8451d6c389") // ترکی
                );
                // filterCriteria = "[Country] = 'ترکی'";
            }
            else if (getRdo == 6)//"انیمیشن"
            {
                _ = GetHeaderFilter(w => w.IsAnimated == true);
                // filterCriteria = "[AnimOrAnimation] = 'انیمیشن'";
            }
            else if (getRdo == 7)//"انیمه"
            {
                _ = GetHeaderFilter(w => w.IsAnime == true);
                //  filterCriteria = "[AnimOrAnimation] = 'انیمه'";
            }
            else if (getRdo == 8)//"حذف فیلتر"
            {
                _ = GetHeader();
            }


            // اعمال فیلتر روی TileView
            // tileView1.ActiveFilterString = filterCriteria;
            Task.Delay(250);

        }
        IEnumerable<MultiCountry> getCountry;
        IEnumerable<MultiGenre> getGenre;
        public async Task GetHeaderFilter(Func<ModelMultiHeader, bool> where)
        {
            this.WindowState = FormWindowState.Normal;
            rdoFiltering.Enabled = false;
            rdoReport.Enabled = false;
            var getlocation = await _multiLocation.GetAsync();
            loadAllVideo = getHeader.Where(where).Select(s =>
            {
                var location = Path.Combine(getlocation.First(f => f.Id == s.LocationId).DirShare_PCIP, s.Num.ToString());
                var getgenerList = s.MultiGenreLists.Split(',');
                var genreTitles = getGenre
                    .Where(g => getgenerList.Contains(g.Id.ToString()))
                    .Select(g => g.Title).Take(4);

                var getGener = string.Join(" , ", genreTitles);
                return new ModelTileView
                {
                    Id = s.Id,
                    NamePersian = s.NamePersian,
                    LocationId = s.LocationId,
                    LocationFull = location,
                    Country = getCountry.First(f => f.Id == s.CountryId).Title,
                    Num = s.Num,
                    MultimediaGroup = s.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef")
                        ? "سریال"
                        : "سینمایی",
                    Gener = getGener,

                    IsAnimated = s.IsAnimated,
                    IsAnime = s.IsAnime,

                    AnimOrAnimation = ""
                };
            }).OrderByDescending(o => o.Num).ToList();
            // Desription sdf mojyabs d
            dgvVideo.DataSource = loadAllVideo;
            this.WindowState = FormWindowState.Maximized;
            btnRefreshList.Enabled = true;
            rdoFiltering.Enabled = true;
            rdoReport.Enabled = true;
        }

        private void grpSelectFlash_Paint(object sender, PaintEventArgs e)
        {

        }
        List<ClassIkOld.MultimediaGroupedDto>? _getCntOldFilms;
        private async void rdoReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rdoFiltering.SelectedIndex = -1;

            var getRdo = rdoReport.SelectedIndex;
            var getNow = DateTime.Now;
            this.WindowState = FormWindowState.Normal;
            if (getRdo == 0)//"سینمایی ایرانی این هفته"
            {
                _ = GetHeaderFilter(w => w.MultimediaGroupId == Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5")// سینمایی
                                         && w.CountryId == Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") // ایرانی
                                         && w is { IsAnimated: false, IsAnime: false } && w.DateTime.AddDays(7) >= getNow);

            }
            else if (getRdo == 1) //ایرانی در جریان
            {
                _ = GetHeaderFilter(w => w.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef")// سریال
                                         && w.CountryId == Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") // ایرانی
                                         && w is { IsAnimated: false, IsAnime: false, DayOfWeek: < 7 });
            }
            else if (getRdo == 2) //خارجی این هفته
            {
                _ = GetHeaderFilter(w => w.CountryId != Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") // غیر ایرانی
                                         && w is { IsAnimated: false, IsAnime: false } && w.DateTime.AddDays(7) >= getNow);
            }
            else if (getRdo == 3) //انیمیشن این هفته
            {
                _ = GetHeaderFilter(w => (w.IsAnimated == true || w.IsAnime == true) && w.DateTime.AddDays(7) >= getNow);
            }
            else if (getRdo == 4) //20 اکشن برتر
            {
                List<long> list = new List<long>();
                var getAnim = _getCntOldFilms.Where(w => (!w.Animation && w.SerSin != "انیمیشن") && w.Action && w.Count > 3).ToList();
                Random rnd = new Random();
                for (int i = 0; i < 12; i++)
                {
                    var getRnd2 = rnd.Next(0, getAnim.Count);
                    list.Add(Convert.ToInt64(getAnim[getRnd2].Num));
                }
                _ = GetHeaderFilter(w => list.Contains(w.Num));
            }
            else if (getRdo == 5) // 12 انمیشین  else if (getRdo == 5) // 12 انمیشین
            {
                List<long> list = new List<long>();
                var getAnim = _getCntOldFilms.Where(w => (w.Animation || w.SerSin == "انیمیشن") && w.Count > 4).ToList();
                Random rnd = new Random();
                for (int i = 0; i < 12; i++)
                {
                    var getRnd2 = rnd.Next(0, getAnim.Count);
                    list.Add(Convert.ToInt64(getAnim[getRnd2].Num));
                }
                _ = GetHeaderFilter(w => list.Contains(w.Num));
            }
            else if (getRdo == 6) // انیمشن های جدید
            {
                List<long> list = new List<long>();
                var getAnimNews = (await _multiChapterDetail.GetAsync()).OrderByDescending(od => od.DateTime).Take(100).Select(s => new ModelGuidFaEn
                {
                    ChapterDetailId = s.Id
                }).ToList();
                var getAnimNews2 = (await _multiChapterDetail.GetByHeaderTranslate(getAnimNews)).ToList();


                for (int i = 0; i < 100; i++)
                {
                    list.Add(Convert.ToInt64(getAnimNews2[i].HeaderCode));
                }

                _ = GetHeaderFilter(w => list.Contains(w.Num) && (w.IsAnimated || w.IsAnime));
            }
            else if (getRdo == 7) // حذف فیلتر
            {
                _ = GetHeader();
            }

        }
        public class ModelPosters
        {
            public Guid Id { get; set; }
            public string NamePersian { get; set; }
            public Guid? LocationId { get; set; }
            public string Location { get; set; }
            public string Country { get; set; }
            public long Fasl { get; set; }
            public long Ghesmat { get; set; }
            public long Num { get; set; }
            public string MultimediaGroup { get; set; } // سینمایی | سریال
            public string Gener { get; set; }
            public Image Image { get; set; }
            public bool IsAnimated { get; set; }
            public bool IsAnime { get; set; }
            public string AnimOrAnimation { get; set; }
            public string DayOfWeek { get; set; }



        }


        public async Task<List<ModelPosters>> GetHeaderForPrintToGroup(Func<ModelMultiHeader, bool> where)
        {

            #region روز انتشار

            var modelWeekOfDays = ClassDateTime.FillDayOfWeek(new ClassDateTime.ModelWeekOfDay
            {
                Code = 7,
                Fa = "پایان",
                En = "Finish"
            });


            #endregion
            try
            {
                getCountry = await _multiCountry.GetAsync();
                getGenre = await _multiGenre.GetAsync();

                var _loadAllVideoPrintToGroup = getHeader.Where(where).Select(s =>
                {
                    var getgenerList = s.MultiGenreLists.Split(',');
                    var genreTitles = getGenre
                        .Where(g => getgenerList.Contains(g.Id.ToString()))
                        .Select(g => g.Title).Take(4);

                    var getGener = string.Join(" , ", genreTitles);
                    var image = getAllImage.First(f => f.Id == s.Id).Location.LoadBitmap();
                    //var modelMultimediaHeader = _multiHeader.GetChapterCount(s.Id);
                    return new ModelPosters
                    {
                        Id = s.Id,
                        NamePersian = s.NamePersian,
                        LocationId = s.LocationId,

                        Country = getCountry.First(f => f.Id == s.CountryId).Title,
                        Num = s.Num,
                        MultimediaGroup = s.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef")
                            ? "سریال"
                            : "سینمایی",
                        Gener = getGener,

                        IsAnimated = s.IsAnimated,
                        IsAnime = s.IsAnime,

                        AnimOrAnimation = "",
                        DayOfWeek = modelWeekOfDays.First(f => f.Code == s.DayOfWeek.Value).Fa + " ها",
                        Image = image,
                        //Section = modelMultimediaHeader.ChapterCount
                    };
                }).OrderByDescending(o => o.DayOfWeek).ToList();


                foreach (ModelPosters p in _loadAllVideoPrintToGroup)
                {
                    var get1 = (await _multiHeader.GetChapterCount(p.Id));
                    if (get1 != null)
                    {
                        p.Fasl = get1.ChapterCount;
                        p.Ghesmat = get1.EndChapterDetailsCount;
                    }

                    //
                }
                return _loadAllVideoPrintToGroup;

            }
            catch (Exception e)
            {

            }


            return null;
        }

        private async void btnIraniNewSerial_Click(object sender, EventArgs e)
        {

            #region Fetch Data

            var getData = await GetHeaderForPrintToGroup(w =>
                w.MultimediaGroupId == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef") // سریال
                && w.CountryId == Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2") // ایرانی
                && w is { IsAnimated: false, IsAnime: false, DayOfWeek: < 7 });

            #endregion
            Pos_Serial_Irani_Week iraniWeek = new Pos_Serial_Irani_Week();

            iraniWeek.Tag = getData;
            //iraniWeek.ShowPreviewDialog();
            //return;

            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var getDesk = Path.Combine(desktopPath, "پوستر فیلم");
            Directory.CreateDirectory(getDesk);

            string addressPoster = Path.Combine(getDesk, "سریال های ایرانی__" + ".jpg");

            await iraniWeek.ExportToImageAsync(addressPoster);
            iraniWeek.Dispose();



        }

        #region ارسال پوستر به وب سایت

        private readonly PostPublisher _publisher = new(new FtpSettings
        {
            Host = "site95258.siteasp.net",             // مثلاً از پنل MonsterASP
            User = "site95258",
            Password = "Bf8?=Fp6n3Y_",                      // از فایل تنظیمات / رمزشده بخوان
            RemoteRoot = "/wwwroot",
            SiteBaseUrl = "https://irankavosh.runasp.net"
        });


        #endregion  
        private async void btnNewFilm_Click(object sender, EventArgs e)
        {

            #region Actor



            var getActors = await _multiActor.GetAsync();



            #endregion


            //var films = new List<PostFilm>();
            List<ModelMultiHeaderPoster> lstgetFull = new List<ModelMultiHeaderPoster>();
            var getSelectForPoster = GetSelectFilmForCopyToMemory(false);
            List<ModelFile> ss = getSelectForPoster.Result.Key;
            foreach (var t in _getSelectOnFilmByFilm)
            {
                var getOtherData = await _multiHeader.GetById(t.HeaderId);
                #region Actor
                string actors = "";
                if (!string.IsNullOrEmpty(getOtherData.ActorsId))
                {
                    var getActorName = getOtherData.ActorsId.Split(",");

                    var actorNames = getActors.Where(actor => getActorName.Any(c => Guid.Parse(c) == actor.Id))
                        .Select(actor => actor.ActorName).Take(7)
                        .ToList();
                    if (actorNames.Any())
                        actors = string.Join(",", actorNames);
                }


                #endregion

                var getFind = loadAllVideo.First(f => f.Id == t.HeaderId);



                ModelMultiHeaderPoster getFull = new ModelMultiHeaderPoster
                {
                    Num = t.ChapterCode,
                    NamePersian = getFind.NamePersian,
                    Poster = t.ImageDir,
                    Country = getFind.Country,
                    IsAnime = getFind.IsAnime,
                    IsAnimated = getFind.IsAnimated,
                    MultiGenreLists = getFind.Gener,
                    MultimediaGroup = getFind.MultimediaGroup,
                    Actors = actors,
                    //*--------------*
                    NameEnglish = "",
                    Created = getOtherData.Created,
                    IMDB = getOtherData.IMDB,
                    Description = getOtherData.Description,


                };
                lstgetFull.Add(getFull);
            }



            //if (ss.Count == 0) { MessageBox.Show(@"حداقل یک فیلم انتخاب کن."); return; }

            btnNewFilm.Enabled = false;
            try
            {
                var locations = await _multiLocation.GetAsync();
                var genreMap = getGenre.ToDictionary(g => g.Id.ToString(), g => g.Title);

                var films = new List<PostFilm>();
                foreach (var t in lstgetFull)
                {

                    //ModelMultiHeaderPoster h = await GetHeaderPosterAsync(t.Id); // ← از سرویس خودت بگیر
                    //var dir = locations.First(l => l.Id == t.LocationId).DirShare_PCIP;
                    //var poster = Path.Combine(dir, t.Num.ToString(), "Image.jpg"); // همان مسیر FunAddressAllImage
                    films.Add(new PostFilm(t, t.Poster));
                }

                var progress = new Progress<string>(msg => lblUSBDrive.Text = msg);
                var result = await _publisher.PublishAsync(films, genreMap, progress);

                Clipboard.SetText(result.Url); // روی رشته‌ی UI اجرا می‌شود
                MessageBox.Show($"پست {result.Number} منتشر شد و لینک کپی شد:\n{result.Url}");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "خطا در انتشار", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnNewFilm.Enabled = true;
            }

            // var getLastFilms = (await _multiChapterDetail.GetAsync()).Where(w => w.DateTime > DateTime.Now.AddDays(-7));
        }
    }
}