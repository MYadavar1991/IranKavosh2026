using DevExpress.Data;
using DevExpress.Utils.Layout;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraLayout.Utils;
using IranKavosh.Layered.Service.Services;
using MyCom.Class;
using MyCom.Object;
using System.Data;
using System.Net.Http;
using IranKavosh.Layered.Model.Tables;

namespace IranKavosh.Layered.WinUI.Forms
{

    public partial class FrmDefiniteAccounts : DevExpress.XtraEditors.XtraForm
    {
        private static readonly HttpClient _httpClient = new();
        private readonly PersonalHttpService _personal = new(_httpClient);
        private readonly DefinitiveAccountHttpService _definitiveAccount = new(_httpClient);
        //private readonly MultiGenreHttpService _multiGenre = new(_httpClient);
        //private readonly MultiCountryHttpService _multiCountry = new(_httpClient);
        //private readonly MultiPriceHttpService _multiPrice = new(_httpClient);
        //private readonly MultimediaHeaderHttpService _multiHeader = new(_httpClient);

        private ClsFont _clsFontBold = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private ClsFont _clsFont = new ClsFont(ClsFont.enumFont.samimBoldFD, false);
        private Guid _personId;
        public FrmDefiniteAccounts(Guid id = default)
        {
            InitializeComponent();

            this.Shown += FrmDefiniteAccounts_Shown;
            _personId = id;
        }

        private async void FrmDefiniteAccounts_Shown(object? sender, EventArgs e)
        {
            await FormLoad();
        }

        private void frmDefiniteAccounts_Load(object sender, EventArgs e)
        {

        }
        public void SetFont()
        {
            _clsFontBold.ChangeFont(dgvDefiniteAccount, 14);
            _clsFontBold.ChangeFont(btnDelEndDoc, 14);
            dgvDefiniteAccount.SetStyle();
        }

        public async Task FormLoad()
        {
            this.WaitDownPage(() =>
            {
                SetFont();
                SetFieldLayDefiniteAccount();
                SetFieldDgvDefiniteAccount();
                LoadFieldDgvDefiniteAccount();
            });


        }
        TextEdit _txtPrice, _txtStatus;
        public GridLookUpEdit cmbPerson;
        public async void SetFieldLayDefiniteAccount()
        {
            // _kgm = new KGM();
            var txtId = ClsCollect.ModelTextEdit("Id", 50, "", false);
            var getPerson = (await _personal.GetAsync()).Select(s => new { s.Id, FullName = s.FirstName + " " + s.LastName + " - " + s.Code }).ToList();

            cmbPerson = ClsCollect.ModelGridToDataLayout("کاربر", getPerson, "Id", "FullName", "انتخاب کاربر");
            cmbPerson.HiddenColumn("Id");

            //_txtPrice = ClsCollect.ModelTextEditNumber("مبلغ", 20, "مبلغ را وارد نمایید");
            _txtPrice = ClsCollect.ModelTextEditPrice("مبلغ", 20, "مبلغ را وارد نمایید");
            var cmbTypePrice = ClsCollect.ModelGridToDataLayout("نوع عملیات",
                new List<ClsCollect.modelDataTable>
                {
                    new ClsCollect.modelDataTable {Code = 1,Title = "دریافتی"},
                    new ClsCollect.modelDataTable {Code = 2,Title = "نسیه"},
                }, "Code", "Title", "");

            _txtStatus = ClsCollect.ModelTextEdit("وضعیت", 30, "");
            _txtStatus.ReadOnly = true;
            var getDes = ClsCollect.ModelLayoutMemoEdit("توضیحات", 300, "توضیحات");

            layDefiniteAccount.SetFieldColumnDataLayout(true, 2, new List<ClsCollect.modelControlDataLayout>
            {
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = cmbPerson, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = _txtPrice, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = cmbTypePrice, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = _txtStatus, AllowNull = true, },
                new ClsCollect.modelControlDataLayout { Grp = 2, Ctrl = getDes },
            }, 14, true);

