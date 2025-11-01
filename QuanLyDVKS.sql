/* Tạo mới DB */
IF DB_ID(N'QuanLyDVKS') IS NOT NULL
BEGIN
    ALTER DATABASE QuanLyDVKS SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE QuanLyDVKS;
END
GO
CREATE DATABASE QuanLyDVKS;
GO
USE QuanLyDVKS;
GO

/* Bảng KhachHang */
CREATE TABLE dbo.KhachHang
(
    id            INT IDENTITY(1,1) PRIMARY KEY,
    TenKH         NVARCHAR(50)  NOT NULL,
    CMND_CCCD     NVARCHAR(12)  NOT NULL UNIQUE,
    DiaChi        NVARCHAR(100) NULL,
    SDT           NVARCHAR(20)  NULL,
    LoaiKH        NVARCHAR(20)  NOT NULL,
    GhiChu        NVARCHAR(100) NULL
);
GO

/* Bảng Phong */
CREATE TABLE dbo.Phong
(
    id       INT IDENTITY(1,1) PRIMARY KEY,
    TenPhong NVARCHAR(50)  NOT NULL UNIQUE,
    MaKH     INT           NULL
        CONSTRAINT FK_Phong_KhachHang
        REFERENCES dbo.KhachHang(id) ON DELETE SET NULL,
    GhiChu   NVARCHAR(100) NULL,
    status   NVARCHAR(20)  NOT NULL DEFAULT N'Trống'
);
GO

/* Bảng HoaDon (tiền dùng decimal) */
CREATE TABLE dbo.HoaDon
(
    id              INT IDENTITY(1,1) PRIMARY KEY,
    NgayLapHD       DATETIME       NOT NULL DEFAULT GETDATE(),
    NgayKetThucHD   DATETIME       NULL,
    MaPhong         INT            NOT NULL
        CONSTRAINT FK_HoaDon_Phong
        REFERENCES dbo.Phong(id),
    TongTien        DECIMAL(18,2)  NOT NULL DEFAULT(0),
    GiamGia         DECIMAL(5,2)   NOT NULL DEFAULT(0),   -- %
    -- ThanhTien tính từ TongTien và GiamGia
    ThanhTien AS (CONVERT(DECIMAL(18,2), CASE WHEN GiamGia >= 0
                     THEN (TongTien * (1 - (GiamGia/100.0)))
                     ELSE TongTien END)) PERSISTED,
    status          TINYINT        NOT NULL DEFAULT(0)    -- 1: đã TT, 0: chưa TT
);
GO

/* Loại dịch vụ */
CREATE TABLE dbo.LoaiDichVu
(
    id   INT IDENTITY(1,1) PRIMARY KEY,
    name NVARCHAR(100) NOT NULL UNIQUE
);
GO

/* Danh sách dịch vụ */
CREATE TABLE dbo.DSDichVu
(
    id            INT IDENTITY(1,1) PRIMARY KEY,
    TenDV         NVARCHAR(70)  NOT NULL,
    idLoaiDichVu  INT           NOT NULL
        CONSTRAINT FK_DSDichVu_Loai
        REFERENCES dbo.LoaiDichVu(id),
    DonGia        DECIMAL(18,2) NOT NULL,
    DVT           NVARCHAR(20)  NOT NULL,
    LuuY          NVARCHAR(100) NULL,
    CONSTRAINT UQ_DSDichVu UNIQUE(TenDV, idLoaiDichVu)
);
GO

/* Chi tiết hóa đơn: lưu đơn giá snapshot, TT là cột tính */
CREATE TABLE dbo.ChiTietHD
(
    id        INT IDENTITY(1,1) PRIMARY KEY,
    MaHD      INT           NOT NULL
        CONSTRAINT FK_CTHD_HD
        REFERENCES dbo.HoaDon(id) ON DELETE CASCADE,
    MaDV      INT           NOT NULL
        CONSTRAINT FK_CTHD_DV
        REFERENCES dbo.DSDichVu(id),
    SoLuong   INT           NOT NULL DEFAULT(1) CHECK (SoLuong > 0),
    DonGia    DECIMAL(18,2) NOT NULL, -- snapshot tại thời điểm thêm
    ThanhTien AS (CONVERT(DECIMAL(18,2), SoLuong * DonGia)) PERSISTED,
    GhiChu    NVARCHAR(100) NULL,
    CONSTRAINT UQ_CTHD UNIQUE(MaHD, MaDV)
);
GO

