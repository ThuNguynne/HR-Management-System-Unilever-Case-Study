namespace UnileverHRM
{
    partial class frmMain
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.Panel panelSidebar;
        private System.Windows.Forms.Panel panelLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblLogoText;
        private System.Windows.Forms.Label lblLogoSubText;
        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnNhanSu;
        private System.Windows.Forms.Button btnTuyenDung;
        private System.Windows.Forms.Button btnChamCong;
        private System.Windows.Forms.Button btnLuong;
        private System.Windows.Forms.Button btnDaoTao;
        private System.Windows.Forms.Button btnBaoCao;
        private System.Windows.Forms.Button btnHeThong;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private System.Windows.Forms.Label lblRole;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelHeaderBottom;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelSidebar = new System.Windows.Forms.Panel();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnHeThong = new System.Windows.Forms.Button();
            this.btnBaoCao = new System.Windows.Forms.Button();
            this.btnDaoTao = new System.Windows.Forms.Button();
            this.btnLuong = new System.Windows.Forms.Button();
            this.btnChamCong = new System.Windows.Forms.Button();
            this.btnTuyenDung = new System.Windows.Forms.Button();
            this.btnNhanSu = new System.Windows.Forms.Button();
            this.btnDashboard = new System.Windows.Forms.Button();
            this.panelLogo = new System.Windows.Forms.Panel();
            this.lblLogoSubText = new System.Windows.Forms.Label();
            this.lblLogoText = new System.Windows.Forms.Label();
            this.panelHeader = new System.Windows.Forms.Panel();
            this.lblRole = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelHeaderBottom = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelSidebar.SuspendLayout();
            this.panelLogo.SuspendLayout();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelSidebar
            // 
            this.panelSidebar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.panelSidebar.Controls.Add(this.btnDangXuat);
            this.panelSidebar.Controls.Add(this.btnHeThong);
            this.panelSidebar.Controls.Add(this.btnBaoCao);
            this.panelSidebar.Controls.Add(this.btnDaoTao);
            this.panelSidebar.Controls.Add(this.btnLuong);
            this.panelSidebar.Controls.Add(this.btnChamCong);
            this.panelSidebar.Controls.Add(this.btnTuyenDung);
            this.panelSidebar.Controls.Add(this.btnNhanSu);
            this.panelSidebar.Controls.Add(this.btnDashboard);
            this.panelSidebar.Controls.Add(this.panelLogo);
            this.panelSidebar.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSidebar.Location = new System.Drawing.Point(0, 0);
            this.panelSidebar.Name = "panelSidebar";
            this.panelSidebar.Size = new System.Drawing.Size(260, 768);
            this.panelSidebar.TabIndex = 0;
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnDangXuat.FlatAppearance.BorderSize = 0;
            this.btnDangXuat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangXuat.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnDangXuat.ForeColor = System.Drawing.Color.White;
            this.btnDangXuat.Image = null;
            this.btnDangXuat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDangXuat.Location = new System.Drawing.Point(0, 708);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDangXuat.Size = new System.Drawing.Size(260, 60);
            this.btnDangXuat.TabIndex = 9;
            this.btnDangXuat.Text = "   🚪  Đăng xuất";
            this.btnDangXuat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDangXuat.UseVisualStyleBackColor = true;
            // 
            // btnHeThong
            // 
            this.btnHeThong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHeThong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnHeThong.FlatAppearance.BorderSize = 0;
            this.btnHeThong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHeThong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnHeThong.ForeColor = System.Drawing.Color.White;
            this.btnHeThong.Image = null;
            this.btnHeThong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHeThong.Location = new System.Drawing.Point(0, 550);
            this.btnHeThong.Name = "btnHeThong";
            this.btnHeThong.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnHeThong.Size = new System.Drawing.Size(260, 60);
            this.btnHeThong.TabIndex = 8;
            this.btnHeThong.Text = "   ⚙️  Quản trị hệ thống";
            this.btnHeThong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnHeThong.UseVisualStyleBackColor = true;
            // 
            // btnBaoCao
            // 
            this.btnBaoCao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnBaoCao.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnBaoCao.FlatAppearance.BorderSize = 0;
            this.btnBaoCao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnBaoCao.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnBaoCao.ForeColor = System.Drawing.Color.White;
            this.btnBaoCao.Image = null;
            this.btnBaoCao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCao.Location = new System.Drawing.Point(0, 490);
            this.btnBaoCao.Name = "btnBaoCao";
            this.btnBaoCao.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnBaoCao.Size = new System.Drawing.Size(260, 60);
            this.btnBaoCao.TabIndex = 7;
            this.btnBaoCao.Text = "   📊  Báo cáo & Thống kê";
            this.btnBaoCao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnBaoCao.UseVisualStyleBackColor = true;
            // 
            // btnDaoTao
            // 
            this.btnDaoTao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDaoTao.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDaoTao.FlatAppearance.BorderSize = 0;
            this.btnDaoTao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDaoTao.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnDaoTao.ForeColor = System.Drawing.Color.White;
            this.btnDaoTao.Image = null;
            this.btnDaoTao.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDaoTao.Location = new System.Drawing.Point(0, 430);
            this.btnDaoTao.Name = "btnDaoTao";
            this.btnDaoTao.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDaoTao.Size = new System.Drawing.Size(260, 60);
            this.btnDaoTao.TabIndex = 6;
            this.btnDaoTao.Text = "   🎓  Đào tạo & Phát triển";
            this.btnDaoTao.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDaoTao.UseVisualStyleBackColor = true;
            // 
            // btnLuong
            // 
            this.btnLuong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLuong.FlatAppearance.BorderSize = 0;
            this.btnLuong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLuong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnLuong.ForeColor = System.Drawing.Color.White;
            this.btnLuong.Image = null;
            this.btnLuong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLuong.Location = new System.Drawing.Point(0, 370);
            this.btnLuong.Name = "btnLuong";
            this.btnLuong.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnLuong.Size = new System.Drawing.Size(260, 60);
            this.btnLuong.TabIndex = 5;
            this.btnLuong.Text = "   💰  Lương thưởng & Phúc lợi";
            this.btnLuong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLuong.UseVisualStyleBackColor = true;
            // 
            // btnChamCong
            // 
            this.btnChamCong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChamCong.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnChamCong.FlatAppearance.BorderSize = 0;
            this.btnChamCong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnChamCong.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnChamCong.ForeColor = System.Drawing.Color.White;
            this.btnChamCong.Image = null;
            this.btnChamCong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChamCong.Location = new System.Drawing.Point(0, 310);
            this.btnChamCong.Name = "btnChamCong";
            this.btnChamCong.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnChamCong.Size = new System.Drawing.Size(260, 60);
            this.btnChamCong.TabIndex = 4;
            this.btnChamCong.Text = "   ⏰  Quản lý Chấm công";
            this.btnChamCong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnChamCong.UseVisualStyleBackColor = true;
            // 
            // btnTuyenDung
            // 
            this.btnTuyenDung.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTuyenDung.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTuyenDung.FlatAppearance.BorderSize = 0;
            this.btnTuyenDung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTuyenDung.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnTuyenDung.ForeColor = System.Drawing.Color.White;
            this.btnTuyenDung.Image = null;
            this.btnTuyenDung.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTuyenDung.Location = new System.Drawing.Point(0, 250);
            this.btnTuyenDung.Name = "btnTuyenDung";
            this.btnTuyenDung.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnTuyenDung.Size = new System.Drawing.Size(260, 60);
            this.btnTuyenDung.TabIndex = 3;
            this.btnTuyenDung.Text = "   👥  Tuyển dụng & Ứng viên";
            this.btnTuyenDung.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTuyenDung.UseVisualStyleBackColor = true;
            // 
            // btnNhanSu
            // 
            this.btnNhanSu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNhanSu.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnNhanSu.FlatAppearance.BorderSize = 0;
            this.btnNhanSu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNhanSu.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnNhanSu.ForeColor = System.Drawing.Color.White;
            this.btnNhanSu.Image = null;
            this.btnNhanSu.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhanSu.Location = new System.Drawing.Point(0, 190);
            this.btnNhanSu.Name = "btnNhanSu";
            this.btnNhanSu.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnNhanSu.Size = new System.Drawing.Size(260, 60);
            this.btnNhanSu.TabIndex = 2;
            this.btnNhanSu.Text = "   👤  Hồ sơ nhân sự";
            this.btnNhanSu.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnNhanSu.UseVisualStyleBackColor = true;
            // 
            // btnDashboard
            // 
            this.btnDashboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDashboard.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDashboard.FlatAppearance.BorderSize = 0;
            this.btnDashboard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDashboard.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnDashboard.ForeColor = System.Drawing.Color.White;
            this.btnDashboard.Image = null;
            this.btnDashboard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.Location = new System.Drawing.Point(0, 130);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Padding = new System.Windows.Forms.Padding(15, 0, 0, 0);
            this.btnDashboard.Size = new System.Drawing.Size(260, 60);
            this.btnDashboard.TabIndex = 1;
            this.btnDashboard.Text = "   🏠  Trang chủ (Dashboard)";
            this.btnDashboard.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // panelLogo
            // 
            this.panelLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(41)))), ((int)(((byte)(82)))));
            this.panelLogo.Controls.Add(this.lblLogoSubText);
            this.panelLogo.Controls.Add(this.lblLogoText);
            this.panelLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelLogo.Location = new System.Drawing.Point(0, 0);
            this.panelLogo.Name = "panelLogo";
            this.panelLogo.Size = new System.Drawing.Size(260, 130);
            this.panelLogo.TabIndex = 0;
            // 
            // lblLogoSubText
            // 
            this.lblLogoSubText.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblLogoSubText.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLogoSubText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(220)))), ((int)(((byte)(240)))));
            this.lblLogoSubText.Location = new System.Drawing.Point(0, 100);
            this.lblLogoSubText.Name = "lblLogoSubText";
            this.lblLogoSubText.Size = new System.Drawing.Size(260, 30);
            this.lblLogoSubText.TabIndex = 1;
            this.lblLogoSubText.Text = "Human Resource Management";
            this.lblLogoSubText.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblLogoText
            // 
            this.lblLogoText.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLogoText.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblLogoText.ForeColor = System.Drawing.Color.White;
            this.lblLogoText.Location = new System.Drawing.Point(0, 0);
            this.lblLogoText.Name = "lblLogoText";
            this.lblLogoText.Size = new System.Drawing.Size(260, 100);
            this.lblLogoText.TabIndex = 0;
            this.lblLogoText.Text = "UNILEVER\r\nHRMS";
            this.lblLogoText.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelHeader
            // 
            this.panelHeader.BackColor = System.Drawing.Color.White;
            this.panelHeader.Controls.Add(this.lblRole);
            this.panelHeader.Controls.Add(this.lblUser);
            this.panelHeader.Controls.Add(this.lblTitle);
            this.panelHeader.Controls.Add(this.panelHeaderBottom);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(260, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1164, 100);
            this.panelHeader.TabIndex = 1;
            // 
            // lblRole
            // 
            this.lblRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblRole.ForeColor = System.Drawing.Color.Gray;
            this.lblRole.Location = new System.Drawing.Point(800, 55);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(350, 25);
            this.lblRole.TabIndex = 3;
            this.lblRole.Text = "Quyền truy cập: Nhân viên hệ thống";
            this.lblRole.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUser
            // 
            this.lblUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblUser.Location = new System.Drawing.Point(800, 20);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(350, 35);
            this.lblUser.TabIndex = 2;
            this.lblUser.Text = "Xin chào, Nguyễn Văn A";
            this.lblUser.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = false;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(51)))), ((int)(((byte)(102)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(700, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TRANG CHỦ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelHeaderBottom
            // 
            this.panelHeaderBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(102)))), ((int)(((byte)(204)))));
            this.panelHeaderBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelHeaderBottom.Location = new System.Drawing.Point(0, 97);
            this.panelHeaderBottom.Name = "panelHeaderBottom";
            this.panelHeaderBottom.Size = new System.Drawing.Size(1164, 3);
            this.panelHeaderBottom.TabIndex = 1;
            // 
            // panelContent
            // 
            this.panelContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(260, 100);
            this.panelContent.Name = "panelContent";
            this.panelContent.Padding = new System.Windows.Forms.Padding(20);
            this.panelContent.Size = new System.Drawing.Size(1164, 668);
            this.panelContent.TabIndex = 2;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1424, 768);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelHeader);
            this.Controls.Add(this.panelSidebar);
            this.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.MinimumSize = new System.Drawing.Size(1280, 720);
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống Quản lý Nhân sự Unilever Việt Nam";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.panelSidebar.ResumeLayout(false);
            this.panelLogo.ResumeLayout(false);
            this.panelHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
    }
}