            layDefiniteAccount.CallNew();
            if (_personId != default)
            {
                cmbPerson.Enabled = false;
                layDefiniteAccount.SetValueType("Id", _personId.ToString());
                layDefiniteAccount.SetValueType("کاربر", _personId);
            }
            else
            {
                layDefiniteAccount.SetValueType("نوع عملیات", 2);
            }

            #region Event

            cmbPerson.EditValueChanged += (s, e) =>
            {
                if (cmbPerson.EditValue != null)
                {
                    _personId = Guid.Parse(cmbPerson.EditValue.ToString());
                    LoadFieldDgvDefiniteAccount();
                }
            };
            _txtPrice.Focus();
            _txtPrice.Select();
            #endregion

        }

        private Guid _tempSaveIdForDeleteNow = default;
        private void layDefiniteAccount_BtnSaveClick(object sender, EventArgs e)
        {

            var getId = Guid.Parse(cmbPerson.EditValue.ToString());
            // var getDescription = layDefiniteAccount.GetValue<string>("توضیحات");
            var getDescription = layDefiniteAccount.GetValue<string>("توضیحات", true, "");
            // _personId = getId;
            var getType = layDefiniteAccount.GetValue<int>("نوع عملیات") == 2 ? -1 : 1;
            DefinitiveAccount definiteAccount = new DefinitiveAccount
            {
                PersonalId = getId,
                Price = Math.Abs((long)layDefiniteAccount.GetValue<decimal>("مبلغ")) * getType,
                Description = getDescription
            };

            _definitiveAccount.AddAsync(definiteAccount);
            _tempSaveIdForDeleteNow = definiteAccount.Id;
            btnDelEndDoc.Enabled = true;
            layDefiniteAccount.SetValueType("مبلغ", 0);
            layDefiniteAccount.SetValueType("نوع عملیات", "");

            LoadFieldDgvDefiniteAccount();

            new Task(async void () =>
            {
                await Task.Delay(1000);
                await layDefiniteAccount.InvokeAsync(new Action(async () =>
                {
                    layDefiniteAccount.CallNew();
                    await Task.Delay(1000);
                    cmbPerson.EditValue = getId;
                    //  layDefiniteAccount.SetValueType("Id", getId.ToString());
                    layDefiniteAccount.SetValueType("کاربر", _personId);
                }));
            }).Start();


        }
        DataTable _dtDefiniteAccount = new();
        public void SetFieldDgvDefiniteAccount()
        {
            _dtDefiniteAccount = dgvDefiniteAccount.GridStructure(new List<KavoshGrid.modelColumn>
            {
                new KavoshGrid.modelColumn {Name = "Id",Type = typeof(Guid)},
                new KavoshGrid.modelColumn {Name = "ردیف"},
                new KavoshGrid.modelColumn {Name = "کاربر"},
                new KavoshGrid.modelColumn {Name = "تاریخ"},
                new KavoshGrid.modelColumn {Name = "توضیحات"},
                new KavoshGrid.modelColumn {Name = "بدهکار", Type = typeof(long), PriceActive = true},
                new KavoshGrid.modelColumn {Name = "بستانکار", Type = typeof(long), PriceActive = true},
                new KavoshGrid.modelColumn {Name = "مانده", Type = typeof(long), PriceActive = true},
                new KavoshGrid.modelColumn {Name = "وضعیت"},

            }, false, false, true);
            int min = 125;
            dgvDefiniteAccount.MaxMinWidth("بدهکار", 120, 120);
            dgvDefiniteAccount.MaxMinWidth("بستانکار", 120, 120);
            dgvDefiniteAccount.MaxMinWidth("مانده", 145, 145);

            dgvDefiniteAccount.AddSummaryItem("بدهکار", "بدهکار", "{0}", SummaryItemType.Custom);
            dgvDefiniteAccount.AddSummaryItem("بستانکار", "بستانکار", "{0}", SummaryItemType.Custom);
            dgvDefiniteAccount.AddSummaryItem("مانده", "مانده", "{0}", SummaryItemType.Custom);
            dgvDefiniteAccount.AddSummaryItem("وضعیت", "وضعیت", "{0}", SummaryItemType.Custom);

            //  dgvDefiniteAccount.FormatStringNegativeNumber("مبلغ");
            dgvDefiniteAccount.HiddenColumn("Id");
            dgvDefiniteAccount.ActiveScrollGrid();


            #region Event

            dgvDefiniteAccount.z_RowCellDoubleClick += (s1, e1) =>
            {
                //var getId = dgvDefiniteAccount.GetValue<Guid>("Id");
                //var getCheckPS = SrvPSPaymentStatus.GetAll().Any(a => a.DefiniteAccountId == getId && a.CalcPsOldId != null);
                //if (getCheckPS)
                //{
                //    var psPaymentStatus = SrvPSPaymentStatus.GetAll().First(a => a.DefiniteAccountId == getId);
                //    var getPSId = psPaymentStatus.CalcPsOldId;
                //    FrmConsols frmConsols = new FrmConsols(false);
                //    XtraForm frmSample = new XtraForm { Width = 400, Height = 650, StartPosition = FormStartPosition.CenterScreen,FormBorderStyle = FormBorderStyle.FixedSingle,MinimizeBox=false,MaximizeBox = false};

                //    frmSample.Load += (s2, e2) =>
                //    {
                //        DataTable dt = null;
                //        KavoshGrid kavoshGrid = new KavoshGrid { Dock = DockStyle.Fill, RightToLeft = RightToLeft.Yes };

                //        var bandedGridView2 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
                //        bandedGridView2.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { new GridBand() });
                //        bandedGridView2.GridControl = kavoshGrid;

                //        kavoshGrid.MainView = bandedGridView2;
                //        kavoshGrid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { bandedGridView2 });


                //        TablePanel tblPanel= new TablePanel{Dock = DockStyle.Fill};
                //        tblPanel.Columns.Add(TablePanelEntityStyle.Relative, 100,true);
                //        tblPanel.Rows.AddRange(new TablePanelRow[]
                //        {
                //            new TablePanelRow { Style = TablePanelEntityStyle.Relative,Height = 100},
                //            new TablePanelRow { Style = TablePanelEntityStyle.Absolute,Height = 40 },
                //            new TablePanelRow { Style = TablePanelEntityStyle.Absolute,Height = 40 },
                //            new TablePanelRow { Style = TablePanelEntityStyle.Absolute,Height = 40 },
                //            new TablePanelRow { Style = TablePanelEntityStyle.Absolute,Height = 40 },
                //        });


                //        frmSample.Controls.Add(tblPanel);
                //        tblPanel.Controls.Add(kavoshGrid);

                //        Label lblPriceGame = new Label{Dock = DockStyle.Fill,TextAlign = ContentAlignment.MiddleRight,AutoSize = false};
                //        Label lblPriceSpecialServices = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, AutoSize = false };
                //        Label lblPriceAll = new Label { Dock = DockStyle.Fill, TextAlign = ContentAlignment.MiddleRight, AutoSize = false };

                //        tblPanel.Controls.Add(lblPriceGame);
                //        tblPanel.Controls.Add(lblPriceSpecialServices);
                //        tblPanel.Controls.Add(lblPriceAll);

                //        tblPanel.SetCell(lblPriceGame, 1, 0);
                //        tblPanel.SetCell(lblPriceSpecialServices, 2, 0);
                //        tblPanel.SetCell(lblPriceAll, 3, 0);

                //        tblPanel.SetCell(kavoshGrid,0,0);

                //        kavoshGrid.SetStyle();
                //        _clsFontBold.ChangeFont(kavoshGrid,12);
                //        _clsFontBold.ChangeFont(lblPriceGame, 14);
                //        _clsFontBold.ChangeFont(lblPriceSpecialServices, 14);
                //        _clsFontBold.ChangeFont(lblPriceAll, 14);

                //        var getCalcOld = SrvCalcOld.GetAll().First(w => w.Id == getPSId.Value);
                //       var Buffet=  SrvBuffet.GetAll().Where(w => w.CalcPsOldId == getPSId.Value);

                //        lblPriceSpecialServices.Text = @"خدمات ویژه: " + getCalcOld.SpecialServicePrice.ToString("N0");
                //        var getSumBuffet = (long)Buffet.Sum(s => s.PriceCommodity * s.Count);
                //        lblPriceGame.Text = @"مبلغ بازی: " + (psPaymentStatus.PriceRound - getCalcOld.SpecialServicePrice - getSumBuffet).ToString("N0");
                //        lblPriceAll.Text = @"مبلغ کل: " + psPaymentStatus.PriceRound.ToString("N0");

                //        frmConsols.SetFieldGridDetailsBuffet(kavoshGrid, ref dt, getPSId.Value);
                //        kavoshGrid.ShowFooter();
                //        kavoshGrid.AddSummaryItem("جمع", "جمع", "{0:n0}", SummaryItemType.Sum);

                //    };
                //    frmSample.ShowDialog();
                //}
            };

            #endregion


            #region Event

            double sumDebtor = 0, sumCreditor = 0, sumRemain = 0;
            dgvDefiniteAccount.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
            {
                //e1.AutoSummaryCalculate("بدهکار", "جمع", ref sumDebtor);
                //e1.AutoSummaryCalculate("بستانکار", "جمع", ref sumCreditor);
                var ConE = e1.ConvertItemSummary();

                switch (e1.SummaryProcess)
                {

                    case CustomSummaryProcess.Start:
                        sumDebtor = 0.0;
                        sumCreditor = 0.0;
                      
                        break;
                    case CustomSummaryProcess.Calculate:
                        if (e1.FieldValue != null)
                        {
                            if (ConE.FieldName.Equals("بدهکار"))
                            {
                                sumDebtor += Convert.ToDouble(e1.FieldValue.ToString());
                              
                            }
                            else if (ConE.FieldName.Equals("بستانکار"))
                            {
                                sumCreditor += Convert.ToDouble(e1.FieldValue.ToString());
                             
                            }
                            //else if (ConE.FieldName.Equals("مانده"))
                            //{

                            //    sumRemain -= sumDebtor;
                            //    sumRemain += sumCreditor;
                            //}
                        }
                        break;
                    case CustomSummaryProcess.Finalize:

                       
                        if (ConE.FieldName.Equals("بدهکار"))
                        {
                            e1.TotalValue = "جمع" + ": " + sumDebtor.ToString("N0") + " ";
                            sumRemain -= sumDebtor;
                        }

                        if (ConE.FieldName.Equals("بستانکار"))
                        {
                            e1.TotalValue = "جمع" + ": " + sumCreditor.ToString("N0") + " ";
                            sumRemain += sumCreditor;
                        }

                        if (ConE.FieldName.Equals("مانده"))
                        {
                            e1.TotalValue = "جمع" + ": " + Math.Abs(sumRemain).ToString("N0") + " تومان";
                            
                        }
                        if (ConE.FieldName.Equals("وضعیت"))
                        {
                            switch (sumRemain)
                            {
                                case 0:
                                    {
                                        e1.TotalValue = "تسویه";
                                        _txtStatus.Properties.AppearanceReadOnly.ForeColor = Color.FromArgb(166, 158, 52);
                                        _txtStatus.Text ="(تسویه)";
                                    }
                                    break;
                                case < 0:
                                    {
                                        e1.TotalValue = "بدهکار";
                                        _txtStatus.Properties.AppearanceReadOnly.ForeColor = Color.FromArgb(237, 18, 18);
                                        _txtStatus.Text = Math.Abs(sumRemain).ToString("N0") + " تومان" + " (بدهکار)";
                                    }
                                    break;
                                case > 0:
                                    {
                                        e1.TotalValue = "بستانکار";
                                        _txtStatus.Properties.AppearanceReadOnly.ForeColor = Color.Green;
                                        _txtStatus.Text = Math.Abs(sumRemain).ToString("N0") + " تومان"+ " (بستانکار)";
                                    }
                                    break;

                                   
                            }
                            sumRemain = 0;

                            //
                        }

                        break;

                }
            };
            dgvDefiniteAccount.GetViewBase.CustomDrawFooterCell += async (s, e) =>
                {
                    try
                    {
                        if (cmbPerson is { EditValue: not null })
                        {
                            var value = cmbPerson.EditValue.ToString();
                            if (value != "")
                            {
                                var getId = Guid.Parse(value);
                                var sumTotal = (await _definitiveAccount.GetAsync()).Where(w => w.PersonalId == getId)
                                    .Sum(su => su.Price);
                                if (e.Column.FieldName == "مانده" || e.Column.FieldName == "وضعیت")
                                {
                                    if (sumTotal < 0)
                                    {
                                        e.Appearance.ForeColor = Color.Red;
                                    }
                                    else
                                    {
                                        e.Appearance.ForeColor = Color.DarkGreen;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception exception)
                    {
                        
                    }
                }
                ;
            //    dgvDefiniteAccount.GetViewBase.CustomDrawFooterCell += async (s, e) =>
            //    {
            //        // var get = e.RowHandle;
            //        // Thread.Sleep(1000);
            //        try
            //        {
            //            if (cmbPerson != null && cmbPerson.EditValue != null)
            //            {
            //                var value = cmbPerson.EditValue.ToString();
            //                // string value = layDefiniteAccount.GetValue<string>("Id", true, "");
            //                if (value != "")
            //                {
            //                    var getId = Guid.Parse(value);
            //                    if (getId == Guid.Empty)
            //                        return;

            //                    var sumTotal = (await _definitiveAccount.GetAsync()).Where(w => w.PersonalId == getId).Sum(su => su.Price);

            //                    if (e.Column.FieldName == "مانده")
            //                    {
            //                        // var total = sumTotal < 0 ? "بدهکار" : "بستانکار";
            //                        e.Info.DisplayText = Math.Abs(sumTotal).ToString(ClsCollect.FormatStringNegativeNumber(0));
            //                        if (sumTotal < 0)
            //                        {
            //                            e.Appearance.ForeColor = Color.Red;
            //                        }
            //                        else
            //                        {
            //                            e.Appearance.ForeColor = Color.DarkGreen;
            //                        }
            //                    }
            //                    else if (e.Column.FieldName == "وضعیت")
            //                    {
            //                        string total;
            //                        if (sumTotal < 0)
            //                        {
            //                            total = "بدهکار";
            //                            e.Appearance.ForeColor = Color.Red;
            //                        }
            //                        else
            //                        {
            //                            total = "بستانکار";
            //                            e.Appearance.ForeColor = Color.DarkGreen;
            //                        }
            //                        //Description TODO: Before Action Event  Relation For Color  E Appearance 

            //                      //  e.Info.DisplayText = total;
            //                    }
            //                }
            //                //dgvDefiniteAccount.GetViewBase.RefreshData();
            //            }
            //        }
            //        catch (Exception exception)
            //        {

            //        }

            //    };

            #endregion
        }
        public async void LoadFieldDgvDefiniteAccount()
        {
            var getPersons = (await _personal.GetAsync()).ToList();
            var getData = (await _definitiveAccount.GetAsync()).Where(w => !w.IsDelete && w.PersonalId == _personId).OrderBy(od => od.DateTime).ToList();
            _dtDefiniteAccount.Rows.Clear();
            //   foreach (DefiniteAccount acc in getData)
            for (int i = 0; i < getData.Count; i++)
            {
                var s = getData[i];
                var getPerson = getPersons.First(f => f.Id == s.PersonalId);
                _dtDefiniteAccount.Rows.Add(
                    s.Id,
                    "",
                    getPerson.FirstName + " " + getPerson.LastName + "  -  " + getPerson.Code,
                    s.DateTime.DateTimePersian().ShortTimeDate,
                    s.Description,
                    s.Price < 0 ? Math.Abs(s.Price) : 0,
                    s.Price > 0 ? Math.Abs(s.Price) : 0,
                    0,
                    "---");

                if (_dtDefiniteAccount.Rows.Count == 1)
                {
                    _dtDefiniteAccount.Rows[0]["مانده"] = Math.Abs(s.Price);
                    _dtDefiniteAccount.Rows[0]["وضعیت"] = s.Price > 0 ? "بستانکار" : "بدهکار";
                }
                else
                {
                    var getOldStatus = _dtDefiniteAccount.Rows[i - 1]["وضعیت"].ToString();

                    if (getOldStatus.Equals("بدهکار"))
                    {
                        long price = Convert.ToInt64(_dtDefiniteAccount.Rows[i - 1]["مانده"].ToString()) -
                                      s.Price;
                        // (s.Price > 0 ? s.Price : -1 * s.Price);
                        _dtDefiniteAccount.Rows[i]["مانده"] = Math.Abs(price);

                        #region Status

                        if (price < 0)
                            _dtDefiniteAccount.Rows[i]["وضعیت"] = "بستانکار";
                        else if (price > 0)
                            _dtDefiniteAccount.Rows[i]["وضعیت"] = "بدهکار";
                        else
                            _dtDefiniteAccount.Rows[i]["وضعیت"] = "تسویه";

                        #endregion
                    }
                    else if (getOldStatus.Equals("بستانکار"))
                    {
                        // long price = (!(s.Price > 0) ? s.Price : -1 * s.Price) 
                        long price = (s.Price * -1) - Convert.ToInt64(_dtDefiniteAccount.Rows[i - 1]["مانده"].ToString());
                        _dtDefiniteAccount.Rows[i]["مانده"] = Math.Abs(price);

                        #region Status

                        if (price < 0)
                            _dtDefiniteAccount.Rows[i]["وضعیت"] = "بستانکار";
                        else if (price > 0)
                            _dtDefiniteAccount.Rows[i]["وضعیت"] = "بدهکار";
                        else
                            _dtDefiniteAccount.Rows[i]["وضعیت"] = "تسویه";

                        #endregion
                    }
                    else if (getOldStatus.Equals("تسویه"))
                    {
                        _dtDefiniteAccount.Rows[i]["وضعیت"] = s.Price > 0 ? "بستانکار" : "بدهکار";
                        _dtDefiniteAccount.Rows[i]["مانده"] = Math.Abs(s.Price);
                    }
                }

            }

            //dgvDefiniteAccount.GetViewBase.Focus();
            //dgvDefiniteAccount.GetViewBase.UpdateSummary();
            //dgvDefiniteAccount.GetViewBase.UpdateTotalSummary();
            dgvDefiniteAccount.SetFieldSizeColumn();
        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //  cmbPerson.Enabled = true;
            // cmbPerson.EditValue = Guid.Parse("6dbf9980-7f58-4205-93ff-042b0131ae3b");

        }

        private void layDefiniteAccount_Load(object sender, EventArgs e)
        {

        }

        private void btnDelEndDoc_Click(object sender, EventArgs e)
        {
            //if (_tempSaveIdForDeleteNow != default)
            //{
            //    ClsCollect.Delete(true, () =>
            //    {
            //        SrvDefiniteAccount.Remove(_tempSaveIdForDeleteNow.ToString());
            //        LoadFieldDgvDefiniteAccount();
            //        btnDelEndDoc.Enabled = false;
            //    });

            //}
        }
    }
}