namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmManageVideo
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmManageVideo));
            DevExpress.XtraEditors.TableLayout.ItemTemplateBase itemTemplateBase2 = new DevExpress.XtraEditors.TableLayout.ItemTemplateBase();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition3 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TableColumnDefinition tableColumnDefinition4 = new DevExpress.XtraEditors.TableLayout.TableColumnDefinition();
            DevExpress.XtraEditors.TableLayout.TemplatedItemElement templatedItemElement3 = new DevExpress.XtraEditors.TableLayout.TemplatedItemElement();
            DevExpress.XtraEditors.TableLayout.TemplatedItemElement templatedItemElement4 = new DevExpress.XtraEditors.TableLayout.TemplatedItemElement();
            DevExpress.XtraEditors.TableLayout.TableRowDefinition tableRowDefinition2 = new DevExpress.XtraEditors.TableLayout.TableRowDefinition();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            splitContainerControl2 = new DevExpress.XtraEditors.SplitContainerControl();
            dgvHeaderChapter = new DevExpress.XtraGrid.GridControl();
            viewHeaderChapter = new DevExpress.XtraGrid.Views.Grid.GridView();
            ChdBtnSelect = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemSelect = new DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel();
            clmChId = new DevExpress.XtraGrid.Columns.GridColumn();
            clmChCode = new DevExpress.XtraGrid.Columns.GridColumn();
            clmChNameChapter = new DevExpress.XtraGrid.Columns.GridColumn();
            clmChNameFilm = new DevExpress.XtraGrid.Columns.GridColumn();
            cmbNameFilm = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            clmChCreated = new DevExpress.XtraGrid.Columns.GridColumn();
            cmbChCreated = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            clmChDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            memoChDescription = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
            ChdBtnCopyTo = new DevExpress.XtraGrid.Columns.GridColumn();
            repositoryItemCopyToSession = new DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel();
            panelSelFile = new DevExpress.XtraBars.Navigation.NavigationPane();
            navigationPage3 = new DevExpress.XtraBars.Navigation.NavigationPage();
            lstSelFiles = new DevExpress.XtraEditors.ListBoxControl();
            htmlTemplate1 = new DevExpress.Utils.Html.HtmlTemplate();
            lblSelChapterHeader = new DevExpress.XtraEditors.LabelControl();
            btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            btnCreatPart = new DevExpress.XtraEditors.SimpleButton();
            btnAddRowChapterDetail = new DevExpress.XtraEditors.SimpleButton();
            dgvChapterDetail = new MyCom.Object.KavoshGrid(components);
            viewChapterDetail = new DevExpress.XtraGrid.Views.BandedGrid.BandedGridView();
            gridBand1 = new DevExpress.XtraGrid.Views.BandedGrid.GridBand();
            barManager1 = new DevExpress.XtraBars.BarManager(components);
            barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            mnubarAddPersian = new DevExpress.XtraBars.BarButtonItem();
            mnubarAddHardSub = new DevExpress.XtraBars.BarButtonItem();
            lblDetailsChapter = new DevExpress.XtraEditors.LabelControl();
            layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            barDownload = new DevExpress.XtraEditors.ProgressBarControl();
            progreesDownload = new DevExpress.XtraEditors.ProgressBarControl();
            lblStar = new DevExpress.XtraEditors.LabelControl();
            btnFindInfoFilm = new DevExpress.XtraEditors.SimpleButton();
            cmbWeekOfDay = new DevExpress.XtraEditors.LookUpEdit();
            tknActor = new DevExpress.XtraEditors.TokenEdit();
            cmbPrice = new DevExpress.XtraEditors.GridLookUpEdit();
            gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            cmbYear = new DevExpress.XtraEditors.LookUpEdit();
            pnlGener = new DevExpress.XtraEditors.PanelControl();
            button2 = new Button();
            lstGener = new DevExpress.XtraEditors.CheckedListBoxControl();
            schGener = new DevExpress.XtraEditors.SearchControl();
            labelControl1 = new DevExpress.XtraEditors.LabelControl();
            btnCancel = new Button();
            btnSave = new Button();
            picVideo = new DevExpress.XtraEditors.PictureEdit();
            rdoAnimeted = new DevExpress.XtraEditors.RadioGroup();
            rdoAge = new DevExpress.XtraEditors.RadioGroup();
            rdoType = new DevExpress.XtraEditors.RadioGroup();
            cmbCountry = new DevExpress.XtraEditors.GridLookUpEdit();
            gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            txtEnglishName = new DevExpress.XtraEditors.TextEdit();
            txtPersianName = new DevExpress.XtraEditors.TextEdit();
            txtCode = new DevExpress.XtraEditors.TextEdit();
            txtID = new DevExpress.XtraEditors.TextEdit();
            Root = new DevExpress.XtraLayout.LayoutControlGroup();
            layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            itemWeekOfDay = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            controleritem55 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem17 = new DevExpress.XtraLayout.LayoutControlItem();
            layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            panel1 = new Panel();
            lblSelMovie = new Label();
            lblRight = new Label();
            btnNew = new DevExpress.XtraEditors.SimpleButton();
            lblLeft = new Label();
            navigationPane1 = new DevExpress.XtraBars.Navigation.NavigationPane();
            navigationPage1 = new DevExpress.XtraBars.Navigation.NavigationPage();
            searchControl1 = new DevExpress.XtraEditors.SearchControl();
            dgvHeader = new DevExpress.XtraGrid.GridControl();
            viewHeader = new DevExpress.XtraGrid.Views.Grid.GridView();
            انتخاب = new DevExpress.XtraGrid.Columns.GridColumn();
            btnSelect = new DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel();
            clmID = new DevExpress.XtraGrid.Columns.GridColumn();
            clmCode = new DevExpress.XtraGrid.Columns.GridColumn();
            clmName = new DevExpress.XtraGrid.Columns.GridColumn();
            clmCreated = new DevExpress.XtraGrid.Columns.GridColumn();
            clmGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            clmIsActive = new DevExpress.XtraGrid.Columns.GridColumn();
            حذف = new DevExpress.XtraGrid.Columns.GridColumn();
            btnRemove = new DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel();
            clmAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            navigationPage2 = new DevExpress.XtraBars.Navigation.NavigationPage();
            lstOldFilm = new DevExpress.XtraEditors.ListBoxControl();
            panel2 = new Panel();
            searchControl2 = new DevExpress.XtraEditors.SearchControl();
            popMenuChapterDetails = new DevExpress.XtraBars.PopupMenu(components);
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
            ((System.ComponentModel.ISupportInitialize)dgvHeaderChapter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewHeaderChapter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSelect).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbNameFilm).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbChCreated).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memoChDescription).BeginInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCopyToSession).BeginInit();
            ((System.ComponentModel.ISupportInitialize)panelSelFile).BeginInit();
            panelSelFile.SuspendLayout();
            navigationPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lstSelFiles).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvChapterDetail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewChapterDetail).BeginInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).BeginInit();
            layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)barDownload.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)progreesDownload.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbWeekOfDay.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tknActor.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbPrice.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbYear.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pnlGener).BeginInit();
            pnlGener.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lstGener).BeginInit();
            ((System.ComponentModel.ISupportInitialize)schGener.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)picVideo.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rdoAnimeted.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rdoAge.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)rdoType.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)cmbCountry.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)gridLookUpEdit1View).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtEnglishName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPersianName.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtCode.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtID.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)Root).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).BeginInit();
            ((System.ComponentModel.ISupportInitialize)itemWeekOfDay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).BeginInit();
            ((System.ComponentModel.ISupportInitialize)controleritem55).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).BeginInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)navigationPane1).BeginInit();
            navigationPane1.SuspendLayout();
            navigationPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)viewHeader).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnSelect).BeginInit();
            ((System.ComponentModel.ISupportInitialize)btnRemove).BeginInit();
            navigationPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)lstOldFilm).BeginInit();
            panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)searchControl2.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)popMenuChapterDetails).BeginInit();
            SuspendLayout();
            // 
            // splitContainerControl1
            // 
            splitContainerControl1.Dock = DockStyle.Fill;
            splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            splitContainerControl1.IsSplitterFixed = true;
            splitContainerControl1.Location = new Point(0, 0);
            splitContainerControl1.Name = "splitContainerControl1";
            splitContainerControl1.Padding = new Padding(0, 0, 62, 0);
            // 
            // splitContainerControl1.Panel1
            // 
            splitContainerControl1.Panel1.Appearance.BackColor = Color.FromArgb(192, 255, 255);
            splitContainerControl1.Panel1.Appearance.Options.UseBackColor = true;
            splitContainerControl1.Panel1.Controls.Add(splitContainerControl2);
            splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            splitContainerControl1.Panel2.Appearance.BackColor = Color.FromArgb(255, 192, 192);
            splitContainerControl1.Panel2.Appearance.Options.UseBackColor = true;
            splitContainerControl1.Panel2.Controls.Add(layoutControl1);
            splitContainerControl1.Panel2.Controls.Add(panel1);
            splitContainerControl1.Panel2.Text = "Panel2";
            splitContainerControl1.Size = new Size(1313, 657);
            splitContainerControl1.SplitterPosition = 601;
            splitContainerControl1.TabIndex = 0;
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
            splitContainerControl2.Panel1.Controls.Add(dgvHeaderChapter);
            splitContainerControl2.Panel1.Controls.Add(panelSelFile);
            splitContainerControl2.Panel1.Controls.Add(lblSelChapterHeader);
            splitContainerControl2.Panel1.Text = "Panel1";
            // 
            // splitContainerControl2.Panel2
            // 
            splitContainerControl2.Panel2.Controls.Add(btnRefresh);
            splitContainerControl2.Panel2.Controls.Add(btnCreatPart);
            splitContainerControl2.Panel2.Controls.Add(btnAddRowChapterDetail);
            splitContainerControl2.Panel2.Controls.Add(dgvChapterDetail);
            splitContainerControl2.Panel2.Controls.Add(lblDetailsChapter);
            splitContainerControl2.Panel2.Text = "Panel2";
            splitContainerControl2.Size = new Size(638, 657);
            splitContainerControl2.SplitterPosition = 260;
            splitContainerControl2.TabIndex = 0;
            // 
            // dgvHeaderChapter
            // 
            dgvHeaderChapter.Dock = DockStyle.Fill;
            dgvHeaderChapter.Location = new Point(59, 23);
            dgvHeaderChapter.MainView = viewHeaderChapter;
            dgvHeaderChapter.Name = "dgvHeaderChapter";
            dgvHeaderChapter.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { cmbNameFilm, memoChDescription, repositoryItemSelect, repositoryItemCopyToSession, cmbChCreated });
            dgvHeaderChapter.RightToLeft = RightToLeft.Yes;
            dgvHeaderChapter.Size = new Size(579, 237);
            dgvHeaderChapter.TabIndex = 2;
            dgvHeaderChapter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewHeaderChapter });
            // 
            // viewHeaderChapter
            // 
            viewHeaderChapter.Appearance.CustomizationFormHint.Font = new Font("Samim FD", 11.25F);
            viewHeaderChapter.Appearance.CustomizationFormHint.Options.UseFont = true;
            viewHeaderChapter.Appearance.DetailTip.Font = new Font("Samim FD", 11.25F);
            viewHeaderChapter.Appearance.DetailTip.Options.UseFont = true;
            viewHeaderChapter.Appearance.EvenRow.Font = new Font("Samim FD", 11.25F);
            viewHeaderChapter.Appearance.EvenRow.Options.UseFont = true;
            viewHeaderChapter.Appearance.FilterPanel.Font = new Font("Samim FD", 11.25F);
            viewHeaderChapter.Appearance.FilterPanel.Options.UseFont = true;
            viewHeaderChapter.Appearance.FocusedCell.Font = new Font("Samim FD", 11.25F);
            viewHeaderChapter.Appearance.FocusedCell.Options.UseFont = true;
            viewHeaderChapter.Appearance.FocusedRow.Font = new Font("Samim FD", 11.25F);
            viewHeaderChapter.Appearance.FocusedRow.Options.UseFont = true;
            viewHeaderChapter.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { ChdBtnSelect, clmChId, clmChCode, clmChNameChapter, clmChNameFilm, clmChCreated, clmChDescription, ChdBtnCopyTo });
            viewHeaderChapter.GridControl = dgvHeaderChapter;
            viewHeaderChapter.Name = "viewHeaderChapter";
            viewHeaderChapter.NewItemRowText = "مشخصات";
            viewHeaderChapter.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            viewHeaderChapter.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.True;
            viewHeaderChapter.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            viewHeaderChapter.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            viewHeaderChapter.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.EditForm;
            viewHeaderChapter.OptionsBehavior.FocusLeaveOnTab = true;
            viewHeaderChapter.OptionsEditForm.ActionOnModifiedRowChange = DevExpress.XtraGrid.Views.Grid.EditFormModifiedAction.Save;
            viewHeaderChapter.OptionsEditForm.BindingMode = DevExpress.XtraGrid.Views.Grid.EditFormBindingMode.Direct;
            viewHeaderChapter.OptionsEditForm.ShowOnDoubleClick = DevExpress.Utils.DefaultBoolean.True;
            viewHeaderChapter.OptionsEditForm.ShowUpdateCancelPanel = DevExpress.Utils.DefaultBoolean.True;
            viewHeaderChapter.OptionsImageLoad.AnimationType = DevExpress.Utils.ImageContentAnimationType.Slide;
            viewHeaderChapter.OptionsImageLoad.AsyncLoad = true;
            viewHeaderChapter.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            viewHeaderChapter.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Top;
            viewHeaderChapter.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedRow;
            viewHeaderChapter.OptionsView.ShowGroupPanel = false;
            viewHeaderChapter.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator;
            // 
            // ChdBtnSelect
            // 
            ChdBtnSelect.ColumnEdit = repositoryItemSelect;
            ChdBtnSelect.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            ChdBtnSelect.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("ChdBtnSelect.ImageOptions.SvgImage");
            ChdBtnSelect.ImageOptions.SvgImageSize = new Size(22, 22);
            ChdBtnSelect.Name = "ChdBtnSelect";
            ChdBtnSelect.OptionsColumn.FixedWidth = true;
            ChdBtnSelect.Visible = true;
            ChdBtnSelect.VisibleIndex = 0;
            ChdBtnSelect.Width = 35;
            // 
            // repositoryItemSelect
            // 
            repositoryItemSelect.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemSelect.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemSelect.ContextImageOptions.AllowChangeAnimation = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemSelect.ContextImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemSelect.ContextImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("repositoryItemSelect.ContextImageOptions.SvgImage");
            repositoryItemSelect.ContextImageOptions.SvgImageSize = new Size(24, 24);
            repositoryItemSelect.Name = "repositoryItemSelect";
            // 
            // clmChId
            // 
            clmChId.Caption = "clmId";
            clmChId.FieldName = "clmId";
            clmChId.Name = "clmChId";
            clmChId.OptionsColumn.ReadOnly = true;
            clmChId.Visible = true;
            clmChId.VisibleIndex = 1;
            clmChId.Width = 83;
            // 
            // clmChCode
            // 
            clmChCode.AppearanceCell.Font = new Font("Samim FD", 12F, FontStyle.Bold);
            clmChCode.AppearanceCell.ForeColor = Color.Red;
            clmChCode.AppearanceCell.Options.UseFont = true;
            clmChCode.AppearanceCell.Options.UseForeColor = true;
            clmChCode.AppearanceCell.Options.UseTextOptions = true;
            clmChCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            clmChCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            clmChCode.Caption = "شماره فصل";
            clmChCode.FieldName = "clmCode";
            clmChCode.Name = "clmChCode";
            clmChCode.OptionsColumn.ReadOnly = true;
            clmChCode.Visible = true;
            clmChCode.VisibleIndex = 2;
            clmChCode.Width = 83;
            // 
            // clmChNameChapter
            // 
            clmChNameChapter.Caption = "نام فصل";
            clmChNameChapter.FieldName = "clmNameChapter";
            clmChNameChapter.Name = "clmChNameChapter";
            clmChNameChapter.Visible = true;
            clmChNameChapter.VisibleIndex = 3;
            clmChNameChapter.Width = 83;
            // 
            // clmChNameFilm
            // 
            clmChNameFilm.Caption = "نام فیلم";
            clmChNameFilm.ColumnEdit = cmbNameFilm;
            clmChNameFilm.FieldName = "cmbNameFilm";
            clmChNameFilm.Name = "clmChNameFilm";
            clmChNameFilm.OptionsColumn.ReadOnly = true;
            clmChNameFilm.Visible = true;
            clmChNameFilm.VisibleIndex = 4;
            clmChNameFilm.Width = 83;
            // 
            // cmbNameFilm
            // 
            cmbNameFilm.Appearance.Font = new Font("Samim FD", 9.75F);
            cmbNameFilm.Appearance.Options.UseFont = true;
            cmbNameFilm.AutoHeight = false;
            cmbNameFilm.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbNameFilm.DisplayMember = "Title";
            cmbNameFilm.Name = "cmbNameFilm";
            cmbNameFilm.PopupView = gridView2;
            cmbNameFilm.ReadOnly = true;
            cmbNameFilm.ValueMember = "Id";
            // 
            // gridView2
            // 
            gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView2.Name = "gridView2";
            gridView2.OptionsFind.AlwaysVisible = true;
            gridView2.OptionsFind.FindDelay = 100;
            gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView2.OptionsView.ShowGroupPanel = false;
            // 
            // clmChCreated
            // 
            clmChCreated.Caption = "انتشار";
            clmChCreated.ColumnEdit = cmbChCreated;
            clmChCreated.FieldName = "cmbChCreated";
            clmChCreated.Name = "clmChCreated";
            clmChCreated.Visible = true;
            clmChCreated.VisibleIndex = 5;
            clmChCreated.Width = 83;
            // 
            // cmbChCreated
            // 
            cmbChCreated.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            cmbChCreated.Appearance.Options.UseTextOptions = true;
            cmbChCreated.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cmbChCreated.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            cmbChCreated.AutoHeight = false;
            cmbChCreated.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            cmbChCreated.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbChCreated.Name = "cmbChCreated";
            cmbChCreated.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cmbChCreated.PopupWidthMode = DevExpress.XtraEditors.PopupWidthMode.UseEditorWidth;
            cmbChCreated.SearchMode = DevExpress.XtraEditors.Controls.SearchMode.AutoSuggest;
            cmbChCreated.ShowFooter = false;
            cmbChCreated.ShowHeader = false;
            // 
            // clmChDescription
            // 
            clmChDescription.Caption = "توضیحات";
            clmChDescription.ColumnEdit = memoChDescription;
            clmChDescription.FieldName = "clmDescription";
            clmChDescription.Name = "clmChDescription";
            clmChDescription.Visible = true;
            clmChDescription.VisibleIndex = 6;
            clmChDescription.Width = 107;
            // 
            // memoChDescription
            // 
            memoChDescription.Appearance.Font = new Font("Samim FD", 11.25F);
            memoChDescription.Appearance.Options.UseFont = true;
            memoChDescription.MaxLength = 300;
            memoChDescription.Name = "memoChDescription";
            // 
            // ChdBtnCopyTo
            // 
            ChdBtnCopyTo.ColumnEdit = repositoryItemCopyToSession;
            ChdBtnCopyTo.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            ChdBtnCopyTo.Name = "ChdBtnCopyTo";
            ChdBtnCopyTo.Visible = true;
            ChdBtnCopyTo.VisibleIndex = 7;
            ChdBtnCopyTo.Width = 35;
            // 
            // repositoryItemCopyToSession
            // 
            repositoryItemCopyToSession.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemCopyToSession.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemCopyToSession.ContextImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            repositoryItemCopyToSession.ContextImageOptions.SvgImage = Properties.Resources.separatorlist;
            repositoryItemCopyToSession.ContextImageOptions.SvgImageSize = new Size(24, 24);
            repositoryItemCopyToSession.Name = "repositoryItemCopyToSession";
            // 
            // panelSelFile
            // 
            panelSelFile.AllowHtmlDraw = true;
            panelSelFile.AllowResize = false;
            panelSelFile.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.True;
            panelSelFile.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            panelSelFile.Appearance.Options.UseFont = true;
            panelSelFile.AppearanceButton.Normal.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            panelSelFile.AppearanceButton.Normal.Options.UseFont = true;
            panelSelFile.Controls.Add(navigationPage3);
            panelSelFile.Dock = DockStyle.Left;
            panelSelFile.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            panelSelFile.Location = new Point(0, 23);
            panelSelFile.Name = "panelSelFile";
            panelSelFile.PageProperties.AppearanceCaption.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            panelSelFile.PageProperties.AppearanceCaption.Options.UseFont = true;
            panelSelFile.PageProperties.AppearanceCaption.Options.UseTextOptions = true;
            panelSelFile.PageProperties.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            panelSelFile.PageProperties.AppearanceCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            panelSelFile.PageProperties.ShowCollapseButton = false;
            panelSelFile.PageProperties.ShowExpandButton = false;
            panelSelFile.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { navigationPage3 });
            panelSelFile.RegularSize = new Size(366, 237);
            panelSelFile.SelectedPage = navigationPage3;
            panelSelFile.Size = new Size(59, 237);
            panelSelFile.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed;
            panelSelFile.TabIndex = 3;
            panelSelFile.Text = "انتخاب شده";
            // 
            // navigationPage3
            // 
            navigationPage3.Appearance.BackColor = Color.Turquoise;
            navigationPage3.Appearance.Options.UseBackColor = true;
            navigationPage3.Caption = "انتخاب شده";
            navigationPage3.Controls.Add(lstSelFiles);
            navigationPage3.ImageOptions.SvgImage = Properties.Resources.glyph_video;
            navigationPage3.Name = "navigationPage3";
            navigationPage3.Size = new Size(0, 0);
            // 
            // lstSelFiles
            // 
            lstSelFiles.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lstSelFiles.Appearance.Options.UseFont = true;
            lstSelFiles.Dock = DockStyle.Fill;
            lstSelFiles.HtmlTemplates.AddRange(new DevExpress.Utils.Html.HtmlTemplate[] { htmlTemplate1 });
            lstSelFiles.ItemHeight = 30;
            lstSelFiles.Location = new Point(0, 0);
            lstSelFiles.Name = "lstSelFiles";
            lstSelFiles.Size = new Size(0, 0);
            lstSelFiles.SortOrder = SortOrder.Ascending;
            lstSelFiles.TabIndex = 0;
            tableColumnDefinition3.Length.Value = 73D;
            tableColumnDefinition4.Length.Value = 204D;
            itemTemplateBase2.Columns.Add(tableColumnDefinition3);
            itemTemplateBase2.Columns.Add(tableColumnDefinition4);
            templatedItemElement3.FieldName = "lblRow";
            templatedItemElement3.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            templatedItemElement3.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            templatedItemElement3.Name = "lblRow";
            templatedItemElement3.Text = "lblRow";
            templatedItemElement3.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            templatedItemElement4.ColumnIndex = 1;
            templatedItemElement4.FieldName = "lblItem";
            templatedItemElement4.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            templatedItemElement4.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            templatedItemElement4.Name = "lblItem";
            templatedItemElement4.Text = "lblItem";
            templatedItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleCenter;
            itemTemplateBase2.Elements.Add(templatedItemElement3);
            itemTemplateBase2.Elements.Add(templatedItemElement4);
            itemTemplateBase2.Name = "template1";
            itemTemplateBase2.Rows.Add(tableRowDefinition2);
            lstSelFiles.Templates.Add(itemTemplateBase2);
            // 
            // htmlTemplate1
            // 
            htmlTemplate1.Name = "htmlTemplate1";
            // 
            // lblSelChapterHeader
            // 
            lblSelChapterHeader.Appearance.Font = new Font("Samim FD", 11.25F);
            lblSelChapterHeader.Appearance.Options.UseFont = true;
            lblSelChapterHeader.Appearance.Options.UseTextOptions = true;
            lblSelChapterHeader.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblSelChapterHeader.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblSelChapterHeader.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblSelChapterHeader.Dock = DockStyle.Top;
            lblSelChapterHeader.Location = new Point(0, 0);
            lblSelChapterHeader.Name = "lblSelChapterHeader";
            lblSelChapterHeader.Size = new Size(638, 23);
            lblSelChapterHeader.TabIndex = 1;
            lblSelChapterHeader.Text = "فصل ها";
            // 
            // btnRefresh
            // 
            btnRefresh.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnRefresh.ImageOptions.SvgImage = Properties.Resources.actions_reload;
            btnRefresh.ImageOptions.SvgImageSize = new Size(25, 25);
            btnRefresh.Location = new Point(1, -1);
            btnRefresh.Name = "btnRefresh";
            btnRefresh.Size = new Size(67, 26);
            btnRefresh.TabIndex = 16;
            btnRefresh.Click += btnRefresh_Click;
            // 
            // btnCreatPart
            // 
            btnCreatPart.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnCreatPart.Enabled = false;
            btnCreatPart.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnCreatPart.ImageOptions.SvgImage = Properties.Resources.actions_addcircled;
            btnCreatPart.ImageOptions.SvgImageSize = new Size(25, 25);
            btnCreatPart.Location = new Point(485, -1);
            btnCreatPart.Name = "btnCreatPart";
            btnCreatPart.Size = new Size(67, 26);
            btnCreatPart.TabIndex = 14;
            btnCreatPart.Click += btnCreatPart_Click;
            // 
            // btnAddRowChapterDetail
            // 
            btnAddRowChapterDetail.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnAddRowChapterDetail.Enabled = false;
            btnAddRowChapterDetail.ImageOptions.Location = DevExpress.XtraEditors.ImageLocation.MiddleCenter;
            btnAddRowChapterDetail.ImageOptions.SvgImage = Properties.Resources.actions_addcircled;
            btnAddRowChapterDetail.ImageOptions.SvgImageSize = new Size(25, 25);
            btnAddRowChapterDetail.Location = new Point(570, 0);
            btnAddRowChapterDetail.Name = "btnAddRowChapterDetail";
            btnAddRowChapterDetail.Size = new Size(67, 26);
            btnAddRowChapterDetail.TabIndex = 14;
            btnAddRowChapterDetail.Click += btnAddRowChapterDetail_Click;
            // 
            // dgvChapterDetail
            // 
            dgvChapterDetail.Dock = DockStyle.Fill;
            dgvChapterDetail.Enabled = false;
            dgvChapterDetail.Location = new Point(0, 25);
            dgvChapterDetail.MainView = viewChapterDetail;
            dgvChapterDetail.MenuManager = barManager1;
            dgvChapterDetail.Name = "dgvChapterDetail";
            dgvChapterDetail.RightToLeft = RightToLeft.Yes;
            dgvChapterDetail.Size = new Size(638, 360);
            dgvChapterDetail.TabIndex = 15;
            dgvChapterDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewChapterDetail });
            // 
            // viewChapterDetail
            // 
            viewChapterDetail.Bands.AddRange(new DevExpress.XtraGrid.Views.BandedGrid.GridBand[] { gridBand1 });
            viewChapterDetail.GridControl = dgvChapterDetail;
            viewChapterDetail.Name = "viewChapterDetail";
            // 
            // gridBand1
            // 
            gridBand1.Caption = "gridBand1";
            gridBand1.Name = "gridBand1";
            gridBand1.VisibleIndex = 0;
            // 
            // barManager1
            // 
            barManager1.DockControls.Add(barDockControlTop);
            barManager1.DockControls.Add(barDockControlBottom);
            barManager1.DockControls.Add(barDockControlLeft);
            barManager1.DockControls.Add(barDockControlRight);
            barManager1.Form = this;
            barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] { mnubarAddPersian, mnubarAddHardSub });
            barManager1.MaxItemId = 2;
            barManager1.RightToLeft = DevExpress.Utils.DefaultBoolean.True;
            // 
            // barDockControlTop
            // 
            barDockControlTop.CausesValidation = false;
            barDockControlTop.Dock = DockStyle.Top;
            barDockControlTop.Location = new Point(0, 0);
            barDockControlTop.Manager = barManager1;
            barDockControlTop.Size = new Size(1313, 0);
            // 
            // barDockControlBottom
            // 
            barDockControlBottom.CausesValidation = false;
            barDockControlBottom.Dock = DockStyle.Bottom;
            barDockControlBottom.Location = new Point(0, 657);
            barDockControlBottom.Manager = barManager1;
            barDockControlBottom.Size = new Size(1313, 0);
            // 
            // barDockControlLeft
            // 
            barDockControlLeft.CausesValidation = false;
            barDockControlLeft.Dock = DockStyle.Left;
            barDockControlLeft.Location = new Point(0, 0);
            barDockControlLeft.Manager = barManager1;
            barDockControlLeft.Size = new Size(0, 657);
            // 
            // barDockControlRight
            // 
            barDockControlRight.CausesValidation = false;
            barDockControlRight.Dock = DockStyle.Right;
            barDockControlRight.Location = new Point(1313, 0);
            barDockControlRight.Manager = barManager1;
            barDockControlRight.Size = new Size(0, 657);
            // 
            // mnubarAddPersian
            // 
            mnubarAddPersian.Caption = "دوبله فارسی";
            mnubarAddPersian.Id = 0;
            mnubarAddPersian.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("mnubarAddPersian.ImageOptions.SvgImage");
            mnubarAddPersian.ImageOptions.SvgImageSize = new Size(30, 30);
            mnubarAddPersian.ItemAppearance.Normal.Font = new Font("Samim FD", 11.25F);
            mnubarAddPersian.ItemAppearance.Normal.Options.UseFont = true;
            mnubarAddPersian.ItemInMenuAppearance.Normal.Font = new Font("Samim FD", 11.25F);
            mnubarAddPersian.ItemInMenuAppearance.Normal.Options.UseFont = true;
            mnubarAddPersian.Name = "mnubarAddPersian";
            // 
            // mnubarAddHardSub
            // 
            mnubarAddHardSub.Caption = "زیر نویس (زبان اصلی)";
            mnubarAddHardSub.Id = 1;
            mnubarAddHardSub.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("mnubarAddHardSub.ImageOptions.SvgImage");
            mnubarAddHardSub.ImageOptions.SvgImageSize = new Size(30, 30);
            mnubarAddHardSub.ItemAppearance.Normal.Font = new Font("Samim FD", 11.25F);
            mnubarAddHardSub.ItemAppearance.Normal.Options.UseFont = true;
            mnubarAddHardSub.ItemInMenuAppearance.Normal.Font = new Font("Samim FD", 11.25F);
            mnubarAddHardSub.ItemInMenuAppearance.Normal.Options.UseFont = true;
            mnubarAddHardSub.Name = "mnubarAddHardSub";
            // 
            // lblDetailsChapter
            // 
            lblDetailsChapter.Appearance.Font = new Font("Samim FD", 11.25F);
            lblDetailsChapter.Appearance.Options.UseFont = true;
            lblDetailsChapter.Appearance.Options.UseTextOptions = true;
            lblDetailsChapter.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblDetailsChapter.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblDetailsChapter.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblDetailsChapter.Dock = DockStyle.Top;
            lblDetailsChapter.Location = new Point(0, 0);
            lblDetailsChapter.Name = "lblDetailsChapter";
            lblDetailsChapter.Size = new Size(638, 25);
            lblDetailsChapter.TabIndex = 5;
            lblDetailsChapter.Text = "قسمت ها";
            // 
            // layoutControl1
            // 
            layoutControl1.Controls.Add(barDownload);
            layoutControl1.Controls.Add(progreesDownload);
            layoutControl1.Controls.Add(lblStar);
            layoutControl1.Controls.Add(btnFindInfoFilm);
            layoutControl1.Controls.Add(cmbWeekOfDay);
            layoutControl1.Controls.Add(tknActor);
            layoutControl1.Controls.Add(cmbPrice);
            layoutControl1.Controls.Add(cmbYear);
            layoutControl1.Controls.Add(pnlGener);
            layoutControl1.Controls.Add(btnCancel);
            layoutControl1.Controls.Add(btnSave);
            layoutControl1.Controls.Add(picVideo);
            layoutControl1.Controls.Add(rdoAnimeted);
            layoutControl1.Controls.Add(rdoAge);
            layoutControl1.Controls.Add(rdoType);
            layoutControl1.Controls.Add(cmbCountry);
            layoutControl1.Controls.Add(txtEnglishName);
            layoutControl1.Controls.Add(txtPersianName);
            layoutControl1.Controls.Add(txtCode);
            layoutControl1.Controls.Add(txtID);
            layoutControl1.Dock = DockStyle.Fill;
            layoutControl1.Location = new Point(0, 39);
            layoutControl1.Name = "layoutControl1";
            layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new Rectangle(1108, 241, 682, 697);
            layoutControl1.OptionsView.RightToLeftMirroringApplied = true;
            layoutControl1.RightToLeft = RightToLeft.Yes;
            layoutControl1.Root = Root;
            layoutControl1.Size = new Size(601, 618);
            layoutControl1.TabIndex = 0;
            layoutControl1.Text = "layoutControl1";
            // 
            // barDownload
            // 
            barDownload.Location = new Point(136, 585);
            barDownload.MenuManager = barManager1;
            barDownload.Name = "barDownload";
            barDownload.Size = new Size(156, 18);
            barDownload.StyleController = layoutControl1;
            barDownload.TabIndex = 20;
            // 
            // progreesDownload
            // 
            progreesDownload.Location = new Point(296, 565);
            progreesDownload.MenuManager = barManager1;
            progreesDownload.Name = "progreesDownload";
            progreesDownload.Size = new Size(156, 38);
            progreesDownload.StyleController = layoutControl1;
            progreesDownload.TabIndex = 19;
            // 
            // lblStar
            // 
            lblStar.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.True;
            lblStar.Appearance.Font = new Font("Samim", 12F, FontStyle.Bold);
            lblStar.Appearance.Options.UseFont = true;
            lblStar.Appearance.Options.UseTextOptions = true;
            lblStar.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            lblStar.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            lblStar.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            lblStar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            lblStar.Location = new Point(144, 47);
            lblStar.Name = "lblStar";
            lblStar.Size = new Size(57, 60);
            lblStar.StyleController = layoutControl1;
            lblStar.TabIndex = 18;
            // 
            // btnFindInfoFilm
            // 
            btnFindInfoFilm.Appearance.Font = new Font("Samim FD", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnFindInfoFilm.Appearance.Options.UseFont = true;
            btnFindInfoFilm.ImageOptions.Image = Properties.Resources.lookup_reference_32x32;
            btnFindInfoFilm.Location = new Point(15, 47);
            btnFindInfoFilm.Name = "btnFindInfoFilm";
            btnFindInfoFilm.Size = new Size(125, 60);
            btnFindInfoFilm.StyleController = layoutControl1;
            btnFindInfoFilm.TabIndex = 17;
            btnFindInfoFilm.Text = "جستجو ...";
            btnFindInfoFilm.Click += btnFindInfoFilm_Click;
            // 
            // cmbWeekOfDay
            // 
            cmbWeekOfDay.Location = new Point(15, 309);
            cmbWeekOfDay.MenuManager = barManager1;
            cmbWeekOfDay.Name = "cmbWeekOfDay";
            cmbWeekOfDay.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            cmbWeekOfDay.Properties.Appearance.Options.UseFont = true;
            cmbWeekOfDay.Properties.AppearanceDropDown.Font = new Font("Samim FD", 9.75F);
            cmbWeekOfDay.Properties.AppearanceDropDown.Options.UseFont = true;
            cmbWeekOfDay.Properties.AppearanceDropDownHeader.Font = new Font("Samim FD", 9.75F);
            cmbWeekOfDay.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            cmbWeekOfDay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbWeekOfDay.Properties.NullText = "روز انتشار ؟ (مثلا شنبه)";
            cmbWeekOfDay.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cmbWeekOfDay.Size = new Size(472, 28);
            cmbWeekOfDay.StyleController = layoutControl1;
            cmbWeekOfDay.TabIndex = 12;
            // 
            // tknActor
            // 
            tknActor.Location = new Point(15, 341);
            tknActor.Name = "tknActor";
            tknActor.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            tknActor.Properties.Appearance.Options.UseFont = true;
            tknActor.Properties.AppearanceDisabled.Font = new Font("Samim FD", 9.75F);
            tknActor.Properties.AppearanceDisabled.Options.UseFont = true;
            tknActor.Properties.AppearanceDropDown.Font = new Font("Samim FD", 9.75F);
            tknActor.Properties.AppearanceDropDown.Options.UseFont = true;
            tknActor.Properties.AppearanceFocused.Font = new Font("Samim FD", 9.75F);
            tknActor.Properties.AppearanceFocused.Options.UseFont = true;
            tknActor.Properties.CheckMode = DevExpress.XtraEditors.TokenEditCheckMode.Multiple;
            tknActor.Properties.PopupFilterMode = DevExpress.XtraEditors.TokenEditPopupFilterMode.Contains;
            tknActor.Properties.Separators.AddRange(new string[] { "," });
            tknActor.Properties.Tokens.Add(new DevExpress.XtraEditors.TokenEditToken("Value1", "Value1"));
            tknActor.Properties.Tokens.Add(new DevExpress.XtraEditors.TokenEditToken("Value2", "Value2"));
            tknActor.Properties.Tokens.Add(new DevExpress.XtraEditors.TokenEditToken("Value3", "Value3"));
            tknActor.Properties.Tokens.Add(new DevExpress.XtraEditors.TokenEditToken("Value4", "Value4"));
            tknActor.Properties.Tokens.Add(new DevExpress.XtraEditors.TokenEditToken("Value5", "Value5"));
            tknActor.Properties.Tokens.Add(new DevExpress.XtraEditors.TokenEditToken("Value6", "Value6"));
            tknActor.Size = new Size(472, 28);
            tknActor.StyleController = layoutControl1;
            tknActor.TabIndex = 13;
            tknActor.SelectedItemsChanged += tknActor_SelectedItemsChanged;
            tknActor.MouseUp += tknActor_MouseUp;
            // 
            // cmbPrice
            // 
            cmbPrice.Location = new Point(179, 111);
            cmbPrice.Name = "cmbPrice";
            cmbPrice.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            cmbPrice.Properties.AdvancedModeOptions.AllowSelectionAnimation = DevExpress.Utils.DefaultBoolean.True;
            cmbPrice.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            cmbPrice.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            cmbPrice.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            cmbPrice.Properties.Appearance.Options.UseFont = true;
            cmbPrice.Properties.Appearance.Options.UseTextOptions = true;
            cmbPrice.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cmbPrice.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            cmbPrice.Properties.AppearanceDisabled.Font = new Font("Samim FD", 9.75F);
            cmbPrice.Properties.AppearanceDisabled.Options.UseFont = true;
            cmbPrice.Properties.AppearanceDropDown.Font = new Font("Samim FD", 9.75F);
            cmbPrice.Properties.AppearanceDropDown.Options.UseFont = true;
            cmbPrice.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbPrice.Properties.ContextImageOptions.AllowChangeAnimation = DevExpress.Utils.DefaultBoolean.True;
            cmbPrice.Properties.DisplayFormat.FormatString = "N0";
            cmbPrice.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            cmbPrice.Properties.NullText = "مبلغ";
            cmbPrice.Properties.PopupView = gridView1;
            cmbPrice.Size = new Size(120, 28);
            cmbPrice.StyleController = layoutControl1;
            cmbPrice.TabIndex = 6;
            // 
            // gridView1
            // 
            gridView1.Appearance.Row.Font = new Font("Samim FD", 9.75F);
            gridView1.Appearance.Row.Options.UseFont = true;
            gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridView1.Name = "gridView1";
            gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridView1.OptionsView.ShowGroupPanel = false;
            // 
            // cmbYear
            // 
            cmbYear.Location = new Point(180, 243);
            cmbYear.Name = "cmbYear";
            cmbYear.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            cmbYear.Properties.Appearance.Options.UseFont = true;
            cmbYear.Properties.Appearance.Options.UseTextOptions = true;
            cmbYear.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            cmbYear.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            cmbYear.Properties.AppearanceDropDown.Font = new Font("Samim FD", 9.75F);
            cmbYear.Properties.AppearanceDropDown.Options.UseFont = true;
            cmbYear.Properties.AppearanceDropDownHeader.Font = new Font("Samim FD", 9.75F);
            cmbYear.Properties.AppearanceDropDownHeader.Options.UseFont = true;
            cmbYear.Properties.AppearanceFocused.Font = new Font("Samim FD", 9.75F);
            cmbYear.Properties.AppearanceFocused.Options.UseFont = true;
            cmbYear.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbYear.Properties.NullText = "سال ساخت";
            cmbYear.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            cmbYear.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cmbYear.RightToLeft = RightToLeft.No;
            cmbYear.Size = new Size(307, 28);
            cmbYear.StyleController = layoutControl1;
            cmbYear.TabIndex = 10;
            // 
            // pnlGener
            // 
            pnlGener.Appearance.BackColor = Color.Transparent;
            pnlGener.Appearance.Options.UseBackColor = true;
            pnlGener.Controls.Add(button2);
            pnlGener.Controls.Add(lstGener);
            pnlGener.Controls.Add(schGener);
            pnlGener.Controls.Add(labelControl1);
            pnlGener.Location = new Point(15, 401);
            pnlGener.Name = "pnlGener";
            pnlGener.Size = new Size(561, 160);
            pnlGener.TabIndex = 14;
            // 
            // button2
            // 
            button2.Location = new Point(25, 71);
            button2.Name = "button2";
            button2.Size = new Size(113, 36);
            button2.TabIndex = 2;
            button2.Text = "button2";
            button2.UseVisualStyleBackColor = true;
            button2.Visible = false;
            button2.Click += button2_Click_1;
            // 
            // lstGener
            // 
            lstGener.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lstGener.Appearance.Options.UseFont = true;
            lstGener.CheckOnClick = true;
            lstGener.ContextButtonOptions.AnimationTime = 300;
            lstGener.ContextButtonOptions.AnimationType = DevExpress.Utils.ContextAnimationType.SequenceAnimation;
            lstGener.Dock = DockStyle.Fill;
            lstGener.ItemHeight = 27;
            lstGener.Location = new Point(2, 51);
            lstGener.MultiColumn = true;
            lstGener.Name = "lstGener";
            lstGener.Size = new Size(557, 107);
            lstGener.TabIndex = 0;
            // 
            // schGener
            // 
            schGener.Client = lstGener;
            schGener.Dock = DockStyle.Top;
            schGener.Location = new Point(2, 23);
            schGener.Name = "schGener";
            schGener.Properties.Appearance.Font = new Font("Samim FD", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            schGener.Properties.Appearance.Options.UseFont = true;
            schGener.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            schGener.Properties.Client = lstGener;
            schGener.Size = new Size(557, 28);
            schGener.TabIndex = 0;
            // 
            // labelControl1
            // 
            labelControl1.Appearance.BackColor = Color.Gold;
            labelControl1.Appearance.Font = new Font("Samim FD", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            labelControl1.Appearance.ForeColor = Color.Black;
            labelControl1.Appearance.Options.UseBackColor = true;
            labelControl1.Appearance.Options.UseFont = true;
            labelControl1.Appearance.Options.UseForeColor = true;
            labelControl1.Appearance.Options.UseTextOptions = true;
            labelControl1.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            labelControl1.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            labelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            labelControl1.Dock = DockStyle.Top;
            labelControl1.LineLocation = DevExpress.XtraEditors.LineLocation.Center;
            labelControl1.Location = new Point(2, 2);
            labelControl1.Name = "labelControl1";
            labelControl1.Size = new Size(557, 21);
            labelControl1.TabIndex = 1;
            labelControl1.Text = "ژانر فیلم";
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Samim FD", 12F, FontStyle.Bold);
            btnCancel.Location = new Point(15, 565);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(117, 38);
            btnCancel.TabIndex = 16;
            btnCancel.Text = "انصراف";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Samim FD", 12F, FontStyle.Bold);
            btnSave.Location = new Point(456, 565);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(120, 38);
            btnSave.TabIndex = 15;
            btnSave.Text = "ذخیره";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // picVideo
            // 
            picVideo.Location = new Point(15, 111);
            picVideo.Name = "picVideo";
            picVideo.Properties.AllowAnimationOnValueChanged = DevExpress.Utils.DefaultBoolean.True;
            picVideo.Properties.AnimatedImageLoopMode = DevExpress.Utils.AnimatedImageLoopMode.Infinite;
            picVideo.Properties.ShowCameraMenuItem = DevExpress.XtraEditors.Controls.CameraMenuItemVisibility.Auto;
            picVideo.Properties.SizeMode = DevExpress.XtraEditors.Controls.PictureSizeMode.Squeeze;
            picVideo.Size = new Size(160, 194);
            picVideo.StyleController = layoutControl1;
            picVideo.TabIndex = 1;
            picVideo.ImageLoading += picVideo_ImageLoading;
            // 
            // rdoAnimeted
            // 
            rdoAnimeted.Location = new Point(180, 276);
            rdoAnimeted.Name = "rdoAnimeted";
            rdoAnimeted.Properties.Appearance.BackColor = Color.White;
            rdoAnimeted.Properties.Appearance.Font = new Font("Samim FD", 9F);
            rdoAnimeted.Properties.Appearance.Options.UseBackColor = true;
            rdoAnimeted.Properties.Appearance.Options.UseFont = true;
            rdoAnimeted.Properties.Appearance.Options.UseTextOptions = true;
            rdoAnimeted.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            rdoAnimeted.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] { new DevExpress.XtraEditors.Controls.RadioGroupItem("Other", "سایر", true, null, ""), new DevExpress.XtraEditors.Controls.RadioGroupItem("Animated", "انیمیشن"), new DevExpress.XtraEditors.Controls.RadioGroupItem("Anime", "انیمه") });
            rdoAnimeted.Size = new Size(307, 29);
            rdoAnimeted.StyleController = layoutControl1;
            rdoAnimeted.TabIndex = 11;
            // 
            // rdoAge
            // 
            rdoAge.Location = new Point(180, 210);
            rdoAge.Name = "rdoAge";
            rdoAge.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            rdoAge.Properties.Appearance.Options.UseFont = true;
            rdoAge.Size = new Size(307, 29);
            rdoAge.StyleController = layoutControl1;
            rdoAge.TabIndex = 9;
            // 
            // rdoType
            // 
            rdoType.Location = new Point(180, 177);
            rdoType.Name = "rdoType";
            rdoType.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            rdoType.Properties.Appearance.Options.UseFont = true;
            rdoType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] { new DevExpress.XtraEditors.Controls.RadioGroupItem("6509e3d3-ee0f-4dac-ad65-446be213d9ef", "سریال"), new DevExpress.XtraEditors.Controls.RadioGroupItem("3e8ea833-5ca0-48d9-819e-c5049a46e3f5", "سینمایی") });
            rdoType.Size = new Size(307, 29);
            rdoType.StyleController = layoutControl1;
            rdoType.TabIndex = 8;
            // 
            // cmbCountry
            // 
            cmbCountry.Location = new Point(180, 144);
            cmbCountry.Name = "cmbCountry";
            cmbCountry.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            cmbCountry.Properties.AdvancedModeOptions.AllowSelectionAnimation = DevExpress.Utils.DefaultBoolean.True;
            cmbCountry.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            cmbCountry.Properties.Appearance.Options.UseFont = true;
            cmbCountry.Properties.AppearanceDropDown.Font = new Font("Samim FD", 9.75F);
            cmbCountry.Properties.AppearanceDropDown.Options.UseFont = true;
            cmbCountry.Properties.AppearanceFocused.Font = new Font("Samim FD", 9.75F);
            cmbCountry.Properties.AppearanceFocused.Options.UseFont = true;
            cmbCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            cmbCountry.Properties.ContextImageOptions.AllowChangeAnimation = DevExpress.Utils.DefaultBoolean.True;
            cmbCountry.Properties.NullText = "ساخت کشور ؟";
            cmbCountry.Properties.PopupView = gridLookUpEdit1View;
            cmbCountry.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            cmbCountry.Size = new Size(307, 28);
            cmbCountry.StyleController = layoutControl1;
            cmbCountry.TabIndex = 7;
            // 
            // gridLookUpEdit1View
            // 
            gridLookUpEdit1View.Appearance.Row.Font = new Font("Samim FD", 9.75F);
            gridLookUpEdit1View.Appearance.Row.Options.UseFont = true;
            gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            gridLookUpEdit1View.OptionsFilter.InHeaderSearchMode = DevExpress.XtraGrid.Views.Grid.GridInHeaderSearchMode.TextSearch;
            gridLookUpEdit1View.OptionsFind.AlwaysVisible = true;
            gridLookUpEdit1View.OptionsFind.FindDelay = 200;
            gridLookUpEdit1View.OptionsFind.SearchInPreview = true;
            gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // txtEnglishName
            // 
            txtEnglishName.Location = new Point(205, 79);
            txtEnglishName.Name = "txtEnglishName";
            txtEnglishName.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            txtEnglishName.Properties.Appearance.Options.UseFont = true;
            txtEnglishName.Properties.Appearance.Options.UseTextOptions = true;
            txtEnglishName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtEnglishName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            txtEnglishName.RightToLeft = RightToLeft.No;
            txtEnglishName.Size = new Size(282, 28);
            txtEnglishName.StyleController = layoutControl1;
            txtEnglishName.TabIndex = 4;
            txtEnglishName.Leave += txtEnglishName_Leave;
            // 
            // txtPersianName
            // 
            txtPersianName.EditValue = "";
            txtPersianName.Location = new Point(205, 47);
            txtPersianName.Name = "txtPersianName";
            txtPersianName.Properties.AdvancedModeOptions.AllowCaretAnimation = DevExpress.Utils.DefaultBoolean.True;
            txtPersianName.Properties.AdvancedModeOptions.AllowSelectionAnimation = DevExpress.Utils.DefaultBoolean.True;
            txtPersianName.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            txtPersianName.Properties.Appearance.Options.UseFont = true;
            txtPersianName.Properties.Appearance.Options.UseTextOptions = true;
            txtPersianName.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtPersianName.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            txtPersianName.Properties.ContextImageOptions.AllowChangeAnimation = DevExpress.Utils.DefaultBoolean.True;
            txtPersianName.Size = new Size(282, 28);
            txtPersianName.StyleController = layoutControl1;
            toolTipTitleItem2.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            toolTipTitleItem2.Appearance.BackColor = Color.FromArgb(255, 255, 192);
            toolTipTitleItem2.Appearance.Font = new Font("Samim", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            toolTipTitleItem2.Appearance.Options.UseBackColor = true;
            toolTipTitleItem2.Appearance.Options.UseFont = true;
            toolTipTitleItem2.Text = "درباره فیلم";
            toolTipItem2.Appearance.BackColor = Color.FromArgb(192, 255, 192);
            toolTipItem2.Appearance.BorderColor = Color.Black;
            toolTipItem2.Appearance.Font = new Font("Samim", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            toolTipItem2.Appearance.Options.UseBackColor = true;
            toolTipItem2.Appearance.Options.UseBorderColor = true;
            toolTipItem2.Appearance.Options.UseFont = true;
            toolTipItem2.Text = "فیلمی زیبایی گرسنه";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            txtPersianName.SuperTip = superToolTip2;
            txtPersianName.TabIndex = 2;
            txtPersianName.Leave += txtPersianName_Leave;
            // 
            // txtCode
            // 
            txtCode.Location = new Point(393, 111);
            txtCode.Name = "txtCode";
            txtCode.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            txtCode.Properties.Appearance.Options.UseFont = true;
            txtCode.Properties.Appearance.Options.UseTextOptions = true;
            txtCode.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtCode.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            txtCode.Properties.ReadOnly = true;
            txtCode.Size = new Size(94, 28);
            txtCode.StyleController = layoutControl1;
            txtCode.TabIndex = 5;
            // 
            // txtID
            // 
            txtID.Location = new Point(16, 15);
            txtID.Name = "txtID";
            txtID.Properties.Appearance.Font = new Font("Samim FD", 9.75F);
            txtID.Properties.Appearance.Options.UseFont = true;
            txtID.Properties.ReadOnly = true;
            txtID.Size = new Size(471, 28);
            txtID.StyleController = layoutControl1;
            txtID.TabIndex = 0;
            // 
            // Root
            // 
            Root.AppearanceGroup.BackColor = Color.Gold;
            Root.AppearanceGroup.Options.UseBackColor = true;
            Root.AppearanceItemCaption.Font = new Font("Samim FD", 9.75F);
            Root.AppearanceItemCaption.ForeColor = Color.Purple;
            Root.AppearanceItemCaption.Options.UseFont = true;
            Root.AppearanceItemCaption.Options.UseForeColor = true;
            Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            Root.GroupBordersVisible = false;
            Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] { layoutControlItem1, emptySpaceItem1, layoutControlItem8, layoutControlItem12, layoutControlItem13, layoutControlItem11, layoutControlItem5, layoutControlItem2, layoutControlItem6, layoutControlItem3, layoutControlItem4, layoutControlItem7, layoutControlItem10, layoutControlItem9, layoutControlItem14, layoutControlItem15, itemWeekOfDay, layoutControlItem16, controleritem55, layoutControlItem17, layoutControlItem18 });
            Root.Name = "Root";
            Root.Padding = new DevExpress.XtraLayout.Utils.Padding(13, 23, 13, 13);
            Root.Size = new Size(601, 618);
            Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            layoutControlItem1.Control = txtID;
            layoutControlItem1.Location = new Point(0, 0);
            layoutControlItem1.MaxSize = new Size(564, 32);
            layoutControlItem1.MinSize = new Size(564, 32);
            layoutControlItem1.Name = "layoutControlItem1";
            layoutControlItem1.Size = new Size(565, 32);
            layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem1.Text = "ID";
            layoutControlItem1.TextSize = new Size(77, 20);
            layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem1
            // 
            emptySpaceItem1.Location = new Point(121, 550);
            emptySpaceItem1.Name = "emptySpaceItem1";
            emptySpaceItem1.Size = new Size(160, 20);
            // 
            // layoutControlItem8
            // 
            layoutControlItem8.Control = pnlGener;
            layoutControlItem8.Location = new Point(0, 386);
            layoutControlItem8.Name = "layoutControlItem8";
            layoutControlItem8.Size = new Size(565, 164);
            layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem12
            // 
            layoutControlItem12.Control = btnSave;
            layoutControlItem12.Location = new Point(441, 550);
            layoutControlItem12.MaxSize = new Size(124, 42);
            layoutControlItem12.MinSize = new Size(124, 42);
            layoutControlItem12.Name = "layoutControlItem12";
            layoutControlItem12.Size = new Size(124, 42);
            layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem13
            // 
            layoutControlItem13.Control = btnCancel;
            layoutControlItem13.Location = new Point(0, 550);
            layoutControlItem13.MaxSize = new Size(121, 42);
            layoutControlItem13.MinSize = new Size(121, 42);
            layoutControlItem13.Name = "layoutControlItem13";
            layoutControlItem13.Size = new Size(121, 42);
            layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            layoutControlItem11.Control = picVideo;
            layoutControlItem11.Location = new Point(0, 96);
            layoutControlItem11.MaxSize = new Size(164, 198);
            layoutControlItem11.MinSize = new Size(164, 198);
            layoutControlItem11.Name = "layoutControlItem11";
            layoutControlItem11.Size = new Size(164, 198);
            layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem11.Text = "تصویر:";
            layoutControlItem11.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            layoutControlItem5.Control = cmbCountry;
            layoutControlItem5.Location = new Point(164, 129);
            layoutControlItem5.MaxSize = new Size(400, 33);
            layoutControlItem5.MinSize = new Size(400, 33);
            layoutControlItem5.Name = "layoutControlItem5";
            layoutControlItem5.Size = new Size(401, 33);
            layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem5.Text = "کشور سازنده:";
            layoutControlItem5.TextSize = new Size(77, 20);
            // 
            // layoutControlItem2
            // 
            layoutControlItem2.Control = txtCode;
            layoutControlItem2.Location = new Point(377, 96);
            layoutControlItem2.MaxSize = new Size(187, 33);
            layoutControlItem2.MinSize = new Size(187, 33);
            layoutControlItem2.Name = "layoutControlItem2";
            layoutControlItem2.Size = new Size(188, 33);
            layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem2.Text = "کد فیلم:";
            layoutControlItem2.TextSize = new Size(77, 20);
            // 
            // layoutControlItem6
            // 
            layoutControlItem6.Control = rdoAge;
            layoutControlItem6.Location = new Point(164, 195);
            layoutControlItem6.MaxSize = new Size(400, 33);
            layoutControlItem6.MinSize = new Size(400, 33);
            layoutControlItem6.Name = "layoutControlItem6";
            layoutControlItem6.Size = new Size(401, 33);
            layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem6.Text = "رده سنی:";
            layoutControlItem6.TextSize = new Size(77, 20);
            // 
            // layoutControlItem3
            // 
            layoutControlItem3.Control = txtPersianName;
            layoutControlItem3.Location = new Point(190, 32);
            layoutControlItem3.MaxSize = new Size(375, 32);
            layoutControlItem3.MinSize = new Size(375, 32);
            layoutControlItem3.Name = "layoutControlItem3";
            layoutControlItem3.Size = new Size(375, 32);
            layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem3.Text = "نام (فارسی):";
            layoutControlItem3.TextSize = new Size(77, 20);
            // 
            // layoutControlItem4
            // 
            layoutControlItem4.Control = txtEnglishName;
            layoutControlItem4.Location = new Point(190, 64);
            layoutControlItem4.MaxSize = new Size(375, 32);
            layoutControlItem4.MinSize = new Size(375, 32);
            layoutControlItem4.Name = "layoutControlItem4";
            layoutControlItem4.Size = new Size(375, 32);
            layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem4.Text = "نام (انگلیسی):";
            layoutControlItem4.TextSize = new Size(77, 20);
            // 
            // layoutControlItem7
            // 
            layoutControlItem7.Control = rdoType;
            layoutControlItem7.Location = new Point(164, 162);
            layoutControlItem7.MaxSize = new Size(400, 33);
            layoutControlItem7.MinSize = new Size(400, 33);
            layoutControlItem7.Name = "layoutControlItem7";
            layoutControlItem7.Size = new Size(401, 33);
            layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem7.Text = "نوع فیلم:";
            layoutControlItem7.TextSize = new Size(77, 20);
            // 
            // layoutControlItem10
            // 
            layoutControlItem10.Control = rdoAnimeted;
            layoutControlItem10.Location = new Point(164, 261);
            layoutControlItem10.MaxSize = new Size(400, 33);
            layoutControlItem10.MinSize = new Size(400, 33);
            layoutControlItem10.Name = "layoutControlItem10";
            layoutControlItem10.Size = new Size(401, 33);
            layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem10.Text = "انیمیشن|انیمه";
            layoutControlItem10.TextSize = new Size(77, 20);
            // 
            // layoutControlItem9
            // 
            layoutControlItem9.Control = cmbYear;
            layoutControlItem9.Location = new Point(164, 228);
            layoutControlItem9.MaxSize = new Size(400, 33);
            layoutControlItem9.MinSize = new Size(400, 33);
            layoutControlItem9.Name = "layoutControlItem9";
            layoutControlItem9.Size = new Size(401, 33);
            layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem9.Text = "انتشار:";
            layoutControlItem9.TextSize = new Size(77, 20);
            // 
            // layoutControlItem14
            // 
            layoutControlItem14.Control = cmbPrice;
            layoutControlItem14.Location = new Point(164, 96);
            layoutControlItem14.MaxSize = new Size(213, 33);
            layoutControlItem14.MinSize = new Size(213, 33);
            layoutControlItem14.Name = "layoutControlItem14";
            layoutControlItem14.Size = new Size(213, 33);
            layoutControlItem14.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem14.Text = "قیمت:";
            layoutControlItem14.TextSize = new Size(77, 20);
            // 
            // layoutControlItem15
            // 
            layoutControlItem15.Control = tknActor;
            layoutControlItem15.Location = new Point(0, 326);
            layoutControlItem15.MaxSize = new Size(0, 60);
            layoutControlItem15.MinSize = new Size(1, 33);
            layoutControlItem15.Name = "layoutControlItem15";
            layoutControlItem15.Size = new Size(565, 60);
            layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem15.Text = "بازیگران:";
            layoutControlItem15.TextSize = new Size(77, 20);
            // 
            // itemWeekOfDay
            // 
            itemWeekOfDay.Control = cmbWeekOfDay;
            itemWeekOfDay.Location = new Point(0, 294);
            itemWeekOfDay.Name = "itemWeekOfDay";
            itemWeekOfDay.Size = new Size(565, 32);
            itemWeekOfDay.Text = "روز انتشار:";
            itemWeekOfDay.TextSize = new Size(77, 20);
            // 
            // layoutControlItem16
            // 
            layoutControlItem16.Control = btnFindInfoFilm;
            layoutControlItem16.Location = new Point(0, 32);
            layoutControlItem16.MaxSize = new Size(129, 64);
            layoutControlItem16.MinSize = new Size(129, 64);
            layoutControlItem16.Name = "layoutControlItem16";
            layoutControlItem16.Size = new Size(129, 64);
            layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem16.TextVisible = false;
            // 
            // controleritem55
            // 
            controleritem55.AppearanceItemCaption.Font = new Font("Samim", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            controleritem55.AppearanceItemCaption.Options.UseFont = true;
            controleritem55.Control = lblStar;
            controleritem55.Location = new Point(129, 32);
            controleritem55.MaxSize = new Size(61, 64);
            controleritem55.MinSize = new Size(61, 64);
            controleritem55.Name = "controleritem55";
            controleritem55.Size = new Size(61, 64);
            controleritem55.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            controleritem55.TextVisible = false;
            // 
            // layoutControlItem17
            // 
            layoutControlItem17.Control = progreesDownload;
            layoutControlItem17.Location = new Point(281, 550);
            layoutControlItem17.MinSize = new Size(54, 20);
            layoutControlItem17.Name = "layoutControlItem17";
            layoutControlItem17.Size = new Size(160, 42);
            layoutControlItem17.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            layoutControlItem17.TextVisible = false;
            // 
            // layoutControlItem18
            // 
            layoutControlItem18.Control = barDownload;
            layoutControlItem18.Location = new Point(121, 570);
            layoutControlItem18.Name = "layoutControlItem18";
            layoutControlItem18.Size = new Size(160, 22);
            layoutControlItem18.TextVisible = false;
            // 
            // panel1
            // 
            panel1.BackColor = Color.White;
            panel1.Controls.Add(lblSelMovie);
            panel1.Controls.Add(lblRight);
            panel1.Controls.Add(btnNew);
            panel1.Controls.Add(lblLeft);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Padding = new Padding(0, 0, 15, 0);
            panel1.Size = new Size(601, 39);
            panel1.TabIndex = 14;
            // 
            // lblSelMovie
            // 
            lblSelMovie.BackColor = Color.AliceBlue;
            lblSelMovie.Dock = DockStyle.Fill;
            lblSelMovie.Font = new Font("Samim FD", 12F, FontStyle.Bold);
            lblSelMovie.ForeColor = Color.SeaGreen;
            lblSelMovie.Location = new Point(24, 0);
            lblSelMovie.Name = "lblSelMovie";
            lblSelMovie.RightToLeft = RightToLeft.Yes;
            lblSelMovie.Size = new Size(389, 39);
            lblSelMovie.TabIndex = 1;
            lblSelMovie.Text = "نام فیلم";
            lblSelMovie.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblRight
            // 
            lblRight.BackColor = Color.AliceBlue;
            lblRight.Dock = DockStyle.Right;
            lblRight.Font = new Font("Samim FD", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblRight.ForeColor = Color.SeaGreen;
            lblRight.Location = new Point(413, 0);
            lblRight.Name = "lblRight";
            lblRight.RightToLeft = RightToLeft.Yes;
            lblRight.Size = new Size(24, 39);
            lblRight.TabIndex = 3;
            lblRight.Text = "(";
            lblRight.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnNew
            // 
            btnNew.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Bold);
            btnNew.Appearance.Options.UseFont = true;
            btnNew.Dock = DockStyle.Right;
            btnNew.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnNew.ImageOptions.SvgImage");
            btnNew.Location = new Point(437, 0);
            btnNew.LookAndFeel.SkinName = "Office 2016 Colorful";
            btnNew.LookAndFeel.UseDefaultLookAndFeel = false;
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(149, 39);
            btnNew.TabIndex = 0;
            btnNew.Text = "ثبت فیلم جدید";
            btnNew.Click += btnNew_Click;
            // 
            // lblLeft
            // 
            lblLeft.BackColor = Color.AliceBlue;
            lblLeft.Dock = DockStyle.Left;
            lblLeft.Font = new Font("Samim FD", 15.75F, FontStyle.Bold);
            lblLeft.ForeColor = Color.SeaGreen;
            lblLeft.Location = new Point(0, 0);
            lblLeft.Name = "lblLeft";
            lblLeft.RightToLeft = RightToLeft.Yes;
            lblLeft.Size = new Size(24, 39);
            lblLeft.TabIndex = 2;
            lblLeft.Text = ")";
            lblLeft.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // navigationPane1
            // 
            navigationPane1.AllowHtmlDraw = true;
            navigationPane1.AllowResize = false;
            navigationPane1.AllowTransitionAnimation = DevExpress.Utils.DefaultBoolean.True;
            navigationPane1.AppearanceButton.Hovered.Font = new Font("Samim FD", 11.25F);
            navigationPane1.AppearanceButton.Hovered.Options.UseFont = true;
            navigationPane1.AppearanceButton.Normal.Font = new Font("Samim FD", 11.25F);
            navigationPane1.AppearanceButton.Normal.Options.UseFont = true;
            navigationPane1.AppearanceButton.Pressed.Font = new Font("Samim FD", 11.25F);
            navigationPane1.AppearanceButton.Pressed.Options.UseFont = true;
            navigationPane1.Controls.Add(navigationPage1);
            navigationPane1.Controls.Add(navigationPage2);
            navigationPane1.Dock = DockStyle.Right;
            navigationPane1.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            navigationPane1.Location = new Point(1242, 0);
            navigationPane1.Margin = new Padding(0);
            navigationPane1.Name = "navigationPane1";
            navigationPane1.PageProperties.AppearanceCaption.Font = new Font("Samim FD", 11.25F);
            navigationPane1.PageProperties.AppearanceCaption.Options.UseFont = true;
            navigationPane1.PageProperties.ShowExpandButton = false;
            navigationPane1.Pages.AddRange(new DevExpress.XtraBars.Navigation.NavigationPageBase[] { navigationPage1, navigationPage2 });
            navigationPane1.RegularSize = new Size(980, 655);
            navigationPane1.SelectedPage = navigationPage1;
            navigationPane1.ShowToolTips = DevExpress.Utils.DefaultBoolean.True;
            navigationPane1.Size = new Size(71, 657);
            navigationPane1.State = DevExpress.XtraBars.Navigation.NavigationPaneState.Collapsed;
            navigationPane1.TabIndex = 1;
            navigationPane1.TransitionAnimationProperties.FrameCount = 600;
            navigationPane1.TransitionAnimationProperties.FrameInterval = 7000;
            navigationPane1.TransitionType = DevExpress.Utils.Animation.Transitions.Fade;
            navigationPane1.Click += navigationPane1_Click;
            // 
            // navigationPage1
            // 
            navigationPage1.Caption = "لیست";
            navigationPage1.Controls.Add(searchControl1);
            navigationPage1.Controls.Add(dgvHeader);
            navigationPage1.Name = "navigationPage1";
            navigationPage1.Properties.AppearanceCaption.Options.UseTextOptions = true;
            navigationPage1.Properties.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            navigationPage1.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            navigationPage1.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            navigationPage1.RightToLeft = RightToLeft.Yes;
            navigationPage1.Size = new Size(0, 0);
            // 
            // searchControl1
            // 
            searchControl1.Client = dgvHeader;
            searchControl1.Location = new Point(562, 4);
            searchControl1.MenuManager = barManager1;
            searchControl1.Name = "searchControl1";
            searchControl1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl1.Properties.Client = dgvHeader;
            searchControl1.Size = new Size(318, 22);
            searchControl1.TabIndex = 2;
            // 
            // dgvHeader
            // 
            dgvHeader.Dock = DockStyle.Fill;
            dgvHeader.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            dgvHeader.Location = new Point(0, 0);
            dgvHeader.MainView = viewHeader;
            dgvHeader.Name = "dgvHeader";
            dgvHeader.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] { btnSelect, btnRemove });
            dgvHeader.Size = new Size(0, 0);
            dgvHeader.TabIndex = 0;
            dgvHeader.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { viewHeader });
            // 
            // viewHeader
            // 
            viewHeader.Appearance.Row.Font = new Font("Samim FD", 9.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            viewHeader.Appearance.Row.Options.UseFont = true;
            viewHeader.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] { انتخاب, clmID, clmCode, clmName, clmCreated, clmGroup, clmIsActive, حذف, clmAddress });
            viewHeader.GridControl = dgvHeader;
            viewHeader.Name = "viewHeader";
            viewHeader.OptionsBehavior.AllowGroupExpandAnimation = DevExpress.Utils.DefaultBoolean.True;
            viewHeader.OptionsBehavior.AllowSortAnimation = DevExpress.Utils.DefaultBoolean.True;
            viewHeader.OptionsBehavior.Editable = false;
            viewHeader.OptionsImageLoad.AnimationType = DevExpress.Utils.ImageContentAnimationType.SegmentedFade;
            viewHeader.OptionsView.AnimationType = DevExpress.XtraGrid.Views.Base.GridAnimationType.AnimateFocusedItem;
            viewHeader.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Panel;
            viewHeader.RowHeight = 10;
            // 
            // انتخاب
            // 
            انتخاب.AppearanceCell.Options.UseTextOptions = true;
            انتخاب.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            انتخاب.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            انتخاب.AppearanceHeader.BackColor = Color.FromArgb(255, 255, 128);
            انتخاب.AppearanceHeader.Options.UseBackColor = true;
            انتخاب.ColumnEdit = btnSelect;
            انتخاب.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            انتخاب.ImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("انتخاب.ImageOptions.SvgImage");
            انتخاب.ImageOptions.SvgImageSize = new Size(20, 20);
            انتخاب.Name = "انتخاب";
            انتخاب.OptionsColumn.AllowSize = false;
            انتخاب.Visible = true;
            انتخاب.VisibleIndex = 0;
            انتخاب.Width = 34;
            // 
            // btnSelect
            // 
            btnSelect.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnSelect.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            btnSelect.ContextImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnSelect.ContextImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnSelect.ContextImageOptions.SvgImage");
            btnSelect.ContextImageOptions.SvgImageSize = new Size(22, 22);
            btnSelect.Name = "btnSelect";
            btnSelect.ReadOnly = true;
            // 
            // clmID
            // 
            clmID.Caption = "clmID";
            clmID.FieldName = "clmId";
            clmID.Name = "clmID";
            clmID.Visible = true;
            clmID.VisibleIndex = 1;
            clmID.Width = 113;
            // 
            // clmCode
            // 
            clmCode.AppearanceCell.Options.UseTextOptions = true;
            clmCode.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            clmCode.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            clmCode.Caption = "کد فیلم";
            clmCode.FieldName = "clmCode";
            clmCode.Name = "clmCode";
            clmCode.Visible = true;
            clmCode.VisibleIndex = 2;
            clmCode.Width = 113;
            // 
            // clmName
            // 
            clmName.Caption = "نام فیلم";
            clmName.FieldName = "clmName";
            clmName.Name = "clmName";
            clmName.Visible = true;
            clmName.VisibleIndex = 3;
            clmName.Width = 113;
            // 
            // clmCreated
            // 
            clmCreated.AppearanceCell.Options.UseTextOptions = true;
            clmCreated.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            clmCreated.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            clmCreated.Caption = "سال انشار";
            clmCreated.FieldName = "clmCreated";
            clmCreated.Name = "clmCreated";
            clmCreated.Visible = true;
            clmCreated.VisibleIndex = 4;
            clmCreated.Width = 113;
            // 
            // clmGroup
            // 
            clmGroup.AppearanceCell.Options.UseTextOptions = true;
            clmGroup.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            clmGroup.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            clmGroup.Caption = "گروه";
            clmGroup.FieldName = "clmGroup";
            clmGroup.Name = "clmGroup";
            clmGroup.Visible = true;
            clmGroup.VisibleIndex = 5;
            clmGroup.Width = 113;
            // 
            // clmIsActive
            // 
            clmIsActive.AppearanceCell.Options.UseTextOptions = true;
            clmIsActive.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            clmIsActive.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            clmIsActive.Caption = "وضعیت";
            clmIsActive.FieldName = "clmIsActive";
            clmIsActive.Name = "clmIsActive";
            clmIsActive.Visible = true;
            clmIsActive.VisibleIndex = 6;
            clmIsActive.Width = 124;
            // 
            // حذف
            // 
            حذف.AppearanceCell.Options.UseTextOptions = true;
            حذف.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            حذف.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            حذف.ColumnEdit = btnRemove;
            حذف.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            حذف.Name = "حذف";
            حذف.OptionsColumn.AllowSize = false;
            حذف.Visible = true;
            حذف.VisibleIndex = 8;
            حذف.Width = 32;
            // 
            // btnRemove
            // 
            btnRemove.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnRemove.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            btnRemove.ContextImageOptions.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            btnRemove.ContextImageOptions.SvgImage = (DevExpress.Utils.Svg.SvgImage)resources.GetObject("btnRemove.ContextImageOptions.SvgImage");
            btnRemove.ContextImageOptions.SvgImageSize = new Size(22, 22);
            btnRemove.Name = "btnRemove";
            btnRemove.ReadOnly = true;
            // 
            // clmAddress
            // 
            clmAddress.AppearanceCell.Options.UseTextOptions = true;
            clmAddress.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            clmAddress.AppearanceCell.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            clmAddress.Caption = "آدرس";
            clmAddress.FieldName = "clmAddress";
            clmAddress.Name = "clmAddress";
            clmAddress.Visible = true;
            clmAddress.VisibleIndex = 7;
            clmAddress.Width = 96;
            // 
            // navigationPage2
            // 
            navigationPage2.Caption = "سیستم";
            navigationPage2.Controls.Add(lstOldFilm);
            navigationPage2.Controls.Add(panel2);
            navigationPage2.Name = "navigationPage2";
            navigationPage2.Properties.AppearanceCaption.Options.UseTextOptions = true;
            navigationPage2.Properties.AppearanceCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            navigationPage2.Properties.ShowCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            navigationPage2.Properties.ShowExpandButton = DevExpress.Utils.DefaultBoolean.False;
            navigationPage2.RightToLeft = RightToLeft.Yes;
            navigationPage2.Size = new Size(883, 602);
            // 
            // lstOldFilm
            // 
            lstOldFilm.Appearance.Font = new Font("Samim FD", 11.25F);
            lstOldFilm.Appearance.Options.UseFont = true;
            lstOldFilm.Dock = DockStyle.Fill;
            lstOldFilm.Location = new Point(0, 41);
            lstOldFilm.Name = "lstOldFilm";
            lstOldFilm.Size = new Size(883, 561);
            lstOldFilm.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.Controls.Add(searchControl2);
            panel2.Dock = DockStyle.Top;
            panel2.Location = new Point(0, 0);
            panel2.Name = "panel2";
            panel2.Size = new Size(883, 41);
            panel2.TabIndex = 1;
            // 
            // searchControl2
            // 
            searchControl2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            searchControl2.Client = lstOldFilm;
            searchControl2.Location = new Point(654, 4);
            searchControl2.MenuManager = barManager1;
            searchControl2.Name = "searchControl2";
            searchControl2.Properties.Appearance.Font = new Font("Samim FD", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            searchControl2.Properties.Appearance.Options.UseFont = true;
            searchControl2.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Repository.ClearButton(), new DevExpress.XtraEditors.Repository.SearchButton() });
            searchControl2.Properties.Client = lstOldFilm;
            searchControl2.Properties.FilterCondition = DevExpress.Data.Filtering.FilterCondition.Contains;
            searchControl2.Properties.FindDelay = 200;
            searchControl2.Size = new Size(225, 34);
            searchControl2.TabIndex = 0;
            // 
            // popMenuChapterDetails
            // 
            popMenuChapterDetails.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] { new DevExpress.XtraBars.LinkPersistInfo(mnubarAddPersian), new DevExpress.XtraBars.LinkPersistInfo(mnubarAddHardSub) });
            popMenuChapterDetails.Manager = barManager1;
            popMenuChapterDetails.Name = "popMenuChapterDetails";
            // 
            // FrmManageVideo
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1313, 657);
            Controls.Add(navigationPane1);
            Controls.Add(splitContainerControl1);
            Controls.Add(barDockControlLeft);
            Controls.Add(barDockControlRight);
            Controls.Add(barDockControlBottom);
            Controls.Add(barDockControlTop);
            Name = "FrmManageVideo";
            Text = "مدیریت ویدئوها";
            FormClosing += FrmManageVideo_FormClosing;
            FormClosed += FrmManageVideo_FormClosed;
            Load += FrmManageVideo_Load;
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
            ((System.ComponentModel.ISupportInitialize)dgvHeaderChapter).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewHeaderChapter).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemSelect).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbNameFilm).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView2).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbChCreated).EndInit();
            ((System.ComponentModel.ISupportInitialize)memoChDescription).EndInit();
            ((System.ComponentModel.ISupportInitialize)repositoryItemCopyToSession).EndInit();
            ((System.ComponentModel.ISupportInitialize)panelSelFile).EndInit();
            panelSelFile.ResumeLayout(false);
            navigationPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lstSelFiles).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvChapterDetail).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewChapterDetail).EndInit();
            ((System.ComponentModel.ISupportInitialize)barManager1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControl1).EndInit();
            layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)barDownload.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)progreesDownload.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbWeekOfDay.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tknActor.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbPrice.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbYear.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)pnlGener).EndInit();
            pnlGener.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lstGener).EndInit();
            ((System.ComponentModel.ISupportInitialize)schGener.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)picVideo.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)rdoAnimeted.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)rdoAge.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)rdoType.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)cmbCountry.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)gridLookUpEdit1View).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtEnglishName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPersianName.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtCode.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtID.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)Root).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)emptySpaceItem1).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem8).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem12).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem13).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem11).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem5).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem2).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem6).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem3).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem4).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem7).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem10).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem9).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem14).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem15).EndInit();
            ((System.ComponentModel.ISupportInitialize)itemWeekOfDay).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem16).EndInit();
            ((System.ComponentModel.ISupportInitialize)controleritem55).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem17).EndInit();
            ((System.ComponentModel.ISupportInitialize)layoutControlItem18).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)navigationPane1).EndInit();
            navigationPane1.ResumeLayout(false);
            navigationPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl1.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)viewHeader).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnSelect).EndInit();
            ((System.ComponentModel.ISupportInitialize)btnRemove).EndInit();
            navigationPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)lstOldFilm).EndInit();
            panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)searchControl2.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)popMenuChapterDetails).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtID;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtEnglishName;
        private DevExpress.XtraEditors.TextEdit txtPersianName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.GridLookUpEdit cmbCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.RadioGroup rdoAge;
        private DevExpress.XtraEditors.RadioGroup rdoType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.RadioGroup rdoAnimeted;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.PictureEdit picVideo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl2;
        private DevExpress.XtraBars.Navigation.NavigationPane navigationPane1;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage1;
        private DevExpress.XtraGrid.Views.Grid.GridView viewHeader;
        private DevExpress.XtraEditors.PanelControl pnlGener;
        private DevExpress.XtraEditors.CheckedListBoxControl lstGener;
        private DevExpress.XtraEditors.SearchControl schGener;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LookUpEdit cmbYear;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.LabelControl lblSelChapterHeader;
        private DevExpress.XtraEditors.LabelControl lblDetailsChapter;
        private DevExpress.XtraGrid.Columns.GridColumn clmID;
        private DevExpress.XtraGrid.Columns.GridColumn clmCode;
        private DevExpress.XtraGrid.Columns.GridColumn clmName;
        private DevExpress.XtraGrid.Columns.GridColumn clmCreated;
        private DevExpress.XtraGrid.Columns.GridColumn clmGroup;
        private DevExpress.XtraGrid.Columns.GridColumn clmIsActive;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.GridLookUpEdit cmbPrice;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private System.Windows.Forms.Label lblSelMovie;
        private DevExpress.XtraGrid.Columns.GridColumn انتخاب;
        private DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel btnSelect;
        private DevExpress.XtraEditors.TokenEdit tknActor;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraGrid.GridControl dgvHeaderChapter;
        private DevExpress.XtraGrid.Views.Grid.GridView viewHeaderChapter;
        private DevExpress.XtraGrid.Columns.GridColumn clmChId;
        private DevExpress.XtraGrid.Columns.GridColumn clmChCode;
        private DevExpress.XtraGrid.Columns.GridColumn clmChNameChapter;
        private DevExpress.XtraGrid.Columns.GridColumn clmChNameFilm;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit cmbNameFilm;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn clmChCreated;
        private DevExpress.XtraGrid.Columns.GridColumn clmChDescription;
        private DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit memoChDescription;
        private DevExpress.XtraBars.PopupMenu popMenuChapterDetails;
        private DevExpress.XtraBars.BarButtonItem mnubarAddPersian;
        private DevExpress.XtraBars.BarButtonItem mnubarAddHardSub;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SimpleButton btnAddRowChapterDetail;
        private DevExpress.XtraGrid.Columns.GridColumn ChdBtnSelect;
        private DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel repositoryItemSelect;
        private DevExpress.XtraGrid.Columns.GridColumn حذف;
        private DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel btnRemove;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage2;
        private DevExpress.XtraEditors.ListBoxControl lstOldFilm;
        private DevExpress.XtraGrid.Columns.GridColumn clmAddress;
        private DevExpress.XtraEditors.SearchControl searchControl1;
        private DevExpress.XtraGrid.Columns.GridColumn ChdBtnCopyTo;
        private DevExpress.XtraEditors.Repository.RepositoryItemHypertextLabel repositoryItemCopyToSession;
        private DevExpress.XtraEditors.RoundedSkinPanel roundedSkinPanel1;
        private DevExpress.XtraEditors.RoundedSkinPanel roundedSkinPanel2;
        private DevExpress.XtraEditors.RoundedSkinPanel roundedSkinPanel3;
        private DevExpress.XtraEditors.RoundedSkinPanel roundedSkinPanel4;
        private DevExpress.XtraEditors.RoundedSkinPanel roundedSkinPanel5;
        private DevExpress.XtraEditors.RoundedSkinPanel roundedSkinPanel6;
        private DevExpress.XtraEditors.RoundedSkinPanel roundedSkinPanel7;
        private DevExpress.XtraEditors.RoundedSkinPanel roundedSkinPanel8;
        private DevExpress.XtraEditors.RoundedSkinPanel roundedSkinPanel9;
        private DevExpress.XtraBars.Navigation.NavigationPane panelSelFile;
        private DevExpress.XtraBars.Navigation.NavigationPage navigationPage3;
        private DevExpress.XtraEditors.ListBoxControl lstSelFiles;
        private DevExpress.Utils.Html.HtmlTemplate htmlTemplate1;
        private System.Windows.Forms.Label lblLeft;
        private System.Windows.Forms.Label lblRight;
        public DevExpress.XtraGrid.GridControl dgvHeader;
        private MyCom.Object.KavoshGrid dgvChapterDetail;
        private DevExpress.XtraGrid.Views.BandedGrid.BandedGridView viewChapterDetail;
        private DevExpress.XtraGrid.Views.BandedGrid.GridBand gridBand1;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit cmbChCreated;
        private DevExpress.XtraEditors.LookUpEdit cmbWeekOfDay;
        private DevExpress.XtraLayout.LayoutControlItem itemWeekOfDay;
        private System.Windows.Forms.Button button2;
        private Panel panel2;
        private DevExpress.XtraEditors.SearchControl searchControl2;
        private DevExpress.XtraEditors.SimpleButton btnCreatPart;
        private DevExpress.XtraEditors.SimpleButton btnFindInfoFilm;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraEditors.LabelControl lblStar;
        private DevExpress.XtraLayout.LayoutControlItem controleritem55;
        private DevExpress.XtraEditors.ProgressBarControl progreesDownload;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem17;
        private DevExpress.XtraEditors.ProgressBarControl barDownload;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
    }
}