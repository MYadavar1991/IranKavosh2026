namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmPriceList
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
            dgvPriceList = new MyCom.Object.KavoshGrid(components);
            viewPriceList = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            panel1 = new Panel();
            dgvPriceListDiscount = new MyCom.Object.KavoshGrid(components);
            viewPriceListDiscount = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            ((System.ComponentModel.ISupportInitialize)dgvPriceList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewPriceList).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvPriceListDiscount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewPriceListDiscount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            SuspendLayout();
            // 
            // dgvPriceList
            // 
            dgvPriceList.Dock = DockStyle.Fill;
            dgvPriceList.Location = new Point(0, 0);
            dgvPriceList.MainView = viewPriceList;
            dgvPriceList.Name = "dgvPriceList";
            dgvPriceList.RightToLeft = RightToLeft.Yes;
            dgvPriceList.Size = new Size(603, 558);
            dgvPriceList.TabIndex = 1;
            dgvPriceList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewPriceList });
            // 
            // viewPriceList
            // 
            viewPriceList.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewPriceList.GridControl = dgvPriceList;
            viewPriceList.Name = "viewPriceList";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(1072, 35);
            panel1.TabIndex = 1;
            // 
            // dgvPriceListDiscount
            // 
            dgvPriceListDiscount.Dock = DockStyle.Fill;
            dgvPriceListDiscount.Location = new Point(0, 0);
            dgvPriceListDiscount.MainView = viewPriceListDiscount;
            dgvPriceListDiscount.Name = "dgvPriceListDiscount";
            dgvPriceListDiscount.RightToLeft = RightToLeft.Yes;
            dgvPriceListDiscount.Size = new Size(457, 558);
            dgvPriceListDiscount.TabIndex = 2;
            dgvPriceListDiscount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewPriceListDiscount });
            // 
            // viewPriceListDiscount
            // 
            viewPriceListDiscount.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });
            viewPriceListDiscount.GridControl = dgvPriceListDiscount;
            viewPriceListDiscount.Name = "viewPriceListDiscount";
            // 
            // gridBand2
            // 
            gridBand2.Caption = "gridBand1";
            gridBand2.Name = "gridBand2";
            gridBand2.VisibleIndex = 0;
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = DockStyle.Fill;
            splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            splitContainerControl1.IsSplitterFixed = true;
            splitContainerControl1.Location = new Point(0, 35);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(dgvPriceListDiscount);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(dgvPriceList);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1072, 558);
            splitContainerControl1.SplitterPosition = 603;
            splitContainerControl1.TabIndex = 3;
            // 
            // FrmPriceList
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1072, 593);
            Controls.Add(splitContainerControl1);
            Controls.Add(panel1);
            Name = "FrmPriceList";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "نرخ نامه";
            Load += FrmPriceList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvPriceList).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewPriceList).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvPriceListDiscount).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewPriceListDiscount).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private MyCom.Object.KavoshGrid dgvPriceList;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewPriceList;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private Panel panel1;
        private MyCom.Object.KavoshGrid dgvPriceListDiscount;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewPriceListDiscount;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
    }
}