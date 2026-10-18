
namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmDefiniteAccounts
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
            layDefiniteAccount = new MyCom.Object.dataLayout();
            dgvDefiniteAccount = new MyCom.Object.KavoshGrid(components);
            bandedGridView1 = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            pnlGrid = new Panel();
            btnDelEndDoc = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)dgvDefiniteAccount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bandedGridView1).BeginInit();
            pnlGrid.SuspendLayout();
            SuspendLayout();
            // 
            // layDefiniteAccount
            // 
            layDefiniteAccount.Dock = DockStyle.Top;
            layDefiniteAccount.Location = new Point(0, 0);
            layDefiniteAccount.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            layDefiniteAccount.LookAndFeel.UseDefaultLookAndFeel = false;
            layDefiniteAccount.Name = "layDefiniteAccount";
            layDefiniteAccount.RightToLeft = RightToLeft.Yes;
            layDefiniteAccount.Size = new Size(933, 181);
            layDefiniteAccount.TabIndex = 1;
            layDefiniteAccount.Z_PanelShow = true;
            layDefiniteAccount.Z_UsedDelete = false;
            layDefiniteAccount.Z_UsedPrint = false;
            layDefiniteAccount.BtnSaveClick += layDefiniteAccount_BtnSaveClick;
            layDefiniteAccount.Load += layDefiniteAccount_Load;
            // 
            // dgvDefiniteAccount
            // 
            dgvDefiniteAccount.Dock = DockStyle.Fill;
            dgvDefiniteAccount.EmbeddedNavigator.Appearance.BackColor = Color.White;
            dgvDefiniteAccount.EmbeddedNavigator.Appearance.Options.UseBackColor = true;
            dgvDefiniteAccount.Location = new Point(0, 215);
            dgvDefiniteAccount.MainView = bandedGridView1;
            dgvDefiniteAccount.Name = "dgvDefiniteAccount";
            dgvDefiniteAccount.RightToLeft = RightToLeft.Yes;
            dgvDefiniteAccount.Size = new Size(933, 326);
            dgvDefiniteAccount.TabIndex = 3;
            dgvDefiniteAccount.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { bandedGridView1 });
            // 
            // bandedGridView1
            // 
            bandedGridView1.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            bandedGridView1.GridControl = dgvDefiniteAccount;
            bandedGridView1.Name = "bandedGridView1";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // pnlGrid
            // 
            pnlGrid.Controls.Add(btnDelEndDoc);
            pnlGrid.Dock = DockStyle.Top;
            pnlGrid.Location = new Point(0, 181);
            pnlGrid.Name = "pnlGrid";
            pnlGrid.Size = new Size(933, 34);
            pnlGrid.TabIndex = 4;
            // 
            // btnDelEndDoc
            // 
            btnDelEndDoc.Dock = DockStyle.Right;
            btnDelEndDoc.Enabled = false;
            btnDelEndDoc.ImageOptions.SvgImage = Properties.Resources.actions_reload;
            btnDelEndDoc.Location = new Point(776, 0);
            btnDelEndDoc.Name = "btnDelEndDoc";
            btnDelEndDoc.Size = new Size(157, 34);
            btnDelEndDoc.TabIndex = 0;
            btnDelEndDoc.Text = "حذف آخرسن سند";
            btnDelEndDoc.Visible = false;
            btnDelEndDoc.Click += btnDelEndDoc_Click;
            // 
            // FrmDefiniteAccounts
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(933, 541);
            Controls.Add(dgvDefiniteAccount);
            Controls.Add(pnlGrid);
            Controls.Add(layDefiniteAccount);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "FrmDefiniteAccounts";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "حساب معین";
            Load += frmDefiniteAccounts_Load;
            ((System.ComponentModel.ISupportInitialize)dgvDefiniteAccount).EndInit();
            ((System.ComponentModel.ISupportInitialize)bandedGridView1).EndInit();
            pnlGrid.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private MyCom.Object.dataLayout layDefiniteAccount;
        private MyCom.Object.KavoshGrid dgvDefiniteAccount;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView bandedGridView1;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private System.Windows.Forms.Panel pnlGrid;
        private DevExpress.XtraEditors.SimpleButton btnDelEndDoc;
    }
}