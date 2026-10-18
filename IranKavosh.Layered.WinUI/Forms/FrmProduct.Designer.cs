namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmProduct
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
            panel1 = new Panel();
            dgvProduct = new MyCom.Object.KavoshGrid(components);
            viewProduct = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            ((System.ComponentModel.ISupportInitialize)dgvProduct).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewProduct).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(896, 34);
            panel1.TabIndex = 0;
            // 
            // dgvProduct
            // 
            dgvProduct.Dock = DockStyle.Fill;
            dgvProduct.Location = new Point(0, 34);
            dgvProduct.MainView = viewProduct;
            dgvProduct.Name = "dgvProduct";
            dgvProduct.Size = new Size(896, 423);
            dgvProduct.TabIndex = 1;
            dgvProduct.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewProduct });
            // 
            // viewProduct
            // 
            viewProduct.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewProduct.GridControl = dgvProduct;
            viewProduct.Name = "viewProduct";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // FrmProduct
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(896, 457);
            Controls.Add(dgvProduct);
            Controls.Add(panel1);
            Name = "FrmProduct";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "محصولات - خدمات";
            Load += FrmProduct_Load;
            ((System.ComponentModel.ISupportInitialize)dgvProduct).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewProduct).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private MyCom.Object.KavoshGrid dgvProduct;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewProduct;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}