/* Bảng tài khoản (đơn giản, demo – khuyến nghị hash/salt trong thực tế) */
CREATE TABLE dbo.TaiKhoan
(
    MaTaiKhoan  NVARCHAR(20)  PRIMARY KEY,     -- mã đăng nhập
    TenTaiKhoan NVARCHAR(70)  NOT NULL UNIQUE, -- tên hiển thị/đăng nhập
    PassWord    NVARCHAR(256) NOT NULL,        -- demo: plaintext
    [Type]      TINYINT       NOT NULL DEFAULT(0)   -- 1: admin, 0: staff
);
GO

/* Seed dữ liệu tối thiểu để FK không lỗi */
INSERT dbo.KhachHang(TenKH, CMND_CCCD, DiaChi, SDT, LoaiKH, GhiChu)
VALUES (N'Nguyễn Văn A', N'012345678901', N'Hà Nội', N'0912345678', N'Khách lẻ', NULL),
       (N'Trần Thị B',   N'012345678902', N'HCM',    N'0912345679', N'Khách đoàn', NULL);

INSERT dbo.Phong(TenPhong, MaKH, GhiChu, status)
VALUES (N'Phòng 101', 1, NULL, N'Trống'),
       (N'Phòng 102', 2, NULL, N'Trống');
GO

INSERT dbo.TaiKhoan (MaTaiKhoan, TenTaiKhoan, PassWord, [Type])
VALUES (N'dttdiep',  N'TIEU DIEP',  N'123', 1),
       (N'datanh',   N'THUY ANH',   N'123', 1),
       (N'pttquyen', N'THAO QUYEN', N'123', 0),
       (N'btthuy',   N'THI THUY',   N'123', 0),
	   (N'admin',   N'ADMIN',   N'123456789', 1);
GO

/* Thủ tục đăng nhập (demo) */
IF OBJECT_ID('dbo.USP_DangNhap') IS NOT NULL DROP PROC dbo.USP_DangNhap;
GO
CREATE PROC dbo.USP_DangNhap
    @TenTaiKhoan NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan;
END
GO

IF OBJECT_ID('dbo.USP_Login') IS NOT NULL DROP PROC dbo.USP_Login;
GO
CREATE PROC dbo.USP_Login
    @TenTaiKhoan NVARCHAR(100),
    @password    NVARCHAR(100)
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan AND PassWord = @password;
END
GO

/* View/Proc tiện ích */
IF OBJECT_ID('dbo.USP_XemPhong') IS NOT NULL DROP PROC dbo.USP_XemPhong;
GO
CREATE PROC dbo.USP_XemPhong
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.Phong;
END
GO

/* Seed loại và dịch vụ */
INSERT dbo.LoaiDichVu(name)
VALUES (N'Ăn uống'), (N'Đi lại'), (N'Giặt là'), (N'Spa'), (N'Giải trí');

INSERT dbo.DSDichVu(TenDV, idLoaiDichVu, DonGia, DVT, LuuY)
VALUES
    (N'Nhà hàng',               1, 30000, N'Lần',  NULL),
    (N'Quầy bar',               1, 20000, N'Lần',  NULL),
    (N'Massage - bấm huyệt',    4, 120000, N'Lần', NULL),
    (N'Chăm sóc da mặt',        4, 50000,  N'Lần', NULL),
    (N'Tắm trắng - tắm dưỡng',  4, 10000,  N'Lần', NULL),
    (N'Xông hơi',               4, 10000,  N'Lần', NULL),
    (N'Đưa đón sân bay',        2, 100000, N'Chuyến', NULL),
    (N'Thuê xe tự lái',         2, 100000, N'Ngày',   NULL),
    (N'Giặt khô',               3, 10000,  N'Cái',  NULL),
    (N'Giặt ướt',               3, 10000,  N'Cái',  NULL),
    (N'Sân golf và tennis',     5, 90000,  N'Ngày', NULL),
    (N'Phòng thể hình',         5, 70000,  N'Ngày', NULL),
    (N'Khu vui chơi phức hợp',  5, 70000,  N'Ngày', NULL),
    (N'Phòng karaoke',          5, 90000,  N'Giờ',  NULL);
GO

/* Tạo hóa đơn (chưa thanh toán) đúng cột */
INSERT dbo.HoaDon(NgayLapHD, NgayKetThucHD, MaPhong, TongTien, GiamGia, status)
VALUES (GETDATE(), NULL, 1, 0, 0, 0),
       (GETDATE(), NULL, 2, 0, 0, 0);
GO

/* Proc xuất dữ liệu hóa đơn/phòng */
IF OBJECT_ID('dbo.USP_XuatHoaDonTheoPhong') IS NOT NULL DROP PROC dbo.USP_XuatHoaDonTheoPhong;
GO
CREATE PROC dbo.USP_XuatHoaDonTheoPhong
    @phongID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT TOP 1 * FROM dbo.HoaDon WHERE MaPhong = @phongID AND status = 0 ORDER BY id DESC;
