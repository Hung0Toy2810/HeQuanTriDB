using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HeQuanTriDB.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ChucVus",
                columns: table => new
                {
                    MaChucVu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenChucVu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChucVus", x => x.MaChucVu);
                });

            migrationBuilder.CreateTable(
                name: "KhachHangs",
                columns: table => new
                {
                    MaKhachHang = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhachHang = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhachHangs", x => x.MaKhachHang);
                });

            migrationBuilder.CreateTable(
                name: "MonAns",
                columns: table => new
                {
                    MaMonAn = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenMonAn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    SoLuongHienCo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MonAns", x => x.MaMonAn);
                });

            migrationBuilder.CreateTable(
                name: "NhaCungCaps",
                columns: table => new
                {
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhaCungCap = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhaCungCaps", x => x.MaNhaCungCap);
                });

            migrationBuilder.CreateTable(
                name: "NhanViens",
                columns: table => new
                {
                    MaNhanVien = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNhanVien = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaChucVu = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhanViens", x => x.MaNhanVien);
                    table.ForeignKey(
                        name: "FK_NhanViens_ChucVus_MaChucVu",
                        column: x => x.MaChucVu,
                        principalTable: "ChucVus",
                        principalColumn: "MaChucVu");
                });

            migrationBuilder.CreateTable(
                name: "KhuyenMais",
                columns: table => new
                {
                    MaKhuyenMai = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenKhuyenMai = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    DaDung = table.Column<bool>(type: "bit", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KhuyenMais", x => x.MaKhuyenMai);
                    table.ForeignKey(
                        name: "FK_KhuyenMais_KhachHangs_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang");
                });

            migrationBuilder.CreateTable(
                name: "NguyenLieus",
                columns: table => new
                {
                    MaNguyenLieu = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenNguyenLieu = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DonViTinh = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Gia = table.Column<double>(type: "float", nullable: false),
                    MaNhaCungCap = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NguyenLieus", x => x.MaNguyenLieu);
                    table.ForeignKey(
                        name: "FK_NguyenLieus_NhaCungCaps_MaNhaCungCap",
                        column: x => x.MaNhaCungCap,
                        principalTable: "NhaCungCaps",
                        principalColumn: "MaNhaCungCap");
                });

            migrationBuilder.CreateTable(
                name: "HoaDons",
                columns: table => new
                {
                    MaHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<int>(type: "int", nullable: false),
                    MaKhachHang = table.Column<int>(type: "int", nullable: false),
                    NgayLap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HoaDons", x => x.MaHoaDon);
                    table.ForeignKey(
                        name: "FK_HoaDons_KhachHangs_MaKhachHang",
                        column: x => x.MaKhachHang,
                        principalTable: "KhachHangs",
                        principalColumn: "MaKhachHang");
                    table.ForeignKey(
                        name: "FK_HoaDons_NhanViens_MaNhanVien",
                        column: x => x.MaNhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "MaNhanVien");
                });

            migrationBuilder.CreateTable(
                name: "LuuTrus",
                columns: table => new
                {
                    MaLuuTru = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<int>(type: "int", nullable: false),
                    MaNguyenLieu = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    NgayHetHan = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LuuTrus", x => x.MaLuuTru);
                    table.ForeignKey(
                        name: "FK_LuuTrus_NguyenLieus_MaNguyenLieu",
                        column: x => x.MaNguyenLieu,
                        principalTable: "NguyenLieus",
                        principalColumn: "MaNguyenLieu");
                    table.ForeignKey(
                        name: "FK_LuuTrus_NhanViens_MaNhanVien",
                        column: x => x.MaNhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "MaNhanVien");
                });

            migrationBuilder.CreateTable(
                name: "NhapKhos",
                columns: table => new
                {
                    MaNhapKho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<int>(type: "int", nullable: false),
                    MaNguyenLieu = table.Column<int>(type: "int", nullable: false),
                    NgayNhap = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    TongTien = table.Column<double>(type: "float", nullable: false),
                    SoNgayHetHan = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NhapKhos", x => x.MaNhapKho);
                    table.ForeignKey(
                        name: "FK_NhapKhos_NguyenLieus_MaNguyenLieu",
                        column: x => x.MaNguyenLieu,
                        principalTable: "NguyenLieus",
                        principalColumn: "MaNguyenLieu");
                    table.ForeignKey(
                        name: "FK_NhapKhos_NhanViens_MaNhanVien",
                        column: x => x.MaNhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "MaNhanVien");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietHoaDons",
                columns: table => new
                {
                    MaChiTietHoaDon = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaHoaDon = table.Column<int>(type: "int", nullable: false),
                    MaMonAn = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    ThanhTien = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietHoaDons", x => x.MaChiTietHoaDon);
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_HoaDons_MaHoaDon",
                        column: x => x.MaHoaDon,
                        principalTable: "HoaDons",
                        principalColumn: "MaHoaDon");
                    table.ForeignKey(
                        name: "FK_ChiTietHoaDons_MonAns_MaMonAn",
                        column: x => x.MaMonAn,
                        principalTable: "MonAns",
                        principalColumn: "MaMonAn");
                });

            migrationBuilder.CreateTable(
                name: "XuatKhos",
                columns: table => new
                {
                    MaXuatKho = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaNhanVien = table.Column<int>(type: "int", nullable: false),
                    MaNguyenLieu = table.Column<int>(type: "int", nullable: false),
                    SoLuong = table.Column<int>(type: "int", nullable: false),
                    NgayXuat = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NguyenNhanXuatKho = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaLuuTru = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_XuatKhos", x => x.MaXuatKho);
                    table.ForeignKey(
                        name: "FK_XuatKhos_LuuTrus_MaLuuTru",
                        column: x => x.MaLuuTru,
                        principalTable: "LuuTrus",
                        principalColumn: "MaLuuTru");
                    table.ForeignKey(
                        name: "FK_XuatKhos_NguyenLieus_MaNguyenLieu",
                        column: x => x.MaNguyenLieu,
                        principalTable: "NguyenLieus",
                        principalColumn: "MaNguyenLieu");
                    table.ForeignKey(
                        name: "FK_XuatKhos_NhanViens_MaNhanVien",
                        column: x => x.MaNhanVien,
                        principalTable: "NhanViens",
                        principalColumn: "MaNhanVien");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_MaHoaDon",
                table: "ChiTietHoaDons",
                column: "MaHoaDon");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietHoaDons_MaMonAn",
                table: "ChiTietHoaDons",
                column: "MaMonAn");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaKhachHang",
                table: "HoaDons",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_HoaDons_MaNhanVien",
                table: "HoaDons",
                column: "MaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_KhuyenMais_MaKhachHang",
                table: "KhuyenMais",
                column: "MaKhachHang");

            migrationBuilder.CreateIndex(
                name: "IX_LuuTrus_MaNguyenLieu",
                table: "LuuTrus",
                column: "MaNguyenLieu");

            migrationBuilder.CreateIndex(
                name: "IX_LuuTrus_MaNhanVien",
                table: "LuuTrus",
                column: "MaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_NguyenLieus_MaNhaCungCap",
                table: "NguyenLieus",
                column: "MaNhaCungCap");

            migrationBuilder.CreateIndex(
                name: "IX_NhanViens_MaChucVu",
                table: "NhanViens",
                column: "MaChucVu");

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhos_MaNguyenLieu",
                table: "NhapKhos",
                column: "MaNguyenLieu");

            migrationBuilder.CreateIndex(
                name: "IX_NhapKhos_MaNhanVien",
                table: "NhapKhos",
                column: "MaNhanVien");

            migrationBuilder.CreateIndex(
                name: "IX_XuatKhos_MaLuuTru",
                table: "XuatKhos",
                column: "MaLuuTru");

            migrationBuilder.CreateIndex(
                name: "IX_XuatKhos_MaNguyenLieu",
                table: "XuatKhos",
                column: "MaNguyenLieu");

            migrationBuilder.CreateIndex(
                name: "IX_XuatKhos_MaNhanVien",
                table: "XuatKhos",
                column: "MaNhanVien");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChiTietHoaDons");

            migrationBuilder.DropTable(
                name: "KhuyenMais");

            migrationBuilder.DropTable(
                name: "NhapKhos");

            migrationBuilder.DropTable(
                name: "XuatKhos");

            migrationBuilder.DropTable(
                name: "HoaDons");

            migrationBuilder.DropTable(
                name: "MonAns");

            migrationBuilder.DropTable(
                name: "LuuTrus");

            migrationBuilder.DropTable(
                name: "KhachHangs");

            migrationBuilder.DropTable(
                name: "NguyenLieus");

            migrationBuilder.DropTable(
                name: "NhanViens");

            migrationBuilder.DropTable(
                name: "NhaCungCaps");

            migrationBuilder.DropTable(
                name: "ChucVus");
        }
    }
}
