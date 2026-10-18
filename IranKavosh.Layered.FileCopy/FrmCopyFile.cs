using DevExpress.Data.Helpers;
using DevExpress.XtraBars.Navigation;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout.Utils;
using DocumentFormat.OpenXml.Presentation;
using IranKavosh.Layered.Model;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using MediaDevices;
using Microsoft.WindowsAPICodePack.Taskbar;
using MyCom.Class;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.IO;
using System.Media;
using System.Net.Http;
using System.Runtime.InteropServices;
using static IranKavosh.Layered.FileCopy.Program;

namespace IranKavosh.Layered.FileCopy;

public partial class FrmCopyFile : XtraForm
{
    private readonly List<ClassFileManage.ModelFile> _modelFiles;
    private List<ClassFileManage.ModelFile> _modelFilesAddNew = new();
    private readonly Guid _memoryId;
    private readonly string _to;
    private readonly bool _fast;
    private readonly MediaDevice? _device = null;
    private readonly string _addressPhone;
    public bool _checkAndroid = false;
    public bool _endCopy = false;


    private readonly Guid _groupId = Guid.NewGuid();

    private ClsFont _clsFont = new(ClsFont.enumFont.samimBold, true);
    private static HttpClient _httpClient = new();


    private FundHttpService _fund = new(_httpClient);
    private PersonalHttpService _personal = new(_httpClient);
    private PersonalGroupHttpService _personalGroup = new(_httpClient);
    private MultimediaChapterDetailHttpService _chapterDetail = new(_httpClient);
    private CopiedFilmToMemoryHttpService _copiedFilmToMemory = new(_httpClient);
    private DefinitiveAccountHttpService _definitiveAccount = new(_httpClient);
    private HddMemoryPersonHttpService _hddMemoryPerson = new(_httpClient);
    private MultimediaChapterDetailHttpService _multiChapterDetail = new(_httpClient);
    private LoginHttpService _login = new(_httpClient);

    //public FrmCopyFile(List<ClassFileManage.ModelFile> modelFiles, Guid memoryId, string to, bool fast, MediaDevice? device)
    public FrmCopyFile(List<ClassFileManage.ModelFile> modelFiles, Guid memoryId, string to, bool fast)
    {
        InitializeComponent();
        this.Shown += FrmCopyFile_Shown;
        btnAddFile.Enabled = true;
        _modelFiles = modelFiles;
        _memoryId = memoryId;
        _to = to;
        _fast = fast;
    }

    public FrmCopyFile(List<ClassFileManage.ModelFile> modelFiles, MediaDevice? device, string addressPhone)
    {
        InitializeComponent();
        this.Shown += FrmCopyFile_Shown;
        btnAddFile.Enabled = false;
        _modelFiles = modelFiles;
        _device = device;
        _addressPhone = addressPhone;
        _checkAndroid = true;
    }
    Thread threadCopyAndroid;
    Guid getRegisterId;
    private async void FrmCopyFile_Shown(object? sender, EventArgs e)
    {


        getRegisterId = (await _login.GetAsync()).First(f => f.WinUser.ToLower() == Environment.UserName.ToLower()).PersonalId;
        //MessageBox.Show(getRegisterId.ToString());
        _clsFont.ChangeFont(tabPane1);
        tabNavigationPage2.PageEnabled = false;

        if (_memoryId == default)
            tabNavigationPage2.PageVisible = false;
        else
            tabNavigationPage2.PageVisible = true;

        //Thread.Sleep(150);

        if (_checkAndroid)
        {
            tabNavigationPage2.PageVisible = true;
        }



        if (_device is null)
        {
            await Copy1();
        }
        else
        {
            threadCopyAndroid = new Thread(async void () =>
            {
                try
                {
                    await CopyAndroid1();
                }
                catch (Exception e1)
                {
                    // ignored
                }
            });
            threadCopyAndroid.Start();
            // await CopyAndroid1();
        }
    }

