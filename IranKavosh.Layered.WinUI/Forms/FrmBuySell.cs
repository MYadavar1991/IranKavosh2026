using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using ExCSS;
using IranKavosh.Layered.Service.Services;
using MyCom.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.Data;
using DevExpress.XtraLayout.Utils;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.Middleware;
using MyCom.Object;
using Color = System.Drawing.Color;
using ShareHolderHeader = EF_Model.Table.ShareHolderHeader;

namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class FrmBuySell : DevExpress.XtraEditors.XtraForm
    {
        ClsFont _font = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private static HttpClient _httpClient = new();
        private PersonalHttpService _personal = new(_httpClient);
        private FundHttpService _fund = new(_httpClient);
        private FundTypeHttpService _fundType = new(_httpClient);
        private CopiedFilmToMemoryHttpService _copiedFilmToMemory = new(_httpClient);
        private BuySellDetailHttpService _BuySellDetail = new(_httpClient);
        private MultimediaChapterDetailHttpService _multimediaChapterDetails = new(_httpClient);
        private PriceListHttpService _priceList = new(_httpClient);
        private ShareHolderHeaderHttpService _shareHolderHeader = new(_httpClient);
        private ShareHolderDetailHttpService _shareHolderDetail = new(_httpClient);
        private DefinitiveAccountHttpService _definitiveAccount = new(_httpClient);
        public FrmBuySell()
        {
            InitializeComponent();
            this.Shown += FrmBuySell_Shown;
        }

        private async void FrmBuySell_Shown(object? sender, EventArgs e)
        {
            await SetFiledLayHeader();
            await SetFiledDgvHeader(DateTime.Now);
            await SetFonts();
        }

        private async void FrmBuySell_Load(object sender, EventArgs e)
        {
            pnlDateOpen1.txtShowCalen.Text = DateTime.Now.MiladiToShamsi("Date");
            pnlDateOpen1.z_btnShowCalen += async (s1, e1) =>
            {
                await SetFiledDgvHeader(pnlDateOpen1.txtShowCalen.Text.ShamsiToMiladi());
            };
        }

        public async Task SetFonts()
        {
            _font.ChangeFont(dgvHeader);
            _font.ChangeFont(dgvDetails);
            _font.ChangeFont(btnCalc);
            //dgvDetails.SettingGridView();
            dgvHeader.SetStyle();
            dgvDetails.SetStyle();
        }
        public async Task SetFiledLayHeader()
        {
            var txtId = ClsCollect.ModelTextEdit("Id", 50, "System", false);
            var txtGlobalId = ClsCollect.ModelTextEdit("GlobalId", 50, "System", false);
            var pnlDate = ClsCollect.ModelDateTime("تاریخ", 10, "", true);
            var cmbType = ClsCollect.ModelRadioGroup("ف|خ", new List<ClsCollect.modelRadioGroup>
            {
                new ClsCollect.modelRadioGroup{Column = 1,Field = "فروش"},
                new ClsCollect.modelRadioGroup{Column = 2,Field = "خرید"},
            }, fontSize: 13);

            //var modelFundTypeRadioGroups = new List<ClsCollect.modelRadioGroup>
            //{
            //    new ClsCollect.modelRadioGroup{Column = 1,Field = "خدمات اینترنتی",},
            //    new ClsCollect.modelRadioGroup{Column = 1,Field = "خدمات چاپ",},
            //    new ClsCollect.modelRadioGroup{Column = 2,Field = "فروش فیلم",},
            //    new ClsCollect.modelRadioGroup{Column = 2,Field = "لوازم جانبی",},
            //};

            //var cmbFundType = ClsCollect.ModelRadioGroup("عملیات", modelFundTypeRadioGroups, fontSize: 13);

            var getPerson = (await _personal.GetAsync()).Select(s => new { Id = s.Id, s.FullName }).ToList();
            var getCmbPerson = ClsCollect.ModelGridToDataLayout("مشتری", getPerson, "Id", "FullName", "انتخاب مشتری");
            getCmbPerson.HiddenColumn("Id");

            var txtPriceTotal = ClsCollect.ModelTextEditPrice("مبلغ کل", 12, "جمع کل", titleLeft: "تومان");
            var txtPriceReci = ClsCollect.ModelTextEditPrice("پرداخت شده", 12, "جمع کل", titleLeft: "تومان");
            var txtTableName = ClsCollect.ModelTextEdit("TableName", 30, "");


            layHeader.SetFieldColumnDataLayout(true, 1, new List<ClsCollect.modelControlDataLayout>
            {
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtId },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtGlobalId },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = pnlDate ,AllowNull = false},
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = cmbType ,AllowNull = false,SizeType = SizeConstraintsType.Custom, AutoHeight = 33},
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = getCmbPerson },

                //new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = cmbFundType ,AllowNull = false,SizeType = SizeConstraintsType.Custom, AutoHeight = 60},
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtPriceTotal ,AllowNull = false},
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtPriceReci ,AllowNull = false},
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtTableName ,Visibility = LayoutVisibility.Never},

            }, 13);
            pnlOperation.Controls.Add(layHeader.ShowPanelOperation());
            layHeader.AddButtonOperation();
            layHeader.BtnSaveClick += LayHeader_BtnSaveClick;
        }

        private async void LayHeader_BtnSaveClick(object? sender, EventArgs e)
        {
            Fund fund = new Fund();
            fund.Id = layHeader.GetValue<Guid>("Id", true, Guid.NewGuid());
            fund.GlobalId = layHeader.GetValue<Guid>("GlobalId", true);
            fund.DateTime = layHeader.GetValue<string>("تاریخ").ShamsiToMiladi();
            fund.SellType = layHeader.GetValue<string>("ف|خ") == "خرید" ? 1 : 2;

            fund.PriceReceived = layHeader.GetValue<string>("پرداخت شده").GetNum<long>();
            fund.PriceTotal = layHeader.GetValue<string>("مبلغ کل").GetNum<long>();
            fund.TableName = layHeader.GetValue<string>("TableName");
            //var getFundType = layHeader.GetValue<string>("عملیات");

            //var fundTypeId = (await _fundType.GetAsync()).First(f => f.Title == getFundType).Id;
            //fund.FundTypeId = fundTypeId;

            //layHeader.SetValueType("عملیات", getFundType.First(f => f.Id == getData.FundTypeId).Title);
            //layHeader.SetValueType("مبلغ کل", getData.PriceTotal);
            //layHeader.SetValueType("پرداخت شده", getData.PriceReceived);

            await _fund.AddAsync(fund);
            await SetFiledDgvHeader(pnlDateOpen1.txtShowCalen.Text.ShamsiToMiladi());
        }

        private DataTable _dtHeader;
        public async Task SetFiledDgvHeader(DateTime dateTime = default)
        {
            if (dgvHeader.ColumnCount() == 0)
            {
                _dtHeader = dgvHeader.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new KavoshGrid.modelColumn{Name = "ویرایش",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.dgvEdit},
                    new KavoshGrid.modelColumn{Name = "حذف",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.delete3},
                    new KavoshGrid.modelColumn{Name = "جزئیات",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.view},
                    new KavoshGrid.modelColumn{Name = "ردیف"},
                    new KavoshGrid.modelColumn{Name = "Id", Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "کد", Type = typeof(long)},
                    new KavoshGrid.modelColumn{Name = "تاریخ", Type = typeof(string)},
                    new KavoshGrid.modelColumn{Name = "ف|خ", Type = typeof(string)},
                    //new KavoshGrid.modelColumn{Name = "عملیات", Type = typeof(string)}, // جدید
                    new KavoshGrid.modelColumn{Name = "مبلغ کل", Type = typeof(long),PriceActive = true},
                    new KavoshGrid.modelColumn{Name = "پرداخت شده", Type = typeof(long),PriceActive = true},
                    new KavoshGrid.modelColumn{Name = "کاربر", Type = typeof(Guid)},
                    //new KavoshGrid.modelColumn{Name = "مشتری", Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "GlobalId", Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "TableName", Type = typeof(string)},

                }, false, true, true);

                dgvHeader.ActiveScrollGrid();
                dgvHeader.HiddenColumn(["Id"]);
                dgvHeader.HiddenColumn(["GlobalId"]);
                dgvHeader.HiddenColumn(["TableName"]);

                dgvHeader.MaxMinWidth("حذف", 35, 35);
                dgvHeader.MaxMinWidth("ویرایش", 35, 35);
                dgvHeader.MaxMinWidth("جزئیات", 35, 35);
                dgvHeader.MaxMinWidth("مبلغ کل", 200, 200);
                dgvHeader.MaxMinWidth("پرداخت شده", 200, 200);
                dgvHeader.AddSummaryItem("مبلغ کل", "مبلغ کل", "{0}", SummaryItemType.Custom);
                dgvHeader.AddSummaryItem("پرداخت شده", "پرداخت شده", "{0}", SummaryItemType.Custom);

                #region Relation

                var getPerson = (await _personal.GetAsync()).Select(s => new { s.Id, s.FullName }).ToList();
                var cmbPerson = dgvHeader.AddGridToGrid(getPerson, "کاربر", "Id", "FullName");

                //var getFundType = (await _fundType.GetAsync()).Select(s => new { s.Id, s.Title }).ToList();
                //var getCmbFundType = dgvHeader.AddGridToGrid(getFundType, "عملیات", "Id", "Title");

                #endregion



                #region Event

                double calc = 0;
                dgvHeader.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
                {
                    dgvHeader.AutoSummaryCalculate(e1, "مبلغ کل", "جمع کل", ref calc, "تومان");
                    dgvHeader.AutoSummaryCalculate(e1, "پرداخت شده", "جمع کل", ref calc, "تومان");
                };
                dgvHeader.AddEventRowCellClick<Guid>(async globalId =>
                {
                    var getTableName = dgvHeader.GetValue<string>("TableName");
                    dgvDetails.GetViewBase.Columns.Clear();
                    if (getTableName == "CopiedFilmToMemory")
                    {
                        await SetFiledDgvDetailsFilm(globalId);
                    }
                    else if (getTableName == "BuySellDetail")
                    {
                        await SetFiledDgvDetailsOther(globalId);
                    }


                }, "GlobalId", "جزئیات");
                dgvHeader.AddEventRowCellClick<Guid>(id =>
                {
                    dgvHeader.DeleteRow(true, async () =>
                    {
                        await _fund.DeleteAsync(id);
                    }, true);
                }, "Id", "حذف");
                dgvHeader.AddEventRowCellClick<Guid>(async id =>
                {
                    var getData = await _fund.GetById(id);
                    layHeader.CallNew();
                    layHeader.SetValueType("Id", getData.Id);
                    layHeader.SetValueType("GlobalId", getData.GlobalId);
                    layHeader.SetValueType("تاریخ", getData.DateTime.DateTimePersian().Date);
                    layHeader.SetValueType("ف|خ", getData.SellType == 1 ? "فروش" : "خرید");

                    //layHeader.SetValueType("عملیات", getFundType.First(f => f.Id == getData.FundTypeId).Title);

                    layHeader.SetValueType("مبلغ کل", getData.PriceTotal);
                    layHeader.SetValueType("پرداخت شده", getData.PriceReceived);
                    layHeader.SetValueType("TableNAme", getData.TableName);

                }, "Id", "ویرایش");

                #endregion
            }

            #region Load Data

            _dtHeader.Rows.Clear();
            //var getData = await _fund.GetAsync();
            IEnumerable<Fund> getData;
            if (dateTime == default)
                getData = await _fund.GetAsync();
            else
                getData = await _fund.GetByDate(dateTime);


            foreach (Fund i in getData.OrderByDescending(o => o.Code))
            {
                _dtHeader.Rows.Add("", "", "", "",
                    i.Id,
                    i.Code,
                    i.DateTime.DateTimePersian().ShortDateTime,
                    "فروش",
                    //i.FundTypeId,
                    i.PriceTotal,
                    i.PriceReceived,
                    i.RegisterId,

                    i.GlobalId,
                    i.TableName);
            }
            dgvHeader.SetFieldSizeColumn();
            #endregion
        }


        private DataTable _dtDetails;


        /// <summary>
        /// بررسی تطابق GLOBALID
        /// فقط مخصوص بارگذاری جزئیات فیلم های کپی شده برای مشتری
        /// بررسی دیگر فعالیت ها پیاده سازی مجدد تابع GetDetails
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public async Task SetFiledDgvDetailsFilm(Guid groupId)
        {
            if (dgvDetails.ColumnCount() == 0)
            {
                _dtDetails = dgvDetails.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new KavoshGrid.modelColumn{Name = "Id",Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "فیلم",Type = typeof(string)},
                    new KavoshGrid.modelColumn{Name = "مبلغ",Type = typeof(long),PriceActive = true},
                }, true, true, true);
                dgvDetails.ActiveScrollGrid();
                dgvDetails.HiddenColumn("Id");
                dgvDetails.MaxMinWidth("فیلم", 250, 250);
                dgvDetails.MaxMinWidth("مبلغ", 150, 150);

                dgvDetails.AddSummaryItem("فیلم", "فیلم", "{0} عدد", SummaryItemType.Count);
                dgvDetails.AddSummaryItem("مبلغ", "مبلغ", "", SummaryItemType.Custom);

                #region Relation



                #endregion

                #region Event

                double cntFilm = 0F;
                dgvDetails.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
                {
                    //var getItem = e1.ConvertItemSummary();

                    e1.AutoSummaryCalculate("مبلغ", "جمع", ref cntFilm, "تومان");

                };

                #endregion
            }

            _dtDetails.Rows.Clear();
            #region Load Data

            var getDetails = (await _copiedFilmToMemory.GetAsync()).Where(w => w.GroupId == groupId);

            var modelGuidFaEns = new List<ModelGuidFaEn>();

            foreach (CopiedFilmToMemory d in getDetails)
            {
                modelGuidFaEns.Add(new ModelGuidFaEn { ChapterDetailId = d.ChapterDetailId });
            }

            var getInfoFullVideo = (await _multimediaChapterDetails.GetByHeaderTranslate(modelGuidFaEns));

            foreach (CopiedFilmToMemory i in getDetails)
            {
                _dtDetails.Rows.Add(i.Id, getInfoFullVideo.First(f => f.ChapterDetailsId == i.ChapterDetailId).HeaderName, i.Price);
            }

            #endregion
        }


        public async Task SetFiledDgvDetailsOther(Guid groupId)
        {

            if (dgvDetails.ColumnCount() == 0)
            {
                _dtDetails = dgvDetails.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new KavoshGrid.modelColumn{Name = "Id",Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "شرح",Type = typeof(string)},
                    new KavoshGrid.modelColumn{Name = "تعداد",Type = typeof(long),PriceActive = true},
                    new KavoshGrid.modelColumn{Name = "مبلغ",Type = typeof(long),PriceActive = true},
                }, true, true, true);
                dgvDetails.ActiveScrollGrid();
                dgvDetails.HiddenColumn("Id");
                dgvDetails.MaxMinWidth("شرح", 350, 350);
                dgvDetails.MaxMinWidth("تعداد", 60, 60);
                dgvDetails.MaxMinWidth("مبلغ", 150, 150);

                dgvDetails.AddSummaryItem("تعداد", "تعداد", "{0} عدد", SummaryItemType.Count);
                dgvDetails.AddSummaryItem("مبلغ", "مبلغ", "", SummaryItemType.Custom);

                #region Relation



                #endregion

                #region Event

                double cnt = 0F;
                dgvDetails.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
                {
                    //var getItem = e1.ConvertItemSummary();

                    e1.AutoSummaryCalculate("مبلغ", "جمع", ref cnt, "تومان");
                };

                #endregion
            }

            _dtDetails.Rows.Clear();

            #region Load Data

            //var getDetails = (await _BuySellDetail.GetAsync()).Where(w => w.GroupId == groupId).AsEnumerable();
            var getDetails = (await _BuySellDetail.GetByGroupId(groupId));
            var getGetFullPriceList = (await _priceList.GetFullPriceList()).AsEnumerable();
            foreach (var i in getDetails)
            {
                _dtDetails.Rows.Add(i.Id,
                    getGetFullPriceList.First(f => f.PriceListId == i.PriceListId).FullTitle,
                    i.Count,
                    i.PriceFinal);
            }

            #endregion
        }

        private async void btnNext_Click(object? sender, EventArgs e)
        {
            pnlDateOpen1.txtShowCalen.Text = pnlDateOpen1.txtShowCalen.Text.ShamsiToMiladi().AddDays(1).MiladiToShamsi("Date");
            await SetFiledDgvHeader(pnlDateOpen1.txtShowCalen.Text.ShamsiToMiladi());
        }

        private async void btnBack_Click(object sender, EventArgs e)
        {
            pnlDateOpen1.txtShowCalen.Text = pnlDateOpen1.txtShowCalen.Text.ShamsiToMiladi().AddDays(-1).MiladiToShamsi("Date");
            await SetFiledDgvHeader(pnlDateOpen1.txtShowCalen.Text.ShamsiToMiladi());
        }
        private async void btnShowAll_Click(object sender, EventArgs e)
        {
            await SetFiledDgvHeader();
        }

        List<ShareHolderDetail> getDataShareHolderDetail;
        private async void btnCalc_Click(object sender, EventArgs e)
        {
            var txtDate = pnlDateOpen1.txtShowCalen.Text.ShamsiToMiladi();

            #region Calc

            var getFund = (await _fund.GetAsync()).Where(w => w.DateTime.Date <= txtDate && !w.Calculation);
            var getFundCalc = getFund.Sum(s => s.PriceReceived);

            #endregion

            XtraForm frmShowHolder = new XtraForm { Width = 450, Height = 240, StartPosition = FormStartPosition.CenterScreen };

            getDataShareHolderDetail = (await _shareHolderDetail.GetAsync()).ToList();
            // KavoshGrid dgvSaham = new KavoshGrid{RightToLeft = RightToLeft.Yes,Dock = DockStyle.Fill};


            Panel pnlBtn = new Panel { Dock = DockStyle.Top, Height = 35, BackColor = Color.Aqua };
            KavoshLayout kavoshLayout = new KavoshLayout { Dock = DockStyle.Fill, RightToLeft = RightToLeft.Yes };
            kavoshLayout.AddButtonOperation();
            pnlBtn.Controls.Add(kavoshLayout.ShowPanelOperation());
            DataTable dtSaham = null;
            frmShowHolder.Shown += async (s1, e1) =>
            {
                await frmShowHolder.Invoke(async () =>
                {
                    dgvSaham.Enabled = true;
                    dgvSaham.Name = "لیست محاسبه";
                    frmShowHolder.Controls.Add(pnlBtn);
                    frmShowHolder.Controls.Add(kavoshLayout);
                   // _font.ChangeFont(dgvSaham);
                    dgvSaham.SetStyle();
                    kavoshLayout.BringToFront();


                    #region Event
                    kavoshLayout.BtnNewClick +=  (s2, e2) =>
                    {
                        dgvSaham.Enabled = true;
                    };
                    kavoshLayout.BtnSaveClick += async (s2, e2) =>
                    {
                        var getQues = ClassMessageBox.ShowMSGQues("مبلغ کل: " + getFundCalc.ToString("N0") + "  تومان", Class_Text.Msg_Name, ClassMessageBox.enumIcon.سوال);
                        if (getQues)
                        {
                           
                            foreach (DataRow row in dtSaham.Rows)
                            {
                                Guid getPId = row["کاربر"].ToGuid();
                                float percent = row["سهم"].GetNum<float>();
                                long getCalc = (long)((getFundCalc * percent) / 100);

                                await _definitiveAccount.AddAsync(new DefinitiveAccount
                                {
                                    PersonalId = getPId,
                                    Price = getCalc,
                                    Description = "تسویه صندوق تا: "+ pnlDateOpen1.txtShowCalen.Text,
                                    Debtor = false,
                                    DateCustom = txtDate,
                                });
                            }
                            await _fund.UpdateRangeDateAsync(txtDate);

                        }
                    };

                    #endregion

                    #region Data Grid

                    dtSaham = dgvSaham.GridStructure([
                        new KavoshGrid.modelColumn { Name = "Id", Type = typeof(Guid) },
                        new KavoshGrid.modelColumn { Name = "کاربر", Type = typeof(Guid) },
                        new KavoshGrid.modelColumn { Name = "سهم", Type = typeof(float) },
                        new KavoshGrid.modelColumn { Name = "محاسبه", Type = typeof(long), PriceActive = true },
                    ], false, false, false);

                    dgvSaham.HiddenColumn("Id");

                    #region Relation

                    var getPerson = (await _personal.GetAsync()).Select(s => new { s.Id, s.FullName }).ToList();
                    dgvSaham.AddGridToGrid(getPerson, "کاربر", "Id", "FullName");

                    #endregion

                    await Task.Delay(500);
                    kavoshLayout.CallNew();
                    dgvSaham.Enabled = true;
                 

                    #endregion
                });
            };
            //await _fund.UpdateRangeDateAsync(Convert.ToDateTime("2026/05/15"));
            #region Field

            var getDataShareHolderHeader = (await _shareHolderHeader.GetAsync()).Select(s => new { s.Id, s.Code, s.Title }).ToList();
            var cmbShareHolderHeader = ClsCollect.ModelGridToDataLayout("لیست سهام داران", getDataShareHolderHeader, "Id", "Title", "");
            cmbShareHolderHeader.HiddenColumn("Id");
            cmbShareHolderHeader.EditValueChanged += (s1, e1) =>
            {
                var getId = cmbShareHolderHeader.EditValue.ToGuid();
                var getDataDetails = getDataShareHolderDetail.Where(w => w.ShareHolderHeaderId == getId)
                .Select(s => new { s.PersonalId, s.Stocks });
                dtSaham.Rows.Clear();

                foreach (var i in getDataDetails)
                {
                    dtSaham.Rows.Add(i.PersonalId, i.PersonalId, i.Stocks);
                }
            };
            kavoshLayout.SetFieldColumnDataLayout(true, 1,
            [
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = cmbShareHolderHeader, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = dgvSaham, AllowNull = true },
            ], 13);

            #endregion
            frmShowHolder.ShowDialog();
        }
    }
}