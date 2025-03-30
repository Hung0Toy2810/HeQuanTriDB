namespace HeQuanTriDB.Respositoies.KhuyenMaiRepository
{
    public interface IKhuyenMaiRepository
    {
        string ConnectionString { get; }
        Task<List<KhuyenMai>> GetAllKhuyenMais();
        Task<KhuyenMai> GetKhuyenMaiByID(int maKhuyenMai);
        Task<int> AddKhuyenMai(KhuyenMai khuyenMai, SqlTransaction transaction);
        Task<int> UpdateKhuyenMai(KhuyenMai khuyenMai, SqlTransaction transaction);
        Task<int> DeleteKhuyenMai(int maKhuyenMai, SqlTransaction transaction);
    }
}

namespace HeQuanTriDB.Respositoies.KhuyenMaiRepository
{
    public class KhuyenMaiRepository : IKhuyenMaiRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public KhuyenMaiRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<KhuyenMai>> GetAllKhuyenMais()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM KhuyenMais";

            using var reader = await command.ExecuteReaderAsync();
            var khuyenMais = new List<KhuyenMai>();
            while (await reader.ReadAsync())
            {
                var khuyenMai = new KhuyenMai
                {
                    MaKhuyenMai = reader.GetInt32(reader.GetOrdinal("MaKhuyenMai")),
                    TenKhuyenMai = reader.GetString(reader.GetOrdinal("TenKhuyenMai")),
                    MaKhachHang = reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    DaDung = reader.GetBoolean(reader.GetOrdinal("DaDung")),
                    NgayHetHan = reader.GetDateTime(reader.GetOrdinal("NgayHetHan"))
                };
                khuyenMais.Add(khuyenMai);
            }
            return khuyenMais;
        }

        public async Task<KhuyenMai> GetKhuyenMaiByID(int maKhuyenMai)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM KhuyenMais WHERE MaKhuyenMai = @MaKhuyenMai";
            command.Parameters.AddWithValue("@MaKhuyenMai", maKhuyenMai);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new KhuyenMai
                {
                    MaKhuyenMai = reader.GetInt32(reader.GetOrdinal("MaKhuyenMai")),
                    TenKhuyenMai = reader.GetString(reader.GetOrdinal("TenKhuyenMai")),
                    MaKhachHang = reader.GetInt32(reader.GetOrdinal("MaKhachHang")),
                    DaDung = reader.GetBoolean(reader.GetOrdinal("DaDung")),
                    NgayHetHan = reader.GetDateTime(reader.GetOrdinal("NgayHetHan"))
                };
            }
            throw new KeyNotFoundException($"KhuyenMai with ID {maKhuyenMai} was not found.");
        }

        public async Task<int> AddKhuyenMai(KhuyenMai khuyenMai, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO KhuyenMais (TenKhuyenMai, MaKhachHang, DaDung, NgayHetHan)
                VALUES (@TenKhuyenMai, @MaKhachHang, @DaDung, @NgayHetHan);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@TenKhuyenMai", khuyenMai.TenKhuyenMai);
            command.Parameters.AddWithValue("@MaKhachHang", khuyenMai.MaKhachHang);
            command.Parameters.AddWithValue("@DaDung", khuyenMai.DaDung);
            command.Parameters.AddWithValue("@NgayHetHan", khuyenMai.NgayHetHan);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        public async Task<int> UpdateKhuyenMai(KhuyenMai khuyenMai, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE KhuyenMais
                SET TenKhuyenMai = @TenKhuyenMai, MaKhachHang = @MaKhachHang, DaDung = @DaDung, NgayHetHan = @NgayHetHan
                WHERE MaKhuyenMai = @MaKhuyenMai";
            command.Parameters.AddWithValue("@MaKhuyenMai", khuyenMai.MaKhuyenMai);
            command.Parameters.AddWithValue("@TenKhuyenMai", khuyenMai.TenKhuyenMai);
            command.Parameters.AddWithValue("@MaKhachHang", khuyenMai.MaKhachHang);
            command.Parameters.AddWithValue("@DaDung", khuyenMai.DaDung);
            command.Parameters.AddWithValue("@NgayHetHan", khuyenMai.NgayHetHan);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteKhuyenMai(int maKhuyenMai, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM KhuyenMais WHERE MaKhuyenMai = @MaKhuyenMai";
            command.Parameters.AddWithValue("@MaKhuyenMai", maKhuyenMai);

            return await command.ExecuteNonQueryAsync();
        }
    }
}