    private async void FrmCopyFile_Load(object sender, System.EventArgs e)
    {
        //var getDetails =await _chapterDetail.GetAsync();
        layFund.btnCancel.Visible = layFund.btnCancel.Enabled = false;
    }

    public class ModelSellType
    {
        public int Id { get; set; }
        [Display(Name = "نوع")]
        public string Title { get; set; }

    }
    GridLookUpEdit cmbPerson;
    public async void SetFieldLayFund()
    {
        var txtId = ClsCollect.ModelTextEdit("Id", 100, "", false);
        var txtPriceSum = ClsCollect.ModelTextEditPrice("مبلغ دریافتی", 50, "مبلغ دریافتی", true, "ت");
        var txtPriceTotal = ClsCollect.ModelTextEditPrice("مبلغ کل", 50, "مبلغ کل", false, "ت");
        var getPerson = await _personal.GetAsync();
        var lstPerson = getPerson.Select(s => new { s.Id, مشتری = s.FirstName + " " + s.LastName }).ToList();

        cmbPerson = ClsCollect.ModelGridToDataLayout("مشتری", lstPerson, "Id", "مشتری", "مشتری را انتخاب کنبد", TextEditStyles.Standard);
        cmbPerson.HiddenColumn("Id");
        cmbPerson.Enabled = false;
        var getSellType = new List<ModelSellType>
        {
            new() { Id = 1, Title = "کارت خوان" },
            new() { Id = 2, Title = "نقدی" },
            new() { Id = 3, Title = "بدهی" }
        };

        var cmbSellType = ClsCollect.ModelGridToDataLayout("نوع عملیات", getSellType, "Id", "Title", "");
        cmbSellType.HiddenColumn("Id");
        cmbSellType.EditValueChanged += (s1, e1) =>
        {
            var getId = cmbSellType.EditValue.GetNum<int>(0);
            cmbPerson.Enabled = getId == 3;
        };
        layFund.SetFieldColumnDataLayout(true, 2, new List<ClsCollect.modelControlDataLayout>
        {
            new() { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never },
            new() { Grp = 1, Ctrl = txtPriceTotal, AllowNull = false, },
            new() { Grp = 1, Ctrl = txtPriceSum, AllowNull = false },

            new() { Grp = 1, Ctrl = cmbSellType, AllowNull = false },
            new() { Grp = 2, Ctrl = cmbPerson},
        }, 12, true);

        layFund.CallNew();
        this.WaitDownPage(async () =>
        {
            //Information Test E
            var getFund = await _fund.GetByGlobalId(_groupId);
            layFund.SetValue("مبلغ کل", getFund.PriceTotal);
            layFund.SetValue("مبلغ دریافتی", getFund.PriceTotal);
        });
    }

