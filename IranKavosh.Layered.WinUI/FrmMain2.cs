using DevExpress.Xpf.Bars;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using DevExpress.XtraBars.ToastNotifications;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraReports.Native;
using DevExpress.XtraSpreadsheet.UI;
using DocumentFormat.OpenXml.Wordprocessing;
using EF_Model.Multimedia;
using EF_Model.Table;
using EitaaIK;
using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using IranKavosh.Layered.WinUI.Class;
using IranKavosh.Layered.WinUI.Forms;
using MediaDevices;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyCom.Class;
using MyCom.Class;
using MyCom.Class.YouTube;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;
using static IranKavosh.Layered.WinUI.Forms.FrmShowVideo;
using BarEditItem = DevExpress.XtraBars.BarEditItem;
using ItemClickEventArgs = DevExpress.XtraBars.ItemClickEventArgs;
using RibbonItemStyles = DevExpress.XtraBars.Ribbon.RibbonItemStyles;

namespace IranKavosh.Layered.WinUI
{
    public partial class FrmMain2 : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        private ClsFont _clsFontBold = new(ClsFont.enumFont.samimBoldFD, true);
        private ClsFont _clsFont = new(ClsFont.enumFont.samimBoldFD, false);


        private static HttpClient _httpClient = new();
        private FundHttpService _fund = new(_httpClient);
        private FundTypeHttpService _fundType = new(_httpClient);
        private ErrorExcepHttpService _errorExcep = new(_httpClient);
        private PriceListHttpService _priceList = new(_httpClient);
        private MultimediaHeaderHttpService _multiHeader = new(_httpClient);
        private MultiLocationHttpService _multiLocation = new(_httpClient);
        private DefinitiveAccountHttpService _definitiveAccount = new(_httpClient);
        Task thread;
        public FrmMain2()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            this.WindowState = FormWindowState.Maximized;


            // this.ribbon.MdiMergeStyle = DevExpress.XtraBars.Ribbon.RibbonMdiMergeStyle.Always;
            // this.ribbon.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            // this.ribbon.ShowToolbarCustomizeItem = false;
            // this.ribbon.Toolbar.ShowCustomizeItem = false;
            // this.ribbon.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;

            // ribbonPageGroup1.ShowCaptionButton = false;



