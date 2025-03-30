namespace HeQuanTriDB.Respositoies.LuuTruRepository
{
    public interface ILuuTruRepository
    {
        string ConnectionString { get; }
        Task<List<LuuTru>> GetAllLuuTrus();
        Task<LuuTru> GetLuuTruByID(int maLuuTru);
        Task<int> AddLuuTru(LuuTru luuTru, SqlTransaction transaction);
        Task<int> UpdateLuuTru(LuuTru luuTru, SqlTransaction transaction);
        Task<int> DeleteLuuTru(int maLuuTru, SqlTransaction transaction);
    }
}


namespace HeQuanTriDB.Respositoies.LuuTruRepository
{
    public class LuuTruRepository : ILuuTruRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public LuuTruRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<LuuTru>> GetAllLuuTrus()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM LuuTrus";

            using var reader = await command.ExecuteReaderAsync();
            var luuTrus = new List<LuuTru>();
            while (await reader.ReadAsync())
            {
                var luuTru = new LuuTru
                {
                    MaLuuTru = reader.GetInt32(reader.GetOrdinal("MaLuuTru")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaNguyenLieu = reader.GetInt32(reader.GetOrdinal("MaNguyenLieu")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    NgayHetHan = reader.GetDateTime(reader.GetOrdinal("NgayHetHan"))
                };
                luuTrus.Add(luuTru);
            }
            return luuTrus;
        }

        public async Task<LuuTru> GetLuuTruByID(int maLuuTru)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM LuuTrus WHERE MaLuuTru = @MaLuuTru";
            command.Parameters.AddWithValue("@MaLuuTru", maLuuTru);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new LuuTru
                {
                    MaLuuTru = reader.GetInt32(reader.GetOrdinal("MaLuuTru")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaNguyenLieu = reader.GetInt32(reader.GetOrdinal("MaNguyenLieu")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    NgayHetHan = reader.GetDateTime(reader.GetOrdinal("NgayHetHan"))
                };
            }
            throw new KeyNotFoundException($"LuuTru with ID {maLuuTru} was not found.");
        }

        public async Task<int> AddLuuTru(LuuTru luuTru, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO LuuTrus (MaNhanVien, MaNguyenLieu, SoLuong, NgayHetHan)
                VALUES (@MaNhanVien, @MaNguyenLieu, @SoLuong, @NgayHetHan);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@MaNhanVien", luuTru.MaNhanVien);
            command.Parameters.AddWithValue("@MaNguyenLieu", luuTru.MaNguyenLieu);
            command.Parameters.AddWithValue("@SoLuong", luuTru.SoLuong);
            command.Parameters.AddWithValue("@NgayHetHan", luuTru.NgayHetHan);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        public async Task<int> UpdateLuuTru(LuuTru luuTru, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE LuuTrus
                SET MaNhanVien = @MaNhanVien, MaNguyenLieu = @MaNguyenLieu, 
                    SoLuong = @SoLuong, NgayHetHan = @NgayHetHan
                WHERE MaLuuTru = @MaLuuTru";
            command.Parameters.AddWithValue("@MaLuuTru", luuTru.MaLuuTru);
            command.Parameters.AddWithValue("@MaNhanVien", luuTru.MaNhanVien);
            command.Parameters.AddWithValue("@MaNguyenLieu", luuTru.MaNguyenLieu);
            command.Parameters.AddWithValue("@SoLuong", luuTru.SoLuong);
            command.Parameters.AddWithValue("@NgayHetHan", luuTru.NgayHetHan);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteLuuTru(int maLuuTru, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM LuuTrus WHERE MaLuuTru = @MaLuuTru";
            command.Parameters.AddWithValue("@MaLuuTru", maLuuTru);

            return await command.ExecuteNonQueryAsync();
        }
    }
}