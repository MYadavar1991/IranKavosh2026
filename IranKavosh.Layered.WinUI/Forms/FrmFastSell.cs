using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraScheduler.Drawing;
using EF_Model.Table;
using IranKavosh.Layered.Model.Tables;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using MyCom.Class;
using MyCom.Object;
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
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraLayout;
using BuySellDetail = IranKavosh.Layered.Model.Tables.BuySellDetail;
using DefinitiveAccount = IranKavosh.Layered.Model.Tables.DefinitiveAccount;

namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class FrmFastSell : DevExpress.XtraEditors.XtraForm
    {
        ClsFont _font = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        ClsFont _fontNotBold = new ClsFont(ClsFont.enumFont.samimBold, false);
        private static HttpClient _httpClient = new HttpClient();
        private PersonalHttpService _personal = new PersonalHttpService(_httpClient);
        private FundHttpService _fund = new FundHttpService(_httpClient);
        private ProductHttpService _product = new ProductHttpService(_httpClient);
        private PriceListHttpService _priceList = new PriceListHttpService(_httpClient);
        private PriceListDiscountHttpService _priceListDiscoun = new PriceListDiscountHttpService(_httpClient);
        private FundTypeHttpService _fundType = new FundTypeHttpService(_httpClient);
        private CopiedFilmToMemoryHttpService _copiedFilmToMemory = new CopiedFilmToMemoryHttpService(_httpClient);
        private BuySellDetailHttpService _buySellDetail = new BuySellDetailHttpService(_httpClient);
        private MultimediaChapterDetailHttpService _multimediaChapterDetails = new MultimediaChapterDetailHttpService(_httpClient);
        private DefinitiveAccountHttpService _definitiveAccount = new DefinitiveAccountHttpService(_httpClient);
        public FrmFastSell()
        {
            InitializeComponent();
            this.Shown += FrmBuySell_Shown;
            this.Activated += (s1, e1) =>
            {
                this.WindowState = FormWindowState.Normal;
            };
        }

        private async void FrmBuySell_Shown(object? sender, EventArgs e)
        {
            //await GetFullPriceList(null);
            // await GetFullPriceList(_lstPaper);
            // return;
            //splitContainerControl1.DefaultSettingSplitter(false);
            splitContainerControl1.DefaultSettingSplitterNoCaption();
            await SetFiledLayHeader();
            await SetFiledGdvDetails();
            await SetFiledGdvPrint();
            await SetFonts();

        }

        private void FrmFastSell_Load(object sender, EventArgs e)
        {
        }
        public async Task SetFonts()
        {
            _font.ChangeFont(dgvDetails, 14);
            _font.ChangeFont(dgvPrint, 15);

            dgvPrint.SetStyle();
            dgvDetails.SetStyle();
        }


        public async Task SetFiledLayHeader()
        {
            pnlOperation.Controls.Add(layHeader.ShowPanelOperation());
            layHeader.AddButtonOperation();

            var pnlDate = ClsCollect.ModelDateTime("تاریخ", 10, "تاریخ",true);
            var getPerson = (await _personal.GetAsync()).Select(s => new { s.Id, s.FullName }).ToList();
            var cmbPerson = ClsCollect.ModelGridToDataLayout("مشتری", getPerson, "Id", "FullName", "مشتری را انتخاب کنید");
            cmbPerson.Enabled = false;
            cmbPerson.HiddenColumn("Id");



            var txtDebtor = ClsCollect.ModelTextEditPrice("مانده حساب", 10, "");
            txtDebtor.ReadOnly = true;

            var txtPriceKol = ClsCollect.ModelTextEditPrice("قابل پرداخت", 10, "", false);
            var txtPriceReci = ClsCollect.ModelTextEditPrice("پرداخت شده", 10, "", true);
            var rdoSellType = ClsCollect.ModelRadioGroup("نوع پرداخت", new List<ClsCollect.modelRadioGroup>
            {
                new ClsCollect.modelRadioGroup{Column = 1,Field = "کارت"},
                new ClsCollect.modelRadioGroup{Column = 2,Field = "نقدی"},
                new ClsCollect.modelRadioGroup{Column = 3,Field = "بدهی"},
            }, true, BorderStyles.Flat, 13F);

            rdoSellType.SelectedIndexChanged += (s1, e1) =>
            {
                var getRdoText = rdoSellType.Text;
                if (getRdoText == "2")
                {
                    cmbPerson.Enabled = true;
                    cmbPerson.EditValue = default;
                    txtDebtor.Text = "0";
                }
                else
                {
                    cmbPerson.Enabled = false;
                    cmbPerson.EditValue = Guid.Parse("1c0b0f57-84ef-4712-a3ec-b39fbbf82d57");
                }

                //1c0b0f57-84ef-4712-a3ec-b39fbbf82d57
            };

            layHeader.SetFieldColumnDataLayout(true, 2, [
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = pnlDate, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = cmbPerson, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtDebtor},
                new ClsCollect.modelControlDataLayout { Grp = 2, Ctrl = txtPriceKol},
                new ClsCollect.modelControlDataLayout { Grp = 2, Ctrl = txtPriceReci, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 2, Ctrl = rdoSellType, AllowNull = false,SizeType = SizeConstraintsType.Custom,AutoHeight = 38}
            ], 13);
            layHeader.CallNew();
            layHeader._btnCancel.Visible = false;

            cmbPerson.EditValueChanged += async (s1, e1) =>
            {
                if (cmbPerson.EditValue != null)
                {
                    Guid getID = cmbPerson.EditValue.ToGuid(false);
                    var getDebtor = (await _definitiveAccount.GetByPersonCalc(getID));
                    txtDebtor.Text = getDebtor.ToString();
                }
            };
            layHeader.BtnSaveClick += async (s1, e1) =>
            {
                #region Save Details

                var getGroupId = Guid.NewGuid();

                #region Header

                var getData = layHeader.GetValue<string>("تاریخ").ShamsiToMiladi();
                var getPersonId = layHeader.GetValue<Guid>("مشتری");
                var getPrice1 = layHeader.GetValue<string>("قابل پرداخت").GetNum<long>();
                var getPrice2 = layHeader.GetValue<string>("پرداخت شده").GetNum<long>();
                var getSellType = layHeader.GetValue<string>("نوع پرداخت");
                //var getPersonId=

                var getTableName = "BuySellDetail";

                await _fund.AddAsync(new Fund
                {
                    Count = 0, // بعد محاسبه بشه ...
                    GlobalId = getGroupId,
                    PriceTotal = getPrice1,
                    PriceReceived = getPrice2,
                    SellType = 1,
                    TableName = getTableName
                });
                if (getSellType == "بدهی")
                {
                   await _definitiveAccount.AddAsync(new DefinitiveAccount
                   {
                       PersonalId = getPersonId,
                       Description = getGroupId.ToString(),
                       Price = getPrice2,
                       Debtor = true
                   });
                }
                #endregion


                #region Print

                var buySellDetail = new List<BuySellDetail>();

                foreach (DataRow i in _dtPrint.Rows)
                {
                    var count = i["تعداد"].GetNum<long>();
                    if (count > 0)
                    {
                        var sellDetail = new BuySellDetail
                        {
                            GroupId = getGroupId,
                            Sell = true,
                            PriceListId = i["خدمات"].ToGuid(false),
                            PriceSell = i["مبلغ کل"].GetNum<long>(),
                            PriceFinal = i["قابل پرداخت"].GetNum<long>(),
                            Count = count,
                        };
                        buySellDetail.Add(sellDetail);
                        await _buySellDetail.AddAsync(sellDetail);
                    }

                }

                #endregion


                #region Details

                foreach (DataRow i in _dtDetails.Rows)
                {
                    var count = i["تعداد"].GetNum<long>();
                    if (count > 0)
                    {
                        var sellDetail = new BuySellDetail
                        {
                            GroupId = getGroupId,
                            Sell = true,
                            PriceListId = i["خدمات"].ToGuid(false),
                            PriceSell = 0,
                            PriceFinal = i["قابل پرداخت"].GetNum<long>(),
                            Count = count,
                        };
                        buySellDetail.Add(sellDetail);
                        await _buySellDetail.AddAsync(sellDetail);
                    }
                }
                _dtDetails.Rows.Clear();
                #endregion




                #endregion


                new Thread(() =>
                {
                    layHeader.Invoke(async () =>
                    {
                        layHeader.CallNew();
                        layHeader._btnCancel.Visible = false;
                        dgvPrint.GetViewBase.BeginDataUpdate();
                        _dtPrint.Rows.Clear();
                       
                        foreach (var i in getFullPriceList.OrderBy(o => o.FullTitle))
                        {
                            _dtPrint.Rows.Add("", "", i.PriceListId, i.Price);
                        }
                        dgvPrint.GetViewBase.EndDataUpdate();
                    });

                }).Start();

            };

            pnlDate.Text = DateTime.Now.DateTimePersian().Date;
        }

        List<Guid> _lstPaper = new List<Guid>
        {
            Guid.Parse("7aa9ed24-684d-4402-89b1-ad1ec660aa77"),
            Guid.Parse("dd172801-fec2-4f9a-a2da-b2a2ebae6396"),
            Guid.Parse("cd763adc-c438-4c3e-aa6c-d1ae387254d5"),
        };
        IEnumerable<ModelFullPriceList> getFullPriceList;
        private DataTable _dtPrint;
        public async Task SetFiledGdvPrint()
        {
            await dgvPrint.InvokeThread(async () =>
            {
                dgvPrint.ActiveScrollGrid();

                getFullPriceList = await _priceList.GetFullPriceList(_lstPaper);
                if (dgvPrint.ColumnCount() == 0)
                {
                    _dtPrint = dgvPrint.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    new KavoshGrid.modelColumn{Name = "ردیف"},
                    new KavoshGrid.modelColumn{Name = "حذف",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.delete4},

                    new KavoshGrid.modelColumn{Name = "خدمات",Type = typeof(Guid)},
                    new KavoshGrid.modelColumn{Name = "مبلغ واحد",Type = typeof(long),PriceActive = true},
                    new KavoshGrid.modelColumn{Name = "تعداد",Type = typeof(long),PriceActive = true},
                    new KavoshGrid.modelColumn{Name = "مبلغ کل",Type = typeof(long),PriceActive = true},
                    new KavoshGrid.modelColumn{Name = "تخفیف",Type = typeof(float)},
                    new KavoshGrid.modelColumn{Name = "قابل پرداخت",Type = typeof(long),PriceActive = true},

                }, true, false, true);


                    dgvPrint.AllowEditColumn("خدمات");
                    dgvPrint.AllowEditColumn("مبلغ واحد");
                    dgvPrint.AllowEditColumn("تخفیف");
                    dgvPrint.AllowEditColumn("مبلغ کل");
                    dgvPrint.AllowEditColumn("قابل پرداخت");
                    dgvPrint.AddSummaryItem("تعداد", "تعداد", "{0}", SummaryItemType.Sum);
                    dgvPrint.AddSummaryItem("مبلغ کل", "مبلغ کل", "{0}", SummaryItemType.Custom);
                    dgvPrint.AddSummaryItem("قابل پرداخت", "قابل پرداخت", "{0}", SummaryItemType.Custom);

                    dgvPrint.MaxMinWidth("حذف", 35, 35);
                    dgvPrint.MaxMinWidth("ردیف", 35, 35);
                    dgvPrint.MaxMinWidth("خدمات", 100, 100);
                    dgvPrint.MaxMinWidth("مبلغ کل", 150, 150);
                    dgvPrint.MaxMinWidth("قابل پرداخت", 150, 150);

                    #region Relation


                    dgvPrint.AddGridToGrid(getFullPriceList.ToList().Select(s => new { s.PriceListId, s.FullTitle }).ToList(), "خدمات", "PriceListId", "FullTitle");

                    #endregion

                    #region Event

                    dgvPrint.GetViewBase.CellValueChanged += (s1, e1) =>
                    {
                        var getClm = e1.Column.FieldName;
                        if (getClm != "مبلغ کل" && getClm != "تخفیف" && getClm != "قابل پرداخت")
                        {
                            var getPriceListId = dgvPrint.GetValue<Guid>(e1.RowHandle, "خدمات");
                            var getUnitPrice = dgvPrint.GetValue<long>(e1.RowHandle, "مبلغ واحد");
                            var getCount = dgvPrint.GetValue<int>(e1.RowHandle, "تعداد");

                            var getDiscount = getFullPriceList.First(f => f.PriceListId == getPriceListId).Count_Discount.OrderBy(o => o.Count).ToList();

                            float getRollBackDis = 0;
                            if (getDiscount.Any(a => a.Count == getCount))
                            {
                                getRollBackDis = getDiscount.First(a => a.Count == getCount).Discount;
                            }
                            else
                            {
                                for (int i = 0; i < getDiscount.Count - 1; i++)
                                {
                                    if (getCount > getDiscount[i].Count && getCount < getDiscount[i + 1].Count)
                                    {
                                        getRollBackDis = getDiscount[i].Discount;
                                    }
                                }
                            }

                            if (getCount > getDiscount[^1].Count)
                            {
                                getRollBackDis = getDiscount[^1].Discount;
                            }

                            float getDisCalc = getRollBackDis; //12%

                            var unitPrice = getUnitPrice * getCount;
                            dgvPrint.SetValue(e1.RowHandle, "مبلغ کل", unitPrice);
                            dgvPrint.SetValue(e1.RowHandle, "تخفیف", getDisCalc.ToString("N2"));

                            var getUnitNewPercent = getUnitPrice - ((getUnitPrice * getDisCalc) / 100);
                            var getPriceFinal = getUnitNewPercent * getCount;
                            dgvPrint.SetValue(e1.RowHandle, "قابل پرداخت", getPriceFinal);
                            
                        }
                    };
                    dgvPrint.GetViewBase.RowCellStyle += (s1, e1) =>
                    {
                        var getClm = e1.Column.FieldName;
                        if (getClm == "تعداد")
                        {
                            e1.Appearance.BackColor = Color.FromArgb(210, 218, 18);
                        }
                    };
                    dgvPrint.GetViewBase.RowStyle += (s1, e1) =>
                    {
                        var getValue = dgvPrint.GetText(e1.RowHandle, "خدمات").ToString();
                        if (getValue != null && getValue.Contains("A4"))
                        {
                            e1.Appearance.BackColor = Color.FromArgb(240, 173, 173);
                        }
                    };
                    double sumK = 0;
                    dgvPrint.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
                    {
                        dgvPrint.AutoSummaryCalculate(e1, "قابل پرداخت", "مبلغ", ref sumKol, "تومان");
                        layHeader.SetValue("قابل پرداخت", sumKol + sumKolDetails);
                        layHeader.SetValue("پرداخت شده", sumKol + sumKolDetails);
                        dgvPrint.AutoSummaryCalculate(e1, "مبلغ کل", "مبلغ", ref sumK, "تومان");
                    };

                    dgvPrint.AddEventRowCellClick<Guid>(obj =>
                    {
                        dgvPrint.SetValue("تعداد", 0);
                        dgvPrint.GetViewBase.UpdateSummary();
                    }, "خدمات", "حذف");

                    #endregion
                }

                #region Load Data

                _dtPrint.Rows.Clear();
                foreach (var i in getFullPriceList.OrderBy(o => o.FullTitle))
                {
                    _dtPrint.Rows.Add("", "", i.PriceListId, i.Price, 0);
                }

                #endregion
            }, true);
        }
       
        double sumKol = 0, sumKolDetails = 0;
        private DataTable _dtDetails;
        public async Task SetFiledGdvDetails()
        {
            var getFullPriceListDetails = (await _priceList.GetFullPriceList()).Where(w => _lstPaper.All(a => a != w.ProductId));
            await dgvDetails.InvokeThread(async () =>
            {
                if (dgvDetails.ColumnCount() == 0)
                {
                    dgvDetails.ActiveScrollGrid();
                    _dtDetails = dgvDetails.GridStructure([
                        new KavoshGrid.modelColumn { Name = "ردیف" },
                        new KavoshGrid.modelColumn
                        {
                            Name = "حذف", Object = KavoshGrid.enumObject.Button,
                            ImageValue = MyCom.Properties.Resources.delete4
                        },
                        new KavoshGrid.modelColumn { Name = "خدمات", Type = typeof(Guid) },
                        //new KavoshGrid.modelColumn { Name = "مبلغ واحد", Type = typeof(Guid), PriceActive = true },
                        new KavoshGrid.modelColumn { Name = "UnitPrice", Type = typeof(Guid), PriceActive = true },
                        new KavoshGrid.modelColumn { Name = "مبلغ واحد", Type = typeof(long), PriceActive = true },
                        new KavoshGrid.modelColumn { Name = "تعداد", Type = typeof(long), PriceActive = true },
                        new KavoshGrid.modelColumn { Name = "قابل پرداخت", Type = typeof(long), PriceActive = true }

                    ], true, true, true);

                    dgvDetails.AllowEditColumn("مبلغ واحد");
                    dgvDetails.AllowEditColumn("قابل پرداخت");

                    dgvDetails.AddSummaryItem("تعداد", "تعداد", "{0}", SummaryItemType.Sum);
                    dgvDetails.AddSummaryItem("قابل پرداخت", "قابل پرداخت", "{0}", SummaryItemType.Custom);

                    dgvDetails.MaxMinWidth("حذف", 35, 35);
                    dgvDetails.MaxMinWidth("ردیف", 35, 35);
                    dgvDetails.MaxMinWidth("خدمات", 200, 200);
                    dgvDetails.MaxMinWidth("خدمات", 200, 200);
                    dgvDetails.MaxMinWidth("تعداد", 50, 50);
                    dgvDetails.MaxMinWidth("خدمات", 305, 305);
                    dgvDetails.MaxMinWidth("قابل پرداخت", 170, 170);
                    dgvDetails.HiddenColumn("UnitPrice");

                    dgvDetails.AddAllowNewRowAndType(DefaultBoolean.True, NewItemRowPosition.Top);

                    #region Relation

                    var getPriceList = getFullPriceListDetails.Select(s => new
                    {
                        s.PriceListId,
                        s.FullTitle,
                        Price = s.Price.ToString("N0")
                    }).ToList();
                    var get = dgvDetails.AddGridToGrid(getPriceList, "خدمات", "PriceListId", "FullTitle");
                    //var get2 = dgvDetails.AddGridToGrid(getPriceList, "مبلغ واحد", "Price", "Price");
                    var get2 = dgvDetails.AddGridToGrid(getPriceList, "UnitPrice", "PriceListId", "Price");
                    //get2.HiddenColumn("Id");
                    get.HiddenColumn("PriceListId");
                    get.HiddenColumn("Price");
                    #endregion


                    #region Event
                    var editor = ClsCollect.ModelTextEditPrice("مبلغ", 10, "");
                    dgvDetails.z_RowCellDoubleClick += (s1, e1) =>
                    {
                        var getValue = dgvDetails.GetText("مبلغ واحد");

                        XtraInputBoxArgs args = new XtraInputBoxArgs();
                        args.Showing += (sender, e) =>
                        {
                            e.MessageBoxForm.Height = 150;
                            // تغییر فونت عنوان پنجره (Caption)
                            e.MessageBoxForm.Font = new Font("Samim FD", 11F);
                            e.Buttons[DialogResult.OK].Text = "&تایید";
                            e.Buttons[DialogResult.Cancel].Text = "انصراف";
                            e.MessageBoxForm.Appearance.Font= new Font("Samim FD", 11F);
                            e.MessageBoxForm.Text = Class_Text.Msg_Name;
                            editor.Text = getValue.ToString();
                        };
                        args.Caption = Class_Text.Msg_Name;      // عنوان پنجره
                        args.Prompt = "لطفاً مبلغ را انتخاب کنید"; // متن بالای ویرایشگر
                        args.DefaultButtonIndex = 0;               // 0 = OK دکمه پیش‌فرض
                   
                        // استفاده از ویرایشگر تاریخ (DateEdit) به جای TextEdit پیش‌فرض
                      
                    
                        //editor.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
                        //editor.Properties.Mask.EditMask = "yyyy/MM/dd";
                        args.Editor = editor;
                        var result = XtraInputBox.Show(args);
                        dgvDetails.SetValue("مبلغ واحد", result.GetNum<long>());

                        var getCount = dgvDetails.GetValue<int>( "تعداد");
                        dgvDetails.SetValue("قابل پرداخت", getCount * result.GetNum<long>());
                        layHeader.SetValue("قابل پرداخت", sumKol + sumKolDetails);
                        layHeader.SetValue("پرداخت شده", sumKol + sumKolDetails);

                    };

                    dgvDetails.GetViewBase.CellValueChanged += (s1, e1) =>
                    {
                        var getClm = e1.Column.FieldName;
                        if (getClm == "خدمات")
                        {
                            dgvDetails.SetValue(e1.RowHandle, "UnitPrice", e1.Value);
                            var getPrice = dgvDetails.GetText("UnitPrice").GetNum<long>();
                            dgvDetails.SetValue(e1.RowHandle, "مبلغ واحد", getPrice);

                            var getUnitPrice = getPriceList.First(f => f.PriceListId == e1.Value.ToGuid(false)).Price.GetNum<long>();
                            var getCount = dgvDetails.GetValue<int>(e1.RowHandle, "تعداد");

                            dgvDetails.SetValue(e1.RowHandle, "قابل پرداخت", getCount * getUnitPrice);

                        }

                        #region Calc


                        if (getClm == "تعداد")
                        {
                            var getUnitPrice = dgvDetails.GetText(e1.RowHandle, "مبلغ واحد").GetNum<long>();
                            var getCount = dgvDetails.GetValue<int>(e1.RowHandle, "تعداد");

                            dgvDetails.SetValue(e1.RowHandle, "قابل پرداخت", getCount * getUnitPrice);

                            //var getDiscount = getPriceList.First(f => f.PriceListId == getPriceListId).Count_Discount.OrderBy(o => o.Count).ToList();
                        }

                        #endregion
                    };

                    dgvDetails.GetViewBase.CustomSummaryCalculate += (s1, e1) =>
                    {
                        dgvDetails.AutoSummaryCalculate(e1, "قابل پرداخت", "مبلغ", ref sumKolDetails, "تومان");
                        layHeader.SetValue("قابل پرداخت", sumKol + sumKolDetails);
                        layHeader.SetValue("پرداخت شده", sumKol + sumKolDetails);
                    };

                    dgvDetails.AddEventRowCellClick<Guid>(obj =>
                    {
                        dgvDetails.DeleteRow(false);
                        dgvDetails.GetViewBase.UpdateSummary();
                    }, "خدمات", "حذف");

                    #endregion
                }
            }, true);
        }
    }
}