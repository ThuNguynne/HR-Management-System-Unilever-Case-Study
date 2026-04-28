using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace UnileverHRM
{
    public partial class frmBangLuong : Form
    {
        public frmBangLuong()
        {
            InitializeComponent();
        }

        private void frmBangLuong_Load(object sender, EventArgs e)
        {
            // Mặc định load dữ liệu lương tháng hiện tại
            LoadDataLuong(DateTime.Now);

            // Format hiển thị ngày tháng cho DateTimePicker
            dtpThang.Format = DateTimePickerFormat.Custom;
            dtpThang.CustomFormat = "MM/yyyy";
        }

        // Sự kiện bấm nút "Chạy tính lương (SP)"
        private void btnTinhLuong_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Hệ thống sẽ tính toán lại toàn bộ lương cho tháng " + dtpThang.Value.ToString("MM/yyyy") + ".\nDữ liệu cũ (nếu có) sẽ bị ghi đè. Bạn có chắc chắn?",
                "Xác nhận tính lương",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                RunTinhLuong();
            }
        }

        // Gọi Stored Procedure tính lương
        private void RunTinhLuong()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_TinhLuongThang", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Truyền tham số ngày tháng (lấy ngày đầu tháng)
                    DateTime selectedDate = new DateTime(dtpThang.Value.Year, dtpThang.Value.Month, 1);
                    cmd.Parameters.AddWithValue("@ThangNam", selectedDate);
                    cmd.Parameters.AddWithValue("@NguoiTao", "Admin"); // Lấy từ session đăng nhập thực tế

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Tính lương hoàn tất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Load lại lưới sau khi tính
                    LoadDataLuong(selectedDate);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tính lương: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Load dữ liệu lên Grid
        private void LoadDataLuong(DateTime thangNam)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    // Sử dụng SP lọc báo cáo lương
                    SqlCommand cmd = new SqlCommand("sp_BaoCaoLuong_Loc", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Thang", thangNam.Month);
                    cmd.Parameters.AddWithValue("@Nam", thangNam.Year);

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    dgvLuong.DataSource = dt;
                    FormatGrid();
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi im lặng hoặc log
            }
        }

        // Format giao diện bảng lương cho đẹp
        private void FormatGrid()
        {
            if (dgvLuong.Columns.Count == 0) return;

            dgvLuong.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvLuong.Columns["LuongThucNhan"].DefaultCellStyle.Format = "N0";
            dgvLuong.Columns["LuongCoBan"].DefaultCellStyle.Format = "N0";
            dgvLuong.Columns["TongThuNhap"].DefaultCellStyle.Format = "N0";
            dgvLuong.Columns["TongKhauTru"].DefaultCellStyle.Format = "N0";

            // Làm nổi bật cột Lương Thực Nhận
            dgvLuong.Columns["LuongThucNhan"].DefaultCellStyle.ForeColor = Color.Red;
            dgvLuong.Columns["LuongThucNhan"].DefaultCellStyle.Font = new Font(dgvLuong.Font, FontStyle.Bold);
            dgvLuong.Columns["LuongThucNhan"].HeaderText = "THỰC NHẬN";
        }

        // Sự kiện khi thay đổi tháng
        private void dtpThang_ValueChanged(object sender, EventArgs e)
        {
            LoadDataLuong(dtpThang.Value);
        }
    }
}