END
GO

IF OBJECT_ID('dbo.USP_XuatChiTietHD_HoaDon') IS NOT NULL DROP PROC dbo.USP_XuatChiTietHD_HoaDon;
GO
CREATE PROC dbo.USP_XuatChiTietHD_HoaDon
    @billID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT cthd.*, dv.TenDV, dv.DVT
    FROM dbo.ChiTietHD cthd
    JOIN dbo.DSDichVu dv ON dv.id = cthd.MaDV
    WHERE cthd.MaHD = @billID;
END
GO

IF OBJECT_ID('dbo.USP_XuatDS_IDHoaDon') IS NOT NULL DROP PROC dbo.USP_XuatDS_IDHoaDon;
GO
CREATE PROC dbo.USP_XuatDS_IDHoaDon
    @billID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT dv.TenDV, cthd.DonGia, dv.DVT, cthd.SoLuong, cthd.ThanhTien
    FROM dbo.ChiTietHD cthd
    JOIN dbo.DSDichVu dv ON dv.id = cthd.MaDV
    WHERE cthd.MaHD = @billID;
END
GO

IF OBJECT_ID('dbo.USP_XuatLoaiDichVu') IS NOT NULL DROP PROC dbo.USP_XuatLoaiDichVu;
GO
CREATE PROC dbo.USP_XuatLoaiDichVu
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.LoaiDichVu;
END
GO

IF OBJECT_ID('dbo.USP_XuatDichVuTheoLoai') IS NOT NULL DROP PROC dbo.USP_XuatDichVuTheoLoai;
GO
CREATE PROC dbo.USP_XuatDichVuTheoLoai
    @ID INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM dbo.DSDichVu WHERE idLoaiDichVu = @ID;
END
GO

/* Thêm hóa đơn nếu phòng chưa có hóa đơn mở */
IF OBJECT_ID('dbo.USP_ThemHoaDon') IS NOT NULL DROP PROC dbo.USP_ThemHoaDon;
GO
CREATE PROC dbo.USP_ThemHoaDon
    @idPhong INT
AS
BEGIN
    SET NOCOUNT ON;
    IF (NOT EXISTS(SELECT 1 FROM dbo.HoaDon WHERE MaPhong = @idPhong AND status = 0))
    BEGIN
        INSERT dbo.HoaDon(NgayLapHD, NgayKetThucHD, MaPhong, TongTien, GiamGia, status)
        VALUES (GETDATE(), NULL, @idPhong, 0, 0, 0);
    END
    SELECT TOP 1 * FROM dbo.HoaDon WHERE MaPhong = @idPhong AND status = 0 ORDER BY id DESC;
END
GO

/* Proc thêm/cập nhật chi tiết hóa đơn: tự tính DonGia/ThanhTien, upsert */
IF OBJECT_ID('dbo.USP_InsertChiTietHD') IS NOT NULL DROP PROC dbo.USP_InsertChiTietHD;
GO
CREATE PROC dbo.USP_InsertChiTietHD
    @idHoaDon INT, @idDichVu INT, @soLuong INT, @ghiChu NVARCHAR(100) = NULL
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @donGia DECIMAL(18,2);
    SELECT @donGia = DonGia FROM dbo.DSDichVu WHERE id = @idDichVu;

    IF EXISTS (SELECT 1 FROM dbo.ChiTietHD WHERE MaHD = @idHoaDon AND MaDV = @idDichVu)
    BEGIN
        DECLARE @newCount INT = (SELECT SoLuong FROM dbo.ChiTietHD WHERE MaHD = @idHoaDon AND MaDV = @idDichVu) + @soLuong;

        IF (@newCount > 0)
            UPDATE dbo.ChiTietHD
            SET SoLuong = @newCount,
                GhiChu = ISNULL(@ghiChu, GhiChu)
            WHERE MaHD = @idHoaDon AND MaDV = @idDichVu;
        ELSE
            DELETE dbo.ChiTietHD WHERE MaHD = @idHoaDon AND MaDV = @idDichVu;
    END
    ELSE
    BEGIN
        INSERT dbo.ChiTietHD(MaHD, MaDV, SoLuong, DonGia, GhiChu)
        VALUES (@idHoaDon, @idDichVu, @soLuong, @donGia, @ghiChu);
    END

    -- Cập nhật tổng tiền hóa đơn theo chi tiết
    UPDATE hd
    SET TongTien = ISNULL(x.TongCT, 0)
    FROM dbo.HoaDon hd
    CROSS APPLY (
        SELECT SUM(ThanhTien) AS TongCT
        FROM dbo.ChiTietHD
        WHERE MaHD = hd.id
    ) x
    WHERE hd.id = @idHoaDon AND hd.status = 0;
