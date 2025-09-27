CREATE DATABASE QuanLyDVKS
go
use QuanLyDVKS
go

create table KhachHang
(id INT IDENTITY PRIMARY KEY,
TenKH nvarchar(50) not null,
CMND_CCCD nvarchar(12) not null,
DiaChi  nvarchar(100),
SDT  nvarchar(100),
LoaiKH nvarchar(20) not null,
GhiChu nvarchar(100) ) 
go

create table Phong
(id INT IDENTITY PRIMARY KEY,
TenPhong nvarchar(50),
MaKH int references KhachHang (ID),
GhiChu nvarchar(100) ,
status NVARCHAR(100) NOT NULL DEFAULT N'Trống')

go
create table HoaDon
(id INT IDENTITY PRIMARY KEY,
NgayLapHD DateTime NOT NULL DEFAULT GETDATE(),
NgayKetThucHD DateTime ,
MaPhong int references Phong (id),
TongTien float DEFAULT 0 ,
GiamGia float DEFAULT 0,
ThanhTien float DEFAULT 0,
status INT NOT NULL DEFAULT 0,-- 1: đã thanh toán && 0: chưa thanh toán
) 
go

CREATE TABLE LoaiDichVu
(
	id INT IDENTITY PRIMARY KEY,
	name NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên'
)
GO

create table DSDichVu
(
id INT IDENTITY PRIMARY KEY,
TenDV nvarchar(70) not null,
idLoaiDichVu INT references LoaiDichVu (id),
DonGia float not null,
DVT nvarchar(20)not null,
LuuY nvarchar(100)
)
go

create table ChiTietHD
(
id INT IDENTITY PRIMARY KEY,
MaHD int references HoaDon(id),
MaDV int references DSDichVu(id),
SoLuong int ,
ThanhTien float ,
GhiChu nvarchar(100),
)
go

 create table DoanhThu
(id INT IDENTITY PRIMARY KEY,
MaHD int references HoaDon(id),
TenDV nvarchar(70) not null,
SoLuong int not null,
DVT nvarchar(20)not null,
ThanhTien float not null,
)
go 
create table TaiKhoan
(
MaTaiKhoan char(6) primary key,
TenTaiKhoan nvarchar(70) not null,
PassWord NVARCHAR(1000) NOT NULL,
Type INT NOT NULL DEFAULT 0	-- 1: admin && 0: staff
)
go

INSERT INTO dbo.TaiKhoan (MaTaiKhoan, TenTaiKhoan, PassWord, Type)
VALUES 
	(N'dttdiep', N'TIEU DIEP', N'123', 1),
	(N'datanh', N'THUY ANH', N'123', 1),
	(N'pttquyen', N'THAO QUYEN', N'123', 0),
	(N'btthuy', N'THI THUY', N'123', 0)
SELECT * FROM dbo.TaiKhoan
GO

CREATE PROC USP_DangNhap
@TenTaiKhoan NVARCHAR(100)
AS
BEGIN
	SELECT * FROM dbo.TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan
END
GO

EXEC USP_DangNhap @TenTaiKhoan = N'THUY ANH'
GO

CREATE PROC USP_Login
@TenTaiKhoan NVARCHAR(100),
@password NVARCHAR(100)
AS
BEGIN
	SELECT * FROM dbo.TaiKhoan WHERE TenTaiKhoan = @TenTaiKhoan AND PassWord = @password
END
GO

EXEC USP_Login @TenTaiKhoan = N'THUY ANH', @password = N'123'

SELECT * FROM Phong
GO

CREATE PROC USP_XemPhong
AS SELECT * FROM Phong
GO

EXEC USP_XemPhong
GO

INSERT LoaiDichVu(name) 
VALUES
	(N'Ăn uống'),
	(N'Đi lại'),
	(N'Giặt là'),
	(N'Spa'),
	(N'GiaiTri')

INSERT DSDichVu(TenDV, idLoaiDichVu, DonGia,DVT,LuuY)
VALUES
	(N'Nhà hàng', 1, 30000,N'Lan',null),
	(N'Quầy bar ', 1, 20000,N'Lan',null),
	(N'Massage - bấm huyệt', 2, 120000,N'Lan',null),
	(N'Chăm sóc da mặt', 2, 50000,N'Lan',null),
	(N'Tắm trắng- tắm dưỡng', 2, 10000,N'Lan',null),
	(N'Xông hơi', 2, 10000,N'Lan',null),
	(N'Đưa đón sân bay', 3, 10000,N'Lan',null),
	(N'Thuê xe tự lái', 3, 10000,N'Lan',null),
	(N'Giặt khô', 4, 10000,N'Cai',null),
	(N'Giặt ướt', 4, 10000,N'Cai',null),
	(N'Sân golf và tennis', 3, 90000,N'Ngay',null),
	(N'Phòng thể hình', 3, 70000,N'Ngay',null),
	(N'Khu vui chơi phức hợp', 4, 70000,N'Ngay',null),
	(N'Phòng karaoke', 4, 90000,N'Ngay',null)

INSERT HoaDon(NgayLapHD, NgayKetThucHD, MaPhong, TongTien,GiamGia,ThanhTien,status)
VALUES 
	(GETDATE(), NULL, 1, 0),
	(GETDATE(), NULL, 2, 0),
	(GETDATE(), GETDATE(), 2, 1)



INSERT ChiTietHD(MaHD, MaDV, SoLuong,ThanhTien,GhiChu)
VALUES 
	(1, 1, 2),
	(2, 3, 4),
	(3, 5, 1),
	(4, 1, 2),
	(5, 5, 3),
	(6, 1, 2),
	(7, 5, 1)

