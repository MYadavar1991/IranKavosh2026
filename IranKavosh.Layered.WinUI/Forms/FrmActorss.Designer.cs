namespace IranKavosh.Layered.WinUI.Forms
{
    partial class FrmActorss
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
            memoEn = new DevExpress.XtraEditors.MemoEdit();
            memoFa = new DevExpress.XtraEditors.MemoEdit();
            panel1 = new Panel();
            btnSave = new DevExpress.XtraEditors.SimpleButton();
            btnCancel = new DevExpress.XtraEditors.SimpleButton();
            memoId = new DevExpress.XtraEditors.MemoEdit();
            memoDes = new DevExpress.XtraEditors.MemoEdit();
            ((System.ComponentModel.ISupportInitialize)memoEn.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memoFa.Properties).BeginInit();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)memoId.Properties).BeginInit();
            ((System.ComponentModel.ISupportInitialize)memoDes.Properties).BeginInit();
            SuspendLayout();
            // 
            // memoEn
            // 
            memoEn.Location = new Point(2, 8);
            memoEn.Name = "memoEn";
            memoEn.Properties.Appearance.Font = new Font("Samim FD", 12.75F);
            memoEn.Properties.Appearance.Options.UseFont = true;
            memoEn.Size = new Size(247, 224);
            memoEn.TabIndex = 0;
            // 
            // memoFa
            // 
            memoFa.Location = new Point(255, 8);
            memoFa.Name = "memoFa";
            memoFa.Properties.Appearance.Font = new Font("Samim FD", 12.75F);
            memoFa.Properties.Appearance.Options.UseFont = true;
            memoFa.RightToLeft = RightToLeft.Yes;
            memoFa.Size = new Size(247, 224);
            memoFa.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(btnSave);
            panel1.Controls.Add(btnCancel);
            panel1.Location = new Point(2, 404);
            panel1.Name = "panel1";
            panel1.Size = new Size(500, 35);
            panel1.TabIndex = 1;
            // 
            // btnSave
            // 
            btnSave.Appearance.Font = new Font("Samim FD", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSave.Appearance.Options.UseFont = true;
            btnSave.Dock = DockStyle.Right;
            btnSave.ImageOptions.SvgImage = Properties.Resources.checkbox;
            btnSave.Location = new Point(373, 0);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(127, 35);
            btnSave.TabIndex = 2;
            btnSave.Text = "ذخیره";
            btnSave.Click += btnSave_Click;
            // 
            // btnCancel
            // 
            btnCancel.Appearance.Font = new Font("Samim FD", 12F);
            btnCancel.Appearance.Options.UseFont = true;
            btnCancel.Dock = DockStyle.Left;
            btnCancel.ImageOptions.SvgImage = Properties.Resources.paymentunpaid;
            btnCancel.Location = new Point(0, 0);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(127, 35);
            btnCancel.TabIndex = 1;
            btnCancel.Text = "انصراف";
            btnCancel.Click += btnCancel_Click;
            // 
            // memoId
            // 
            memoId.Location = new Point(508, 8);
            memoId.Name = "memoId";
            memoId.Properties.Appearance.Font = new Font("Samim FD", 12.75F);
            memoId.Properties.Appearance.Options.UseFont = true;
            memoId.RightToLeft = RightToLeft.Yes;
            memoId.Size = new Size(247, 224);
            memoId.TabIndex = 0;
            // 
            // memoDes
            // 
            memoDes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            memoDes.Location = new Point(2, 238);
            memoDes.Name = "memoDes";
            memoDes.Properties.Appearance.Font = new Font("Samim FD", 11.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            memoDes.Properties.Appearance.Options.UseFont = true;
            memoDes.RightToLeft = RightToLeft.Yes;
            memoDes.Size = new Size(500, 160);
            memoDes.TabIndex = 0;
            // 
            // FrmActorss
            // 
            AutoScaleDimensions = new SizeF(6F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(507, 444);
            Controls.Add(panel1);
            Controls.Add(memoDes);
            Controls.Add(memoId);
            Controls.Add(memoFa);
            Controls.Add(memoEn);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "FrmActorss";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "مدیریت بازیگران";
            Load += FrmActorss_Load;
            ((System.ComponentModel.ISupportInitialize)memoEn.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)memoFa.Properties).EndInit();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)memoId.Properties).EndInit();
            ((System.ComponentModel.ISupportInitialize)memoDes.Properties).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.MemoEdit memoEn;
        private DevExpress.XtraEditors.MemoEdit memoFa;
        private Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.MemoEdit memoId;
        private DevExpress.XtraEditors.MemoEdit memoDes;
    }
}