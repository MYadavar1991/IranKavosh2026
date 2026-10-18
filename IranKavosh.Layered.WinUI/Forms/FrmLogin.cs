using DevExpress.XtraEditors;
using DevExpress.XtraScheduler.Drawing;
using DocumentFormat.OpenXml.Drawing.Charts;
using IranKavosh.Layered.Service.Middleware;
using IranKavosh.Layered.Service.Services;
using MyCom.Class;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http;
using IranKavosh.Layered.Model.Tables;

//using Microsoft.AspNetCore.SignalR.Client;


namespace IranKavosh.Layered.WinUI.Forms
{
    public partial class FrmLogin : DevExpress.XtraEditors.XtraForm
    {
        private ClsFont _clsFontBold = new ClsFont(ClsFont.enumFont.samimBoldFD, true);
        private ClsFont _clsFont = new ClsFont(ClsFont.enumFont.samimBoldFD, false);
        public Guid _UserLogin = default;
        public bool exit = false;
        private static readonly HttpClient _httpClient = new();
        private readonly MultiAgeHttpService _multiAge = new(_httpClient);
        private readonly MultiActorHttpService _multiActor = new(_httpClient);
        private readonly MultiGenreHttpService _multiGenre = new(_httpClient);
        private readonly MultiCountryHttpService _multiCountry = new(_httpClient);
        private readonly MultiPriceHttpService _multiPrice = new(_httpClient);
        private readonly MultimediaHeaderHttpService _multiHeader = new(_httpClient);
        private readonly MultimediaHeaderChapterHttpService _multiHeaderChapter = new(_httpClient);
        private readonly ChanelMemberHttpService _chanelMember = new(_httpClient);
        private readonly PersonalHttpService _personal = new(_httpClient);
        private readonly LoginHttpService _login = new(_httpClient);
        public FrmLogin()
        {
            InitializeComponent();
        }

        public void SetFonts()
        {
            _clsFontBold.ChangeFont(btnPass, 14);
            _clsFontBold.ChangeFont(btnEnter, 14);
            _clsFontBold.ChangeFont(btnExit, 14);
            _clsFontBold.ChangeFont(label1, 17);
            _clsFontBold.ChangeFont(label2, 17);
            //btnExit.LookAndFeel.UseDefaultLookAndFeel = false;
            btnExit.LookAndFeel.SkinName = "WXI";
            btnEnter.LookAndFeel.SkinName = "WXI";
        }
        private void FrmLogin_Load(object sender, EventArgs e)
        {
            //connection = new HubConnectionBuilder()
            //    .WithUrl(ClsURI.GetURI + "chathub")
            //    .Build();
            //connection.StartAsync();


            var defaultCurrentUsername = Properties.Settings.Default.CurrentUsername;
            if (!string.IsNullOrEmpty(defaultCurrentUsername))
            {
                txtUser.Text = defaultCurrentUsername;
                txtPassEnter.Select();
                txtPassEnter.Focus();
            }
            //TransparencyKey = Color.FromArgb(30, 231, 15);
            TransparencyKey = Color.DarkOliveGreen;
            SetFonts();
        }
        //HubConnection connection;

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
            //connection.InvokeAsync("SendMessage", "User1", "Test SignalR");
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            //this.Wait(() =>
            //{
            //    var getDate = DateTime.Now.DateTimePersian().ShortDateTime.Replace("/", "-").Replace(":", "-");
            //    //SrvBackupRestore.Backup(getDate);
            //});
            //// Application.ExitThread();
            ////Application.Exit();
            //exit = true;
            //Hide();
            Environment.Exit(0);
        }
        public void GetNewPass(Personal person)
        {
            var txtPass = ClsCollect.ModelTextEditPassword("رمز", 8, 8, "کلمه عبور حداقل 8 کاراکتر");
        
            XtraInputBoxArgs args = new XtraInputBoxArgs();

            args.Showing += (sender, e) =>
            {
                e.MessageBoxForm.Height = 150;
                // تغییر فونت عنوان پنجره (Caption)
                e.MessageBoxForm.Font = new Font("Samim FD", 11F);
                e.Buttons[DialogResult.OK].Text = "&تایید";
                e.Buttons[DialogResult.Cancel].Text = "انصراف";
                e.MessageBoxForm.Appearance.Font = new Font("Samim FD", 11F);
                e.MessageBoxForm.Text = Class_Text.Msg_Name;
                e.Buttons[DialogResult.OK].Enabled = false;

                txtPass.TextChanged += (s1, e1) =>
                {
                    e.Buttons[DialogResult.OK].Enabled = txtPass.Text.Length > 0;
                };

                e.Buttons[DialogResult.OK].Click += async (s1, e1) =>
                {
                    await _personal.UpdatePassword(person.BotUserId, txtPass.Text);
                  Environment.Exit(0);
                   
                };

            };

            args.Caption = Class_Text.Msg_Name;      // عنوان پنجره
            args.Prompt = "کلمه عبور جدید را تا 8 عدد یا حرف وارد نمایید"; // متن بالای ویرایشگر
            args.DefaultButtonIndex = 0;               // 0 = OK دکمه پیش‌فرض

            // استفاده از ویرایشگر تاریخ (DateEdit) به جای TextEdit پیش‌فرض


            //editor.Properties.CalendarView = DevExpress.XtraEditors.Repository.CalendarView.TouchUI;
            //editor.Properties.Mask.EditMask = "yyyy/MM/dd";
            args.Editor = txtPass;
            var result = XtraInputBox.Show(args);


        }
        private async void btnEnter_Click(object sender, EventArgs e)
        {
            await FunLogin();
        }