END
GO

/* Hoàn tất hóa đơn: set status = 1, set ngày kết thúc, cập nhật giảm giá */
IF OBJECT_ID('dbo.USP_DienHoaDon') IS NOT NULL DROP PROC dbo.USP_DienHoaDon;
GO
CREATE PROC dbo.USP_DienHoaDon
    @billID INT, @giamgia DECIMAL(5,2)
AS
BEGIN
    SET NOCOUNT ON;

    -- làm tròn tổng chi tiết trước khi kết
    UPDATE hd
    SET TongTien = ISNULL(x.TongCT, 0)
    FROM dbo.HoaDon hd
    CROSS APPLY (
        SELECT SUM(ThanhTien) AS TongCT
        FROM dbo.ChiTietHD
        WHERE MaHD = hd.id
    ) x
    WHERE hd.id = @billID;

    UPDATE dbo.HoaDon
    SET status = 1,
        GiamGia = @giamgia,
        NgayKetThucHD = GETDATE()
    WHERE id = @billID;
END
GO

/* Trigger: cập nhật trạng thái phòng khi có chi tiết DV (hóa đơn mở) */
IF OBJECT_ID('dbo.UTG_UpdateChiTietHD') IS NOT NULL DROP TRIGGER dbo.UTG_UpdateChiTietHD;
GO
CREATE TRIGGER dbo.UTG_UpdateChiTietHD
ON dbo.ChiTietHD
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @AffectedHD TABLE(id INT);
    INSERT @AffectedHD(id)
    SELECT DISTINCT COALESCE(i.MaHD, d.MaHD)
    FROM inserted i
    FULL JOIN deleted d ON 1 = 0;

    -- Cập nhật tổng tiền hóa đơn (đã có trong proc, thêm đảm bảo khi update trực tiếp)
    UPDATE hd
    SET TongTien = ISNULL(x.TongCT, 0)
    FROM dbo.HoaDon hd
    JOIN @AffectedHD a ON a.id = hd.id
    CROSS APPLY (
        SELECT SUM(ThanhTien) AS TongCT
        FROM dbo.ChiTietHD
        WHERE MaHD = hd.id
    ) x;

    -- cập nhật trạng thái phòng: nếu có CTHD và HĐ mở -> 'Có dịch vụ'
    UPDATE p
    SET status = N'Có dịch vụ'
    FROM dbo.Phong p
    JOIN dbo.HoaDon hd ON hd.MaPhong = p.id AND hd.status = 0
    WHERE EXISTS (SELECT 1 FROM dbo.ChiTietHD c WHERE c.MaHD = hd.id);
END
GO

/* Trigger: khi hóa đơn đóng, nếu không còn HĐ mở thì trả phòng về 'Trống' */
IF OBJECT_ID('dbo.UTG_UpdateHoaDon') IS NOT NULL DROP TRIGGER dbo.UTG_UpdateHoaDon;
GO
CREATE TRIGGER dbo.UTG_UpdateHoaDon
ON dbo.HoaDon
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    ;WITH A AS (
        SELECT DISTINCT i.id, i.MaPhong
        FROM inserted i
    )
    UPDATE p
    SET status = N'Trống'
    FROM dbo.Phong p
    JOIN A ON A.MaPhong = p.id
    WHERE NOT EXISTS (SELECT 1 FROM dbo.HoaDon h WHERE h.MaPhong = p.id AND h.status = 0);
END
GO

/* Demo thêm chi tiết bằng proc mới */
DECLARE @Bill1 INT = (SELECT TOP 1 id FROM dbo.HoaDon WHERE MaPhong = 1 AND status = 0 ORDER BY id DESC);
EXEC dbo.USP_InsertChiTietHD @Bill1, 1, 2, N'Gọi 2 suất Nhà hàng';
EXEC dbo.USP_InsertChiTietHD @Bill1, 2, 1, N'Quầy bar';
GO

/* Kiểm tra */
SELECT * FROM dbo.TaiKhoan;
SELECT * FROM dbo.KhachHang;
SELECT * FROM dbo.Phong;
SELECT * FROM dbo.LoaiDichVu;
SELECT * FROM dbo.DSDichVu;
SELECT * FROM dbo.HoaDon;
SELECT * FROM dbo.ChiTietHD;
GO