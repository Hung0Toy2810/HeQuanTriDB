using System.Data;
namespace HeQuanTriDB.Respositoies.NhaCungCapRepository
{
    public interface INhaCungCapRepository
    {
        string ConnectionString { get; }
        Task<List<NhaCungCap>> GetAllNhaCungCaps();
        Task<NhaCungCap> GetNhaCungCapByID(int maNhaCungCap);
        Task<int> AddNhaCungCap(NhaCungCap nhaCungCap, SqlTransaction transaction);
        Task<int> UpdateNhaCungCap(NhaCungCap nhaCungCap, SqlTransaction transaction);
        Task<int> DeleteNhaCungCap(int maNhaCungCap, SqlTransaction transaction);
    }
}


namespace HeQuanTriDB.Respositoies.NhaCungCapRepository
{
    public class NhaCungCapRepository : INhaCungCapRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public NhaCungCapRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<NhaCungCap>> GetAllNhaCungCaps()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NhaCungCaps";

            using var reader = await command.ExecuteReaderAsync();
            var nhaCungCaps = new List<NhaCungCap>();
            while (await reader.ReadAsync())
            {
                var nhaCungCap = new NhaCungCap
                {
                    MaNhaCungCap = reader.GetInt32(reader.GetOrdinal("MaNhaCungCap")),
                    TenNhaCungCap = reader.GetString(reader.GetOrdinal("TenNhaCungCap")),
                    DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                    SoDienThoai = reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                };
                nhaCungCaps.Add(nhaCungCap);
            }
            return nhaCungCaps;
        }

        public async Task<NhaCungCap> GetNhaCungCapByID(int maNhaCungCap)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NhaCungCaps WHERE MaNhaCungCap = @MaNhaCungCap";
            command.Parameters.AddWithValue("@MaNhaCungCap", maNhaCungCap);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new NhaCungCap
                {
                    MaNhaCungCap = reader.GetInt32(reader.GetOrdinal("MaNhaCungCap")),
                    TenNhaCungCap = reader.GetString(reader.GetOrdinal("TenNhaCungCap")),
                    DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                    SoDienThoai = reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    Email = reader.GetString(reader.GetOrdinal("Email"))
                };
            }
            throw new KeyNotFoundException($"NhaCungCap with ID {maNhaCungCap} was not found.");
        }

        public async Task<int> AddNhaCungCap(NhaCungCap nhaCungCap, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO NhaCungCaps (TenNhaCungCap, DiaChi, SoDienThoai, Email)
                VALUES (@TenNhaCungCap, @DiaChi, @SoDienThoai, @Email);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@TenNhaCungCap", nhaCungCap.TenNhaCungCap);
            command.Parameters.AddWithValue("@DiaChi", nhaCungCap.DiaChi);
            command.Parameters.AddWithValue("@SoDienThoai", nhaCungCap.SoDienThoai);
            command.Parameters.AddWithValue("@Email", nhaCungCap.Email);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        public async Task<int> UpdateNhaCungCap(NhaCungCap nhaCungCap, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE NhaCungCaps
                SET TenNhaCungCap = @TenNhaCungCap, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, Email = @Email
                WHERE MaNhaCungCap = @MaNhaCungCap";
            command.Parameters.AddWithValue("@MaNhaCungCap", nhaCungCap.MaNhaCungCap);
            command.Parameters.AddWithValue("@TenNhaCungCap", nhaCungCap.TenNhaCungCap);
            command.Parameters.AddWithValue("@DiaChi", nhaCungCap.DiaChi);
            command.Parameters.AddWithValue("@SoDienThoai", nhaCungCap.SoDienThoai);
            command.Parameters.AddWithValue("@Email", nhaCungCap.Email);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteNhaCungCap(int maNhaCungCap, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM NhaCungCaps WHERE MaNhaCungCap = @MaNhaCungCap";
            command.Parameters.AddWithValue("@MaNhaCungCap", maNhaCungCap);

            return await command.ExecuteNonQueryAsync();
        }
    }
}