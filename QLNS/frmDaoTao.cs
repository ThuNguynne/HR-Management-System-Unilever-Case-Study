using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnileverHRM
{
    public partial class frmDaoTao : Form
    {
        public frmDaoTao()
        {
            InitializeComponent();
        }

        private void frmDaoTao_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // View danh sách khóa học
                    string query = "SELECT MaDT, TenKhoaHoc, GiangVien, DiaDiem, NgayBatDau, NgayKetThuc, SoLuongToiDa, TrangThai FROM DaoTao";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvDaoTao.DataSource = dt;

                    // Cập nhật thống kê
                    lblTongKhoa.Text = "Tổng số khóa học: " + dt.Rows.Count;
                    int active = dt.Select("TrangThai = 'Sắp diễn ra' OR TrangThai = 'Đang diễn ra'").Length;
                    lblDangMo.Text = "Đang mở: " + active;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu đào tạo: " + ex.Message);
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtTenKhoaHoc.Text))
            {
                MessageBox.Show("Vui lòng nhập tên khóa học!");
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"INSERT INTO DaoTao (TenKhoaHoc, GiangVien, DiaDiem, NgayBatDau, NgayKetThuc, SoLuongToiDa, TrangThai, NguoiTao) 
                                   VALUES (@TenKhoaHoc, @GiangVien, @DiaDiem, @NgayBatDau, @NgayKetThuc, @SoLuong, N'Sắp diễn ra', 'Admin')";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@TenKhoaHoc", txtTenKhoaHoc.Text);
                    cmd.Parameters.AddWithValue("@GiangVien", txtGiangVien.Text);
                    cmd.Parameters.AddWithValue("@DiaDiem", txtDiaDiem.Text);
                    cmd.Parameters.AddWithValue("@NgayBatDau", dtpNgayBatDau.Value);
                    cmd.Parameters.AddWithValue("@NgayKetThuc", dtpNgayKetThuc.Value);
                    cmd.Parameters.AddWithValue("@SoLuong", numSoLuong.Value);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm khóa đào tạo thành công!");
                    LoadData();
                    ClearInputs();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khóa học: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvDaoTao.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khóa học để xóa!");
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa khóa học này? Lưu ý: Không thể xóa nếu đã có học viên đăng ký.", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int maDT = Convert.ToInt32(dgvDaoTao.SelectedRows[0].Cells["MaDT"].Value);
                    using (SqlConnection conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        string query = "DELETE FROM DaoTao WHERE MaDT = @MaDT";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaDT", maDT);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Đã xóa khóa học!");
                        LoadData();
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Không thể xóa khóa học này vì đã có dữ liệu tham gia đào tạo liên quan. Vui lòng hủy đăng ký học viên trước.", "Lỗi ràng buộc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi hệ thống: " + ex.Message);
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
            LoadData();
        }

        private void btnDangKyHV_Click(object sender, EventArgs e)
        {
            if (dgvDaoTao.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn khóa học để quản lý danh sách học viên!");
                return;
            }
            string tenKhoa = dgvDaoTao.SelectedRows[0].Cells["TenKhoaHoc"].Value.ToString();
            MessageBox.Show($"Mở form quản lý học viên cho khóa: {tenKhoa}\n(Tính năng này sẽ được phát triển trong sprint tiếp theo)", "Thông báo");
        }

        private void ClearInputs()
        {
            txtTenKhoaHoc.Clear();
            txtGiangVien.Clear();
            txtDiaDiem.Clear();
            dtpNgayBatDau.Value = DateTime.Now;
            dtpNgayKetThuc.Value = DateTime.Now;
            numSoLuong.Value = 30;
        }

        private void dgvDaoTao_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvDaoTao.Rows[e.RowIndex];
                txtTenKhoaHoc.Text = row.Cells["TenKhoaHoc"].Value.ToString();
                txtGiangVien.Text = row.Cells["GiangVien"].Value.ToString();
                txtDiaDiem.Text = row.Cells["DiaDiem"].Value.ToString();
                dtpNgayBatDau.Value = Convert.ToDateTime(row.Cells["NgayBatDau"].Value);
                dtpNgayKetThuc.Value = Convert.ToDateTime(row.Cells["NgayKetThuc"].Value);
                if (row.Cells["SoLuongToiDa"].Value != DBNull.Value)
                    numSoLuong.Value = Convert.ToDecimal(row.Cells["SoLuongToiDa"].Value);
            }
        }
    }
}