            // this.ribbon.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            // this.ribbon.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.Hide;

        }

        #region New TEst

        private readonly ClsFont _clsFontEn = new(ClsFont.enumFont.samimBold, true);
        private static readonly HttpClient HttpClient = new();
        private readonly PersonalHttpService _personal = new(HttpClient);
        private PersonalGroupHttpService _personalGroup = new(HttpClient);
        private readonly MultimediaHeaderChapterHttpService _multimediaHeaderChapter = new(HttpClient);
        private readonly MultiCountryHttpService _multiCountry = new(HttpClient);
        private readonly MultiGenreHttpService _multiGenre = new(HttpClient);
        private readonly HddMemoryHttpService _hddMemoryHttp = new(HttpClient);
        private readonly HddMemoryPersonHttpService _hddMemoryPerson = new(HttpClient);

        List<ModelTileView> loadAllVideo;
        IEnumerable<ModelMultiHeader> getHeader;
        public async Task GetHeader()
        {
            _clsFontBold.ChangeFont(kavoshGrid1);
            kavoshGrid1.SetStyle();

            //getHeader = await _multiHeader.GetAsync();
            // var getlocation = await _multiLocation.GetAsync();
            getHeader = (await _multiHeader.GetByModel());
            var getCountry = await _multiCountry.GetAsync();
            var getGenre = await _multiGenre.GetAsync();

            loadAllVideo = getHeader.Select(s =>
            {
                //  var location = Path.Combine(getlocation.First(f => f.Id == s.LocationId).DirShare_PCIP, s.Num.ToString(), "Image.jpg");
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

                    AnimOrAnimation = ""
                    //  Image = Image.FromFile(location)

                };
            }).OrderByDescending(o => o.Num).ToList();
            kavoshGrid1.DataSource = loadAllVideo;
            this.WindowState = FormWindowState.Maximized;


        }

        #endregion
        public void SetFonts()
        {
            //_ = EitaaBot.SendFileAsync("caption","Title");
            _clsFontBold.ChangeFont(this.ribbon);
            this.ribbon.CustomizeReborn();
            ribbon.SelectedPage = ribbonPage2;

        }
        private void ribbon_Click(object sender, EventArgs e)
        {

        }



        public async Task StatusServer()
        {
            await ribbon.Invoke(async () =>
            {
                pnlSrvStatus.ItemLinks.Clear();
                var getLoc = (await _multiLocation.GetSizeHDD()).ToList();
                for (int i = 0; i < getLoc.Count; i++)
                {

                    RepositoryItemProgressBar pBar = new RepositoryItemProgressBar { Name = $"bar{i}", ShowTitle = true, Maximum = (int)getLoc[i].Total };
                    BarEditItem barEditItem = new BarEditItem { Name = $"barS{i}", EditHeight = 20, EditWidth = 100 };

                    barEditItem.Caption = getLoc[i].Drive.Substring(0, 1) + $" ({getLoc[i].Free.ToString("N0")} GB)";
                    barEditItem.Edit = pBar;
                    barEditItem.EditValue = getLoc[i].Total - getLoc[i].Free;

                    barEditItem.Id = ribbon.Manager.GetNewItemId();
                    barEditItem.Name = $"barEditItem{i}";
                    barEditItem.RibbonStyle = RibbonItemStyles.Large;

                    // اضافه کردن به Ribbon
                    ribbon.Items.Add(barEditItem);

                    // اضافه کردن به گروه
                    pnlSrvStatus.ItemLinks.Add(barEditItem);

                }

                // تنظیمات نهایی
                pnlSrvStatus.State = RibbonPageGroupState.Expanded;
                ribbon.Refresh();
            });

        }
        //public static void GetDriveWearPercentage()
        //{
        //    try
        //    {
        //        var scope = new ManagementScope(@"root\Microsoft\Windows\Storage");
        //        scope.Connect();

        //        var diskQuery = new ObjectQuery("SELECT * FROM MSFT_PhysicalDisk");

        //        using (var diskSearcher = new ManagementObjectSearcher(scope, diskQuery))
        //        {
        //            foreach (ManagementObject disk in diskSearcher.Get())
        //            {
        //                string friendlyName = disk["FriendlyName"]?.ToString() ?? "Unknown";

        //                // روش صحیح استفاده از GetRelated
        //                try
        //                {
        //                    var relCounters = disk.GetRelated("MSFT_StorageReliabilityCounter");
        //                    bool hasCounter = false;

        //                    foreach (ManagementObject rc in relCounters)
        //                    {
        //                        hasCounter = true;
        //                        byte? wear = rc["Wear"] as byte?;
        //                        byte? temp = rc["Temperature"] as byte?;
        //                        ushort? powerOn = rc["PowerOnHours"] as ushort?;

        //                        Console.WriteLine($"دیسک: {friendlyName}");
        //                        Console.WriteLine($"درصد سایش: {(wear.HasValue ? wear.Value + "%" : "N/A")}");
        //                        Console.WriteLine($"دما: {(temp.HasValue ? temp.Value + "°C" : "N/A")}");
        //                        Console.WriteLine($"ساعت کارکرد: {(powerOn.HasValue ? powerOn.Value.ToString() : "N/A")}");
        //                        Console.WriteLine("---");
        //                    }

        //                    if (!hasCounter)
        //                    {
        //                        Console.WriteLine($"دیسک {friendlyName}: اطلاعاتی یافت نشد (ممکن است NVMe باشد)");
        //                    }
        //                }
        //                catch (Exception ex)
        //                {
        //                    Console.WriteLine($"خطا در دریافت اطلاعات برای {friendlyName}: {ex.Message}");
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"خطای کلی: {ex.Message}");
        //    }
        //}
        private readonly MultimediaChapterDetailHttpService _multiChapterDetail = new(_httpClient);
        private async void FrmMain2_Load(object sender, EventArgs e)
        {


            //await GetHeader();

            // ClassConvertVideo convertVideo = new ClassConvertVideo();
            //await convertVideo.ConvertAllFilesAsync(VideoCodec.h264_nvenc, new List<string> {"C:\\Users\\Mojtaba\\Desktop\\فیلم\\انیمه\\1.mp4" },
            //     ClassConvertVideo.SizeVideo.a360, progressBarControl1);


            if (Environment.UserName.ToLower() != "mojtaba")
            {
                FrmLogin frmLogin = new FrmLogin();
                frmLogin.ShowDialog();
            }

            //await new ClsPublic().CreatePosterByHeader(Guid.Parse("723247e8-46b0-4264-a1ac-ce3628d44167"));

            //GetDriveWearPercentage();
            new Task(async () =>
             {
                 while (true)
                 {
                     try
                     {
                         await StatusServer();
                         //await Task.Delay(1000);
                         await Task.Delay(30000);
                     }
                     catch (Exception exception)
                     {

                     }
                 }

             }).Start();

            thread = new Task(async () =>
            {
                while (true)
                {

                    try
                    {
                        var errorExceps = (await _errorExcep.GetAsync()).ToList();
                        var any = errorExceps.Any();
                        if (any)
                        {
                            var getT = DateTime.Now;
                            var getDate = errorExceps.Last().DateTime;
                            if (getDate >= getT.AddSeconds(-6))
                            {
                                await this.InvokeThread(() =>
                                {
                                    toastNotificationsManager1.Notifications[0].Body = errorExceps.Last().Message;
                                    toastNotificationsManager1.Notifications[0].Body2 = errorExceps.Last().Type;
                                    toastNotificationsManager1.Notifications[0].Header = "Header";
                                    toastNotificationsManager1.ApplicationName = "ایران کاوش";
                                    toastNotificationsManager1.ShowNotification(toastNotificationsManager1
                                        .Notifications[0].ID);
                                    // MessageBox.Show("Error");
                                }, false);
                            }
                        }
                    }
                    catch (Exception exception)
                    {

                    }
                    Thread.Sleep(4000);
                }
            });
            thread.Start();
            //var getMobile = ClsDeviceAndroid.ConnectFirstDevice();
            //var getdr = getMobile.GetDrives();

            // return;
            //new Task(() =>
            //{
            //    var getMobile = ClsDeviceAndroid.ConnectFirstDevice();
            //    var getDir = ClsDeviceAndroid.ListDirectories(getMobile);

            //    var localFile = @"C:\Users\Mojtaba\Desktop\Test\2.mp4";
            //    var targetOnPhone = $@"\{getDir.First()}\KavoshVideo\977-1.mp4";

            //    getMobile.CreateDirectory(@"\Phone\KavoshVideo");

            //    // -------------------------
            //    // اندازه‌گیری سرعت فایل اول
            //    // -------------------------
            //    var sw = Stopwatch.StartNew();
            //    ClsDeviceAndroid.CopyToPhone(getMobile, localFile, targetOnPhone);
            //    sw.Stop();

            //    long size1 = new FileInfo(localFile).Length;
            //    double seconds = sw.Elapsed.TotalSeconds;
            //    double speed = size1 / seconds; // bytes per second

            //    Console.WriteLine($"Speed = {speed / 1024 / 1024:0.00} MB/s");

            //    // -------------------------
            //    // فایل‌های بعدی با Progress تخمینی
            //    // -------------------------
            //    var getFiles = Directory.GetFiles(@"C:\Users\Mojtaba\Desktop\Test");

            //    foreach (string file in getFiles)
            //    {
            //        long fileSize = new FileInfo(file).Length;
            //        double estimatedSeconds = fileSize / speed;

            //        targetOnPhone = $@"\{getDir.First()}\KavoshVideo\{Path.GetFileName(file)}";

            //        // تایمر برای نمایش درصد
            //        var timer = new System.Timers.Timer(100);
            //        var start = DateTime.Now;

            //        timer.Elapsed += (s, e) =>
            //        {
            //            double elapsed = (DateTime.Now - start).TotalSeconds;
            //            double percent = (elapsed / estimatedSeconds) * 100;

            //            if (percent > 100) percent = 100;

            //            bar1.Invoke(() => { bar1.EditValue = percent;});
            //        };

            //        timer.Start();

            //        // کپی واقعی
            //        ClsDeviceAndroid.CopyToPhone(getMobile, file, targetOnPhone);

            //        timer.Stop();
            //        Console.WriteLine($"{Path.GetFileName(file)} : 100%");
            //    }

            //}).Start();




            SetFonts();
            try
            {
                var getAddress = Assembly.GetExecutingAssembly().Location.Replace(".dll", ".exe");
                ClassRegistery.AddMenuToRightClickFiles("IranKavosh", getAddress, new ClassRegistery.ModelMenu
                {
                    NameMenu = "ایران کاوش",
                    SubCommand = new List<ClassRegistery.ModelSubMenu>
                    {
                        new() { SubMenu = "AddVideo", SubMenuPersian = "افزودن فیلم (حداکثر 15 فایل)" },
                        new() { SubMenu = "CreatePoster", SubMenuPersian = "ساخت پوستر" },
                        //new ClassRegistery.ModelSubMenu { SubMenu = "SortName", SubMenuPersian = "مرتب سازی (حداکثر 15 فایل)" },
                    }
                });
            }
            catch (Exception exception)
            {

            }
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            new FrmManageVideo(false).OverShowWait<FrmManageVideo>(this);
        }

        private void barVideoShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            new FrmShowVideo().OverShowWait<FrmShowVideo>(this);
        }

        private void barPerson_ItemClick(object sender, ItemClickEventArgs e)
        {
            new FrmPerson().OverShowWait<FrmPerson>(this);
        }

        private void barFactorSell_ItemClick(object sender, ItemClickEventArgs e)
        {

            new FrmFastSell().OverShowWait<FrmFastSell>(this);
        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            //var fundType = await _fundType.GetByTitle("فروش فیلم");
            //var fundType2 = "";
        }

        private void barPriceList_ItemClick(object sender, ItemClickEventArgs e)
        {
            new FrmPriceList().OverShowWait<FrmPriceList>(this);
        }

        private void FrmMain2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thread != null)
                thread.Dispose();
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {

        }

        private void barFund_ItemClick(object sender, ItemClickEventArgs e)
        {
            new FrmBuySell().OverShowWait<FrmBuySell>(this);
        }

        private void simpleButton1_Click_2(object sender, EventArgs e)
        {
            //var get = ClassPrinter.GetInfo();
            //var getfff = get.QueueStatus.ToString();
        }

        private void simpleButton1_Click_3(object sender, EventArgs e)
        {
            StatusServer();
        }

        private void barBtnProduct_ItemClick(object sender, ItemClickEventArgs e)
        {

            new FrmProduct().OverShowWait<FrmProduct>(this);
        }

        private void btnFactorDor_ItemClick(object sender, ItemClickEventArgs e)
        {
            new BFrmFactor().OverShowWait<BFrmFactor>(this);
        }

        private void barDefAccount_ItemClick(object sender, ItemClickEventArgs e)
        {
            new FrmDefiniteAccounts().OverShowWait<FrmDefiniteAccounts>(this);

        }

        private async void button1_Click(object sender, EventArgs e)
        {

        }

        private async void simpleButton1_Click_4(object sender, EventArgs e)
        {

        }

        private async void simpleButton1_Click_5(object sender, EventArgs e)
        {

            await new ClassConvertVideo().ConvertAllFilesAsync(VideoCodec.h264_nvenc,
                ["M:\\IranKavosh 2026\\IranKavosh.Layered.WinUI\\bin\\Debug\\net9.0-windows\\Downloads\\Trl=Supergirl_2026.mp4"],
                ClassConvertVideo.SizeVideo.trl);

        }
    }
}