    private async void layFund_BtnSaveClick(object sender, EventArgs e)
    {
        layFund._disableAfterSave = true;
        lblFileCopied.Text = "0";

        var getPriceTotal = layFund.GetValue<decimal>("مبلغ کل").GetNum<long>();
        var getPriceRec = layFund.GetValue<decimal>("مبلغ دریافتی").GetNum<long>();
        var entity = new Fund
        {
            GlobalId = _groupId,
            SellType = layFund.GetValue<int>("نوع عملیات"),
            PriceTotal = getPriceTotal,
            PriceReceived = getPriceRec,
            TableName = "CopiedFilmToMemory",
            RegisterId = getRegisterId
        };

        if (entity.SellType == 3) // بدهی
        {
            var personId = cmbPerson.EditValue.ToGuidN(false, true);
            if (personId == default)
            {
                ClassMessageBox.ShowMSG("مشتری را انتخاب کنید", Class_Text.Msg_Name, ClassMessageBox.enumIcon.بستن_مربع);
                layFund._disableAfterSave = false;
                return;
                // layFund._disableAfterSave = false;
                // return;
            }
            else
            {
                DefinitiveAccount account = new DefinitiveAccount
                {
                    PersonalId = personId.Value,
                    Price = getPriceRec,
                    Debtor = true,
                    DateCustom = DateTime.Now,
                    Description = "فیلم",
                    DocNumber = 0,

                };
                await _definitiveAccount.AddAsync(account);

                #region Save HDD Person

                //var getPersonId = layFund.GetValue<Guid>("مشتری");
                await _hddMemoryPerson.AddAsync(new HddMemoryPerson
                {
                    HddMemoryId = _memoryId,
                    PersonalId = personId.Value,

                });

                #endregion
            }
        }

        else
        {
            long percentage = 100 - ((getPriceRec / getPriceTotal) * 100);
            if (percentage >= 25)
            {
                
            }
        }

        await _fund.AddByGroupId(entity);
        _finalClose = true;
        Application.Exit();
        //Application.ExitThread();
        Close();
    }
    public void SetFieldLayPerson()
    {
        var txtId = ClsCollect.ModelTextEdit("Id", 100, "", false);
        var txtFName = ClsCollect.ModelTextEdit("نام", 50, "نام", true);
        var txtLName = ClsCollect.ModelTextEdit("فامیلی", 50, "فامیلی", true);
        var txtMobile = ClsCollect.ModelTextEdit("موبایل", 11, "موبایل", true);
        layPerson.SetFieldColumnDataLayout(true, 1, new List<ClsCollect.modelControlDataLayout>
        {
            new() { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never },
            new() { Grp = 1, Ctrl = txtFName, AllowNull = false },
            new() { Grp = 1, Ctrl = txtLName, AllowNull = false },
            new() { Grp = 1, Ctrl = txtMobile, AllowNull = true },
        }, 12, true);
    }
    private async void layPerson_BtnSaveClick(object sender, EventArgs e)
    {
        await _personal.AddAsync(new Personal
        {
            FirstName = layPerson.GetValue<string>("نام"),
            LastName = layPerson.GetValue<string>("فامیلی"),
            Mobile = layPerson.GetValue<string>("موبایل"),
            PersonalGroupId = Guid.Parse("8168205e-2218-43bc-95aa-b53dde855bb4")
        });
        var getPerson = await _personal.GetAsync();
        var lstPerson = getPerson.Select(s => new { s.Id, مشتری = s.FirstName + " " + s.LastName }).ToList();
        cmbPerson.UpdateGridLookUpEdit(lstPerson);
        cmbPerson.RefreshEditValue();
        cmbPerson.Refresh();
    }