        private async Task FunLogin()
        {
            var getUsername = txtUser.Text;
            var getPass = txtPassEnter.Text;
            if (string.IsNullOrEmpty(getUsername))
            {
                txtUser.Select();
                txtUser.Focus();

                return;
            }
            var getUsers = (await _personal.GetAsync()).Where(w => w.Username == getUsername).ToList();

            if (getUsers.Any(a => a.Username.ToLower() == getUsername.ToLower() && a.Password == getPass))
            {
                Personal getF = getUsers.First(a => a.Username.ToLower() == getUsername.ToLower() && a.Password == getPass);
                if (getPass.Length == 6)
                {
                    GetNewPass(getF);
                    await FunLogin();
                }
                else
                {
                    Properties.Settings.Default.CurrentUsername = txtUser.Text;
                    //Properties.Settings.Default.CurrentToken = getToken;
                    Properties.Settings.Default.Save();

                    var getLogin = (await _login.GetAsync()).Where(w => w.PersonalId == getF.Id);
                    if (getLogin.Any())
                    {
                        var getFirst = getLogin.First();
                        getFirst.WinUser = Environment.UserName;
                        await _login.AddAsync(getFirst);
                    }
                    else
                    {
                        await _login.AddAsync(new Login
                        {
                            PersonalId = getF.Id,
                            WinUser = Environment.UserName
                        });
                    }

                    //SrvLogin.SetUser(person.Id.ToString());
                    this.Hide();
                }
                //if (!getUsers.First().IsActive)
                //{
                //    new Class_Text().Frm_Msg("حساب شما غیر فعال می باشد.", Class_Text.Msg_Name, 4);
                //    return;
                //}

                // var getToken = SrvLogin.Login(new Services.Login.LoginModel(getUsername.ToLower(), getPass)).Result;


            
                // _UserLogin = person.Id;
            }
            else
            {
                new Class_Text().Frm_Msg("نام کاربری و رمز را صحیح وارد نمایید", Class_Text.Msg_Name, 4);

                txtPassEnter.Focus();
                txtPassEnter.SelectAll();
                return;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtUser_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyData == Keys.Enter || e.KeyData == Keys.Return) && txtUser.Text != "")
            {
                txtPassEnter.Select();
                txtPassEnter.Focus();
            }
        }

        private async void txtPass_KeyDown(object sender, KeyEventArgs e)
        {

            if ((e.KeyData == Keys.Enter || e.KeyData == Keys.Return))
            {
                if (txtUser.Text == "")
                {
                    txtUser.Select();
                    txtUser.Focus();
                }
                else if (txtPassEnter.Text != "")
                {
                    btnEnter.Focus();
                   await FunLogin();
                }

            }
        }

        #region New Login












        public class LoginModel
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }

        public class TokenResponse
        {
            public string Token { get; set; }
        }

        #endregion

        //    var loginModel = new LoginModel { Username = "admin", Password = "admin" };
        //    var token = await AuthenticateUser(loginModel);
        //    if (!string.IsNullOrEmpty(token))
        //    {
        //        MessageBox.Show("Login successful!");
        //        // ذخیره توکن یا انجام عملیات دیگر
        //    }
        //    else
        //    {
        //        MessageBox.Show("Login failed. Please check your credentials.");
        //    }
        //}

        private void btnGetUserId_Click(object sender, EventArgs e)
        {




        }

        private void txtUser_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            //https://ble.ir/@IK2026bot
            ProcessStartInfo psi = new ProcessStartInfo
            {
                FileName = "https://ble.ir/@IK2026bot",
                UseShellExecute = true  // مهم: از مرورگر پیش‌فرض استفاده می‌کند
            };
            Process.Start(psi);
        }
    }
}