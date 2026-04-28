using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UnileverHRM
{
    public static class DatabaseHelper
    {
        // Chuỗi kết nối (Đảm bảo Server đúng với máy của bạn)
        private static string connectionString = @"Server=.;Database=UnileverHRM;Integrated Security=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        // 1. Lấy danh sách nhân viên đầy đủ (JOIN các bảng để hiển thị tên thay vì mã)
        public static DataTable GetNhanVienFull()
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"
                        SELECT 
                            nv.MaNV, nv.HoTen, nv.NgaySinh, nv.GioiTinh, 
                            nv.CMND, nv.DienThoai, nv.Email, nv.DiaChiThuongTru, 
                            nv.NgayVaoLam, nv.LuongCoBan, nv.TrangThai,
                            pb.TenPB, cv.TenCV
                        FROM NhanVien nv
                        LEFT JOIN PhongBan pb ON nv.MaPB = pb.MaPB
                        LEFT JOIN ChucVu cv ON nv.MaCV = cv.MaCV";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy dữ liệu nhân viên: " + ex.Message);
                    return null;
                }
            }
        }

        // 2. Hàm FIX LỖI "Invalid column name 'TenPB'" cho biểu đồ
        public static DataTable GetThongKeNhanSuTheoPhongBan()
        {
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    // Query này JOIN bảng NhanVien và PhongBan để lấy TenPB chính xác
                    string query = @"
                        SELECT pb.TenPB, COUNT(nv.MaNV) as SoLuong
                        FROM PhongBan pb
                        LEFT JOIN NhanVien nv ON pb.MaPB = nv.MaPB
                        GROUP BY pb.TenPB";

                    SqlDataAdapter da = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    return dt;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi lấy dữ liệu biểu đồ: " + ex.Message);
                    return null;
                }
            }
        }

        // 3. Lấy danh sách phòng ban (cho ComboBox)
        public static DataTable GetPhongBanList()
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaPB, TenPB FROM PhongBan", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // 4. Lấy danh sách chức vụ (cho ComboBox)
        public static DataTable GetChucVuList()
        {
            using (SqlConnection conn = GetConnection())
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT MaCV, TenCV FROM ChucVu", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        // Hàm kiểm tra đăng nhập (Giữ nguyên logic cũ của bạn)
        public static bool CheckLogin(string username, string password, out string role, out string fullName)
        {
            role = "";
            fullName = "";
            using (SqlConnection conn = GetConnection())
            {
                try
                {
                    conn.Open();
                    string query = @"SELECT nd.VaiTro, nv.HoTen 
                                   FROM NguoiDung nd 
                                   LEFT JOIN NhanVien nv ON nd.MaNV = nv.MaNV 
                                   WHERE nd.TenDangNhap = @user AND nd.MatKhau = @pass AND nd.TrangThai = 1";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@user", username);
                        cmd.Parameters.AddWithValue("@pass", password);
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                role = reader["VaiTro"].ToString();
                                fullName = reader["HoTen"] != DBNull.Value ? reader["HoTen"].ToString() : username;
                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi đăng nhập: " + ex.Message); }
            }
            return false;
        }
    }
}