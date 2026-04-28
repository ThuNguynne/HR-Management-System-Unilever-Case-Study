using System;
using System.Drawing;
using System.Windows.Forms;

namespace UnileverHRM
{
    public partial class frmMain : Form
    {
        private string _fullName;
        private string _role;

        public frmMain()
        {
            InitializeComponent();

            _fullName = UserSession.FullName;
            _role = UserSession.Role;

            if (string.IsNullOrEmpty(_fullName)) _fullName = "Debug User";
            if (string.IsNullOrEmpty(_role)) _role = RoleConst.ADMIN;

            UpdateUserInfo();
            BindEvents();
            ApplyAuthorization();
            LoadDefaultPage();
        }

        private void UpdateUserInfo()
        {
            lblUser.Text = $"{_fullName}";
            lblRole.Text = "Quyền: " + GetRoleNameVN(_role);
        }

        private string GetRoleNameVN(string roleCode)
        {
            switch (roleCode)
            {
                case RoleConst.ADMIN: return "Quản trị hệ thống";
                case RoleConst.LANH_DAO: return "Ban lãnh đạo";
                case RoleConst.HR_MANAGER: return "Trưởng phòng HR";
                case RoleConst.HR_STAFF: return "Nhân viên HR";
                case RoleConst.TAI_CHINH: return "Phòng Tài chính";
                case RoleConst.QUAN_LY: return "Quản lý";
                case RoleConst.NHAN_VIEN: return "Nhân viên";
                case RoleConst.UNG_VIEN: return "Ứng viên";
                default: return "Người dùng";
            }
        }

        private void BindEvents()
        {
            // Dashboard
            btnDashboard.Click += (s, e) =>
            {
                SetActiveButton(btnDashboard);
                OpenChildForm(new Dashboard());
            };

            // Hồ sơ nhân sự
            btnNhanSu.Click += (s, e) =>
            {
                SetActiveButton(btnNhanSu);
                OpenChildForm(new frmNhanVien());
            };

            // Tuyển dụng
            btnTuyenDung.Click += (s, e) =>
            {
                SetActiveButton(btnTuyenDung);
                OpenChildForm(new frmTuyenDung());
            };

            // Chấm công
            btnChamCong.Click += (s, e) =>
            {
                SetActiveButton(btnChamCong);
                OpenChildForm(new frmChamCong());
            };

            // Lương thưởng & phúc lợi
            btnLuong.Click += (s, e) =>
            {
                SetActiveButton(btnLuong);
                OpenChildForm(new frmBangLuong());
            };

            // ===== CÁC CHỨC NĂNG MỚI =====

            // Đào tạo & Phát triển
            btnDaoTao.Click += (s, e) =>
            {
                SetActiveButton(btnDaoTao);
                OpenChildForm(new frmDaoTao());
            };

            // Báo cáo & Thống kê
            btnBaoCao.Click += (s, e) =>
            {
                SetActiveButton(btnBaoCao);
                OpenChildForm(new frmBaoCao());
            };

            // Quản trị hệ thống
            btnHeThong.Click += (s, e) =>
            {
                SetActiveButton(btnHeThong);
                OpenChildForm(new frmHeThong());
            };

            // Đăng xuất
            btnDangXuat.Click += BtnDangXuat_Click;
        }

        private void BtnDangXuat_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show(
                "Bạn có chắc chắn muốn đăng xuất?",
                "Đăng xuất",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                UserSession.Clear();

                this.Hide();
                var login = new frmLogin();
                login.FormClosed += (s, args) => this.Close();
                login.Show();
            }
        }

        private void OpenChildForm(Form childForm)
        {
            if (panelContent.Controls.Count > 0)
            {
                var oldForm = panelContent.Controls[0] as Form;
                panelContent.Controls.Clear();
                if (oldForm != null) oldForm.Dispose();
            }

            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;

            panelContent.Controls.Add(childForm);
            panelContent.Tag = childForm;

            lblTitle.Text = childForm.Text.ToUpperInvariant();
            childForm.BringToFront();
            childForm.Show();
        }

        private void SetActiveButton(Button activeButton)
        {
            Color defaultColor = Color.FromArgb(0, 51, 102);
            Color activeColor = Color.FromArgb(0, 102, 204);

            // Reset tất cả các button về màu mặc định
            btnDashboard.BackColor = defaultColor;
            btnNhanSu.BackColor = defaultColor;
            btnTuyenDung.BackColor = defaultColor;
            btnChamCong.BackColor = defaultColor;
            btnLuong.BackColor = defaultColor;
            btnDaoTao.BackColor = defaultColor;
            btnBaoCao.BackColor = defaultColor;
            btnHeThong.BackColor = defaultColor;

            // Set button được chọn thành màu active
            activeButton.BackColor = activeColor;
        }

        private void ApplyAuthorization()
        {
            // Ứng viên: chỉ dùng phân hệ tuyển dụng
            if (_role == RoleConst.UNG_VIEN)
            {
                btnDashboard.Visible = false;
                btnNhanSu.Visible = false;
                btnChamCong.Visible = false;
                btnLuong.Visible = false;
                btnDaoTao.Visible = false;
                btnBaoCao.Visible = false;
                btnHeThong.Visible = false;
                return;
            }

            // Nhân viên bình thường: không xem danh sách nhân sú, không quản lý tuyển dụng, lương toàn công ty
            if (_role == RoleConst.NHAN_VIEN)
            {
                btnNhanSu.Visible = false;
                btnTuyenDung.Visible = false;
                btnLuong.Visible = false;
                btnBaoCao.Visible = false; // Nhân viên không xem báo cáo tổng hợp
                btnHeThong.Visible = false; // Nhân viên không quản trị hệ thống
            }

            // Nhân viên HR: không quản trị hệ thống
            if (_role == RoleConst.HR_STAFF)
            {
                btnHeThong.Visible = false;
            }

            // Quản lý: có thể xem báo cáo nhưng không quản trị hệ thống
            if (_role == RoleConst.QUAN_LY)
            {
                btnHeThong.Visible = false;
            }

            // Phòng tài chính: tập trung lương & báo cáo
            if (_role == RoleConst.TAI_CHINH)
            {
                btnNhanSu.Visible = false;
                btnTuyenDung.Visible = false;
                btnChamCong.Visible = false;
                btnDaoTao.Visible = false;
                btnHeThong.Visible = false;
            }

            // Admin, Lãnh đạo, Trưởng phòng HR: giữ nguyên, được xem đầy đủ
            // Không cần điều kiện gì thêm, mặc định tất cả đều visible
        }

        private void LoadDefaultPage()
        {
            SetActiveButton(btnDashboard);
            OpenChildForm(new Dashboard());
        }
    }
}