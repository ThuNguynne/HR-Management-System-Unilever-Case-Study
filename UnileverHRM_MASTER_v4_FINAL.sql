-- =============================================
-- Hệ thống Quản lý Nhân sự Unilever Việt Nam
-- PHẦN 0: KIỂM TRA VÀ XÓA DATABASE CŨ (NẾU CẦN)
-- =============================================
USE master;
GO

-- Drop database nếu đã tồn tại
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'UnileverHRM')
BEGIN
    PRINT N'⚠ Database UnileverHRM đã tồn tại. Đang xóa...';
    
    -- Ngắt tất cả connections
    ALTER DATABASE UnileverHRM SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    
    -- Xóa database
    DROP DATABASE UnileverHRM;
    
    PRINT N'✓ Đã xóa database cũ thành công';
END
ELSE
BEGIN
    PRINT N'✓ Không tìm thấy database cũ, tiếp tục tạo mới';
END
GO

-- Tạo database mới
CREATE DATABASE UnileverHRM;
GO

PRINT N'✓ Đã tạo database UnileverHRM thành công';
GO

USE UnileverHRM;
GO

-- =============================================
-- PHẦN 1: TẠO CÁC BẢNG CƠ SỞ DỮ LIỆU
-- =============================================

PRINT N'';
PRINT N'================================================';
PRINT N'  ĐANG TẠO CÁC BẢNG CƠ SỞ DỮ LIỆU';
PRINT N'================================================';
GO

-- Bảng 1: PhongBan (Phòng ban)
CREATE TABLE PhongBan (
    MaPB INT IDENTITY(1,1) PRIMARY KEY,
    TenPB NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(500),
    NgayThanhLap DATE,
    TrangThai BIT DEFAULT 1,
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT CHK_PhongBan_TenPB CHECK (LEN(TenPB) >= 3)
);
PRINT N'✓ Đã tạo bảng PhongBan';

-- Bảng 2: ChucVu (Chức vụ)
CREATE TABLE ChucVu (
    MaCV INT IDENTITY(1,1) PRIMARY KEY,
    TenCV NVARCHAR(100) NOT NULL UNIQUE,
    MoTa NVARCHAR(500),
    CapBac INT CHECK (CapBac BETWEEN 1 AND 10),
    MucLuongToiThieu DECIMAL(18,0),
    MucLuongToiDa DECIMAL(18,0),
    TrangThai BIT DEFAULT 1,
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT CHK_ChucVu_Luong CHECK (MucLuongToiDa >= MucLuongToiThieu)
);
PRINT N'✓ Đã tạo bảng ChucVu';

-- Bảng 3: NhanVien (Nhân viên)
CREATE TABLE NhanVien (
    MaNV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE NOT NULL,
    GioiTinh NVARCHAR(10) CHECK (GioiTinh IN (N'Nam', N'Nữ', N'Khác')),
    CMND NVARCHAR(20) UNIQUE,
    DienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    DiaChiThuongTru NVARCHAR(255),
    DiaChiTamTru NVARCHAR(255),
    MaPB INT NOT NULL,
    MaCV INT NOT NULL,
    NgayVaoLam DATE NOT NULL,
    NgayNghiViec DATE,
    LuongCoBan DECIMAL(18,0) NOT NULL CHECK (LuongCoBan > 0),
    PhuCap DECIMAL(18,0) DEFAULT 0 CHECK (PhuCap >= 0),
    HeSoLuong DECIMAL(5,2) DEFAULT 1.0,
    SoTaiKhoan NVARCHAR(50),
    NganHang NVARCHAR(100),
    TrangThai NVARCHAR(20) DEFAULT N'Đang làm việc' CHECK (TrangThai IN (N'Đang làm việc', N'Nghỉ việc', N'Tạm nghỉ')),
    SoNgayPhep INT DEFAULT 12 CHECK (SoNgayPhep >= 0),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_NhanVien_PhongBan FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB),
    CONSTRAINT FK_NhanVien_ChucVu FOREIGN KEY (MaCV) REFERENCES ChucVu(MaCV),
    CONSTRAINT CHK_NhanVien_NgaySinh CHECK (NgaySinh <= DATEADD(YEAR, -18, GETDATE())),
    CONSTRAINT CHK_NhanVien_NgayVaoLam CHECK (NgayVaoLam >= NgaySinh),
    CONSTRAINT CHK_NhanVien_Email CHECK (Email LIKE '%@%' OR Email IS NULL)
);
PRINT N'✓ Đã tạo bảng NhanVien';

-- Bảng 4: HopDong (Hợp đồng lao động)
CREATE TABLE HopDong (
    MaHD INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    SoHopDong NVARCHAR(50) UNIQUE NOT NULL,
    LoaiHopDong NVARCHAR(50) CHECK (LoaiHopDong IN (N'Thử việc', N'Xác định thời hạn', N'Không xác định thời hạn', N'Thời vụ')),
    NgayKy DATE NOT NULL,
    NgayHieuLuc DATE NOT NULL,
    NgayHetHan DATE,
    LuongHopDong DECIMAL(18,0) NOT NULL,
    DieuKhoanDacBiet NVARCHAR(MAX),
    TrangThai NVARCHAR(20) DEFAULT N'Có hiệu lực' CHECK (TrangThai IN (N'Có hiệu lực', N'Hết hạn', N'Đã thanh lý')),
    FileDinhKem NVARCHAR(255),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_HopDong_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT CHK_HopDong_NgayHieuLuc CHECK (NgayHieuLuc >= NgayKy),
    CONSTRAINT CHK_HopDong_NgayHetHan CHECK (NgayHetHan IS NULL OR NgayHetHan > NgayHieuLuc)
);
PRINT N'✓ Đã tạo bảng HopDong';

-- Bảng 5: UngVien (Ứng viên tuyển dụng)
CREATE TABLE UngVien (
    MaUV INT IDENTITY(1,1) PRIMARY KEY,
    HoTen NVARCHAR(100) NOT NULL,
    NgaySinh DATE,
    GioiTinh NVARCHAR(10),
    DienThoai NVARCHAR(15),
    Email NVARCHAR(100),
    DiaChiLienHe NVARCHAR(255),
    MaCV INT,
    NgayUngTuyen DATE DEFAULT GETDATE(),
    NguonUngTuyen NVARCHAR(100),
    TrangThai NVARCHAR(50) DEFAULT N'Mới ứng tuyển' 
        CHECK (TrangThai IN (N'Mới ứng tuyển', N'Đang xét hồ sơ', N'Đã liên hệ', N'Đang phỏng vấn', 
                             N'Chờ quyết định', N'Trúng tuyển', N'Đã loại', N'Từ chối offer')),
    NgayPhongVan DATETIME,
    KetQuaPhongVan NVARCHAR(MAX),
    DiemPhongVan DECIMAL(4,2),
    NguoiPhongVan NVARCHAR(100),
    MucLuongDeNghi DECIMAL(18,0),
    FileHoSo NVARCHAR(255),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_UngVien_ChucVu FOREIGN KEY (MaCV) REFERENCES ChucVu(MaCV)
);
PRINT N'✓ Đã tạo bảng UngVien';

-- Bảng 6: ChamCong (Chấm công)
CREATE TABLE ChamCong (
    MaCC INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    Ngay DATE NOT NULL,
    GioVao TIME,
    GioRa TIME,
    GioCong DECIMAL(4,2) DEFAULT 0 CHECK (GioCong >= 0 AND GioCong <= 24),
    TangCa DECIMAL(4,2) DEFAULT 0 CHECK (TangCa >= 0 AND TangCa <= 12),
    DiMuon INT DEFAULT 0 CHECK (DiMuon >= 0),
    VeSom INT DEFAULT 0 CHECK (VeSom >= 0),
    TrangThai NVARCHAR(50) DEFAULT N'Đi làm' 
        CHECK (TrangThai IN (N'Đi làm', N'Nghỉ có phép', N'Nghỉ không phép', N'Nghỉ bệnh', N'Công tác', N'Nghỉ lễ', N'Phép năm', N'Nghỉ việc riêng', N'Nghỉ thai sản')),
    GhiChu NVARCHAR(255),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_ChamCong_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT UQ_ChamCong_NhanVien_Ngay UNIQUE (MaNV, Ngay)
);
PRINT N'✓ Đã tạo bảng ChamCong';

-- Bảng 7: NghiPhep (Đơn nghỉ phép)
CREATE TABLE NghiPhep (
    MaNP INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    LoaiNghi NVARCHAR(50) CHECK (LoaiNghi IN (N'Phép năm', N'Nghỉ bệnh', N'Nghỉ việc riêng', N'Nghỉ thai sản', N'Nghỉ không lương')),
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    SoNgayNghi AS (DATEDIFF(DAY, NgayBatDau, NgayKetThuc) + 1) PERSISTED,
    LyDo NVARCHAR(500) NOT NULL,
    NguoiDuyet INT,
    NgayNopDon DATETIME DEFAULT GETDATE(),
    NgayDuyet DATETIME,
    TrangThai NVARCHAR(20) DEFAULT N'Chờ duyệt' 
        CHECK (TrangThai IN (N'Chờ duyệt', N'Đã duyệt', N'Từ chối', N'Đã hủy')),
    LyDoTuChoi NVARCHAR(255),
    FileDinhKem NVARCHAR(255),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_NghiPhep_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_NghiPhep_NguoiDuyet FOREIGN KEY (NguoiDuyet) REFERENCES NhanVien(MaNV),
    CONSTRAINT CHK_NghiPhep_NgayKetThuc CHECK (NgayKetThuc >= NgayBatDau)
);
PRINT N'✓ Đã tạo bảng NghiPhep';

-- Bảng 8: DaoTao (Khóa đào tạo)
CREATE TABLE DaoTao (
    MaDT INT IDENTITY(1,1) PRIMARY KEY,
    TenKhoaHoc NVARCHAR(200) NOT NULL,
    MoTa NVARCHAR(MAX),
    NoiDung NVARCHAR(MAX),
    LoaiDaoTao NVARCHAR(50) CHECK (LoaiDaoTao IN (N'Kỹ năng mềm', N'Chuyên môn', N'Hội nhập', N'Bắt buộc', N'Nâng cao')),
    GiangVien NVARCHAR(100),
    DiaDiem NVARCHAR(255),
    NgayBatDau DATE NOT NULL,
    NgayKetThuc DATE NOT NULL,
    ThoiLuong INT CHECK (ThoiLuong > 0),
    HocPhi DECIMAL(18,0) DEFAULT 0,
    SoLuongToiDa INT CHECK (SoLuongToiDa > 0),
    TrangThai NVARCHAR(20) DEFAULT N'Sắp diễn ra' 
        CHECK (TrangThai IN (N'Sắp diễn ra', N'Đang diễn ra', N'Đã kết thúc', N'Đã hủy')),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT CHK_DaoTao_NgayKetThuc CHECK (NgayKetThuc >= NgayBatDau)
);
PRINT N'✓ Đã tạo bảng DaoTao';

-- Bảng 9: ThamGiaDaoTao (Nhân viên tham gia đào tạo)
CREATE TABLE ThamGiaDaoTao (
    MaTGDT INT IDENTITY(1,1) PRIMARY KEY,
    MaDT INT NOT NULL,
    MaNV INT NOT NULL,
    NgayDangKy DATETIME DEFAULT GETDATE(),
    TrangThaiThamGia NVARCHAR(50) DEFAULT N'Đã đăng ký' 
        CHECK (TrangThaiThamGia IN (N'Đã đăng ký', N'Đang học', N'Hoàn thành', N'Chưa hoàn thành', N'Vắng mặt')),
    DiemDanhGia DECIMAL(4,2) CHECK (DiemDanhGia IS NULL OR (DiemDanhGia >= 0 AND DiemDanhGia <= 10)),
    XepLoai NVARCHAR(20) CHECK (XepLoai IS NULL OR XepLoai IN (N'Xuất sắc', N'Giỏi', N'Khá', N'Trung bình', N'Yếu')),
    NhanXet NVARCHAR(500),
    ChungChiDinhKem NVARCHAR(255),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_ThamGiaDaoTao_DaoTao FOREIGN KEY (MaDT) REFERENCES DaoTao(MaDT),
    CONSTRAINT FK_ThamGiaDaoTao_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT UQ_ThamGiaDaoTao UNIQUE (MaDT, MaNV)
);
PRINT N'✓ Đã tạo bảng ThamGiaDaoTao';

-- Bảng 10: DanhGia (Đánh giá hiệu suất/KPI)
CREATE TABLE DanhGia (
    MaDG INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    KyDanhGia NVARCHAR(50) NOT NULL,
    NgayDanhGia DATE NOT NULL,
    NguoiDanhGia INT NOT NULL,
    LoaiDanhGia NVARCHAR(50) CHECK (LoaiDanhGia IN (N'Thử việc', N'Định kỳ', N'Đột xuất', N'Cuối năm')),
    DiemKPI DECIMAL(5,2) CHECK (DiemKPI IS NULL OR (DiemKPI >= 0 AND DiemKPI <= 100)),
    DiemThaiDo DECIMAL(4,2) CHECK (DiemThaiDo IS NULL OR (DiemThaiDo >= 0 AND DiemThaiDo <= 10)),
    DiemKyNang DECIMAL(4,2) CHECK (DiemKyNang IS NULL OR (DiemKyNang >= 0 AND DiemKyNang <= 10)),
    DiemChuyenMon DECIMAL(4,2) CHECK (DiemChuyenMon IS NULL OR (DiemChuyenMon >= 0 AND DiemChuyenMon <= 10)),
    DiemTrungBinh DECIMAL(4,2) CHECK (DiemTrungBinh IS NULL OR (DiemTrungBinh >= 0 AND DiemTrungBinh <= 10)),
    XepLoai NVARCHAR(10) CHECK (XepLoai IN (N'A', N'B', N'C', N'D')),
    DiemManh NVARCHAR(MAX),
    DiemCanCaiThien NVARCHAR(MAX),
    MucTieuTiepTheo NVARCHAR(MAX),
    NhanXet NVARCHAR(MAX),
    DeXuatKhenThuong DECIMAL(18,0),
    DeXuatTangLuong DECIMAL(18,0),
    TrangThai NVARCHAR(20) DEFAULT N'Đã đánh giá' CHECK (TrangThai IN (N'Đã đánh giá', N'Đã phê duyệt', N'Đã phản hồi')),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_DanhGia_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_DanhGia_NguoiDanhGia FOREIGN KEY (NguoiDanhGia) REFERENCES NhanVien(MaNV)
);
PRINT N'✓ Đã tạo bảng DanhGia';

-- Bảng 11: BangLuong (Bảng lương chi tiết)
CREATE TABLE BangLuong (
    MaBL INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    ThangNam DATE NOT NULL,
    LuongCoBan DECIMAL(18,0) NOT NULL,
    PhuCap DECIMAL(18,0) DEFAULT 0,
    ThuongHieuSuat DECIMAL(18,0) DEFAULT 0,
    ThuongKhac DECIMAL(18,0) DEFAULT 0,
    TienTangCa DECIMAL(18,0) DEFAULT 0,
    TongThuNhap AS (LuongCoBan + PhuCap + ThuongHieuSuat + ThuongKhac + TienTangCa) PERSISTED,
    BHXH DECIMAL(18,0) DEFAULT 0,
    BHYT DECIMAL(18,0) DEFAULT 0,
    BHTN DECIMAL(18,0) DEFAULT 0,
    TongBaoHiem AS (BHXH + BHYT + BHTN) PERSISTED,
    ThueTNCN DECIMAL(18,0) DEFAULT 0,
    CacKhoanKhauTruKhac DECIMAL(18,0) DEFAULT 0,
    TongKhauTru AS (BHXH + BHYT + BHTN + ThueTNCN + CacKhoanKhauTruKhac) PERSISTED,
    LuongThucNhan AS (LuongCoBan + PhuCap + ThuongHieuSuat + ThuongKhac + TienTangCa - 
                       (BHXH + BHYT + BHTN + ThueTNCN + CacKhoanKhauTruKhac)) PERSISTED,
    SoGioTangCa DECIMAL(6,2) DEFAULT 0,
    SoNgayLamViec INT DEFAULT 0,
    SoNgayNghiCoPhep INT DEFAULT 0,
    SoNgayNghiKhongPhep INT DEFAULT 0,
    TrangThai NVARCHAR(20) DEFAULT N'Chưa thanh toán' 
        CHECK (TrangThai IN (N'Chưa thanh toán', N'Đã thanh toán', N'Tạm ứng')),
    NgayThanhToan DATE,
    PhuongThucThanhToan NVARCHAR(50) CHECK (PhuongThucThanhToan IS NULL OR PhuongThucThanhToan IN (N'Chuyển khoản', N'Tiền mặt', N'Ví điện tử')),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_BangLuong_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT UQ_BangLuong_NhanVien_ThangNam UNIQUE (MaNV, ThangNam)
);
PRINT N'✓ Đã tạo bảng BangLuong';

