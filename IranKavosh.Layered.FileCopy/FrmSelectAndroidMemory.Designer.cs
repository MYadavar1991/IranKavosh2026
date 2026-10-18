namespace IranKavosh.Layered.FileCopy
{
    partial class FrmSelectAndroidMemory
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
            tableLayoutPanel1 = new TableLayoutPanel();
            label3 = new Label();
            btnHDD2 = new DevExpress.XtraEditors.SimpleButton();
            btnHDD1 = new DevExpress.XtraEditors.SimpleButton();
            barCharge = new DevExpress.XtraEditors.ProgressBarControl();
            lblFree1 = new Label();
            lblName1 = new Label();
            label4 = new Label();
            label2 = new Label();
            lblModel = new Label();
            lblName2 = new Label();
            lblFree2 = new Label();
            label1 = new Label();
            panel1 = new Panel();
            simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)barCharge.Properties).BeginInit();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 4;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 101F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 63.50711F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 36.49289F));
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 82F));
            tableLayoutPanel1.Controls.Add(label3, 3, 2);
            tableLayoutPanel1.Controls.Add(btnHDD2, 0, 2);
            tableLayoutPanel1.Controls.Add(btnHDD1, 0, 1);
            tableLayoutPanel1.Controls.Add(barCharge, 0, 0);
            tableLayoutPanel1.Controls.Add(lblFree1, 1, 1);
            tableLayoutPanel1.Controls.Add(lblName1, 2, 1);
            tableLayoutPanel1.Controls.Add(label4, 3, 1);
            tableLayoutPanel1.Controls.Add(label2, 3, 0);
            tableLayoutPanel1.Controls.Add(lblModel, 1, 0);
            tableLayoutPanel1.Controls.Add(lblName2, 2, 2);
            tableLayoutPanel1.Controls.Add(lblFree2, 1, 2);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 29);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 34F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 33F));
            tableLayoutPanel1.Size = new Size(395, 108);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            label3.Dock = DockStyle.Fill;
            label3.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            label3.Location = new Point(315, 71);
            label3.Name = "label3";
            label3.Size = new Size(77, 37);
            label3.TabIndex = 1;
            label3.Text = "حافظه 2";
            label3.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnHDD2
            // 
            btnHDD2.Appearance.Font = new Font("Samim FD", 8.25F, FontStyle.Bold);
            btnHDD2.Appearance.Options.UseFont = true;
            btnHDD2.Dock = DockStyle.Fill;
            btnHDD2.Location = new Point(3, 74);
            btnHDD2.Name = "btnHDD2";
            btnHDD2.Size = new Size(95, 31);
            btnHDD2.TabIndex = 2;
            btnHDD2.Text = "انتقال";
            btnHDD2.Click += btnHDD2_Click;
            // 
            // btnHDD1
            // 
            btnHDD1.Appearance.Font = new Font("Samim FD", 8.25F, FontStyle.Bold);
            btnHDD1.Appearance.Options.UseFont = true;
            btnHDD1.Dock = DockStyle.Fill;
            btnHDD1.Location = new Point(3, 39);
            btnHDD1.Name = "btnHDD1";
            btnHDD1.Size = new Size(95, 29);
            btnHDD1.TabIndex = 2;
            btnHDD1.Text = "انتقال";
            btnHDD1.Click += btnHDD1_Click;
            // 
            // barCharge
            // 
            barCharge.Dock = DockStyle.Fill;
            barCharge.EditValue = 50;
            barCharge.Location = new Point(3, 3);
            barCharge.Name = "barCharge";
            barCharge.Properties.AccessibleDescription = "درصد شارژ";
            barCharge.Properties.LookAndFeel.SkinMaskColor = Color.Lime;
            barCharge.Properties.LookAndFeel.SkinMaskColor2 = Color.White;
            barCharge.Properties.LookAndFeel.SkinName = "WXI";
            barCharge.Properties.LookAndFeel.UseDefaultLookAndFeel = false;
            barCharge.Properties.ShowTitle = true;
            barCharge.Properties.Step = 1;
            barCharge.Size = new Size(95, 30);
            barCharge.TabIndex = 2;
            // 
            // lblFree1
            // 
            lblFree1.Dock = DockStyle.Fill;
            lblFree1.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblFree1.Location = new Point(104, 36);
            lblFree1.Name = "lblFree1";
            lblFree1.Size = new Size(128, 35);
            lblFree1.TabIndex = 1;
            lblFree1.Text = "فضای خالی";
            lblFree1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblName1
            // 
            lblName1.Dock = DockStyle.Fill;
            lblName1.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblName1.Location = new Point(238, 36);
            lblName1.Name = "lblName1";
            lblName1.Size = new Size(71, 35);
            lblName1.TabIndex = 1;
            lblName1.Text = "نام";
            lblName1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            label4.Dock = DockStyle.Fill;
            label4.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            label4.Location = new Point(315, 36);
            label4.Name = "label4";
            label4.Size = new Size(77, 35);
            label4.TabIndex = 1;
            label4.Text = "حافظه 1";
            label4.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            label2.Dock = DockStyle.Fill;
            label2.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            label2.Location = new Point(315, 0);
            label2.Name = "label2";
            label2.Size = new Size(77, 36);
            label2.TabIndex = 1;
            label2.Text = "مدل";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblModel
            // 
            tableLayoutPanel1.SetColumnSpan(lblModel, 2);
            lblModel.Dock = DockStyle.Fill;
            lblModel.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblModel.Location = new Point(104, 0);
            lblModel.Name = "lblModel";
            lblModel.Size = new Size(205, 36);
            lblModel.TabIndex = 1;
            lblModel.Text = "نام مدل";
            lblModel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblName2
            // 
            lblName2.Dock = DockStyle.Fill;
            lblName2.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblName2.Location = new Point(238, 71);
            lblName2.Name = "lblName2";
            lblName2.Size = new Size(71, 37);
            lblName2.TabIndex = 1;
            lblName2.Text = "نام";
            lblName2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblFree2
            // 
            lblFree2.Dock = DockStyle.Fill;
            lblFree2.Font = new Font("Samim FD", 9.75F, FontStyle.Bold);
            lblFree2.Location = new Point(104, 71);
            lblFree2.Name = "lblFree2";
            lblFree2.Size = new Size(128, 37);
            lblFree2.TabIndex = 1;
            lblFree2.Text = "فضای خالی";
            lblFree2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(307, 0);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 0;
            label1.Text = "مدل";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            panel1.Controls.Add(simpleButton1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(395, 29);
            panel1.TabIndex = 1;
            // 
            // simpleButton1
            // 
            simpleButton1.Appearance.Font = new Font("Samim FD", 8.25F, FontStyle.Bold);
            simpleButton1.Appearance.Options.UseFont = true;
            simpleButton1.Dock = DockStyle.Fill;
            simpleButton1.Location = new Point(0, 0);
            simpleButton1.Name = "simpleButton1";
            simpleButton1.Size = new Size(395, 29);
            simpleButton1.TabIndex = 3;
            simpleButton1.Text = "بررسی مجدد اتصال";
            simpleButton1.Click += simpleButton1_Click;
            // 
            // FrmSelectAndroidMemory
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(395, 137);
            Controls.Add(tableLayoutPanel1);
            Controls.Add(panel1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FrmSelectAndroidMemory";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "انتخاب حافظه";
            TopMost = true;
            Load += FrmSelectAndroidMemory_Load;
            tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)barCharge.Properties).EndInit();
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private Label label1;
        private Label label4;
        private DevExpress.XtraEditors.ProgressBarControl barCharge;
        private Label label2;
        private Label lblModel;
        private Label lblFree1;
        private Label lblName1;
        private Label lblName2;
        private Label lblFree2;
        private DevExpress.XtraEditors.SimpleButton btnHDD2;
        private DevExpress.XtraEditors.SimpleButton btnHDD1;
        private Label label3;
        private Panel panel1;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
    }
}