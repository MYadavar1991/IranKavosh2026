using DevExpress.XtraEditors;
using DocumentFormat.OpenXml.Wordprocessing;
using IranKavosh.Layered.Model.Tables;
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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;
using Personal = IranKavosh.Layered.Model.Tables.Personal;

namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class FrmPerson : DevExpress.XtraEditors.XtraForm
    {
        private readonly Guid _hddMemory = default;
        private ClsFont _clsFont = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private static HttpClient _httpClient = new HttpClient();
        private PersonalHttpService _personal = new PersonalHttpService(_httpClient);
        private PersonalGroupHttpService _personalGroup = new PersonalGroupHttpService(_httpClient);
        private HddMemoryPersonHttpService _hddMemoryPerson = new HddMemoryPersonHttpService(_httpClient);
        public FrmPerson()
        {
            InitializeComponent();

        }

        public FrmPerson(Guid hddMemory)
        {
            InitializeComponent();
            _hddMemory = hddMemory;

        }
        private void FrmPerson_Load(object sender, EventArgs e)
        {
            this.Shown += FrmPerson_Shown;
            // this.Shown += async (s1, e1) => await FrmPerson_Shown(s1, e1);
        }

        private void FrmPerson_Shown(object sender, EventArgs e)
        {
            this.WaitDownPage(() =>
            {
                SetFont();
                _ = SetFieldLayPerson();
                _ = SetFieldGridPerson();
                if (_hddMemory != default)
                {
                    btnTakhsis.Invoke(() => { btnTakhsis.Enabled = true; });

                }

            });

        }
        public Task SetFont()
        {
            gridPerson.SetStyle();
            //  gridPerson.SettingGridView();
            _clsFont.ChangeFont(gridPerson, 12);
            _clsFont.ChangeFont(searchControl1, 12);
            _clsFont.ChangeFont(btnTakhsis, 12);
            return Task.CompletedTask;
        }
        public async Task SetFieldLayPerson()
        {
            var txtId = ClsCollect.ModelTextEdit("Id", 100, "", false);
            var txtFName = ClsCollect.ModelTextEdit("نام", 30, "نام", true);
            var txtLName = ClsCollect.ModelTextEdit("فامیلی", 30, "نام خانوادگی", true);

            var getPGroup = await _personalGroup.GetAsync();

            var lkpGroup = ClsCollect.ModelGridToDataLayout("گروه", getPGroup.Select(s => new { s.Id, s.Title }).ToList(),
                "Id", "Title", "گروه را انتخاب نمایید");
            lkpGroup.HiddenColumn("Id");
            // var txtNationalCode = ClsCollect.ModelTextEdit("کد ملی", 100, "", false);
            // var lkpGender = ClsCollect.ModelLookUpEdit("جنسیت", 100, "", false);
            var txtMobile = ClsCollect.ModelTextEdit("موبایل", 100, "", true);
            var txtUser = ClsCollect.ModelTextEdit("نام کاربری", 15, "نام کاربری", true);
            var txtPass = ClsCollect.ModelTextEditPassword("رمز عبور", 6, 10, "رمز عبور", true);
            var txtAddress = ClsCollect.ModelLayoutMemoEdit("آدرس", 400, "آدرس", true);
            //var txtDescription = ClsCollect.ModelLayoutMemoEdit("توضیحات", 300, "...");


            layPerson.SetFieldColumnDataLayout(true, 1, new List<ClsCollect.modelControlDataLayout>
            {
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtId, Visibility = LayoutVisibility.Never },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtFName, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtLName, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtMobile, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = lkpGroup, AllowNull = false },
                //new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtNationalCode, AllowNull = false },
                //new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = lkpGender, AllowNull = false },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtUser, AllowNull = true },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtPass, AllowNull = true },
                new ClsCollect.modelControlDataLayout { Grp = 1, Ctrl = txtAddress, AllowNull = true },
            }, 12, true);

        }

        private DataTable _dtPerson;
        public async Task SetFieldGridPerson()
        {
            if (gridPerson.ColumnCount() == 0)
            {
                _dtPerson = gridPerson.GridStructure(new List<KavoshGrid.modelColumn>
                {
                    //new KavoshGrid.modelColumn { Name = "ویرایش",Object = KavoshGrid.enumObject.Link,ImageValue = MyCom.Properties.Resources.edit},
                    new KavoshGrid.modelColumn { Name = "ویرایش",Object = KavoshGrid.enumObject.Button,ImageValue = MyCom.Properties.Resources.edit},
                    new KavoshGrid.modelColumn { Name = "Id", Type = typeof(Guid) },
                    new KavoshGrid.modelColumn { Name = "کد", Type = typeof(int) },
                    new KavoshGrid.modelColumn { Name = "مشخصات", Type = typeof(string) },
                    new KavoshGrid.modelColumn { Name = "گروه", Type = typeof(Guid) },
                    //new KavoshGrid.modelColumn { Name = "کد ملی", Type = typeof(string) },
                    //new KavoshGrid.modelColumn { Name = "جنسیت", Type = typeof(string) },
                    new KavoshGrid.modelColumn { Name = "موبایل", Type = typeof(string) },
                    new KavoshGrid.modelColumn { Name = "نام کاربری", Type = typeof(string) },

                }, false, true, false);
                gridPerson.HiddenColumn("Id");
                #region Setting

                gridPerson.MaxMinWidth("ویرایش", 35, 35);

                #endregion

                #region Relation

                var getPGroup = await _personalGroup.GetAsync();
                var dataGroup = getPGroup.Select(s => new { s.Id, s.Title }).ToList();
                gridPerson.AddGridToGrid(dataGroup, "گروه", "Id", "Title");

                #endregion

                #region Event

                gridPerson.AddEventRowCellClick<Guid>(async void (obj) =>
                {
                    var getData = await _personal.GetById(obj);
                    layPerson.CallNew();
                    layPerson.SetValueType("Id", getData.Id);
                    layPerson.SetValueType("نام", getData.FirstName);
                    layPerson.SetValueType("فامیلی", getData.LastName);
                    layPerson.SetValueType("گروه", getData.PersonalGroupId);
                    layPerson.SetValueType("موبایل", getData.Mobile);
                    layPerson.SetValueType("نام کاربری", getData.Username);
                    layPerson.SetValueType("رمز عبور", getData.Password);

                }, "Id", "ویرایش");

                #endregion

            }

            #region Load Data

            _dtPerson.Rows.Clear();
            this.Wait(async void () =>
            {
                try
                {
                    var get = await _personal.GetAsync();
                    gridPerson.BeginUpdate();
                    foreach (var item in get.OrderByDescending(o=>o.Code))
                    {
                        _dtPerson.Rows.Add(new object[]
                        {
                            "",
                                item.Id,
                                item.Code,
                                item.FirstName + " " + item.LastName,
                                item.PersonalGroupId,
                                item.Mobile,
                                item.Username,
                        });
                    }
                    gridPerson.EndUpdate();
                }
                catch (Exception e)
                {
                    throw; // TODO handle exception
                }
            });

            #endregion
        }


        private async void layPerson_BtnSaveClick(object sender, EventArgs e)
        {
            this.WaitDownPage(async void () =>
            {
                var getId = layPerson.GetValue<Guid>("Id");
                var txtFName = layPerson.GetValue<string>("نام");
                var txtLName = layPerson.GetValue<string>("فامیلی");
                var txtMobile = layPerson.GetValue<string>("موبایل");
                var lueGroup = layPerson.GetValue<Guid>("گروه");
                var txtUser = layPerson.GetValue<string>("نام کاربری");
                var luePass = layPerson.GetValue<string>("رمز عبور");
                var getAddress= layPerson.GetValue<string>("آدرس");
                //var get = await _personal.GetAsync();
                await _personal.AddAsync(new Personal()
                {
                    Id = getId,
                    FirstName = txtFName,
                    LastName = txtLName,
                    PersonalGroupId = lueGroup,
                    Mobile = txtMobile,
                    Username = txtUser,
                    Password = luePass,
                    Code = 1,
                    CodeMelli = "000000000",
                    Address = getAddress,
                    

                });
                await SetFieldGridPerson();

            });

        }

        private async void btnTakhsis_Click(object? sender, EventArgs e)
        {
            var getPersonId = gridPerson.GetValue<Guid>("Id");
            var getHddPerson = (await _hddMemoryPerson.GetAsync());
            var hddId = Guid.NewGuid();
            if (getHddPerson.Any(a=>a.HddMemoryId == _hddMemory))
            {
                hddId = getHddPerson.First(a => a.HddMemoryId == _hddMemory).Id;
            }
            await _hddMemoryPerson.AddAsync(new HddMemoryPerson
            {
            
                Id = hddId,
                HddMemoryId = _hddMemory,
                PersonalId = getPersonId
            });

            Close();
        }
    }
}