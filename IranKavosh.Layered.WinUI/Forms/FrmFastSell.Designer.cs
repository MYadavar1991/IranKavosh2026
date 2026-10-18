namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmFastSell
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
            layHeader = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            dgvPrint = new MyCom.Object.KavoshGrid(components);
            viewPrint = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            dgvDetails = new MyCom.Object.KavoshGrid(components);
            viewDetails = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlOperation = new Panel();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel1).BeginInit();
            splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel2).BeginInit();
            splitContainerControl2.Panel2.SuspendLayout();
            splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvPrint).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewPrint).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewDetails).BeginInit();
            SuspendLayout();
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = DockStyle.Fill;
            splitContainerControl1.Horizontal = false;
            splitContainerControl1.IsSplitterFixed = true;
            splitContainerControl1.Location = new Point(0, 37);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Appearance.BackColor = Color.FromArgb(255, 255, 192);
            splitContainerControl1.Panel1.Appearance.Options.UseBackColor = true;
            splitContainerControl1.Panel1.Controls.Add(layHeader);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(splitContainerControl2);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(699, 635);
            splitContainerControl1.SplitterPosition = 115;
            splitContainerControl1.TabIndex = 0;
            // 
            // layHeader
            // 
            layHeader.Dock = DockStyle.Fill;
            layHeader.Location = new Point(0, 0);
            layHeader.Name = "layHeader";
            layHeader.OptionsView.RightToLeftMirroringApplied = true;
            layHeader.RightToLeft = RightToLeft.Yes;
            layHeader.Root = Root;
            layHeader.Size = new Size(699, 115);
            layHeader.TabIndex = 0;
            layHeader.Text = "kavoshLayout1";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(699, 115);
            Root.TextVisible = false;
            // 
            // splitContainerControl2
            // 
            splitContainerControl2.Dock = DockStyle.Fill;
            splitContainerControl2.Horizontal = false;
            splitContainerControl2.IsSplitterFixed = true;
            splitContainerControl2.Location = new Point(0, 0);
            splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            splitContainerControl2.Panel1.Controls.Add(dgvPrint);
            splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            splitContainerControl2.Panel2.Controls.Add(dgvDetails);
            splitContainerControl2.Panel2.Text = "Panel2";
            splitContainerControl2.Size = new Size(699, 508);
            splitContainerControl2.SplitterPosition = 253;
            splitContainerControl2.TabIndex = 2;
            // 
            // dgvPrint
            // 
            dgvPrint.Dock = DockStyle.Fill;
            dgvPrint.Location = new Point(0, 0);
            dgvPrint.MainView = viewPrint;
            dgvPrint.Name = "dgvPrint";
            dgvPrint.RightToLeft = RightToLeft.Yes;
            dgvPrint.Size = new Size(699, 253);
            dgvPrint.TabIndex = 1;
            dgvPrint.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewPrint });
            // 
            // viewPrint
            // 
            viewPrint.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });
            viewPrint.GridControl = dgvPrint;
            viewPrint.Name = "viewPrint";
            // 
            // gridBand2
            // 
            gridBand2.Caption = "gridBand1";
            gridBand2.Name = "gridBand2";
            gridBand2.VisibleIndex = 0;
            // 
            // dgvDetails
            // 
            dgvDetails.Dock = DockStyle.Fill;
            dgvDetails.Location = new Point(0, 0);
            dgvDetails.MainView = viewDetails;
            dgvDetails.Name = "dgvDetails";
            dgvDetails.RightToLeft = RightToLeft.Yes;
            dgvDetails.Size = new Size(699, 243);
            dgvDetails.TabIndex = 0;
            dgvDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewDetails });
            // 
            // viewDetails
            // 
            viewDetails.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewDetails.GridControl = dgvDetails;
            viewDetails.Name = "viewDetails";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // pnlOperation
            // 
            pnlOperation.Dock = DockStyle.Top;
            pnlOperation.Location = new Point(0, 0);
            pnlOperation.Name = "pnlOperation";
            pnlOperation.Size = new Size(699, 37);
            pnlOperation.TabIndex = 1;
            // 
            // FrmFastSell
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(699, 672);
            Controls.Add(splitContainerControl1);
            Controls.Add(pnlOperation);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            Name = "FrmFastSell";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "صدور فاکتور فروش";
            Load += FrmFastSell_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel1).EndInit();
            splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel2).EndInit();
            splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2).EndInit();
            splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvPrint).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewPrint).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewDetails).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private MyCom.Object.KavoshLayout layHeader;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private MyCom.Object.KavoshGrid dgvDetails;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewDetails;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private Panel pnlOperation;
        private MyCom.Object.KavoshGrid dgvPrint;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewPrint;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
    }
}