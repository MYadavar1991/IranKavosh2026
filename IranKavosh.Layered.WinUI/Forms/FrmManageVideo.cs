using DevExpress.CodeParser;
using DevExpress.Data;
using DevExpress.DataAccess.Web.Native;
using DevExpress.LookAndFeel;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.XtraBars;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraMap.Drawing.DirectD3D9;
using DevExpress.XtraPrinting;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using DocumentFormat.OpenXml.Vml;
using EF_Model.Class;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using IranKavosh.Layered.Service.Years;
using IranKavosh.Layered.WinUI.Class;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Win32;
using MyCom.Class;
using MyCom.Class.YouTube;
using MyCom.Object;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xabe.FFmpeg;
using static DevExpress.XtraRichEdit.Forms.Design.BorderShadingUserControl;
using static MyCom.Class.ClassFileManage;
using ClassEncryptDecrypt = MyCom.Class.ClassEncryptDecrypt;
using Color = System.Drawing.Color;
using JsonSerializer = System.Text.Json.JsonSerializer;
using OpenFileDialog = System.Windows.Forms.OpenFileDialog;
using Path = System.IO.Path;
using ToolTipAnchor = DevExpress.Utils.ToolTipAnchor;

namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class FrmManageVideo : DevExpress.XtraEditors.XtraForm
    {
        private readonly bool _addVideo;

        #region http

        private static readonly HttpClient _httpClient = new();
        private readonly MultiAgeHttpService _multiAge = new(_httpClient);
        private readonly MultiActorHttpService _multiActor = new(_httpClient);
        private readonly MultiGenreHttpService _multiGenre = new(_httpClient);
        private readonly MultiCountryHttpService _multiCountry = new(_httpClient);
        private readonly MultiPriceHttpService _multiPrice = new(_httpClient);
        private readonly MultimediaHeaderHttpService _multiHeader = new(_httpClient);

        private readonly MultimediaHeaderChapterHttpService _multiHeaderChapter = new(_httpClient);

        private readonly MultimediaChapterDetailHttpService _multiChapterDetail = new(_httpClient);

        private readonly MultiLocationHttpService _multiLocation = new(_httpClient);
        //  private PersonalGroupHttpService _personalGroup = new PersonalGroupHttpService(_httpClient);

        #endregion

        private readonly ClsFont _clsFontNoBold = new(ClsFont.enumFont.samimBoldFD, false);
        private readonly ClsFont _clsFont = new(ClsFont.enumFont.samimBoldFD, true);
        private Guid _headerId;
        private Guid _chapterHeaderId;
        public long SelHeaderCode = -1;
        public string SelHeaderFullAddress = "";
        public int HeaderChapterCode;

        private Image _selImageList;

        //List<string> allGene
        public FrmManageVideo(bool addVideo)
        {
            _addVideo = addVideo;
            InitializeComponent();

            picVideo.ImageLoading += (s1, e1) =>
            {
                if (picVideo.Width < 500 || picVideo.Height < 650)
                {
                    picVideo.Image = null;
                }
            };
        }

        public class ModelSelFiles
        {
            public int lblRow { get; set; }
            public string lblItem { get; set; }
        }

        private readonly string uriFile = @"\\192.168.1.160";
        private Guid _rdoAge = Guid.Empty;
        private Guid _rdoType = Guid.Empty;
        private bool _Anime = false;
        private bool _Animated = false;
        private Guid _InsertOldFilm;

        private void FrmManageVideo_Shown(object sender, EventArgs e)
        {
            var modelWeekOfDays = ClassDateTime.FillDayOfWeek(new ClassDateTime.ModelWeekOfDay
            {
                Code = 7,
                Fa = "پایان",
                En = "Finish"
            });
            itemWeekOfDay.Enabled = false;
            cmbWeekOfDay.Properties.DataSource = modelWeekOfDays;
            cmbWeekOfDay.Properties.ValueMember = "Code";
            cmbWeekOfDay.Properties.DisplayMember = "Fa";
            cmbWeekOfDay.Properties.PopulateColumns();
            cmbWeekOfDay.Properties.Columns["Code"].Visible = false;
            cmbWeekOfDay.Properties.Columns["En"].Visible = false;
            cmbWeekOfDay.Properties.PopupFilterMode = PopupFilterMode.Contains;

            rdoType.EditValueChanged += (s1, e1) =>
            {
                if (rdoType.EditValue.ToGuid() == Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef"))
                {
                    cmbWeekOfDay.EditValue = null;
                    itemWeekOfDay.Enabled = true;
                }
                else
                {
                    cmbWeekOfDay.EditValue = 7;
                    itemWeekOfDay.Enabled = false;
                }
            };

            LoadYear();

            this.WaitDownPage(async void () =>
            {
                await Task.WhenAll(
                    LoadActors(),
                    LoadMultiGener(),
                    LoadMultiAge(),
                    LoadMultiCountry(),
                    LoadMultiPrice()
                );

                await Task.WhenAll(
                    LoadViewHeader(),
                    LoadVideo()

                // LoadForSelFile()
                );
                if (_addVideo)
                {
                    await Task.WhenAll(LoadForSelFile());
                }
            });

            FontGrid();
            LoadOldFilm();
            //var progress = new Progress<double>(percent =>
            //{
            //    progressBar.Value = (int)percent;
            //    lblPercent.Text = $"{percent:F1}%";
            //});
        }

        public async Task<bool> LoadForSelFile()
        {
            #region Read

            var getAssress = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            lstSelFiles.Items.Clear();
            List<ModelSelFiles> selFilesList = new List<ModelSelFiles>();
            Thread.Sleep(1000);

            try
            {
                var lines = await File.ReadAllLinesAsync(getAssress + "\\SelFiles.txt");

                int lineCount = lines.Length; // تعداد خطوط

                if (lineCount == 1 &&
                    (lines[0].ToLower().Contains(".mp4") || lines[0].ToLower().Contains(".mkv"))) // ادامه سریال
                {
                    selFilesList.Add(new ModelSelFiles
                    {
                        lblItem = lines[0],
                        lblRow = 1
                    });
                }
                else if (lineCount == 2) // جدید سینمایی
                {
                    var getDir = lines.Select(addressFull => new { addressFull, Ex = Path.GetExtension(addressFull) })
                        .ToList();
                    var getImage = getDir.Any(f => f.Ex.ToLower() == ".webp")
                        ? getDir.First(f => f.Ex.ToLower() == ".webp").addressFull
                        : getDir.First(f => f.Ex.ToLower() == ".jpg" || f.Ex.ToLower() == ".bmp").addressFull;

                    var getFile = getDir.First(f => f.Ex.ToLower() == ".mp4" || f.Ex.ToLower() == ".mkv").addressFull;
                    selFilesList.Add(new ModelSelFiles
                    {
                        lblItem = getFile,
                        lblRow = 1
                    });
                    selFilesList.Add(new ModelSelFiles
                    {
                        lblItem = getImage,
                        lblRow = 2
                    });
                }

                else
                {
                    //Description Line To List 
                    foreach (string line in lines)
                    {
                        var getNameFileNumber = Path.GetFileNameWithoutExtension(line);
                        //var newName = Regex.Replace(getNameFileNumber, @"^\d+-", ""); // خروجی: "1"
                        try
                        {
                            if (line.ToLower().Contains(".mp4") || line.ToLower().Contains(".mkv")) // تشخیض فقط فیلم
                            {
                                var lblRow = Convert.ToInt32(getNameFileNumber);
                                selFilesList.Add(new ModelSelFiles
                                {
                                    lblItem = line,
                                    lblRow = lblRow
                                });
                            }
                            else // تفکیک تصویر و فیلم
                            {
                                selFilesList.Add(new ModelSelFiles
                                {
                                    lblItem = line,
                                    lblRow = 1000
                                });
                            }

                        }
                        catch (Exception e)
                        {
                            File.Delete(getAssress + "\\SelFiles.txt");
                            ClassMessageBox.ShowMSG("اگر میخایید چند قسمت وارد نمایید، باید آنهارو شماره گذاری کنید",
                                Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار);
                            Application.Exit();
                            return false;
                        }
                    }
                }

            }
            catch (Exception e)
            {

                File.Delete(getAssress + "\\SelFiles.txt");
                ClassMessageBox.ShowMSG("فایل های که انتخاب شدن با قوانین نرم افزار مطابقت ندارد", Class_Text.Msg_Name,
                    ClassMessageBox.enumIcon.هشدار);
                Application.Exit();
                return false;
            }


            lstSelFiles.DataSource = selFilesList.OrderBy(o => o.lblRow).ToList();
            File.Delete(getAssress + "\\SelFiles.txt");
            //  Thread.Sleep(1000);
            //  MessageBox.Show(getSel);

            #endregion

            if (selFilesList.Count > 0)
            {
                var getImg = selFilesList.Where(w =>
                    w.lblItem.ToLower().Contains(".jpg") || w.lblItem.ToLower().Contains(".bmp") ||
                    w.lblItem.ToLower().Contains(".webp"));
                var getVideo = selFilesList.Where(w =>
                    w.lblItem.ToLower().Contains(".mp4") || w.lblItem.ToLower().Contains(".mkv"));
                if (getImg.Count() > 1)
                {
                    ClassMessageBox.ShowMSG("فقط یک تصویر میتوانید انتخاب کنید", Class_Text.Msg_Name,
                        ClassMessageBox.enumIcon.هشدار);
                    Application.Exit();
                    return false;
                }

                if (getVideo.Any()) // بررسی وجود ویدئو
                {
                    #region فیلم جدید

                    if (getImg.Count() == 1)
                    {
                        _selImageList = getImg.Select(s => Path.GetExtension(s.lblItem.ToLower())).Contains(".webp")
                            ? getImg.First().lblItem.ConvertWebpToImage()
                            : Image.FromFile(getImg.First().lblItem);
                    }

                    #endregion

                    #region فیلم قدیمی

                    else if (!getImg.Any())
                    {
                        XtraForm frmSelFilmForm = new XtraForm
                        {
                            StartPosition = FormStartPosition.CenterParent,
                            Width = 800,
                            Height = 400,
                            MaximizeBox = false,
                            MinimizeBox = false,
                            FormBorderStyle = FormBorderStyle.FixedSingle,
                            Text = @"انتخاب فیلم از لیست",
                        };

                        #region Copy Grid

                        SearchControl srchVideoControl = new SearchControl
                        {
                            Client = this.dgvHeader,
                            Dock = DockStyle.Top,
                            Properties =
                            {
                                FindDelay = 500,
                                Appearance =
                                {
                                    Font = _clsFont.ChangeFont(13),
                                    TextOptions =
                                    {
                                        RightToLeft = true, HAlignment = HorzAlignment.Center,
                                        VAlignment = VertAlignment.Center
                                    }
                                }
                            }

                        };
                        var getParent = this.dgvHeader.Parent;
                        dgvHeader.RightToLeft = RightToLeft.Yes;

                        frmSelFilmForm.Controls.Add(this.dgvHeader);
                        frmSelFilmForm.Controls.Add(srchVideoControl);

                        frmSelFilmForm.FormClosed += (s1, e1) =>
                        {
                            viewHeader.Columns[7].Visible = true;
                            getParent.AddControl(this.dgvHeader);
                        };

                        frmSelFilmForm.Shown += (s1, e1) =>
                        {
                            viewHeader.Columns[7].Visible = false;

                            srchVideoControl.Select();
                            srchVideoControl.Focus();
                        };
                        frmSelFilmForm.ShowDialog();

                        #endregion
                    }

                    #endregion
                }
            }


            if (_selImageList != null)
                picVideo.Image = _selImageList;
            return true;
        }

        private readonly DataTable dtHeader = new();

        public async Task LoadVideo()
        {
            if (dtHeader.Columns.Count == 0)
            {
                dtHeader.Columns.Add("clmId", typeof(Guid));
                dtHeader.Columns.Add("clmCode", typeof(long));
                dtHeader.Columns.Add("clmName");
                dtHeader.Columns.Add("clmCreated");
                dtHeader.Columns.Add("clmGroup");
                dtHeader.Columns.Add("clmIsActive", typeof(bool));
                dtHeader.Columns.Add("clmAddress", typeof(string));
            }

            var getAllMovie = await _multiHeader.GetByModel();
            var getLocations = await _multiLocation.GetAsync();
            //  dgvHeader.BeginUpdate();
            dtHeader.Rows.Clear();
            _InsertNum.Clear();
            foreach (var item in getAllMovie)
            {
                dgvHeader.DataSource = dtHeader;
                var getLocation = getLocations.First(f => f.Id == item.LocationId).DirShare_PCIP;
                dtHeader.Rows.Add(
                    item.Id,
                    item.Num,
                    item.NamePersian,
                    item.Created,
                    item.MultimediaGroupId == "6509e3d3-ee0f-4dac-ad65-446be213d9ef".ToGuid(false)
                        ? "سریال"
                        : "سینمایی",
                    item.IsActive,
                    getLocation);
                _InsertNum.Add(item.Num);
            }

            //LoadOldFilm();
            //  dgvHeader.EndUpdate();
        }

        public void LoadYear()
        {
            var getY = ClassYear.GetYear();
            cmbYear.Properties.DataSource = getY;
        }

        public async Task LoadMultiAge()
        {
            rdoAge.Properties.BeginUpdate();
            var getAge = await _multiAge.GetAsync();
            if (getAge != null)
                foreach (MultiAge age in getAge)
                {
                    rdoAge.Properties.Items.Add(new RadioGroupItem
                    {
                        Value = age.Id,
                        Description = age.Title
                    });
                }

            rdoAge.Properties.EndUpdate();
        }

        private IEnumerable<MultiGenre> _getGenre;

        public async Task LoadMultiGener()
        {
            lstGener.Items.Clear();
            _getGenre = await _multiGenre.GetAsync();
            foreach (var g in _getGenre)
            {
                lstGener.Items.Add(new CheckedListBoxItem(g.Id, g.Title, CheckState.Unchecked));
                //rdoAge.Properties.Items.Add(new RadioGroupItem
                //{
                //    Value = age.Id.ToString(),
                //    Description = age.Title
                //});
            }
        }

        public async Task LoadActors()
        {
            tknActor.Properties.Tokens.Clear();
            var getActors = await _multiActor.GetAsync();
            if (getActors != null)
            {
                foreach (var actor in getActors.ToList())
                {
                    tknActor.Properties.Tokens.Add(new TokenEditToken
                    {
                        Value = actor.Id.ToString(),
                        Description = actor.ActorName != "" ? actor.ActorName : actor.ActorNameEn,
                    });
                }
            }
        }

        public async Task LoadMultiPrice()
        {
            cmbPrice.Properties.BeginUpdate();
            var getPrice = await _multiPrice.GetAsync();
            cmbPrice.Properties.DataSource = getPrice.Select(s => new { s.Id, s.Price }).OrderBy(o => o.Price).ToList();
            cmbPrice.Properties.DisplayMember = "Price";
            cmbPrice.Properties.ValueMember = "Id";
            cmbPrice.Properties.PopulateViewColumns();
            cmbPrice.Properties.PopupView.Columns["Id"].Visible = false;
            cmbPrice.Properties.EndUpdate();
            // cmbPrice.Refresh();
        }

        public async Task LoadMultiCountry()
        {
            cmbCountry.Properties.BeginUpdate();
            var getCountry = await _multiCountry.GetAsync();

            cmbCountry.Properties.DataSource =
                getCountry.OrderBy(o => o.DateTime).Select(s => new { s.Id, s.Title }).ToList();
            cmbCountry.Properties.ValueMember = "Id";
            cmbCountry.Properties.DisplayMember = "Title";
            cmbCountry.Properties.PopulateViewColumns();
            cmbCountry.Properties.PopupView.Columns["Id"].Visible = false;
            cmbCountry.Properties.EndUpdate();
        }

        bool runExporer = false;

        public void LoadOldFilm()
        {
            Ik ik = new Ik();
            var getData = ik.MultimediaHeaders
                .Where(w => !w.Delete)
                //.Where(w => w.MultiGroup1Id == 1) // ایرانی 
                //.Where(w => w.MultiGroup1Id == 1) // ایرانی 
                //.Where(w => w.MultiGroup2Id == 1) // سینمایی 
                .Where(w => _InsertNum.All(a => a != w.Num))
                //.Where(w => _InsertNum.All(a => a != w.Num) && !w.NamePersian.Contains("فصل"))
                .OrderBy(o => o.Num)
                //.OrderByDescending(o => o.DateTime)
                //.ThenByDescending(t => t.Num)
                //.ThenByDescending(t => t.Num)
                .Select(s => new { s.Id, s.NamePersian, s.Hdd.Location, s.Num }).ToList();
            lstOldFilm.DataSource = getData;
            lstOldFilm.DisplayMember = "NamePersian";
            lstOldFilm.ValueMember = "Id";
            if (runExporer == false)
            {
                runExporer = true;
                lstOldFilm.DoubleClick += async (s1, e1) =>
                {

                    long text = Convert.ToInt64(lstOldFilm.SelectedValue);
                    navigationPane1.State = NavigationPaneState.Collapsed;
                    var getDataFilm = ik.MultimediaHeaders.First(f => f.Id == text);

                    #region Get Info Video

                    //List<CastMember> getActor = GetInfoFilm(getDataFilm.NamePersian);
                    List<CastMember> getActor = null;

                    if (getActor != null)
                    {
                        //getActor.Select(s => s.name).ToList();
                        var entity = getActor.Take(7).Select(s => s.name).ToList();
                        await _multiActor.AddIfNewAsync(entity);
                    }

                    #endregion


                    #region Feed Data

                    _headerId = Guid.NewGuid();
                    txtID.Text = _headerId.ToString();
                    _oldAddress = getDataFilm.Hdd.Root;
                    bool checkAnime = false;
                    if (getDataFilm.NamePersian.Contains("انیمه"))
                    {
                        txtPersianName.Text = lblSelMovie.Text = getDataFilm.NamePersian.Replace("انیمه ", "");
                        rdoAnimeted.EditValue = @"Anime";
                        checkAnime = true;
                    }
                    else
                    {
                        txtPersianName.Text = lblSelMovie.Text = getDataFilm.NamePersian;
                        rdoAnimeted.EditValue = @"Other";
                    }

                    layoutControl1.Root.BeginUpdate();

                    //txtID.Text = getDataFilm.Id.ToString();

                    txtEnglishName.Text = getDataFilm.NameEnglish.ToString();
                    txtCode.Text = getDataFilm.Num.ToString();
                    var cmbYearText = getDataFilm.Created.Split('-');
                    if (cmbYearText.Length > 1)
                    {
                        cmbYear.Text = cmbYearText[1] + "-" + cmbYearText[0];
                    }
                    //cmbYear.Text = getDataFilm.Created.ToString();
                    Guid ccc2 = default;
                    if (getDataFilm.MultiGroup2Id == 2) // سریال
                    {
                        ccc2 = Guid.Parse("6509e3d3-ee0f-4dac-ad65-446be213d9ef");
                    }
                    else if (getDataFilm.MultiGroup2Id == 1) // سینمایی
                    {
                        ccc2 = Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5");
                    }
                    else
                    {
                        ccc2 = Guid.Parse("3e8ea833-5ca0-48d9-819e-c5049a46e3f5");
                    }

                    cmbPrice.EditValue = Guid.Parse("acec5a6d-ceea-44e0-9ec0-f4d76f301b1c");
                    rdoType.EditValue = ccc2.ToString();
                    rdoAge.EditValue = Guid.Parse(@"fc0b33ec-e830-406a-8ed5-000d59542608"); // همه سنین
                    cmbPrice.Refresh();
                    //cmbCountry.EditValue = @"17b8eece-f270-4a67-af8d-e15bb28c62e2";
                    cmbCountry.EditValue = @"fc4bc08a-bdf1-4b97-9759-75dca23de1f5";
                    cmbWeekOfDay.Text = "پایان";
                    rdoAnimeted.EditValue = "Animated";
                    //rdoAnimeted.EditValue = "Anime";
                    //rdoAnimeted.EditValue = "Other";

                    #region Image

                    var getImg1 = ik.MultimediaHeaders.First(f => f.Id == text);
                    var getAddressImg =
                        Path.Combine(uriFile, getImg1.Hdd.Location, getImg1.Num.ToString(), "Image.jpg");

                    var getImage = ClassImage.LoadBitmap(getAddressImg);

                    if (File.Exists(getAddressImg))
                        //picVideo.Image = Image.FromFile(getAddressImg);
                        picVideo.Image = getImage;
                    else
                        picVideo.Image = null;

                    #endregion

                    // rdoAge.EditValue = getDataFilm.AgeId.ToString();

                    //////var getHeaders=ik.MultimediaHeaders.First(f => f.Id == text);

                    //////var isAnimated = getHeaders.IsAnimated;
                    ////// var isAnim = getHeaders.IsAnime;

                    //////rdoAnimeted.EditValue = isAnimated switch
                    //////{
                    //////    false when isAnim is false => "Other",
                    //////    true => "Animated",
                    //////    _ => "Anime"
                    //////};

                    lstGener.UnCheckAll();
                    lstGener.BeginUpdate();

                    var getGenerList = ik.MultiGenreLists
                        .Where(w => w.MultimediaHeaderId == text)
                        .Select(s => new { s.MultimediaHeader.NamePersian, s.Id, s.MultiGenre.Title }).ToList();

                    // Clipboard.SetText(text.ToString());



                    foreach (var y in getGenerList)
                    {
                        //if (y.NamePersian.Contains("انیمیشن") && !checkAnime)
                        //    rdoAnimeted.EditValue = @"Animated";

                        if (_getGenre.ToList().Any(f => f.Title == y.Title))
                        {
                            var getIdGenre = _getGenre.ToList().First(f => f.Title == y.Title);
                            lstGener.Items[getIdGenre.Id].CheckState = CheckState.Checked;
                        }
                        //  if (y.NamePersian.Contains(y.Title))
                        //   {

                        //lstGener.Items[y.Id].CheckState = CheckState.Checked;
                        //  }
                    }


                    //lstGener.Items[y.MultiGenre.Id].CheckState = CheckState.Checked;


                    // getHeaders?.Geners)
                    //foreach (Guid guid in ik.gen)
                    //    lstGener.Items[guid].CheckState = CheckState.Checked;

                    lstGener.EndUpdate();
                    layoutControl1.Root.EndUpdate();
                    lstGener.Refresh();

                    cmbCountry.ShowPopup();

                    #endregion

                    var getHdd = getData.First(f => f.Id == lstOldFilm.SelectedValue.GetNum<long>()).Location;
                    var getNum = getData.First(f => f.Id == lstOldFilm.SelectedValue.GetNum<long>()).Num;
                    _dirFilm = $@"\\KVSERVER\{getHdd}\{getNum}";

                    Process.Start("explorer.exe", _dirFilm);
                };
            }
        }

        private string _dirFilm = "";

        public class MovieSearchResult
        {
            public List<Movie> results { get; set; }
        }

        public class Movie
        {
            public long id { get; set; }
            public string title { get; set; }
            public string overview { get; set; }
            public string release_date { get; set; }
            public string poster_path { get; set; }
        }

        public List<CastMember> GetInfoFilm(string nameMovie)
        {
            List<CastMember> getActor = null;
            string bearerToken =
                "eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiIyNzIxYjg1YjU0MjFjYmIyYTk1MDgwZGM4NzMxNGYwMCIsIm5iZiI6MTc1NjEyMDg3Mi4xNTI5OTk5LCJzdWIiOiI2OGFjNDcyOGE2ZTZiMjFiYmM0NDkwZTIiLCJzY29wZXMiOlsiYXBpX3JlYWQiXSwidmVyc2lvbiI6MX0.JR1iCGmj4LLijmolMtiy0yT16tqn01KX2wU5k1_iqH4";

            string url =
                $"https://api.themoviedb.org/3/search/movie?query={Uri.EscapeDataString(nameMovie)}&language=fa";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);

            try
            {
                HttpResponseMessage response = client.GetAsync(url).Result;
                response.EnsureSuccessStatusCode();

                string json = response.Content.ReadAsStringAsync().Result;
                MovieSearchResult result = JsonSerializer.Deserialize<MovieSearchResult>(json);

                if (result.results is { Count: > 0 })
                {
                    getActor = GetMovieCastAsync(result.results.First().id, bearerToken);
                }
                else
                {
                    Console.WriteLine("هیچ فیلمی با این عنوان پیدا نشد.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("❌ خطا در دریافت اطلاعات:");
                Console.WriteLine(ex.Message);
            }

            return getActor;
        }

        public class CastResponse
        {
            public List<CastMember> cast { get; set; }
        }

        public class CastMember
        {
            public string name { get; set; }
            public string character { get; set; }
        }

        private static List<CastMember> GetMovieCastAsync(long movieId, string bearerToken)
        {
            string url = $"https://api.themoviedb.org/3/movie/{movieId}/credits";

            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            string json = response.Content.ReadAsStringAsync().Result;
            CastResponse castResponse = JsonSerializer.Deserialize<CastResponse>(json);
            //   TranslatorService();
            return castResponse.cast;
        }

        public class ListGame
        {
            public int Id { get; set; }
            [JsonProperty("Code")] public int Num { get; set; }
            [JsonProperty("Name")] public string NamePersian { get; set; }
            public string Root { get; set; }

            public string Location { get; set; }
            //{,"Genre":null,"Poster":"/3683/Image.bmp"}
        }

        public static IEnumerable<ListGame> GetAllOld()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"http://192.168.1.160:5554/Films/");
                var response = client.GetAsync("Get").Result;
                var json = response.Content.ReadAsStringAsync().Result;
                var getJson = JsonConvert.DeserializeObject<IEnumerable<ListGame>>(json);
                return getJson;

            }
            // return null;
        }

        private void FrmManageVideo_Load(object sender, EventArgs e)
        {

            this.Shown += FrmManageVideo_Shown;
            //LoadOldFilm();

            rdoAge.SelectedIndex = -1;
            rdoAnimeted.SelectedIndex = -1;
            rdoType.SelectedIndex = -1;
        }

        private readonly DataTable _dtViewHeader = new();

        private void picVideo_ImageLoading(object sender, DevExpress.XtraEditors.Repository.SaveLoadImageEventArgs e)
        {
            if (e.FileName.ToLower().Contains(".webp"))
            {
                var getImage = e.FileName.ConvertWebpToImage();
                if (getImage.Width < 500 || getImage.Height < 650)
                {
                    e.Handled = true;
                }

                e.Image = getImage;
                //  picVideo.Image = getImage;

                e.Handled = true;
            }
        }

        private async void btnSave_Click(object sender, EventArgs e)
        {
            if (!ValidateNewFilmInputs())
            {
                ClassMessageBox.ShowMSG("لطفا اطلاعات را کامل وارد کنید", Class_Text.Msg_Name,
                    ClassMessageBox.enumIcon.هشدار);
                return;
            }

            this.WaitDownPage(async void () =>
            {
                schGener.Text = "";

                //await Task.WhenAll(
                //    SaveHeader(),
                //    //Task.Delay(1000),

                //    LoadViewHeader(),
                //    LoadVideo()


                //// LoadForSelFile()
                //);
                //LoadOldFilm();
                await SaveHeader();
                //Task.Delay(1000),

                await LoadViewHeader();
                await LoadVideo();

            });

            //Thread.Sleep(1000);
            //await LoadVideo();
        }

        public class ModelFileInfo
        {
            public Guid Id { get; set; }
            public string Location { get; set; }
            public int Count { get; set; }
            public string Root { get; set; }
        }

        private string _oldAddress = "";

        private async Task SaveHeader()
        {
            var getLocation = await _multiLocation.GetAsync();

            var getRoot = getLocation.Select(s => new { s.Id, s.DirShare_PCIP, s.Root });

            string dirVideo;
            ModelFileInfo getLocationFinal = null;
        createDir:
            #region For Edit

            MultimediaHeader getHeaderFind = null;
            var getAny = await _multiHeader.Any(_headerId);

            long getCode;
            if (!string.IsNullOrEmpty(txtCode.Text))
            {
                getCode = Convert.ToInt64(txtCode.Text);
            }
            else
            {
                getCode = await _multiHeader.GetMaxCode();
            }

            if (getAny)
            {
                getHeaderFind = await _multiHeader.GetById(_headerId);
                //getLocationFinal = fileInfos.First(f => f.Root.Contains(getMax2.Drive + ":"));
                dirVideo = Path.Combine(getRoot.First(f => f.Id == getHeaderFind.LocationId).DirShare_PCIP, getCode.ToString());
            }
            else
            {
                List<ModelFileInfo> fileInfos = new List<ModelFileInfo>();

                foreach (var location in getRoot)
                {
                    var getFiles = Directory.GetFiles(location.DirShare_PCIP, "*.*", SearchOption.AllDirectories);
                    fileInfos.Add(new ModelFileInfo
                    {
                        Id = location.Id,
                        Location = location.DirShare_PCIP,
                        Count = getFiles.Length,
                        Root = location.Root
                    });
                }
                var getHDDSize = await _multiLocation.GetSizeHDD();

                var getMax = getHDDSize.Max(m => m.Free);
                var getMax2 = getHDDSize.First(f => f.Free >= getMax);

                getLocationFinal = fileInfos.First(f => f.Root.Contains(getMax2.Drive + ":"));

                dirVideo = Path.Combine(getLocationFinal.Location, getCode.ToString());
            }

            #endregion



            var getSelGener = lstGener.CheckedItems.OfType<CheckedListBoxItem>().ToList();




            var getId = txtID.Text.ToGuid();
            var getNameFa = txtPersianName.Text.Replace(":", "-");
            var getNameEn = txtEnglishName.Text.Replace(":", "-");



            float getImdb = 0;
            try
            {
                getImdb = Convert.ToSingle(lblStar.Text);
            }
            catch (Exception e)
            {

            }
            string getDesc = "";
            if (txtPersianName.Tag != null)
                getDesc = txtPersianName.Tag.ToString();

            var getYear = cmbYear.Text;
            var getTypeId = rdoType.EditValue.ToGuid(false);
            var getAgeId = rdoAge.EditValue.ToGuid(false);
            var getCountryId = cmbCountry.EditValue.ToGuid(false);
            var getPriceId = cmbPrice.EditValue.ToGuid(false);
            var getIsAnime = rdoAnimeted.EditValue.ToString() == "Anime";
            var getIsAnimated = rdoAnimeted.EditValue.ToString() == "Animated";
            var getActors = string.Join(',', tknActor.SelectedItems.Select(s => s.Value));

            var getGeneres = string.Join(',', getSelGener.Select(s => s.Value.ToString()));

            //  var getGeneres = getSelGener.Select(s => s.Value.ToString()).ToList();
            //  var getImage = picVideo.Image;


            //var dirVideo = "";


            //حتما اصلاح شود

            //    خطا در ویرایش

            if (!getAny)
            {
                Directory.CreateDirectory(dirVideo);
                if (!Directory.Exists(dirVideo))
                {
                    Directory.CreateDirectory(dirVideo);
                    goto createDir;
                }


            }

            try
            {
                //  getImage.Save(Path.Combine(dirVideo, "Image.jpg"), ImageFormat.Jpeg);
                using Bitmap bmp = new Bitmap(picVideo.Image);
                var fnImage = Path.Combine(dirVideo, "Image.jpg");
                bmp.Save(fnImage, ImageFormat.Jpeg);

                var fnImageSmall = Path.Combine(dirVideo, "ImageSmall.jpg");
                if (File.Exists(fnImageSmall))
                    File.Delete(fnImageSmall);

            }
            catch (Exception e)
            {
                ClassMessageBox.ShowMSG(e.Message, Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);
                return;
            }


            var multimediaHeader = new MultimediaHeader();
            multimediaHeader.Id = getId;
            multimediaHeader.NamePersian = getNameFa;
            multimediaHeader.NameEnglish = getNameEn;
            multimediaHeader.Num = getCode;
            multimediaHeader.Created = getYear;
            multimediaHeader.MultimediaGroupId = getTypeId;
            multimediaHeader.AgeId = getAgeId;
            multimediaHeader.CountryId = getCountryId;
            multimediaHeader.PriceId = getPriceId;
            multimediaHeader.IsAnime = getIsAnime;
            multimediaHeader.IsAnimated = getIsAnimated;
            multimediaHeader.ActorsId = getActors;


            if (getAny == false)
                multimediaHeader.LocationId = getLocationFinal.Id;
            else
                multimediaHeader.LocationId = getHeaderFind?.LocationId;


            multimediaHeader.MultiGenreLists = getGeneres;
            multimediaHeader.DayOfWeek = (int?)cmbWeekOfDay.EditValue;
            multimediaHeader.Description = getDesc;
            multimediaHeader.IMDB = getImdb;
            await _multiHeader.AddAsync(multimediaHeader);

            var anyHeaderChapter = await _multiHeaderChapter.GetHeaderChapter(getId);
            if (!anyHeaderChapter.Any())
                await _multiHeaderChapter.AddAsync(new MultimediaHeaderChapter
                {
                    Code = 1,
                    Title = null,
                    MultimediaHeaderId = getId,
                    Created = getYear,
                });
            await LoadViewHeader();

            #region Load Chapter Details

            SelHeaderCode = getCode;

            SelHeaderFullAddress = await _multiHeader.LocationForImage(getId);
            _headerId = getId;
            await FindVideo(_headerId);
            await LoadViewHeaderChapter(_headerId);

            navigationPane1.State = NavigationPaneState.Collapsed;
            lblSelChapterHeader.Text = @"فصل های: " + lblSelMovie.Text;
            dgvChapterDetail.Enabled = false;

            //_headerId = Guid.Empty;

            #endregion



            //  LoadOldFilm();
            // ListNewFilmNull();
        }

        private void ListNewFilmNull()
        {
            var newGuid = Guid.NewGuid();
            txtID.Text = newGuid.ToString();
            _headerId = newGuid;
            lblSelMovie.Text = txtCode.Text = txtEnglishName.Text = txtPersianName.Text = "";
            lblDetailsChapter.Text = "0";
            txtPersianName.Tag = null;
            txtEnglishName.Tag = null;
            txtPersianName.SuperTip = null;
            txtEnglishName.SuperTip = null;
            lblStar.Text = "0";
            cmbYear.Text = null;
            _rdoAge = default;
            rdoAge.EditValue = null;
            rdoType.EditValue = null;
            cmbCountry.EditValue = null;
            cmbPrice.EditValue = null;
            rdoAnimeted.EditValue = null;
            lstGener.UnCheckAll();
            tknActor.EditValue = null;
            picVideo.Image = null;
            cmbWeekOfDay.EditValue = null;


            //_dtHeaderChapter.Columns.Clear();
            //_dtChapterDetails.Columns.Clear();


            _dtHeaderChapter.Rows.Clear();
            _dtChapterDetails.Rows.Clear();
        }

        private bool ValidateNewFilmInputs()
        {

            if (string.IsNullOrWhiteSpace(txtEnglishName.Text)) return false;
            if (string.IsNullOrWhiteSpace(txtPersianName.Text)) return false;

            if (string.IsNullOrWhiteSpace(cmbYear.Text)) return false;

            if (rdoAge.EditValue == null) return false;
            if (rdoType.EditValue == null) return false;
            if (cmbCountry.EditValue == null) return false;
            if (cmbPrice.EditValue == null) return false;
            if (rdoAnimeted.EditValue == null) return false;

            if (lstGener.CheckedItems.Count == 0) return false;
            //if (tknActor.EditValue == null) return false;
            if (picVideo.Image == null) return false;
            if (cmbWeekOfDay.EditValue == null) return false;

            return true;
        }


        private void btnCancel_Click(object sender, EventArgs e)
        {

            cmbCountry.EditValue = Guid.Parse("17b8eece-f270-4a67-af8d-e15bb28c62e2");
            cmbCountry.Text = "ایرانی";
            //  cmbCountry.EditValue = "17b8eece-f270-4a67-af8d-e15bb28c62e2";
            // cmbCountry.Refresh();
            // this.Refresh();
            //lstGener.Items["30243317-f5a7-49be-895a-7c03cb10622d".ToGuid()].CheckState = CheckState.Checked;
            //cmbPrice.EditValue = Guid.Parse("92b57817-5b9c-4512-bd5a-453af2f09c2a");
            //  cmbPrice.EditValue = "92b57817-5b9c-4512-bd5a-453af2f09c2a";
            // var getITem = string.Join(',', tknActor.SelectedItems.Select(s => s.Value));

            //// MessageBox.Show(cmbCountry.EditValue.ToString());
            //// C:\Users\Kavosh\Desktop\gold black 2.tif

            //var client = new HttpClient
            //{
            //    BaseAddress = new(ClassURI.GetIP)
            //};

            //using var stream = System.IO.File.OpenRead(@"C:\Users\Kavosh\Desktop\gold black 2.tif");
            //using var request = new HttpRequestMessage(HttpMethod.Post, $"Multimedia/AddHeader?data={}");
            //using var content = new MultipartFormDataContent
            //{
            //    { new StreamContent(stream), "file", "gold black 2.tif" },

            //};

            //request.Content = content;

            //var ggg = client.Send(request);
        }

        public async Task LoadViewHeader()
        {
            if (_dtViewHeader.Columns.Count == 0)
            {
                _dtViewHeader.Columns.Add("clmId", typeof(Guid));
                _dtViewHeader.Columns.Add("clmCode", typeof(long));
                _dtViewHeader.Columns.Add("clmName", typeof(string));
                _dtViewHeader.Columns.Add("clmCreated", typeof(string));
                _dtViewHeader.Columns.Add("clmGroup", typeof(string));
                _dtViewHeader.Columns.Add("clmIsActive", typeof(string));
                _dtViewHeader.Columns.Add("clmAddress", typeof(string));

                dgvHeader.DataSource = _dtViewHeader;

                #region Event

                viewHeader.RowCellClick += async (s1, e1) =>
                {
                    if (e1.Button.IsLeft())
                    {
                        var getData = e1.Column.ColumnEditName;
                        _headerId = Guid.Parse(viewHeader.GetRowCellValue(e1.RowHandle, "clmId").ToString());

                        if (getData == "btnSelect") // Select
                        {
                            SelHeaderCode =
                                Convert.ToInt64(viewHeader.GetRowCellValue(e1.RowHandle, "clmCode").ToString());
                            SelHeaderFullAddress = viewHeader.GetRowCellValue(e1.RowHandle, "clmAddress").ToString();
                            await FindVideo(_headerId);
                            await LoadViewHeaderChapter(_headerId);
                            navigationPane1.State = NavigationPaneState.Collapsed;
                            lblSelChapterHeader.Text = @"فصل های: " + lblSelMovie.Text;
                            dgvChapterDetail.Enabled = false;
                            btnCreatPart.Enabled = btnAddRowChapterDetail.Enabled = false;
                            if (dgvHeader.Parent is XtraForm form)
                            {
                                form.Close();
                            }
                        }
                        else if (getData == "btnRemove")
                        {
                            //var getStatus = MessageBox.Show(@"حذف شود ؟", @"کاوش", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                            //     MessageBoxDefaultButton.Button1);
                            //if (getStatus == DialogResult.Yes)
                            //{
                            //    // var ggg = typeof(MultimediaHeader).ToString();
                            //    // Assembly asm = typeof(MultimediaHeader).Assembly;
                            //    // Type type = asm.GetType(ggg);

                            //    var getHeaders = JsonConvert.SerializeObject(_headerId);
                            //    using (var client = new HttpClient())
                            //    {
                            //        client.BaseAddress = new Uri(ClassURI.GetIP);
                            //        var response = client.GetAsync($"Multimedia/RemoveHeader?id={getHeaders}").Result;
                            //        viewHeader.DeleteRow(e1.RowHandle);
                            //    }
                            //}
                        }
                    }
                };

                #endregion
            }


        }

        private readonly List<long> _InsertNum = new();
        private string _decAboutFilm;
        public async Task FindVideo(Guid id)
        {
            //  layoutControl1.Root.BeginUpdate();
            tknActor.EditValue = null;
            var getHeaders = await _multiHeader.GetById(id);
            lblSelMovie.Text = getHeaders.NamePersian;


            txtID.Text = getHeaders.Id.ToString();
            txtPersianName.Text = getHeaders.NamePersian.ToString();
            _decAboutFilm = getHeaders.Description;
            SetAboutFilmSuperTip(getHeaders.Description);
            lblStar.Text = getHeaders.IMDB.ToString("N1");
            txtEnglishName.Text = getHeaders.NameEnglish.ToString();
            txtCode.Text = getHeaders.Num.ToString();
            cmbPrice.EditValue = getHeaders.PriceId.ToString();
            cmbCountry.EditValue = getHeaders.CountryId;
            cmbYear.Text = getHeaders.Created.ToString();
            rdoType.EditValue = getHeaders.MultimediaGroupId.ToString();
            rdoAge.EditValue = getHeaders.AgeId;


            cmbWeekOfDay.EditValue = getHeaders.DayOfWeek.Value;


            var getGener = getHeaders.MultiGenreLists.Split(",");
            picVideo.Image = null;

            if (getHeaders.ActorsId != null)
            {
                var getActors = getHeaders.ActorsId.Split(',');

                foreach (string actor in getActors)
                {
                    tknActor.SelectItem(actor);
                }
            }

            var getLoc = await _multiHeader.LocationForImage(getHeaders.Id);
            var getLoc1 = Path.Combine(getLoc, getHeaders.Num.ToString(), "Image.jpg");
            picVideo.Image = ClassGraphic.OpenImageFrom(getLoc1);
            //picVideo.Image = Image.FromFile(getLoc1);



            var isAnimated = getHeaders.IsAnimated;
            var isAnim = getHeaders.IsAnime;

            rdoAnimeted.EditValue = isAnimated switch
            {
                false when isAnim is false => "Other",
                true => "Animated",
                _ => "Anime"
            };

            lstGener.UnCheckAll();
            lstGener.BeginUpdate();

            foreach (var guid in getGener)
                lstGener.Items[guid.ToGuid()].CheckState = CheckState.Checked;

            lstGener.EndUpdate();
            lstGener.Refresh();

            // layoutControl1.EndUpdate();


            //  layoutControl1.Root.Update();
            layoutControl1.Refresh();
        }


        bool _Poster = false;

        public void FontGrid()
        {

            Font _font = _clsFont.ChangeFont(13);


            viewHeader.Appearance.Row.Font = _font;
            viewHeader.Appearance.HeaderPanel.Font = _font;
            viewHeader.Columns["clmId"].Visible = false;

            viewHeaderChapter.Appearance.Row.Font = _font;
            viewHeaderChapter.Appearance.HeaderPanel.Font = _font;
            viewHeaderChapter.Columns["clmId"].Visible = false;

            _clsFont.ChangeFont(dgvChapterDetail);
            dgvChapterDetail.SetStyle();

            //viewChapterDetail.Appearance.Row.Font = _font;
            //viewChapterDetail.Appearance.HeaderPanel.Font = _font;
            //viewChapterDetail.Columns["Id"].Visible = false;
            //viewChapterDetail.Columns["ChapterId"].Visible = false;
        }

        private readonly DataTable _dtHeaderChapter = new();

        public async Task LoadViewHeaderChapter(Guid headerId)
        {
            if (_dtHeaderChapter.Columns.Count == 0)
            {
                _dtHeaderChapter.Columns.Add("clmId", typeof(Guid));
                _dtHeaderChapter.Columns.Add("clmCode", typeof(long));
                _dtHeaderChapter.Columns.Add("cmbNameFilm", typeof(Guid)); // نام فیلم
                _dtHeaderChapter.Columns.Add("clmNameChapter", typeof(string)); // نام فصل
                _dtHeaderChapter.Columns.Add("cmbChCreated", typeof(string)); // سال انتشار
                _dtHeaderChapter.Columns.Add("clmDescription", typeof(string)); // توضیحات

                dgvHeaderChapter.DataSource = _dtHeaderChapter;

                #region Relation


                var getHeaders = await _multiHeader.GetByModel();

                cmbNameFilm.DataSource = getHeaders.Select(s => new { s.Id, s.NamePersian }).ToList();
                cmbNameFilm.ValueMember = "Id";
                cmbNameFilm.DisplayMember = "NamePersian";
                cmbNameFilm.PopulateViewColumns();
                cmbNameFilm.View.Columns[0].Visible = false;

                cmbChCreated.DataSource = ClassYear.GetYear();
                //cmbChCreated.DataSource = ClassYear.GetYearForHeaderChapter();
                // cmbChCreated.DisplayMember = cmbChCreated.ValueMember = "";

                #endregion

                #region Event


                viewHeaderChapter.RowCellClick += async (s1, e1) =>
                {
                    if (e1.Button.IsLeft())
                    {
                        var getData = e1.Column.Name;
                        _chapterHeaderId =
                            Guid.Parse(viewHeaderChapter.GetRowCellValue(e1.RowHandle, "clmId").ToString());

                        var getChapterName = viewHeaderChapter.GetRowCellValue(e1.RowHandle, "clmNameChapter")
                            .ToString();
                        HeaderChapterCode =
                            Convert.ToInt32(viewHeaderChapter.GetRowCellValue(e1.RowHandle, "clmCode").ToString());

                        lblDetailsChapter.Text = lblSelMovie.Text + @" => " + getChapterName + @"  -  فصل: " +
                                                 HeaderChapterCode;

                        if (getData == "ChdBtnSelect") // Select
                        {

                            //lblDetailsChapter


                            lblDetailsChapter.RightToLeft = RightToLeft.Yes;
                            lblDetailsChapter.Text = lblSelMovie.Text + @" => " + getChapterName + @"  -  فصل: " +
                                                     HeaderChapterCode;

                            await LoadViewChapterDetails(_chapterHeaderId);
                            dgvChapterDetail.Enabled = true;
                            btnCreatPart.Enabled = btnAddRowChapterDetail.Enabled = true;
                            // FindVideo(_headerId);
                            // navigationPane1.State = NavigationPaneState.Collapsed;
                        }
                        else if (getData == "ChdBtnCopyTo")
                        {
                            if (lstSelFiles.DataSource != null)
                            {
                                var getListFilms2 = (lstSelFiles.DataSource as List<ModelSelFiles>)
                                    .Where(w => w.lblItem.ToLower().Contains(".mp4") ||
                                                w.lblItem.ToLower().Contains(".mkv")).OrderBy(o => o.lblRow).ToList();
                                // var getMerge = getListFilms2.Select(s => s.lblItem).ToList();
                                // string multiLineText = string.Join(Environment.NewLine, getMerge);

                                #region Get Session Chapter

                                var getMaxCode = await _multiChapterDetail.GetMaxCode(_chapterHeaderId);
                                var getMaxCodeAfter = (getListFilms2.Count + getMaxCode) - 1;

                                #endregion

                                var getQues = ClassMessageBox.ShowMSGQues(
                                    lblDetailsChapter.Text + Environment.NewLine +
                                    $"از قسمت {getMaxCode} تا {getMaxCodeAfter}" + Environment.NewLine +
                                    "                                          "
                                    , Class_Text.Msg_Name,
                                    ClassMessageBox.enumIcon.سوال);

                                bool afterPoster = false;
                                if (getQues)
                                {
                                    //var getQuesGrp = ClassMessageBox.ShowMSGQues($"ارسال به گروه ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);
                                    //if (getQuesGrp)
                                    //{
                                    //    _Poster = true;

                                    //}
                                    this.WaitDownPage(async void () =>
                                    {
                                        var getFileModel = new List<ModelFile>();

                                        //getId, "Fa-", code

                                        var getFolder = SelHeaderFullAddress;
                                        var getCodeFilm = SelHeaderCode;
                                        var getCodeHeaderChapter = HeaderChapterCode;

                                        foreach (ModelSelFiles files in getListFilms2)
                                        {
                                            var getFileInfo = Path.GetExtension(files.lblItem);
                                            //var getAddressTo = Path.Combine(getFolder, getCodeFilm.ToString(), getCodeHeaderChapter.ToString());
                                            var getAddressTo = Path.Combine(getFolder, getCodeFilm.ToString(),
                                                getCodeHeaderChapter.ToString(), "Fa-" + getMaxCode + getFileInfo);

                                            getFileModel.Add(new ModelFile
                                            {
                                                ChapterHeaderId = _chapterHeaderId,
                                                Name = txtPersianName.Text + "-" + "Fa-" + getMaxCode,
                                                From = files.lblItem,
                                                To = getAddressTo,
                                            });
                                            getMaxCode++;
                                        }

                                        var getMultiArg = ClassFillToCopy.FillToCopy(getFileModel) + " " + Guid.Empty +
                                                          " " + getCodeFilm + " " + "AddFast";
                                        //await File.WriteAllTextAsync("d:\\__CopyTest.txt", getMultiArg);
                                        // Clipboard.SetText(getMultiArg);
                                        RunExecutable(getMultiArg);
                                        await LoadViewChapterDetails(_chapterHeaderId);

                                    });




                                    //dgvHeaderChapter
                                }
                            }
                        }
                    }
                    //if (e1.Button.IsLeft())
                    //{
                    //    var getData = e1.Column.FieldName;
                    //    if (getData == "") // Select
                    //    {
                    //        _headerId = Guid.Parse(viewHeader.GetRowCellValue(e1.RowHandle, "clmId").ToString());
                    //        FindVideo(_headerId);
                    //        LoadViewHeaderChapter(_headerId);
                    //        navigationPane1.State = NavigationPaneState.Collapsed;
                    //    }
                    //}
                };
                viewHeaderChapter.ShowingPopupEditForm += (s1, e1) =>
                {
                    //var getId = viewPerson.GetRowCellValue(e1.RowHandle, "clmId").ToString();

                    Font _font = new Font("Samim FD", 12F);
                    e1.EditForm.Height = 330;
                    e1.EditForm.Width = 800;
                    e1.EditForm.RightToLeft = RightToLeft.Yes;
                    e1.EditForm.RightToLeftLayout = true;
                    foreach (Control control in e1.EditForm.Controls)
                    {
                        foreach (Control childControl in control.Controls)
                        {
                            if (childControl is PanelControl)
                            {
                                var getCtrl = ((PanelControl)childControl).Controls;
                                // (childControl as TextEdit).AutoSize = true;
                                //  (childControl as TextEdit).Font = _font;
                            }

                            foreach (Control childChildControl in childControl.Controls)
                            {
                                var getCtrl = (childChildControl).Controls;
                                foreach (Control myEnd in childChildControl.Controls)
                                {
                                    //TableLayoutPanel
                                    if (myEnd is GridLookUpEdit)
                                    {
                                        (myEnd as GridLookUpEdit).AutoSize = true;
                                        (myEnd as GridLookUpEdit).Font = _font;
                                        (myEnd as GridLookUpEdit).RightToLeft = RightToLeft.No;
                                    }

                                    else if (myEnd is TableLayoutPanel)
                                    {
                                        foreach (var memo in myEnd.Controls.OfType<LabelControl>())
                                        {
                                            memo.AutoSize = true;
                                            memo.Font = _font;
                                            memo.RightToLeft = RightToLeft.Yes;
                                        }
                                    }

                                    else if (myEnd is LabelControl)
                                    {
                                        (myEnd as LabelControl).AutoSize = true;
                                        (myEnd as LabelControl).Font = _font;
                                        var getControl = (myEnd as LabelControl).Parent;
                                        (getControl as TableLayoutPanel).AutoSize = false;
                                        (getControl as TableLayoutPanel).AutoSize = true;
                                    }
                                    else if (myEnd is TextEdit)
                                    {
                                        (myEnd as TextEdit).AutoSize = true;
                                        (myEnd as TextEdit).Font = _font;
                                        (myEnd as TextEdit).RightToLeft = RightToLeft.Yes;
                                    }

                                }

                                if (childChildControl is SimpleButton)
                                {
                                    var btn = (childChildControl as SimpleButton);
                                    btn.Height = 30;
                                    btn.Top = 15;
                                    //  btn.AutoSize = true;
                                    btn.Font = _font;
                                    if (btn.Text.ToLower() == "update")
                                        btn.Text = @"ذخیره";
                                    else if (btn.Text.ToLower() == "cancel")
                                        btn.Text = @"انصراف";
                                }

                                //if (childChildControl is LabelControl)
                                //{
                                //    var gettetx = childChildControl.Text;
                                //    var getname = childChildControl.Name;
                                //    (childChildControl as LabelControl).AutoSize = true;
                                //    (childChildControl as LabelControl).Font = _font;
                                //}
                            }
                        }
                    }
                };
                viewHeaderChapter.InitNewRow += async (s1, e1) =>
                {
                    viewHeaderChapter.BeginDataUpdate();
                    viewHeaderChapter.SetRowCellValue(e1.RowHandle, "cmbNameFilm", _headerId);
                    var newGuid = Guid.NewGuid();
                    viewHeaderChapter.SetRowCellValue(e1.RowHandle, "clmId", newGuid);



                    var getColumn = viewHeaderChapter.Columns.Select(s => new { s.Name, s.FieldName }).ToList();
                    var getMaxCode = await _multiHeaderChapter.GetAsync();

                    var getMaxCode1 = getMaxCode.Where(w => w.MultimediaHeaderId == _headerId).ToList()
                        .Select(s => s.Code).DefaultIfEmpty(0).Max() + 1;
                    viewHeaderChapter.SetRowCellValue(e1.RowHandle, "clmCode", getMaxCode1);
                    viewHeaderChapter.EndDataUpdate();
                    viewHeaderChapter.RefreshRow(e1.RowHandle);

                };
                viewHeaderChapter.ValidateRow += async (s1, e1) =>
                {
                    var dr = (e1.Row as DataRowView);

                    var getId = viewHeaderChapter.GetRowCellValue(e1.RowHandle, "clmId");

                    //var getId = dr["clmId"].ToString(); خطا یا اشتباه در ID
                    HeaderChapterCode = Convert.ToInt32(dr["clmCode"].ToString());
                    //var getNameFilm = _headerId;
                    var getNameChapter = dr["clmNameChapter"].ToString();
                    var getCreated = dr["cmbChCreated"].ToString();
                    var getDescription = dr["clmDescription"].ToString();

                    var multimediaHeaderChapter = new MultimediaHeaderChapter
                    {
                        Id = getId.ToGuid(),
                        Code = HeaderChapterCode,
                        Title = getNameChapter,
                        Created = getCreated,
                        Description = getDescription,
                        MultimediaHeaderId = txtID.Text.ToGuid()
                    };
                    await _multiHeaderChapter.AddAsync(multimediaHeaderChapter);
                    var getAddress = Path.Combine(SelHeaderFullAddress, SelHeaderCode.ToString(),
                        HeaderChapterCode.ToString());
                    Directory.CreateDirectory(getAddress);

                    //  SaveHeaderChapter(getId, getCreated, getDescription, getNameChapter, getNameFilm);
                };

                #endregion
            }

            #region Get Data Chapter

            _dtHeaderChapter.Rows.Clear();
            //FindHeaderChapter
            if (headerId != default)
            {
                var items = await _multiHeaderChapter.GetHeaderChapter(_headerId);

                foreach (var item in items.OrderBy(o => o.Code))
                {
                    _dtHeaderChapter.Rows.Add(item.Id, item.Code, item.MultimediaHeaderId, item.Title, item.Created,
                        item.Description);
                }
            }


            #endregion

        }



        ////////#endregion

        #region Details Chapter

        private DataTable _dtChapterDetails = new();

        private XtraOpenFileDialog xofd = new()
        {
            Title = @"انتخاب ویدیو",
            CheckFileExists = true,
            Multiselect = false,
            KeepPosition = true,
            RestoreDirectory = true,
            StartPosition = FormStartPosition.CenterScreen,
        };

        private int _selRowChapterDetail = -1;
        private string _selClmChapterDetail;
        private long _selCodeChapterDetails = -1;

        public async Task LoadViewChapterDetails(Guid headerChapterId)
        {
            this.WaitDownPage(async () =>
            {
                var getFocus = dgvChapterDetail.GetViewBase.FocusedRowHandle;
                if (dgvChapterDetail.ColumnCount() == 0)
                {
                    _dtChapterDetails =
                        dgvChapterDetail.GridStructure(new List<KavoshGrid.modelColumn>
                        {
                            new() { Name = "Id", Type = typeof(Guid) },
                            new() { Name = "ChapterId", Type = typeof(Guid) },
                            new() { Name = "قسمت", Type = typeof(int) },

                            new() { Name = "دوبله", Object = KavoshGrid.enumObject.Link },
                            new() { Name = "زیرنویس", Object = KavoshGrid.enumObject.Link },


                            new() { Name = "کیفیت پرده", Type = typeof(bool), Object = KavoshGrid.enumObject.Checked },
                            new() { Name = "وضعیت", Type = typeof(bool), Object = KavoshGrid.enumObject.Checked },

                            new() { Name = "Fa", Type = typeof(bool) },
                            new() { Name = "En", Type = typeof(bool) },
                        }, false, false, false);
                    dgvChapterDetail.HiddenColumn(["Id", "ChapterId", "Fa", "En"]);

                    #region Event

                    dgvChapterDetail.GetViewBase.SelectionChanged += (s1, e1) => { };
                    dgvChapterDetail.DragOver += (s1, e1) => { };

                    dgvChapterDetail.AddEventRowCellClick<int>(async void (code) =>
                    {
                        var getId = dgvChapterDetail.GetValue<Guid>("Id");
                        await FunCopyVideoEvent(getId, "Fa-", code);
                    }, "قسمت", "دوبله");

                    dgvChapterDetail.AddEventRowCellClick<int>(async void (code) =>
                    {
                        var getId = dgvChapterDetail.GetValue<Guid>("Id");
                        await FunCopyVideoEvent(getId, "En-", code);
                    }, "قسمت", "زیرنویس");

                    dgvChapterDetail.AddEventRowCellClick<bool>(async void (value) =>
                    {
                        var getId = dgvChapterDetail.GetValue<Guid>("Id");
                        await _multiChapterDetail.IsWebDl(getId, !value);
                        await LoadViewChapterDetails(_chapterHeaderId);

                    }, "کیفیت پرده", "کیفیت پرده");

                    dgvChapterDetail.GetViewBase.RowCellStyle += (s1, e1) =>
                    {
                        var getClm = e1.Column.FieldName;
                        if (getClm == "دوبله")
                        {
                            var getValue = dgvChapterDetail.GetValueRowHandle<bool>("Fa", e1.RowHandle);
                            e1.Appearance.BackColor = getValue ? ClsUI.Ok : ClsUI.NotOk;
                        }
                        else if (getClm == "زیرنویس")
                        {
                            var getValue = dgvChapterDetail.GetValueRowHandle<bool>("En", e1.RowHandle);
                            e1.Appearance.BackColor = getValue ? ClsUI.Ok : ClsUI.NotOk;
                        }
                    };

                    #endregion
                }

                #region Load Data

                #region Get Files

                var getFolder = SelHeaderFullAddress;
                var getCodeFilm = SelHeaderCode;
                var getCodeHeaderChapter = HeaderChapterCode;
                var getAddressTo = Path.Combine(getFolder, getCodeFilm.ToString(), getCodeHeaderChapter.ToString());
                _dtChapterDetails.Rows.Clear();
                Directory.CreateDirectory(getAddressTo);
                if (Directory.Exists(getAddressTo))
                {
                    var getFiles = Directory.GetFiles(getAddressTo)
                        .Select(Path.GetFileNameWithoutExtension)
                        .ToList();

                    #endregion


                    var getData = await _multiChapterDetail.GetByHeaderChapter(headerChapterId);
                    foreach (MultimediaChapterDetail detail in getData.OrderBy(o => o.Code))
                    {
                        //En-1.mp4
                        string faa = $"Fa-{detail.Code}";
                        bool Fa = false;
                        bool En = false;
                        if (getFiles.Contains(faa))
                            Fa = true;
                        if (getFiles.Contains($"En-{detail.Code}"))
                            En = true;

                        //   var getFileInfo = new FileInfo();

                        _dtChapterDetails.Rows.Add(
                            detail.Id,
                            detail.HeaderChapterId,
                            detail.Code,
                            "دوبله",
                            "زیرنویس",
                            //  detail.IsFarsi,
                            //  detail.IsEnglish,
                            detail.IsWebDl,
                            detail.IsActive,
                            Fa,
                            En);

                    }
                }

                #endregion

                dgvChapterDetail.GetViewBase.FocusedRowHandle = getFocus;
            });
        }

        private async Task FunCopyVideoEvent(Guid id, string getFa, int Code)
        {
            //  int obj;
            OpenFileDialog ofdFa = new OpenFileDialog
            {
                Title = "انتخاب فایل دوبله",
                Filter = "Video Files|*.mp4;*.mkv",
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true
            };
            var getStatus = ofdFa.ShowDialog();
            if (getStatus == DialogResult.OK)
            {
                //     \\MOJTABAPC\Video1\3692\1
                var getFolder = SelHeaderFullAddress;
                var getCodeFilm = SelHeaderCode;
                var getCodeHeaderChapter = HeaderChapterCode;
                // var getFa = "Fa-";
                var getCodeChapter = Code.ToString();
                var getFileInfo = Path.GetExtension(ofdFa.FileName);
                var getAddressTo = Path.Combine(getFolder, getCodeFilm.ToString(), getCodeHeaderChapter.ToString(),
                    getFa + getCodeChapter + getFileInfo);

                var getIsWebDl = ClassMessageBox.ShowMSGQues("آیا کیفیت فیلم معمولی می باشد ؟", Class_Text.Msg_Name,
                    ClassMessageBox.enumIcon.سوال);
                if (!getIsWebDl)
                {
                    await _multiChapterDetail.IsWebDl(id, true);
                }

                #region Copy

                var getfileModel = new List<ModelFile>
                {
                    new()
                    {
                        ChapterHeaderId = id,
                        Name = txtPersianName.Text + "-" + getFa + Code,
                        From = ofdFa.FileName,
                        To = getAddressTo
                    },
                };
                var getMultiArg = ClassFillToCopy.FillToCopy(getfileModel) + " " + Guid.Empty + " " + getCodeFilm;
                RunExecutable(getMultiArg);
                await LoadViewChapterDetails(_chapterHeaderId);

                #endregion
            }
        }



        private async void btnAddRowChapterDetail_Click(object sender, EventArgs e)
        {
            btnCreatPart.Enabled = btnAddRowChapterDetail.Enabled = false;

            await _multiChapterDetail.AddChapterCounts(_chapterHeaderId, 1);

            await LoadViewChapterDetails(_chapterHeaderId);
            btnCreatPart.Enabled = btnAddRowChapterDetail.Enabled = true;
        }

        #endregion

        private async void btnSendVideo_Click(object sender, EventArgs e)
        {
            // UploadVideoDetailChapter();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ManagementObjectSearcher search = new
            //    ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");

            //foreach (ManagementObject wmi in search.Get())
            //{
            //    Console.WriteLine(wmi["Manufacturer"].ToString());
            //}
        }

        private void txtAddActor_KeyDown(object sender, KeyEventArgs e)
        {
            XtraMessageBox.SmartTextWrap = true;
            XtraMessageBoxForm messageBox = new XtraMessageBoxForm();
            messageBox.Font = new Font("Samim FD", 12);
            messageBox.Appearance.ForeColor = ClsUI.NotOk;
            messageBox.DialogResult = DialogResult.Yes;

            // Description: Test Dialog 

            //  messageBox.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            //  Specify the skin.
            //  messageBox.Properties.LookAndFeel.SkinName = "Seven Classic";

            // UserLookAndFeel andFeel = new UserLookAndFeel("");

            //if (e.KeyCode == Keys.Enter)
            //{
            //    XtraMessageBoxArgs boxArgs = new XtraMessageBoxArgs(UserLookAndFeel.Default, this, txtAddActor.Text, "ایران کاوش", new[]
            //    {
            //        DialogResult.Yes,DialogResult.No
            //    });
            //    boxArgs.LookAndFeel = new DevExpress.LookAndFeel.UserLookAndFeel(this);
            //    boxArgs.LookAndFeel.UseDefaultLookAndFeel = false;
            //    boxArgs.LookAndFeel.SetSkinStyle(DevExpress.LookAndFeel.SkinStyle.Office2016Colorful);
            //    var getStatus = messageBox.ShowMessageBoxDialog(boxArgs);
            //    if (getStatus == DialogResult.Yes)
            //    {
            //        using (var Client = new HttpClient())
            //        {
            //            ModelTitle actor = new ModelTitle
            //            {
            //                Id = Guid.NewGuid(),
            //                Title = txtAddActor.Text
            //            };
            //            var getActor = JsonSerializer.Serialize(actor);

            //            Client.BaseAddress = new Uri(ClassURI.GetIP);
            //            using var request = new HttpRequestMessage(HttpMethod.Post,
            //                $"Multimedia/AddUpdateActor?data={getActor}");
            //            var ggg = Client.Send(request);

            //            tknActor.Properties.Tokens.AddToken(actor.Title, actor.Id.ToString());
            //            tknActor.SelectItem(actor.Id.ToString());
            //            txtAddActor.Text = "";
            //        }
            //    }
            //}
        }

        private void tknActor_SelectedItemsChanged(object sender, System.ComponentModel.ListChangedEventArgs e)
        {

        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ListNewFilmNull();


            if (_selImageList != null)
                picVideo.Image = _selImageList;
        }

        private void navigationPane1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var getfileModel = new List<ModelFile>
            {
                new()
                {
                    From = @"F:\Games Demo\watch_dogs 2014-12-23 17-38-46-657.avi",
                    To = @"L:\KavoshTest\3.avi"
                },
                new()
                {
                    //  From = @"F:\Games Demo\watch_dogs 2014-12-23 18-15-16-992.avi",
                    From = @"F:\Games Demo\Need For Speed The Run\Need For Speed.wmv",
                    To = @"L:\KavoshTest\1.avi"
                },
                new()
                {
                    From = @"F:\Games Demo\watch_dogs 2014-12-23 18-06-15-501.avi",
                    To = @"L:\KavoshTest\2.avi"
                },


            };
            var Json = JsonConvert.SerializeObject(getfileModel);
            var encrypt = ClassEncryptDecrypt.Encrypt(Json, "IranKavosh123456");


            //var dec = ClassEncryptDecrypt.Decrypt(encrypt, "IranKavosh123456");
            //var  Json2 = JsonConvert.DeserializeObject<List<ModelFile>>(dec);

            RunExecutable(encrypt);

            //C:\Users\Mojtaba\source\repos\IranKavosh\IranKavosh.Layered.FileCopy\bin\Debug\net9.0-windows\IranKavosh.Layered.FileCopy.dll
        }

        //public void RunExecutable(string arguments)
        //{
        //    //C:\Users\Mojtaba\source\repos\IranKavosh\IranKavosh.Layered.FileCopy\bin\Debug\net9.0-windows\
        //    var process = new Process();
        //    process.StartInfo.FileName =
        //        @"C:\Users\Mojtaba\source\repos\IranKavosh\IranKavosh.Layered.FileCopy\bin\Debug\net9.0-windows\IranKavosh.Layered.FileCopy.exe";
        //    process.StartInfo.Arguments = arguments;
        //    process.StartInfo.UseShellExecute = false;132
        //    //process.StartInfo.CreateNoWindow = true;
        //    process.Start();
        //}
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

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await LoadViewChapterDetails(_chapterHeaderId);
        }



        private async void txtPersianName_Leave(object sender, EventArgs e)
        {
            //if (txtPersianName.Text.Length > 2)
            //{
            //    await GetNameActorRealTimeTextBox(txtPersianName.Text);
            //}
        }

        private async void txtEnglishName_Leave(object sender, EventArgs e)
        {
            //if (txtEnglishName.Text.Length > 2)
            //{
            //    var getCnt = tknActor.SelectedItems.Select(s => s.Value);
            //    if (!getCnt.Any())
            //    {
            //        await GetNameActorRealTimeTextBox(txtEnglishName.Text);
            //    }

            //}

        }

        private async Task GetNameActorRealTimeTextBox(string nameMovie)
        {
            ////List<CastMember> getActor = GetInfoFilm(txtPersianName.Text);
            //List<CastMember> getActor = null;

            //if (getActor != null)
            //{
            //    //getActor.Select(s => s.name).ToList();
            //    var entity = getActor.Take(7).Select(s => s.name).ToList();
            //    await _multiActor.AddIfNewAsync(entity);
            //}

            //tknActor.EditValue = null;
            //tknActor.Properties.Tokens.Clear();
            ////   tknActor.Properties.sty
            //var getActors = await _multiActor.GetAsync();
            //if (getActors != null)
            //{
            //    foreach (var actor in getActors)
            //    {
            //        tknActor.Properties.Tokens.Add(new TokenEditToken
            //        {
            //            Value = actor.Id.ToString(),
            //            Description = actor.ActorName != "" ? actor.ActorName : actor.ActorNameEn,
            //        });
            //    }

            //    #region Sel Actor

            //    var matchedIds = getActors
            //        .Where(actor => getActor != null && getActor.Any(c =>
            //            string.Equals(c.name?.Trim(), actor.ActorNameEn?.Trim(),
            //                StringComparison.OrdinalIgnoreCase)))
            //        .Select(actor => actor.Id).Take(7)
            //        .ToList();

            //    foreach (Guid id in matchedIds)
            //    {
            //        tknActor.SelectItem(id.ToString());
            //    }

            //    #endregion
            //}
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            await LoadVideo();
        }

        private async void btnCreatPart_Click(object sender, EventArgs e)
        {



            #region ساخت تعداد فیلم های قدیمی

            if (!string.IsNullOrEmpty(_dirFilm))
            {
                var getDir = Directory.GetFiles(_dirFilm).Where(w => w.Contains(".mp4") || w.Contains(".mkv"));


                await _multiChapterDetail.AddChapterCounts(_chapterHeaderId, getDir.Count());


                //foreach (var s in getDir)
                //{
                //    if (s.Contains(".mp4") || s.Contains(".mkv"))
                //    {
                //        //var getMaxCode = await _multiChapterDetail.GetMaxCode(_chapterHeaderId);
                //        await _multiChapterDetail.AddAsync(new MultimediaChapterDetail
                //        {
                //            HeaderChapterId = _chapterHeaderId,

                //        });
                //        // await LoadViewChapterDetails(_chapterHeaderId);
                //    }

                //}

                _dirFilm = "";
            }

            #endregion
        }

        private void FrmManageVideo_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private bool finishGrp = false;

        public async Task CreatePoster()
        {
            if (_Poster)
            {
                try
                {
                    // await new ClsPublic().CreatePosterByHeader(_headerId);

                    #region Check File

                    string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    var getDesk = Path.Combine(desktopPath, "پوستر فیلم");
                    string addressPoster = Path.Combine(getDesk, txtPersianName.Text + ".jpg");

                    #endregion

                    if (File.Exists(addressPoster))
                    {
                        File.Delete(addressPoster);
                        stopTask = false;
                        finishGrp = true;
                        Close();
                    }

                }
                catch (Exception e)
                {

                }
                // await Task.Delay(1000);
            }
            else
            {
                //MessageBox.Show("True");
                stopTask = false;
                finishGrp = true;
                //Close();

            }
        }

        Task task;
        private bool oneCloseClick = false;
        private bool stopTask = true;

        private void FrmManageVideo_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //if (finishGrp)
            //{
            //    e.Cancel = false;
            //}
            //if (!oneCloseClick)
            //{
            //    Action action = async () =>
            //    {

            //        while (stopTask)
            //        {
            //            await CreatePoster();
            //            await Task.Delay(1000);
            //            if (!finishGrp)
            //            {
            //                e.Cancel = true;
            //            }
            //        }
            //    };

            //    task = new Task(action);
            //    task.Start();
            //    oneCloseClick = true;
            //}



        }

        private async void btnFindInfoFilm_Click(object sender, EventArgs e)
        {
            //layoutControl1.Invoke(() => );



            ImdbService imdbService = new ImdbService();

            var txtFa = txtPersianName.Text;
            var txtEn = txtEnglishName.Text;

            await imdbService.Search(txtFa, txtEn);
            await imdbService.ShowResult();
            var getSel = imdbService.SelectedMovie;
            if (getSel is not null)
            {
                //DownloadTriler(getSel);


                //UpdateField(getSel, imdbService);
                await Task.WhenAll(
                    //DownloadTriler(getSel),
                    UpdateField(getSel, imdbService)
                );
            }
        }

        private async Task UpdateField(MovieInfo getSel, ImdbService imdbService)
        {
            layoutControl1.WaitDownPage(async () =>
            {
                var getQues = ClassMessageBox.ShowMSGQues("آیا در حال ثبت فیلم جدید هستید ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);
                if (getQues)
                {
                    ListNewFilmNull();
                }
                else
                {
                    picVideo.Image = null;
                    tknActor.EditValue = null;
                    lstGener.UnCheckAll();
                }

                await Task.Delay(150);
                _decAboutFilm = getSel.Description;
                SetAboutFilmSuperTip(getSel.Description);
                await Task.Delay(150);
                lblStar.Text = getSel.ImdbRating;
                await Task.Delay(250);

                #region کشور

                if (getSel.Country != null)
                {
                    var country = getSel.Country.ToLower();
                    if (country.Contains("united state") || country.Contains("united states"))
                    {
                        cmbCountry.EditValue = @"fc4bc08a-bdf1-4b97-9759-75dca23de1f5";
                        //cmbCountry.EditValue = Guid.Parse(@"fc4bc08a-bdf1-4b97-9759-75dca23de1f5");

                    }
                    else if (country.Contains("japan"))
                    {
                        cmbCountry.EditValue = "940782e8-4a4f-4d00-b2be-93219f3e3ee4";
                    }
                    else if (country.Contains("iran"))
                    {
                        cmbCountry.EditValue = "17b8eece-f270-4a67-af8d-e15bb28c62e2";
                    }
                    else if (country.Contains("india"))
                    {
                        cmbCountry.EditValue = "c219de1c-00cd-4195-aad0-fff47b44401e";
                    }
                    else
                    {
                        cmbCountry.EditValue = @"e4ccc9ad-16e3-4921-a21d-d65581cb092c";
                    }
                }
                else
                {
                    cmbCountry.EditValue = @"e4ccc9ad-16e3-4921-a21d-d65581cb092c";
                }

                #endregion

                await Task.Delay(150);
                picVideo.Image = getSel.Poster;

                txtEnglishName.Text = getSel.TitleEn;
                await Task.Delay(150);
                if (getSel.Sinema)
                    rdoType.EditValue = @"3e8ea833-5ca0-48d9-819e-c5049a46e3f5";
                if (getSel.Serial)
                    rdoType.EditValue = @"6509e3d3-ee0f-4dac-ad65-446be213d9ef";

                rdoAge.EditValue = Guid.Parse(@"fc0b33ec-e830-406a-8ed5-000d59542608"); // همه سنین
                await Task.Delay(150);

                #region سال

                var getY = ClassYear.GetYear().First(f => f.Contains(getSel.Year));
                cmbYear.Text = getY;

                #endregion

                await Task.Delay(150);
                if (getSel.Animation)
                    rdoAnimeted.EditValue = @"Animated";
                else if (getSel.Anime)
                    rdoAnimeted.EditValue = @"Anime";
                else
                    rdoAnimeted.EditValue = @"Other";

                #region بازیگران
                try
                {
                    var getActors = await _multiActor.GetAsync();
                    if (getSel.Actors != null)
                    {

                     

                        var getActs = getSel.Actors.Split(", ");

                        #region CheckBeforeAdd

                        //var matchedIdsOld = getActors
                        //    .Where(actor => getActs.Any(c =>
                        //        string.Equals(c.Trim(), actor.ActorNameEn.Trim(), StringComparison.OrdinalIgnoreCase)))
                        //    .Select(actor => actor.Id).Take(7)
                        //    .ToList();

                        #endregion


                        var acts = new List<ModelActor>();
                        foreach (var act in getActs)
                        {
                            string fa;
                            try
                            {
                                fa = await imdbService.GoogleTranslate(act);
                            }
                            catch (Exception e)
                            {
                                fa = getActors.FirstOrDefault(f => f.ActorNameEn == act).ActorName;
                            }
                            acts.Add(new ModelActor
                            {
                                En = act,
                                Fa = fa
                            });
                        }


                        await Task.Delay(50);
                        await _multiActor.AddIfNewFullAsync(acts);
                        await Task.Delay(200);
                        await LoadActors();
                        await Task.Delay(150);
                        getActors = await _multiActor.GetAsync();
                        await Task.Delay(150);
                        var matchedIds = getActors
                            .Where(actor => acts.Any(c =>
                                string.Equals(c.Fa.Trim(), actor.ActorName.Trim(), StringComparison.OrdinalIgnoreCase)))
                            .Select(actor => actor.Id).Take(7)
                            .ToList();


                        await Task.Delay(120);
                        tknActor.Properties.BeginUpdate();

                        foreach (var actor in matchedIds)
                        {
                            await Task.Delay(10);
                            tknActor.SelectItem(actor.ToString());
                        }



                        tknActor.Properties.EndUpdate();
                        tknActor.Update();
                        tknActor.Refresh();
                    }
                }
                catch (Exception e)
                {

                }

                #endregion

                await Task.Delay(150);

                #region Genre

                var genres = (await Task.WhenAll(
                    getSel.Genre.Split(", ").Where(w => !w.ToLower().Contains("animation")).Select(async s =>
                        new ModelGenre
                        {
                            //Title = await imdbService.GoogleTranslate(s),
                            TitleEn = s
                        })
                )).ToList();

                await Task.Delay(150);
                await _multiGenre.AddIfNewAsync(genres);
                await Task.Delay(150);
                await LoadMultiGener();
                await Task.Delay(150);

                foreach (var title in genres)
                {

                    var getId = _getGenre.First(f => f.TitleEn.ToLower() == title.TitleEn.ToLower()).Id;
                    lstGener.Items[getId].CheckState = CheckState.Checked;
                }

                #endregion

            });
        }

        private async Task DownloadTriler(MovieInfo getSel)
        {

            var progress = new Progress<int>(percent =>
            {
                barDownload.Position = percent;
            });

            layoutControl1.WaitDownPage(async void () =>
            {
                try
                {
                    if (getSel.TitleEn != null && getSel.Year != null)
                    {

                        var getQues = ClassMessageBox.ShowMSGQues("تیزر فیلم دانلود شود ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);
                        if (getQues)
                        {
                            //await ClassYoutubeService.GetTrailer(getSel.TitleEn, Convert.ToInt32(getSel.Year), "C:\\Users\\Mojtaba\\Desktop\\فیلم\\تیزر", TrailerDownloader.TrailerQuality.Q480, progress);

                            #region New

                            var closePort = PortManager.CloseProcessesOnPort(10808);
                            //string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Downloads");
                            string outputFolder = Path.Combine(@"C:\Users\Mojtaba\Desktop\فیلم\تیزر");
                            var ytdlp = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "yt-dlp.exe");

                            var ytDlpOptions = new YtDlpOptions
                            {
                                YtDlpExecutablePath = ytdlp,
                                OutputDirectory = outputFolder,
                                AutoUpdateBeforeRun = false,
                                EnablePoTokenProvider = false,
                                CookiesFilePath = @"M:\IranKavosh 2026\IranKavosh.Layered.WinUI\bin\Debug\net9.0-windows\cookies.txt"
                            };

                            var cookieFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "cookies.txt");


                            using var proxyManager = new ProxyManager(socksPort: 10808);
                            using var ytDlpManager = new YtDlpManager(ytDlpOptions);
                            var youtubeService =
                                new YoutubeMovieService(proxyManager, ytDlpManager, cookieFile: cookieFile);

                            var movieName = getSel.TitleEn.Replace(":", "-");
                            var result =
                                await youtubeService.DownloadMovieTrailerAsync(movieName, Convert.ToInt32(getSel.Year));

                            #endregion

                            //if (result.Success)
                            //{

                            //    var dirVideo = Path.Combine(outputFolder, "Trl=" + movieName + "_" + getSel.Year+".mp4");
                            //    if (File.Exists(dirVideo))
                            //    {
                            //        await Task.Delay(400);
                            //        await new ClassConvertVideo().ConvertAllFilesAsync(VideoCodec.h264_nvenc, [dirVideo], ClassConvertVideo.SizeVideo.trl, progreesDownload);
                            //    }
                            //}

                        }

                    }
                }
                catch (Exception e)
                {

                }
            }, "در حال دانلود");
        }

        private void SetAboutFilmSuperTip(string text)
        {

            var superToolTip = new SuperToolTip
            {

                AllowHtmlText = DefaultBoolean.True,
                Items =
                {
                    new ToolTipTitleItem
                    {
                        Text = "درباره فیلم",
                        AllowHtmlText = DefaultBoolean.True,
                        Font = _clsFont.ChangeFont(13),
                        Appearance = { TextOptions = {HAlignment = HorzAlignment.Center,RightToLeft = true},BackColor = Color.FromArgb(195, 218, 37)}
                    },
                    new ToolTipTitleItem
                    {

                        Text = text,
                        AllowHtmlText = DefaultBoolean.True,
                        Font = _clsFont.ChangeFont(15),
                        Appearance = { TextOptions = { WordWrap = WordWrap.Wrap ,RightToLeft = true,HAlignment = HorzAlignment.Near,VAlignment = VertAlignment.Center},ForeColor = Color.Red}
                    }
                }
            };
            txtPersianName.SuperTip = superToolTip;
            txtEnglishName.SuperTip = superToolTip;
            txtPersianName.Tag = text;
        }

        private async void tknActor_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {

                //var getActors = getHeaders.ActorsId.Split(',');
                var modelActorsList = new List<FrmActorss.ModelActors>();
                //var getActors = string.Join(',', tknActor.SelectedItems.Select(s => new FrmActorss.ModelActors { Id = s.Value.ToGuid(),En= s.Description})).ToList();
                var getActors = tknActor.SelectedItems.Select(s => new FrmActorss.ModelActors { Id = s.Value.ToGuid(), En = s.Description }).ToList();

                foreach (var a in getActors)
                {
                    modelActorsList.Add(new FrmActorss.ModelActors
                    {
                        Id = a.Id,
                        En = a.En,
                        Fa = a.Fa
                    });
                }

                var frmAct = new FrmActorss(modelActorsList, _decAboutFilm);
                frmAct.ShowDialog();
                var getTran = frmAct._actorsList;
                
                if (getTran != null)
                {

                    List<MultiActor> getData1 = getTran.Select(s => new MultiActor
                        { Id = s.Id, ActorNameEn = s.En, ActorName = s.Fa }).ToList();

                    foreach (var m in getData1)
                    {
                        await _multiActor.UpdateAsync(m);
                    }
                }

                if (frmAct._description != null)
                    SetAboutFilmSuperTip(frmAct._description);
                //await _multiHeader.UpdateDescriptionAsync(_headerId, _decAboutFilm);
            }
        }
    }
}





