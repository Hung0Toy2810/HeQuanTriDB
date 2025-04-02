using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace HeQuanTriDB.Repositories.OrderRepository // Sửa "Repositoies" thành "Repositories"
{
    public interface IOrderRepository
    {
        string ConnectionString { get; }
        Task<int> AddOrder(HoaDon hoaDon, SqlTransaction transaction);
        Task<int> AddOrderDetail(ChiTietHoaDon chiTietHoaDon, SqlTransaction transaction);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public OrderRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<int> AddOrder(HoaDon hoaDon, SqlTransaction transaction)
        {
            using var command = transaction.Connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO HoaDons (MaNhanVien, MaKhachHang, NgayLap, TongTien)
                OUTPUT INSERTED.MaHoaDon
                VALUES (@MaNhanVien, @MaKhachHang, @NgayLap, @TongTien)";

            command.Parameters.AddWithValue("@MaNhanVien", hoaDon.MaNhanVien);
            command.Parameters.AddWithValue("@MaKhachHang", hoaDon.MaKhachHang);
            command.Parameters.AddWithValue("@NgayLap", hoaDon.NgayLap);
            command.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }

        public async Task<int> AddOrderDetail(ChiTietHoaDon chiTietHoaDon, SqlTransaction transaction)
        {
            using var command = transaction.Connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO ChiTietHoaDons (MaHoaDon, MaMonAn, SoLuong, ThanhTien)
                OUTPUT INSERTED.MaChiTietHoaDon
                VALUES (@MaHoaDon, @MaMonAn, @SoLuong, @ThanhTien)";

            command.Parameters.AddWithValue("@MaHoaDon", chiTietHoaDon.MaHoaDon);
            command.Parameters.AddWithValue("@MaMonAn", chiTietHoaDon.MaMonAn);
            command.Parameters.AddWithValue("@SoLuong", chiTietHoaDon.SoLuong);
            command.Parameters.AddWithValue("@ThanhTien", chiTietHoaDon.ThanhTien);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
    }
}