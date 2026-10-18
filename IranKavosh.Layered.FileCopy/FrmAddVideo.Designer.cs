namespace IranKavosh.Layered.FileCopy
{
    partial class FrmAddVideo
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
            lueHeader = new DevExpress.XtraEditors.GridLookUpEdit();
            gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            lstHeaderChapters = new DevExpress.XtraEditors.ListBoxControl();
            lstChapterDetails = new DevExpress.XtraEditors.ListBoxControl();
            dgvSelect = new MyCom.Object.KavoshGrid(components);
            viewSelect = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            btnAdd = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)lueHeader.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridLookUpEdit1View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lstHeaderChapters).BeginInit();
            ((System.ComponentModel.ISupportInitialize)lstChapterDetails).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSelect).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewSelect).BeginInit();
            SuspendLayout();
            // 
            // lueHeader
            // 
            lueHeader.Location = new Point(348, 12);
            lueHeader.Name = "lueHeader";
            lueHeader.Properties.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lueHeader.Properties.Appearance.Options.UseFont = true;
            lueHeader.Properties.Appearance.Options.UseTextOptions = true;
            lueHeader.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lueHeader.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lueHeader.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            lueHeader.Properties.PopupView = gridLookUpEdit1View;
            lueHeader.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            lueHeader.RightToLeft = RightToLeft.Yes;
            lueHeader.Size = new Size(303, 26);
            lueHeader.TabIndex = 0;
            // 
            // gridLookUpEdit1View
            // 
            gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // lstHeaderChapters
            // 
            lstHeaderChapters.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lstHeaderChapters.Appearance.Options.UseFont = true;
            lstHeaderChapters.Location = new Point(348, 44);
            lstHeaderChapters.Name = "lstHeaderChapters";
            lstHeaderChapters.RightToLeft = RightToLeft.Yes;
            lstHeaderChapters.Size = new Size(303, 171);
            lstHeaderChapters.TabIndex = 1;
            // 
            // lstChapterDetails
            // 
            lstChapterDetails.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lstChapterDetails.Appearance.Options.UseFont = true;
            lstChapterDetails.Location = new Point(348, 221);
            lstChapterDetails.Name = "lstChapterDetails";
            lstChapterDetails.RightToLeft = RightToLeft.Yes;
            lstChapterDetails.Size = new Size(303, 246);
            lstChapterDetails.TabIndex = 1;
            // 
            // dgvSelect
            // 
            dgvSelect.Location = new Point(12, 12);
            dgvSelect.MainView = viewSelect;
            dgvSelect.Name = "dgvSelect";
            dgvSelect.Size = new Size(330, 455);
            dgvSelect.TabIndex = 2;
            dgvSelect.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewSelect });
            // 
            // viewSelect
            // 
            viewSelect.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewSelect.GridControl = dgvSelect;
            viewSelect.Name = "viewSelect";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // btnAdd
            // 
            btnAdd.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            btnAdd.Appearance.Options.UseFont = true;
            btnAdd.ImageOptions.SvgImage = Properties.Resources.actions_add;
            btnAdd.Location = new Point(553, 475);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(98, 32);
            btnAdd.TabIndex = 3;
            btnAdd.Text = "افزودن";
            btnAdd.Visible = false;
            // 
            // btnCancel
            // 
            btnCancel.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            btnCancel.Appearance.Options.UseFont = true;
            btnCancel.ImageOptions.SvgImage = Properties.Resources.actions_forbid;
            btnCancel.Location = new Point(12, 475);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(101, 32);
            btnCancel.TabIndex = 3;
            btnCancel.Text = "انصراف";
            btnCancel.Visible = false;
            // 
            // FrmAddVideo
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(663, 474);
            Controls.Add(btnCancel);
            Controls.Add(btnAdd);
            Controls.Add(dgvSelect);
            Controls.Add(lstChapterDetails);
            Controls.Add(lstHeaderChapters);
            Controls.Add(lueHeader);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmAddVideo";
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "افزودن فیلم";
            Load += FrmAddVideo_Load;
            ((System.ComponentModel.ISupportInitialize)lueHeader.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridLookUpEdit1View).EndInit();
            ((System.ComponentModel.ISupportInitialize)lstHeaderChapters).EndInit();
            ((System.ComponentModel.ISupportInitialize)lstChapterDetails).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSelect).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewSelect).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.GridLookUpEdit lueHeader;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.ListBoxControl lstHeaderChapters;
        private DevExpress.XtraEditors.ListBoxControl lstChapterDetails;
        private MyCom.Object.KavoshGrid dgvSelect;
        private DevExpress.XtraEditors.SimpleButton btnAdd;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewSelect;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
    }
}