using DevExpress.Skins;
using DevExpress.UserSkins;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using IranKavosh.Layered.WinUI.Forms;
using MyCom.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;
using static Microsoft.WindowsAPICodePack.Shell.PropertySystem.SystemProperties.System;

namespace IranKavosh.Layered.WinUI
{
    public partial class FrmMain : DevExpress.XtraBars.FluentDesignSystem.FluentDesignForm
    {
        public FrmMain()
        {
            InitializeComponent();
            //DevExpress.LookAndFeel.UserLookAndFeel.Default.SetSkinStyle("WMX");
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            this.LookAndFeel.SkinName = "WXI";
            this.LookAndFeel.UseDefaultLookAndFeel = false;
            this.LookAndFeel.UseWindowsXPTheme = false;
        }

        private void fluentDesignFormContainer1_Click(object sender, EventArgs e)
        {

        }

        private void mnuMangeVideo_CommandChanged(object sender, EventArgs e)
        {

        }

        private void fluentDesignFormControl1_Click(object sender, EventArgs e)
        {

        }

        private void mnuMangeVideo_Click(object sender, EventArgs e)
        {
          
           

        }
        private void LoadForm(XtraForm frm)
        {
            // fluentDesignFormContainer1.Controls.Clear();
            BonusSkins.Register();
            SkinManager.EnableFormSkins();
            SkinManager.EnableMdiFormSkins();
            frm.LookAndFeel.SkinName = "WXI";
            frm.LookAndFeel.UseDefaultLookAndFeel = false;
            frm.LookAndFeel.UseWindowsXPTheme = false;


            frm.TopLevel = false;
            
            //form.FormBorderStyle = FormBorderStyle.None;
            //form.Dock = DockStyle.Fill;
            fluentDesignFormContainer1.Controls.Add(frm);
            frm.Show();
        }
    }
}
