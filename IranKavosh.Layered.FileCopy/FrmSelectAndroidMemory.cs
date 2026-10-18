using DevExpress.XtraEditors;
using MediaDevices;
using MyCom.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static MyCom.Class.ClassFileManage;

namespace IranKavosh.Layered.FileCopy
{
    public partial class FrmSelectAndroidMemory : DevExpress.XtraEditors.XtraForm
    {
        private readonly KeyValuePair<List<ModelFile>, ulong> _dataCopy;
        public FrmSelectAndroidMemory(KeyValuePair<List<ModelFile>, ulong> data)
        {
            InitializeComponent();
            //var getSplit = data.Split(" ");
            //var encryptedText = getSplit[0];
            //var getFile = ClassEncryptDecrypt.Decrypt(encryptedText, "IranKavosh123456");
            //_dataCopy = JsonConvert.DeserializeObject<KeyValuePair<List<ModelFile>, ulong>>(getFile);
            _dataCopy = data;
        }

        private void FrmSelectAndroidMemory_Load(object sender, EventArgs e)
        {
            LoadAndroidDevice();
        }
        MediaDevice? getDevice;
        public void LoadAndroidDevice()
        {
            try
            {
                var getUsb = ClassUsbDriveInfoProvider.GetUsbDrives();

                getDevice = ClsDeviceAndroid.ConnectFirstDevice(getUsb.Select(s => s.VolumeLabel).ToList());
                if (getDevice is not null)
                {
                    label2.Text = getDevice.FriendlyName;
                    lblModel.Text = getDevice.Model;
                    barCharge.Position = getDevice.PowerLevel;
                    List<MediaDriveInfo> getDir = null;
                    try
                    {
                        getDir = getDevice.GetDrives().ToList();
                        //getDevice.im
                    }
                    catch (Exception e)
                    {
                        label2.Text = "";
                        getDevice.Disconnect();
                        getDevice.Dispose();
                        barCharge.Position = 0;
                        lblModel.Text = @"(باز بودن قفل را بررسی کنید) خطا";
                    }
                    
                    if (getDir != null && getDir.Count == 1)
                    {
                        lblFree1.Text = ((float)getDir.First().AvailableFreeSpace / 1024 / 1024 / 1024).ToString("N2") + @" GB";
                        lblName1.Text = getDir.First().Name;
                        if (getDir.First().AvailableFreeSpace < (long)_dataCopy.Value)
                        {
                            btnHDD1.Text = @"کمبود حافظه";
                            btnHDD1.Enabled = false;
                        }

                        btnHDD2.Visible = lblFree2.Visible = lblName2.Visible = label3.Visible = false;
                    }
                    else if (getDir != null && getDir.Count > 1)
                    {
                        lblFree1.Text = ((float)getDir[0].AvailableFreeSpace / 1024 / 1024 / 1024).ToString("N2") + @" GB";
                        lblName1.Text = getDir[0].Name;

                        lblFree2.Text = ((float)getDir[1].AvailableFreeSpace / 1024 / 1024 / 1024).ToString("N2") + @" GB";
                        lblName2.Text = getDir[1].Name;

                        if (getDir[0].AvailableFreeSpace < (long)_dataCopy.Value)
                        {
                            btnHDD1.Text = @"کمبود حافظه";
                            btnHDD1.Enabled = false;
                        }

                        if (getDir[1].AvailableFreeSpace < (long)_dataCopy.Value)
                        {
                            btnHDD2.Text = @"کمبود حافظه";
                            btnHDD2.Enabled = false;
                        }
                        btnHDD2.Visible = lblFree2.Visible = lblName2.Visible = label3.Visible = true;
                    }
                }
                else
                {
                    label2.Text = "";
                    barCharge.Position = 0;
                    lblModel.Text = @"(باز بودن قفل را بررسی کنید) خطا";
                }
            }
            catch (Exception e)
            {
                label2.Text = "";
                barCharge.Position = 0;
                lblModel.Text = @"(باز بودن قفل را بررسی کنید) خطا";
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadAndroidDevice();
        }

        private void btnHDD1_Click(object sender, EventArgs e)
        {
            FrmCopyFile copyFile = new FrmCopyFile(_dataCopy.Key, getDevice, lblName1.Text);
            copyFile.Show();
            Hide();
        }

        private void btnHDD2_Click(object sender, EventArgs e)
        {
            FrmCopyFile copyFile = new FrmCopyFile(_dataCopy.Key, getDevice, lblName2.Text);
            copyFile.Show();
            Hide();
        }
    }
}