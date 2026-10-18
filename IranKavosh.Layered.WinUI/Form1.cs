using IranKavosh.Layered.Service.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace IranKavosh.Layered.WinUI
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();

        }
        // private PersonalHttpService _personalService;

        private void Form1_Load(object sender, EventArgs e)
        {


            //try
            //{
            //    var client = new HttpClient();
            //    client.BaseAddress = new Uri("http://192.168.1.160:1212/");
            //    var response =  client.GetAsync("api/Personal").Result;
            //    var json =  response.Content.ReadAsStringAsync();
            //    MessageBox.Show(json.Result);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show($"خطا: {ex.Message}");
            //}





        }

        private async void simpleButton1_Click(object sender, EventArgs e)
        {
           // var httpClient = new HttpClient(); // BaseAddress در HttpServiceBase تنظیم می‌شه
           // var _personalService = await new PersonalHttpService(httpClient).GetAsync();
            //MessageBox.Show(_personalService.Count.ToString());
        }
    }
}
