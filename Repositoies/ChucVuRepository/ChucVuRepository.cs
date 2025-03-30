namespace HeQuanTriDB.Repositoies.ChucVuRepository
{
    public interface IChucVuRepository
    {
        string ConnectionString { get; }
        Task<List<ChucVu>> GetAllChucVus();
        Task<ChucVu> GetChucVuByID(int maChucVu);
        Task<int> AddChucVu(ChucVu chucVu, SqlTransaction transaction);
        Task<int> UpdateChucVu(ChucVu chucVu, SqlTransaction transaction);
        Task<int> DeleteChucVu(int maChucVu, SqlTransaction transaction);
        Task<List<NhanVien>> GetAllNhanViensByChucVuID(int maChucVu);
    }
}



namespace HeQuanTriDB.Repositoies.ChucVuRepository
{
    public class ChucVuRepository : IChucVuRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public ChucVuRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<ChucVu>> GetAllChucVus()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM ChucVus";

            using var reader = await command.ExecuteReaderAsync();
            var chucVus = new List<ChucVu>();
            while (await reader.ReadAsync())
            {
                var chucVu = new ChucVu
                {
                    MaChucVu = reader.GetInt32(reader.GetOrdinal("MaChucVu")),
                    TenChucVu = reader.GetString(reader.GetOrdinal("TenChucVu"))
                };
                chucVus.Add(chucVu);
            }
            return chucVus;
        }

        public async Task<ChucVu> GetChucVuByID(int maChucVu)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM ChucVus WHERE MaChucVu = @MaChucVu";
            command.Parameters.AddWithValue("@MaChucVu", maChucVu);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new ChucVu
                {
                    MaChucVu = reader.GetInt32(reader.GetOrdinal("MaChucVu")),
                    TenChucVu = reader.GetString(reader.GetOrdinal("TenChucVu"))
                };
            }
            throw new KeyNotFoundException($"ChucVu with ID {maChucVu} was not found.");
        }

        public async Task<int> AddChucVu(ChucVu chucVu, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO ChucVus (TenChucVu)
                VALUES (@TenChucVu);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@TenChucVu", chucVu.TenChucVu);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        public async Task<int> UpdateChucVu(ChucVu chucVu, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE ChucVus
                SET TenChucVu = @TenChucVu
                WHERE MaChucVu = @MaChucVu";
            command.Parameters.AddWithValue("@MaChucVu", chucVu.MaChucVu);
            command.Parameters.AddWithValue("@TenChucVu", chucVu.TenChucVu);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteChucVu(int maChucVu, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM ChucVus WHERE MaChucVu = @MaChucVu";
            command.Parameters.AddWithValue("@MaChucVu", maChucVu);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<List<NhanVien>> GetAllNhanViensByChucVuID(int maChucVu)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NhanViens WHERE MaChucVu = @MaChucVu";
            command.Parameters.AddWithValue("@MaChucVu", maChucVu);

            using var reader = await command.ExecuteReaderAsync();
            var nhanViens = new List<NhanVien>();
            while (await reader.ReadAsync())
            {
                var nhanVien = new NhanVien
                {
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    TenNhanVien = reader.GetString(reader.GetOrdinal("TenNhanVien")),
                    DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                    SoDienThoai = reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    MatKhau = reader.GetString(reader.GetOrdinal("MatKhau")),
                    MaChucVu = reader.GetInt32(reader.GetOrdinal("MaChucVu"))
                };
                nhanViens.Add(nhanVien);
            }
            return nhanViens;
        }
    }
}