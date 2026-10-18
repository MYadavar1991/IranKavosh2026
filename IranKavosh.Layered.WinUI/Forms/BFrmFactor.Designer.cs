namespace IranKavosh.Layered.WinUI.Forms
{
    partial class BFrmFactor
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
            dgvHeader = new MyCom.Object.KavoshGrid(components);
            viewHeader = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand2 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            panel1 = new Panel();
            splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            layHeader = new MyCom.Object.KavoshLayout(components);
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            pnlOperation = new Panel();
            dgvDetail = new MyCom.Object.KavoshGrid(components);
            viewDetail = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).BeginInit();
            splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).BeginInit();
            splitContainerControl1.Panel2.SuspendLayout();
            splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel1).BeginInit();
            splitContainerControl2.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel2).BeginInit();
            splitContainerControl2.Panel2.SuspendLayout();
            splitContainerControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)layHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewDetail).BeginInit();
            SuspendLayout();
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = DockStyle.Fill;
            splitContainerControl1.Location = new Point(0, 0);
            splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Controls.Add(dgvHeader);
            splitContainerControl1.Panel1.Controls.Add(panel1);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Controls.Add(splitContainerControl2);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1140, 588);
            splitContainerControl1.SplitterPosition = 521;
            splitContainerControl1.TabIndex = 0;
            // 
            // dgvHeader
            // 
            dgvHeader.Dock = DockStyle.Fill;
            dgvHeader.Location = new Point(0, 37);
            dgvHeader.MainView = viewHeader;
            dgvHeader.Name = "dgvHeader";
            dgvHeader.RightToLeft = RightToLeft.Yes;
            dgvHeader.Size = new Size(521, 551);
            dgvHeader.TabIndex = 0;
            dgvHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewHeader });
            // 
            // viewHeader
            // 
            viewHeader.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand2 });
            viewHeader.GridControl = dgvHeader;
            viewHeader.Name = "viewHeader";
            // 
            // gridBand2
            // 
            gridBand2.Caption = "gridBand2";
            gridBand2.Name = "gridBand2";
            gridBand2.VisibleIndex = 0;
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(521, 37);
            panel1.TabIndex = 4;
            // 
            // splitContainerControl2
            // 
            splitContainerControl2.Dock = DockStyle.Fill;
            splitContainerControl2.Horizontal = false;
            splitContainerControl2.Location = new Point(0, 0);
            splitContainerControl2.Name = "splitContainerControl2";
            // 
            // splitContainerControl2.Panel1
            // 
            splitContainerControl2.Panel1.Controls.Add(layHeader);
            splitContainerControl2.Panel1.Controls.Add(pnlOperation);
            splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            splitContainerControl2.Panel2.Controls.Add(dgvDetail);
            splitContainerControl2.Panel2.Text = "Panel2";
            splitContainerControl2.Size = new Size(607, 588);
            splitContainerControl2.SplitterPosition = 239;
            splitContainerControl2.TabIndex = 0;
            // 
            // layHeader
            // 
            layHeader.Dock = DockStyle.Fill;
            layHeader.Location = new Point(0, 37);
            layHeader.Name = "layHeader";
            layHeader.OptionsView.RightToLeftMirroringApplied = true;
            layHeader.RightToLeft = RightToLeft.Yes;
            layHeader.Root = Root;
            layHeader.Size = new Size(607, 202);
            layHeader.TabIndex = 2;
            layHeader.Text = "kavoshLayout1";
            // 
            // Root
            // 
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Name = "Root";
            Root.Size = new Size(607, 202);
            Root.TextVisible = false;
            // 
            // pnlOperation
            // 
            pnlOperation.Dock = DockStyle.Top;
            pnlOperation.Location = new Point(0, 0);
            pnlOperation.Name = "pnlOperation";
            pnlOperation.Size = new Size(607, 37);
            pnlOperation.TabIndex = 3;
            // 
            // dgvDetail
            // 
            dgvDetail.Dock = DockStyle.Fill;
            dgvDetail.Enabled = false;
            dgvDetail.Location = new Point(0, 0);
            dgvDetail.MainView = viewDetail;
            dgvDetail.Name = "dgvDetail";
            dgvDetail.RightToLeft = RightToLeft.Yes;
            dgvDetail.Size = new Size(607, 337);
            dgvDetail.TabIndex = 0;
            dgvDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewDetail });
            // 
            // viewDetail
            // 
            viewDetail.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewDetail.GridControl = dgvDetail;
            viewDetail.Name = "viewDetail";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // BFrmFactor
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1140, 588);
            Controls.Add(splitContainerControl1);
            Name = "BFrmFactor";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "فاکتورها";
            Load += BFrmFactor_Load;
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel1).EndInit();
            splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1.Panel2).EndInit();
            splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl1).EndInit();
            splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel1).EndInit();
            splitContainerControl2.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2.Panel2).EndInit();
            splitContainerControl2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainerControl2).EndInit();
            splitContainerControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)layHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvDetail).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewDetail).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private MyCom.Object.KavoshLayout layHeader;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private Panel pnlOperation;
        private MyCom.Object.KavoshGrid dgvHeader;
        private Panel panel1;
        private MyCom.Object.KavoshGrid dgvDetail;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewDetail;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewHeader;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand2;
    }
}