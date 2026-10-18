namespace IranKavosh.Layered.WinUI
{
    partial class FrmMain2
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            ribbon = new DevExpress.XtraBars.Ribbon.RibbonControl();
            barVideoManage = new DevExpress.XtraBars.BarButtonItem();
            barVideoShow = new DevExpress.XtraBars.BarButtonItem();
            barPerson = new DevExpress.XtraBars.BarButtonItem();
            barFund = new DevExpress.XtraBars.BarButtonItem();
            barFactorSell = new DevExpress.XtraBars.BarButtonItem();
            barPriceList = new DevExpress.XtraBars.BarButtonItem();
            barEditItem1 = new DevExpress.XtraBars.BarEditItem();
            repositoryItemMarqueeProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar();
            barBtnProduct = new DevExpress.XtraBars.BarButtonItem();
            btnFactorDor = new DevExpress.XtraBars.BarButtonItem();
            barDefAccount = new DevExpress.XtraBars.BarButtonItem();
            ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            pnlSrvStatus = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ribbonPage3 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            repositoryItemButtonEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            repositoryItemProgressBar1 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            repositoryItemProgressBar2 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            repositoryItemProgressBar3 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            repositoryItemProgressBar4 = new DevExpress.XtraEditors.Repository.RepositoryItemProgressBar();
            ribbonStatusBar = new DevExpress.XtraBars.Ribbon.RibbonStatusBar();
            toastNotificationsManager1 = new DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager(components);
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            kavoshGrid1 = new MyCom.Object.KavoshGrid(components);
            bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)ribbon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemMarqueeProgressBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)toastNotificationsManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)kavoshGrid1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bandedGridView1).BeginInit();
            SuspendLayout();
            // 
            // ribbon
            // 
            ribbon.ExpandCollapseItem.Id = 0;
            ribbon.Items.AddRange(new DevExpress.XtraBars.BarItem[] { ribbon.ExpandCollapseItem, barVideoManage, barVideoShow, barPerson, barFund, barFactorSell, barPriceList, barEditItem1, barBtnProduct, btnFactorDor, barDefAccount });
            ribbon.Location = new Point(0, 0);
            ribbon.MaxItemId = 16;
            ribbon.Name = "ribbon";
            ribbon.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] { ribbonPage1, ribbonPage2, ribbonPage3 });
            ribbon.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { repositoryItemButtonEdit1, repositoryItemProgressBar1, repositoryItemProgressBar2, repositoryItemProgressBar3, repositoryItemProgressBar4, repositoryItemMarqueeProgressBar1 });
            ribbon.Size = new Size(967, 170);
            ribbon.StatusBar = ribbonStatusBar;
            ribbon.Click += ribbon_Click;
            // 
            // barVideoManage
            // 
            barVideoManage.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barVideoManage.Caption = "مدیریت ویدیوها";
            barVideoManage.Id = 1;
            barVideoManage.ImageOptions.SvgImage = Properties.Resources.glyph_video;
            barVideoManage.LargeWidth = 100;
            barVideoManage.Name = "barVideoManage";
            barVideoManage.ItemClick += barButtonItem1_ItemClick;
            // 
            // barVideoShow
            // 
            barVideoShow.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barVideoShow.Caption = "پلی لیست (F5)";
            barVideoShow.Id = 2;
            barVideoShow.ImageOptions.Image = Properties.Resources.multimedia;
            barVideoShow.ImageOptions.LargeImage = Properties.Resources.multimedia;
            barVideoShow.ItemShortcut = new DevExpress.XtraBars.BarShortcut(Keys.F5);
            barVideoShow.LargeWidth = 100;
            barVideoShow.Name = "barVideoShow";
            barVideoShow.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.False;
            barVideoShow.ItemClick += barVideoShow_ItemClick;
            // 
            // barPerson
            // 
            barPerson.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barPerson.Caption = "مشتری";
            barPerson.Id = 3;
            barPerson.ImageOptions.SvgImage = Properties.Resources.bo_employee;
            barPerson.LargeWidth = 70;
            barPerson.Name = "barPerson";
            barPerson.ItemClick += barPerson_ItemClick;
            // 
            // barFund
            // 
            barFund.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barFund.Caption = "مدیریت صندوق";
            barFund.Id = 4;
            barFund.ImageOptions.SvgImage = Properties.Resources.business_cash;
            barFund.LargeWidth = 100;
            barFund.Name = "barFund";
            barFund.ItemClick += barFund_ItemClick;
            // 
            // barFactorSell
            // 
            barFactorSell.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barFactorSell.Caption = "فاکتور فروش (F2)";
            barFactorSell.Id = 5;
            barFactorSell.ImageOptions.SvgImage = Properties.Resources.thisweek;
            barFactorSell.ItemShortcut = new DevExpress.XtraBars.BarShortcut(Keys.F2);
            barFactorSell.LargeWidth = 120;
            barFactorSell.Name = "barFactorSell";
            barFactorSell.ShowItemShortcut = DevExpress.Utils.DefaultBoolean.False;
            barFactorSell.ItemClick += barFactorSell_ItemClick;
            // 
            // barPriceList
            // 
            barPriceList.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barPriceList.Caption = "نرخ نامه";
            barPriceList.Id = 6;
            barPriceList.ImageOptions.SvgImage = Properties.Resources.bo_invoice;
            barPriceList.LargeWidth = 70;
            barPriceList.Name = "barPriceList";
            barPriceList.ItemClick += barPriceList_ItemClick;
            // 
            // barEditItem1
            // 
            barEditItem1.Edit = repositoryItemMarqueeProgressBar1;
            barEditItem1.Id = 12;
            barEditItem1.Name = "barEditItem1";
            // 
            // repositoryItemMarqueeProgressBar1
            // 
            repositoryItemMarqueeProgressBar1.Name = "repositoryItemMarqueeProgressBar1";
            // 
            // barBtnProduct
            // 
            barBtnProduct.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barBtnProduct.Caption = "محصولات و خدمات";
            barBtnProduct.Id = 13;
            barBtnProduct.ImageOptions.SvgImage = Properties.Resources.productquickshippments;
            barBtnProduct.LargeWidth = 115;
            barBtnProduct.Name = "barBtnProduct";
            barBtnProduct.ItemClick += barBtnProduct_ItemClick;
            // 
            // btnFactorDor
            // 
            btnFactorDor.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            btnFactorDor.Caption = "فاکتور چاپی";
            btnFactorDor.Id = 14;
            btnFactorDor.ImageOptions.Image = Properties.Resources.insert_16x16;
            btnFactorDor.ImageOptions.LargeImage = Properties.Resources.insert_32x32;
            btnFactorDor.LargeWidth = 80;
            btnFactorDor.Name = "btnFactorDor";
            btnFactorDor.ItemClick += btnFactorDor_ItemClick;
            // 
            // barDefAccount
            // 
            barDefAccount.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            barDefAccount.Caption = "حساب مشتری (F9)";
            barDefAccount.Id = 15;
            barDefAccount.ImageOptions.SvgImage = Properties.Resources.bo_person;
            barDefAccount.ItemShortcut = new DevExpress.XtraBars.BarShortcut(Keys.F9);
            barDefAccount.LargeWidth = 120;
            barDefAccount.Name = "barDefAccount";
            barDefAccount.ItemClick += barDefAccount_ItemClick;
            // 
            // ribbonPage1
            // 
            ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup1 });
            ribbonPage1.Name = "ribbonPage1";
            ribbonPage1.Text = "اطلاعات پایه";
            // 
            // ribbonPageGroup1
            // 
            ribbonPageGroup1.ItemLinks.Add(barBtnProduct);
            ribbonPageGroup1.ItemLinks.Add(barPerson);
            ribbonPageGroup1.ItemLinks.Add(barFactorSell);
            ribbonPageGroup1.ItemLinks.Add(barPriceList);
            ribbonPageGroup1.ItemLinks.Add(btnFactorDor);
            ribbonPageGroup1.ItemLinks.Add(barDefAccount);
            ribbonPageGroup1.Name = "ribbonPageGroup1";
            // 
            // ribbonPage2
            // 
            ribbonPage2.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup2, pnlSrvStatus });
            ribbonPage2.Name = "ribbonPage2";
            ribbonPage2.Text = "ویدیو";
            // 
            // ribbonPageGroup2
            // 
            ribbonPageGroup2.ItemLinks.Add(barVideoManage);
            ribbonPageGroup2.ItemLinks.Add(barVideoShow);
            ribbonPageGroup2.ItemLinks.Add(barFund);
            ribbonPageGroup2.Name = "ribbonPageGroup2";
            ribbonPageGroup2.Text = "نمایش";
            // 
            // pnlSrvStatus
            // 
            pnlSrvStatus.ItemLinks.Add(barEditItem1);
            pnlSrvStatus.Name = "pnlSrvStatus";
            pnlSrvStatus.Text = "وضعیت سرور";
            // 
            // ribbonPage3
            // 
            ribbonPage3.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] { ribbonPageGroup3 });
            ribbonPage3.Name = "ribbonPage3";
            ribbonPage3.Text = "گزارشات";
            ribbonPage3.Visible = false;
            // 
            // ribbonPageGroup3
            // 
            ribbonPageGroup3.Name = "ribbonPageGroup3";
            ribbonPageGroup3.Text = "گزارش فیلم ها";
            // 
            // repositoryItemButtonEdit1
            // 
            repositoryItemButtonEdit1.AutoHeight = false;
            repositoryItemButtonEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton() });
            repositoryItemButtonEdit1.Name = "repositoryItemButtonEdit1";
            // 
            // repositoryItemProgressBar1
            // 
            repositoryItemProgressBar1.Name = "repositoryItemProgressBar1";
            // 
            // repositoryItemProgressBar2
            // 
            repositoryItemProgressBar2.Name = "repositoryItemProgressBar2";
            // 
            // repositoryItemProgressBar3
            // 
            repositoryItemProgressBar3.Name = "repositoryItemProgressBar3";
            // 
            // repositoryItemProgressBar4
            // 
            repositoryItemProgressBar4.Name = "repositoryItemProgressBar4";
            repositoryItemProgressBar4.ShowTitle = true;
            // 
            // ribbonStatusBar
            // 
            ribbonStatusBar.Location = new Point(0, 545);
            ribbonStatusBar.Name = "ribbonStatusBar";
            ribbonStatusBar.Ribbon = ribbon;
            ribbonStatusBar.Size = new Size(967, 31);
            // 
            // toastNotificationsManager1
            // 
            toastNotificationsManager1.ApplicationId = "e3db69df-0be3-433a-b973-8e7874935156";
            toastNotificationsManager1.Notifications.AddRange(new DevExpress.XtraBars.ToastNotifications.IToastNotificationProperties[] { new DevExpress.XtraBars.ToastNotifications.ToastNotification("918e99bd-1ae8-4552-8453-fa536c342408", Properties.Resources.errorbarsnone_32x32, "Pellentesque lacinia tellus eget volutpat", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.", DevExpress.XtraBars.ToastNotifications.ToastNotificationSound.IM, DevExpress.XtraBars.ToastNotifications.ToastNotificationDuration.Default, DevExpress.XtraBars.ToastNotifications.ToastNotificationTemplate.ImageAndText01) });
            // 
            // simpleButton1
            // 
            simpleButton1.Location = new Point(12, 176);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(103, 30);
            simpleButton1.TabIndex = 3;
            simpleButton1.Text = "simpleButton1";
            simpleButton1.Visible = false;
            simpleButton1.Click += simpleButton1_Click_5;
            // 
            // kavoshGrid1
            // 
            kavoshGrid1.Dock = DockStyle.Fill;
            kavoshGrid1.Location = new Point(0, 170);
            kavoshGrid1.MainView = bandedGridView1;
            kavoshGrid1.MenuManager = ribbon;
            kavoshGrid1.Name = "kavoshGrid1";
            kavoshGrid1.Size = new Size(967, 375);
            kavoshGrid1.TabIndex = 7;
            kavoshGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { bandedGridView1 });
            kavoshGrid1.Visible = false;
            // 
            // bandedGridView1
            // 
            bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            bandedGridView1.GridControl = kavoshGrid1;
            bandedGridView1.Name = "bandedGridView1";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // FrmMain2
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(967, 576);
            Controls.Add(kavoshGrid1);
            Controls.Add(simpleButton1);
            Controls.Add(ribbon);
            Controls.Add(ribbonStatusBar);
            IsMdiContainer = true;
            Name = "FrmMain2";
            Ribbon = ribbon;
            RightToLeft = RightToLeft.Yes;
            StatusBar = ribbonStatusBar;
            Text = "فنی مهندسی ایران کاوش";
            FormClosing += FrmMain2_FormClosing;
            Load += FrmMain2_Load;
            ((System.ComponentModel.ISupportInitialize)ribbon).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemMarqueeProgressBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemButtonEdit1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar1).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar2).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar3).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemProgressBar4).EndInit();
            ((System.ComponentModel.ISupportInitialize)toastNotificationsManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)kavoshGrid1).EndInit();
            ((System.ComponentModel.ISupportInitialize)bandedGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraBars.Ribbon.RibbonControl ribbon;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.Ribbon.RibbonStatusBar ribbonStatusBar;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.BarButtonItem barVideoManage;
        private DevExpress.XtraBars.BarButtonItem barVideoShow;
        private DevExpress.XtraBars.BarButtonItem barPerson;
        private DevExpress.XtraBars.BarButtonItem barFund;
        private DevExpress.XtraBars.BarButtonItem barFactorSell;
        private DevExpress.XtraBars.BarButtonItem barPriceList;
        private DevExpress.XtraBars.ToastNotifications.ToastNotificationsManager toastNotificationsManager1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup pnlSrvStatus;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repositoryItemButtonEdit1;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar3;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar2;
        private DevExpress.XtraEditors.Repository.RepositoryItemProgressBar repositoryItemProgressBar4;
        private DevExpress.XtraBars.BarEditItem barEditItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemMarqueeProgressBar repositoryItemMarqueeProgressBar1;
        private DevExpress.XtraBars.BarButtonItem barBtnProduct;
        private DevExpress.XtraBars.BarButtonItem btnFactorDor;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage3;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem barDefAccount;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private MyCom.Object.KavoshGrid kavoshGrid1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}