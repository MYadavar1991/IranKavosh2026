namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmBuySell
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
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            dgvSaham = new MyCom.Object.KavoshGrid(components);
            bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            dgvHeader = new MyCom.Object.KavoshGrid(components);
            viewHeader = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlHeader = new Panel();
            btnCalc = new DevExpress.XtraEditors.SimpleButton();
            btnShowAll = new DevExpress.XtraEditors.SimpleButton();
            btnBack = new DevExpress.XtraEditors.SimpleButton();
            btnNext = new DevExpress.XtraEditors.SimpleButton();
            pnlDateOpen1 = new MyCom.Object.pnlDateOpen();
            layHeader = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlOperation = new Panel();
            dgvDetails = new MyCom.Object.KavoshGrid(components);
            viewDetails = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel1).BeginInit();
            splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel2).BeginInit();
            splitContainerControl2.Panel2.SuspendLayout();
            splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvSaham).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bandedGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewHeader).BeginInit();
            pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewDetails).BeginInit();
            SuspendLayout();
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = DockStyle.Fill;
            splitContainerControl1.Horizontal = false;
            splitContainerControl1.IsSplitterFixed = true;
            splitContainerControl1.Location = new Point(0, 0);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(splitContainerControl2);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Appearance.BackColor = Color.FromArgb(224, 224, 224);
            splitContainerControl1.Panel2.Appearance.Options.UseBackColor = true;
            splitContainerControl1.Panel2.Controls.Add(dgvDetails);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1137, 671);
            splitContainerControl1.SplitterPosition = 344;
            splitContainerControl1.TabIndex = 0;
            // 
            // splitContainerControl2
            // 
            splitContainerControl2.Dock = DockStyle.Fill;
            splitContainerControl2.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            splitContainerControl2.IsSplitterFixed = true;
            splitContainerControl2.Location = new Point(0, 0);
            splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            splitContainerControl2.Panel1.Controls.Add(dgvHeader);
            splitContainerControl2.Panel1.Controls.Add(pnlHeader);
            splitContainerControl2.Panel1.Controls.Add(dgvSaham);
            splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            splitContainerControl2.Panel2.Appearance.BackColor = Color.PowderBlue;
            splitContainerControl2.Panel2.Appearance.Options.UseBackColor = true;
            splitContainerControl2.Panel2.Controls.Add(layHeader);
            splitContainerControl2.Panel2.Controls.Add(pnlOperation);
            splitContainerControl2.Panel2.Text = "Panel2";
            splitContainerControl2.Size = new Size(1137, 344);
            splitContainerControl2.SplitterPosition = 405;
            splitContainerControl2.TabIndex = 0;
            // 
            // dgvSaham
            // 
            dgvSaham.Location = new Point(139, 177);
            dgvSaham.MainView = bandedGridView1;
            dgvSaham.Name = "dgvSaham";
            dgvSaham.Size = new Size(348, 164);
            dgvSaham.TabIndex = 4;
            dgvSaham.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { bandedGridView1 });
            // 
            // bandedGridView1
            // 
            bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand3 });
            bandedGridView1.GridControl = dgvSaham;
            bandedGridView1.Name = "bandedGridView1";
            // 
            // gridBand3
            // 
            gridBand3.Caption = "gridBand3";
            gridBand3.Name = "gridBand3";
            gridBand3.VisibleIndex = 0;
            // 
            // dgvHeader
            // 
            dgvHeader.Dock = DockStyle.Fill;
            dgvHeader.Location = new Point(0, 35);
            dgvHeader.LookAndFeel.SkinName = "Office 2019 Colorful";
            dgvHeader.LookAndFeel.UseDefaultLookAndFeel = false;
            dgvHeader.MainView = viewHeader;
            dgvHeader.Name = "dgvHeader";
            dgvHeader.RightToLeft = RightToLeft.Yes;
            dgvHeader.Size = new Size(720, 309);
            dgvHeader.TabIndex = 0;
            dgvHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewHeader });
            // 
            // viewHeader
            // 
            viewHeader.Appearance.HeaderPanel.Font = new Font("Tahoma", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            viewHeader.Appearance.HeaderPanel.Options.UseFont = true;
            viewHeader.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewHeader.GridControl = dgvHeader;
            viewHeader.Name = "viewHeader";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // pnlHeader
            // 
            pnlHeader.Controls.Add(btnCalc);
            pnlHeader.Controls.Add(btnShowAll);
            pnlHeader.Controls.Add(btnBack);
            pnlHeader.Controls.Add(btnNext);
            pnlHeader.Controls.Add(pnlDateOpen1);
            pnlHeader.Dock = DockStyle.Top;
            pnlHeader.Location = new Point(0, 0);
            pnlHeader.Name = "pnlHeader";
            pnlHeader.Size = new Size(720, 35);
            pnlHeader.TabIndex = 1;
            // 
            // btnCalc
            // 
            btnCalc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            btnCalc.ImageOptions.SvgImage = Properties.Resources.accounting;
            btnCalc.ImageOptions.SvgImageSize = new Size(28, 28);
            btnCalc.Location = new Point(3, 3);
            btnCalc.Name = "btnCalc";
            btnCalc.Size = new Size(246, 29);
            btnCalc.TabIndex = 2;
            btnCalc.Text = "تسویه کامل (به جز فروش لوازم)";
            btnCalc.Click += btnCalc_Click;
            // 
            // btnShowAll
            // 
            btnShowAll.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnShowAll.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnShowAll.Appearance.Options.UseFont = true;
            btnShowAll.Location = new Point(291, 3);
            btnShowAll.Name = "btnShowAll";
            btnShowAll.Size = new Size(104, 29);
            btnShowAll.TabIndex = 1;
            btnShowAll.Text = "نمایش همه";
            btnShowAll.Click += btnShowAll_Click;
            // 
            // btnBack
            // 
            btnBack.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnBack.ImageOptions.SvgImage = Properties.Resources.prev;
            btnBack.Location = new Point(401, 3);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(39, 29);
            btnBack.TabIndex = 1;
            btnBack.Click += btnBack_Click;
            // 
            // btnNext
            // 
            btnNext.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.ImageOptions.SvgImage = Properties.Resources.next;
            btnNext.Location = new Point(678, 3);
            btnNext.Name = "btnNext";
            btnNext.Size = new Size(39, 30);
            btnNext.TabIndex = 1;
            btnNext.Click += btnNext_Click;
            // 
            // pnlDateOpen1
            // 
            pnlDateOpen1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            pnlDateOpen1.Location = new Point(442, 3);
            pnlDateOpen1.Name = "pnlDateOpen1";
            pnlDateOpen1.Size = new Size(234, 30);
            pnlDateOpen1.TabIndex = 0;
            // 
            // layHeader
            // 
            layHeader.Dock = DockStyle.Fill;
            layHeader.Location = new Point(0, 32);
            layHeader.LookAndFeel.SkinName = "VS2010";
            layHeader.LookAndFeel.UseDefaultLookAndFeel = false;
            layHeader.Name = "layHeader";
            layHeader.OptionsView.RightToLeftMirroringApplied = true;
            layHeader.RightToLeft = RightToLeft.Yes;
            layHeader.Root = Root;
            layHeader.Size = new Size(405, 312);
            layHeader.TabIndex = 0;
            layHeader.Text = "kavoshLayout1";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(405, 312);
            Root.TextVisible = false;
            // 
            // pnlOperation
            // 
            pnlOperation.Dock = DockStyle.Top;
            pnlOperation.Location = new Point(0, 0);
            pnlOperation.Name = "pnlOperation";
            pnlOperation.Size = new Size(405, 32);
            pnlOperation.TabIndex = 1;
            // 
            // dgvDetails
            // 
            dgvDetails.Dock = DockStyle.Fill;
            dgvDetails.Location = new Point(0, 0);
            dgvDetails.MainView = viewDetails;
            dgvDetails.Name = "dgvDetails";
            dgvDetails.RightToLeft = RightToLeft.Yes;
            dgvDetails.Size = new Size(1137, 315);
            dgvDetails.TabIndex = 1;
            dgvDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewDetails });
            // 
            // viewDetails
            // 
            viewDetails.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });
            viewDetails.GridControl = dgvDetails;
            viewDetails.Name = "viewDetails";
            // 
            // gridBand2
            // 
            gridBand2.Caption = "gridBand1";
            gridBand2.Name = "gridBand2";
            gridBand2.VisibleIndex = 0;
            // 
            // FrmBuySell
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1137, 671);
            Controls.Add(splitContainerControl1);
            Name = "FrmBuySell";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "فاکتورها";
            Load += FrmBuySell_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel1).EndInit();
            splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel2).EndInit();
            splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2).EndInit();
            splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvSaham).EndInit();
            ((System.ComponentModel.ISupportInitialize)bandedGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewHeader).EndInit();
            pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewDetails).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private MyCom.Object.KavoshLayout layHeader;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Panel pnlOperation;
        private MyCom.Object.KavoshGrid dgvHeader;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewHeader;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private MyCom.Object.KavoshGrid dgvDetails;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewDetails;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private Panel pnlHeader;
        private MyCom.Object.pnlDateOpen pnlDateOpen1;
        private DevExpress.XtraEditors.SimpleButton btnNext;
        private DevExpress.XtraEditors.SimpleButton btnCalc;
        private DevExpress.XtraEditors.SimpleButton btnShowAll;
        private DevExpress.XtraEditors.SimpleButton btnBack;
        private MyCom.Object.KavoshGrid dgvSaham;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
    }
}