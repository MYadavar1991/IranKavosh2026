using DevExpress.XtraEditors;
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
using DevExpress.XtraGrid.Views.Grid;
using IranKavosh.Layered.Model.Tables;
using MyCom.Object;

namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class FrmPriceList : DevExpress.XtraEditors.XtraForm
    {
        private ClsFont _font = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private static HttpClient _httpClient = new HttpClient();
        private ProductHttpService _product = new ProductHttpService(_httpClient);
        private PriceListHttpService _priceList = new PriceListHttpService(_httpClient);
        private PriceListDiscountHttpService _priceListDiscount = new PriceListDiscountHttpService(_httpClient);
        //private PersonalHttpService _personal = new PersonalHttpService(_httpClient);
        //private PersonalGroupHttpService _personalGroup = new PersonalGroupHttpService(_httpClient);
        public FrmPriceList()
        {
            InitializeComponent();
            this.Shown += FrmBuySell_Shown;
        }

        private async void FrmBuySell_Shown(object? sender, EventArgs e)
        {
            await SetFieldDgvPriceList();
            //await SetFiledGdvDetails();
            await SetFonts();
        }

        private void FrmPriceList_Load(object sender, EventArgs e)
        {
          
        }
        public async Task SetFonts()
        {
            _font.ChangeFont(dgvPriceListDiscount);
            _font.ChangeFont(dgvPriceList);

            dgvPriceListDiscount.SetStyle();
            dgvPriceList.SetStyle();
        }

        private DataTable _dtPriceList;
        private Guid _ProductId;
        public async Task SetFieldDgvPriceList()
        {
            await dgvPriceList.InvokeThread(async () =>
            {
                if (dgvPriceList.ColumnCount() == 0)
                {
                    _dtPriceList = dgvPriceList.GridStructure([
                        new KavoshGrid.modelColumn
                        {
                            Name = "انتخاب", Object = KavoshGrid.enumObject.Button,
                            ImageValue = MyCom.Properties.Resources.view
                        },

                        new KavoshGrid.modelColumn
                        {
                            Name = "حذف", Object = KavoshGrid.enumObject.Button,
                            ImageValue = MyCom.Properties.Resources.delete3
                        },

                        new KavoshGrid.modelColumn { Name = "ردیف", },
                        new KavoshGrid.modelColumn { Name = "Id", Type = typeof(Guid) },
                        new KavoshGrid.modelColumn { Name = "خدمات", Type = typeof(Guid) }, // ProductId + 
                        new KavoshGrid.modelColumn { Name = "موضوع", Type = typeof(string) },
                        new KavoshGrid.modelColumn { Name = "مبلغ", Type = typeof(long), PriceActive = true }


                    ], true, true, false);

                    dgvPriceList.ActiveScrollGrid();
                    dgvPriceList.HiddenColumn("Id");
                    dgvPriceList.MaxMinWidth("حذف", 35, 35);
                    dgvPriceList.MaxMinWidth("مبلغ", 70, 70);
                    dgvPriceList.ReadOnlyColumn("ردیف");
                    dgvPriceList.MaxMinWidth("انتخاب", 35, 35);
                    dgvPriceList.MaxMinWidth("موضوع", 200, 200);

                    dgvPriceList.AddAllowNewRowAndType(DefaultBoolean.True, NewItemRowPosition.Top);

                    #region Relation

                    var getProduct = (await _product.GetAsync()).Select(s => new { s.Id, s.NameApp }).ToList();
                    var cmbProduct = dgvPriceList.AddGridToGrid(getProduct, "خدمات", "Id", "NameApp");
                    cmbProduct.HiddenColumn("Id");

                    #endregion

                    #region Event

                    dgvPriceList.GetViewBase.ValidateRow += async (s1, e1) =>
                    {
                        try
                        {
                            var row = e1.Row as DataRowView;
                            var getId = row["Id"].ToGuid(true);
                            var getProduct = row["خدمات"].ToGuid(false);
                            var getPrice = row["مبلغ"].GetNum<long>();
                            var getTitle = row["موضوع"].ToString();

                            var priceList = new PriceList
                            {
                                Id = getId,
                                Price = getPrice,
                                ProductId = getProduct,
                                Title = getTitle,
                                DateTime = DateTime.Now
                            };
                            await _priceList.AddAsync(priceList);
                            await SetFieldDgvPriceList();
                        }
                        catch (Exception e)
                        {

                            ClassMessageBox.ShowMSG("ورودی ها رو کنترل کنید.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار, ClsUI.NotOk);
                        }
                    };

                    dgvPriceList.AddEventRowCellClick<Guid>(async id =>
                    {
                        _ProductId = dgvPriceList.GetValue<Guid>("خدمات");

                        await SetFieldDgvPriceListDiscount(id, _ProductId);
                    }, "Id", "انتخاب");

                    #endregion
                }

                #region Load Data

                _dtPriceList.Rows.Clear();
                var getData = await _priceList.GetAsync();
                foreach (PriceList i in getData)
                {
                    _dtPriceList.Rows.Add("", "", "", i.Id, i.ProductId, i.Title, i.Price);
                }

                dgvPriceList.SetFieldSizeColumn();

                #endregion

            }, true);
            //await dgvPriceList.InvokeThread(async () =>
            // {

            // }, true);

        }

        #region (Discount)
        private DataTable _dtPriceListDiscount;
        public async Task SetFieldDgvPriceListDiscount(Guid PriceListId, Guid ProductId)
        {
            await dgvPriceListDiscount.InvokeThread(async () =>
            {
                var getPriceList = (await _priceList.GetAsync()).Select(s => new { s.Id, s.Title }).ToList();

                if (dgvPriceListDiscount.ColumnCount() == 0)
                {
                    _dtPriceListDiscount = dgvPriceListDiscount.GridStructure(new List<KavoshGrid.modelColumn>
                    {
                        new KavoshGrid.modelColumn { Name = "حذف", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete3 },
                        new KavoshGrid.modelColumn { Name = "ردیف", },
                        new KavoshGrid.modelColumn { Name = "Id", Type = typeof(Guid) },
                        new KavoshGrid.modelColumn { Name = "خدمات", Type = typeof(Guid) },
                        new KavoshGrid.modelColumn { Name = "تعداد", Type = typeof(int) },
                        new KavoshGrid.modelColumn { Name = "درصد", Type = typeof(float),FormatString  = "N2"},
                        new KavoshGrid.modelColumn { Name = "توضیحات", Type = typeof(string), Object = KavoshGrid.enumObject.TextMemo },
                    }, true, true, false);
                    dgvPriceListDiscount.ReadOnlyColumn("ردیف");
                    dgvPriceListDiscount.ActiveScrollGrid();
                    dgvPriceListDiscount.HiddenColumn("Id");
                    dgvPriceListDiscount.MaxMinWidth("حذف", 35, 35);
                    dgvPriceListDiscount.MaxMinWidth("توضیحات", 200, 200);
                    dgvPriceListDiscount.MaxMinWidth("خدمات", 110, 110);

                    dgvPriceListDiscount.AddAllowNewRowAndType(DefaultBoolean.True, NewItemRowPosition.Top);

                    #region Relation





                    //var cmbPriceList = dgvPriceListDiscount.AddGridToGrid(getPriceList, "نوع خدمات", "Id", "Title");
                    //cmbPriceList.HiddenColumn("Id");

                    #endregion

                    #region Event

                    dgvPriceListDiscount.GetViewBase.ValidateRow += async (s1, e1) =>
                    {
                        try
                        {
                            var row = e1.Row as DataRowView;
                            var getId = row["Id"].ToGuid(true);
                            var getCount = row["تعداد"].GetNum<int>();
                            var getPriceListId = row["خدمات"].ToGuid(false);
                            float getDiscount = Convert.ToSingle(row["درصد"].ToString());
                            //var getProduct = row["خدمات"].ToGuid(false);
                            //var getPrice = row["مبلغ"].GetNum<long>();
                            var getTitle = row["توضیحات"].ToString();

                            var priceListDiscount = new PriceListDiscount
                            {
                                Id = getId,
                                DateTime = DateTime.Now,
                                Count = getCount,
                                PriceListId = getPriceListId,
                                Discount = getDiscount,
                                Description = getTitle
                            };
                            await _priceListDiscount.AddAsync(priceListDiscount);
                        }
                        catch (Exception e)
                        {
                            ClassMessageBox.ShowMSG("ورودی ها رو کنترل کنید.", Class_Text.Msg_Name, ClassMessageBox.enumIcon.هشدار, ClsUI.NotOk);
                        }
                    };

                    #endregion
                }
                #region Load Data

                #region Relation
                var getProduct = (await _product.GetAsync()).Where(w => w.Id == ProductId).Select(s =>
                {
                    var first = getPriceList.First(f => f.Id == PriceListId);
                    return new
                    {
                        first.Id,
                        Product = s.NameApp + ": " + first.Title
                    };
                }).ToList();
                var cmbProduct = dgvPriceListDiscount.AddGridToGrid(getProduct, "خدمات", "Id", "Product");
                cmbProduct.HiddenColumn("Id");

                #endregion



                _dtPriceListDiscount.Rows.Clear();
                var getData = (await _priceListDiscount.GetAsync()).Where(w => w.PriceListId == PriceListId).OrderBy(o => o.Count);
                foreach (var i in getData)
                {
                    //new KavoshGrid.modelColumn { Name = "حذف", Object = KavoshGrid.enumObject.Button, ImageValue = MyCom.Properties.Resources.delete3 },
                    //new KavoshGrid.modelColumn { Name = "ردیف", },
                    //new KavoshGrid.modelColumn { Name = "Id", Type = typeof(Guid) },
                    //new KavoshGrid.modelColumn { Name = "خدمات", Type = typeof(Guid) },
                    //new KavoshGrid.modelColumn { Name = "تعداد", Type = typeof(int) },
                    //new KavoshGrid.modelColumn { Name = "درصد", Type = typeof(int), },
                    //new KavoshGrid.modelColumn { Name = "توضیحات", Type = typeof(string), Object = KavoshGrid.enumObject.TextMemo },

                    _dtPriceListDiscount.Rows.Add("", "", i.Id, i.PriceListId, i.Count, i.Discount.ToString("N2"), i.Description);
                }

                dgvPriceListDiscount.SetFieldSizeColumn();
                #endregion
            }, true);




        }


        #endregion
    }
}