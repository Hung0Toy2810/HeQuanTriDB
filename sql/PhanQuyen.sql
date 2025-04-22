luongnguyenthanhhung@Luongs-MacBook-Pro HeQuanTriDB % dotnet ef database update
Build started...
Build succeeded.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (302ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      CREATE DATABASE [RestaurantManagement];
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (133ms) [Parameters=[], CommandType='Text', CommandTimeout='60']
      IF SERVERPROPERTY('EngineEdition') <> 5
      BEGIN
          ALTER DATABASE [RestaurantManagement] SET READ_COMMITTED_SNAPSHOT ON;
      END;
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Migrations[20411]
      Acquiring an exclusive lock for migration application. See https://aka.ms/efcore-docs-migrations-lock for more information if this takes too long.
Acquiring an exclusive lock for migration application. See https://aka.ms/efcore-docs-migrations-lock for more information if this takes too long.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (17ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      DECLARE @result int;
      EXEC @result = sp_getapplock @Resource = '__EFMigrationsLock', @LockOwner = 'Session', @LockMode = 'Exclusive';
      SELECT @result
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (9ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
      BEGIN
          CREATE TABLE [__EFMigrationsHistory] (
              [MigrationId] nvarchar(150) NOT NULL,
              [ProductVersion] nvarchar(32) NOT NULL,
              CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
          );
      END;
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT 1
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT OBJECT_ID(N'[__EFMigrationsHistory]');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      SELECT [MigrationId], [ProductVersion]
      FROM [__EFMigrationsHistory]
      ORDER BY [MigrationId];
info: Microsoft.EntityFrameworkCore.Migrations[20402]
      Applying migration '20250324210711_InitialCreate'.
Applying migration '20250324210711_InitialCreate'.
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [ChucVus] (
          [MaChucVu] int NOT NULL IDENTITY,
          [TenChucVu] nvarchar(100) NOT NULL,
          CONSTRAINT [PK_ChucVus] PRIMARY KEY ([MaChucVu])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [KhachHangs] (
          [MaKhachHang] int NOT NULL IDENTITY,
          [TenKhachHang] nvarchar(100) NOT NULL,
          [DiaChi] nvarchar(max) NULL,
          [SoDienThoai] nvarchar(20) NOT NULL,
          CONSTRAINT [PK_KhachHangs] PRIMARY KEY ([MaKhachHang])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [MonAns] (
          [MaMonAn] int NOT NULL IDENTITY,
          [TenMonAn] nvarchar(100) NOT NULL,
          [Gia] float NOT NULL,
          [SoLuongHienCo] int NOT NULL,
          CONSTRAINT [PK_MonAns] PRIMARY KEY ([MaMonAn])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [NhaCungCaps] (
          [MaNhaCungCap] int NOT NULL IDENTITY,
          [TenNhaCungCap] nvarchar(100) NOT NULL,
          [DiaChi] nvarchar(100) NOT NULL,
          [SoDienThoai] nvarchar(20) NOT NULL,
          [Email] nvarchar(100) NOT NULL,
          CONSTRAINT [PK_NhaCungCaps] PRIMARY KEY ([MaNhaCungCap])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [NhanViens] (
          [MaNhanVien] int NOT NULL IDENTITY,
          [TenNhanVien] nvarchar(max) NOT NULL,
          [DiaChi] nvarchar(max) NOT NULL,
          [SoDienThoai] nvarchar(max) NOT NULL,
          [Email] nvarchar(max) NOT NULL,
          [MatKhau] nvarchar(max) NOT NULL,
          [MaChucVu] int NOT NULL,
          CONSTRAINT [PK_NhanViens] PRIMARY KEY ([MaNhanVien]),
          CONSTRAINT [FK_NhanViens_ChucVus_MaChucVu] FOREIGN KEY ([MaChucVu]) REFERENCES [ChucVus] ([MaChucVu])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [KhuyenMais] (
          [MaKhuyenMai] int NOT NULL IDENTITY,
          [TenKhuyenMai] nvarchar(100) NOT NULL,
          [MaKhachHang] int NOT NULL,
          [DaDung] bit NOT NULL,
          [NgayHetHan] datetime2 NOT NULL,
          CONSTRAINT [PK_KhuyenMais] PRIMARY KEY ([MaKhuyenMai]),
          CONSTRAINT [FK_KhuyenMais_KhachHangs_MaKhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [KhachHangs] ([MaKhachHang])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [NguyenLieus] (
          [MaNguyenLieu] int NOT NULL IDENTITY,
          [TenNguyenLieu] nvarchar(100) NOT NULL,
          [DonViTinh] nvarchar(20) NOT NULL,
          [Gia] float NOT NULL,
          [MaNhaCungCap] int NOT NULL,
          CONSTRAINT [PK_NguyenLieus] PRIMARY KEY ([MaNguyenLieu]),
          CONSTRAINT [FK_NguyenLieus_NhaCungCaps_MaNhaCungCap] FOREIGN KEY ([MaNhaCungCap]) REFERENCES [NhaCungCaps] ([MaNhaCungCap])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [HoaDons] (
          [MaHoaDon] int NOT NULL IDENTITY,
          [MaNhanVien] int NOT NULL,
          [MaKhachHang] int NOT NULL,
          [NgayLap] datetime2 NOT NULL,
          [TongTien] float NOT NULL,
          CONSTRAINT [PK_HoaDons] PRIMARY KEY ([MaHoaDon]),
          CONSTRAINT [FK_HoaDons_KhachHangs_MaKhachHang] FOREIGN KEY ([MaKhachHang]) REFERENCES [KhachHangs] ([MaKhachHang]),
          CONSTRAINT [FK_HoaDons_NhanViens_MaNhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES [NhanViens] ([MaNhanVien])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [LuuTrus] (
          [MaLuuTru] int NOT NULL IDENTITY,
          [MaNhanVien] int NOT NULL,
          [MaNguyenLieu] int NOT NULL,
          [SoLuong] int NOT NULL,
          [NgayHetHan] datetime2 NOT NULL,
          CONSTRAINT [PK_LuuTrus] PRIMARY KEY ([MaLuuTru]),
          CONSTRAINT [FK_LuuTrus_NguyenLieus_MaNguyenLieu] FOREIGN KEY ([MaNguyenLieu]) REFERENCES [NguyenLieus] ([MaNguyenLieu]),
          CONSTRAINT [FK_LuuTrus_NhanViens_MaNhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES [NhanViens] ([MaNhanVien])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (4ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [NhapKhos] (
          [MaNhapKho] int NOT NULL IDENTITY,
          [MaNhanVien] int NOT NULL,
          [MaNguyenLieu] int NOT NULL,
          [NgayNhap] datetime2 NOT NULL,
          [SoLuong] int NOT NULL,
          [TongTien] float NOT NULL,
          [SoNgayHetHan] int NOT NULL,
          CONSTRAINT [PK_NhapKhos] PRIMARY KEY ([MaNhapKho]),
          CONSTRAINT [FK_NhapKhos_NguyenLieus_MaNguyenLieu] FOREIGN KEY ([MaNguyenLieu]) REFERENCES [NguyenLieus] ([MaNguyenLieu]),
          CONSTRAINT [FK_NhapKhos_NhanViens_MaNhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES [NhanViens] ([MaNhanVien])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [ChiTietHoaDons] (
          [MaChiTietHoaDon] int NOT NULL IDENTITY,
          [MaHoaDon] int NOT NULL,
          [MaMonAn] int NOT NULL,
          [SoLuong] int NOT NULL,
          [ThanhTien] float NOT NULL,
          CONSTRAINT [PK_ChiTietHoaDons] PRIMARY KEY ([MaChiTietHoaDon]),
          CONSTRAINT [FK_ChiTietHoaDons_HoaDons_MaHoaDon] FOREIGN KEY ([MaHoaDon]) REFERENCES [HoaDons] ([MaHoaDon]),
          CONSTRAINT [FK_ChiTietHoaDons_MonAns_MaMonAn] FOREIGN KEY ([MaMonAn]) REFERENCES [MonAns] ([MaMonAn])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE TABLE [XuatKhos] (
          [MaXuatKho] int NOT NULL IDENTITY,
          [MaNhanVien] int NOT NULL,
          [MaNguyenLieu] int NOT NULL,
          [SoLuong] int NOT NULL,
          [NgayXuat] datetime2 NOT NULL,
          [NguyenNhanXuatKho] nvarchar(max) NOT NULL,
          [MaLuuTru] int NOT NULL,
          CONSTRAINT [PK_XuatKhos] PRIMARY KEY ([MaXuatKho]),
          CONSTRAINT [FK_XuatKhos_LuuTrus_MaLuuTru] FOREIGN KEY ([MaLuuTru]) REFERENCES [LuuTrus] ([MaLuuTru]),
          CONSTRAINT [FK_XuatKhos_NguyenLieus_MaNguyenLieu] FOREIGN KEY ([MaNguyenLieu]) REFERENCES [NguyenLieus] ([MaNguyenLieu]),
          CONSTRAINT [FK_XuatKhos_NhanViens_MaNhanVien] FOREIGN KEY ([MaNhanVien]) REFERENCES [NhanViens] ([MaNhanVien])
      );
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_ChiTietHoaDons_MaHoaDon] ON [ChiTietHoaDons] ([MaHoaDon]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_ChiTietHoaDons_MaMonAn] ON [ChiTietHoaDons] ([MaMonAn]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_HoaDons_MaKhachHang] ON [HoaDons] ([MaKhachHang]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_HoaDons_MaNhanVien] ON [HoaDons] ([MaNhanVien]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_KhuyenMais_MaKhachHang] ON [KhuyenMais] ([MaKhachHang]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_LuuTrus_MaNguyenLieu] ON [LuuTrus] ([MaNguyenLieu]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_LuuTrus_MaNhanVien] ON [LuuTrus] ([MaNhanVien]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_NguyenLieus_MaNhaCungCap] ON [NguyenLieus] ([MaNhaCungCap]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_NhanViens_MaChucVu] ON [NhanViens] ([MaChucVu]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_NhapKhos_MaNguyenLieu] ON [NhapKhos] ([MaNguyenLieu]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_NhapKhos_MaNhanVien] ON [NhapKhos] ([MaNhanVien]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_XuatKhos_MaLuuTru] ON [XuatKhos] ([MaLuuTru]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (1ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_XuatKhos_MaNguyenLieu] ON [XuatKhos] ([MaNguyenLieu]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (2ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      CREATE INDEX [IX_XuatKhos_MaNhanVien] ON [XuatKhos] ([MaNhanVien]);
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (17ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
      VALUES (N'20250324210711_InitialCreate', N'9.0.3');
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (3ms) [Parameters=[], CommandType='Text', CommandTimeout='30']
      DECLARE @result int;
      EXEC @result = sp_releaseapplock @Resource = '__EFMigrationsLock', @LockOwner = 'Session';
      SELECT @result
-- View -- 
-- View hiển thị hóa đơn
CREATE VIEW View_HoaDon AS
SELECT 
	hd.MaHoaDon,
	nv.TenNhanVien,
	kh.TenKhachHang,
	hd.NgayLap,
	hd.TongTien
FROM HoaDons hd
INNER JOIN NhanViens nv ON hd.MaNhanVien = nv.MaNhanVien
INNER JOIN KhachHangs kh ON hd.MaKhachHang = kh.MaKhachHang 

--View xem thông tin nhân viên
CREATE VIEW View_NhanVien AS
SELECT 
    nv.MaNhanVien,
    nv.TenNhanVien,
    nv.DiaChi,
    nv.SoDienThoai,
    nv.Email,
    cv.TenChucVu
FROM NhanViens nv
LEFT JOIN ChucVus cv ON nv.MaChucVu = cv.MaChucVu;

-- View hiển thị thông tin khuyến mãi
CREATE VIEW View_KhuyenMai AS
SELECT 
    km.MaKhuyenMai,
    km.TenKhuyenMai,
    kh.MaKhachHang,
    kh.TenKhachHang,
    km.DaDung,
    km.NgayHetHan,
    -- Trạng thái khuyến mãi hiển thị dễ hiểu
    CASE 
        WHEN km.DaDung = 1 THEN N'Đã dùng'
        WHEN km.NgayHetHan < GETDATE() THEN N'Hết hạn'
        ELSE N'Còn hạn'
    END AS TrangThai
FROM KhuyenMais km
JOIN KhachHangs kh ON km.MaKhachHang = kh.MaKhachHang;

-- View hiển thị danh mục nguyên liệu
CREATE VIEW View_NguyenLieu AS
SELECT 
    nl.MaNguyenLieu,
	nl.TenNguyenLieu,
	nl.DonViTinh,
	nl.Gia,
	ncc.TenNhaCungCap
FROM NguyenLieus nl
LEFT JOIN NhaCungCaps ncc ON nl.MaNhaCungCap = ncc.MaNhaCungCap;

-- View nhập kho
CREATE VIEW View_NhapKho AS
SELECT 
	nk.MaNhapKho,
	nv.TenNhanVien,
	nl.TenNguyenLieu,
	nl.Gia,
	nk.NgayNhap,
	nk.SoLuong,
	nk.TongTien,
	nk.SoNgayHetHan
FROM NhapKhos nk
INNER JOIN NhanViens nv ON nk.MaNhanVien = nv.MaNhanVien
INNER JOIN NguyenLieus nl ON nl.MaNguyenLieu = nk.MaNguyenLieu

-- View xuất kho
CREATE VIEW View_XuatKho AS
SELECT 
	nk.MaXuatKho,
	nv.TenNhanVien,
	nl.TenNguyenLieu,
	nk.NgayXuat,
	nk.SoLuong,
	nk.NguyenNhanXuatKho,
	nk.MaLuuTru
FROM XuatKhos nk
INNER JOIN NhanViens nv ON nk.MaNhanVien = nv.MaNhanVien
INNER JOIN NguyenLieus nl ON nl.MaNguyenLieu = nk.MaNguyenLieu

-- View Lưu Trữ
--view luu tru
CREATE VIEW View_LuuTru AS
SELECT 
	lt.MaLuuTru,
	lt.MaNguyenLieu,
	nv.TenNhanVien,
	nl.TenNguyenLieu,
	lt.NgayHetHan,
	lt.SoLuong
FROM LuuTrus lt
INNER JOIN NhanViens nv ON lt.MaNhanVien = nv.MaNhanVien
INNER JOIN NguyenLieus nl ON nl.MaNguyenLieu = lt.MaNguyenLieu

-------------------------------------------------------------------------------
-- Procedure--
-- Đổi Mật Khẩu
CREATE PROCEDURE proc_DoiMatKhau
	@TaiKhoan NVARCHAR(MAX),
	@MatKhauMoi NVARCHAR(MAX)
AS
BEGIN 
	UPDATE TaiKhoanNhanViens
	SET MatKhau = @MatKhauMoi
	WHERE TaiKhoan = @TaiKhoan
END

-- Lấy Mật Khẩu
CREATE PROCEDURE proc_LayMatKhau
	@TaiKhoan NVARCHAR(MAX)
AS
BEGIN
	SELECT MatKhau FROM TaiKhoanNhanViens WHERE TaiKhoan = @TaiKhoan
END

-- Đăng Nhập
CREATE PROCEDURE proc_DangNhap
	@TaiKhoan NVARCHAR(MAX),
	@MatKhau NVARCHAR(MAX)
AS
BEGIN
	SELECT * FROM TaiKhoanNhanViens WHERE TaiKhoan = @TaiKhoan AND MatKhau = @MatKhau
END

-- Lấy Thông Tin Nhân Viên
CREATE PROCEDURE proc_LayThongTin
AS
BEGIN
	SELECT *
	FROM TaiKhoanNhanViens tk
	INNER JOIN NhanViens nv ON tk.MaNhanVien = nv.MaNhanVien
	INNER JOIN ChucVus cv ON nv.MaChucVu = cv.MaChucVu
END

-- Đăng Ký
CREATE PROCEDURE proc_DangKy
    @MaNhanVien INT,
    @TaiKhoan NVARCHAR(MAX),
    @MatKhau NVARCHAR(MAX)
AS
BEGIN
    SET XACT_ABORT ON;

	IF EXISTS (SELECT 1 FROM TaiKhoanNhanViens WHERE TaiKhoan = @TaiKhoan)
    BEGIN
        RAISERROR('Tên tài khoản đã được sử dụng!', 16, 1);
        RETURN;
    END

    IF EXISTS (SELECT 1 FROM TaiKhoanNhanViens WHERE MaNhanVien = @MaNhanVien AND TaiKhoan IS NOT NULL)
    BEGIN
        RAISERROR('Nhân viên này đã có tài khoản!', 16, 1);
        RETURN;
    END

    UPDATE TaiKhoanNhanViens
    SET TaiKhoan = @TaiKhoan,
        MatKhau = @MatKhau
    WHERE MaNhanVien = @MaNhanVien;
END
GO

-- NhanViens
-- Cập nhật thông tin nhân viên
CREATE PROCEDURE proc_CapNhatThongTinNhanVien
    @MaNhanVien INT,
    @TenNhanVien NVARCHAR(100),
    @MaChucVu NVARCHAR(50),
    @SoDienThoai NVARCHAR(15),
    @DiaChi NVARCHAR(200),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE NhanViens
    SET 
        TenNhanVien = @TenNhanVien,
        MaChucVu = @MaChucVu,
        SoDienThoai = @SoDienThoai,
        DiaChi = @DiaChi,
        Email = @Email
    WHERE MaNhanVien = @MaNhanVien;
END

-- Thêm Nhân Viên
CREATE PROCEDURE proc_ThemNhanVien
    @TenNhanVien NVARCHAR(100),
    @MaChucVu NVARCHAR(50),
    @SoDienThoai NVARCHAR(15),
    @DiaChi NVARCHAR(200),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO NhanViens (TenNhanVien, MaChucVu, SoDienThoai, DiaChi, Email)
    VALUES (@TenNhanVien, @MaChucVu, @SoDienThoai, @DiaChi, @Email);

	DECLARE @MaNhanVienMoi INT = SCOPE_IDENTITY();

	INSERT INTO TaiKhoanNhanViens (MaNhanVien, TaiKhoan, MatKhau)
	VALUES (@MaNhanVienMoi, Null, Null);
END

-- Xóa Nhân Viên
CREATE PROCEDURE proc_XoaNhanVien
    @MaNhanVien INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM TaiKhoanNhanViens WHERE MaNhanVien = @MaNhanVien)
    BEGIN
        DELETE FROM TaiKhoanNhanViens WHERE MaNhanVien = @MaNhanVien;
    END

    DELETE FROM NhanViens WHERE MaNhanVien = @MaNhanVien;
END

-- NhaCungCaps
-- Thêm Nhà Cung Cấp
CREATE PROCEDURE proc_ThemNhaCungCap
    @TenNhaCungCap NVARCHAR(100),
    @SoDienThoai NVARCHAR(15),
    @DiaChi NVARCHAR(200),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO NhaCungCaps (TenNhaCungCap, SoDienThoai, DiaChi, Email)
    VALUES (@TenNhaCungCap, @SoDienThoai, @DiaChi, @Email);
END

-- Cập Nhật Thông Tin Nhà Cung Cấp
CREATE PROCEDURE proc_CapNhatThongTinNhaCungCap
    @MaNhaCungCap INT,
    @TenNhaCungCap NVARCHAR(100),
    @SoDienThoai NVARCHAR(15),
    @DiaChi NVARCHAR(200),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE NhaCungCaps
    SET 
        TenNhaCungCap = @TenNhaCungCap,
        SoDienThoai = @SoDienThoai,
        DiaChi = @DiaChi,
        Email = @Email
    WHERE MaNhaCungCap = @MaNhaCungCap;
END

-- Xóa Nhà Cung Cấp
CREATE PROCEDURE proc_XoaNhaCungCap
    @MaNhaCungCap INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM NguyenLieus WHERE MaNhaCungCap = @MaNhaCungCap)
    BEGIN
        DELETE FROM NguyenLieus WHERE MaNhaCungCap = @MaNhaCungCap;
    END

    DELETE FROM NhaCungCaps WHERE MaNhaCungCap = @MaNhaCungCap;
END

-- MonAns
-- Thêm Món Ăn
CREATE PROCEDURE proc_ThemMonAn
    @TenMonAn NVARCHAR(100),
    @Gia FLOAT,
    @SoLuong INT
AS
BEGIN
    INSERT INTO MonAns (TenMonAn, Gia, SoLuongHienCo)
    VALUES (@TenMonAn, @Gia, @SoLuong);
END

-- Cập Nhật Thông Tin Món Ăn
CREATE PROCEDURE proc_CapNhatThongTinMonAn
    @MaMonAn INT,
    @TenMonAn NVARCHAR(100),
    @Gia FLOAT,
    @SoLuong INT
AS
BEGIN
    UPDATE MonAns
    SET 
        TenMonAn = @TenMonAn,
		Gia = @Gia,
		SoLuongHienCo = @SoLuong
    WHERE MaMonAn = @MaMonAn;
END

-- Xóa Món Ăn
CREATE PROCEDURE proc_XoaMonAn
    @MaMonAn INT
AS
BEGIN
    DELETE FROM MonAns WHERE MaMonAn = @MaMonAn;
END

-- KhachHangs
-- Thêm Khách Hàng
CREATE PROCEDURE proc_ThemKhachHang
    @TenKhachHang NVARCHAR(100),
    @DiaChi NVARCHAR(100),
    @SoDienThoai NVARCHAR(100)
AS
BEGIN
    INSERT INTO KhachHangs (TenKhachHang, DiaChi, SoDienThoai)
    VALUES (@TenKhachHang, @DiaChi, @SoDienThoai);
END

-- Cập Nhật Thông Tin Khách Hàng
CREATE PROCEDURE proc_CapNhatThongTinKhachHang
    @MaKhachHang INT,
    @TenKhachHang NVARCHAR(100),
    @DiaChi NVARCHAR(100),
    @SoDienThoai NVARCHAR(100)
AS
BEGIN
    UPDATE KhachHangs
    SET 
        TenKhachHang = @TenKhachHang,
		DiaChi = @DiaChi,
		SoDienThoai = @SoDienThoai
    WHERE MaKhachHang = @MaKhachHang;
END

-- Xóa Khách Hàng
CREATE PROCEDURE proc_XoaKhachHang
    @MaKhachHang INT
AS
BEGIN
	IF EXISTS (SELECT 1 FROM KhuyenMais WHERE MaKhachHang = @MaKhachHang)
    BEGIN
        DELETE FROM KhuyenMais WHERE MaKhachHang = @MaKhachHang;
    END
	IF EXISTS (SELECT 1 FROM HoaDons WHERE MaKhachHang = @MaKhachHang)
    BEGIN
        DELETE FROM HoaDons WHERE MaKhachHang = @MaKhachHang;
    END

    DELETE FROM KhachHangs WHERE MaKhachHang = @MaKhachHang;
END

-- NguyenLieus
-- Cập Nhật Thông Tin Nguyên Liệu
CREATE PROCEDURE proc_CapNhatThongTinNguyenLieu
    @MaNguyenLieu INT,
    @TenNguyenLieu NVARCHAR(100),
    @DonViTinh NVARCHAR(50),
    @Gia FLOAT,
    @MaNhaCungCap INT
AS
BEGIN
    UPDATE NguyenLieus
    SET 
        TenNguyenLieu = @TenNguyenLieu,
        DonViTinh = @DonViTinh,
        Gia = @Gia,
        MaNhaCungCap = @MaNhaCungCap
    WHERE MaNguyenLieu = @MaNguyenLieu;
END

-- Thêm Nguyên Liệu
CREATE PROCEDURE proc_ThemNguyenLieu
    @TenNguyenLieu NVARCHAR(100),
    @DonViTinh NVARCHAR(50),
    @Gia FLOAT,
    @MaNhaCungCap INT
AS
BEGIN
    INSERT INTO NguyenLieus(TenNguyenLieu, DonViTinh, Gia, MaNhaCungCap)
    VALUES (@TenNguyenLieu, @DonViTinh, @Gia, @MaNhaCungCap);
END

-- Xóa Nguyên Liệu
CREATE PROCEDURE proc_XoaNguyenLieu
    @MaNguyenLieu INT
AS
BEGIN
    DELETE FROM NguyenLieus WHERE MaNguyenLieu = @MaNguyenLieu;
END

-- KhuyenMais
-- Cập Nhật Thông Tin Khuyến Mãi
CREATE PROCEDURE proc_CapNhatThongTinKhuyenMai
    @MaKhuyenMai INT,
    @TenKhuyenMai NVARCHAR(100),
    @DaDung BIT,
    @NgayHetHan DATETIME2(7),
    @MaKhachHang INT
AS
BEGIN
    UPDATE KhuyenMais
    SET 
        TenKhuyenMai = @TenKhuyenMai,
        DaDung = @DaDung,
        NgayHetHan = @NgayHetHan,
        MaKhachHang = @MaKhachHang
    WHERE MaKhuyenMai = @MaKhuyenMai;
END

-- Thêm Khuyến Mãi
CREATE PROCEDURE proc_ThemKhuyenMai
    @TenKhuyenMai NVARCHAR(100),
    @DaDung BIT,
    @NgayHetHan DATETIME2(7),
    @MaKhachHang INT
AS
BEGIN
    INSERT INTO KhuyenMais(TenKhuyenMai, DaDung, NgayHetHan, MaKhachHang)
    VALUES (@TenKhuyenMai, @DaDung, @NgayHetHan, @MaKhachHang);
END

-- Xóa Khuyến Mãi
CREATE PROCEDURE proc_XoaKhuyenMai
    @MaKhuyenMai INT
AS
BEGIN
    DELETE FROM KhuyenMais WHERE MaKhuyenMai = @MaKhuyenMai;
END

-- HoaDons
-- Cập Nhật Thông Tin Hóa Đơn
CREATE PROCEDURE proc_CapNhatThongTinHoaDon
    @MaNhanVien INT,
    @MaHoaDon INT,
    @MaKhachHang INT,
    @NgayLap DATETIME2(7)
AS
BEGIN
    UPDATE HoaDons
    SET 
        MaNhanVien = @MaNhanVien,
        MaKhachHang = @MaKhachHang,
        NgayLap = @NgayLap
    WHERE MaHoaDon = @MaHoaDon;
END

-- Thêm Hóa Đơn
CREATE PROCEDURE proc_ThemHoaDon
    @MaNhanVien INT,
    @MaKhachHang INT,
    @NgayLap DATETIME2(7)
AS
BEGIN
    INSERT INTO HoaDons (MaNhanVien, MaKhachHang, NgayLap, TongTien)
    VALUES (@MaNhanVien, @MaKhachHang, @NgayLap, 0);
END

DROP PROCEDURE proc_CapNhatThongTinHoaDon
DROP PROCEDURE proc_ThemHoaDon

-- Xóa Hơn Đơn
CREATE PROCEDURE proc_XoaHoaDon
    @MaHoaDon INT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM ChiTietHoaDons WHERE MaHoaDon = @MaHoaDon)
    BEGIN
        DELETE FROM ChiTietHoaDons WHERE MaHoaDon = @MaHoaDon;
    END

    DELETE FROM HoaDons WHERE MaHoaDon = @MaHoaDon;
END

-- ChiTietHoaDons
-- Lấy Chi Tiết Hóa Đơn 
CREATE PROCEDURE proc_LayCTHD_MaHoaDon
	@MaHoaDon INT
AS
BEGIN
	SELECT 
		cthd.MaChiTietHoaDon,
		cthd.MaHoaDon,
		ma.TenMonAn,
		cthd.SoLuong,
		ma.Gia,
		cthd.ThanhTien
	FROM ChiTietHoaDons cthd
	INNER JOIN MonAns ma ON cthd.MaMonAn = ma.MaMonAn
	WHERE cthd.MaHoaDon = @MaHoaDon
END

-- Cập Nhật Chi Tiết Hóa Đơn
CREATE PROCEDURE proc_CapNhatThongTinChiTietHoaDon
	@MaChiTietHoaDon INT,
    @SoLuong INT
AS
BEGIN
    UPDATE ChiTietHoaDons
    SET 
        SoLuong = @SoLuong
    WHERE MaChiTietHoaDon = @MaChiTietHoaDon;
END

-- Thêm Chi Tiết Hóa Đơn
CREATE PROCEDURE proc_ThemChiTietHoaDon
	@MaHoaDon INT,
    @MaMonAn INT,
    @SoLuong INT
AS
BEGIN
    INSERT INTO ChiTietHoaDons (MaHoaDon, MaMonAn, SoLuong, ThanhTien)
    VALUES (@MaHoaDon, @MaMonAn, @SoLuong, 0);
END

-- Xóa Chi Tiết Hóa Đơn
CREATE PROCEDURE proc_XoaChiTietHoaDon
    @MaChiTietHoaDon INT
AS
BEGIN
    DELETE FROM ChiTietHoaDons WHERE MaChiTietHoaDon = @MaChiTietHoaDon;
END

-- NhapKhos
-- Cập Nhật Thông Tin Nhập Kho
CREATE PROCEDURE proc_CapNhatThongTinNhapKho
	@MaNhapKho INT,
    @SoLuong INT
AS
BEGIN
    UPDATE NhapKhos
    SET 
        SoLuong = @SoLuong
    WHERE MaNhapKho = @MaNhapKho;
END

-- Thêm Nhập Kho
CREATE PROCEDURE proc_ThemNhapKho
	@MaNhanVien INT,
    @MaNguyenLieu INT,
    @SoLuong INT,
	@NgayNhap DATETIME2(7),
	@SoNgayHetHan INT
AS
BEGIN
    INSERT INTO NhapKhos(MaNhanVien, MaNguyenLieu, NgayNhap, SoLuong, TongTien, SoNgayHetHan)
    VALUES (@MaNhanVien, @MaNguyenLieu, @NgayNhap, @SoLuong, 0, @SoNgayHetHan);
END

-- Xóa Nhập Kho
CREATE PROCEDURE proc_XoaNhapKho
    @MaNhapKho INT
AS
BEGIN
    DELETE FROM NhapKhos WHERE MaNhapKho = @MaNhapKho;
END

-- XuatKhos
-- Cập Nhật Thông Tin Xuất Kho
CREATE PROCEDURE proc_CapNhatThongTinXuatKho
	@MaXuatKho INT,
    @SoLuong INT,
	@NguyenNhan NVARCHAR(MAX)
AS
BEGIN
    UPDATE XuatKhos
    SET 
        SoLuong = @SoLuong,
		NguyenNhanXuatKho = @NguyenNhan 
    WHERE MaXuatKho = @MaXuatKho;
END

EXEC proc_CapNhatThongTinXuatKho @MaXuatKho = 1, @SoLuong = 10, @NguyenNhan = 'Nguyên nhân test';

-- Thêm Xuất Kho
CREATE PROCEDURE proc_ThemXuatKho
	@MaNhanVien INT,
    @MaNguyenLieu INT,
    @SoLuong INT,
	@NgayXuat DATETIME2(7),
	@NguyenNhan NVARCHAR(MAX),
	@MaLuuTru INT
AS
BEGIN
    INSERT INTO XuatKhos(MaNhanVien, MaNguyenLieu, SoLuong, NgayXuat, NguyenNhanXuatKho, MaLuuTru)
    VALUES (@MaNhanVien, @MaNguyenLieu, @SoLuong, @NgayXuat, @NguyenNhan, @MaLuuTru);
END

-- Xóa Xuất Kho
CREATE PROCEDURE proc_XoaXuatKho
    @MaXuatKho INT
AS
BEGIN
    DELETE FROM XuatKhos WHERE MaXuatKho = @MaXuatKho;
END

-- LuuTrus
-- Cập Nhật Thông Tin Lưu Trữ
CREATE PROCEDURE proc_CapNhatThongTinLuuTru
	@MaLuuTru INT,
    @SoLuong INT,
	@NgayHetHan DATETIME2(7)
AS
BEGIN
    UPDATE LuuTrus
    SET 
        SoLuong = @SoLuong,
		NgayHetHan = @NgayHetHan 
    WHERE MaLuuTru = @MaLuuTru;
END

-- Thêm Lưu Trữ
CREATE PROCEDURE proc_ThemLuuTru
	@MaNhanVien INT,
    @MaNguyenLieu INT,
    @SoLuong INT,
	@NgayHetHan DATETIME2(7)
AS
BEGIN
    INSERT INTO LuuTrus(MaNhanVien, MaNguyenLieu, SoLuong, NgayHetHan)
    VALUES (@MaNhanVien, @MaNguyenLieu, @SoLuong, @NgayHetHan);
END

-- Xóa Lưu Trữ
CREATE PROCEDURE proc_XoaLuuTru
    @MaLuuTru INT
AS
BEGIN
    DELETE FROM LuuTrus WHERE MaLuuTru = @MaLuuTru;
END

--------------------------------------------------------------
-- Trigger --
-- HoaDons
-- Trigger kiểm tra số lượng món ăn trước khi thêm hóa đơn để ngănKha không cho tạo hóa đơn nếu món ăn đã hết hàng.
CREATE TRIGGER trg_Check_SoLuongMonAn_Before_Insert_HoaDon
ON HoaDons
INSTEAD OF INSERT
AS
BEGIN
    DECLARE @MaHoaDon INT, @MaMonAn INT, @SoLuongDat INT;

    SELECT @MaHoaDon = inserted.MaHoaDon, @MaMonAn = MA.MaMonAn, @SoLuongDat = MA.SoLuongHienCo
    FROM inserted
    JOIN MonAns MA ON MA.MaMonAn = inserted.MaHoaDon;

    IF @SoLuongDat <= 0
    BEGIN
        PRINT 'Không thể tạo hóa đơn vì món ăn đã hết hàng!';
        ROLLBACK TRANSACTION;
        RETURN;
    END;

    INSERT INTO HoaDons (MaHoaDon, MaNhanVien, MaKhachHang, NgayLap, TongTien)
    SELECT MaHoaDon, MaNhanVien, MaKhachHang, NgayLap, TongTien FROM inserted;
END;
GO
DISABLE TRIGGER [dbo].[trg_Check_SoLuongMonAn_Before_Insert_HoaDon]
    ON [dbo].[HoaDons];

-- ChiTietHoaDons
-- Trigger Tăng số lượng món ăn nếu đã có trong hóa đơn đó
CREATE TRIGGER trg_MergeChiTietHoaDonIfExist
ON ChiTietHoaDons
INSTEAD OF INSERT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaHoaDon INT,
            @MaMonAn INT,
            @SoLuong INT,
            @Gia FLOAT,
            @ThanhTien FLOAT;

    SELECT 
        @MaHoaDon = MaHoaDon,
        @MaMonAn = MaMonAn,
        @SoLuong = SoLuong
    FROM inserted;

    SELECT @Gia = Gia FROM MonAns WHERE MaMonAn = @MaMonAn;
    SET @ThanhTien = @SoLuong * @Gia;

    IF EXISTS (
        SELECT 1 FROM ChiTietHoaDons 
        WHERE MaHoaDon = @MaHoaDon AND MaMonAn = @MaMonAn
    )
    BEGIN
        UPDATE ChiTietHoaDons
        SET SoLuong = SoLuong + @SoLuong,
            ThanhTien = (SoLuong + @SoLuong) * @Gia
        WHERE MaHoaDon = @MaHoaDon AND MaMonAn = @MaMonAn;
    END
    ELSE
    BEGIN
        INSERT INTO ChiTietHoaDons (MaHoaDon, MaMonAn, SoLuong, ThanhTien)
        VALUES (@MaHoaDon, @MaMonAn, @SoLuong, @ThanhTien);
    END
END;

-- Trigger tự động cập nhật thành tiền trong chi tiết hóa đơn sau mỗi lần thêm món vào hóa đơn hoặc sửa số lượng món trong hóa đơn
CREATE TRIGGER trg_UpdateThanhTien
ON ChiTietHoaDons
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE cthd
    SET cthd.ThanhTien = cthd.SoLuong * ma.Gia
    FROM ChiTietHoaDons cthd
    INNER JOIN inserted i ON cthd.MaChiTietHoaDon = i.MaChiTietHoaDon
    INNER JOIN MonAns ma ON i.MaMonAn = ma.MaMonAn;
END;

-- Trigger cập nhật tổng tiền của hóa đơn dựa trên chi tiết hóa đơn
CREATE TRIGGER trg_UpdateTongTien
ON ChiTietHoaDons
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @MaHoaDonTable TABLE (MaHoaDon INT);

    -- Trường hợp INSERT và UPDATE
    INSERT INTO @MaHoaDonTable(MaHoaDon)
    SELECT DISTINCT MaHoaDon FROM inserted;

    -- Trường hợp DELETE
    INSERT INTO @MaHoaDonTable(MaHoaDon)
    SELECT DISTINCT MaHoaDon FROM deleted;

    -- Cập nhật lại tổng tiền trong bảng HoaDons
    UPDATE hd
    SET TongTien = ISNULL((
        SELECT SUM(ThanhTien)
        FROM ChiTietHoaDons cthd
        WHERE cthd.MaHoaDon = hd.MaHoaDon
    ), 0)
    FROM HoaDons hd
    WHERE hd.MaHoaDon IN (SELECT MaHoaDon FROM @MaHoaDonTable);
END;


-- NhanViens
-- Trigger kiểm tra trùng lặp email và số điện thoại của nhân viên
CREATE TRIGGER trg_KiemTraTrungLapNhanVien
ON NhanViens
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorMessage NVARCHAR(255);

    -- Kiểm tra trùng lặp số điện thoại
    IF EXISTS (
        SELECT I.SoDienThoai
        FROM inserted I
        JOIN NhanViens N ON I.SoDienThoai = N.SoDienThoai AND I.MaNhanVien <> N.MaNhanVien
    )
    BEGIN
        SET @ErrorMessage = N'Lỗi: Số điện thoại đã tồn tại!';
        THROW 51000, @ErrorMessage, 1;
    END

    -- Kiểm tra trùng lặp email
    IF EXISTS (
        SELECT I.Email
        FROM inserted I
        JOIN NhanViens N ON I.Email = N.Email AND I.MaNhanVien <> N.MaNhanVien
    )
    BEGIN
        SET @ErrorMessage = N'Lỗi: Email đã tồn tại!';
        THROW 51000, @ErrorMessage, 1;
    END
END;

-- KhuyenMais
-- Trigger tự chặn khuyến mãi nếu khuyến mãi tạo ra đã hết hạn hoặc đã dùng
CREATE TRIGGER trg_Block_Invalid_KhuyenMai
ON KhuyenMais
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (
        SELECT 1
        FROM inserted
        WHERE DaDung = 1 OR NgayHetHan < CAST(GETDATE() AS DATE)
    )
    BEGIN
        THROW 50001, N'Lỗi: Không thể thêm khuyến mãi đã dùng hoặc đã hết hạn!', 1;
        RETURN;
    END

-- MonAns
-- Trigger kiểm tra trùng lặp món 
CREATE TRIGGER trg_KiemTraTrungLapMonAn
ON MonAns
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorMessage NVARCHAR(255);

    -- Kiểm tra trùng lặp tên món ăn
    IF EXISTS (
        SELECT I.TenMonAn
        FROM inserted I
        JOIN MonAns M ON I.TenMonAn = M.TenMonAn AND I.MaMonAn <> M.MaMonAn
    )
    BEGIN
        SET @ErrorMessage = N'Lỗi: Tên món ăn đã tồn tại!';
        THROW 51000, @ErrorMessage, 1;
    END
END;

-- KhachHangs
-- Kiểm tra trùng lặp số điện thoại của khách hàng, nếu trùng thì sẽ không cho phép nhập
CREATE TRIGGER trg_KiemTraTrungLapSDT_KhachHang
ON KhachHangs
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @ErrorMessage NVARCHAR(255);

    -- Kiểm tra trùng số điện thoại
    IF EXISTS (
        SELECT I.SoDienThoai
        FROM inserted I
        JOIN KhachHangs K ON I.SoDienThoai = K.SoDienThoai AND I.MaKhachHang <> K.MaKhachHang
    )
    BEGIN
        SET @ErrorMessage = N'Lỗi: Số điện thoại đã tồn tại!';
        THROW 51000, @ErrorMessage, 1;
    END
END;

-- NhapKhos
--Trigger tự động tính tổng tiền khi nhập kho
CREATE TRIGGER trg_UpdateTongTien_NhapKho
ON NhapKhos
AFTER INSERT, UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE nk
    SET nk.TongTien = nk.SoLuong * nl.Gia
    FROM NhapKhos nk
    INNER JOIN inserted i ON nk.MaNhapKho = i.MaNhapKho
    INNER JOIN NguyenLieus nl ON i.MaNguyenLieu = nl.MaNguyenLieu;
END;

-- XuatKhos
-- Trigger kiểm tra và cập nhật tồn kho khi xuất kho
CREATE TRIGGER trg_XuatKho_Insert
ON XuatKhos
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE lt
    SET lt.SoLuong = lt.SoLuong - i.SoLuong
    FROM LuuTrus lt
    INNER JOIN inserted i ON lt.MaLuuTru = i.MaLuuTru;

    IF EXISTS (SELECT 1 FROM LuuTrus WHERE SoLuong < 0)
    BEGIN
        RAISERROR(N'Số lượng trong kho không đủ để xuất!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;

-- Trigger kiểm tra và cập nhật tồn kho khi cập nhật thông tin xuất kho
CREATE TRIGGER trg_XuatKho_Update
ON XuatKhos
AFTER UPDATE
AS
BEGIN
    SET NOCOUNT ON;

    -- Trừ số lượng cũ
    UPDATE lt
    SET lt.SoLuong = lt.SoLuong + d.SoLuongXuat
    FROM LuuTrus lt
    INNER JOIN deleted d ON lt.MaLuuTru = d.MaLuuTru;

    -- Trừ số lượng mới
    UPDATE lt
    SET lt.SoLuong = lt.SoLuong - i.SoLuongXuat
    FROM LuuTrus lt
    INNER JOIN inserted i ON lt.MaLuuTru = i.MaLuuTru;

    -- Kiểm tra không âm
    IF EXISTS (SELECT 1 FROM LuuTrus WHERE SoLuong < 0)
    BEGIN
        RAISERROR(N'Cập nhật không hợp lệ: Số lượng trong kho âm!', 16, 1);
        ROLLBACK TRANSACTION;
    END
END;

-- Trigger khôi phục lại số lượng trong kho khi xoá phiếu xuất kho
CREATE TRIGGER trg_XuatKho_Delete
ON XuatKhos
AFTER DELETE
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE lt
    SET lt.SoLuong = lt.SoLuong + d.SoLuongXuat
    FROM LuuTrus lt
    INNER JOIN deleted d ON lt.MaLuuTru = d.MaLuuTru;
END;

-- LuuTrus
-- Trigger tự động đặt số lượng nguyên liệu về 0 khi hết hạn
CREATE TRIGGER trg_LuuTrus_CheckExpiration
ON [dbo].[LuuTrus]
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    SET NOCOUNT ON;

    -- Cập nhật SoLuong = 0 cho các bản ghi đã hết hạn
    UPDATE [dbo].[LuuTrus]
    SET SoLuong = 0
    WHERE NgayHetHan < GETDATE()
    AND SoLuong > 0;
END;
GO
-- Tạo login
CREATE LOGIN NhanVienPhucVuLogin WITH PASSWORD = 'PhucVu@2025';
CREATE LOGIN NhanVienQuanLyKhoLogin WITH PASSWORD = 'QuanLyKho@2025';
CREATE LOGIN AdminLogin WITH PASSWORD = 'Admin@2025';
GO
-- Tạo user trong cơ sở dữ liệu cho các login
CREATE USER NhanVienPhucVuUser FOR LOGIN NhanVienPhucVuLogin;
CREATE USER NhanVienQuanLyKhoUser FOR LOGIN NhanVienQuanLyKhoLogin;
CREATE USER AdminUser FOR LOGIN AdminLogin;
GO
-- Tạo role trong cơ sở dữ liệu để quản lý quyền
CREATE ROLE PhucVuRole;
CREATE ROLE QuanLyKhoRole;
CREATE ROLE AdminRole;
GO
-- Gán user vào các role tương ứng
EXEC sp_addrolemember 'PhucVuRole', 'NhanVienPhucVuUser';
EXEC sp_addrolemember 'QuanLyKhoRole', 'NhanVienQuanLyKhoUser';
EXEC sp_addrolemember 'AdminRole', 'AdminUser';
GO
---- Phân quyền cho PhucVuRole
GRANT SELECT ON [dbo].[MonAns] TO PhucVuRole;
GRANT SELECT, INSERT ON [dbo].[HoaDons] TO PhucVuRole;
GRANT SELECT, INSERT ON [dbo].[ChiTietHoaDons] TO PhucVuRole;
GO