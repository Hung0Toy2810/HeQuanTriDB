namespace HeQuanTriDB.repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddOrder(HoaDon hoaDon, SqlConnection connection, SqlTransaction transaction)
        {
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

        public async Task<int> AddOrderDetail(ChiTietHoaDon chiTietHoaDon, SqlConnection connection, SqlTransaction transaction)
        {
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
    }
}