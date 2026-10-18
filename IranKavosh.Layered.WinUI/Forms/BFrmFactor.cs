using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.Drawing;
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
using DevExpress.Utils;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraReports.UI;
using IranKavosh.Layered.Model.Tables.BOT;
using IranKavosh.Layered.WinUI.Report;
using MyCom.Object;

namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class BFrmFactor : DevExpress.XtraEditors.XtraForm
    {
        ClsFont _clsFont = new(ClsFont.enumFont.samimBoldFD, true);
        private static HttpClient _httpClient = new HttpClient();

        private PersonalHttpService _personal = new(_httpClient);
        private ProductHttpService _product = new(_httpClient);
        private BFactorDetailHttpService _factorDetail = new(_httpClient);
        private BFactorHeaderHttpService _factorHeader = new(_httpClient);


        //private FundHttpService _fund = new FundHttpService(_httpClient);
        //private FundTypeHttpService _fundType = new FundTypeHttpService(_httpClient);
        //private CopiedFilmToMemoryHttpService _copiedFilmToMemory = new CopiedFilmToMemoryHttpService(_httpClient);
        //private BuySellDetailHttpService _BuySellDetail = new BuySellDetailHttpService(_httpClient);
        //private MultimediaChapterDetailHttpService _multimediaChapterDetails = new MultimediaChapterDetailHttpService(_httpClient);
        //private PriceListHttpService _priceList = new PriceListHttpService(_httpClient);
        public BFrmFactor()
        {
            InitializeComponent();
            this.Shown += BFrmFactor_Shown;
            layHeader.BtnNewClick += LayHeader_BtnNewClick;
            layHeader.BtnSaveClick += LayHeader_BtnSaveClick;
            layHeader.BtnCancelClick += LayHeader_BtnCancelClick;
        }
        private async void BFrmFactor_Shown(object? sender, EventArgs e)
        {
            SetFonts();
            await SetFieldLayHeader();
            await SetFieldDgvDetail();
            await SetFieldDgvHeader();
        }
        private void LayHeader_BtnNewClick(object? sender, EventArgs e)
        {
            dgvDetail.Enabled = true;
        }
        private async void LayHeader_BtnSaveClick(object? sender, EventArgs e)
        {
            var getId = layHeader.GetValue<Guid>("Id", true, Guid.NewGuid());
            var cmbDate = layHeader.GetValue<string>("تاریخ").ShamsiToMiladi();
            var cmbBuyer = layHeader.GetValue<Guid>("خریدار");

            await _factorHeader.AddAsync(new BFactorHeader
            {
                Id = getId,
                Date = cmbDate,
                BuyerId = cmbBuyer,
                Header = "فنی مهندسی ایران کاوش",

            });
            var details = new List<BFactorDetail>();

            for (var index = 0; index < _dtDetail.Rows.Count; index++)
            {
                var i = _dtDetail.Rows[index];
                var bFactorDetail = new BFactorDetail
                {
                    FactorHeaderId = getId,
                    Code = index + 1,
                    Count = i["تعداد"].GetNum<long>(),
                    ProductId = i["نام کالا"].ToGuid(false),
                    PriceUnit = i["قیمت خرید"].GetNum<long>(),
                    PriceSell = i["قیمت فروش"].GetNum<long>(),
                };
                details.Add(bFactorDetail);
                //  await _factorDetail.AddAsync(bFactorDetail);
            }

            await _factorDetail.AddRange(details, getId);
            dgvDetail.Enabled = false;
            await SetFieldDgvHeader();
        }
        private void LayHeader_BtnCancelClick(object? sender, EventArgs e)
        {
            dgvDetail.Enabled = false;
        }


        private void BFrmFactor_Load(object sender, EventArgs e)
        {

        }

        public void SetFonts()
        {
            _clsFont.ChangeFont(dgvHeader, 13);
            _clsFont.ChangeFont(dgvDetail, 13);
            dgvHeader.SetStyle();
            dgvDetail.SetStyle();

            dgvHeader.ActiveScrollGrid();
            //_clsFont.ChangeFont(dgvChapter);
            //_clsFont.ChangeFont(dgvChapterEn);
            //_clsFont.ChangeFont(dgvHeaderChapter);
            //_clsFont.ChangeFont(dgvAllSelect, 12);

            //dgvChapter.SetStyle();
            //dgvChapterEn.SetStyle();
            //dgvHeaderChapter.SetStyle();
            //dgvAllSelect.SetStyle();

            //viewChapter.IndicatorWidth = 30;
            //viewChapterEn.IndicatorWidth = 30;

            //viewChapter.OptionsSelection.MultiSelect=true;
            //viewChapter.OptionsSelection.UseIndicatorForSelection = true;
            //viewChapter.OptionsSelection.CheckBoxSelectorField = "انتخاب";
            //viewChapter.OptionsSelection.EnableAppearanceFocusedRow = true;
            //viewChapter.OptionsSelection.MultiSelectMode = GridMultiSelectMode.RowSelect;

        }

        public async Task SetFieldLayHeader()
        {
            pnlOperation.WaitDownPage(async () =>
            {
                layHeader.AddButtonOperation();
                pnlOperation.Controls.Add(layHeader.ShowPanelOperation());

                var txtId = ClsCollect.ModelTextEdit("Id", 50, "System", false);
                var txtCode = ClsCollect.ModelTextEdit("کد", 10, "System", false);
                var cmbDate = ClsCollect.ModelDateTime("تاریخ", 10, "تاریخ");

                var getPerson = (await _personal.GetAsync()).Select(s => new { s.Id, s.FullName }).ToList();

                var cmbBuyer = ClsCollect.ModelGridToDataLayout("خریدار", getPerson, "Id", "FullName", "خریدار");
                cmbBuyer.HiddenColumn("Id");

                layHeader.SetFieldColumnDataLayout(true, 1, [
                    new() { Grp = 1, Ctrl = txtId, },
                new() { Grp = 1, Ctrl = txtCode },
                new() { Grp = 1, Ctrl = cmbDate, AllowNull = false },
                new() { Grp = 1, Ctrl = cmbBuyer, AllowNull = false }
                ], 13);
            });
        }

        private DataTable _dtDetail;

        public async Task SetFieldDgvDetail()
        {
            dgvDetail.WaitDownPage(async () =>
            {
                if (dgvDetail.ColumnCount() == 0)
                {

                    _dtDetail = dgvDetail.GridStructure([
                        new KavoshGrid.modelColumn
                    {
                        Name = "حذف", Object = KavoshGrid.enumObject.Button,
                        ImageValue = MyCom.Properties.Resources.delete5
                    },
                    new KavoshGrid.modelColumn { Name = "ردیف", },
                    new KavoshGrid.modelColumn { Name = "Id", Type = typeof(Guid) },
                    new KavoshGrid.modelColumn { Name = "کد" },
                    new KavoshGrid.modelColumn { Name = "نام کالا", Type = typeof(Guid) },
                    //new KavoshGrid.modelColumn { Name = "واحد" },
                    new KavoshGrid.modelColumn { Name = "تعداد", Type = typeof(long), PriceActive = true },
                    new KavoshGrid.modelColumn { Name = "قیمت خرید", Type = typeof(long), PriceActive = true },
                    new KavoshGrid.modelColumn { Name = "قیمت فروش", Type = typeof(long), PriceActive = true },
                    new KavoshGrid.modelColumn { Name = "قیمت کل", Type = typeof(long), PriceActive = true },
                ], true, false, true);

                    dgvDetail.AllowEditColumn("قیمت کل");
                    dgvDetail.AllowEditColumn("ردیف");
                    dgvDetail.AllowEditColumn("کد");
                    dgvDetail.AddAllowNewRowAndType(DefaultBoolean.True, NewItemRowPosition.Top);

                    #region Relation

                    var getProduct = (await _product.GetAsync()).Where(w=>w.FundTypeId==Guid.Parse("6b9fbf13-0c69-4080-85f0-e367a748a7c8")).Select(s => new { s.Id, s.NameApp }).ToList();
                    var cmbProduct = dgvDetail.AddGridToGrid(getProduct, "نام کالا", "Id", "NameApp");
                    cmbProduct.HiddenColumn("Id");
                    #endregion

                    #region Event

                    dgvDetail.GetViewBase.CellValueChanged += (s1, e1) =>
                    {
                        var clnN = e1.Column.FieldName;
                        var getCnt = dgvDetail.GetValue<string>("تعداد").GetNum<long>();
                        var getPriceUnit = dgvDetail.GetValue<string>("قیمت فروش").GetNum<long>();

                        if (clnN == "تعداد" || clnN == "قیمت فروش")
                        {
                            var getTotal = getCnt * getPriceUnit;
                            dgvDetail.SetValue("قیمت کل", getTotal);
                        }

                    };

                    #endregion
                }

                #region Load



                #endregion
            });
        }

        private DataTable _dtHeader;

        public async Task SetFieldDgvHeader()
        {
            dgvHeader.WaitDownPage(async () =>
            {

                if (dgvHeader.ColumnCount() == 0)
                {
                    _dtHeader = dgvHeader.GridStructure([
                    new KavoshGrid.modelColumn { Name = "انتخاب", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.view },
                    new KavoshGrid.modelColumn { Name = "حذف", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete5 },
                    new KavoshGrid.modelColumn { Name = "ردیف" },
                    new KavoshGrid.modelColumn { Name = "Id", Type = typeof(Guid) },
                    new KavoshGrid.modelColumn { Name = "کد" },
                    new KavoshGrid.modelColumn { Name = "تاریخ", Type = typeof(string) },
                    new KavoshGrid.modelColumn { Name = "خریدار", Type = typeof(Guid) },
                    new KavoshGrid.modelColumn { Name = "سربرگ" },
                    new KavoshGrid.modelColumn { Name = "مبلغ کل", Type = typeof(long), PriceActive = true },
                    new KavoshGrid.modelColumn { Name = "آدرس", Type = typeof(string)},
                    new KavoshGrid.modelColumn { Name = "چاپ", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.Print2 },
                ], false, false, true);

                    //dgvHeader.GetViewBase.Columns["چاپ"].OwnerBand.Fixed = FixedStyle.Right;
                    //dgvHeader.GetViewBase.Columns["انتخاب"].OwnerBand.Fixed = FixedStyle.Left;

                    dgvHeader.FixColumn("چاپ", FixedStyle.Right);
                    dgvHeader.FixColumn("انتخاب", FixedStyle.Left);

                    //dgvHeader.GetViewBase.Bands.Add(new GridBand{Name = "Print"});
                    //dgvHeader.GetViewBase.Bands["Print"].Columns.Add(dgvHeader.GetViewBase.Columns["چاپ"]);
                    //dgvHeader.GetViewBase.Bands["Print"].Fixed = FixedStyle.Right;


                    //dgvHeader.GetViewBase.Columns["چاپ"].Fixed = FixedStyle.Right;
                    //dgvHeader.GetViewBase.Columns["انتخاب"].Fixed = FixedStyle.Left;
                    dgvHeader.HiddenColumn("Id");
                    dgvHeader.MaxMinWidth("انتخاب", 30, 30);
                    dgvHeader.MaxMinWidth("حذف", 30, 30);
                    dgvHeader.MaxMinWidth("ردیف", 30, 30);
                    dgvHeader.MaxMinWidth("چاپ", 30, 30);
                    //dgvHeader.AllowEditColumn("مبلغ کل");
                    //dgvHeader.AllowEditColumn("ردیف");
                    //dgvHeader.AllowEditColumn("خریدار");
                    //dgvHeader.AllowEditColumn("تاریخ");
                    dgvHeader.AddAllowNewRowAndType(DefaultBoolean.False, NewItemRowPosition.None);

                    #region Relation

                    var getPerson = (await _personal.GetAsync()).Select(s => new { s.Id, s.FullName }).ToList();
                    var cmbBuyer = dgvHeader.AddGridToGrid(getPerson, "خریدار", "Id", "FullName");
                    cmbBuyer.HiddenColumn("Id");

                    #endregion

                    #region Event
                    dgvHeader.AddEventRowCellClick<Guid>(obj =>
                    {

                    }, "Id", "حذف");
                    dgvHeader.AddEventRowCellClick<Guid>(obj =>
                    {

                    }, "Id", "انتخاب");
                    dgvHeader.AddEventRowCellClick<Guid>(async obj =>
                    {
                        var getProduct = await _product.GetAsync();
                        var getHeader = (await _factorHeader.GetById(obj));
                        var getDetails = (await _factorDetail.GetAsync()).Where(w => w.FactorHeaderId == obj).Select(s => new ClassReport.ClassReport.ModelFactorDetail
                        {
                            Code = s.Code,
                            Count = s.Count.GetNum<long>(),
                            Price = s.PriceSell,
                            Product = getProduct.First(f => f.Id == s.ProductId).NameApp,
                            Total = s.Count.GetNum<long>() * s.PriceSell
                        }).ToList();
                        ClassReport.ClassReport.ModelFactor modelFactor = new ClassReport.ClassReport.ModelFactor
                        {
                            Num = getHeader.Code.ToString(),
                            Buyer = (string)dgvHeader.GetText("خریدار"),
                            Date = (string)dgvHeader.GetText("تاریخ"),
                            Header = (string)dgvHeader.GetText("سربرگ"),
                            Address = (string)dgvHeader.GetText("آدرس"),
                            Mobile = "09191490131",
                            FactorDetails = getDetails
                        };
                        RptFactor rptFactor = new RptFactor();
                        rptFactor.Tag = modelFactor;
                        rptFactor.ShowPreview();
                        //rptFactor.ShowDesigner();
                    }, "Id", "چاپ");

                    #endregion

                }

                // Load existing headers and compute totals from details
                _dtHeader.Rows.Clear();
                var headers = (await _factorHeader.GetAsync()).ToList();
                var details = (await _factorDetail.GetAsync());

                foreach (var h in headers.OrderByDescending(o => o.Code))
                {
                    var row = _dtHeader.NewRow();
                    row["Id"] = h.Id;
                    row["کد"] = h.Code.ToString(); // در صورت داشتن فیلد کد متفاوت، این را اصلاح کنید
                    row["تاریخ"] = h.Date.DateTimePersian().Date;
                    row["خریدار"] = h.BuyerId;
                    row["سربرگ"] = h.Header;

                    var total = details
                        .Where(d => d.FactorHeaderId == h.Id)
                        .Sum(d => (long)(d.Count * d.PriceSell));
                    row["مبلغ کل"] = total;

                    _dtHeader.Rows.Add(row);
                }
            });
        }
    }
}