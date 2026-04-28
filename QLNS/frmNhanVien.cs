using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace UnileverHRM
{
    public partial class frmNhanVien : Form
    {
        private string currentMaNV = "";

        public frmNhanVien()
        {
            InitializeComponent();
            this.Text = "Quản lý Nhân viên";
        }

        // --- 1. SỰ KIỆN LOAD FORM ---
        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            LoadComboBoxes();
            LoadData();
            ResetForm();
            FormatDataGridView(); // Thêm formatting cho DataGridView
        }

        // --- 2. FORMAT DATAGRIDVIEW ĐỂ HIỂN THỊ RÕ RÀNG HƠN ---
        private void FormatDataGridView()
        {
            try
            {
                // Cấu hình chung cho DataGridView
                dgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;
                dgvNhanVien.AllowUserToAddRows = false;
                dgvNhanVien.AllowUserToDeleteRows = false;
                dgvNhanVien.ReadOnly = true;
                dgvNhanVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dgvNhanVien.MultiSelect = false;
                dgvNhanVien.RowHeadersVisible = false;
                dgvNhanVien.BackgroundColor = Color.White;
                dgvNhanVien.BorderStyle = BorderStyle.None;
                dgvNhanVien.DefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 102, 204);
                dgvNhanVien.DefaultCellStyle.SelectionForeColor = Color.White;
                dgvNhanVien.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 247, 250);
                dgvNhanVien.RowTemplate.Height = 35;
                dgvNhanVien.ColumnHeadersHeight = 40;
                dgvNhanVien.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 51, 102);
                dgvNhanVien.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
                dgvNhanVien.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
                dgvNhanVien.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dgvNhanVien.EnableHeadersVisualStyles = false;

                // Kiểm tra xem có cột nào chưa
                if (dgvNhanVien.Columns.Count == 0) return;

                // Đặt tên hiển thị và độ rộng cho các cột
                if (dgvNhanVien.Columns.Contains("MaNV"))
                {
                    dgvNhanVien.Columns["MaNV"].HeaderText = "Mã NV";
                    dgvNhanVien.Columns["MaNV"].Width = 80;
                    dgvNhanVien.Columns["MaNV"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvNhanVien.Columns.Contains("HoTen"))
                {
                    dgvNhanVien.Columns["HoTen"].HeaderText = "Họ và Tên";
                    dgvNhanVien.Columns["HoTen"].Width = 200;
                }

                if (dgvNhanVien.Columns.Contains("NgaySinh"))
                {
                    dgvNhanVien.Columns["NgaySinh"].HeaderText = "Ngày sinh";
                    dgvNhanVien.Columns["NgaySinh"].Width = 100;
                    dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvNhanVien.Columns["NgaySinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvNhanVien.Columns.Contains("GioiTinh"))
                {
                    dgvNhanVien.Columns["GioiTinh"].HeaderText = "Giới tính";
                    dgvNhanVien.Columns["GioiTinh"].Width = 80;
                    dgvNhanVien.Columns["GioiTinh"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvNhanVien.Columns.Contains("CMND"))
                {
                    dgvNhanVien.Columns["CMND"].HeaderText = "CMND/CCCD";
                    dgvNhanVien.Columns["CMND"].Width = 120;
                    dgvNhanVien.Columns["CMND"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvNhanVien.Columns.Contains("DienThoai"))
                {
                    dgvNhanVien.Columns["DienThoai"].HeaderText = "Điện thoại";
                    dgvNhanVien.Columns["DienThoai"].Width = 110;
                    dgvNhanVien.Columns["DienThoai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvNhanVien.Columns.Contains("Email"))
                {
                    dgvNhanVien.Columns["Email"].HeaderText = "Email";
                    dgvNhanVien.Columns["Email"].Width = 200;
                }

                if (dgvNhanVien.Columns.Contains("TenPB"))
                {
                    dgvNhanVien.Columns["TenPB"].HeaderText = "Phòng ban";
                    dgvNhanVien.Columns["TenPB"].Width = 150;
                }

                if (dgvNhanVien.Columns.Contains("TenCV"))
                {
                    dgvNhanVien.Columns["TenCV"].HeaderText = "Chức vụ";
                    dgvNhanVien.Columns["TenCV"].Width = 130;
                }

                if (dgvNhanVien.Columns.Contains("NgayVaoLam"))
                {
                    dgvNhanVien.Columns["NgayVaoLam"].HeaderText = "Vào làm";
                    dgvNhanVien.Columns["NgayVaoLam"].Width = 100;
                    dgvNhanVien.Columns["NgayVaoLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
                    dgvNhanVien.Columns["NgayVaoLam"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                if (dgvNhanVien.Columns.Contains("LuongCoBan"))
                {
                    dgvNhanVien.Columns["LuongCoBan"].HeaderText = "Lương CB";
                    dgvNhanVien.Columns["LuongCoBan"].Width = 120;
                    dgvNhanVien.Columns["LuongCoBan"].DefaultCellStyle.Format = "#,##0 đ";
                    dgvNhanVien.Columns["LuongCoBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

                if (dgvNhanVien.Columns.Contains("TrangThai"))
                {
                    dgvNhanVien.Columns["TrangThai"].HeaderText = "Trạng thái";
                    dgvNhanVien.Columns["TrangThai"].Width = 100;
                    dgvNhanVien.Columns["TrangThai"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                }

                // Ẩn các cột không cần thiết nếu có
                if (dgvNhanVien.Columns.Contains("MaPB"))
                    dgvNhanVien.Columns["MaPB"].Visible = false;
                if (dgvNhanVien.Columns.Contains("MaCV"))
                    dgvNhanVien.Columns["MaCV"].Visible = false;
                if (dgvNhanVien.Columns.Contains("DiaChiThuongTru"))
                    dgvNhanVien.Columns["DiaChiThuongTru"].Visible = false;
                if (dgvNhanVien.Columns.Contains("PhuCap"))
                    dgvNhanVien.Columns["PhuCap"].Visible = false;
                if (dgvNhanVien.Columns.Contains("NgayNghiViec"))
                    dgvNhanVien.Columns["NgayNghiViec"].Visible = false;
                if (dgvNhanVien.Columns.Contains("NguoiTao"))
                    dgvNhanVien.Columns["NguoiTao"].Visible = false;
                if (dgvNhanVien.Columns.Contains("NgayTao"))
                    dgvNhanVien.Columns["NgayTao"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi format DataGridView: " + ex.Message);
            }
        }

        // --- 3. CÁC HÀM HỖ TRỢ DATABASE ---
        private void LoadData()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM vw_ThongTinNhanVien ORDER BY MaNV DESC";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvNhanVien.DataSource = dt;

                    // Format lại DataGridView sau khi load dữ liệu
                    FormatDataGridView();

                    // Thêm màu sắc cho trạng thái
                    ApplyRowColors();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Thêm màu sắc cho các dòng theo trạng thái
        private void ApplyRowColors()
        {
            try
            {
                if (dgvNhanVien.Columns.Contains("TrangThai"))
                {
                    foreach (DataGridViewRow row in dgvNhanVien.Rows)
                    {
                        if (row.Cells["TrangThai"].Value != null)
                        {
                            string trangThai = row.Cells["TrangThai"].Value.ToString();
                            if (trangThai == "Nghỉ việc")
                            {
                                row.DefaultCellStyle.ForeColor = Color.Gray;
                                row.DefaultCellStyle.Font = new Font(dgvNhanVien.Font, FontStyle.Italic);
                            }
                            else if (trangThai == "Đang làm việc")
                            {
                                // Giữ nguyên màu mặc định
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void LoadComboBoxes()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();

                    // Phòng ban
                    SqlDataAdapter daPB = new SqlDataAdapter("SELECT MaPB, TenPB FROM PhongBan WHERE TrangThai = 1 ORDER BY TenPB", conn);
                    DataTable dtPB = new DataTable();
                    daPB.Fill(dtPB);
                    cboPhongBan.DataSource = dtPB;
                    cboPhongBan.DisplayMember = "TenPB";
                    cboPhongBan.ValueMember = "MaPB";
                    cboPhongBan.SelectedIndex = -1;

                    // Chức vụ
                    SqlDataAdapter daCV = new SqlDataAdapter("SELECT MaCV, TenCV FROM ChucVu WHERE TrangThai = 1 ORDER BY TenCV", conn);
                    DataTable dtCV = new DataTable();
                    daCV.Fill(dtCV);
                    cboChucVu.DataSource = dtCV;
                    cboChucVu.DisplayMember = "TenCV";
                    cboChucVu.ValueMember = "MaCV";
                    cboChucVu.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải danh mục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 4. SỰ KIỆN TRÊN GRIDVIEW ---
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dgvNhanVien.Rows[e.RowIndex];
                currentMaNV = row.Cells["MaNV"].Value.ToString();

                // Hiển thị thông tin lên các ô nhập
                txtHoTen.Text = row.Cells["HoTen"].Value.ToString();

                if (DateTime.TryParse(row.Cells["NgaySinh"].Value.ToString(), out DateTime ns))
                    dtpNgaySinh.Value = ns;

                cboGioiTinh.Text = row.Cells["GioiTinh"].Value.ToString();

                // Gọi hàm load chi tiết để lấy các trường không có trên lưới
                LoadChiTietNhanVien(currentMaNV);
            }
        }

        private void LoadChiTietNhanVien(string maNV)
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT * FROM NhanVien WHERE MaNV = @MaNV";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNV", maNV);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        txtCMND.Text = reader["CMND"].ToString();
                        txtDienThoai.Text = reader["DienThoai"].ToString();
                        txtEmail.Text = reader["Email"].ToString();
                        txtDiaChi.Text = reader["DiaChiThuongTru"].ToString();
                        txtLuongCoBan.Text = reader["LuongCoBan"].ToString();

                        if (reader["MaPB"] != DBNull.Value)
                            cboPhongBan.SelectedValue = reader["MaPB"];
                        if (reader["MaCV"] != DBNull.Value)
                            cboChucVu.SelectedValue = reader["MaCV"];

                        if (DateTime.TryParse(reader["NgayVaoLam"].ToString(), out DateTime nvl))
                            dtpNgayVaoLam.Value = nvl;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải chi tiết: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // --- 5. CÁC SỰ KIỆN NÚT BẤM ---
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("sp_ThemNhanVien", conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.Text);
                    cmd.Parameters.AddWithValue("@CMND", txtCMND.Text.Trim());
                    cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@DiaChiThuongTru", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaPB", cboPhongBan.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaCV", cboChucVu.SelectedValue);
                    cmd.Parameters.AddWithValue("@NgayVaoLam", dtpNgayVaoLam.Value);

                    decimal luong = 0;
                    decimal.TryParse(txtLuongCoBan.Text, out luong);
                    cmd.Parameters.AddWithValue("@LuongCoBan", luong);

                    cmd.Parameters.AddWithValue("@PhuCap", 0);
                    cmd.Parameters.AddWithValue("@NguoiTao", UserSession.FullName ?? "Admin");

                    SqlParameter outParam = new SqlParameter("@MaNV", SqlDbType.Int);
                    outParam.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outParam);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Thêm thành công! Mã NV: " + outParam.Value.ToString(), "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần sửa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!ValidateInput()) return;

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"UPDATE NhanVien SET 
                                    HoTen=@HoTen, NgaySinh=@NgaySinh, GioiTinh=@GioiTinh, 
                                    CMND=@CMND, DienThoai=@DienThoai, Email=@Email, 
                                    DiaChiThuongTru=@DiaChi, MaPB=@MaPB, MaCV=@MaCV, 
                                    NgayVaoLam=@NgayVaoLam, LuongCoBan=@Luong 
                                    WHERE MaNV=@MaNV";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@MaNV", currentMaNV);
                    cmd.Parameters.AddWithValue("@HoTen", txtHoTen.Text.Trim());
                    cmd.Parameters.AddWithValue("@NgaySinh", dtpNgaySinh.Value);
                    cmd.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.Text);
                    cmd.Parameters.AddWithValue("@CMND", txtCMND.Text.Trim());
                    cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text.Trim());
                    cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaPB", cboPhongBan.SelectedValue);
                    cmd.Parameters.AddWithValue("@MaCV", cboChucVu.SelectedValue);
                    cmd.Parameters.AddWithValue("@NgayVaoLam", dtpNgayVaoLam.Value);
                    cmd.Parameters.AddWithValue("@Luong", decimal.Parse(txtLuongCoBan.Text));

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData();
                    ResetForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi cập nhật: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(currentMaNV))
            {
                MessageBox.Show("Vui lòng chọn nhân viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Xác nhận cho nhân viên này nghỉ việc?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (SqlConnection conn = DatabaseHelper.GetConnection())
                    {
                        conn.Open();
                        string query = "UPDATE NhanVien SET TrangThai = N'Nghỉ việc', NgayNghiViec = GETDATE() WHERE MaNV = @MaNV";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@MaNV", currentMaNV);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Đã cập nhật trạng thái nghỉ việc!", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                        ResetForm();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string keyword = txtTimKiem.Text.Trim();

            if (string.IsNullOrEmpty(keyword))
            {
                LoadData(); // Load lại toàn bộ dữ liệu nếu không có từ khóa
                return;
            }

            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = @"SELECT * FROM vw_ThongTinNhanVien 
                                    WHERE HoTen LIKE @kw 
                                       OR DienThoai LIKE @kw 
                                       OR Email LIKE @kw 
                                       OR TenPB LIKE @kw
                                       OR TenCV LIKE @kw
                                    ORDER BY MaNV DESC";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@kw", "%" + keyword + "%");
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvNhanVien.DataSource = dt;
                    FormatDataGridView();
                    ApplyRowColors();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetForm();
            LoadData();
            txtTimKiem.Clear();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (dgvNhanVien.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "CSV (*.csv)|*.csv",
                FileName = "DanhSachNhanVien_" + DateTime.Now.ToString("yyyyMMdd") + ".csv"
            };

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(sfd.FileName, false, System.Text.Encoding.UTF8))
                    {
                        // Header - chỉ xuất các cột visible
                        for (int i = 0; i < dgvNhanVien.Columns.Count; i++)
                        {
                            if (dgvNhanVien.Columns[i].Visible)
                            {
                                sw.Write(dgvNhanVien.Columns[i].HeaderText);
                                if (i < dgvNhanVien.Columns.Count - 1) sw.Write(",");
                            }
                        }
                        sw.WriteLine();

                        // Rows - chỉ xuất các cột visible
                        foreach (DataGridViewRow row in dgvNhanVien.Rows)
                        {
                            for (int i = 0; i < dgvNhanVien.Columns.Count; i++)
                            {
                                if (dgvNhanVien.Columns[i].Visible)
                                {
                                    if (row.Cells[i].Value != null)
                                        sw.Write(row.Cells[i].Value.ToString().Replace(",", ";"));
                                    if (i < dgvNhanVien.Columns.Count - 1) sw.Write(",");
                                }
                            }
                            sw.WriteLine();
                        }
                    }
                    MessageBox.Show("Xuất file thành công!\nĐường dẫn: " + sfd.FileName, "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi xuất file: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // --- 6. HELPER FUNCTIONS ---
        private void ResetForm()
        {
            currentMaNV = "";
            txtHoTen.Clear();
            txtCMND.Clear();
            txtDienThoai.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            txtLuongCoBan.Text = "0";

            if (cboGioiTinh.Items.Count > 0)
                cboGioiTinh.SelectedIndex = 0;

            cboPhongBan.SelectedIndex = -1;
            cboChucVu.SelectedIndex = -1;
            dtpNgaySinh.Value = DateTime.Now.AddYears(-18);
            dtpNgayVaoLam.Value = DateTime.Now;
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                MessageBox.Show("Vui lòng nhập họ tên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTen.Focus();
                return false;
            }

            if (cboPhongBan.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn phòng ban!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboPhongBan.Focus();
                return false;
            }

            if (cboChucVu.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn chức vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboChucVu.Focus();
                return false;
            }

            // Validate CMND
            if (!string.IsNullOrWhiteSpace(txtCMND.Text) && txtCMND.Text.Length < 9)
            {
                MessageBox.Show("CMND/CCCD không hợp lệ (tối thiểu 9 số)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtCMND.Focus();
                return false;
            }

            // Validate phone
            if (!string.IsNullOrWhiteSpace(txtDienThoai.Text) && txtDienThoai.Text.Length < 10)
            {
                MessageBox.Show("Số điện thoại không hợp lệ (tối thiểu 10 số)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();
                return false;
            }

            // Validate email
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("Email không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return false;
            }

            // Validate lương
            decimal luong = 0;
            if (!decimal.TryParse(txtLuongCoBan.Text, out luong) || luong < 0)
            {
                MessageBox.Show("Lương cơ bản không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtLuongCoBan.Focus();
                return false;
            }

            return true;
        }
    }
}