    private bool _isRunning = true;
    CancellationTokenSource cts = new();
    ulong sizeAll = 0;
    ulong sizeCopied = 0;
    int getOldCount;
    public async Task Copy1(bool plus = false, int countPlus = 0)
    {
        try
        {
            #region Start

            var dateTime = DateTime.Now;
            Task.Run(() =>
            {
                if (plus == false)
                    while (_isRunning)
                    {
                        var time = DateTime.Now;
                        Thread.Sleep(1000);
                        lblNow.Invoke(() =>
                        {
                            var timeSpan = time - dateTime;
                            lblNow.Text = timeSpan.Hours.ToString("D2") + @":" +
                                          timeSpan.Minutes.ToString("D2") + @":" +
                                          timeSpan.Seconds.ToString("D2");
                        });
                    }
            });

            #endregion

            lblTo.Text = _to;
            if (plus == false)
                lblStart.Text = dateTime.ToString("HH:mm:ss");

            lblFileAll.Text = (_modelFiles.Count + countPlus).ToString();
            prgCopy2.Position = 0;

            prgCopy1.Properties.Minimum = 0;
            prgCopy1.Properties.Maximum = _modelFiles.Count + countPlus;
            if (plus == false)
                prgCopy1.Position = 0;
            else
                prgCopy1.Position = countPlus;

            var cfm = new ClassFileManage();
            var progress = new Progress<double>(percent =>
            {
                prgCopy2.Position = (int)percent;
                // lblPercent.Text = $"{percent:F1}%";
            });

            #region Size
            if (plus == false)
                sizeAll = 0;


            //foreach (var file in _modelFiles)
            //{
            //    sizeAll += (ulong)new FileInfo(file.From).Length;
            //}



            #endregion

            foreach (var file in _modelFiles)
            {
                if (!lstRemain.Items.Contains(file.Name))
                {
                    lstRemain.Items.Add(file.Name);
                    sizeAll += (ulong)new FileInfo(file.From).Length;
                }
            }
            lblSizeAll.Text = sizeAll.FormatSize();
            //lstRemain.Items.AddRange(_modelFiles.Select(s => s.Name).ToArray<object>());


            foreach (var t in _modelFiles)
            {
                //await gg.CopyFileAsync(t.From, t.To, progress);

                await cfm.CopyFileAsyncUseCancel(t.From, t.To, progress, cts.Token);

                #region Add Film Fast

                // MessageBox.Show(_fast.ToString());
                if (_fast)
                {
                    //MessageBox.Show(t.ChapterHeaderId.ToString());
                    var getMaxCode = await _chapterDetail.GetMaxCode(t.ChapterHeaderId);
                    //MessageBox.Show(getMaxCode.ToString());
                    await _chapterDetail.AddAsync(new MultimediaChapterDetail
                    {
                        Code = getMaxCode,
                        HeaderChapterId = t.ChapterHeaderId,

                    });
                }

                #endregion
                // 123
                if (_memoryId != default)
                {
                    #region Copy To Memory

                    await _copiedFilmToMemory.AddAsync(new CopiedFilmToMemory()
                    {
                        GroupId = _groupId,
                        ChapterDetailId = t.ChapterHeaderId,
                        HddMemoryId = _memoryId,
                        Price = 0
                    });

                    #endregion

                    #region Save To Fund

                    await _fund.AddByGroupId(new Fund
                    {
                        GlobalId = _groupId,
                        TableName = "CopiedFilmToMemory",
                        RegisterId = getRegisterId
                        //PriceTotal = await _chapterDetail.GetPrice(t.DetailChapterId)
                    });

                    #endregion
                }

                lstRemain.Items.Remove(t.Name);
                lstCopied.Items.Add(t.Name);
                prgCopy1.Position += 1;
                TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                TaskbarManager.Instance.SetProgressValue((int)prgCopy1.Position, _modelFiles.Count + countPlus);
                lblFileCopied.Text = prgCopy1.Position.ToString();
                lblFileRemian.Text = (_modelFiles.Count - prgCopy1.Position + countPlus).ToString();

                sizeCopied += (ulong)new FileInfo(t.From).Length;

                lblSizeCopied.Text = sizeCopied.FormatSize();

                var remain = sizeAll - sizeCopied;
                lblSizeRemian.Text = remain.FormatSize();

                if (lblSizeAll.Text == lblSizeCopied.Text)
                {
                    btnAddFile.Invoke(() => btnAddFile.Enabled = false);
                    #region بررسی اضافه شدن فایل جدید

                    if (_modelFilesAddNew.Count > 0)
                    {
                       // btnAddFile.Invoke(() => btnAddFile.Enabled = false);

                        getOldCount = _modelFiles.Count;
                        _modelFiles.Clear();
                        foreach (var f in _modelFilesAddNew)
                        {
                            var getDire = Directory.GetParent(f.To);
                            Directory.CreateDirectory(getDire.FullName);
                            _modelFiles.Add(f);
                        }


                        _modelFilesAddNew.Clear();
                        await Copy1(true, getOldCount);
                        return;
                    }

                    #endregion

                    tabNavigationPage2.PageEnabled = true;
                    btnCancel.Enabled = false;
                   
                    lblEnd.Text = DateTime.Now.ToString("HH:mm:ss");
                    lblSizeRemian.Text = @"0";
                    _isRunning = false;
                    this.Top = 0;
                    this.TopLevel = true;
                    this.TopMost = true;
                    tablePanel1.BackColor = Color.GreenYellow;
                    SoundPlay();
                    _endCopy = true;
                }
            }
        }
        catch (Exception e)
        {
            if ((e.Message.Contains("opened file is no longer valid") || e.Message.Contains("Could not find a part of the path") || e.Message.Contains("A device which does not exist was spe")) && !Directory.Exists(_to))
            {
                var getStatus = ClassMessageBox.ShowMSGQues("فلش موری یافت نشد!" + " ," + "اتصال را بررسی کنید", Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار);
                if (getStatus)
                {

                    if (getOldCount > 0)
                    {
                        await Copy1(true, getOldCount);
                    }
                    else
                    {
                        await Copy1(true);
                    }

                    return;
                }
                else
                {
                    _endCopy = true;
                    return;
                }
            }
            _endCopy = true;

            MessageBox.Show(e.Message);
        }
    }
    SoundPlayer? funSound;
    public void SoundPlay()
    {
        funSound = ClassSound.Play1();
    }
    public async Task CopyAndroid1()
    {
        double getSe = 0;
        var getDrivers = _device?.GetDrives().ToList();
        #region Find HDD

        var getHddSelect = getDrivers.First(w => w.Name.Contains(_addressPhone)).RootDirectory;

        #endregion
        _device.ObjectUpdated += (s1, e1) =>
        {

        };
        if (getDrivers?.Count > 0)
        {
            ulong sizeCopied = 0;
            ulong sizeAll = 0;
            #region Start

            var dateTime = DateTime.Now;
            Task.Run(() =>
            {
                while (_isRunning)
                {
                    var time = DateTime.Now;
                    Thread.Sleep(1000);
                    lblNow.Invoke(() =>
                    {
                        var timeSpan = time - dateTime;
                        lblNow.Text = timeSpan.Hours.ToString("D2") + ":" + timeSpan.Minutes.ToString("D2") + ":" +
                                      timeSpan.Seconds.ToString("D2");
                    });
                }
            });

            #endregion

            await this.InvokeAsync(() =>
            {
                lblTo.Text = _to;
                lblStart.Text = dateTime.ToString("HH:mm:ss");

                lblFileAll.Text = _modelFiles.Count.ToString();
                prgCopy2.Position = 0;

                prgCopy1.Properties.Minimum = 0;
                prgCopy1.Properties.Maximum = _modelFiles.Count;
                prgCopy1.Position = 0;
                prgCopy1.ShowProgressInTaskBar = true;
                prgCopy2.Visible = false;

                // var cfm = new ClassFileManage();
                //var progress = new Progress<double>(percent =>
                //{
                //    prgCopy2.Position = (int)percent;
                //    // lblPercent.Text = $"{percent:F1}%";
                //});

                #region Size


                foreach (var file in _modelFiles)
                {
                    sizeAll += (ulong)new FileInfo(file.From).Length;
                }

                lblSizeAll.Text = sizeAll.FormatSize();

                #endregion
                lstRemain.Items.Clear();
                lstRemain.Items.AddRange(_modelFiles.Select(s => s.Name).ToArray<object>());
            });

            #region Copy

            // باید اصلاح شود برای انتقال به کارت حافظه
            //var rootDirectoryFullName = getDrivers[0].RootDirectory.FullName;

            getHddSelect.CreateSubdirectory("KavoshVideo");
            // getDrivers[0].RootDirectory.CreateSubdirectory("KavoshVideo");

            #region MyRegion

            //var getDataChapterDetailFull =
            //    await _multiChapterDetail.GetByHeaderTranslate(_modelFiles.Select(s => new ModelGuidFaEn
            //    {
            //        ChapterDetailId = s.ChapterHeaderId,
            //        //Type = "Fa"
            //    }).ToList());

            #endregion

            foreach (ClassFileManage.ModelFile file in _modelFiles)
            {
                file.To = file.To.Replace("ANDROIDDATA", _addressPhone + "\\" + "KavoshVideo");
                var gg = Path.GetDirectoryName(file.To);
                var getSplit = gg.Split("\\");
                string getCreateAddress = "KavoshVideo";
                for (int i = 3; i < getSplit.Length; i++)
                {
                    getCreateAddress += "\\" + getSplit[i];



                    //getDrivers[0].RootDirectory.CreateSubdirectory(getCreateAddress);
                    getHddSelect.CreateSubdirectory(getCreateAddress);
                }
            }
            try
            {
                foreach (var t in _modelFiles)
                {
                    if (_device.FileExists(t.To))
                    {
                        await this.InvokeAsync(() =>
                        {

                            prgCopy1.Position = prgCopy1.Position + 1;
                            Thread.Sleep(10);
                            prgCopy1.Refresh();
                            MessageBox.Show(@"FileExists");
                        });
                    }
                    else
                    {
                        _device.UploadFile(t.From, t.To);
                        await this.InvokeAsync(() =>
                        {
                            prgCopy1.Position = prgCopy1.Position + 1;
                            Thread.Sleep(10);
                        });

                        #region Copy To Memory

                        await _copiedFilmToMemory.AddAsync(new CopiedFilmToMemory()
                        {
                            GroupId = _groupId,
                            ChapterDetailId = t.ChapterHeaderId,
                            HddMemoryId = Guid.Parse("ee59cf5b-a0a1-4654-80e4-984d0633055a"), // فقط مخصوص اندروید
                            Price = 0,

                        });

                        #endregion

                        await this.InvokeAsync(() =>
                        {
                            lstRemain.Items.Remove(t.Name);
                            lstCopied.Items.Add(t.Name);
                            prgCopy1.Refresh();
                        });
                        #region Save To Fund

                        var price = await _chapterDetail.GetPrice(t.ChapterHeaderId);
                        // مشکل در قیمت 
                        await _fund.AddByGroupId(new Fund
                        {
                            GlobalId = _groupId,
                            PriceTotal = price,
                            TableName = "CopiedFilmToMemory"
                        });
                        #region Pulse

                        //await this.InvokeAsync(() =>
                        //{

                        //    Form df = new Form { };
                        //    df.Show();
                        //    Thread.Sleep(150);
                        //    df.Close();
                        //});


                        #endregion


                        // Description Delay Task Show 
                        //await Task.Delay(250);

                        //Clipboard.SetText(t.DetailChapterId.ToString());

                        //MessageBox.Show(t.DetailChapterId.ToString() + "  -  " + price.ToString());
                        #endregion


                    }
                    Thread.Sleep(10);
                    await this.InvokeAsync(() =>
                    {
                        lblFileCopied.Text = prgCopy1.Position.ToString();
                        var remain = sizeAll - sizeCopied;
                        lblSizeRemian.Text = remain.FormatSize();
                        sizeCopied += (ulong)new FileInfo(t.From).Length;
                        lblFileRemian.Text = (_modelFiles.Count - prgCopy1.Position).ToString();
                        lblSizeCopied.Text = sizeCopied.FormatSize();


                        TaskbarManager.Instance.SetProgressState(TaskbarProgressBarState.Normal);
                        TaskbarManager.Instance.SetProgressValue((int)prgCopy1.Position, _modelFiles.Count);
                        prgCopy1.Refresh();
                        Thread.Sleep(50);
                    });


                    //var getDetails1 = await _chapterDetail.GetAsync();
                    //var getDetails = await _chapterDetail.GetById(t.DetailChapterId);


                }
            }
            catch (Exception e)
            {
                _endCopy = true;

                MessageBox.Show(e.Message);
            }

            #region End Copy


            await this.InvokeAsync(() =>
            {
                if (lblSizeAll.Text == lblSizeCopied.Text)
                {
                    //lblFileCopied.Text = "0";
                    lblFileCopied.Text = lstCopied.Items.Count.ToString();
                    lblFileRemian.Text = lstRemain.Items.Count.ToString();
                    label5.Text = "فایل تکراری:";
                    tabNavigationPage2.PageEnabled = true;
                    btnCancel.Enabled = false;
                    lblEnd.Text = DateTime.Now.ToString("HH:mm:ss");
                    lblSizeRemian.Text = @"0";
                    _isRunning = false;
                    this.Top = 0;
                    this.TopLevel = true;
                    this.TopMost = true;
                    tablePanel1.BackColor = Color.GreenYellow;
                    SoundPlay();
                    _endCopy = true;
                }
            });

            #endregion


            #endregion

        }

    }

