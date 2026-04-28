// frmChamCong.cs
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnileverHRM
{
    public partial class frmChamCong : Form
    {
        public frmChamCong()
        {
            InitializeComponent();
        }

        private void frmChamCong_Load(object sender, EventArgs e)
        {
            dtpNgayChamCong.Value = DateTime.Now;
            LoadChamCongNgay();
        }

        private void LoadChamCongNgay()
        {
            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    string query = @"
                        SELECT nv.MaNV, nv.HoTen, pb.TenPB, 
                               cc.GioVao, cc.GioRa, cc.TrangThai
                        FROM NhanVien nv
                        LEFT JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                        LEFT JOIN ChamCong cc ON nv.MaNV = cc.MaNV AND cc.Ngay = @Ngay
                        WHERE nv.TrangThai = N'Đang làm việc'";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Ngay", dtpNgayChamCong.Value.Date);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        dgvChamCong.DataSource = dt;
                        CapNhatThongKe(dt);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi tải dữ liệu chấm công: " + ex.Message);
                }
            }
        }

        private void CapNhatThongKe(DataTable dt)
        {
            int tongNV = dt.Rows.Count;
            int diLam = 0;
            int vang = 0;
            int diMuon = 0;

            foreach (DataRow row in dt.Rows)
            {
                bool coGioVao = row["GioVao"] != DBNull.Value;

                if (coGioVao)
                {
                    diLam++;

                    // Nếu cột TrangThai có dùng để đánh dấu đi muộn
                    if (row.Table.Columns.Contains("TrangThai") &&
                        row["TrangThai"] != DBNull.Value &&
                        row["TrangThai"].ToString().Equals("Đi muộn", StringComparison.OrdinalIgnoreCase))
                    {
                        diMuon++;
                    }
                }
                else
                {
                    vang++;
                }
            }

            lblTongNV.Text = $"Tổng nhân viên: {tongNV}";
            lblDiLam.Text = $"Đã Check-in: {diLam}";
            lblVang.Text = $"Vắng mặt: {vang}";
            lblDiMuon.Text = $"Đi muộn: {diMuon}";
        }

        private void dtpNgayChamCong_ValueChanged(object sender, EventArgs e)
        {
            LoadChamCongNgay();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dtpNgayChamCong.Value = DateTime.Now;
            LoadChamCongNgay();
        }

        private void btnXemThang_Click(object sender, EventArgs e)
        {
            // Tạm thời: load lại theo ngày đang chọn
            LoadChamCongNgay();
        }

        // Check-In (Vào làm)
        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            if (dgvChamCong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để chấm công!");
                return;
            }

            string maNV = dgvChamCong.SelectedRows[0].Cells["MaNV"].Value.ToString();
            DateTime ngay = dtpNgayChamCong.Value.Date;
            TimeSpan gioVao = DateTime.Now.TimeOfDay;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_ChamCong", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@Ngay", ngay);
                        cmd.Parameters.AddWithValue("@GioVao", gioVao);
                        cmd.Parameters.AddWithValue("@GioRa", DBNull.Value);
                        cmd.Parameters.AddWithValue("@NguoiTao", "Admin");

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Check-in thành công cho NV {maNV} lúc {DateTime.Now:HH:mm}!");
                    LoadChamCongNgay();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi chấm công: " + ex.Message);
                }
            }
        }

        // Check-Out (Ra về)
        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            if (dgvChamCong.SelectedRows.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để chấm công!");
                return;
            }

            string maNV = dgvChamCong.SelectedRows[0].Cells["MaNV"].Value.ToString();
            DateTime ngay = dtpNgayChamCong.Value.Date;
            TimeSpan gioRa = DateTime.Now.TimeOfDay;

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand("sp_ChamCong", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@MaNV", maNV);
                        cmd.Parameters.AddWithValue("@Ngay", ngay);
                        cmd.Parameters.AddWithValue("@GioVao", DBNull.Value);
                        cmd.Parameters.AddWithValue("@GioRa", gioRa);
                        cmd.Parameters.AddWithValue("@NguoiTao", "Admin");

                        cmd.ExecuteNonQuery();
                    }

                    MessageBox.Show($"Check-out thành công cho NV {maNV} lúc {DateTime.Now:HH:mm}!");
                    LoadChamCongNgay();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi chấm công ra: " + ex.Message);
                }
            }
        }
    }
}