-- Bảng 12: KhenThuong (Khen thưởng)
CREATE TABLE KhenThuong (
    MaKT INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    HinhThuc NVARCHAR(100) NOT NULL,
    LyDo NVARCHAR(500) NOT NULL,
    NgayKhenThuong DATE NOT NULL,
    NguoiKy NVARCHAR(100),
    GiaTriTien DECIMAL(18,0) DEFAULT 0,
    FileDinhKem NVARCHAR(255),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_KhenThuong_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
PRINT N'✓ Đã tạo bảng KhenThuong';

-- Bảng 13: KyLuat (Kỷ luật)
CREATE TABLE KyLuat (
    MaKL INT IDENTITY(1,1) PRIMARY KEY,
    MaNV INT NOT NULL,
    HinhThuc NVARCHAR(100) NOT NULL CHECK (HinhThuc IN (N'Nhắc nhở', N'Cảnh cáo', N'Phạt tiền', N'Buộc thôi việc')),
    LyDo NVARCHAR(500) NOT NULL,
    NgayKyLuat DATE NOT NULL,
    NguoiKy NVARCHAR(100),
    MucPhat DECIMAL(18,0) DEFAULT 0,
    ThoiGianHieuLuc INT,
    FileDinhKem NVARCHAR(255),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_KyLuat_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
PRINT N'✓ Đã tạo bảng KyLuat';

-- Bảng 14: ThongBao (Thông báo nội bộ)
CREATE TABLE ThongBao (
    MaTB INT IDENTITY(1,1) PRIMARY KEY,
    TieuDe NVARCHAR(255) NOT NULL,
    NoiDung NVARCHAR(MAX) NOT NULL,
    LoaiThongBao NVARCHAR(50) CHECK (LoaiThongBao IN (N'Công ty', N'Cá nhân', N'Phòng ban', N'Khẩn cấp')),
    DoUuTien NVARCHAR(20) CHECK (DoUuTien IN (N'Thấp', N'Trung bình', N'Cao', N'Khẩn cấp')),
    NgayDang DATETIME DEFAULT GETDATE(),
    NgayHetHan DATE,
    MaPB INT,
    MaNV INT,
    NguoiGui INT NOT NULL,
    FileDinhKem NVARCHAR(255),
    TrangThai BIT DEFAULT 1,
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_ThongBao_PhongBan FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB),
    CONSTRAINT FK_ThongBao_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_ThongBao_NguoiGui FOREIGN KEY (NguoiGui) REFERENCES NhanVien(MaNV)
);
PRINT N'✓ Đã tạo bảng ThongBao';

-- Bảng 15: NguoiDung (Tài khoản đăng nhập)
CREATE TABLE NguoiDung (
    TenDangNhap NVARCHAR(50) PRIMARY KEY,
    MatKhau NVARCHAR(255) NOT NULL,
    VaiTro NVARCHAR(20) NOT NULL CHECK (VaiTro IN (N'Admin', N'QL', N'NV', N'QL_HR', N'NV_HR', N'TaiChinh', N'LanhDao', N'UngVien')),
    MaNV INT,
    TrangThai BIT DEFAULT 1,
    LanDangNhapCuoi DATETIME,
    SoLanDangNhapThatBai INT DEFAULT 0,
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_NguoiDung_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
PRINT N'✓ Đã tạo bảng NguoiDung';

-- Bảng 16: LichSuThayDoi (Audit log)
CREATE TABLE LichSuThayDoi (
    MaLS INT IDENTITY(1,1) PRIMARY KEY,
    TenBang NVARCHAR(100) NOT NULL,
    MaBanGhi INT NOT NULL,
    HanhDong NVARCHAR(20) CHECK (HanhDong IN (N'INSERT', N'UPDATE', N'DELETE')),
    DuLieuCu NVARCHAR(MAX),
    DuLieuMoi NVARCHAR(MAX),
    NguoiThucHien NVARCHAR(50),
    ThoiGian DATETIME DEFAULT GETDATE()
);
PRINT N'✓ Đã tạo bảng LichSuThayDoi';

GO

PRINT N'✓ Hoàn thành tạo 16 bảng cơ sở dữ liệu';
GO

-- =============================================
-- PHẦN 2: TẠO INDEX ĐỂ TỐI ƯU HIỆU SUẤT
-- =============================================

PRINT N'';
PRINT N'================================================';
PRINT N'  ĐANG TẠO CÁC INDEX';
PRINT N'================================================';
GO

CREATE NONCLUSTERED INDEX IX_NhanVien_MaPB ON NhanVien(MaPB);
CREATE NONCLUSTERED INDEX IX_NhanVien_MaCV ON NhanVien(MaCV);
CREATE NONCLUSTERED INDEX IX_NhanVien_TrangThai ON NhanVien(TrangThai);
CREATE NONCLUSTERED INDEX IX_NhanVien_HoTen ON NhanVien(HoTen);
CREATE NONCLUSTERED INDEX IX_NhanVien_Email ON NhanVien(Email);

CREATE NONCLUSTERED INDEX IX_ChamCong_MaNV ON ChamCong(MaNV);
CREATE NONCLUSTERED INDEX IX_ChamCong_Ngay ON ChamCong(Ngay);
CREATE NONCLUSTERED INDEX IX_ChamCong_MaNV_Ngay ON ChamCong(MaNV, Ngay);

CREATE NONCLUSTERED INDEX IX_NghiPhep_MaNV ON NghiPhep(MaNV);
CREATE NONCLUSTERED INDEX IX_NghiPhep_TrangThai ON NghiPhep(TrangThai);
CREATE NONCLUSTERED INDEX IX_NghiPhep_NgayBatDau ON NghiPhep(NgayBatDau);

CREATE NONCLUSTERED INDEX IX_DanhGia_MaNV ON DanhGia(MaNV);
CREATE NONCLUSTERED INDEX IX_DanhGia_KyDanhGia ON DanhGia(KyDanhGia);
CREATE NONCLUSTERED INDEX IX_DanhGia_NguoiDanhGia ON DanhGia(NguoiDanhGia);

CREATE NONCLUSTERED INDEX IX_BangLuong_MaNV ON BangLuong(MaNV);
CREATE NONCLUSTERED INDEX IX_BangLuong_ThangNam ON BangLuong(ThangNam);
CREATE NONCLUSTERED INDEX IX_BangLuong_TrangThai ON BangLuong(TrangThai);

CREATE NONCLUSTERED INDEX IX_DaoTao_NgayBatDau ON DaoTao(NgayBatDau);
CREATE NONCLUSTERED INDEX IX_DaoTao_TrangThai ON DaoTao(TrangThai);

CREATE NONCLUSTERED INDEX IX_ThamGiaDaoTao_MaNV ON ThamGiaDaoTao(MaNV);
CREATE NONCLUSTERED INDEX IX_ThamGiaDaoTao_MaDT ON ThamGiaDaoTao(MaDT);

CREATE NONCLUSTERED INDEX IX_UngVien_TrangThai ON UngVien(TrangThai);
CREATE NONCLUSTERED INDEX IX_UngVien_MaCV ON UngVien(MaCV);
CREATE NONCLUSTERED INDEX IX_UngVien_NgayUngTuyen ON UngVien(NgayUngTuyen);

CREATE NONCLUSTERED INDEX IX_ThongBao_NgayDang ON ThongBao(NgayDang);
CREATE NONCLUSTERED INDEX IX_ThongBao_MaPB ON ThongBao(MaPB);
CREATE NONCLUSTERED INDEX IX_ThongBao_MaNV ON ThongBao(MaNV);

PRINT N'✓ Đã tạo 30+ indexes';
GO

-- =============================================
-- PHẦN 3: TẠO CÁC VIEW
-- =============================================

PRINT N'';
PRINT N'================================================';
PRINT N'  ĐANG TẠO CÁC VIEW';
PRINT N'================================================';
GO

-- View 1: Thông tin nhân viên cơ bản
CREATE VIEW vw_ThongTinNhanVien AS
SELECT 
    nv.MaNV,
    nv.HoTen,
    nv.NgaySinh,
    DATEDIFF(YEAR, nv.NgaySinh, GETDATE()) AS Tuoi,
    nv.GioiTinh,
    nv.DienThoai,
    nv.Email,
    pb.TenPB AS PhongBan,
    cv.TenCV AS ChucVu,
    cv.CapBac,
    nv.NgayVaoLam,
    DATEDIFF(MONTH, nv.NgayVaoLam, GETDATE()) AS ThangLamViec,
    nv.LuongCoBan,
    nv.PhuCap,
    nv.TrangThai
FROM NhanVien nv
INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
INNER JOIN ChucVu cv ON nv.MaCV = cv.MaCV;
GO
PRINT N'✓ Đã tạo vw_ThongTinNhanVien';

-- View 2: Chấm công tháng hiện tại
CREATE VIEW vw_ChamCongThangHienTai AS
SELECT 
    nv.MaNV,
    nv.HoTen,
    pb.TenPB,
    COUNT(CASE WHEN cc.TrangThai = N'Đi làm' THEN 1 END) AS SoNgayLamViec,
    COUNT(CASE WHEN cc.TrangThai IN (N'Nghỉ có phép', N'Phép năm', N'Nghỉ việc riêng') THEN 1 END) AS SoNgayNghiCoPhep,
    COUNT(CASE WHEN cc.TrangThai = N'Nghỉ không phép' THEN 1 END) AS SoNgayNghiKhongPhep,
    SUM(ISNULL(cc.TangCa, 0)) AS TongGioTangCa,
    SUM(CASE WHEN cc.DiMuon > 0 THEN 1 ELSE 0 END) AS SoLanDiMuon,
    SUM(ISNULL(cc.DiMuon, 0)) AS TongPhutDiMuon
FROM NhanVien nv
INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
LEFT JOIN ChamCong cc ON nv.MaNV = cc.MaNV 
    AND MONTH(cc.Ngay) = MONTH(GETDATE()) 
    AND YEAR(cc.Ngay) = YEAR(GETDATE())
WHERE nv.TrangThai = N'Đang làm việc'
GROUP BY nv.MaNV, nv.HoTen, pb.TenPB;
GO
PRINT N'✓ Đã tạo vw_ChamCongThangHienTai';

-- View 3: Đơn nghỉ phép chờ duyệt
CREATE VIEW vw_NghiPhepChoDuyet AS
SELECT 
    np.MaNP,
    nv.MaNV,
    nv.HoTen AS NhanVien,
    pb.TenPB AS PhongBan,
    np.LoaiNghi,
    np.NgayBatDau,
    np.NgayKetThuc,
    np.SoNgayNghi,
    np.LyDo,
    np.NgayNopDon,
    DATEDIFF(DAY, np.NgayNopDon, GETDATE()) AS SoNgayChoDuyet,
    nd.HoTen AS NguoiDuyet
FROM NghiPhep np
INNER JOIN NhanVien nv ON np.MaNV = nv.MaNV
INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
LEFT JOIN NhanVien nd ON np.NguoiDuyet = nd.MaNV
WHERE np.TrangThai = N'Chờ duyệt';
GO
PRINT N'✓ Đã tạo vw_NghiPhepChoDuyet';

-- View 4: Kết quả đánh giá gần nhất
CREATE VIEW vw_DanhGiaGanNhat AS
SELECT 
    nv.MaNV,
    nv.HoTen,
    pb.TenPB,
    cv.TenCV,
    dg.KyDanhGia,
    dg.NgayDanhGia,
    dg.XepLoai,
    dg.DiemTrungBinh,
    nd.HoTen AS NguoiDanhGia,
    dg.DeXuatTangLuong,
    dg.DeXuatKhenThuong
FROM NhanVien nv
INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
INNER JOIN ChucVu cv ON nv.MaCV = cv.MaCV
INNER JOIN DanhGia dg ON nv.MaNV = dg.MaNV
INNER JOIN NhanVien nd ON dg.NguoiDanhGia = nd.MaNV
WHERE dg.NgayDanhGia = (
    SELECT MAX(NgayDanhGia) 
    FROM DanhGia 
    WHERE MaNV = nv.MaNV
);
GO
PRINT N'✓ Đã tạo vw_DanhGiaGanNhat';

-- View 5: Bảng lương tháng hiện tại
CREATE VIEW vw_BangLuongThangHienTai AS
SELECT 
    nv.MaNV,
    nv.HoTen,
    pb.TenPB,
    bl.ThangNam,
    bl.LuongCoBan,
    bl.PhuCap,
    bl.TienTangCa,
    bl.TongThuNhap,
    bl.TongKhauTru,
    bl.LuongThucNhan,
    bl.TrangThai
FROM BangLuong bl
INNER JOIN NhanVien nv ON bl.MaNV = nv.MaNV
INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
WHERE MONTH(bl.ThangNam) = MONTH(GETDATE()) 
    AND YEAR(bl.ThangNam) = YEAR(GETDATE());
GO
PRINT N'✓ Đã tạo vw_BangLuongThangHienTai';

-- View 6: Ứng viên đang trong quy trình tuyển dụng
CREATE VIEW vw_UngVienDangXuLy AS
SELECT 
    uv.MaUV,
    uv.HoTen,
    cv.TenCV AS ViTriUngTuyen,
    uv.DienThoai,
    uv.Email,
    uv.TrangThai,
    uv.NgayUngTuyen,
    uv.NgayPhongVan,
    DATEDIFF(DAY, uv.NgayUngTuyen, GETDATE()) AS SoNgayXuLy,
    uv.DiemPhongVan,
    uv.NguoiPhongVan
FROM UngVien uv
LEFT JOIN ChucVu cv ON uv.MaCV = cv.MaCV
WHERE uv.TrangThai NOT IN (N'Trúng tuyển', N'Đã loại', N'Từ chối offer');
GO
PRINT N'✓ Đã tạo vw_UngVienDangXuLy';

-- View 7: Khóa đào tạo và số lượng học viên
CREATE VIEW vw_DaoTaoVaHocVien AS
SELECT 
    dt.MaDT,
    dt.TenKhoaHoc,
    dt.LoaiDaoTao,
    dt.NgayBatDau,
    dt.NgayKetThuc,
    dt.ThoiLuong,
    dt.GiangVien,
    dt.TrangThai,
    COUNT(tgdt.MaTGDT) AS SoLuongHocVien,
    dt.SoLuongToiDa,
    dt.SoLuongToiDa - COUNT(tgdt.MaTGDT) AS SoChoConLai
FROM DaoTao dt
LEFT JOIN ThamGiaDaoTao tgdt ON dt.MaDT = tgdt.MaDT
GROUP BY dt.MaDT, dt.TenKhoaHoc, dt.LoaiDaoTao, dt.NgayBatDau, dt.NgayKetThuc, 
         dt.ThoiLuong, dt.GiangVien, dt.TrangThai, dt.SoLuongToiDa;
GO
PRINT N'✓ Đã tạo vw_DaoTaoVaHocVien';

-- View 8: Hợp đồng sắp hết hạn (trong 3 tháng tới)
CREATE VIEW vw_HopDongSapHetHan AS
SELECT 
    hd.MaHD,
    nv.MaNV,
    nv.HoTen,
    pb.TenPB,
    hd.SoHopDong,
    hd.LoaiHopDong,
    hd.NgayHetHan,
    DATEDIFF(DAY, GETDATE(), hd.NgayHetHan) AS SoNgayConLai,
    hd.LuongHopDong
FROM HopDong hd
INNER JOIN NhanVien nv ON hd.MaNV = nv.MaNV
INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
WHERE hd.TrangThai = N'Có hiệu lực'
    AND hd.NgayHetHan IS NOT NULL
    AND hd.NgayHetHan BETWEEN GETDATE() AND DATEADD(MONTH, 3, GETDATE());
GO
PRINT N'✓ Đã tạo vw_HopDongSapHetHan';

-- View 9: Thống kê nhân sự theo phòng ban
CREATE VIEW vw_ThongKeNhanSuPhongBan AS
SELECT 
    pb.MaPB,
    pb.TenPB,
    COUNT(nv.MaNV) AS TongNhanVien,
    COUNT(CASE WHEN nv.GioiTinh = N'Nam' THEN 1 END) AS SoNam,
    COUNT(CASE WHEN nv.GioiTinh = N'Nữ' THEN 1 END) AS SoNu,
    AVG(DATEDIFF(YEAR, nv.NgaySinh, GETDATE())) AS TuoiTrungBinh,
    AVG(nv.LuongCoBan) AS LuongTrungBinh,
    COUNT(CASE WHEN cv.CapBac <= 2 THEN 1 END) AS SoQuanLy,
    COUNT(CASE WHEN cv.CapBac > 2 THEN 1 END) AS SoNhanVienThucHien
FROM PhongBan pb
LEFT JOIN NhanVien nv ON pb.MaPB = nv.MaPB AND nv.TrangThai = N'Đang làm việc'
LEFT JOIN ChucVu cv ON nv.MaCV = cv.MaCV
GROUP BY pb.MaPB, pb.TenPB;
GO
PRINT N'✓ Đã tạo vw_ThongKeNhanSuPhongBan';

-- View 10: Lịch sử khen thưởng và kỷ luật
CREATE VIEW vw_LichSuKhenThuongKyLuat AS
SELECT 
    nv.MaNV,
    nv.HoTen,
    pb.TenPB,
    N'Khen thưởng' AS LoaiHanhDong,
    kt.HinhThuc,
    kt.LyDo,
    kt.NgayKhenThuong AS NgayThucHien,
    kt.GiaTriTien,
    NULL AS MucPhat
FROM NhanVien nv
INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
INNER JOIN KhenThuong kt ON nv.MaNV = kt.MaNV

UNION ALL

SELECT 
    nv.MaNV,
    nv.HoTen,
    pb.TenPB,
    N'Kỷ luật' AS LoaiHanhDong,
    kl.HinhThuc,
    kl.LyDo,
    kl.NgayKyLuat AS NgayThucHien,
    NULL AS GiaTriTien,
    kl.MucPhat
FROM NhanVien nv
INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
INNER JOIN KyLuat kl ON nv.MaNV = kl.MaNV;
GO
PRINT N'✓ Đã tạo vw_LichSuKhenThuongKyLuat';

PRINT N'✓ Hoàn thành tạo 10 views';
GO

-- =============================================
-- PHẦN 4: INSERT DỮ LIỆU MẪU
-- =============================================

PRINT N'';
PRINT N'================================================';
PRINT N'  ĐANG INSERT DỮ LIỆU MẪU';
PRINT N'================================================';
GO

-- Insert PhongBan
SET IDENTITY_INSERT PhongBan ON;
INSERT INTO PhongBan (MaPB, TenPB, MoTa, NgayThanhLap, TrangThai, NguoiTao) VALUES
(1, N'Ban Giám đốc', N'Điều hành toàn bộ hoạt động công ty', '2000-01-01', 1, N'System'),
(2, N'Phòng Nhân sự', N'Quản lý nhân sự và tuyển dụng', '2000-01-01', 1, N'System'),
(3, N'Phòng Kinh doanh', N'Phát triển kinh doanh và bán hàng', '2000-01-01', 1, N'System'),
(4, N'Phòng Kỹ thuật', N'Nghiên cứu và phát triển sản phẩm', '2000-01-01', 1, N'System'),
(5, N'Phòng Tài chính', N'Kế toán và tài chính công ty', '2000-01-01', 1, N'System'),
(6, N'Phòng Marketing', N'Marketing và truyền thông', '2005-01-01', 1, N'System'),
(7, N'Phòng IT', N'Công nghệ thông tin', '2010-01-01', 1, N'System');
SET IDENTITY_INSERT PhongBan OFF;
PRINT N'✓ Đã insert PhongBan';

-- Insert ChucVu
SET IDENTITY_INSERT ChucVu ON;
INSERT INTO ChucVu (MaCV, TenCV, MoTa, CapBac, MucLuongToiThieu, MucLuongToiDa, TrangThai, NguoiTao) VALUES
(1, N'Giám đốc', N'Giám đốc điều hành', 1, 50000000, 100000000, 1, N'System'),
(2, N'Trưởng phòng', N'Trưởng phòng ban', 2, 25000000, 50000000, 1, N'System'),
(3, N'Phó phòng', N'Phó phòng ban', 3, 20000000, 35000000, 1, N'System'),
(4, N'Nhân viên chính thức', N'Nhân viên làm việc chính thức', 4, 10000000, 25000000, 1, N'System'),
(5, N'Nhân viên thử việc', N'Nhân viên đang trong thời gian thử việc', 5, 8000000, 15000000, 1, N'System'),
(6, N'Thực tập sinh', N'Sinh viên thực tập', 6, 4000000, 7000000, 1, N'System');
SET IDENTITY_INSERT ChucVu OFF;
PRINT N'✓ Đã insert ChucVu';

-- Insert 55 NhanVien (V4 UPDATE - Mở rộng từ 30 lên 55)
SET IDENTITY_INSERT NhanVien ON;
INSERT INTO NhanVien (MaNV, HoTen, NgaySinh, GioiTinh, CMND, DienThoai, Email, DiaChiThuongTru, MaPB, MaCV, NgayVaoLam, LuongCoBan, PhuCap, TrangThai, NguoiTao) VALUES
-- Ban lãnh đạo (5 người)
(1, N'Nguyễn Văn An', '1975-05-20', N'Nam', '001075012345', '0901234567', 'an.nguyen@unilever.com', N'123 Nguyễn Huệ, Q1, TP.HCM', 1, 1, '2000-01-15', 80000000, 10000000, N'Đang làm việc', N'System'),
(2, N'Trần Thị Bích', '1980-07-10', N'Nữ', '001080023456', '0912345678', 'bich.tran@unilever.com', N'456 Lê Lợi, Q1, TP.HCM', 2, 2, '2010-03-01', 35000000, 5000000, N'Đang làm việc', N'System'),
(3, N'Lê Minh Châu', '1982-03-15', N'Nữ', '001082034567', '0923456789', 'chau.le@unilever.com', N'789 Điện Biên Phủ, Q3, TP.HCM', 3, 2, '2012-06-15', 32000000, 5000000, N'Đang làm việc', N'System'),
(4, N'Phạm Quốc Dũng', '1985-11-20', N'Nam', '001085045678', '0934567890', 'dung.pham@unilever.com', N'321 Võ Văn Tần, Q3, TP.HCM', 4, 2, '2015-08-01', 30000000, 4000000, N'Đang làm việc', N'System'),
(5, N'Hoàng Thị Hà', '1983-09-05', N'Nữ', '001083056789', '0945678901', 'ha.hoang@unilever.com', N'654 Pasteur, Q1, TP.HCM', 5, 2, '2014-02-15', 33000000, 5000000, N'Đang làm việc', N'System'),
-- Phòng Nhân sự (10 người: 6-15)
(6, N'Võ Thị Mai', '1990-01-05', N'Nữ', '001090067890', '0956789012', 'mai.vo@unilever.com', N'111 Hai Bà Trưng, Q1, TP.HCM', 2, 3, '2016-08-01', 22000000, 3000000, N'Đang làm việc', N'System'),
(7, N'Đỗ Văn Hùng', '1992-04-12', N'Nam', '001092078901', '0967890123', 'hung.do@unilever.com', N'222 Lý Tự Trọng, Q1, TP.HCM', 2, 4, '2018-05-10', 15000000, 2000000, N'Đang làm việc', N'System'),
(8, N'Ngô Thị Lan', '1993-07-18', N'Nữ', '001093089012', '0978901234', 'lan.ngo@unilever.com', N'333 Trần Hưng Đạo, Q5, TP.HCM', 2, 4, '2019-03-20', 14000000, 2000000, N'Đang làm việc', N'System'),
(9, N'Trịnh Văn Thành', '1994-10-25', N'Nam', '001094090123', '0989012345', 'thanh.trinh@unilever.com', N'444 Nguyễn Thị Minh Khai, Q3, TP.HCM', 2, 4, '2020-01-15', 13000000, 1500000, N'Đang làm việc', N'System'),
(10, N'Bùi Thị Nga', '1995-12-30', N'Nữ', '001095001234', '0990123456', 'nga.bui@unilever.com', N'555 Cách Mạng Tháng 8, Q10, TP.HCM', 2, 4, '2021-06-01', 12000000, 1500000, N'Đang làm việc', N'System'),
(11, N'Phan Văn Long', '1996-02-14', N'Nam', '001096012345', '0901234568', 'long.phan@unilever.com', N'666 Lê Văn Sỹ, Q3, TP.HCM', 2, 4, '2022-03-10', 11000000, 1500000, N'Đang làm việc', N'System'),
(12, N'Lý Thị Hương', '1997-05-20', N'Nữ', '001097023456', '0912345679', 'huong.ly@unilever.com', N'777 Hoàng Văn Thụ, PN, TP.HCM', 2, 4, '2023-01-20', 10000000, 1000000, N'Đang làm việc', N'System'),
(13, N'Vương Văn Minh', '1998-08-08', N'Nam', '001098034567', '0923456780', 'minh.vuong@unilever.com', N'888 Phan Đăng Lưu, PN, TP.HCM', 2, 5, '2024-01-05', 6000000, 500000, N'Đang làm việc', N'System'),
(14, N'Dương Thị Phương', '1999-11-11', N'Nữ', '001099045678', '0934567891', 'phuong.duong@unilever.com', N'999 Ba Tháng Hai, Q10, TP.HCM', 2, 5, '2024-05-10', 5500000, 500000, N'Đang làm việc', N'System'),
(15, N'Tô Văn Quang', '2000-03-15', N'Nam', '001000056789', '0945678902', 'quang.to@unilever.com', N'100 Võ Thị Sáu, Q3, TP.HCM', 2, 5, '2024-09-01', 5000000, 500000, N'Đang làm việc', N'System'),
-- Phòng Kinh doanh (15 người: 16-30)
(16, N'Nguyễn Thị Tuyết', '1988-06-10', N'Nữ', '001088067890', '0956789013', 'tuyet.nguyen@unilever.com', N'201 Đinh Tiên Hoàng, Q1, TP.HCM', 3, 3, '2017-04-01', 25000000, 4000000, N'Đang làm việc', N'System'),
(17, N'Trần Văn Tài', '1989-09-20', N'Nam', '001089078901', '0967890124', 'tai.tran@unilever.com', N'202 Lê Duẩn, Q1, TP.HCM', 3, 4, '2018-07-15', 16000000, 2500000, N'Đang làm việc', N'System'),
(18, N'Lê Thị Uyên', '1990-12-05', N'Nữ', '001090089012', '0978901235', 'uyen.le@unilever.com', N'203 Nguyễn Đình Chiểu, Q3, TP.HCM', 3, 4, '2019-02-20', 15500000, 2500000, N'Đang làm việc', N'System'),
(19, N'Phạm Văn Vũ', '1991-03-18', N'Nam', '001091090123', '0989012346', 'vu.pham@unilever.com', N'204 Trần Quốc Toản, Q3, TP.HCM', 3, 4, '2019-10-10', 15000000, 2000000, N'Đang làm việc', N'System'),
(20, N'Hoàng Thị Xuân', '1992-06-25', N'Nữ', '001092001235', '0990123457', 'xuan.hoang@unilever.com', N'205 Nguyễn Trãi, Q1, TP.HCM', 3, 4, '2020-05-15', 14500000, 2000000, N'Đang làm việc', N'System'),
(21, N'Đỗ Văn Yên', '1993-09-30', N'Nam', '001093012346', '0901234569', 'yen.do@unilever.com', N'206 Hoàng Hoa Thám, TB, TP.HCM', 3, 4, '2021-01-20', 14000000, 2000000, N'Đang làm việc', N'System'),
(22, N'Ngô Thị An', '1994-12-12', N'Nữ', '001094023457', '0912345680', 'an.ngo@unilever.com', N'207 Lý Thường Kiệt, Q11, TP.HCM', 3, 4, '2021-08-01', 13500000, 1800000, N'Đang làm việc', N'System'),
(23, N'Trịnh Văn Bình', '1995-04-20', N'Nam', '001095034568', '0923456781', 'binh.trinh@unilever.com', N'208 Nguyễn Kiệm, PN, TP.HCM', 3, 4, '2022-02-15', 13000000, 1800000, N'Đang làm việc', N'System'),
(24, N'Bùi Thị Cẩm', '1996-07-08', N'Nữ', '001096045679', '0934567892', 'cam.bui@unilever.com', N'209 Phạm Văn Đồng, GV, TP.HCM', 3, 4, '2022-09-01', 12500000, 1500000, N'Đang làm việc', N'System'),
(25, N'Phan Văn Đức', '1997-10-15', N'Nam', '001097056790', '0945678903', 'duc.phan@unilever.com', N'210 Quang Trung, GV, TP.HCM', 3, 4, '2023-03-10', 12000000, 1500000, N'Đang làm việc', N'System'),
(26, N'Lý Thị Em', '1998-01-22', N'Nữ', '001098067901', '0956789014', 'em.ly@unilever.com', N'211 Phan Văn Trị, GV, TP.HCM', 3, 4, '2023-07-20', 11500000, 1500000, N'Đang làm việc', N'System'),
(27, N'Vương Văn Phát', '1999-04-28', N'Nam', '001099078012', '0967890125', 'phat.vuong@unilever.com', N'212 Lê Quang Định, BT, TP.HCM', 3, 5, '2024-02-01', 6500000, 800000, N'Đang làm việc', N'System'),
(28, N'Dương Thị Giang', '2000-07-05', N'Nữ', '001000089123', '0978901236', 'giang.duong@unilever.com', N'213 Xô Viết Nghệ Tĩnh, BT, TP.HCM', 3, 5, '2024-06-15', 6000000, 800000, N'Đang làm việc', N'System'),
(29, N'Tô Văn Hải', '2001-10-10', N'Nam', '001001090234', '0989012347', 'hai.to@unilever.com', N'214 Đinh Bộ Lĩnh, BT, TP.HCM', 3, 5, '2024-10-01', 5500000, 500000, N'Đang làm việc', N'System'),
(30, N'Nguyễn Thị Ivy', '2002-01-18', N'Nữ', '001002001345', '0990123458', 'ivy.nguyen@unilever.com', N'215 Nguyễn Oanh, GV, TP.HCM', 3, 5, '2024-11-01', 5000000, 500000, N'Đang làm việc', N'System'),
-- Phòng Kỹ thuật (15 người: 31-45)
(31, N'Trần Văn Khải', '1987-05-15', N'Nam', '001087012456', '0901234570', 'khai.tran@unilever.com', N'301 Lê Thánh Tôn, Q1, TP.HCM', 4, 3, '2016-09-01', 28000000, 4000000, N'Đang làm việc', N'System'),
(32, N'Lê Thị Linh', '1988-08-22', N'Nữ', '001088023567', '0912345681', 'linh.le@unilever.com', N'302 Mạc Đĩnh Chi, Q1, TP.HCM', 4, 4, '2017-11-15', 18000000, 3000000, N'Đang làm việc', N'System'),
(33, N'Phạm Văn Minh', '1989-11-30', N'Nam', '001089034678', '0923456782', 'minh.pham@unilever.com', N'303 Nguyễn Bỉnh Khiêm, Q1, TP.HCM', 4, 4, '2018-04-10', 17500000, 2800000, N'Đang làm việc', N'System'),
(34, N'Hoàng Thị Nga', '1990-03-08', N'Nữ', '001090045789', '0934567893', 'nga.hoang@unilever.com', N'304 Hai Bà Trưng, Q3, TP.HCM', 4, 4, '2019-01-20', 17000000, 2500000, N'Đang làm việc', N'System'),
(35, N'Đỗ Văn Oai', '1991-06-14', N'Nam', '001091056890', '0945678904', 'oai.do@unilever.com', N'305 Yersin, Q1, TP.HCM', 4, 4, '2019-08-05', 16500000, 2500000, N'Đang làm việc', N'System'),
(36, N'Ngô Thị Phượng', '1992-09-20', N'Nữ', '001092067901', '0956789015', 'phuong.ngo@unilever.com', N'306 Nam Kỳ Khởi Nghĩa, Q1, TP.HCM', 4, 4, '2020-03-15', 16000000, 2200000, N'Đang làm việc', N'System'),
(37, N'Trịnh Văn Quân', '1993-12-25', N'Nam', '001093078012', '0967890126', 'quan.trinh@unilever.com', N'307 Trần Hưng Đạo, Q1, TP.HCM', 4, 4, '2020-10-01', 15500000, 2200000, N'Đang làm việc', N'System'),
(38, N'Bùi Thị Rạng', '1994-04-02', N'Nữ', '001094089123', '0978901237', 'rang.bui@unilever.com', N'308 Lê Lai, Q1, TP.HCM', 4, 4, '2021-05-10', 15000000, 2000000, N'Đang làm việc', N'System'),
(39, N'Phan Văn Sang', '1995-07-10', N'Nam', '001095090234', '0989012348', 'sang.phan@unilever.com', N'309 Đề Thám, Q1, TP.HCM', 4, 4, '2022-01-15', 14500000, 2000000, N'Đang làm việc', N'System'),
(40, N'Lý Thị Thu', '1996-10-18', N'Nữ', '001096001346', '0990123459', 'thu.ly@unilever.com', N'310 Phạm Ngũ Lão, Q1, TP.HCM', 4, 4, '2022-08-01', 14000000, 1800000, N'Đang làm việc', N'System'),
(41, N'Vương Văn Uyên', '1997-01-25', N'Nam', '001097012457', '0901234571', 'uyen.vuong@unilever.com', N'311 Bùi Viện, Q1, TP.HCM', 4, 4, '2023-02-20', 13500000, 1800000, N'Đang làm việc', N'System'),
(42, N'Dương Thị Vân', '1998-04-30', N'Nữ', '001098023568', '0912345682', 'van.duong@unilever.com', N'312 Trần Nhật Duật, Q1, TP.HCM', 4, 4, '2023-09-01', 13000000, 1500000, N'Đang làm việc', N'System'),
(43, N'Tô Văn Xuân', '1999-08-05', N'Nam', '001099034679', '0923456783', 'xuan.to@unilever.com', N'313 Đồng Khởi, Q1, TP.HCM', 4, 5, '2024-03-10', 7000000, 1000000, N'Đang làm việc', N'System'),
(44, N'Nguyễn Thị Yến', '2000-11-12', N'Nữ', '001000045790', '0934567894', 'yen.nguyen@unilever.com', N'314 Nguyễn Huệ, Q1, TP.HCM', 4, 5, '2024-07-15', 6500000, 1000000, N'Đang làm việc', N'System'),
(45, N'Trần Văn Anh', '2001-02-20', N'Nam', '001001056801', '0945678905', 'anh.tran2@unilever.com', N'315 Lê Lợi, Q1, TP.HCM', 4, 5, '2024-11-01', 6000000, 800000, N'Đang làm việc', N'System'),
-- Phòng Tài chính (10 người: 46-55)
(46, N'Lê Văn Bảo', '1986-04-10', N'Nam', '001086067912', '0956789016', 'bao.le@unilever.com', N'401 Tôn Đức Thắng, Q1, TP.HCM', 5, 3, '2015-06-01', 26000000, 4000000, N'Đang làm việc', N'System'),
(47, N'Phạm Thị Cúc', '1987-07-18', N'Nữ', '001087078023', '0967890127', 'cuc.pham@unilever.com', N'402 Nguyễn Thị Minh Khai, Q3, TP.HCM', 5, 4, '2016-12-15', 17000000, 2500000, N'Đang làm việc', N'System'),
(48, N'Hoàng Văn Dũng', '1988-10-25', N'Nam', '001088089134', '0978901238', 'dung.hoang@unilever.com', N'403 Điện Biên Phủ, Q3, TP.HCM', 5, 4, '2017-09-01', 16500000, 2500000, N'Đang làm việc', N'System'),
(49, N'Đỗ Thị Em', '1989-02-02', N'Nữ', '001089090245', '0989012349', 'em.do@unilever.com', N'404 Võ Văn Tần, Q3, TP.HCM', 5, 4, '2018-06-10', 16000000, 2200000, N'Đang làm việc', N'System'),
(50, N'Ngô Văn Phúc', '1990-05-10', N'Nam', '001090001356', '0990123460', 'phuc.ngo@unilever.com', N'405 Pasteur, Q1, TP.HCM', 5, 4, '2019-03-20', 15500000, 2000000, N'Đang làm việc', N'System'),
(51, N'Trịnh Thị Giang', '1991-08-18', N'Nữ', '001091012467', '0901234572', 'giang.trinh@unilever.com', N'406 Trường Sơn, TB, TP.HCM', 5, 4, '2020-01-05', 15000000, 2000000, N'Đang làm việc', N'System'),
(52, N'Bùi Văn Hòa', '1992-11-25', N'Nam', '001092023578', '0912345683', 'hoa.bui@unilever.com', N'407 Hoàng Văn Thụ, TB, TP.HCM', 5, 4, '2021-07-15', 14500000, 1800000, N'Đang làm việc', N'System'),
(53, N'Phan Thị Ivy', '1993-03-05', N'Nữ', '001093034689', '0923456784', 'ivy.phan@unilever.com', N'408 Cộng Hòa, TB, TP.HCM', 5, 4, '2022-04-01', 14000000, 1800000, N'Đang làm việc', N'System'),
(54, N'Lý Văn Khôi', '1994-06-12', N'Nam', '001094045790', '0934567895', 'khoi.ly@unilever.com', N'409 Lý Thường Kiệt, TB, TP.HCM', 5, 5, '2023-10-10', 6500000, 800000, N'Đang làm việc', N'System'),
(55, N'Vương Thị Liên', '1995-09-20', N'Nữ', '001095056801', '0945678906', 'lien.vuong@unilever.com', N'410 Ba Tháng Hai, Q10, TP.HCM', 5, 5, '2024-08-01', 6000000, 800000, N'Đang làm việc', N'System');
SET IDENTITY_INSERT NhanVien OFF;
PRINT N'✓ Đã insert 55 nhân viên (V4 UPDATE)';

PRINT N'✓ Đã insert 30 NhanVien';

-- Insert HopDong
SET IDENTITY_INSERT HopDong ON;
INSERT INTO HopDong (MaHD, MaNV, SoHopDong, LoaiHopDong, NgayKy, NgayHieuLuc, NgayHetHan, LuongHopDong, TrangThai, NguoiTao) VALUES
(1, 1, 'HD2000-001', N'Không xác định thời hạn', '2000-01-15', '2000-01-15', NULL, 80000000, N'Có hiệu lực', N'System'),
(2, 2, 'HD2010-002', N'Không xác định thời hạn', '2010-03-01', '2010-03-01', NULL, 28000000, N'Có hiệu lực', N'System'),
(3, 3, 'HD2015-003', N'Xác định thời hạn', '2015-06-15', '2015-06-15', '2026-06-15', 22000000, N'Có hiệu lực', N'System'),
(4, 4, 'HD2016-004', N'Không xác định thời hạn', '2016-08-01', '2016-08-01', NULL, 15000000, N'Có hiệu lực', N'System'),
(5, 5, 'HD2018-005', N'Không xác định thời hạn', '2018-04-20', '2018-04-20', NULL, 14000000, N'Có hiệu lực', N'System'),
(6, 7, 'HD2024-006', N'Thử việc', '2024-07-01', '2024-07-01', '2024-09-01', 5000000, N'Có hiệu lực', N'System'),
(7, 8, 'HD2012-007', N'Không xác định thời hạn', '2012-09-20', '2012-09-20', NULL, 30000000, N'Có hiệu lực', N'System'),
(8, 9, 'HD2014-008', N'Không xác định thời hạn', '2014-02-02', '2014-02-02', NULL, 16000000, N'Có hiệu lực', N'System'),
(9, 10, 'HD2018-009', N'Xác định thời hạn', '2018-08-08', '2018-08-08', '2026-08-08', 14500000, N'Có hiệu lực', N'System'),
(10, 14, 'HD2015-010', N'Không xác định thời hạn', '2015-06-15', '2015-06-15', NULL, 32000000, N'Có hiệu lực', N'System'),
(11, 15, 'HD2015-011', N'Không xác định thời hạn', '2015-07-15', '2015-07-15', NULL, 18000000, N'Có hiệu lực', N'System'),
(12, 18, 'HD2012-012', N'Không xác định thời hạn', '2012-12-01', '2012-12-01', NULL, 24000000, N'Có hiệu lực', N'System'),
(13, 19, 'HD2025-013', N'Thử việc', '2025-01-10', '2025-01-10', '2025-03-10', 6000000, N'Có hiệu lực', N'System'),
(14, 20, 'HD2011-014', N'Không xác định thời hạn', '2011-04-01', '2011-04-01', NULL, 29000000, N'Có hiệu lực', N'System'),
(15, 21, 'HD2017-015', N'Xác định thời hạn', '2017-04-04', '2017-04-04', '2026-04-04', 15500000, N'Có hiệu lực', N'System'),
(16, 26, 'HD2013-016', N'Không xác định thời hạn', '2013-08-10', '2013-08-10', NULL, 27000000, N'Có hiệu lực', N'System'),
(17, 27, 'HD2017-017', N'Xác định thời hạn', '2017-09-15', '2017-09-15', '2025-12-15', 14500000, N'Có hiệu lực', N'System'),
(18, 28, 'HD2016-018', N'Xác định thời hạn', '2016-05-20', '2016-05-20', '2026-01-20', 15000000, N'Có hiệu lực', N'System'),
(19, 29, 'HD2014-019', N'Không xác định thời hạn', '2014-11-01', '2014-11-01', NULL, 31000000, N'Có hiệu lực', N'System'),
(20, 30, 'HD2019-020', N'Xác định thời hạn', '2019-02-14', '2019-02-14', '2026-02-14', 16500000, N'Có hiệu lực', N'System');
SET IDENTITY_INSERT HopDong OFF;
PRINT N'✓ Đã insert HopDong';

-- Insert UngVien
SET IDENTITY_INSERT UngVien ON;
INSERT INTO UngVien (MaUV, HoTen, NgaySinh, GioiTinh, DienThoai, Email, MaCV, NgayUngTuyen, NguonUngTuyen, TrangThai, DiemPhongVan, NguoiTao) VALUES
(1, N'Đào Thị Thảo', '1995-06-15', N'Nữ', '0901111111', 'thao.dao@gmail.com', 4, '2025-10-01', N'Website công ty', N'Mới ứng tuyển', NULL, N'System'),
(2, N'Phạm Văn Cường', '1993-08-20', N'Nam', '0902222222', 'cuong.pham@gmail.com', 4, '2025-09-15', N'LinkedIn', N'Đang phỏng vấn', 7.5, N'System'),
(3, N'Nguyễn Thùy Trang', '1990-03-10', N'Nữ', '0903333333', 'trang.nguyen@gmail.com', 2, '2025-09-20', N'Headhunter', N'Đang phỏng vấn', 8.0, N'System'),
(4, N'Lê Minh Tuấn', '1992-11-25', N'Nam', '0904444444', 'tuan.le@gmail.com', 4, '2025-08-30', N'Giới thiệu', N'Trúng tuyển', 9.0, N'System'),
(5, N'Trần Ngọc Minh', '1994-07-08', N'Nam', '0905555555', 'minh.tran@gmail.com', 6, '2025-08-01', N'Website công ty', N'Đã loại', 5.0, N'System'),
(6, N'Hoàng Thị Lan', '1996-02-14', N'Nữ', '0906666666', 'lan.hoang@gmail.com', 4, '2025-10-15', N'JobStreet', N'Đang xét hồ sơ', NULL, N'System'),
(7, N'Vũ Quốc Anh', '1991-09-30', N'Nam', '0907777777', 'anh.vu@gmail.com', 3, '2025-10-10', N'Giới thiệu', N'Đã liên hệ', NULL, N'System'),
(8, N'Nguyễn Mai Linh', '1997-12-05', N'Nữ', '0908888888', 'linh.nguyen@gmail.com', 4, '2025-10-20', N'LinkedIn', N'Mới ứng tuyển', NULL, N'System'),
(9, N'Trần Văn Hải', '1989-04-18', N'Nam', '0909999999', 'hai.tran@gmail.com', 2, '2025-09-25', N'Website công ty', N'Chờ quyết định', 8.5, N'System'),
(10, N'Phạm Thị Hương', '1995-10-22', N'Nữ', '0910000000', 'huong.pham@gmail.com', 4, '2025-10-05', N'CareerBuilder', N'Đang phỏng vấn', 7.0, N'System');
SET IDENTITY_INSERT UngVien OFF;
PRINT N'✓ Đã insert UngVien';

-- Insert ChamCong
SET IDENTITY_INSERT ChamCong ON;
INSERT INTO ChamCong (MaCC, MaNV, Ngay, GioVao, GioRa, GioCong, TangCa, DiMuon, TrangThai, NguoiTao) VALUES
(1, 2, '2025-10-20', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(2, 2, '2025-10-21', '08:15', '17:00', 8.0, 0.0, 15, N'Đi làm', N'System'),
(3, 4, '2025-10-20', '08:00', '17:00', 8.0, 2.0, 0, N'Đi làm', N'System'),
(4, 4, '2025-10-21', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(5, 8, '2025-10-20', '08:00', '17:00', 8.0, 1.0, 0, N'Đi làm', N'System'),
(6, 8, '2025-10-21', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(7, 9, '2025-10-20', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(8, 9, '2025-10-21', '08:00', '17:00', 8.0, 3.0, 0, N'Đi làm', N'System'),
(9, 14, '2025-10-20', '08:00', '17:00', 8.0, 2.0, 0, N'Đi làm', N'System'),
(10, 14, '2025-10-21', '08:00', '17:00', 8.0, 1.0, 0, N'Đi làm', N'System'),
(11, 2, '2025-11-05', '08:00', '17:00', 8.0, 1.0, 0, N'Đi làm', N'System'),
(12, 2, '2025-11-06', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(13, 4, '2025-11-05', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(14, 4, '2025-11-06', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(15, 8, '2025-11-05', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(16, 8, '2025-11-06', '08:20', '17:00', 8.0, 2.0, 20, N'Đi làm', N'System'),
(17, 9, '2025-11-05', '08:00', '17:00', 8.0, 4.0, 0, N'Đi làm', N'System'),
(18, 9, '2025-11-06', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(19, 14, '2025-11-05', '08:00', '17:00', 8.0, 1.0, 0, N'Đi làm', N'System'),
(20, 14, '2025-11-06', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(21, 15, '2025-11-05', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(22, 15, '2025-11-06', '08:00', '17:00', 8.0, 1.5, 0, N'Đi làm', N'System'),
(23, 20, '2025-11-05', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(24, 20, '2025-11-06', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(25, 21, '2025-11-05', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(26, 21, '2025-11-06', '08:05', '17:00', 8.0, 0.0, 5, N'Đi làm', N'System'),
(27, 26, '2025-11-05', '08:00', '17:00', 8.0, 2.0, 0, N'Đi làm', N'System'),
(28, 26, '2025-11-06', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System'),
(29, 29, '2025-11-05', '08:00', '17:00', 8.0, 3.0, 0, N'Đi làm', N'System'),
(30, 29, '2025-11-06', '08:00', '17:00', 8.0, 0.0, 0, N'Đi làm', N'System');
SET IDENTITY_INSERT ChamCong OFF;
PRINT N'✓ Đã insert ChamCong';

-- Insert NghiPhep
SET IDENTITY_INSERT NghiPhep ON;
INSERT INTO NghiPhep (MaNP, MaNV, LoaiNghi, NgayBatDau, NgayKetThuc, LyDo, NguoiDuyet, TrangThai, NguoiTao) VALUES
(1, 4, N'Phép năm', '2025-11-01', '2025-11-03', N'Nghỉ phép năm', 2, N'Chờ duyệt', N'System'),
(2, 9, N'Nghỉ bệnh', '2025-10-10', '2025-10-10', N'Bị cảm', 8, N'Đã duyệt', N'System'),
(3, 15, N'Phép năm', '2025-12-01', '2025-12-05', N'Nghỉ cuối năm', 14, N'Chờ duyệt', N'System'),
(4, 21, N'Nghỉ việc riêng', '2025-07-15', '2025-07-20', N'Đi du lịch', 20, N'Đã duyệt', N'System'),
(5, 10, N'Phép năm', '2025-11-15', '2025-11-16', N'Việc gia đình', 8, N'Chờ duyệt', N'System'),
(6, 17, N'Nghỉ bệnh', '2025-10-25', '2025-10-26', N'Đau răng', 14, N'Đã duyệt', N'System'),
(7, 22, N'Phép năm', '2025-12-20', '2025-12-24', N'Nghỉ lễ', 20, N'Chờ duyệt', N'System'),
(8, 27, N'Nghỉ việc riêng', '2025-11-08', '2025-11-08', N'Làm việc cá nhân', 26, N'Đã duyệt', N'System'),
(9, 28, N'Phép năm', '2025-11-20', '2025-11-22', N'Nghỉ phép', 26, N'Chờ duyệt', N'System'),
(10, 30, N'Nghỉ bệnh', '2025-10-30', '2025-10-31', N'Cảm cúm', 29, N'Đã duyệt', N'System');
SET IDENTITY_INSERT NghiPhep OFF;
PRINT N'✓ Đã insert NghiPhep';

-- Insert DaoTao
SET IDENTITY_INSERT DaoTao ON;
INSERT INTO DaoTao (MaDT, TenKhoaHoc, MoTa, LoaiDaoTao, GiangVien, NgayBatDau, NgayKetThuc, ThoiLuong, HocPhi, SoLuongToiDa, TrangThai, NguoiTao) VALUES
(1, N'Đào tạo hội nhập nhân viên mới 2025', N'Chương trình đào tạo cho nhân viên mới về văn hóa công ty và quy trình làm việc', N'Hội nhập', N'Trần Thị Bích', '2025-07-15', '2025-07-17', 24, 0, 30, N'Đã kết thúc', N'System'),
(2, N'Kỹ năng giao tiếp và làm việc nhóm', N'Phát triển kỹ năng mềm cho nhân viên', N'Kỹ năng mềm', N'Nguyễn Văn Phúc (Chuyên gia ngoài)', '2025-09-10', '2025-09-12', 20, 5000000, 25, N'Đã kết thúc', N'System'),
(3, N'Digital Marketing 2025', N'Xu hướng marketing kỹ thuật số', N'Chuyên môn', N'Võ Thanh Tùng', '2025-10-15', '2025-10-20', 40, 8000000, 20, N'Đã kết thúc', N'System'),
(4, N'Quản lý dự án với Agile/Scrum', N'Phương pháp quản lý dự án hiện đại', N'Nâng cao', N'Phạm Quốc Huy', '2025-11-15', '2025-11-18', 32, 10000000, 15, N'Sắp diễn ra', N'System'),
(5, N'An toàn lao động và phòng cháy chữa cháy', N'Đào tạo bắt buộc hàng năm', N'Bắt buộc', N'Công ty PCCC ABC', '2025-12-05', '2025-12-06', 16, 0, 50, N'Sắp diễn ra', N'System');
SET IDENTITY_INSERT DaoTao OFF;
PRINT N'✓ Đã insert DaoTao';

-- Insert ThamGiaDaoTao
SET IDENTITY_INSERT ThamGiaDaoTao ON;
INSERT INTO ThamGiaDaoTao (MaTGDT, MaDT, MaNV, TrangThaiThamGia, DiemDanhGia, XepLoai, NguoiTao) VALUES
(1, 1, 7, N'Hoàn thành', 8.5, N'Giỏi', N'System'),
(2, 1, 13, N'Hoàn thành', 9.0, N'Xuất sắc', N'System'),
(3, 1, 19, N'Hoàn thành', 8.0, N'Giỏi', N'System'),
(4, 1, 25, N'Hoàn thành', 7.5, N'Khá', N'System'),
(5, 2, 4, N'Hoàn thành', 8.0, N'Giỏi', N'System'),
(6, 2, 5, N'Hoàn thành', 7.0, N'Khá', N'System'),
(7, 2, 9, N'Hoàn thành', 8.5, N'Giỏi', N'System'),
(8, 2, 10, N'Hoàn thành', 9.0, N'Xuất sắc', N'System'),
(9, 2, 15, N'Hoàn thành', 7.5, N'Khá', N'System'),
(10, 2, 21, N'Vắng mặt', NULL, NULL, N'System'),
(11, 3, 9, N'Hoàn thành', 8.5, N'Giỏi', N'System'),
(12, 3, 10, N'Hoàn thành', 7.0, N'Khá', N'System'),
(13, 3, 27, N'Hoàn thành', 9.5, N'Xuất sắc', N'System'),
(14, 3, 28, N'Hoàn thành', 8.0, N'Giỏi', N'System'),
(15, 4, 8, N'Đã đăng ký', NULL, NULL, N'System'),
(16, 4, 14, N'Đã đăng ký', NULL, NULL, N'System'),
(17, 4, 18, N'Đã đăng ký', NULL, NULL, N'System'),
(18, 4, 26, N'Đã đăng ký', NULL, NULL, N'System'),
(19, 4, 29, N'Đã đăng ký', NULL, NULL, N'System'),
(20, 4, 30, N'Đã đăng ký', NULL, NULL, N'System');
SET IDENTITY_INSERT ThamGiaDaoTao OFF;
PRINT N'✓ Đã insert ThamGiaDaoTao';

-- Insert DanhGia
SET IDENTITY_INSERT DanhGia ON;
INSERT INTO DanhGia (MaDG, MaNV, KyDanhGia, NgayDanhGia, NguoiDanhGia, LoaiDanhGia, DiemKPI, DiemThaiDo, DiemKyNang, DiemChuyenMon, DiemTrungBinh, XepLoai, DeXuatKhenThuong, DeXuatTangLuong, NguoiTao) VALUES
(1, 2, N'Năm 2024', '2024-12-31', 1, N'Cuối năm', 95.0, 9.5, 9.0, 9.5, 9.3, N'A', 20000000, 5000000, N'System'),
(2, 4, N'Năm 2024', '2024-12-31', 2, N'Cuối năm', 85.0, 8.5, 8.0, 8.0, 8.2, N'B', 10000000, 2000000, N'System'),
(3, 5, N'Năm 2024', '2024-12-31', 2, N'Cuối năm', 70.0, 7.0, 7.5, 7.0, 7.2, N'C', 0, 0, N'System'),
(4, 8, N'Năm 2024', '2024-12-31', 1, N'Cuối năm', 90.0, 9.0, 8.5, 9.0, 8.8, N'A', 15000000, 5000000, N'System'),
(5, 9, N'Năm 2024', '2024-12-31', 8, N'Cuối năm', 88.0, 8.5, 8.5, 8.5, 8.5, N'B', 12000000, 3000000, N'System'),
(6, 10, N'Năm 2024', '2024-12-31', 8, N'Cuối năm', 75.0, 7.5, 7.5, 7.0, 7.3, N'C', 5000000, 1000000, N'System'),
(7, 14, N'Năm 2024', '2024-12-31', 1, N'Cuối năm', 92.0, 9.0, 9.0, 9.5, 9.2, N'A', 18000000, 5000000, N'System'),
(8, 15, N'Năm 2024', '2024-12-31', 14, N'Cuối năm', 87.0, 8.5, 8.5, 8.5, 8.5, N'B', 10000000, 2500000, N'System'),
(9, 20, N'Năm 2024', '2024-12-31', 1, N'Cuối năm', 90.0, 9.0, 8.5, 9.0, 8.8, N'A', 15000000, 4000000, N'System'),
(10, 21, N'Năm 2024', '2024-12-31', 20, N'Cuối năm', 80.0, 8.0, 8.0, 7.5, 7.8, N'B', 8000000, 2000000, N'System'),
(11, 26, N'Năm 2024', '2024-12-31', 1, N'Cuối năm', 89.0, 8.5, 8.5, 9.0, 8.7, N'B', 12000000, 3000000, N'System'),
(12, 27, N'Năm 2024', '2024-12-31', 26, N'Cuối năm', 83.0, 8.0, 8.0, 8.0, 8.0, N'B', 10000000, 2000000, N'System'),
(13, 29, N'Năm 2024', '2024-12-31', 1, N'Cuối năm', 93.0, 9.0, 9.0, 9.5, 9.2, N'A', 20000000, 5000000, N'System'),
(14, 30, N'Năm 2024', '2024-12-31', 29, N'Cuối năm', 86.0, 8.5, 8.0, 8.5, 8.3, N'B', 10000000, 2500000, N'System'),
(15, 7, N'Thử việc Q3/2024', '2024-09-01', 2, N'Thử việc', 80.0, 8.0, 8.0, 7.5, 7.8, N'B', 0, 0, N'System');
SET IDENTITY_INSERT DanhGia OFF;
PRINT N'✓ Đã insert DanhGia';

-- Insert BangLuong
SET IDENTITY_INSERT BangLuong ON;
INSERT INTO BangLuong (MaBL, MaNV, ThangNam, LuongCoBan, PhuCap, ThuongHieuSuat, TienTangCa, BHXH, BHYT, BHTN, ThueTNCN, TrangThai, NguoiTao) VALUES
(1, 1, '2025-10-01', 80000000, 10000000, 10000000, 0, 6400000, 1200000, 800000, 8500000, N'Đã thanh toán', N'System'),
(2, 2, '2025-10-01', 28000000, 5000000, 10000000, 0, 2240000, 420000, 280000, 3600000, N'Đã thanh toán', N'System'),
(3, 4, '2025-10-01', 15000000, 2000000, 5000000, 480000, 1200000, 225000, 150000, 1900000, N'Đã thanh toán', N'System'),
(4, 8, '2025-10-01', 30000000, 5000000, 8000000, 240000, 2400000, 450000, 300000, 3800000, N'Đã thanh toán', N'System'),
(5, 9, '2025-10-01', 16000000, 2500000, 5000000, 0, 1280000, 240000, 160000, 2100000, N'Đã thanh toán', N'System'),
(6, 10, '2025-10-01', 14500000, 2000000, 3000000, 720000, 1160000, 217500, 145000, 1600000, N'Đã thanh toán', N'System'),
(7, 14, '2025-10-01', 32000000, 5000000, 10000000, 480000, 2560000, 480000, 320000, 4100000, N'Đã thanh toán', N'System'),
(8, 15, '2025-10-01', 18000000, 3000000, 6000000, 0, 1440000, 270000, 180000, 2400000, N'Đã thanh toán', N'System'),
(9, 20, '2025-10-01', 29000000, 5000000, 8000000, 0, 2320000, 435000, 290000, 3700000, N'Đã thanh toán', N'System'),
(10, 21, '2025-10-01', 15500000, 2000000, 4000000, 0, 1240000, 232500, 155000, 1800000, N'Đã thanh toán', N'System'),
(11, 26, '2025-10-01', 27000000, 4500000, 7000000, 360000, 2160000, 405000, 270000, 3400000, N'Đã thanh toán', N'System'),
(12, 27, '2025-10-01', 14500000, 2000000, 4000000, 0, 1160000, 217500, 145000, 1700000, N'Đã thanh toán', N'System'),
(13, 29, '2025-10-01', 31000000, 5000000, 12000000, 0, 2480000, 465000, 310000, 4200000, N'Đã thanh toán', N'System'),
(14, 30, '2025-10-01', 16500000, 2500000, 5000000, 0, 1320000, 247500, 165000, 2000000, N'Đã thanh toán', N'System'),
(15, 7, '2025-10-01', 5000000, 0, 0, 0, 0, 0, 0, 0, N'Đã thanh toán', N'System');
SET IDENTITY_INSERT BangLuong OFF;
PRINT N'✓ Đã insert BangLuong';

-- Insert KhenThuong
SET IDENTITY_INSERT KhenThuong ON;
INSERT INTO KhenThuong (MaKT, MaNV, HinhThuc, LyDo, NgayKhenThuong, NguoiKy, GiaTriTien, NguoiTao) VALUES
(1, 2, N'Giấy khen + Tiền thưởng', N'Hoàn thành xuất sắc công việc năm 2024', '2025-01-10', N'Nguyễn Văn An', 20000000, N'System'),
(2, 8, N'Bằng khen + Tiền thưởng', N'Đạt doanh số cao nhất quý 4/2024', '2025-01-10', N'Nguyễn Văn An', 15000000, N'System'),
(3, 14, N'Giấy khen + Tiền thưởng', N'Hoàn thành dự án đúng tiến độ', '2025-02-15', N'Nguyễn Văn An', 18000000, N'System'),
(4, 29, N'Giấy khen + Tiền thưởng', N'Xuất sắc trong công tác IT', '2025-01-10', N'Nguyễn Văn An', 20000000, N'System'),
(5, 27, N'Giấy khen', N'Sáng tạo trong chiến dịch marketing', '2025-03-20', N'Võ Thanh Tùng', 5000000, N'System');
SET IDENTITY_INSERT KhenThuong OFF;
PRINT N'✓ Đã insert KhenThuong';

-- Insert KyLuat
SET IDENTITY_INSERT KyLuat ON;
INSERT INTO KyLuat (MaKL, MaNV, HinhThuc, LyDo, NgayKyLuat, NguoiKy, MucPhat, ThoiGianHieuLuc, NguoiTao) VALUES
(1, 5, N'Nhắc nhở', N'Đi làm muộn nhiều lần', '2025-09-15', N'Trần Thị Bích', 0, 3, N'System'),
(2, 10, N'Cảnh cáo', N'Vi phạm quy định công ty', '2025-08-20', N'Lê Hoàng Nam', 0, 6, N'System'),
(3, 21, N'Phạt tiền', N'Sai sót trong báo cáo tài chính', '2025-07-10', N'Đặng Thu Hương', 1000000, 12, N'System');
SET IDENTITY_INSERT KyLuat OFF;
PRINT N'✓ Đã insert KyLuat';

-- Insert ThongBao
SET IDENTITY_INSERT ThongBao ON;
INSERT INTO ThongBao (MaTB, TieuDe, NoiDung, LoaiThongBao, DoUuTien, MaPB, NguoiGui, TrangThai, NguoiTao) VALUES
(1, N'Thông báo nghỉ lễ Quốc khánh 2/9', N'Công ty thông báo nghỉ lễ Quốc khánh từ 31/8 đến 3/9/2025. Toàn thể CBNV quay lại làm việc ngày 4/9.', N'Công ty', N'Cao', NULL, 1, 1, N'System'),
(2, N'Họp toàn thể phòng Kinh doanh', N'Họp tổng kết quý 3 và triển khai kế hoạch quý 4. Thời gian: 15h ngày 5/10/2025 tại phòng họp tầng 3.', N'Phòng ban', N'Cao', 3, 8, 1, N'System'),
(3, N'Chương trình đào tạo Agile/Scrum', N'Mời các anh chị tham gia khóa đào tạo Agile/Scrum từ 15-18/11/2025. Vui lòng đăng ký trước ngày 10/11.', N'Công ty', N'Trung bình', NULL, 2, 1, N'System'),
(4, N'Thông báo thay đổi chính sách nghỉ phép', N'Từ tháng 1/2026, chính sách nghỉ phép năm sẽ được điều chỉnh. Chi tiết xem file đính kèm.', N'Công ty', N'Cao', NULL, 2, 1, N'System'),
(5, N'Chúc mừng thành tích xuất sắc', N'Ban Giám đốc chúc mừng các cá nhân đạt thành tích xuất sắc năm 2024.', N'Công ty', N'Thấp', NULL, 1, 1, N'System');
SET IDENTITY_INSERT ThongBao OFF;
PRINT N'✓ Đã insert ThongBao';

-- Insert NguoiDung
INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai, NguoiTao) VALUES
(N'admin', N'Admin@123', N'Admin', 1, 1, N'System'),
(N'hr.manager', N'Hr@123', N'QL', 2, 1, N'System'),
(N'sale.manager', N'Sale@123', N'QL', 8, 1, N'System'),
(N'tech.manager', N'Tech@123', N'QL', 14, 1, N'System'),
(N'finance.manager', N'Finance@123', N'QL', 20, 1, N'System'),
(N'it.manager', N'It@123', N'QL', 29, 1, N'System'),
(N'nv.mai', N'Nv@123', N'NV', 4, 1, N'System'),
(N'nv.hang', N'Nv@123', N'NV', 9, 1, N'System'),
(N'nv.long', N'Nv@123', N'NV', 15, 1, N'System'),
(N'nv.dung', N'Nv@123', N'NV', 21, 1, N'System');
PRINT N'✓ Đã insert NguoiDung';

GO

PRINT N'✓ Hoàn thành insert dữ liệu mẫu';
GO

-- =============================================

-- =============================================
-- PHẦN 5: STORED PROCEDURES (ĐÃ SỬA LỖI LOGIC)
-- =============================================

PRINT N'';
PRINT N'================================================';
PRINT N'  ĐANG TẠO STORED PROCEDURES (VERSION 3.0)';
PRINT N'================================================';
GO

-- SP 1: Chấm Công (Đã fix lỗi mất dữ liệu khi checkout)
CREATE OR ALTER PROCEDURE sp_ChamCong
    @MaNV INT,
    @Ngay DATE,
    @GioVao TIME = NULL,
    @GioRa TIME = NULL,
    @GioCong DECIMAL(4,2) = 8.0,
    @TangCa DECIMAL(4,2) = 0,
    @TrangThai NVARCHAR(50) = N'Đi làm',
    @NguoiTao NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        IF NOT EXISTS (SELECT 1 FROM NhanVien WHERE MaNV = @MaNV AND TrangThai = N'Đang làm việc')
            THROW 50020, N'Nhân viên không tồn tại hoặc đã nghỉ việc', 1;
        
        IF @Ngay > GETDATE()
            THROW 50021, N'Không thể chấm công cho ngày trong tương lai', 1;

        DECLARE @FinalGioVao TIME, @FinalGioRa TIME;
        DECLARE @FinalDiMuon INT = 0, @FinalVeSom INT = 0;
        DECLARE @FinalGioCong DECIMAL(4,2) = 0;

        IF EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @MaNV AND Ngay = @Ngay)
        BEGIN
            DECLARE @OldGioVao TIME, @OldGioRa TIME;
            SELECT @OldGioVao = GioVao, @OldGioRa = GioRa FROM ChamCong WHERE MaNV = @MaNV AND Ngay = @Ngay;
            
            SET @FinalGioVao = ISNULL(@GioVao, @OldGioVao);
            SET @FinalGioRa = ISNULL(@GioRa, @OldGioRa);

            IF @FinalGioVao IS NOT NULL AND @FinalGioVao > '08:00:00'
                SET @FinalDiMuon = DATEDIFF(MINUTE, '08:00:00', @FinalGioVao);
            
            IF @FinalGioRa IS NOT NULL AND @FinalGioRa < '17:00:00' AND @TrangThai = N'Đi làm'
                SET @FinalVeSom = DATEDIFF(MINUTE, @FinalGioRa, '17:00:00');

            IF @FinalGioVao IS NOT NULL AND @FinalGioRa IS NOT NULL
            BEGIN
                DECLARE @PhutLamViec INT = DATEDIFF(MINUTE, @FinalGioVao, @FinalGioRa);
                IF @FinalGioVao <= '12:00:00' AND @FinalGioRa >= '13:00:00' SET @PhutLamViec = @PhutLamViec - 60;
                IF @PhutLamViec < 0 SET @PhutLamViec = 0;
                SET @FinalGioCong = CAST(@PhutLamViec AS DECIMAL(10,2)) / 60.0;
                IF @FinalGioCong > 24 SET @FinalGioCong = 24;
            END

            UPDATE ChamCong
            SET GioVao = @FinalGioVao, GioRa = @FinalGioRa, GioCong = @FinalGioCong,
                TangCa = @TangCa, DiMuon = @FinalDiMuon, VeSom = @FinalVeSom,
                TrangThai = @TrangThai, NguoiCapNhat = @NguoiTao, NgayCapNhat = GETDATE()
            WHERE MaNV = @MaNV AND Ngay = @Ngay;
        END
        ELSE
        BEGIN
            SET @FinalGioVao = @GioVao;
            SET @FinalGioRa = @GioRa;
            IF @FinalGioVao IS NOT NULL AND @FinalGioVao > '08:00:00'
                SET @FinalDiMuon = DATEDIFF(MINUTE, '08:00:00', @FinalGioVao);

            INSERT INTO ChamCong (MaNV, Ngay, GioVao, GioRa, GioCong, TangCa, DiMuon, VeSom, TrangThai, NguoiTao, NgayTao)
            VALUES (@MaNV, @Ngay, @FinalGioVao, @FinalGioRa, 0, @TangCa, @FinalDiMuon, 0, @TrangThai, @NguoiTao, GETDATE());
        END
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @Msg NVARCHAR(MAX) = ERROR_MESSAGE();
        RAISERROR(@Msg, 16, 1);
    END CATCH
END;
GO
PRINT N'✓ Đã tạo sp_ChamCong (Version 3.0 - Fixed)';

-- SP 2: Duyệt Nghỉ Phép (Đã fix lỗi trừ ngày phép T7 CN và cập nhật bảng công)
CREATE OR ALTER PROCEDURE sp_DuyetNghiPhep
    @MaNP INT,
    @NguoiDuyet INT,
    @TrangThai NVARCHAR(20),
    @LyDoTuChoi NVARCHAR(255) = NULL
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        IF NOT EXISTS (SELECT 1 FROM NghiPhep WHERE MaNP = @MaNP) THROW 50040, N'Đơn không tồn tại', 1;
        
        DECLARE @MaNV INT, @LoaiNghi NVARCHAR(50), @NgayBatDau DATE, @NgayKetThuc DATE;
        SELECT @MaNV = MaNV, @LoaiNghi = LoaiNghi, @NgayBatDau = NgayBatDau, @NgayKetThuc = NgayKetThuc
        FROM NghiPhep WHERE MaNP = @MaNP;
        
        IF @TrangThai = N'Đã duyệt' 
        BEGIN
            DECLARE @SoNgayPhepTru INT = 0;
            DECLARE @CurrentDate DATE = @NgayBatDau;
            DECLARE @TrangThaiChamCong NVARCHAR(50) = CASE @LoaiNghi 
                WHEN N'Phép năm' THEN N'Phép năm' 
                WHEN N'Nghỉ bệnh' THEN N'Nghỉ bệnh' 
                ELSE N'Nghỉ có phép' END;

            WHILE @CurrentDate <= @NgayKetThuc
            BEGIN
                IF DATEPART(WEEKDAY, @CurrentDate) NOT IN (1, 7) 
                BEGIN
                    SET @SoNgayPhepTru = @SoNgayPhepTru + 1;
                    
                    IF EXISTS (SELECT 1 FROM ChamCong WHERE MaNV = @MaNV AND Ngay = @CurrentDate)
                        UPDATE ChamCong SET TrangThai = @TrangThaiChamCong, GhiChu = N'Duyệt nghỉ', NgayCapNhat = GETDATE()
                        WHERE MaNV = @MaNV AND Ngay = @CurrentDate;
                    ELSE
                        INSERT INTO ChamCong (MaNV, Ngay, GioCong, TrangThai, NguoiTao, NgayTao)
                        VALUES (@MaNV, @CurrentDate, 0, @TrangThaiChamCong, CAST(@NguoiDuyet AS NVARCHAR(50)), GETDATE());
                END
                SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
            END

            IF @LoaiNghi = N'Phép năm'
            BEGIN
                UPDATE NhanVien SET SoNgayPhep = SoNgayPhep - @SoNgayPhepTru WHERE MaNV = @MaNV;
            END
        END
        
        UPDATE NghiPhep SET TrangThai = @TrangThai, NguoiDuyet = @NguoiDuyet, NgayDuyet = GETDATE(), LyDoTuChoi = @LyDoTuChoi WHERE MaNP = @MaNP;
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @Msg2 NVARCHAR(MAX) = ERROR_MESSAGE();
        RAISERROR(@Msg2, 16, 1);
    END CATCH
END;
GO
PRINT N'✓ Đã tạo sp_DuyetNghiPhep (Version 3.0 - Fixed)';

-- SP 3: Thêm Nhân Viên (Giữ nguyên)
CREATE OR ALTER PROCEDURE sp_ThemNhanVien
    @HoTen NVARCHAR(100),
    @NgaySinh DATE,
    @GioiTinh NVARCHAR(10),
    @CMND NVARCHAR(20),
    @DienThoai NVARCHAR(15),
    @Email NVARCHAR(100),
    @DiaChiThuongTru NVARCHAR(255),
    @MaPB INT,
    @MaCV INT,
    @NgayVaoLam DATE,
    @LuongCoBan DECIMAL(18,0),
    @PhuCap DECIMAL(18,0) = 0,
    @NguoiTao NVARCHAR(50),
    @MaNV INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        IF @HoTen IS NULL OR LEN(LTRIM(RTRIM(@HoTen))) = 0 THROW 50001, N'Họ tên không được để trống', 1;
        
        INSERT INTO NhanVien (HoTen, NgaySinh, GioiTinh, CMND, DienThoai, Email, DiaChiThuongTru, MaPB, MaCV, NgayVaoLam, LuongCoBan, PhuCap, TrangThai, NguoiTao)
        VALUES (@HoTen, @NgaySinh, @GioiTinh, @CMND, @DienThoai, @Email, @DiaChiThuongTru, @MaPB, @MaCV, @NgayVaoLam, @LuongCoBan, @PhuCap, N'Đang làm việc', @NguoiTao);
        
        SET @MaNV = SCOPE_IDENTITY();
        
        COMMIT TRANSACTION;
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        THROW;
    END CATCH
END;
GO
PRINT N'✓ Đã tạo sp_ThemNhanVien';

GO

PRINT N'';
PRINT N'================================================';

-- =============================================
-- V4 NEW: 3 STORED PROCEDURES MỚI
-- =============================================

-- SP 4: sp_TinhLuongThang (V4 MỚI - Tính lương tự động)
CREATE OR ALTER PROCEDURE sp_TinhLuongThang
    @ThangNam DATE,
    @NguoiTao NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    BEGIN TRY
        BEGIN TRANSACTION;
        
        DECLARE @Thang INT = MONTH(@ThangNam);
        DECLARE @Nam INT = YEAR(@ThangNam);
        
        -- Xóa dữ liệu lương cũ của tháng này (nếu có)
        DELETE FROM BangLuong WHERE MONTH(ThangNam) = @Thang AND YEAR(ThangNam) = @Nam;

        -- Tính lương cho tất cả nhân viên đang làm việc
        INSERT INTO BangLuong (MaNV, ThangNam, LuongCoBan, PhuCap, TienTangCa, ThuongHieuSuat, ThuongKhac, BHXH, BHYT, BHTN, ThueTNCN, CacKhoanKhauTruKhac, TrangThai, NgayTao, NguoiTao)
        SELECT 
            nv.MaNV,
            @ThangNam,
            nv.LuongCoBan,
            nv.PhuCap,
            0 AS TienTangCa, -- Tạm tính 0, có thể cập nhật sau từ bảng ChamCong
            0 AS ThuongHieuSuat,
            0 AS ThuongKhac,
            CAST(nv.LuongCoBan * 0.08 AS DECIMAL(18,0)) AS BHXH,
            CAST(nv.LuongCoBan * 0.015 AS DECIMAL(18,0)) AS BHYT,
            CAST(nv.LuongCoBan * 0.01 AS DECIMAL(18,0)) AS BHTN,
            CASE 
                WHEN (nv.LuongCoBan + nv.PhuCap - 11000000) <= 0 THEN 0
                WHEN (nv.LuongCoBan + nv.PhuCap - 11000000) <= 5000000 THEN CAST((nv.LuongCoBan + nv.PhuCap - 11000000) * 0.05 AS DECIMAL(18,0))
                WHEN (nv.LuongCoBan + nv.PhuCap - 11000000) <= 10000000 THEN CAST((nv.LuongCoBan + nv.PhuCap - 11000000) * 0.10 AS DECIMAL(18,0))
                ELSE CAST((nv.LuongCoBan + nv.PhuCap - 11000000) * 0.15 AS DECIMAL(18,0))
            END AS ThueTNCN,
            0 AS CacKhoanKhauTruKhac,
            N'Chưa thanh toán',
            GETDATE(),
            @NguoiTao
        FROM NhanVien nv
        WHERE nv.TrangThai = N'Đang làm việc';
        
        COMMIT TRANSACTION;
        PRINT N'✓ Đã tính lương tháng ' + CAST(@Thang AS NVARCHAR) + '/' + CAST(@Nam AS NVARCHAR);
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
        DECLARE @Msg3 NVARCHAR(MAX) = ERROR_MESSAGE();
        RAISERROR(@Msg3, 16, 1);
    END CATCH
END;
GO
PRINT N'✓ Đã tạo sp_TinhLuongThang (V4 - New)';

-- SP 5: sp_BaoCaoThongKeNhanSu (V4 MỚI - Dashboard)
CREATE OR ALTER PROCEDURE sp_BaoCaoThongKeNhanSu
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        N'Tổng nhân viên đang làm việc' AS TieuChi, 
        CAST(COUNT(*) AS NVARCHAR) AS GiaTri 
    FROM NhanVien 
    WHERE TrangThai = N'Đang làm việc'
    
    UNION ALL
    
    SELECT 
        N'Ứng viên chờ phỏng vấn', 
        CAST(COUNT(*) AS NVARCHAR) 
    FROM UngVien 
    WHERE TrangThai IN (N'Mới ứng tuyển', N'Đang phỏng vấn')
    
    UNION ALL
    
    SELECT 
        N'Tổng lương tháng này (VND)', 
        FORMAT(ISNULL(SUM(LuongThucNhan),0), 'N0') 
    FROM BangLuong 
    WHERE MONTH(ThangNam) = MONTH(GETDATE()) AND YEAR(ThangNam) = YEAR(GETDATE())
    
    UNION ALL
    
    SELECT 
        N'Số phòng ban', 
        CAST(COUNT(*) AS NVARCHAR) 
    FROM PhongBan 
    WHERE TrangThai = 1;
END;
GO
PRINT N'✓ Đã tạo sp_BaoCaoThongKeNhanSu (V4 - New)';

-- SP 6: sp_BaoCaoLuong_Loc (V4 MỚI - Báo cáo lương có lọc)
CREATE OR ALTER PROCEDURE sp_BaoCaoLuong_Loc
    @Thang INT = NULL,
    @Nam INT = NULL,
    @MaPB INT = 0
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Mặc định lấy tháng hiện tại nếu không truyền tham số
    IF @Thang IS NULL SET @Thang = MONTH(GETDATE());
    IF @Nam IS NULL SET @Nam = YEAR(GETDATE());
    
    SELECT 
        nv.MaNV,
        nv.HoTen,
        pb.TenPB AS PhongBan,
        cv.TenCV AS ChucVu,
        bl.LuongCoBan,
        bl.PhuCap,
        bl.TongThuNhap,
        bl.TongKhauTru,
        bl.LuongThucNhan,
        bl.TrangThai
    FROM BangLuong bl
    INNER JOIN NhanVien nv ON bl.MaNV = nv.MaNV
    INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
    INNER JOIN ChucVu cv ON nv.MaCV = cv.MaCV
    WHERE MONTH(bl.ThangNam) = @Thang 
      AND YEAR(bl.ThangNam) = @Nam
      AND (@MaPB = 0 OR nv.MaPB = @MaPB)
    ORDER BY pb.TenPB, nv.HoTen;
END;
GO
PRINT N'✓ Đã tạo sp_BaoCaoLuong_Loc (V4 - New)';

GO

INSERT INTO UngVien (HoTen, NgaySinh, GioiTinh, DienThoai, Email, MaCV, NgayUngTuyen, NguonUngTuyen, TrangThai, DiemPhongVan, NguoiTao) VALUES
(N'Lê Văn Hùng', '1998-05-12', N'Nam', '0909111222', 'hung.le@email.com', 4, '2025-11-01', N'LinkedIn', N'Đang phỏng vấn', 7.5, N'System'),
(N'Trần Thị Mai', '2000-10-20', N'Nữ', '0909111223', 'mai.tran@email.com', 6, '2025-11-02', N'TopCV', N'Mới ứng tuyển', NULL, N'System'),
(N'Nguyễn Quốc Bảo', '1995-01-15', N'Nam', '0909111224', 'bao.nguyen@email.com', 2, '2025-10-25', N'Headhunter', N'Chờ quyết định', 8.0, N'System'),
(N'Phạm Thanh Hằng', '1999-03-08', N'Nữ', '0909111225', 'hang.pham@email.com', 4, '2025-11-05', N'Website', N'Đang xét hồ sơ', NULL, N'System'),
(N'Võ Tấn Phát', '1997-12-12', N'Nam', '0909111226', 'phat.vo@email.com', 3, '2025-10-28', N'Giới thiệu', N'Trúng tuyển', 9.0, N'System'),
(N'Đinh Thu Hà', '2001-06-01', N'Nữ', '0909111227', 'ha.dinh@email.com', 6, '2025-11-10', N'Facebook', N'Mới ứng tuyển', NULL, N'System'),
(N'Lương Thế Vinh', '1996-09-09', N'Nam', '0909111228', 'vinh.luong@email.com', 4, '2025-10-30', N'VietnamWorks', N'Đã loại', 5.5, N'System'),
(N'Trương Mỹ Lan', '1998-02-14', N'Nữ', '0909111229', 'lan.truong@email.com', 5, '2025-11-03', N'LinkedIn', N'Đang phỏng vấn', NULL, N'System'),
(N'Cao Văn Đạt', '1994-11-20', N'Nam', '0909111230', 'dat.cao@email.com', 2, '2025-10-20', N'Headhunter', N'Từ chối offer', 8.5, N'System'),
(N'Ngô Phương Thảo', '2000-07-07', N'Nữ', '0909111231', 'thao.ngo@email.com', 4, '2025-11-12', N'Website', N'Mới ứng tuyển', NULL, N'System'),
(N'Bùi Anh Tuấn', '1993-04-30', N'Nam', '0909111232', 'tuan.bui@email.com', 3, '2025-10-15', N'Giới thiệu', N'Đang phỏng vấn', 7.0, N'System'),
(N'Dương Thị Ngọc', '1999-08-15', N'Nữ', '0909111233', 'ngoc.duong@email.com', 5, '2025-11-08', N'TopCV', N'Đang xét hồ sơ', NULL, N'System'),
(N'Hồ Văn Cường', '1995-12-25', N'Nam', '0909111234', 'cuong.ho@email.com', 4, '2025-10-29', N'LinkedIn', N'Trúng tuyển', 8.8, N'System'),
(N'Lý Nhã Kỳ', '1992-07-19', N'Nữ', '0909111235', 'ky.ly@email.com', 2, '2025-10-22', N'Headhunter', N'Chờ quyết định', 8.2, N'System'),
(N'Mai Văn Sáu', '1990-05-05', N'Nam', '0909111236', 'sau.mai@email.com', 3, '2025-10-18', N'VietnamWorks', N'Đã loại', 4.0, N'System'),
(N'Phan Thị Bích', '2002-01-01', N'Nữ', '0909111237', 'bich.phan@email.com', 6, '2025-11-15', N'Website', N'Mới ứng tuyển', NULL, N'System'),
(N'Vũ Văn Long', '1991-09-02', N'Nam', '0909111238', 'long.vu@email.com', 2, '2025-10-25', N'Giới thiệu', N'Đang phỏng vấn', 7.8, N'System'),
(N'Kiều Thị Dung', '1998-03-26', N'Nữ', '0909111239', 'dung.kieu@email.com', 4, '2025-11-01', N'TopCV', N'Đang xét hồ sơ', NULL, N'System'),
(N'Đặng Văn Lâm', '1993-08-13', N'Nam', '0909111240', 'lam.dang@email.com', 4, '2025-10-20', N'LinkedIn', N'Trúng tuyển', 9.2, N'System'),
(N'Trịnh Kim Chi', '1994-10-10', N'Nữ', '0909111241', 'chi.trinh@email.com', 3, '2025-10-28', N'CareerBuilder', N'Đang phỏng vấn', 7.2, N'System');
GO


-- Bắt đầu khối lệnh xử lý chấm công
BEGIN
    -- 1. Xóa dữ liệu cũ của tháng 11/2025 để tránh trùng lặp/lỗi
    DELETE FROM ChamCong WHERE MONTH(Ngay) = 11 AND YEAR(Ngay) = 2025;
    PRINT N'✓ Đã xóa dữ liệu chấm công cũ tháng 11.';

    -- 2. Khai báo các biến dùng chung (QUAN TRỌNG: Phải chạy cùng lúc với vòng lặp bên dưới)
    DECLARE @FromDate DATE = '2025-11-01';
    DECLARE @ToDate DATE = '2025-11-23'; -- Ngày hiện tại giả lập
    DECLARE @MaNV INT;
    
    -- 3. Tạo con trỏ (Cursor) để duyệt qua từng nhân viên đang làm việc
    DECLARE emp_cursor CURSOR FOR 
    SELECT MaNV FROM NhanVien WHERE TrangThai = N'Đang làm việc';

    OPEN emp_cursor;
    FETCH NEXT FROM emp_cursor INTO @MaNV;

    -- 4. Bắt đầu vòng lặp từng nhân viên
    WHILE @@FETCH_STATUS = 0
    BEGIN
        DECLARE @CurrentDate DATE = @FromDate;
        
        -- Vòng lặp từng ngày trong tháng
        WHILE @CurrentDate <= @ToDate
        BEGIN
            -- Chỉ chấm công nếu không phải Chủ Nhật (1 = Sunday)
            IF DATEPART(WEEKDAY, @CurrentDate) != 1
            BEGIN
                -- Random Giờ Vào: 07:30 đến 08:15
                -- Công thức: DATEADD(MINUTE, Random(0-45), '07:30')
                DECLARE @RandMinsIn INT = CAST(RAND(CHECKSUM(NEWID())) * 45 AS INT); 
                DECLARE @GioVao TIME = DATEADD(MINUTE, @RandMinsIn, '07:30:00');
                
                -- Random Giờ Ra: 17:00 đến 18:30
                DECLARE @RandMinsOut INT = CAST(RAND(CHECKSUM(NEWID())) * 90 AS INT);
                DECLARE @GioRa TIME = DATEADD(MINUTE, @RandMinsOut, '17:00:00');
                
                -- Random Trạng thái: 95% Đi làm, 5% Nghỉ
                DECLARE @TrangThai NVARCHAR(50) = N'Đi làm';
                DECLARE @GioCong DECIMAL(4,2) = 8.0;
                DECLARE @TangCa DECIMAL(4,2) = 0;
                
                -- Giả lập ngẫu nhiên nghỉ phép (5% cơ hội)
                IF CAST(RAND(CHECKSUM(NEWID())) * 100 AS INT) > 95 
                BEGIN
                    SET @TrangThai = N'Nghỉ có phép';
                    SET @GioVao = NULL;
                    SET @GioRa = NULL;
                    SET @GioCong = 0;
                END
                ELSE 
                BEGIN
                    -- Nếu đi làm, tính tăng ca ngẫu nhiên (10% cơ hội tăng ca 1-2 tiếng)
                     IF CAST(RAND(CHECKSUM(NEWID())) * 100 AS INT) > 90
                     BEGIN
                        SET @TangCa = CAST(RAND(CHECKSUM(NEWID())) * 2 + 0.5 AS DECIMAL(4,2)); -- 0.5 đến 2.5h
                     END
                END

                -- Tính đi muộn (nếu vào sau 08:00)
                DECLARE @DiMuon INT = 0;
                IF @GioVao IS NOT NULL AND @GioVao > '08:00:00' 
                    SET @DiMuon = DATEDIFF(MINUTE, '08:00:00', @GioVao);

                -- Chèn dữ liệu vào bảng ChamCong
                INSERT INTO ChamCong (MaNV, Ngay, GioVao, GioRa, GioCong, TangCa, DiMuon, VeSom, TrangThai, NguoiTao)
                VALUES (@MaNV, @CurrentDate, @GioVao, @GioRa, @GioCong, @TangCa, @DiMuon, 0, @TrangThai, N'System_Auto');
            END
            
            -- Tăng ngày lên 1
            SET @CurrentDate = DATEADD(DAY, 1, @CurrentDate);
        END
        
        -- Chuyển sang nhân viên tiếp theo
        FETCH NEXT FROM emp_cursor INTO @MaNV;
    END

    -- 5. Dọn dẹp bộ nhớ
    CLOSE emp_cursor;
    DEALLOCATE emp_cursor;

go

-- Thủ tục lấy dữ liệu chi tiết cho báo cáo thống kê nhân sự
CREATE OR ALTER PROCEDURE sp_Rpt_ThongKeNhanSu
AS
BEGIN
    SELECT 
        pb.MaPB,
        pb.TenPB,
        -- Lấy tên trưởng phòng (Người có chức vụ chứa chữ 'Trưởng phòng' trong phòng ban đó)
        ISNULL((SELECT TOP 1 nv.HoTen 
                FROM NhanVien nv 
                JOIN ChucVu cv ON nv.MaCV = cv.MaCV 
                WHERE nv.MaPB = pb.MaPB AND cv.TenCV LIKE N'%Trưởng phòng%' AND nv.TrangThai = N'Đang làm việc'), N'Chưa có') AS TenTruongPhong,
        
        -- Tổng số nhân viên
        COUNT(nv.MaNV) AS TongSoNV,
        
        -- Số lượng Nam/Nữ
        SUM(CASE WHEN nv.GioiTinh = N'Nam' THEN 1 ELSE 0 END) AS SoNVNam,
        SUM(CASE WHEN nv.GioiTinh = N'Nữ' THEN 1 ELSE 0 END) AS SoNVNu,
        
        -- Thống kê loại hợp đồng (Dựa vào bảng HopDong kết nối với NhanVien)
        (SELECT COUNT(*) FROM HopDong hd JOIN NhanVien n ON hd.MaNV = n.MaNV 
         WHERE n.MaPB = pb.MaPB AND hd.LoaiHopDong = N'Thử việc' AND hd.TrangThai = N'Có hiệu lực') AS SoHopDongThuViec,
         
        (SELECT COUNT(*) FROM HopDong hd JOIN NhanVien n ON hd.MaNV = n.MaNV 
         WHERE n.MaPB = pb.MaPB AND hd.LoaiHopDong != N'Thử việc' AND hd.TrangThai = N'Có hiệu lực') AS SoHopDongChinhThuc

    FROM PhongBan pb
    LEFT JOIN NhanVien nv ON pb.MaPB = nv.MaPB AND nv.TrangThai = N'Đang làm việc'
    GROUP BY pb.MaPB, pb.TenPB
END;
GO


-- Cập nhật thủ tục để hỗ trợ lọc theo Phòng Ban (Tham số @MaPB)
CREATE OR ALTER PROCEDURE sp_Rpt_ThongKeNhanSu
    @MaPB INT = 0 -- Mặc định 0 là lấy tất cả
AS
BEGIN
    SELECT 
        pb.MaPB,
        pb.TenPB,
        -- Lấy tên trưởng phòng
        ISNULL((SELECT TOP 1 nv.HoTen 
                FROM NhanVien nv 
                JOIN ChucVu cv ON nv.MaCV = cv.MaCV 
                WHERE nv.MaPB = pb.MaPB AND cv.TenCV LIKE N'%Trưởng phòng%' AND nv.TrangThai = N'Đang làm việc'), N'Chưa có') AS TenTruongPhong,
        
        -- Các cột số liệu (đặt tên khớp với file RDLC)
        COUNT(nv.MaNV) AS TongSoNV,
        SUM(CASE WHEN nv.GioiTinh = N'Nam' THEN 1 ELSE 0 END) AS SoNVNam,
        SUM(CASE WHEN nv.GioiTinh = N'Nữ' THEN 1 ELSE 0 END) AS SoNVNu,
        
        (SELECT COUNT(*) FROM HopDong hd JOIN NhanVien n ON hd.MaNV = n.MaNV 
         WHERE n.MaPB = pb.MaPB AND hd.LoaiHopDong = N'Thử việc' AND hd.TrangThai = N'Có hiệu lực') AS SoHopDongThuViec,
         
        (SELECT COUNT(*) FROM HopDong hd JOIN NhanVien n ON hd.MaNV = n.MaNV 
         WHERE n.MaPB = pb.MaPB AND hd.LoaiHopDong != N'Thử việc' AND hd.TrangThai = N'Có hiệu lực') AS SoHopDongChinhThuc

    FROM PhongBan pb
    LEFT JOIN NhanVien nv ON pb.MaPB = nv.MaPB AND nv.TrangThai = N'Đang làm việc'
    -- Logic lọc: Nếu @MaPB = 0 thì lấy hết, ngược lại chỉ lấy phòng ban khớp
    WHERE (@MaPB = 0 OR pb.MaPB = @MaPB)
    GROUP BY pb.MaPB, pb.TenPB
END;
GO

-- =============================================
-- SCRIPT BỔ SUNG TÀI KHOẢN ĐĂNG NHẬP
-- Hệ thống Quản lý Nhân sự Unilever
-- Ngày tạo: 2025-11-24
-- =============================================

USE UnileverHRM;
GO

PRINT N'';
PRINT N'================================================';
PRINT N'  BẮT ĐẦU THÊM TÀI KHOẢN BỔ SUNG';
PRINT N'================================================';
GO

-- =============================================
-- PHẦN 1: THÊM TÀI KHOẢN CHO CÁC VAI TRÒ THIẾU
-- =============================================

PRINT N'Đang thêm tài khoản bổ sung...';

-- 1. Tài khoản LÃNH ĐẠO (Ban Giám đốc)
INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai, NguoiTao) 
VALUES 
(N'ceo', N'Ceo@123', N'LanhDao', 1, 1, N'System');
PRINT N'✓ Đã thêm tài khoản Lãnh đạo (CEO)';

-- 2. Tài khoản QUẢN LÝ HR (QL_HR) - Trưởng phòng Nhân sự
INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai, NguoiTao) 
VALUES 
(N'hr.tphan', N'HrQL@123', N'QL_HR', 2, 1, N'System');
PRINT N'✓ Đã thêm tài khoản Trưởng phòng HR (QL_HR)';

-- 3. Tài khoản NHÂN VIÊN HR (NV_HR) - Nhân viên phòng Nhân sự
INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai, NguoiTao) 
VALUES 
(N'hr.nhanvien', N'HrNV@123', N'NV_HR', 4, 1, N'System'),
(N'hr.pho', N'HrNV@123', N'NV_HR', 3, 1, N'System');
PRINT N'✓ Đã thêm tài khoản Nhân viên HR (NV_HR)';

-- 4. Tài khoản TÀI CHÍNH (TaiChinh) - Phòng Tài chính - Kế toán
INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai, NguoiTao) 
VALUES 
(N'finance.ketoan', N'Fin@123', N'TaiChinh', 21, 1, N'System'),
(N'finance.accounting', N'Fin@123', N'TaiChinh', 22, 1, N'System');
PRINT N'✓ Đã thêm tài khoản Tài chính (TaiChinh)';

-- 5. Thêm thêm tài khoản NHÂN VIÊN cho các phòng ban khác
INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai, NguoiTao) 
VALUES 
(N'nv.tuan', N'Nv@123', N'NV', 16, 1, N'System'),
(N'nv.linh', N'Nv@123', N'NV', 22, 1, N'System'),
(N'nv.hieu', N'Nv@123', N'NV', 23, 1, N'System'),
(N'nv.nga', N'Nv@123', N'NV', 28, 1, N'System');
PRINT N'✓ Đã thêm thêm tài khoản Nhân viên';

-- 6. Thêm tài khoản THỬ NGHIỆM cho Demo
INSERT INTO NguoiDung (TenDangNhap, MatKhau, VaiTro, MaNV, TrangThai, NguoiTao) 
VALUES 
(N'demo.admin', N'Demo@123', N'Admin', 1, 1, N'System'),
(N'demo.ql', N'Demo@123', N'QL', 8, 1, N'System'),
(N'demo.nv', N'Demo@123', N'NV', 10, 1, N'System');
PRINT N'✓ Đã thêm tài khoản Demo';

GO


PRINT N'';
PRINT N'================================================';
PRINT N'  KIỂM TRA TÀI KHOẢN SAU KHI THÊM';
PRINT N'================================================';
GO

-- Hiển thị tất cả tài khoản theo vai trò
SELECT 
    VaiTro,
    COUNT(*) AS SoLuong
FROM NguoiDung
WHERE TrangThai = 1
GROUP BY VaiTro
ORDER BY 
    CASE VaiTro
        WHEN N'Admin' THEN 1
        WHEN N'LanhDao' THEN 2
        WHEN N'QL_HR' THEN 3
        WHEN N'QL' THEN 4
        WHEN N'NV_HR' THEN 5
        WHEN N'TaiChinh' THEN 6
        WHEN N'NV' THEN 7
        ELSE 8
    END;

PRINT N'';
PRINT N'Danh sách chi tiết tất cả tài khoản:';
SELECT 
    nd.TenDangNhap,
    nd.MatKhau,
    nd.VaiTro,
    nv.HoTen AS NhanVien,
    pb.TenPB AS PhongBan,
    cv.TenCV AS ChucVu,
    nd.TrangThai,
    nd.LanDangNhapCuoi
FROM NguoiDung nd
LEFT JOIN NhanVien nv ON nd.MaNV = nv.MaNV
LEFT JOIN PhongBan pb ON nv.MaPB = pb.MaPB
LEFT JOIN ChucVu cv ON nv.MaCV = cv.MaCV
WHERE nd.TrangThai = 1
ORDER BY 
    CASE nd.VaiTro
        WHEN N'Admin' THEN 1
        WHEN N'LanhDao' THEN 2
        WHEN N'QL_HR' THEN 3
        WHEN N'QL' THEN 4
        WHEN N'NV_HR' THEN 5
        WHEN N'TaiChinh' THEN 6
        WHEN N'NV' THEN 7
        ELSE 8
    END,
    nd.TenDangNhap;

GO

-- =============================================
-- BỔ SUNG CÁC BẢNG CHO MODULE TUYỂN DỤNG
-- Hệ thống Quản lý Nhân sự Unilever Việt Nam
-- =============================================

USE UnileverHRM;
GO

PRINT N'';
PRINT N'================================================';
PRINT N'  BỔ SUNG CÁC BẢNG MODULE TUYỂN DỤNG';
PRINT N'================================================';
GO

-- =============================================
-- BẢNG 1: YeuCauTuyenDung (Recruitment Request)
-- Quản lý yêu cầu tuyển dụng từ phòng ban
-- =============================================
CREATE TABLE YeuCauTuyenDung (
    MaYCTD INT IDENTITY(1,1) PRIMARY KEY,
    MaPB INT NOT NULL,                      -- Phòng ban yêu cầu
    MaCV INT NOT NULL,                      -- Chức vụ cần tuyển
    SoLuong INT NOT NULL CHECK (SoLuong > 0),
    LyDoTuyenDung NVARCHAR(500),           -- Tuyển mới/thay thế/mở rộng
    DoUuTien NVARCHAR(20) DEFAULT N'Bình thường' 
        CHECK (DoUuTien IN (N'Thấp', N'Bình thường', N'Cao', N'Khẩn cấp')),
    NgayYeuCau DATE DEFAULT GETDATE(),
    NgayCanTuyen DATE,                      -- Deadline cần có người
    NguoiYeuCau INT,                        -- MaNV người yêu cầu (thường là trưởng phòng)
    TrangThai NVARCHAR(50) DEFAULT N'Chờ duyệt' 
        CHECK (TrangThai IN (N'Chờ duyệt', N'Đã duyệt', N'Từ chối', N'Đang tuyển', N'Hoàn thành', N'Đã hủy')),
    NguoiDuyet INT,                         -- MaNV người phê duyệt (HR Manager/CEO)
    NgayDuyet DATE,
    LyDoTuChoi NVARCHAR(500),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_YeuCauTD_PhongBan FOREIGN KEY (MaPB) REFERENCES PhongBan(MaPB),
    CONSTRAINT FK_YeuCauTD_ChucVu FOREIGN KEY (MaCV) REFERENCES ChucVu(MaCV),
    CONSTRAINT FK_YeuCauTD_NguoiYeuCau FOREIGN KEY (NguoiYeuCau) REFERENCES NhanVien(MaNV),
    CONSTRAINT FK_YeuCauTD_NguoiDuyet FOREIGN KEY (NguoiDuyet) REFERENCES NhanVien(MaNV)
);
PRINT N'✓ Đã tạo bảng YeuCauTuyenDung';

-- =============================================
-- BẢNG 2: TinTuyenDung (Job Posting)
-- Quản lý tin tuyển dụng, JD được đăng tuyển
-- =============================================
CREATE TABLE TinTuyenDung (
    MaTTD INT IDENTITY(1,1) PRIMARY KEY,
    MaYCTD INT NOT NULL,                    -- Liên kết với yêu cầu tuyển dụng
    TieuDe NVARCHAR(200) NOT NULL,         -- Ví dụ: "Tuyển Marketing Executive"
    MoTaCongViec NVARCHAR(MAX),            -- Job Description chi tiết
    YeuCauCongViec NVARCHAR(MAX),          -- Requirements (học vấn, kinh nghiệm, kỹ năng)
    QuyenLoi NVARCHAR(MAX),                 -- Benefits
    MucLuongMin DECIMAL(18,0),              -- Mức lương tối thiểu
    MucLuongMax DECIMAL(18,0),              -- Mức lương tối đa
    SoLuongCanTuyen INT NOT NULL,
    HinhThucLamViec NVARCHAR(50) DEFAULT N'Toàn thời gian'
        CHECK (HinhThucLamViec IN (N'Toàn thời gian', N'Bán thời gian', N'Thực tập', N'Hợp đồng')),
    DiaDiemLamViec NVARCHAR(255),           -- Địa điểm làm việc
    NgayDangTin DATE DEFAULT GETDATE(),
    NgayHetHan DATE,
    KenhDang NVARCHAR(255),                 -- Ví dụ: "LinkedIn, VietnamWorks, Website"
    ChuongTrinh NVARCHAR(100),              -- UFLP, UFresh, hoặc NULL
    TrangThai NVARCHAR(50) DEFAULT N'Đang đăng' 
        CHECK (TrangThai IN (N'Nháp', N'Đang đăng', N'Đã đóng', N'Đã hủy')),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_TinTD_YeuCauTD FOREIGN KEY (MaYCTD) REFERENCES YeuCauTuyenDung(MaYCTD)
);
PRINT N'✓ Đã tạo bảng TinTuyenDung';

-- =============================================
-- BẢNG 3: CẬP NHẬT BẢNG UngVien
-- Thêm liên kết với TinTuyenDung
-- =============================================
-- Thêm cột MaTTD vào bảng UngVien để liên kết với tin tuyển dụng
ALTER TABLE UngVien
ADD MaTTD INT;

ALTER TABLE UngVien
ADD CONSTRAINT FK_UngVien_TinTuyenDung FOREIGN KEY (MaTTD) REFERENCES TinTuyenDung(MaTTD);

PRINT N'✓ Đã cập nhật bảng UngVien với liên kết TinTuyenDung';

-- =============================================
-- BẢNG 4: LichPhongVan (Interview Schedule)
-- Quản lý lịch phỏng vấn chi tiết từng vòng
-- =============================================
CREATE TABLE LichPhongVan (
    MaLichPV INT IDENTITY(1,1) PRIMARY KEY,
    MaUV INT NOT NULL,                      -- Ứng viên được phỏng vấn
    VongPhongVan INT NOT NULL CHECK (VongPhongVan >= 1), -- Vòng 1, 2, 3...
    LoaiPhongVan NVARCHAR(100),             -- "Sơ tuyển", "Phỏng vấn chuyên môn", "Phỏng vấn cuối"
    NgayPhongVan DATETIME NOT NULL,
    DiaDiem NVARCHAR(255),                  -- Phòng họp, địa chỉ
    HinhThuc NVARCHAR(50) DEFAULT N'Trực tiếp'
        CHECK (HinhThuc IN (N'Trực tiếp', N'Online', N'Điện thoại')),
    LinkPhongVan NVARCHAR(500),             -- Link Zoom/Meet nếu online
    NguoiPhongVan NVARCHAR(255),            -- Danh sách người phỏng vấn (có thể nhiều người)
    TrangThai NVARCHAR(50) DEFAULT N'Đã lên lịch'
        CHECK (TrangThai IN (N'Đã lên lịch', N'Đã xác nhận', N'Đã hoàn thành', N'Ứng viên vắng', N'Đã hủy')),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_LichPV_UngVien FOREIGN KEY (MaUV) REFERENCES UngVien(MaUV)
);
PRINT N'✓ Đã tạo bảng LichPhongVan';

-- =============================================
-- BẢNG 5: KetQuaPhongVan (Interview Results)
-- Ghi nhận kết quả đánh giá từng vòng phỏng vấn
-- =============================================
CREATE TABLE KetQuaPhongVan (
    MaKQPV INT IDENTITY(1,1) PRIMARY KEY,
    MaLichPV INT NOT NULL,                  -- Liên kết với lịch phỏng vấn
    MaNV INT NOT NULL,                      -- Người phỏng vấn/đánh giá
    NoiDungDanhGia NVARCHAR(MAX),           -- Nội dung đánh giá chi tiết
    DiemChuyenMon DECIMAL(4,2),             -- Điểm chuyên môn (0-10)
    DiemGiaoTiep DECIMAL(4,2),              -- Điểm giao tiếp (0-10)
    DiemThaiDo DECIMAL(4,2),                -- Điểm thái độ (0-10)
    DiemTongThe DECIMAL(4,2),               -- Điểm tổng thể (0-10)
    KetLuan NVARCHAR(50) 
        CHECK (KetLuan IN (N'Đạt', N'Không đạt', N'Cân nhắc', N'Xuất sắc')),
    DeXuat NVARCHAR(500),                   -- Đề xuất: "Qua vòng tiếp theo", "Trúng tuyển", "Loại"
    NgayDanhGia DATETIME DEFAULT GETDATE(),
    GhiChu NVARCHAR(500),
    NgayTao DATETIME DEFAULT GETDATE(),
    NguoiTao NVARCHAR(50),
    NgayCapNhat DATETIME,
    NguoiCapNhat NVARCHAR(50),
    CONSTRAINT FK_KetQuaPV_LichPV FOREIGN KEY (MaLichPV) REFERENCES LichPhongVan(MaLichPV),
    CONSTRAINT FK_KetQuaPV_NhanVien FOREIGN KEY (MaNV) REFERENCES NhanVien(MaNV)
);
PRINT N'✓ Đã tạo bảng KetQuaPhongVan';

GO

-- =============================================
-- TẠO CÁC INDEX ĐỂ TỐI ƯU HIỆU SUẤT
-- =============================================
PRINT N'';
PRINT N'Đang tạo các INDEX...';

CREATE INDEX IX_YeuCauTD_TrangThai ON YeuCauTuyenDung(TrangThai);
CREATE INDEX IX_YeuCauTD_PhongBan ON YeuCauTuyenDung(MaPB);
CREATE INDEX IX_TinTD_TrangThai ON TinTuyenDung(TrangThai);
CREATE INDEX IX_TinTD_NgayHetHan ON TinTuyenDung(NgayHetHan);
CREATE INDEX IX_UngVien_TinTD ON UngVien(MaTTD);
CREATE INDEX IX_LichPV_UngVien ON LichPhongVan(MaUV);
CREATE INDEX IX_LichPV_NgayPhongVan ON LichPhongVan(NgayPhongVan);
CREATE INDEX IX_KetQuaPV_LichPV ON KetQuaPhongVan(MaLichPV);

PRINT N'✓ Đã tạo các INDEX';
GO

-- =============================================
-- DỮ LIỆU MẪU CHO CÁC BẢNG MỚI
-- =============================================
PRINT N'';
PRINT N'Đang thêm dữ liệu mẫu...';

-- Dữ liệu mẫu cho YeuCauTuyenDung
INSERT INTO YeuCauTuyenDung (MaPB, MaCV, SoLuong, LyDoTuyenDung, DoUuTien, NgayYeuCau, NgayCanTuyen, NguoiYeuCau, TrangThai, NguoiDuyet, NgayDuyet, GhiChu)
VALUES 
(2, 3, 2, N'Mở rộng đội ngũ Marketing cho dự án ra mắt sản phẩm mới', N'Cao', '2024-01-15', '2024-03-01', 5, N'Đã duyệt', 1, '2024-01-20', N'Ưu tiên ứng viên có kinh nghiệm Digital Marketing'),
(3, 4, 1, N'Thay thế Sales Manager nghỉ việc', N'Khẩn cấp', '2024-02-01', '2024-02-20', 8, N'Đang tuyển', 1, '2024-02-03', N'Cần người có kinh nghiệm quản lý team từ 5-10 người'),
(4, 2, 1, N'Tuyển Finance Executive cho kỳ báo cáo tài chính', N'Bình thường', '2024-03-10', '2024-04-15', 12, N'Đã duyệt', 1, '2024-03-12', N'Yêu cầu có chứng chỉ kế toán'),
(2, 1, 3, N'Tuyển dụng UFLP Intake 2024 - Marketing Track', N'Cao', '2024-01-05', '2024-06-01', 5, N'Đã duyệt', 1, '2024-01-08', N'Chương trình UFLP 2 năm');

-- Dữ liệu mẫu cho TinTuyenDung
INSERT INTO TinTuyenDung (MaYCTD, TieuDe, MoTaCongViec, YeuCauCongViec, QuyenLoi, MucLuongMin, MucLuongMax, SoLuongCanTuyen, HinhThucLamViec, DiaDiemLamViec, NgayDangTin, NgayHetHan, KenhDang, ChuongTrinh, TrangThai)
VALUES 
(1, N'TUYỂN DỤNG MARKETING EXECUTIVE - UNILEVER VIỆT NAM', 
N'- Xây dựng và triển khai chiến lược Marketing cho các thương hiệu FMCG
- Quản lý campaigns trên nền tảng digital và traditional
- Phân tích thị trường và đối thủ cạnh tranh
- Làm việc với agency và đối tác',
N'- Tốt nghiệp Đại học chuyên ngành Marketing, Quản trị kinh doanh hoặc tương đương
- Có ít nhất 2 năm kinh nghiệm trong lĩnh vực Marketing FMCG
- Thành thạo tiếng Anh (IELTS 6.5+)
- Kỹ năng phân tích dữ liệu tốt
- Nhiệt huyết, sáng tạo và có khả năng làm việc nhóm',
N'- Lương cạnh tranh từ 18-25 triệu
- Thưởng hiệu suất và theo dự án
- Bảo hiểm sức khỏe cao cấp
- Đào tạo chuyên sâu từ tập đoàn
- Môi trường làm việc quốc tế
- Cơ hội thăng tiến rõ ràng',
18000000, 25000000, 2, N'Toàn thời gian', N'Tòa nhà Unilever, 159 Hai Bà Trưng, Quận 3, TP.HCM', 
'2024-01-25', '2024-03-25', N'LinkedIn, VietnamWorks, CareerBuilder, Website Unilever', NULL, N'Đang đăng'),

(2, N'SALES MANAGER - MODERN TRADE CHANNEL', 
N'- Quản lý đội ngũ bán hàng kênh Modern Trade (siêu thị, cửa hàng tiện lợi)
- Xây dựng và duy trì mối quan hệ với các đối tác lớn
- Phân tích dữ liệu bán hàng và đề xuất chiến lược
- Đào tạo và phát triển đội ngũ',
N'- Tốt nghiệp Đại học
- Có ít nhất 5 năm kinh nghiệm quản lý bán hàng, ưu tiên FMCG
- Kỹ năng quản lý team tốt
- Thành thạo tiếng Anh
- Kỹ năng đàm phán và thuyết phục tốt',
N'- Lương: 30-40 triệu + thưởng KPI
- Xe công ty
- Laptop, điện thoại
- Bảo hiểm cao cấp cho cả gia đình
- Du lịch hàng năm',
30000000, 40000000, 1, N'Toàn thời gian', N'TP. Hồ Chí Minh', 
'2024-02-05', '2024-03-05', N'LinkedIn, Headhunter, VietnamWorks', NULL, N'Đã đóng'),

(4, N'UNILEVER FUTURE LEADERS PROGRAMME 2024 - MARKETING TRACK', 
N'Chương trình đào tạo quản lý tương lai 2 năm với lộ trình phát triển:
- Năm 1: Rotation qua các functions (Brand, Digital, Insights)
- Năm 2: Đảm nhận vai trò Assistant Brand Manager
- Được mentor bởi Senior Leadership
- Tham gia các dự án chiến lược của công ty',
N'- Sinh viên năm cuối hoặc mới tốt nghiệp (tối đa 2 năm)
- GPA từ 7.0/10 trở lên
- IELTS 7.0+ hoặc tương đương
- Có kinh nghiệm hoạt động ngoại khóa, câu lạc bộ
- Leadership potential, entrepreneurial mindset',
N'- Lương khởi điểm: 15-18 triệu (tăng theo năng lực)
- Lộ trình thăng tiến rõ ràng lên Manager sau 2-3 năm
- Đào tạo chuyên sâu tại Unilever Leadership Development Centre
- Cơ hội rotation khu vực Châu Á
- Môi trường làm việc trẻ trung, năng động',
15000000, 18000000, 3, N'Toàn thời gian', N'TP. Hồ Chí Minh', 
'2024-01-10', '2024-05-31', N'LinkedIn, Website Unilever, Hội nghề nghiệp các trường ĐH', N'UFLP', N'Đang đăng');

-- Cập nhật dữ liệu UngVien với MaTTD
UPDATE UngVien SET MaTTD = 1 WHERE MaUV IN (1, 2, 3);
UPDATE UngVien SET MaTTD = 3 WHERE MaUV IN (4, 5);

-- Dữ liệu mẫu cho LichPhongVan
INSERT INTO LichPhongVan (MaUV, VongPhongVan, LoaiPhongVan, NgayPhongVan, DiaDiem, HinhThuc, NguoiPhongVan, TrangThai, GhiChu)
VALUES 
(1, 1, N'Phỏng vấn sơ tuyển', '2024-02-15 09:00:00', N'Phòng họp A - Tầng 3', N'Trực tiếp', N'Nguyễn Văn A (HR Manager)', N'Đã hoàn thành', N'Ứng viên ấn tượng'),
(1, 2, N'Phỏng vấn chuyên môn', '2024-02-22 14:00:00', N'Phòng họp B - Tầng 5', N'Trực tiếp', N'Trần Thị B (Marketing Director)', N'Đã hoàn thành', N'Kỹ năng tốt'),
(2, 1, N'Phỏng vấn sơ tuyển', '2024-02-16 10:00:00', NULL, N'Online', N'Nguyễn Văn A (HR Manager)', N'Đã hoàn thành', N'Link Zoom: https://zoom.us/j/xxx'),
(3, 1, N'Phỏng vấn sơ tuyển', '2024-02-20 09:30:00', N'Phòng họp A - Tầng 3', N'Trực tiếp', N'Nguyễn Văn A (HR Manager)', N'Ứng viên vắng', N'Không đến và không báo trước'),
(4, 1, N'Phỏng vấn UFLP - Vòng 1', '2024-03-05 09:00:00', N'Phòng hội thảo - Tầng 2', N'Trực tiếp', N'Panel: HR Manager + Marketing Director', N'Đã lên lịch', N'Assessment center'),
(5, 1, N'Phỏng vấn UFLP - Vòng 1', '2024-03-05 13:00:00', N'Phòng hội thảo - Tầng 2', N'Trực tiếp', N'Panel: HR Manager + Marketing Director', N'Đã xác nhận', N'Assessment center');

-- Dữ liệu mẫu cho KetQuaPhongVan
INSERT INTO KetQuaPhongVan (MaLichPV, MaNV, NoiDungDanhGia, DiemChuyenMon, DiemGiaoTiep, DiemThaiDo, DiemTongThe, KetLuan, DeXuat, NgayDanhGia)
VALUES 
(1, 1, N'Ứng viên có kinh nghiệm tốt trong Digital Marketing. Đã làm việc cho 2 agency lớn. Kỹ năng trình bày rõ ràng, tự tin. Hiểu biết về thị trường FMCG Việt Nam.', 
8.5, 8.0, 9.0, 8.5, N'Đạt', N'Chuyển qua vòng 2 - Phỏng vấn với Marketing Director', '2024-02-15 11:30:00'),

(2, 5, N'Ứng viên xuất sắc! Có tư duy chiến lược tốt, am hiểu consumer insights. Đã từng làm các campaigns thành công. Personality phù hợp với văn hóa Unilever. Đề xuất trúng tuyển với mức lương 22 triệu.', 
9.0, 8.5, 9.0, 8.8, N'Xuất sắc', N'Đề xuất trúng tuyển - Gửi offer letter', '2024-02-22 16:00:00'),

(3, 1, N'Ứng viên có kiến thức cơ bản tốt nhưng thiếu kinh nghiệm thực tế. Cần cải thiện kỹ năng giao tiếp tiếng Anh. Có thể xem xét cho vị trí Junior hơn.', 
6.5, 6.0, 7.5, 6.7, N'Cân nhắc', N'Cân nhắc cho vị trí Marketing Assistant thay vì Executive', '2024-02-16 11:30:00');

PRINT N'✓ Đã thêm dữ liệu mẫu';
GO

-- Kiểm tra và xóa view cũ nếu có
IF OBJECT_ID('vw_ThongTinNhanVien', 'V') IS NOT NULL
    DROP VIEW vw_ThongTinNhanVien;
GO

PRINT N'Đang tạo view vw_ThongTinNhanVien...';
GO

-- Tạo view mới
CREATE VIEW vw_ThongTinNhanVien
AS
SELECT 
    -- Thông tin cơ bản
    nv.MaNV,
    nv.HoTen,
    nv.NgaySinh,
    YEAR(GETDATE()) - YEAR(nv.NgaySinh) AS Tuoi,
    nv.GioiTinh,
    nv.CMND,
    nv.DienThoai,
    nv.Email,
    nv.DiaChiThuongTru,
    nv.DiaChiTamTru,
    
    -- Thông tin công việc
    pb.MaPB,
    pb.TenPB AS PhongBan,
    cv.MaCV,
    cv.TenCV AS ChucVu,
    cv.CapBac,
    nv.NgayVaoLam,
    DATEDIFF(YEAR, nv.NgayVaoLam, GETDATE()) AS NamKinhNghiem,
    nv.NgayNghiViec,
    
    -- Thông tin lương
    nv.LuongCoBan,
    nv.PhuCap,
    nv.HeSoLuong,
    nv.LuongCoBan + nv.PhuCap AS TongLuong,
    
    -- Thông tin ngân hàng
    nv.SoTaiKhoan,
    nv.NganHang,
    
    -- Trạng thái
    nv.TrangThai,
    nv.SoNgayPhep,
    
    -- ✅ Thông tin hợp đồng (SỬA TÊN CỘT)
    hd.MaHD,
    hd.SoHopDong,
    hd.LoaiHopDong,
    hd.NgayKy,
    hd.NgayHieuLuc,         -- ✅ ĐÚNG: NgayHieuLuc (không phải NgayBatDau)
    hd.NgayHetHan,          -- ✅ ĐÚNG: NgayHetHan (không phải NgayKetThuc)
    hd.LuongHopDong,
    hd.TrangThai AS TrangThaiHopDong,
    CASE 
        WHEN hd.NgayHetHan IS NULL THEN N'Không xác định thời hạn'
        WHEN DATEDIFF(DAY, GETDATE(), hd.NgayHetHan) < 0 THEN N'Đã hết hạn'
        WHEN DATEDIFF(DAY, GETDATE(), hd.NgayHetHan) <= 30 THEN N'Sắp hết hạn'
        ELSE N'Còn hiệu lực'
    END AS CanhBaoHopDong,
    
    -- Metadata
    nv.NgayTao,
    nv.NguoiTao,
    nv.NgayCapNhat,
    nv.NguoiCapNhat
    
FROM NhanVien nv
INNER JOIN PhongBan pb ON nv.MaPB = pb.MaPB
INNER JOIN ChucVu cv ON nv.MaCV = cv.MaCV
LEFT JOIN HopDong hd ON nv.MaNV = hd.MaNV 
    AND hd.TrangThai = N'Có hiệu lực';  -- ✅ ĐÚNG: 'Có hiệu lực' (không phải 'Đang hiệu lực')

GO

PRINT N'✓ Đã tạo view vw_ThongTinNhanVien thành công!';
GO

-- =============================================
-- KIỂM TRA VIEW
-- =============================================
PRINT N'';
PRINT N'Đang kiểm tra view...';
GO

-- Test query - Lấy tất cả nhân viên đang làm
SELECT 
    MaNV,
    HoTen,
    PhongBan,
    ChucVu,
    NgayVaoLam,
    TongLuong,
    TrangThai,
    LoaiHopDong,
    NgayHieuLuc,
    NgayHetHan,
    TrangThaiHopDong
FROM vw_ThongTinNhanVien
WHERE TrangThai = N'Đang làm việc';

GO

-- Thống kê theo phòng ban
PRINT N'';
PRINT N'Thống kê theo phòng ban:';
SELECT 
    PhongBan,
    COUNT(*) AS SoNhanVien,
    AVG(TongLuong) AS LuongTrungBinh
FROM vw_ThongTinNhanVien
WHERE TrangThai = N'Đang làm việc'
GROUP BY PhongBan
ORDER BY SoNhanVien DESC;

GO

PRINT N'';
PRINT N'================================================';
PRINT N'  ✓ HOÀN THÀNH TẠO VIEW (FIXED)';
PRINT N'================================================';
PRINT N'';
PRINT N'✅ View name: vw_ThongTinNhanVien';
PRINT N'✅ Đã sửa lỗi tên cột HopDong';
PRINT N'✅ Sử dụng trong: frmBaoCao.cs';
PRINT N'✅ File RDLC: Rpt_ThongTinCaNhan.rdlc';
PRINT N'';

-- =============================================
-- STORED PROCEDURE HỖ TRỢ
-- =============================================

-- Xóa SP cũ nếu có
IF OBJECT_ID('sp_GetThongTinNhanVien', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetThongTinNhanVien;
GO

-- Tạo SP mới
CREATE PROCEDURE sp_GetThongTinNhanVien
    @MaNV INT
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT *
    FROM vw_ThongTinNhanVien
    WHERE MaNV = @MaNV;
END
GO

PRINT N'✓ Đã tạo stored procedure sp_GetThongTinNhanVien';
GO

-- Test stored procedure với MaNV = 1
PRINT N'';
PRINT N'Test stored procedure với MaNV = 1:';
EXEC sp_GetThongTinNhanVien @MaNV = 1;
GO

PRINT N'';
PRINT N'✅✅✅ TẤT CẢ ĐÃ HOÀN THÀNH! ✅✅✅';
GO