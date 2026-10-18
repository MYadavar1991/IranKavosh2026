namespace IranKavosh.Layered.FileCopy {
    partial class FrmCopyFile {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            lblFileAll = new Label();
            label3 = new Label();
            lblFileCopied = new Label();
            label5 = new Label();
            lblFileRemian = new Label();
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            panel1 = new Panel();
            lblNow = new Label();
            label21 = new Label();
            lblEnd = new Label();
            label19 = new Label();
            lblStart = new Label();
            label17 = new Label();
            lblTo = new Label();
            label15 = new Label();
            lstRemain = new DevExpress.XtraEditors.ListBoxControl();
            label14 = new Label();
            label13 = new Label();
            lstCopied = new DevExpress.XtraEditors.ListBoxControl();
            prgCopy1 = new DevExpress.XtraEditors.ProgressBarControl();
            prgCopy2 = new DevExpress.XtraEditors.ProgressBarControl();
            lblSizeRemian = new Label();
            lblSizeCopied = new Label();
            lblSizeAll = new Label();
            label9 = new Label();
            label8 = new Label();
            label7 = new Label();
            tabPane1 = new DevExpress.XtraBars.Navigation.TabPane();
            btnMute = new DevExpress.XtraEditors.SimpleButton();
            tabNavigationPage1 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            tabNavigationPage2 = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            groupControl2 = new DevExpress.XtraEditors.GroupControl();
            layFund = new MyCom.Object.dataLayout();
            groupControl1 = new DevExpress.XtraEditors.GroupControl();
            layPerson = new MyCom.Object.dataLayout();
            btnAddFile = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lstRemain).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lstCopied).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prgCopy1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)prgCopy2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tabPane1).BeginInit();
            tabPane1.SuspendLayout();
            tabNavigationPage1.SuspendLayout();
            tabNavigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupControl2).BeginInit();
            groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)groupControl1).BeginInit();
            groupControl1.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            tablePanel1.SetColumn(label1, 3);
            label1.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(322, 13);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label1, 0);
            label1.Size = new Size(68, 20);
            label1.TabIndex = 1;
            label1.Text = "تعداد فایل:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFileAll
            // 
            tablePanel1.SetColumn(lblFileAll, 2);
            lblFileAll.Dock = DockStyle.Fill;
            lblFileAll.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFileAll.Location = new Point(219, 10);
            lblFileAll.Name = "lblFileAll";
            lblFileAll.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(lblFileAll, 0);
            lblFileAll.Size = new Size(97, 26);
            lblFileAll.TabIndex = 1;
            lblFileAll.Text = "0";
            lblFileAll.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            label3.AutoSize = true;
            tablePanel1.SetColumn(label3, 3);
            label3.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label3.Location = new Point(322, 39);
            label3.Name = "label3";
            label3.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label3, 1);
            label3.Size = new Size(60, 20);
            label3.TabIndex = 1;
            label3.Text = "کپی شده:";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFileCopied
            // 
            tablePanel1.SetColumn(lblFileCopied, 2);
            lblFileCopied.Dock = DockStyle.Fill;
            lblFileCopied.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFileCopied.Location = new Point(219, 36);
            lblFileCopied.Name = "lblFileCopied";
            lblFileCopied.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(lblFileCopied, 1);
            lblFileCopied.Size = new Size(97, 26);
            lblFileCopied.TabIndex = 1;
            lblFileCopied.Text = "0";
            lblFileCopied.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            label5.AutoSize = true;
            tablePanel1.SetColumn(label5, 3);
            label5.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label5.Location = new Point(322, 65);
            label5.Name = "label5";
            label5.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label5, 2);
            label5.Size = new Size(60, 20);
            label5.TabIndex = 1;
            label5.Text = "باقیمانده:";
            label5.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFileRemian
            // 
            tablePanel1.SetColumn(lblFileRemian, 2);
            lblFileRemian.Dock = DockStyle.Fill;
            lblFileRemian.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblFileRemian.Location = new Point(219, 62);
            lblFileRemian.Name = "lblFileRemian";
            lblFileRemian.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(lblFileRemian, 2);
            lblFileRemian.Size = new Size(97, 26);
            lblFileRemian.TabIndex = 1;
            lblFileRemian.Text = "0";
            lblFileRemian.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tablePanel1
            // 
            tablePanel1.Appearance.BackColor = Color.LightYellow;
            tablePanel1.Appearance.Options.UseBackColor = true;
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 25F) });
            tablePanel1.Controls.Add(btnCancel);
            tablePanel1.Controls.Add(panel1);
            tablePanel1.Controls.Add(lblTo);
            tablePanel1.Controls.Add(label15);
            tablePanel1.Controls.Add(lstRemain);
            tablePanel1.Controls.Add(label14);
            tablePanel1.Controls.Add(label13);
            tablePanel1.Controls.Add(lstCopied);
            tablePanel1.Controls.Add(prgCopy1);
            tablePanel1.Controls.Add(prgCopy2);
            tablePanel1.Controls.Add(lblSizeRemian);
            tablePanel1.Controls.Add(lblSizeCopied);
            tablePanel1.Controls.Add(lblSizeAll);
            tablePanel1.Controls.Add(label9);
            tablePanel1.Controls.Add(label8);
            tablePanel1.Controls.Add(label7);
            tablePanel1.Controls.Add(label1);
            tablePanel1.Controls.Add(lblFileRemian);
            tablePanel1.Controls.Add(label3);
            tablePanel1.Controls.Add(lblFileCopied);
            tablePanel1.Controls.Add(label5);
            tablePanel1.Controls.Add(lblFileAll);
            tablePanel1.Dock = DockStyle.Fill;
            tablePanel1.Location = new Point(0, 0);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 30F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 22F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 140F) });
            tablePanel1.Size = new Size(432, 402);
            tablePanel1.TabIndex = 2;
            tablePanel1.UseSkinIndents = true;
            // 
            // btnCancel
            // 
            btnCancel.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnCancel.Appearance.Options.UseFont = true;
            tablePanel1.SetColumn(btnCancel, 0);
            btnCancel.Dock = DockStyle.Fill;
            btnCancel.ImageOptions.SvgImage = Properties.Resources.actions_forbid;
            btnCancel.ImageOptions.SvgImageSize = new Size(20, 20);
            btnCancel.Location = new Point(13, 90);
            btnCancel.Name = "btnCancel";
            tablePanel1.SetRow(btnCancel, 3);
            btnCancel.Size = new Size(99, 22);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "انصراف";
            btnCancel.Click += btnCancel_Click;
            // 
            // panel1
            // 
            tablePanel1.SetColumn(panel1, 0);
            tablePanel1.SetColumnSpan(panel1, 4);
            panel1.Controls.Add(lblNow);
            panel1.Controls.Add(label21);
            panel1.Controls.Add(lblEnd);
            panel1.Controls.Add(label19);
            panel1.Controls.Add(lblStart);
            panel1.Controls.Add(label17);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(13, 116);
            panel1.Name = "panel1";
            tablePanel1.SetRow(panel1, 4);
            panel1.Size = new Size(407, 22);
            panel1.TabIndex = 15;
            // 
            // lblNow
            // 
            lblNow.Dock = DockStyle.Right;
            lblNow.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblNow.Location = new Point(9, 0);
            lblNow.Name = "lblNow";
            lblNow.RightToLeft = RightToLeft.Yes;
            lblNow.Size = new Size(71, 22);
            lblNow.TabIndex = 19;
            lblNow.Text = "00:00:00";
            lblNow.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label21
            // 
            label21.Dock = DockStyle.Right;
            label21.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label21.Location = new Point(80, 0);
            label21.Name = "label21";
            label21.RightToLeft = RightToLeft.Yes;
            label21.Size = new Size(99, 22);
            label21.TabIndex = 18;
            label21.Text = "سپری شده:";
            label21.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblEnd
            // 
            lblEnd.Dock = DockStyle.Right;
            lblEnd.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblEnd.Location = new Point(179, 0);
            lblEnd.Name = "lblEnd";
            lblEnd.RightToLeft = RightToLeft.Yes;
            lblEnd.Size = new Size(71, 22);
            lblEnd.TabIndex = 17;
            lblEnd.Text = "00:00:00";
            lblEnd.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label19
            // 
            label19.Dock = DockStyle.Right;
            label19.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label19.Location = new Point(250, 0);
            label19.Name = "label19";
            label19.RightToLeft = RightToLeft.Yes;
            label19.Size = new Size(46, 22);
            label19.TabIndex = 16;
            label19.Text = "پایان:";
            label19.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblStart
            // 
            lblStart.Dock = DockStyle.Right;
            lblStart.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblStart.Location = new Point(296, 0);
            lblStart.Name = "lblStart";
            lblStart.RightToLeft = RightToLeft.Yes;
            lblStart.Size = new Size(65, 22);
            lblStart.TabIndex = 15;
            lblStart.Text = "00:00:00";
            lblStart.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label17
            // 
            label17.Dock = DockStyle.Right;
            label17.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label17.Location = new Point(361, 0);
            label17.Name = "label17";
            label17.RightToLeft = RightToLeft.Yes;
            label17.Size = new Size(46, 22);
            label17.TabIndex = 14;
            label17.Text = "شروع:";
            label17.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblTo
            // 
            tablePanel1.SetColumn(lblTo, 1);
            tablePanel1.SetColumnSpan(lblTo, 2);
            lblTo.Dock = DockStyle.Fill;
            lblTo.Font = new Font("Samim FD", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblTo.ForeColor = Color.LightCoral;
            lblTo.Location = new Point(117, 88);
            lblTo.Name = "lblTo";
            lblTo.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(lblTo, 3);
            lblTo.Size = new Size(199, 26);
            lblTo.TabIndex = 14;
            lblTo.Text = "0";
            lblTo.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label15
            // 
            label15.AutoSize = true;
            tablePanel1.SetColumn(label15, 3);
            label15.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label15.Location = new Point(322, 91);
            label15.Name = "label15";
            label15.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label15, 3);
            label15.Size = new Size(43, 20);
            label15.TabIndex = 13;
            label15.Text = "آدرس:";
            label15.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lstRemain
            // 
            lstRemain.Appearance.Font = new Font("Samim FD", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lstRemain.Appearance.Options.UseFont = true;
            tablePanel1.SetColumn(lstRemain, 0);
            tablePanel1.SetColumnSpan(lstRemain, 2);
            lstRemain.Dock = DockStyle.Fill;
            lstRemain.Location = new Point(13, 224);
            lstRemain.Name = "lstRemain";
            lstRemain.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(lstRemain, 8);
            lstRemain.Size = new Size(202, 165);
            lstRemain.TabIndex = 12;
            // 
            // label14
            // 
            tablePanel1.SetColumn(label14, 0);
            tablePanel1.SetColumnSpan(label14, 2);
            label14.Dock = DockStyle.Fill;
            label14.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label14.ForeColor = Color.Red;
            label14.Location = new Point(14, 200);
            label14.Name = "label14";
            label14.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label14, 7);
            label14.Size = new Size(200, 22);
            label14.TabIndex = 11;
            label14.Text = "فیلم های باقیمانده";
            label14.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label13
            // 
            tablePanel1.SetColumn(label13, 2);
            tablePanel1.SetColumnSpan(label13, 2);
            label13.Dock = DockStyle.Fill;
            label13.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label13.ForeColor = Color.LimeGreen;
            label13.Location = new Point(219, 200);
            label13.Name = "label13";
            label13.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label13, 7);
            label13.Size = new Size(200, 22);
            label13.TabIndex = 10;
            label13.Text = "فیلم های کپی شده";
            label13.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lstCopied
            // 
            lstCopied.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lstCopied.Appearance.Options.UseFont = true;
            tablePanel1.SetColumn(lstCopied, 2);
            tablePanel1.SetColumnSpan(lstCopied, 2);
            lstCopied.Dock = DockStyle.Fill;
            lstCopied.Location = new Point(218, 224);
            lstCopied.Name = "lstCopied";
            lstCopied.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(lstCopied, 8);
            lstCopied.Size = new Size(202, 165);
            lstCopied.TabIndex = 9;
            // 
            // prgCopy1
            // 
            tablePanel1.SetColumn(prgCopy1, 0);
            tablePanel1.SetColumnSpan(prgCopy1, 4);
            prgCopy1.Dock = DockStyle.Fill;
            prgCopy1.Location = new Point(13, 142);
            prgCopy1.Name = "prgCopy1";
            prgCopy1.Properties.AllowAnimationOnValueChanged = DevExpress.Utils.DefaultBoolean.True;
            prgCopy1.Properties.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            prgCopy1.Properties.FlowAnimationEnabled = true;
            prgCopy1.Properties.LookAndFeel.SkinMaskColor = Color.Aqua;
            prgCopy1.Properties.LookAndFeel.SkinName = "Money Twins";
            prgCopy1.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            prgCopy1.Properties.ShowTitle = true;
            tablePanel1.SetRow(prgCopy1, 5);
            prgCopy1.Size = new Size(407, 26);
            prgCopy1.TabIndex = 8;
            // 
            // prgCopy2
            // 
            tablePanel1.SetColumn(prgCopy2, 0);
            tablePanel1.SetColumnSpan(prgCopy2, 4);
            prgCopy2.Dock = DockStyle.Fill;
            prgCopy2.Location = new Point(13, 172);
            prgCopy2.Name = "prgCopy2";
            prgCopy2.Properties.AllowAnimationOnValueChanged = DevExpress.Utils.DefaultBoolean.True;
            prgCopy2.Properties.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            prgCopy2.Properties.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            prgCopy2.Properties.FlowAnimationEnabled = true;
            prgCopy2.Properties.LookAndFeel.SkinName = "Money Twins";
            prgCopy2.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            prgCopy2.Properties.ShowTitle = true;
            tablePanel1.SetRow(prgCopy2, 6);
            prgCopy2.ShowProgressInTaskBar = true;
            prgCopy2.Size = new Size(407, 26);
            prgCopy2.TabIndex = 6;
            // 
            // lblSizeRemian
            // 
            tablePanel1.SetColumn(lblSizeRemian, 0);
            lblSizeRemian.Dock = DockStyle.Fill;
            lblSizeRemian.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSizeRemian.Location = new Point(14, 62);
            lblSizeRemian.Name = "lblSizeRemian";
            tablePanel1.SetRow(lblSizeRemian, 2);
            lblSizeRemian.Size = new Size(97, 26);
            lblSizeRemian.TabIndex = 3;
            lblSizeRemian.Text = "0";
            lblSizeRemian.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSizeCopied
            // 
            tablePanel1.SetColumn(lblSizeCopied, 0);
            lblSizeCopied.Dock = DockStyle.Fill;
            lblSizeCopied.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSizeCopied.Location = new Point(14, 36);
            lblSizeCopied.Name = "lblSizeCopied";
            tablePanel1.SetRow(lblSizeCopied, 1);
            lblSizeCopied.Size = new Size(97, 26);
            lblSizeCopied.TabIndex = 3;
            lblSizeCopied.Text = "0";
            lblSizeCopied.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblSizeAll
            // 
            tablePanel1.SetColumn(lblSizeAll, 0);
            lblSizeAll.Dock = DockStyle.Fill;
            lblSizeAll.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblSizeAll.Location = new Point(14, 10);
            lblSizeAll.Name = "lblSizeAll";
            lblSizeAll.RightToLeft = RightToLeft.No;
            tablePanel1.SetRow(lblSizeAll, 0);
            lblSizeAll.Size = new Size(97, 26);
            lblSizeAll.TabIndex = 3;
            lblSizeAll.Text = "0";
            lblSizeAll.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label9
            // 
            label9.AutoSize = true;
            tablePanel1.SetColumn(label9, 1);
            label9.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label9.Location = new Point(117, 65);
            label9.Name = "label9";
            label9.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label9, 2);
            label9.Size = new Size(90, 20);
            label9.TabIndex = 5;
            label9.Text = "حجم باقیمانده:";
            label9.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label8
            // 
            label8.AutoSize = true;
            tablePanel1.SetColumn(label8, 1);
            label8.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label8.Location = new Point(117, 39);
            label8.Name = "label8";
            label8.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label8, 1);
            label8.Size = new Size(90, 20);
            label8.TabIndex = 4;
            label8.Text = "حجم کپی شده:";
            label8.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label7
            // 
            label7.AutoSize = true;
            tablePanel1.SetColumn(label7, 1);
            label7.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label7.Location = new Point(117, 13);
            label7.Name = "label7";
            label7.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label7, 0);
            label7.Size = new Size(59, 20);
            label7.TabIndex = 3;
            label7.Text = "حجم کل:";
            label7.TextAlign = ContentAlignment.MiddleCenter;
            label7.Click += label7_Click;
            // 
            // tabPane1
            // 
            tabPane1.Appearance.BackColor = Color.LightBlue;
            tabPane1.Appearance.Options.UseBackColor = true;
            tabPane1.Controls.Add(btnMute);
            tabPane1.Controls.Add(tabNavigationPage1);
            tabPane1.Controls.Add(tabNavigationPage2);
            tabPane1.Dock = DockStyle.Fill;
            tabPane1.Location = new Point(0, 0);
            tabPane1.Name = "tabPane1";
            tabPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { tabNavigationPage1, tabNavigationPage2 });
            tabPane1.RegularSize = new Size(432, 435);
            tabPane1.RightToLeft = RightToLeft.Yes;
            tabPane1.SelectedPage = tabNavigationPage1;
            tabPane1.Size = new Size(432, 435);
            tabPane1.TabIndex = 15;
            tabPane1.Text = "tabPane1";
            tabPane1.SelectedPageChanged += tabPane1_SelectedPageChanged;
            // 
            // btnMute
            // 
            btnMute.ImageOptions.SvgImage = Properties.Resources.Mute;
            btnMute.ImageOptions.SvgImageSize = new Size(25, 25);
            btnMute.Location = new Point(137, 3);
            btnMute.Name = "btnMute";
            btnMute.Size = new Size(34, 28);
            btnMute.TabIndex = 2;
            btnMute.Click += btnMute_Click;
            // 
            // tabNavigationPage1
            // 
            tabNavigationPage1.Caption = "عملیات کپی";
            tabNavigationPage1.Controls.Add(tablePanel1);
            tabNavigationPage1.Name = "tabNavigationPage1";
            tabNavigationPage1.RightToLeft = RightToLeft.No;
            tabNavigationPage1.Size = new Size(432, 402);
            // 
            // tabNavigationPage2
            // 
            tabNavigationPage2.Caption = "صورت حساب";
            tabNavigationPage2.Controls.Add(groupControl2);
            tabNavigationPage2.Controls.Add(groupControl1);
            tabNavigationPage2.Name = "tabNavigationPage2";
            tabNavigationPage2.Size = new Size(432, 402);
            // 
            // groupControl2
            // 
            groupControl2.Controls.Add(layFund);
            groupControl2.Location = new Point(3, 4);
            groupControl2.Name = "groupControl2";
            groupControl2.Size = new Size(426, 191);
            groupControl2.TabIndex = 0;
            groupControl2.Text = "وضعیت";
            // 
            // layFund
            // 
            layFund.Dock = DockStyle.Fill;
            layFund.Location = new Point(2, 23);
            layFund.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            layFund.LookAndFeel.UseDefaultLookAndFeel = false;
            layFund.Name = "layFund";
            layFund.Size = new Size(422, 166);
            layFund.TabIndex = 0;
            layFund.Z_PanelShow = true;
            layFund.Z_UsedDelete = false;
            layFund.Z_UsedPrint = false;
            layFund.BtnSaveClick += layFund_BtnSaveClick;
            // 
            // groupControl1
            // 
            groupControl1.Controls.Add(layPerson);
            groupControl1.Location = new Point(68, 201);
            groupControl1.Name = "groupControl1";
            groupControl1.Size = new Size(297, 194);
            groupControl1.TabIndex = 0;
            groupControl1.Text = "کاربر جدید";
            // 
            // layPerson
            // 
            layPerson.Dock = DockStyle.Fill;
            layPerson.Location = new Point(2, 23);
            layPerson.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            layPerson.LookAndFeel.UseDefaultLookAndFeel = false;
            layPerson.Name = "layPerson";
            layPerson.Size = new Size(293, 169);
            layPerson.TabIndex = 0;
            layPerson.Z_PanelShow = true;
            layPerson.Z_UsedDelete = false;
            layPerson.Z_UsedPrint = false;
            layPerson.BtnSaveClick += layPerson_BtnSaveClick;
            // 
            // btnAddFile
            // 
            btnAddFile.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddFile.Appearance.Options.UseFont = true;
            btnAddFile.ImageOptions.SvgImage = Properties.Resources.actions_addcircled;
            btnAddFile.Location = new Point(0, 3);
            btnAddFile.Name = "btnAddFile";
            btnAddFile.Size = new Size(133, 28);
            btnAddFile.TabIndex = 16;
            btnAddFile.Text = "افزودن فیلم";
            btnAddFile.Click += btnAddFile_Click;
            // 
            // FrmCopyFile
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(432, 435);
            Controls.Add(btnAddFile);
            Controls.Add(tabPane1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            IconOptions.Image = Properties.Resources.copy;
            MaximizeBox = false;
            Name = "FrmCopyFile";
            StartPosition = FormStartPosition.CenterScreen;
            FormClosing += FrmCopyFile_FormClosing;
            FormClosed += FrmCopyFile_FormClosed;
            Load += FrmCopyFile_Load;
            ((System.ComponentModel.ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            tablePanel1.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lstRemain).EndInit();
            ((System.ComponentModel.ISupportInitialize)lstCopied).EndInit();
            ((System.ComponentModel.ISupportInitialize)prgCopy1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)prgCopy2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tabPane1).EndInit();
            tabPane1.ResumeLayout(false);
            tabNavigationPage1.ResumeLayout(false);
            tabNavigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupControl2).EndInit();
            groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)groupControl1).EndInit();
            groupControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblFileAll;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblFileCopied;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblFileRemian;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblSizeRemian;
        private System.Windows.Forms.Label lblSizeCopied;
        private System.Windows.Forms.Label lblSizeAll;
        private DevExpress.XtraEditors.ProgressBarControl prgCopy2;
        private DevExpress.XtraEditors.ProgressBarControl prgCopy1;
        private System.Windows.Forms.Label label13;
        private DevExpress.XtraEditors.ListBoxControl lstCopied;
        private System.Windows.Forms.Label label14;
        private DevExpress.XtraEditors.ListBoxControl lstRemain;
        private DevExpress.XtraBars.Navigation.TabPane tabPane1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage1;
        private DevExpress.XtraBars.Navigation.TabNavigationPage tabNavigationPage2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lblStart;
        private System.Windows.Forms.Label lblNow;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label lblEnd;
        private System.Windows.Forms.Label label19;
        private DevExpress.XtraEditors.SimpleButton btnAddFile;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private MyCom.Object.dataLayout layPerson;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private MyCom.Object.dataLayout layFund;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnMute;
    }
}
