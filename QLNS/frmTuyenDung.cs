using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnileverHRM
{
    public partial class frmTuyenDung : Form
    {
        public frmTuyenDung()
        {
            InitializeComponent();
        }

        // Sự kiện khi Form Tuyển Dụng được mở lên
        private void frmTuyenDung_Load(object sender, EventArgs e)
        {
            // 1. Tải dữ liệu ứng viên từ CSDL lên lưới
            LoadUngVien();

            // 2. CẤU HÌNH GIAO DIỆN (QUAN TRỌNG)
            // Dòng lệnh này giúp bảng tự động giãn đầy màn hình như bạn muốn
            dgvTuyenDung.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Tinh chỉnh thêm cho đẹp
            dgvTuyenDung.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Chọn cả dòng
            dgvTuyenDung.RowHeadersVisible = false; // Ẩn cột mũi tên chỉ dòng thừa bên trái
            dgvTuyenDung.AllowUserToAddRows = false; // Không cho user tự thêm dòng trống ở cuối
            dgvTuyenDung.ReadOnly = true; // Chỉ xem, không sửa trực tiếp trên lưới
        }

        // Hàm lấy dữ liệu từ SQL
        private void LoadUngVien()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Câu truy vấn lấy thông tin ứng viên và tên chức vụ ứng tuyển
                    // LEFT JOIN để lấy tên chức vụ thay vì mã chức vụ
                    string query = @"SELECT 
                                        uv.MaUV, 
                                        uv.HoTen, 
                                        uv.DienThoai, 
                                        uv.Email, 
                                        cv.TenCV AS ViTriUngTuyen, 
                                        uv.TrangThai, 
                                        uv.NgayUngTuyen
                                     FROM UngVien uv
                                     LEFT JOIN ChucVu cv ON uv.MaCV = cv.MaCV";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    // Gán dữ liệu vào DataGridView
                    dgvTuyenDung.DataSource = dt;

                    // Đặt tên tiếng Việt cho tiêu đề cột (Header)
                    if (dgvTuyenDung.Columns.Count > 0)
                    {
                        dgvTuyenDung.Columns["MaUV"].HeaderText = "Mã UV";
                        dgvTuyenDung.Columns["HoTen"].HeaderText = "Họ Tên";
                        dgvTuyenDung.Columns["DienThoai"].HeaderText = "Điện Thoại";
                        dgvTuyenDung.Columns["Email"].HeaderText = "Email";
                        dgvTuyenDung.Columns["ViTriUngTuyen"].HeaderText = "Vị Trí Ứng Tuyển";
                        dgvTuyenDung.Columns["TrangThai"].HeaderText = "Trạng Thái";
                        dgvTuyenDung.Columns["NgayUngTuyen"].HeaderText = "Ngày Ứng Tuyển";

                        // Định dạng ngày tháng cho đẹp (dd/MM/yyyy)
                        dgvTuyenDung.Columns["NgayUngTuyen"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu ứng viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}