    private void label7_Click(object sender, EventArgs e)
    {

    }

    private void panel1_Paint(object sender, PaintEventArgs e)
    {

    }
    FrmAddVideo frmAddVideo;
    private async void btnAddFile_Click(object sender, EventArgs e)
    {
        if (_checkAndroid)
            frmAddVideo = new FrmAddVideo();
        else
            frmAddVideo = new FrmAddVideo(_to);



        frmAddVideo.ShowDialog();
        btnAddFile.Text = "افزودن فیلم (" + frmAddVideo._modelFiles.Count + ")";
        if (lstRemain.Items.Count == 0)
        {
            await Copy1(true, lstCopied.Items.Count);
        }
        else
        {
            _modelFilesAddNew = frmAddVideo._modelFiles;
        }


    }

    private void FrmCopyFile_FormClosed(object sender, FormClosedEventArgs e)
    {

    }

    private bool _finalClose = false;
    private void FrmCopyFile_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (!_finalClose)
        {
            if (!_endCopy)
            {
                e.Cancel = true;
            }

            if (tabNavigationPage2.PageVisible == true)
            {
                e.Cancel = true;
            }

            if (_endCopy && tabNavigationPage2.PageVisible == true)
            {
                e.Cancel = true;
                tabPane1.SelectedPage = tabNavigationPage2;
            }
        }
    }

    private void tabPane1_SelectedPageChanged(object sender, DevExpress.XtraBars.Navigation.SelectedPageChangedEventArgs e)
    {
        if (e.Page == tabNavigationPage2)
        {
            SetFieldLayPerson();
            SetFieldLayFund();

            layPerson.CallNew();
            //  layFund.CallNew();
        }
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {

        if (tablePanel1.BackColor == Color.GreenYellow && tabNavigationPage2.PageVisible == false)
        {
            _finalClose = true;
            Close();
        }
        else
        {
            var getQues = ClassMessageBox.ShowMSGQues("از انصراف مطمئن هستید ؟", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);

            if (getQues)
            {
                _endCopy = true;

                btnCancel.Enabled = false;
                cts.Cancel();
                lblEnd.Text = DateTime.Now.ToString("HH:mm:ss");
                _isRunning = false;
                this.Top = 0;
                this.TopLevel = true;
                this.TopMost = true;
                tablePanel1.BackColor = Color.FromArgb(245, 88, 88);
                SoundPlay();
                if (lblFileCopied.Text != @"0")
                {
                    if (tabNavigationPage2.PageVisible)
                    {
                        tabNavigationPage2.PageEnabled = true;
                        tabPane1.SelectedPage = tabNavigationPage2;
                    }
                }
                else
                {
                    _finalClose = true;
                    Application.Exit();
                }


            }
        }
    }

    private void btnMute_Click(object sender, EventArgs e)
    {

        funSound?.Stop();
    }
}