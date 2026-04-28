using System;

namespace UnileverHRM
{
    // Lưu trữ thông tin phiên làm việc
    public static class UserSession
    {
        public static string Username { get; set; }
        public static string Role { get; set; }     // Vai trò khớp với SQL (VD: "QL_HR")
        public static string FullName { get; set; } // Họ tên hiển thị
        public static int EmployeeId { get; set; }  // Mã NV để truy vấn dữ liệu cá nhân

        public static void Clear()
        {
            Username = null;
            Role = null;
            FullName = null;
            EmployeeId = 0;
        }
    }

    // Định nghĩa hằng số vai trò KHỚP CHÍNH XÁC với Database SQL v4
    public static class RoleConst
    {
        public const string ADMIN = "Admin";        // Quản trị hệ thống
        public const string LANH_DAO = "LanhDao";   // Ban Giám đốc
        public const string HR_MANAGER = "QL_HR";   // Trưởng phòng Nhân sự
        public const string HR_STAFF = "NV_HR";     // Nhân viên Nhân sự
        public const string TAI_CHINH = "TaiChinh"; // Phòng Tài chính - Kế toán
        public const string QUAN_LY = "QL";         // Quản lý các phòng ban khác (Kỹ thuật, Marketing...)
        public const string NHAN_VIEN = "NV";       // Nhân viên bình thường
        public const string UNG_VIEN = "UngVien";   // Ứng viên (Dùng cho phân hệ tuyển dụng online/kiosk)
    }
}