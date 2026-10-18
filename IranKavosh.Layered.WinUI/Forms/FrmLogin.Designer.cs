namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmLogin
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
            txtUser = new DevExpress.XtraEditors.TextEdit();
            txtPassEnter = new DevExpress.XtraEditors.TextEdit();
            label1 = new Label();
            tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            btnPass = new DevExpress.XtraEditors.SimpleButton();
            btnEnter = new DevExpress.XtraEditors.SimpleButton();
            label2 = new Label();
            btnExit = new DevExpress.XtraEditors.SimpleButton();
            panel1 = new Panel();
            panel2 = new Panel();
            panel3 = new Panel();
            ((System.ComponentModel.ISupportInitialize)txtUser.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)txtPassEnter.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tablePanel1).BeginInit();
            tablePanel1.SuspendLayout();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // txtUser
            // 
            tablePanel1.SetColumn(txtUser, 0);
            tablePanel1.SetColumnSpan(txtUser, 2);
            txtUser.Location = new Point(3, 6);
            txtUser.Name = "txtUser";
            txtUser.Properties.Appearance.Font = new Font("Tahoma", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtUser.Properties.Appearance.Options.UseFont = true;
            txtUser.Properties.Appearance.Options.UseTextOptions = true;
            txtUser.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtUser.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            txtUser.Properties.MaxLength = 15;
            tablePanel1.SetRow(txtUser, 0);
            txtUser.Size = new Size(254, 32);
            txtUser.TabIndex = 0;
            txtUser.EditValueChanged += txtUser_EditValueChanged;
            txtUser.KeyDown += txtUser_KeyDown;
            // 
            // txtPassEnter
            // 
            tablePanel1.SetColumn(txtPassEnter, 0);
            tablePanel1.SetColumnSpan(txtPassEnter, 2);
            txtPassEnter.EditValue = "";
            txtPassEnter.Location = new Point(3, 51);
            txtPassEnter.Name = "txtPassEnter";
            txtPassEnter.Properties.Appearance.Font = new Font("Tahoma", 14.25F);
            txtPassEnter.Properties.Appearance.Options.UseFont = true;
            txtPassEnter.Properties.Appearance.Options.UseTextOptions = true;
            txtPassEnter.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            txtPassEnter.Properties.Appearance.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Center;
            txtPassEnter.Properties.MaxLength = 15;
            txtPassEnter.Properties.UseSystemPasswordChar = true;
            tablePanel1.SetRow(txtPassEnter, 1);
            txtPassEnter.Size = new Size(254, 32);
            txtPassEnter.TabIndex = 1;
            txtPassEnter.KeyDown += txtPass_KeyDown;
            // 
            // label1
            // 
            tablePanel1.SetColumn(label1, 2);
            label1.Dock = DockStyle.Fill;
            label1.Location = new Point(262, 45);
            label1.Name = "label1";
            label1.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label1, 1);
            label1.Size = new Size(90, 45);
            label1.TabIndex = 1;
            label1.Text = "رمز عبور:";
            label1.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // tablePanel1
            // 
            tablePanel1.Appearance.BackColor = Color.Transparent;
            tablePanel1.Appearance.Options.UseBackColor = true;
            tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] { new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 36.5F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 36.5F), new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 27F) });
            tablePanel1.Controls.Add(btnPass);
            tablePanel1.Controls.Add(btnEnter);
            tablePanel1.Controls.Add(label2);
            tablePanel1.Controls.Add(txtPassEnter);
            tablePanel1.Controls.Add(label1);
            tablePanel1.Controls.Add(txtUser);
            tablePanel1.Controls.Add(btnExit);
            tablePanel1.Location = new Point(101, 102);
            tablePanel1.Name = "tablePanel1";
            tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] { new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.3F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.3F), new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 33.3F) });
            tablePanel1.Size = new Size(355, 136);
            tablePanel1.TabIndex = 2;
            // 
            // btnPass
            // 
            tablePanel1.SetColumn(btnPass, 2);
            btnPass.Dock = DockStyle.Fill;
            btnPass.Location = new Point(262, 93);
            btnPass.Name = "btnPass";
            tablePanel1.SetRow(btnPass, 2);
            btnPass.Size = new Size(90, 40);
            btnPass.TabIndex = 6;
            btnPass.Text = "کلمه عبور ؟";
            btnPass.Click += btnPass_Click;
            // 
            // btnEnter
            // 
            tablePanel1.SetColumn(btnEnter, 1);
            btnEnter.Dock = DockStyle.Fill;
            btnEnter.Location = new Point(133, 93);
            btnEnter.Name = "btnEnter";
            tablePanel1.SetRow(btnEnter, 2);
            btnEnter.Size = new Size(124, 40);
            btnEnter.TabIndex = 2;
            btnEnter.Text = "ورود";
            btnEnter.Click += btnEnter_Click;
            // 
            // label2
            // 
            tablePanel1.SetColumn(label2, 2);
            label2.Dock = DockStyle.Fill;
            label2.Location = new Point(262, 0);
            label2.Name = "label2";
            label2.RightToLeft = RightToLeft.Yes;
            tablePanel1.SetRow(label2, 0);
            label2.Size = new Size(90, 45);
            label2.TabIndex = 1;
            label2.Text = "نام کاربری:";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // btnExit
            // 
            tablePanel1.SetColumn(btnExit, 0);
            btnExit.Dock = DockStyle.Fill;
            btnExit.Location = new Point(3, 93);
            btnExit.Name = "btnExit";
            tablePanel1.SetRow(btnExit, 2);
            btnExit.Size = new Size(124, 40);
            btnExit.TabIndex = 3;
            btnExit.Text = "خروج";
            btnExit.Click += btnExit_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.Transparent;
            panel1.BackgroundImage = Properties.Resources.Password6;
            panel1.BackgroundImageLayout = ImageLayout.Stretch;
            panel1.Controls.Add(panel2);
            panel1.Controls.Add(panel3);
            panel1.Controls.Add(tablePanel1);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(554, 297);
            panel1.TabIndex = 3;
            panel1.Paint += panel1_Paint;
            // 
            // panel2
            // 
            panel2.BackColor = Color.Transparent;
            panel2.BackgroundImage = Properties.Resources.Logo_Rampage_1;
            panel2.BackgroundImageLayout = ImageLayout.Zoom;
            panel2.Location = new Point(104, -5);
            panel2.Name = "panel2";
            panel2.Size = new Size(480, 89);
            panel2.TabIndex = 5;
            // 
            // panel3
            // 
            panel3.BackColor = Color.Transparent;
            panel3.BackgroundImage = Properties.Resources.Version_1;
            panel3.BackgroundImageLayout = ImageLayout.Zoom;
            panel3.Location = new Point(186, 253);
            panel3.Name = "panel3";
            panel3.Size = new Size(107, 29);
            panel3.TabIndex = 3;
            // 
            // FrmLogin
            // 
            Appearance.BackColor = Color.DarkOliveGreen;
            Appearance.ForeColor = Color.Transparent;
            Appearance.Options.UseBackColor = true;
            Appearance.Options.UseForeColor = true;
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(554, 297);
            Controls.Add(panel1);
            DoubleBuffered = true;
            FormBorderEffect = DevExpress.XtraEditors.FormBorderEffect.None;
            FormBorderStyle = FormBorderStyle.None;
            Name = "FrmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "ورود";
            Load += FrmLogin_Load;
            ((System.ComponentModel.ISupportInitialize)txtUser.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)txtPassEnter.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)tablePanel1).EndInit();
            tablePanel1.ResumeLayout(false);
            panel1.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.TextEdit txtPassEnter;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton btnEnter;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel2;
        public DevExpress.XtraEditors.TextEdit txtUser;
        private DevExpress.XtraEditors.SimpleButton btnPass;
    }
}