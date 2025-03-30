namespace HeQuanTriDB.Repositoies.OrderRepository
{
    public interface IOrderRepository
    {
        string ConnectionString { get; }
        Task<int> AddOrder(HoaDon hoaDon, SqlTransaction transaction);
        Task<int> AddOrderDetail(ChiTietHoaDon chiTietHoaDon, SqlTransaction transaction);
        Task<List<HoaDon>> GetAllOrders();
        Task<List<HoaDon>> GetAllOrderByKhachHangId(int maKhachHang);
        Task<int> UpdateOrder(HoaDon hoaDon, SqlTransaction transaction);
        Task<int> DeleteOrder(int maHoaDon, SqlTransaction transaction);
        Task<List<ChiTietHoaDon>> GetAllOrderDetailsByOrderId(int maHoaDon);
        Task<int> UpdateOrderDetail(ChiTietHoaDon chiTietHoaDon, SqlTransaction transaction);
        Task<int> DeleteOrderDetail(int maChiTietHoaDon, SqlTransaction transaction);
    }
}
namespace HeQuanTriDB.Repositoies.OrderRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddOrder(HoaDon hoaDon, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
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
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
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

        public async Task<List<HoaDon>> GetAllOrders()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM HoaDons";

            using var reader = await command.ExecuteReaderAsync();
            var hoaDons = new List<HoaDon>();
            while (await reader.ReadAsync())
            {
                var hoaDon = new HoaDon
                {
                    MaHoaDon = reader.GetInt32(reader.GetOrdinal("MaHoaDon")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaKhachHang = reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    NgayLap = reader.GetDateTime(reader.GetOrdinal("NgayLap")),
                    TongTien = reader.GetDouble(reader.GetOrdinal("TongTien"))
                };
                hoaDons.Add(hoaDon);
            }
            return hoaDons;
        }

        public async Task<List<HoaDon>> GetAllOrderByKhachHangId(int maKhachHang)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM HoaDons WHERE MaKhachHang = @MaKhachHang";
            command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

            using var reader = await command.ExecuteReaderAsync();
            var hoaDons = new List<HoaDon>();
            while (await reader.ReadAsync())
            {
                var hoaDon = new HoaDon
                {
                    MaHoaDon = reader.GetInt32(reader.GetOrdinal("MaHoaDon")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaKhachHang = reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    NgayLap = reader.GetDateTime(reader.GetOrdinal("NgayLap")),
                    TongTien = reader.GetDouble(reader.GetOrdinal("TongTien"))
                };
                hoaDons.Add(hoaDon);
            }
            return hoaDons;
        }

        public async Task<int> UpdateOrder(HoaDon hoaDon, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE HoaDons
                SET MaNhanVien = @MaNhanVien, MaKhachHang = @MaKhachHang, NgayLap = @NgayLap, TongTien = @TongTien
                WHERE MaHoaDon = @MaHoaDon";

            command.Parameters.AddWithValue("@MaHoaDon", hoaDon.MaHoaDon);
            command.Parameters.AddWithValue("@MaNhanVien", hoaDon.MaNhanVien);
            command.Parameters.AddWithValue("@MaKhachHang", hoaDon.MaKhachHang);
            command.Parameters.AddWithValue("@NgayLap", hoaDon.NgayLap);
            command.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteOrder(int maHoaDon, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM HoaDons WHERE MaHoaDon = @MaHoaDon";
            command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<List<ChiTietHoaDon>> GetAllOrderDetailsByOrderId(int maHoaDon)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM ChiTietHoaDons WHERE MaHoaDon = @MaHoaDon";
            command.Parameters.AddWithValue("@MaHoaDon", maHoaDon);

            using var reader = await command.ExecuteReaderAsync();
            var chiTietHoaDons = new List<ChiTietHoaDon>();
            while (await reader.ReadAsync())
            {
                var chiTietHoaDon = new ChiTietHoaDon
                {
                    MaChiTietHoaDon = reader.GetInt32(reader.GetOrdinal("MaChiTietHoaDon")),
                    MaHoaDon = reader.GetInt32(reader.GetOrdinal("MaHoaDon")),
                    MaMonAn = reader.GetInt32(reader.GetOrdinal("MaMonAn")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    ThanhTien = reader.GetDouble(reader.GetOrdinal("ThanhTien"))
                };
                chiTietHoaDons.Add(chiTietHoaDon);
            }
            return chiTietHoaDons;
        }

        public async Task<int> UpdateOrderDetail(ChiTietHoaDon chiTietHoaDon, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE ChiTietHoaDons
                SET MaHoaDon = @MaHoaDon, MaMonAn = @MaMonAn, SoLuong = @SoLuong, ThanhTien = @ThanhTien
                WHERE MaChiTietHoaDon = @MaChiTietHoaDon";

            command.Parameters.AddWithValue("@MaChiTietHoaDon", chiTietHoaDon.MaChiTietHoaDon);
            command.Parameters.AddWithValue("@MaHoaDon", chiTietHoaDon.MaHoaDon);
            command.Parameters.AddWithValue("@MaMonAn", chiTietHoaDon.MaMonAn);
            command.Parameters.AddWithValue("@SoLuong", chiTietHoaDon.SoLuong);
            command.Parameters.AddWithValue("@ThanhTien", chiTietHoaDon.ThanhTien);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteOrderDetail(int maChiTietHoaDon, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM ChiTietHoaDons WHERE MaChiTietHoaDon = @MaChiTietHoaDon";
            command.Parameters.AddWithValue("@MaChiTietHoaDon", maChiTietHoaDon);

            return await command.ExecuteNonQueryAsync();
        }
    }
}