using System;
using System.Windows.Forms;

namespace UnileverHRM
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string user = txtUsername.Text.Trim();
            string pass = txtPassword.Text.Trim();

            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string role;
            string fullName;

            // Hàm kiểm tra đăng nhập từ DatabaseHelper
            if (DatabaseHelper.CheckLogin(user, pass, out role, out fullName))
            {
                MessageBox.Show($"Xin chào {fullName}!\nĐăng nhập thành công với vai trò: {role}", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // 1. LƯU THÔNG TIN VÀO SESSION TOÀN CỤC
                // Các thông tin này sẽ được frmMain tự động đọc khi khởi tạo
                UserSession.Username = user;
                UserSession.Role = role;
                UserSession.FullName = fullName;

                // 2. MỞ FORM MAIN
                // Bây giờ frmMain đã có constructor không tham số nên dòng này hợp lệ
                frmMain mainForm = new frmMain();

                this.Hide();
                mainForm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Code test nhanh (Uncomment nếu muốn test không cần DB)
                /*
                UserSession.Role = "Admin";
                UserSession.FullName = "Admin Test";
                frmMain main = new frmMain(); 
                this.Hide(); 
                main.ShowDialog(); 
                this.Close();
                */
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            // Focus vào ô user khi mở
            this.ActiveControl = txtUsername;
        }
    }
}