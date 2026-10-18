namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmSelectChapter
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
            PnlChapterEnFa = new DevExpress.XtraBars.Navigation.TabPane();
            PageFa = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            dgvChapter = new MyCom.Object.KavoshGrid(components);
            viewChapter = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            PageEn = new DevExpress.XtraBars.Navigation.TabNavigationPage();
            dgvChapterEn = new MyCom.Object.KavoshGrid(components);
            viewChapterEn = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand4 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            panel2 = new Panel();
            lblCountFilm = new Label();
            btnAllSelect = new DevExpress.XtraEditors.SimpleButton();
            btnRemlSelect = new DevExpress.XtraEditors.SimpleButton();
            dgvHeaderChapter = new MyCom.Object.KavoshGrid(components);
            veiwHeaderChapter = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            panel1 = new Panel();
            lblName = new Label();
            panel3 = new Panel();
            btnRemlSelectAll = new DevExpress.XtraEditors.SimpleButton();
            dgvAllSelect = new MyCom.Object.KavoshGrid(components);
            viewAllSelect = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand3 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            label1 = new Label();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)PnlChapterEnFa).BeginInit();
            PnlChapterEnFa.SuspendLayout();
            PageFa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChapter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewChapter).BeginInit();
            PageEn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvChapterEn).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewChapterEn).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHeaderChapter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)veiwHeaderChapter).BeginInit();
            panel1.SuspendLayout();
            panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvAllSelect).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewAllSelect).BeginInit();
            SuspendLayout();
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = DockStyle.Fill;
            splitContainerControl1.Location = new Point(0, 30);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(PnlChapterEnFa);
            splitContainerControl1.Panel1.Controls.Add(panel2);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(dgvHeaderChapter);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(441, 485);
            splitContainerControl1.SplitterPosition = 239;
            splitContainerControl1.TabIndex = 0;
            // 
            // PnlChapterEnFa
            // 
            PnlChapterEnFa.Controls.Add(PageFa);
            PnlChapterEnFa.Controls.Add(PageEn);
            PnlChapterEnFa.Dock = DockStyle.Fill;
            PnlChapterEnFa.Location = new Point(0, 35);
            PnlChapterEnFa.Name = "PnlChapterEnFa";
            PnlChapterEnFa.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { PageFa, PageEn });
            PnlChapterEnFa.RegularSize = new Size(239, 450);
            PnlChapterEnFa.RightToLeft = RightToLeft.Yes;
            PnlChapterEnFa.SelectedPage = PageFa;
            PnlChapterEnFa.Size = new Size(239, 450);
            PnlChapterEnFa.TabIndex = 3;
            PnlChapterEnFa.Text = "tabPane1";
            // 
            // PageFa
            // 
            PageFa.Caption = "دوبله";
            PageFa.Controls.Add(dgvChapter);
            PageFa.Name = "PageFa";
            PageFa.Size = new Size(239, 418);
            // 
            // dgvChapter
            // 
            dgvChapter.AllowDrop = true;
            dgvChapter.Dock = DockStyle.Fill;
            dgvChapter.Location = new Point(0, 0);
            dgvChapter.MainView = viewChapter;
            dgvChapter.Name = "dgvChapter";
            dgvChapter.RightToLeft = RightToLeft.Yes;
            dgvChapter.Size = new Size(239, 418);
            dgvChapter.TabIndex = 1;
            dgvChapter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewChapter });
            dgvChapter.DragDrop += dgvChapter_DragDrop;
            dgvChapter.DragEnter += dgvChapter_DragEnter;
            // 
            // viewChapter
            // 
            viewChapter.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewChapter.GridControl = dgvChapter;
            viewChapter.Name = "viewChapter";
            viewChapter.OptionsSelection.MultiSelect = true;
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // PageEn
            // 
            PageEn.Caption = "زیرنویس";
            PageEn.Controls.Add(dgvChapterEn);
            PageEn.Name = "PageEn";
            PageEn.Size = new Size(239, 406);
            // 
            // dgvChapterEn
            // 
            dgvChapterEn.Dock = DockStyle.Fill;
            dgvChapterEn.Location = new Point(0, 0);
            dgvChapterEn.MainView = viewChapterEn;
            dgvChapterEn.Name = "dgvChapterEn";
            dgvChapterEn.RightToLeft = RightToLeft.Yes;
            dgvChapterEn.Size = new Size(239, 406);
            dgvChapterEn.TabIndex = 2;
            dgvChapterEn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewChapterEn });
            // 
            // viewChapterEn
            // 
            viewChapterEn.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand4 });
            viewChapterEn.GridControl = dgvChapterEn;
            viewChapterEn.Name = "viewChapterEn";
            viewChapterEn.OptionsSelection.MultiSelect = true;
            // 
            // gridBand4
            // 
            gridBand4.Caption = "gridBand1";
            gridBand4.Name = "gridBand4";
            gridBand4.VisibleIndex = 0;
            // 
            // panel2
            // 
            panel2.BackColor = Color.DarkSeaGreen;
            panel2.Controls.Add(lblCountFilm);
            panel2.Controls.Add(btnAllSelect);
            panel2.Controls.Add(btnRemlSelect);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(239, 35);
            panel2.TabIndex = 2;
            // 
            // lblCountFilm
            // 
            lblCountFilm.BackColor = Color.Yellow;
            lblCountFilm.Dock = DockStyle.Fill;
            lblCountFilm.Font = new Font("Samim FD", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblCountFilm.ForeColor = Color.LightCoral;
            lblCountFilm.Location = new Point(50, 0);
            lblCountFilm.Name = "lblCountFilm";
            lblCountFilm.RightToLeft = RightToLeft.Yes;
            lblCountFilm.Size = new Size(139, 35);
            lblCountFilm.TabIndex = 2;
            lblCountFilm.Text = "0";
            lblCountFilm.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnAllSelect
            // 
            btnAllSelect.Dock = DockStyle.Right;
            btnAllSelect.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnAllSelect.ImageOptions.ImageToTextIndent = 1;
            btnAllSelect.ImageOptions.SvgImage = Properties.Resources.bo_task;
            btnAllSelect.ImageOptions.SvgImageSize = new Size(37, 37);
            btnAllSelect.Location = new Point(189, 0);
            btnAllSelect.Name = "btnAllSelect";
            btnAllSelect.Size = new Size(50, 35);
            btnAllSelect.TabIndex = 0;
            btnAllSelect.Click += btnAllSelect_Click;
            // 
            // btnRemlSelect
            // 
            btnRemlSelect.Dock = DockStyle.Left;
            btnRemlSelect.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnRemlSelect.ImageOptions.ImageToTextIndent = 1;
            btnRemlSelect.ImageOptions.SvgImage = Properties.Resources.paymentunpaid;
            btnRemlSelect.Location = new Point(0, 0);
            btnRemlSelect.Name = "btnRemlSelect";
            btnRemlSelect.Size = new Size(50, 35);
            btnRemlSelect.TabIndex = 0;
            btnRemlSelect.Click += btnRemlSelect_Click;
            // 
            // dgvHeaderChapter
            // 
            dgvHeaderChapter.Dock = DockStyle.Fill;
            dgvHeaderChapter.Location = new Point(0, 0);
            dgvHeaderChapter.MainView = veiwHeaderChapter;
            dgvHeaderChapter.Name = "dgvHeaderChapter";
            dgvHeaderChapter.RightToLeft = RightToLeft.Yes;
            dgvHeaderChapter.Size = new Size(190, 485);
            dgvHeaderChapter.TabIndex = 0;
            dgvHeaderChapter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { veiwHeaderChapter });
            // 
            // veiwHeaderChapter
            // 
            veiwHeaderChapter.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });
            veiwHeaderChapter.GridControl = dgvHeaderChapter;
            veiwHeaderChapter.Name = "veiwHeaderChapter";
            // 
            // gridBand2
            // 
            gridBand2.Caption = "gridBand2";
            gridBand2.Name = "gridBand2";
            gridBand2.VisibleIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(lblName);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(685, 30);
            panel1.TabIndex = 1;
            // 
            // lblName
            // 
            lblName.BackColor = Color.PaleGoldenrod;
            lblName.Dock = DockStyle.Fill;
            lblName.Font = new Font("Samim FD", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblName.ForeColor = Color.DarkGreen;
            lblName.Location = new Point(0, 0);
            lblName.Name = "lblName";
            lblName.RightToLeft = RightToLeft.Yes;
            lblName.Size = new Size(685, 30);
            lblName.TabIndex = 0;
            lblName.Text = "(نام فیلم)";
            lblName.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel3
            // 
            panel3.Controls.Add(btnRemlSelectAll);
            panel3.Controls.Add(dgvAllSelect);
            panel3.Controls.Add(label1);
            panel3.Dock = DockStyle.Right;
            panel3.Location = new Point(441, 30);
            panel3.Name = "panel3";
            panel3.Size = new Size(244, 485);
            panel3.TabIndex = 2;
            // 
            // btnRemlSelectAll
            // 
            btnRemlSelectAll.ImageOptions.ImageToTextAlignment = DevExpress.XtraEditors.ImageAlignToText.RightCenter;
            btnRemlSelectAll.ImageOptions.ImageToTextIndent = 1;
            btnRemlSelectAll.ImageOptions.SvgImage = Properties.Resources.paymentunpaid;
            btnRemlSelectAll.ImageOptions.SvgImageSize = new Size(27, 27);
            btnRemlSelectAll.Location = new Point(0, 0);
            btnRemlSelectAll.Name = "btnRemlSelectAll";
            btnRemlSelectAll.Size = new Size(50, 30);
            btnRemlSelectAll.TabIndex = 6;
            btnRemlSelectAll.Click += btnRemlSelectAll_Click;
            // 
            // dgvAllSelect
            // 
            dgvAllSelect.Dock = DockStyle.Fill;
            dgvAllSelect.Location = new Point(0, 30);
            dgvAllSelect.MainView = viewAllSelect;
            dgvAllSelect.Name = "dgvAllSelect";
            dgvAllSelect.RightToLeft = RightToLeft.Yes;
            dgvAllSelect.Size = new Size(244, 455);
            dgvAllSelect.TabIndex = 2;
            dgvAllSelect.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewAllSelect });
            // 
            // viewAllSelect
            // 
            viewAllSelect.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand3 });
            viewAllSelect.GridControl = dgvAllSelect;
            viewAllSelect.Name = "viewAllSelect";
            // 
            // gridBand3
            // 
            gridBand3.Caption = "gridBand2";
            gridBand3.Name = "gridBand3";
            gridBand3.VisibleIndex = 0;
            // 
            // label1
            // 
            label1.BackColor = Color.GreenYellow;
            label1.Dock = DockStyle.Top;
            label1.Font = new Font("Samim FD", 11.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.DarkMagenta;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            label1.Size = new Size(244, 30);
            label1.TabIndex = 1;
            label1.Text = "انتخاب شده ها";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // FrmSelectChapter
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(685, 515);
            Controls.Add(splitContainerControl1);
            Controls.Add(panel3);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            Name = "FrmSelectChapter";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "انتخا فیلم";
            Load += FrmSelectChapter_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)PnlChapterEnFa).EndInit();
            PnlChapterEnFa.ResumeLayout(false);
            PageFa.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvChapter).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewChapter).EndInit();
            PageEn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvChapterEn).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewChapterEn).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHeaderChapter).EndInit();
            ((System.ComponentModel.ISupportInitialize)veiwHeaderChapter).EndInit();
            panel1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvAllSelect).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewAllSelect).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private MyCom.Object.KavoshGrid dgvChapter;
        private MyCom.Object.KavoshGrid dgvHeaderChapter;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblName;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewChapter;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView veiwHeaderChapter;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private System.Windows.Forms.Panel panel2;
        private DevExpress.XtraEditors.SimpleButton btnAllSelect;
        private DevExpress.XtraEditors.SimpleButton btnRemlSelect;
        private System.Windows.Forms.Label lblCountFilm;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private MyCom.Object.KavoshGrid dgvAllSelect;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewAllSelect;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand3;
        private DevExpress.XtraBars.Navigation.TabPane PnlChapterEnFa;
        private DevExpress.XtraBars.Navigation.TabNavigationPage PageFa;
        private DevExpress.XtraBars.Navigation.TabNavigationPage PageEn;
        private MyCom.Object.KavoshGrid dgvChapterEn;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewChapterEn;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand4;
        private DevExpress.XtraEditors.SimpleButton btnRemlSelectAll;
    }
}