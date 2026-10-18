namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmPerson
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
            layPerson = new MyCom.Object.dataLayout();
            gridPerson = new MyCom.Object.KavoshGrid(components);
            viewPerson = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            btnTakhsis = new DevExpress.XtraEditors.SimpleButton();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            ((System.ComponentModel.ISupportInitialize)gridPerson).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewPerson).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            SuspendLayout();
            // 
            // layPerson
            // 
            layPerson.Dock = DockStyle.Right;
            layPerson.Location = new Point(680, 0);
            layPerson.LookAndFeel.SkinName = "Visual Studio 2013 Blue";
            layPerson.LookAndFeel.UseDefaultLookAndFeel = false;
            layPerson.Name = "layPerson";
            layPerson.Size = new Size(396, 513);
            layPerson.TabIndex = 0;
            layPerson.Z_PanelShow = true;
            layPerson.Z_UsedDelete = false;
            layPerson.Z_UsedPrint = false;
            layPerson.BtnSaveClick += layPerson_BtnSaveClick;
            // 
            // gridPerson
            // 
            tablePanel1.SetColumn(gridPerson, 0);
            tablePanel1.SetColumnSpan(gridPerson, 2);
            gridPerson.Dock = DockStyle.Fill;
            gridPerson.Location = new Point(13, 46);
            gridPerson.MainView = viewPerson;
            gridPerson.Name = "gridPerson";
            tablePanel1.SetRow(gridPerson, 1);
            gridPerson.Size = new Size(654, 454);
            gridPerson.TabIndex = 1;
            gridPerson.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewPerson });
            // 
            // viewPerson
            // 
            viewPerson.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewPerson.GridControl = gridPerson;
            viewPerson.Name = "viewPerson";
            viewPerson.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            viewPerson.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            viewPerson.OptionsImageLoad.AnimationType = DevExpress.Utils.ImageContentAnimationType.Slide;
            viewPerson.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateAllContent;
            viewPerson.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Panel;
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // tablePanel1
            // 
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 30.07F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 29.93F) });
            tablePanel1.Controls.Add(btnTakhsis);
            tablePanel1.Controls.Add(searchControl1);
            tablePanel1.Controls.Add(gridPerson);
            tablePanel1.Dock = DockStyle.Fill;
            tablePanel1.Location = new Point(0, 0);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.RightToLeft = RightToLeft.Yes;
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 34F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F) });
            tablePanel1.Size = new Size(680, 513);
            tablePanel1.TabIndex = 2;
            tablePanel1.UseSkinIndents = true;
            // 
            // btnTakhsis
            // 
            tablePanel1.SetColumn(btnTakhsis, 1);
            btnTakhsis.Dock = DockStyle.Right;
            btnTakhsis.Enabled = false;
            btnTakhsis.Location = new Point(13, 12);
            btnTakhsis.Name = "btnTakhsis";
            tablePanel1.SetRow(btnTakhsis, 0);
            btnTakhsis.Size = new Size(199, 30);
            btnTakhsis.TabIndex = 4;
            btnTakhsis.Text = "تخصیص فلش مموری";
            btnTakhsis.Click += btnTakhsis_Click;
            // 
            // searchControl1
            // 
            searchControl1.Client = gridPerson;
            tablePanel1.SetColumn(searchControl1, 0);
            searchControl1.Dock = DockStyle.Fill;
            searchControl1.Location = new Point(341, 12);
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = gridPerson;
            tablePanel1.SetRow(searchControl1, 0);
            searchControl1.Size = new Size(326, 22);
            searchControl1.TabIndex = 2;
            // 
            // FrmPerson
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1076, 513);
            Controls.Add(tablePanel1);
            Controls.Add(layPerson);
            Name = "FrmPerson";
            RightToLeft = RightToLeft.Yes;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "پرسنل و مشتریان";
            Load += FrmPerson_Load;
            ((System.ComponentModel.ISupportInitialize)gridPerson).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewPerson).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private MyCom.Object.dataLayout layPerson;
        private MyCom.Object.KavoshGrid gridPerson;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewPerson;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.SimpleButton btnTakhsis;
    }
}