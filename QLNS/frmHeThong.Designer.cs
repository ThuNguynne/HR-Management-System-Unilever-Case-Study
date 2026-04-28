namespace UnileverHRM
{
    partial class frmHeThong
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing) { if (disposing && (components != null)) components.Dispose(); base.Dispose(disposing); }

        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabTaiKhoan = new System.Windows.Forms.TabPage();
            this.dgvTaiKhoan = new System.Windows.Forms.DataGridView();
            this.panelAction = new System.Windows.Forms.Panel();
            this.btnKhoaTK = new System.Windows.Forms.Button();
            this.btnResetPass = new System.Windows.Forms.Button();
            this.tabDoiMatKhau = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabTaiKhoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).BeginInit();
            this.panelAction.SuspendLayout();
            this.tabDoiMatKhau.SuspendLayout();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabTaiKhoan);
            this.tabControl1.Controls.Add(this.tabDoiMatKhau);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 50);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(878, 494);
            this.tabControl1.TabIndex = 0;
            // 
            // tabTaiKhoan
            // 
            this.tabTaiKhoan.Controls.Add(this.dgvTaiKhoan);
            this.tabTaiKhoan.Controls.Add(this.panelAction);
            this.tabTaiKhoan.Location = new System.Drawing.Point(4, 29);
            this.tabTaiKhoan.Name = "tabTaiKhoan";
            this.tabTaiKhoan.Size = new System.Drawing.Size(870, 461);
            this.tabTaiKhoan.TabIndex = 0;
            this.tabTaiKhoan.Text = "Quản lý tài khoản";
            this.tabTaiKhoan.UseVisualStyleBackColor = true;
            // 
            // dgvTaiKhoan
            // 
            this.dgvTaiKhoan.AllowUserToAddRows = false;
            this.dgvTaiKhoan.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTaiKhoan.ColumnHeadersHeight = 34;
            this.dgvTaiKhoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTaiKhoan.Location = new System.Drawing.Point(0, 50);
            this.dgvTaiKhoan.Name = "dgvTaiKhoan";
            this.dgvTaiKhoan.RowHeadersWidth = 62;
            this.dgvTaiKhoan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTaiKhoan.Size = new System.Drawing.Size(870, 411);
            this.dgvTaiKhoan.TabIndex = 0;
            // 
            // panelAction
            // 
            this.panelAction.Controls.Add(this.btnKhoaTK);
            this.panelAction.Controls.Add(this.btnResetPass);
            this.panelAction.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAction.Location = new System.Drawing.Point(0, 0);
            this.panelAction.Name = "panelAction";
            this.panelAction.Size = new System.Drawing.Size(870, 50);
            this.panelAction.TabIndex = 1;
            // 
            // btnKhoaTK
            // 
            this.btnKhoaTK.Location = new System.Drawing.Point(148, 10);
            this.btnKhoaTK.Name = "btnKhoaTK";
            this.btnKhoaTK.Size = new System.Drawing.Size(120, 30);
            this.btnKhoaTK.TabIndex = 0;
            this.btnKhoaTK.Text = "Khóa/Mở Khóa";
            this.btnKhoaTK.Click += new System.EventHandler(this.btnKhoaTK_Click);
            // 
            // btnResetPass
            // 
            this.btnResetPass.Location = new System.Drawing.Point(10, 10);
            this.btnResetPass.Name = "btnResetPass";
            this.btnResetPass.Size = new System.Drawing.Size(120, 30);
            this.btnResetPass.TabIndex = 1;
            this.btnResetPass.Text = "Reset Mật khẩu";
            this.btnResetPass.Click += new System.EventHandler(this.btnResetPass_Click);
            // 
            // tabDoiMatKhau
            // 
            this.tabDoiMatKhau.Controls.Add(this.label1);
            this.tabDoiMatKhau.Location = new System.Drawing.Point(4, 29);
            this.tabDoiMatKhau.Name = "tabDoiMatKhau";
            this.tabDoiMatKhau.Size = new System.Drawing.Size(892, 517);
            this.tabDoiMatKhau.TabIndex = 1;
            this.tabDoiMatKhau.Text = "Đổi mật khẩu cá nhân";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.label1.Location = new System.Drawing.Point(30, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(464, 32);
            this.label1.TabIndex = 0;
            this.label1.Text = "Chức năng đổi mật khẩu đang phát triển...";
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.White;
            this.panelTop.Controls.Add(this.lblTitle);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(878, 50);
            this.panelTop.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(59)))), ((int)(((byte)(113)))));
            this.lblTitle.Location = new System.Drawing.Point(12, 9);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(350, 45);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN TRỊ HỆ THỐNG";
            // 
            // frmHeThong
            // 
            this.ClientSize = new System.Drawing.Size(878, 544);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelTop);
            this.Name = "frmHeThong";
            this.tabControl1.ResumeLayout(false);
            this.tabTaiKhoan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTaiKhoan)).EndInit();
            this.panelAction.ResumeLayout(false);
            this.tabDoiMatKhau.ResumeLayout(false);
            this.tabDoiMatKhau.PerformLayout();
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabTaiKhoan;
        private System.Windows.Forms.TabPage tabDoiMatKhau;
        private System.Windows.Forms.DataGridView dgvTaiKhoan;
        private System.Windows.Forms.Panel panelAction;
        private System.Windows.Forms.Button btnKhoaTK;
        private System.Windows.Forms.Button btnResetPass;
        private System.Windows.Forms.Label label1;
    }
}