SELECT * FROM HoaDon
SELECT * FROM ChiTietHD
SELECT * FROM DSDichVu
SELECT * FROM LoaiDichVu
GO

CREATE PROC USP_XuatHoaDonTheoPhong
@phongID INT
AS
BEGIN
	SELECT * FROM HoaDon WHERE MaPhong = @phongID AND status = 0
END
GO

EXEC USP_XuatHoaDonTheoPhong 1
GO

CREATE PROC USP_XuatChiTietHD_HoaDon
@billID INT
AS
BEGIN
	SELECT * FROM ChiTietHD WHERE MaHD = @billID
END
GO

EXEC USP_XuatChiTietHD_HoaDon 2
GO

CREATE PROC USP_XuatDS_IDHoaDon
@billID INT
AS
BEGIN
	SELECT DSDichVu.TenDV ,DonGia ,DVT ,ChiTietHD.SoLuong 
	From DSDichVu, ChiTietHD,HoaDon
	WHERE ChiTietHD.MaHD = @billID and ChiTietHD.MaHD = HoaDon.id and ChiTietHD.MaDV = DSDichVu.id
END
GO

EXEC USP_XuatDS_IDHoaDon 2
GO

CREATE PROC USP_XuatLoaiDichVu
AS SELECT * FROM LoaiDichVu
GO

EXEC USP_XuatLoaiDichVu
GO

CREATE PROC USP_XuatDichVuTheoLoai
@ID INT
AS
BEGIN
	SELECT * FROM DSDichVu WHERE DSDichVu.idLoaiDichVu = @ID
END
GO

EXEC USP_XuatDichVuTheoLoai 3
GO

CREATE PROC USP_ThemHoaDon
@idPhong INT
AS
BEGIN
	IF (NOT EXISTS(SELECT * FROM HoaDon WHERE MaPhong = @idPhong AND status = 0))
	BEGIN
		INSERT HoaDon(NgayLapHD, NgayKetThucHD, MaPhong,TongTien,GiamGia,ThanhTien, status)
		VALUES (GETDATE(), NULL, @idPhong, 0, 0,0,0)
	END
END
GO

EXEC USP_ThemHoaDon 1
GO
--Them chi tiết hóa đơn
CREATE PROC USP_InsertChiTietHD
@idHoaDon INT, @idDichVu INT, @soluong INT, @thanhtien float, @ghichu nvarchar(100)
AS
BEGIN
	DECLARE @ChiTietHD INT
	DECLARE @soluongDV INT = 1

	SELECT @ChiTietHD = id, @soluongDV = SoLuong 
	FROM ChiTietHD 
	WHERE MaHD = @idHoaDon AND MADV = @idDichVu

	IF (@ChiTietHD > 0)
	BEGIN
		DECLARE @newCount INT = @soluongDV + @soluong
		IF (@newCount > 0)
			UPDATE ChiTietHD
			SET SoLuong = @newCount
			WHERE MaHD = @idHoaDon AND MADV = @idDichVu
		ELSE
			DELETE ChiTietHD WHERE MaHD = @idHoaDon AND MADV = @idDichVu
	END
	ELSE
	BEGIN
		INSERT ChiTietHD(MaHD, MADV, SoLuong,ThanhTien,GhiChu)
		VALUES (@idHoaDon , @idDichVu , @soluong , @thanhtien , @ghichu)
	END
END
GO

EXEC USP_InsertChiTietHD 1, 1, 1,1, N'da co'
GO

CREATE PROC USP_DienHoaDon
@billID INT, @tongtien float, @giamgia float
AS
BEGIN
	UPDATE HoaDon
	SET status = 1, GiamGia = @giamgia, NgayKetThucHD = GETDATE(),TongTien = @tongtien
	WHERE id = @billID
END
GO

EXEC USP_DienHoaDon 1,1,1
GO


-- Khi thêm mới hoặc cập nhật ChiTietHDt hì thay đổi trạng thái của phong
CREATE TRIGGER UTG_UpdateChiTietHD
ON dbo.ChiTietHD FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idHD INT

	SELECT @idHD = MaHD FROM inserted

	DECLARE @idPhong INT

	SELECT @idPhong = MaPhong FROM HoaDon WHERE id = @idHD AND status = 0

	UPDATE Phong SET status = N'Có dich vụ' WHERE id = @idPhong
END
GO	

CREATE TRIGGER UTG_UpdateHoaDon
ON dbo.HoaDon FOR UPDATE
AS
BEGIN
	DECLARE @idHoaDon INT
	
	SELECT @idHoaDon = id FROM inserted

	DECLARE @idPhong INT

	SELECT @idPhong = MaPhong FROM HoaDon WHERE id = @idHoaDon

	DECLARE @count INT = 0

	SELECT @count = COUNT(*) FROM HoaDon WHERE MaPhong = @idPhong AND status = 0

	IF (@count = 0)
		UPDATE Phong SET status = N'Trống' WHERE id = @idPhong
END
GO 

SELECT * FROM HoaDon

CREATE PROC USP_XuatDSHoaDon_Ngay
@ngaylap DateTime, @ngayketthuc DateTime
AS
BEGIN
	SELECT Phong.TenPhong AS [Tên phòng], NgayLapHD  AS [Ngày vào], NgayKetThucHD  AS [Ngày ra], GiamGia  AS [Giảm giá], TongTien  AS [Tổng tiền]
	FROM HoaDon
	JOIN Phong ON HoaDon.MaPhong = Phong.id
	WHERE HoaDon.status = 1 AND HoaDon.NgayLapHD >= @ngaylap AND HoaDon.NgayKetThucHD <= @ngayketthuc
END
GO





