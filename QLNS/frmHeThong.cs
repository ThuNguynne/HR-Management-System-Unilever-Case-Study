using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnileverHRM
{
    public partial class frmHeThong : Form
    {
        public frmHeThong()
        {
            InitializeComponent();
        }

        private void frmHeThong_Load(object sender, EventArgs e)
        {
            LoadTaiKhoan();
        }

        private void LoadTaiKhoan()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT nd.TenDangNhap, nd.VaiTro, nv.HoTen, nd.TrangThai, nd.LanDangNhapCuoi 
                                     FROM NguoiDung nd 
                                     LEFT JOIN NhanVien nv ON nd.MaNV = nv.MaNV";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvTaiKhoan.DataSource = dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải tài khoản: " + ex.Message);
                }
            }
        }

        private void btnResetPass_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0) return;
            string user = dgvTaiKhoan.SelectedRows[0].Cells["TenDangNhap"].Value.ToString();

            if (MessageBox.Show($"Đặt lại mật khẩu cho '{user}' về mặc định '123456'?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // Demo reset pass đơn giản
                    string query = "UPDATE NguoiDung SET MatKhau = '123456' WHERE TenDangNhap = @User";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@User", user);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã reset mật khẩu thành công!");
                }
            }
        }

        private void btnKhoaTK_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.SelectedRows.Count == 0) return;
            string user = dgvTaiKhoan.SelectedRows[0].Cells["TenDangNhap"].Value.ToString();
            bool currentStatus = Convert.ToBoolean(dgvTaiKhoan.SelectedRows[0].Cells["TrangThai"].Value);

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                conn.Open();
                string query = "UPDATE NguoiDung SET TrangThai = @Status WHERE TenDangNhap = @User";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@Status", !currentStatus); // Đảo trạng thái
                cmd.Parameters.AddWithValue("@User", user);
                cmd.ExecuteNonQuery();

                MessageBox.Show(currentStatus ? "Đã khóa tài khoản!" : "Đã mở khóa tài khoản!");
                LoadTaiKhoan();
            }
        }
    }
}