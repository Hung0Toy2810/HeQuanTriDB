using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HeQuanTriDB;
using Microsoft.EntityFrameworkCore;
using System;

namespace HeQuanTriDB
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var orderService = services.GetRequiredService<IOrderService>();

                    int maNhanVien = 1;
                    int maKhachHang = 2;
                    var orderItems = new List<OrderDTO>
                    {
                        new OrderDTO { MaMonAn = 1, TenMonAn = "Pho Bo", Gia = 50000, SoLuongHienCo = 100, SoLuong = 2 },
                        new OrderDTO { MaMonAn = 2, TenMonAn = "Bun Cha", Gia = 45000, SoLuongHienCo = 100, SoLuong = 1 }
                    };

                    int maHoaDon = await orderService.Order(maNhanVien, maKhachHang, orderItems);
                    Console.WriteLine($"Đơn hàng đã được tạo thành công với MaHoaDon: {maHoaDon}");
                    Console.WriteLine("Kiểm tra bảng HoaDons và ChiTietHoaDons trong DB để xác nhận.");
                    var context = services.GetRequiredService<DBContext>();
                    // Console.WriteLine("DbContext resolved successfully.");
                    // await SeedData(services.GetRequiredService<DBContext>());
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Lỗi khi tạo đơn hàng: {ex.Message}");
                }
            }

            Console.WriteLine("Nhấn phím bất kỳ để thoát...");
            Console.ReadKey();
        }

        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {

                    services.AddDbContext<DBContext>(options =>
                        options.UseSqlServer("Server=localhost,1433;Database=RestaurantManagement;User Id=sa;Password=YourPassword123;TrustServerCertificate=true;"));

                    services.AddScoped<IOrderRepository, OrderRepository>(sp =>
                        new OrderRepository("Server=localhost,1433;Database=RestaurantManagement;User Id=sa;Password=YourPassword123;TrustServerCertificate=true;"));

                    services.AddScoped<IOrderService, OrderService>();
                });


        static async Task SeedData(DBContext context)
        {
            if (!context.ChucVus.Any())
            {
                context.ChucVus.AddRange(
                    new ChucVu { TenChucVu = "QuanLyKho" },
                    new ChucVu { TenChucVu = "DauBep" },
                    new ChucVu { TenChucVu = "NhanVienPhucVu" },
                    new ChucVu { TenChucVu = "QuanLy" }
                );
                await context.SaveChangesAsync();
                Console.WriteLine("ChucVu data seeded.");
            }
            if (!context.NhanViens.Any())
            {
                context.NhanViens.AddRange(
                    new NhanVien { TenNhanVien = "Nguyen Van A", DiaChi = "123 ABC", SoDienThoai = "0123456789", Email = "a@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "QuanLy").MaChucVu },
                    new NhanVien { TenNhanVien = "Nguyen Van B", DiaChi = "456 DEF", SoDienThoai = "0123456788", Email = "b@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "QuanLyKho").MaChucVu },
                    new NhanVien { TenNhanVien = "Nguyen Van C", DiaChi = "789 GHI", SoDienThoai = "0123456787", Email = "c@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "QuanLyKho").MaChucVu },
                    new NhanVien { TenNhanVien = "Nguyen Van D", DiaChi = "101 JKL", SoDienThoai = "0123456786", Email = "d@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "NhanVienPhucVu").MaChucVu },
                    new NhanVien { TenNhanVien = "Nguyen Van E", DiaChi = "102 MNO", SoDienThoai = "0123456785", Email = "e@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "NhanVienPhucVu").MaChucVu },
                    new NhanVien { TenNhanVien = "Nguyen Van F", DiaChi = "103 PQR", SoDienThoai = "0123456784", Email = "f@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "NhanVienPhucVu").MaChucVu },
                    new NhanVien { TenNhanVien = "Nguyen Van G", DiaChi = "104 STU", SoDienThoai = "0123456783", Email = "g@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "NhanVienPhucVu").MaChucVu },
                    new NhanVien { TenNhanVien = "Nguyen Van H", DiaChi = "105 VWX", SoDienThoai = "0123456782", Email = "h@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "NhanVienPhucVu").MaChucVu },
                    new NhanVien { TenNhanVien = "Nguyen Van I", DiaChi = "106 YZ", SoDienThoai = "0123456781", Email = "i@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "NhanVienPhucVu").MaChucVu },
                    new NhanVien { TenNhanVien = "Nguyen Van J", DiaChi = "107 ABC", SoDienThoai = "0123456780", Email = "j@example.com", MatKhau = "password", MaChucVu = context.ChucVus.First(c => c.TenChucVu == "NhanVienPhucVu").MaChucVu }
                );
                await context.SaveChangesAsync();
                Console.WriteLine("NhanVien data seeded.");
            }
            if (!context.KhachHangs.Any())
            {
                context.KhachHangs.AddRange(
                    new KhachHang { TenKhachHang = "Tran Thi A", DiaChi = "123 ABC", SoDienThoai = "0987654321" },
                    new KhachHang { TenKhachHang = "Tran Thi B", DiaChi = "456 DEF", SoDienThoai = "0987654322" },
                    new KhachHang { TenKhachHang = "Tran Thi C", DiaChi = "789 GHI", SoDienThoai = "0987654323" },
                    new KhachHang { TenKhachHang = "Tran Thi D", DiaChi = "101 JKL", SoDienThoai = "0987654324" },
                    new KhachHang { TenKhachHang = "Tran Thi E", DiaChi = "102 MNO", SoDienThoai = "0987654325" },
                    new KhachHang { TenKhachHang = "Tran Thi F", DiaChi = "103 PQR", SoDienThoai = "0987654326" },
                    new KhachHang { TenKhachHang = "Tran Thi G", DiaChi = "104 STU", SoDienThoai = "0987654327" },
                    new KhachHang { TenKhachHang = "Tran Thi H", DiaChi = "105 VWX", SoDienThoai = "0987654328" },
                    new KhachHang { TenKhachHang = "Tran Thi I", DiaChi = "106 YZ", SoDienThoai = "0987654329" },
                    new KhachHang { TenKhachHang = "Tran Thi J", DiaChi = "107 ABC", SoDienThoai = "0987654330" }
                );
                await context.SaveChangesAsync();
                Console.WriteLine("KhachHang data seeded.");
            }
            if (!context.MonAns.Any())
            {
                context.MonAns.AddRange(
                    new MonAn { TenMonAn = "Pho Bo", Gia = 50000, SoLuongHienCo = 100 },
                    new MonAn { TenMonAn = "Bun Cha", Gia = 45000, SoLuongHienCo = 100 },
                    new MonAn { TenMonAn = "Banh Mi", Gia = 30000, SoLuongHienCo = 100 },
                    new MonAn { TenMonAn = "Com Tam", Gia = 40000, SoLuongHienCo = 100 },
                    new MonAn { TenMonAn = "Goi Cuon", Gia = 35000, SoLuongHienCo = 100 },
                    new MonAn { TenMonAn = "Banh Xeo", Gia = 40000, SoLuongHienCo = 100 },
                    new MonAn { TenMonAn = "Bun Bo Hue", Gia = 50000, SoLuongHienCo = 100 },
                    new MonAn { TenMonAn = "Hu Tieu", Gia = 45000, SoLuongHienCo = 100 },
                    new MonAn { TenMonAn = "Mi Quang", Gia = 50000, SoLuongHienCo = 100 },
                    new MonAn { TenMonAn = "Cha Gio", Gia = 30000, SoLuongHienCo = 100 }
                );
                await context.SaveChangesAsync();
                Console.WriteLine("MonAn data seeded.");
            }
            if (!context.HoaDons.Any())
            {
                context.HoaDons.AddRange(
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(0).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(0).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 100000 },
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(1).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(1).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 150000 },
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(2).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(2).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 200000 },
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(3).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(3).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 250000 },
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(4).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(4).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 300000 },
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(5).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(5).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 350000 },
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(6).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(6).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 400000 },
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(7).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(7).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 450000 },
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(8).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(8).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 500000 },
                    new HoaDon { MaNhanVien = context.NhanViens.Skip(9).First().MaNhanVien, MaKhachHang = context.KhachHangs.Skip(9).First().MaKhachHang, NgayLap = DateTime.Now, TongTien = 550000 }
                );

                await context.SaveChangesAsync();
                Console.WriteLine("HoaDon data seeded.");
            }
            if (!context.ChiTietHoaDons.Any())
            {
                var hoaDons = context.HoaDons.ToList();
                var monAns = context.MonAns.ToList();
                var random = new Random();

                for (int i = 0; i < 20; i++)
                {
                    var hoaDon = hoaDons[random.Next(hoaDons.Count)];
                    var monAn = monAns[random.Next(monAns.Count)];
                    context.ChiTietHoaDons.Add(new ChiTietHoaDon
                    {
                        MaHoaDon = hoaDon.MaHoaDon,
                        MaMonAn = monAn.MaMonAn,
                        SoLuong = random.Next(1, 5),
                        ThanhTien = monAn.Gia * random.Next(1, 5)
                    });
                }
                await context.SaveChangesAsync();
            }
            if (!context.KhuyenMais.Any())
            {
                var khachHangs = context.KhachHangs.Take(10).ToList();
                var random = new Random();

                context.KhuyenMais.AddRange(
                    new KhuyenMai { TenKhuyenMai = "Giảm 10%", MaKhachHang = khachHangs[random.Next(khachHangs.Count)].MaKhachHang, DaDung = false, NgayHetHan = DateTime.Now.AddMonths(1) },
                    new KhuyenMai { TenKhuyenMai = "Giảm 20%", MaKhachHang = khachHangs[random.Next(khachHangs.Count)].MaKhachHang, DaDung = false, NgayHetHan = DateTime.Now.AddMonths(2) },
                    new KhuyenMai { TenKhuyenMai = "Mua 1 tặng 1", MaKhachHang = khachHangs[random.Next(khachHangs.Count)].MaKhachHang, DaDung = false, NgayHetHan = DateTime.Now.AddDays(15) },
                    new KhuyenMai { TenKhuyenMai = "Freeship", MaKhachHang = khachHangs[random.Next(khachHangs.Count)].MaKhachHang, DaDung = false, NgayHetHan = DateTime.Now.AddDays(30) },
                    new KhuyenMai { TenKhuyenMai = "Giảm 50K", MaKhachHang = khachHangs[random.Next(khachHangs.Count)].MaKhachHang, DaDung = false, NgayHetHan = DateTime.Now.AddMonths(3) }
                );

                await context.SaveChangesAsync();
                Console.WriteLine("KhuyenMai data seeded.");
            }
            if (!context.NhaCungCaps.Any())
            {
                context.NhaCungCaps.AddRange(
                    new NhaCungCap { TenNhaCungCap = "Công ty Thực phẩm ABC", DiaChi = "123 Đường A, TP.HCM", SoDienThoai = "0901234567", Email = "abc@food.com" },
                    new NhaCungCap { TenNhaCungCap = "Nhà cung cấp Gạo XYZ", DiaChi = "456 Đường B, Hà Nội", SoDienThoai = "0912345678", Email = "xyz@rice.com" },
                    new NhaCungCap { TenNhaCungCap = "Công ty Rau Sạch", DiaChi = "789 Đường C, Đà Nẵng", SoDienThoai = "0923456789", Email = "rausach@organic.com" },
                    new NhaCungCap { TenNhaCungCap = "Thủy Hải Sản Minh Phú", DiaChi = "321 Đường D, Hải Phòng", SoDienThoai = "0934567890", Email = "minhphu@seafood.com" },
                    new NhaCungCap { TenNhaCungCap = "Công ty Sữa Việt", DiaChi = "654 Đường E, Cần Thơ", SoDienThoai = "0945678901", Email = "suaviet@milk.com" }
                );

                await context.SaveChangesAsync();
            }
            if (!context.NguyenLieus.Any())
            {
                var nhaCungCaps = context.NhaCungCaps.ToList();
                var random = new Random();

                context.NguyenLieus.AddRange(
                    new NguyenLieu { TenNguyenLieu = "Gạo", DonViTinh = "Kg", Gia = 15000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap },
                    new NguyenLieu { TenNguyenLieu = "Thịt Bò", DonViTinh = "Kg", Gia = 250000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap },
                    new NguyenLieu { TenNguyenLieu = "Thịt Gà", DonViTinh = "Kg", Gia = 120000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap },
                    new NguyenLieu { TenNguyenLieu = "Rau Xà Lách", DonViTinh = "Kg", Gia = 30000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap },
                    new NguyenLieu { TenNguyenLieu = "Cà Chua", DonViTinh = "Kg", Gia = 25000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap },
                    new NguyenLieu { TenNguyenLieu = "Hành Tây", DonViTinh = "Kg", Gia = 20000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap },
                    new NguyenLieu { TenNguyenLieu = "Khoai Tây", DonViTinh = "Kg", Gia = 18000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap },
                    new NguyenLieu { TenNguyenLieu = "Tôm Tươi", DonViTinh = "Kg", Gia = 300000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap },
                    new NguyenLieu { TenNguyenLieu = "Trứng Gà", DonViTinh = "Hộp", Gia = 35000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap },
                    new NguyenLieu { TenNguyenLieu = "Sữa Tươi", DonViTinh = "Lít", Gia = 25000, MaNhaCungCap = nhaCungCaps[random.Next(nhaCungCaps.Count)].MaNhaCungCap }
                );

                await context.SaveChangesAsync();
                Console.WriteLine("NguyenLieu data seeded.");
            }
            if (!context.NhapKhos.Any())
            {
                var nguyenLieus = context.NguyenLieus.ToList();
                var random = new Random();
                var nhanVienIds = new List<int> { 2, 3 };

                context.NhapKhos.AddRange(
                    Enumerable.Range(1, 10).Select(_ => new NhapKho
                    {
                        MaNhanVien = nhanVienIds[random.Next(nhanVienIds.Count)],
                        MaNguyenLieu = nguyenLieus[random.Next(nguyenLieus.Count)].MaNguyenLieu,
                        NgayNhap = DateTime.Now.AddDays(-random.Next(1, 30)),
                        SoLuong = random.Next(10, 100),
                        TongTien = random.Next(50000, 2000000),
                        SoNgayHetHan = random.Next(7, 365)
                    })
                );

                await context.SaveChangesAsync();
                Console.WriteLine("NhapKho data seeded.");
            }
            if (!context.LuuTrus.Any())
            {
                var nguyenLieus = context.NguyenLieus.ToList();
                var random = new Random();
                var nhanVienIds = new List<int> { 2, 3 };

                context.LuuTrus.AddRange(
                    Enumerable.Range(1, 10).Select(_ => new LuuTru
                    {
                        MaNhanVien = nhanVienIds[random.Next(nhanVienIds.Count)],
                        MaNguyenLieu = nguyenLieus[random.Next(nguyenLieus.Count)].MaNguyenLieu,
                        SoLuong = random.Next(5, 50),
                        NgayHetHan = DateTime.Now.AddDays(random.Next(30, 365))
                    })
                );

                await context.SaveChangesAsync();
                Console.WriteLine("LuuTru data seeded.");
            }
            if (!context.XuatKhos.Any())
            {
                var nhanViens = context.NhanViens.ToList();
                var luuTrus = context.LuuTrus.ToList();
                var random = new Random();

                context.XuatKhos.AddRange(
                    Enumerable.Range(1, 10).Select(_ =>
                    {
                        var luuTru = luuTrus[random.Next(luuTrus.Count)];
                        return new XuatKho
                        {
                            MaNhanVien = nhanViens[random.Next(nhanViens.Count)].MaNhanVien,
                            MaNguyenLieu = luuTru.MaNguyenLieu,
                            SoLuong = random.Next(1, Math.Min(10, luuTru.SoLuong)),
                            NgayXuat = DateTime.Now.AddDays(-random.Next(1, 30)),
                            NguyenNhanXuatKho = "Xuất kho để sử dụng",
                            MaLuuTru = luuTru.MaLuuTru
                        };
                    })
                );

                await context.SaveChangesAsync();
                Console.WriteLine("XuatKho data seeded.");
            }

        }
    }
}

