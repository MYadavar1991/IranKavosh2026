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
    public partial class FrmProduct : XtraForm
    {
        private ClsFont _clsFont = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private static readonly HttpClient _httpClient = new();
        private readonly ProductHttpService _product = new(_httpClient);
        private readonly FundTypeHttpService _fundType = new(_httpClient);

        public FrmProduct()
        {
            InitializeComponent();
            this.Shown += FrmProduct_Shown;
        }

        private async void FrmProduct_Shown(object? sender, EventArgs e)
        {
            SetFonts();
            await SetFieldDgvProduct();
        }

        public void SetFonts()
        {
            _clsFont.ChangeFont(dgvProduct, 12);
            dgvProduct.RightToLeft = RightToLeft.Yes;

            //_clsFont.ChangeFont(dgvChapter);
            //_clsFont.ChangeFont(dgvChapterEn);
            //_clsFont.ChangeFont(dgvHeaderChapter);
            //_clsFont.ChangeFont(dgvAllSelect, 12);

            dgvProduct.SetStyle();
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

        private DataTable _dtProduct;

        public async Task SetFieldDgvProduct()
        {
            await dgvProduct.InvokeThread(async () =>
             {
                 if (dgvProduct.ColumnCount() == 0)
                 {
                     _dtProduct = dgvProduct.GridStructure([
                          new KavoshGrid.modelColumn { Name = "Id", Type = typeof(Guid) },
                        new KavoshGrid.modelColumn
                        {
                            Name = "حذف", Object = KavoshGrid.enumObject.Button,
                            ImageValue = MyCom.Properties.Resources.delete4
                        },
                        new KavoshGrid.modelColumn { Name = "ردیف", },
                        new KavoshGrid.modelColumn { Name = "کد", Type = typeof(long) },
                        new KavoshGrid.modelColumn { Name = "نام فارسی", Type = typeof(string) },
                        new KavoshGrid.modelColumn { Name = "نام چاپ", Type = typeof(string) },
                        //new KavoshGrid.modelColumn{Name = "واحد",Type = typeof(Guid)},
                        new KavoshGrid.modelColumn { Name = "دسته بندی", Type = typeof(Guid) },
                        new KavoshGrid.modelColumn
                            { Name = "وضعیت", Type = typeof(bool), Object = KavoshGrid.enumObject.Checked }
                      ], true, true, true);

                     dgvProduct.GetViewBase.RowHeight = 30;

                     dgvProduct.AddAllowNewRowAndType(DefaultBoolean.True, NewItemRowPosition.Top);
                     dgvProduct.HiddenColumn("Id");
                     dgvProduct.AllowEditColumn("کد");
                     dgvProduct.AllowEditColumn("ردیف");
                     dgvProduct.MaxMinWidth("حذف", 35, 35);
                     dgvProduct.MaxMinWidth("ردیف", 45, 45);
                     dgvProduct.MaxMinWidth("کد", 45, 45);
                     dgvProduct.ActiveScrollGrid();

                     #region Relation

                     var getFundType = (await _fundType.GetAsync()).Select(s => new { s.Id, s.Title }).ToList();
                     var getFund = dgvProduct.AddGridToGrid(getFundType, "دسته بندی", "Id", "Title");
                     getFund.HiddenColumn("Id");

                     #endregion

                     #region Event

                     dgvProduct.GetViewBase.ValidateRow += async (s1, e1) =>
                      {

                          var getRow = e1.Row as DataRowView;

                          var getId = getRow["Id"].ToGuid(true);
                          var getName1 = getRow["نام فارسی"].ToString();
                          var getName2 = getRow["نام چاپ"].ToString();
                          var getFundTypeId = getRow["دسته بندی"].ToGuid(false);
                          var getIsActive1 = getRow["وضعیت"].ToString();
                          var getIsActive = getIsActive1 != "" && Convert.ToBoolean(getRow["وضعیت"].ToString());

                          if (!string.IsNullOrEmpty(getName1) && getFundTypeId != Guid.Empty)
                          {
                              await _product.AddAsync(new Product
                              {
                                  Id = getId,
                                  FundTypeId = getFundTypeId,
                                  NameApp = getName1,
                                  NamePrint = getName2,
                                  UnitId = Guid.Parse("308e6b00-e1dd-42de-bd60-12fec7304cc9"),
                                  IsActive = getIsActive

                              });
                              await SetFieldDgvProduct();
                          }
                          else
                          {
                              e1.Valid = false;
                          }


                      };

                     #endregion
                 }

                 #region Data

                 _dtProduct.Rows.Clear();
                 var getProduct = await _product.GetAsync();
                 foreach (Product i in getProduct.OrderByDescending(o => o.Code))
                 {
                     _dtProduct.Rows.Add(i.Id, "", "", i.Code, i.NameApp, i.NameApp, i.FundTypeId, i.IsActive);
                 }

                 dgvProduct.SetFieldSizeColumn();

                 #endregion
             }, true);
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {

        }
    }
}
