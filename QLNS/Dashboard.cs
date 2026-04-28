using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace UnileverHRM
{
    public partial class Dashboard : Form
    {
        // Lấy chuỗi kết nối từ DatabaseHelper (giả định bạn đã có class này từ file bạn upload)
        // Nếu chưa, hãy dùng: "Server=.;Database=UnileverHRM;Integrated Security=True";
        private string connectionString = @"Server=.;Database=UnileverHRM;Integrated Security=True";

        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            // Khi Dashboard được load vào frmMain, ta chỉ cần tải dữ liệu
            // Không cần xử lý phân quyền Sidebar ở đây nữa vì frmMain đã làm việc đó.
            LoadDashboardStats();
        }

        private void LoadDashboardStats()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // --- CARD 1: TỔNG NHÂN VIÊN (Đang làm việc) ---
                    string sqlNV = "SELECT Count(*) FROM NhanVien WHERE TrangThai = N'Đang làm việc'";
                    SqlCommand cmd = new SqlCommand(sqlNV, conn);
                    int totalNV = (int)cmd.ExecuteScalar();
                    SetupCard(lblTitle1, lblValue1, "Tổng nhân viên", totalNV.ToString(), Color.FromArgb(0, 51, 153));

                    // --- CARD 2: ỨNG VIÊN MỚI ---
                    string sqlUV = "SELECT Count(*) FROM UngVien WHERE TrangThai = N'Mới ứng tuyển'";
                    cmd.CommandText = sqlUV;
                    int newUV = (int)cmd.ExecuteScalar();
                    SetupCard(lblTitle2, lblValue2, "Ứng viên mới", newUV.ToString(), Color.Green);

                    // --- CARD 3: NHÂN VIÊN NGHỈ/TẠM NGHỈ ---
                    string sqlNghi = "SELECT Count(*) FROM NhanVien WHERE TrangThai != N'Đang làm việc'";
                    cmd.CommandText = sqlNghi;
                    int nghiViec = (int)cmd.ExecuteScalar();
                    SetupCard(lblTitle3, lblValue3, "Nghỉ việc/Tạm nghỉ", nghiViec.ToString(), Color.Orange);

                    // --- CARD 4: TỔNG LƯƠNG THÁNG NÀY ---
                    // Lưu ý: Cần kiểm tra quyền xem lương ở frmMain hoặc UserSession trước khi hiển thị số liệu nhạy cảm này
                    // Ở đây ta cứ load, nếu user không có quyền thì frmMain sẽ không cho họ vào các mục sâu hơn
                    string sqlLuong = "SELECT ISNULL(SUM(LuongThucNhan), 0) FROM BangLuong WHERE MONTH(ThangNam) = MONTH(GETDATE()) AND YEAR(ThangNam) = YEAR(GETDATE())";
                    cmd.CommandText = sqlLuong;
                    object resultLuong = cmd.ExecuteScalar();
                    decimal tongLuong = resultLuong != DBNull.Value ? Convert.ToDecimal(resultLuong) : 0;
                    SetupCard(lblTitle4, lblValue4, "Quỹ lương tháng", FormatCurrency(tongLuong), Color.Red);

                    // --- BIỂU ĐỒ: NHÂN SỰ THEO PHÒNG BAN ---
                    LoadChartByDepartment(conn);
                }
            }
            catch (Exception ex)
            {
                // Trong thực tế nên ghi log, tránh hiện popup lỗi quá nhiều
                // MessageBox.Show("Lỗi tải dữ liệu Dashboard: " + ex.Message);
                lblChartTitle.Text = "Không thể tải dữ liệu biểu đồ.";
            }
        }

        private void SetupCard(Label lblTitle, Label lblValue, string title, string value, Color color)
        {
            lblTitle.Text = title;
            lblValue.Text = value;
            lblValue.ForeColor = color;
        }

        private string FormatCurrency(decimal value)
        {
            if (value >= 1000000000) return (value / 1000000000).ToString("0.##") + " tỷ";
            if (value >= 1000000) return (value / 1000000).ToString("0.##") + " triệu";
            return value.ToString("N0");
        }

        private void LoadChartByDepartment(SqlConnection conn)
        {
            string query = @"SELECT pb.TenPB, COUNT(nv.MaNV) as SoLuong 
                             FROM PhongBan pb 
                             LEFT JOIN NhanVien nv ON pb.MaPB = nv.MaPB AND nv.TrangThai = N'Đang làm việc'
                             GROUP BY pb.TenPB";

            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);

            chartThongKe.Series["Series1"].ChartType = SeriesChartType.Column;
            chartThongKe.Series["Series1"].Points.Clear();
            chartThongKe.ChartAreas[0].AxisX.Interval = 1; // Hiển thị hết tên trục X

            foreach (DataRow row in dt.Rows)
            {
                // Thêm điểm vào biểu đồ: X = Tên phòng, Y = Số lượng
                chartThongKe.Series["Series1"].Points.AddXY(row["TenPB"], row["SoLuong"]);
            }
            lblChartTitle.Text = "Thống kê nhân sự theo phòng ban";
        }
    }
}