using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace UnileverHRM
{
    public partial class frmBaoCao : Form
    {
        private string _userRole;
        private int _userId;
        private string _userFullName;

        public frmBaoCao(string role = "Admin", int userId = 0, string fullName = "Admin")
        {
            InitializeComponent();

            _userRole = role;
            _userId = userId;
            _userFullName = fullName;

            // Phóng to báo cáo: dùng PrintLayout + PageWidth
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.PageWidth;
        }

        private void frmBaoCao_Load(object sender, EventArgs e)
        {
            try
            {
                dtpTuNgay.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                dtpDenNgay.Value = DateTime.Now;

                reportViewer1.RefreshReport();

                LoadReportTypesByRole();
                LoadPhongBan();

                this.Text = $"Hệ thống Báo cáo & Thống kê Unilever - {_userFullName} ({_userRole})";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khởi tạo form báo cáo:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadReportTypesByRole()
        {
            var reports = new Dictionary<string, string>();

            switch (_userRole)
            {
                case "Admin":
                case "LanhDao":
                    reports.Add("RP_HR_STAT", "Thống kê nhân sự tổng quát");
                    reports.Add("RP_PAYROLL", "Báo cáo bảng lương toàn công ty");
                    reports.Add("RP_RECRUIT", "Báo cáo tình hình tuyển dụng");
                    reports.Add("RP_CONTRACT", "Hợp đồng sắp hết hạn");
                    reports.Add("RP_ATTENDANCE", "Tổng hợp chấm công");
                    reports.Add("RP_PERSONAL", "📋 Thông tin cá nhân");
                    break;

                case "QL_HR":
                case "NV_HR":
                    reports.Add("RP_HR_STAT", "Thống kê nhân sự tổng quát");
                    reports.Add("RP_RECRUIT", "Báo cáo tình hình tuyển dụng");
                    reports.Add("RP_CONTRACT", "Hợp đồng sắp hết hạn");
                    reports.Add("RP_ATTENDANCE", "Tổng hợp chấm công");
                    reports.Add("RP_PERSONAL", "📋 Thông tin cá nhân");
                    break;

                case "TaiChinh":
                    reports.Add("RP_PAYROLL", "Báo cáo bảng lương toàn công ty");
                    reports.Add("RP_ATTENDANCE", "Tổng hợp chấm công");
                    break;

                case "QL":
                    reports.Add("RP_HR_STAT", "Thống kê nhân sự tổng quát");
                    reports.Add("RP_ATTENDANCE", "Tổng hợp chấm công");
                    break;

                default:
                    break;
            }

            cboLoaiBaoCao.DataSource = new BindingSource(reports, null);
            cboLoaiBaoCao.DisplayMember = "Value";
            cboLoaiBaoCao.ValueMember = "Key";

            if (reports.Count > 0)
                cboLoaiBaoCao.SelectedIndex = 0;
        }

        private void LoadPhongBan()
        {
            try
            {
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    string query = "SELECT MaPB, TenPB FROM PhongBan WHERE TrangThai = 1 ORDER BY TenPB";
                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    DataRow row = dt.NewRow();
                    row["MaPB"] = 0;
                    row["TenPB"] = "--- Tất cả phòng ban ---";
                    dt.Rows.InsertAt(row, 0);

                    cboPhongBan.DataSource = dt;
                    cboPhongBan.DisplayMember = "TenPB";
                    cboPhongBan.ValueMember = "MaPB";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi load danh sách phòng ban:\n{ex.Message}",
                    "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnTaoBaoCao_Click(object sender, EventArgs e)
        {
            if (cboLoaiBaoCao.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn loại báo cáo!",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string reportType = cboLoaiBaoCao.SelectedValue.ToString();
            GenerateReport(reportType);
        }

        private void GenerateReport(string reportType)
        {
            try
            {
                reportViewer1.LocalReport.DataSources.Clear();
                reportViewer1.ProcessingMode = ProcessingMode.Local;

                string sqlQuery = "";
                string reportFileName = "";
                string dataSetName = "DataSet1";

                int selectedPhongBan = 0;
                if (cboPhongBan.SelectedValue != null)
                    int.TryParse(cboPhongBan.SelectedValue.ToString(), out selectedPhongBan);

                switch (reportType)
                {
                    case "RP_HR_STAT":
                        sqlQuery = $"EXEC sp_Rpt_ThongKeNhanSu @MaPB={selectedPhongBan}";
                        reportFileName = "Rpt_ThongKeNhanSu.rdlc";
                        break;

                    case "RP_PAYROLL":
                        sqlQuery = $"EXEC sp_BaoCaoLuong_Loc @Thang={dtpTuNgay.Value.Month}, " +
                                   $"@Nam={dtpTuNgay.Value.Year}, @MaPB={selectedPhongBan}";
                        reportFileName = "Rpt_BangLuong.rdlc";
                        break;

                    case "RP_RECRUIT":
                        sqlQuery = "SELECT * FROM vw_UngVienDangXuLy";
                        reportFileName = "Rpt_TuyenDung.rdlc";
                        break;

                    case "RP_CONTRACT":
                        sqlQuery = "SELECT * FROM vw_HopDongSapHetHan";
                        reportFileName = "Rpt_HopDong.rdlc";
                        break;

                    case "RP_ATTENDANCE":
                        sqlQuery = $"SELECT * FROM vw_ChamCongThang " +
                                   $"WHERE Thang = {dtpTuNgay.Value.Month} AND Nam = {dtpTuNgay.Value.Year}";
                        if (selectedPhongBan > 0)
                            sqlQuery += $" AND MaPB = {selectedPhongBan}";
                        reportFileName = "Rpt_ChamCong.rdlc";
                        break;

                    default:
                        MessageBox.Show("Chức năng báo cáo này đang được phát triển.",
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                }

                DataTable dt = new DataTable();
                using (SqlConnection conn = DatabaseHelper.GetConnection())
                {
                    conn.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, conn);
                    adapter.Fill(dt);
                }

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu cho điều kiện lọc này.",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    reportViewer1.Clear();
                    return;
                }

                string reportPath = FindReportFile(reportFileName);

                if (string.IsNullOrEmpty(reportPath))
                {
                    string currentFolder = Path.GetDirectoryName(Application.ExecutablePath);
                    string message = $"KHÔNG TÌM THẤY FILE REPORT: {reportFileName}\n\n" +
                                   $"Thư mục chạy hiện tại: {currentFolder}\n\n" +
                                   "Giải pháp:\n" +
                                   "1. Đảm bảo trong project có folder 'Report', chứa file .rdlc tương ứng\n" +
                                   "2. Thuộc tính file: Build Action = Content, Copy to Output Directory = Copy always\n" +
                                   "3. Build lại project và chạy lại chương trình.";
                    MessageBox.Show(message, "Hướng dẫn khắc phục",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                reportViewer1.LocalReport.ReportPath = reportPath;

                ReportDataSource rds = new ReportDataSource(dataSetName, dt);
                reportViewer1.LocalReport.DataSources.Add(rds);

                try
                {
                    var parameters = new List<ReportParameter>();
                    parameters.Add(new ReportParameter("pNguoiLap", _userFullName));
                    parameters.Add(new ReportParameter("pNgayLap", DateTime.Now.ToString("dd/MM/yyyy HH:mm")));

                    string tieuDeBaoCao = cboLoaiBaoCao.Text.ToUpper();
                    if (reportType == "RP_PAYROLL")
                        tieuDeBaoCao += $" THÁNG {dtpTuNgay.Value.Month}/{dtpTuNgay.Value.Year}";

                    parameters.Add(new ReportParameter("pTieuDe", tieuDeBaoCao));

                    reportViewer1.LocalReport.SetParameters(parameters);
                }
                catch { }

                reportViewer1.RefreshReport();

                MessageBox.Show($"✓ Đã tạo báo cáo thành công!\n\n" +
                              $"Loại: {cboLoaiBaoCao.Text}\n" +
                              $"Số dòng dữ liệu: {dt.Rows.Count}\n" +
                              $"File: {reportFileName}",
                              "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                string errorMessage = $"Lỗi hiển thị báo cáo:\n\n{ex.Message}\n";
                MessageBox.Show(errorMessage, "Lỗi Hệ Thống",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string FindReportFile(string reportFileName)
        {
            string[] possiblePaths = new string[]
            {
                Path.Combine(Application.StartupPath, "Report", reportFileName),
                Path.Combine(Application.StartupPath, "Reports", reportFileName),

                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Report", reportFileName),
                Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports", reportFileName),

                Path.Combine(Application.StartupPath, reportFileName),
                reportFileName
            };

            foreach (string path in possiblePaths)
            {
                if (File.Exists(path))
                    return path;
            }

            return null;
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer1.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu báo cáo để xuất!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string extension;

                byte[] bytes = reportViewer1.LocalReport.Render(
                    "Excel", null, out mimeType, out encoding,
                    out extension, out streamids, out warnings);

                SaveFileDialog saveDialog = new SaveFileDialog();
                saveDialog.Filter = "Excel Files|*.xls";
                saveDialog.FileName = $"BaoCao_{DateTime.Now:yyyyMMdd_HHmmss}.xls";

                if (saveDialog.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllBytes(saveDialog.FileName, bytes);
                    MessageBox.Show($"✓ Xuất file Excel thành công!\n\nĐã lưu tại:\n{saveDialog.FileName}",
                        "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi xuất file Excel:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (reportViewer1.LocalReport.DataSources.Count == 0)
                {
                    MessageBox.Show("Chưa có dữ liệu báo cáo để in!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                reportViewer1.PrintDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi in báo cáo:\n{ex.Message}",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cboLoaiBaoCao_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiBaoCao.SelectedValue != null)
            {
                string reportType = cboLoaiBaoCao.SelectedValue.ToString();

                bool showDateFilter = reportType.Contains("PAYROLL") ||
                                      reportType.Contains("ATTENDANCE");

                lblTuNgay.Visible = showDateFilter;
                dtpTuNgay.Visible = showDateFilter;
                lblDenNgay.Visible = false;
                dtpDenNgay.Visible = false;

                bool showDeptFilter = !reportType.Contains("PERSONAL");
                lblPhongBan.Visible = showDeptFilter;
                cboPhongBan.Visible = showDeptFilter;
            }
        }
    }
}
