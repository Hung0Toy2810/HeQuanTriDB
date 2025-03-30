namespace HeQuanTriDB.Repositoies.KhachHangRepository
{
    public interface IKhachHangRepository
    {
        string ConnectionString { get; }
        Task<List<KhachHang>> GetAllKhachHangs();
        Task<KhachHang> GetKhachHangByID(int maKhachHang);
        Task<int> AddKhachHang(KhachHang khachHang, SqlTransaction transaction);
        Task<int> UpdateKhachHang(KhachHang khachHang, SqlTransaction transaction);
        Task<int> DeleteKhachHang(int maKhachHang, SqlTransaction transaction);
    }
}
namespace HeQuanTriDB.Repositoies.KhachHangRepository
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public KhachHangRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<KhachHang>> GetAllKhachHangs()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM KhachHangs";

            using var reader = await command.ExecuteReaderAsync();
            var khachHangs = new List<KhachHang>();
            while (await reader.ReadAsync())
            {
                var khachHang = new KhachHang
                {
                    MaKhachHang = reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    TenKhachHang = reader.GetString(reader.GetOrdinal("TenKhachHang")),
                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi")),
                    SoDienThoai = reader.GetString(reader.GetOrdinal("SoDienThoai"))
                };
                khachHangs.Add(khachHang);
            }
            return khachHangs;
        }

        public async Task<KhachHang> GetKhachHangByID(int maKhachHang)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM KhachHangs WHERE MaKhachHang = @MaKhachHang";
            command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new KhachHang
                {
                    MaKhachHang = reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    TenKhachHang = reader.GetString(reader.GetOrdinal("TenKhachHang")),
                    DiaChi = reader.IsDBNull(reader.GetOrdinal("DiaChi")) ? null : reader.GetString(reader.GetOrdinal("DiaChi")),
                    SoDienThoai = reader.GetString(reader.GetOrdinal("SoDienThoai"))
                };
            }
            throw new KeyNotFoundException($"KhachHang with ID {maKhachHang} was not found.");
        }

        public async Task<int> AddKhachHang(KhachHang khachHang, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO KhachHangs (TenKhachHang, DiaChi, SoDienThoai)
                VALUES (@TenKhachHang, @DiaChi, @SoDienThoai);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@TenKhachHang", khachHang.TenKhachHang);
            command.Parameters.AddWithValue("@DiaChi", (object?)khachHang.DiaChi ?? DBNull.Value);
            command.Parameters.AddWithValue("@SoDienThoai", khachHang.SoDienThoai);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        public async Task<int> UpdateKhachHang(KhachHang khachHang, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE KhachHangs
                SET TenKhachHang = @TenKhachHang, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai
                WHERE MaKhachHang = @MaKhachHang";
            command.Parameters.AddWithValue("@MaKhachHang", khachHang.MaKhachHang);
            command.Parameters.AddWithValue("@TenKhachHang", khachHang.TenKhachHang);
            command.Parameters.AddWithValue("@DiaChi", (object?)khachHang.DiaChi ?? DBNull.Value);
            command.Parameters.AddWithValue("@SoDienThoai", khachHang.SoDienThoai);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteKhachHang(int maKhachHang, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM KhachHangs WHERE MaKhachHang = @MaKhachHang";
            command.Parameters.AddWithValue("@MaKhachHang", maKhachHang);

            return await command.ExecuteNonQueryAsync();
        }
    }
}