using System.Data;
namespace HeQuanTriDB.Repositoies.NhanVienRepository
{
    public interface INhanVienRepository
    {
        string ConnectionString { get; }
        Task<List<NhanVien>> GetAllNhanViens();
        Task<NhanVien> GetNhanVienByID(int maNhanVien);
        Task<int> AddNhanVien(NhanVien nhanVien, SqlTransaction transaction);
        Task<int> UpdateNhanVien(NhanVien nhanVien, SqlTransaction transaction);
        Task<int> DeleteNhanVien(int maNhanVien, SqlTransaction transaction);
    }
}


namespace HeQuanTriDB.Repositoies.NhanVienRepository
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public NhanVienRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<NhanVien>> GetAllNhanViens()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NhanViens";

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

        public async Task<NhanVien> GetNhanVienByID(int maNhanVien)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NhanViens WHERE MaNhanVien = @MaNhanVien";
            command.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new NhanVien
                {
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    TenNhanVien = reader.GetString(reader.GetOrdinal("TenNhanVien")),
                    DiaChi = reader.GetString(reader.GetOrdinal("DiaChi")),
                    SoDienThoai = reader.GetString(reader.GetOrdinal("SoDienThoai")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    MatKhau = reader.GetString(reader.GetOrdinal("MatKhau")),
                    MaChucVu = reader.GetInt32(reader.GetOrdinal("MaChucVu"))
                };
            }
            throw new KeyNotFoundException($"NhanVien with ID {maNhanVien} was not found.");
        }

        public async Task<int> AddNhanVien(NhanVien nhanVien, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO NhanViens (TenNhanVien, DiaChi, SoDienThoai, Email, MatKhau, MaChucVu)
                VALUES (@TenNhanVien, @DiaChi, @SoDienThoai, @Email, @MatKhau, @MaChucVu);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@TenNhanVien", nhanVien.TenNhanVien);
            command.Parameters.AddWithValue("@DiaChi", nhanVien.DiaChi);
            command.Parameters.AddWithValue("@SoDienThoai", nhanVien.SoDienThoai);
            command.Parameters.AddWithValue("@Email", nhanVien.Email);
            command.Parameters.AddWithValue("@MatKhau", nhanVien.MatKhau);
            command.Parameters.AddWithValue("@MaChucVu", nhanVien.MaChucVu);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        public async Task<int> UpdateNhanVien(NhanVien nhanVien, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE NhanViens
                SET TenNhanVien = @TenNhanVien, DiaChi = @DiaChi, SoDienThoai = @SoDienThoai, 
                    Email = @Email, MatKhau = @MatKhau, MaChucVu = @MaChucVu
                WHERE MaNhanVien = @MaNhanVien";
            command.Parameters.AddWithValue("@MaNhanVien", nhanVien.MaNhanVien);
            command.Parameters.AddWithValue("@TenNhanVien", nhanVien.TenNhanVien);
            command.Parameters.AddWithValue("@DiaChi", nhanVien.DiaChi);
            command.Parameters.AddWithValue("@SoDienThoai", nhanVien.SoDienThoai);
            command.Parameters.AddWithValue("@Email", nhanVien.Email);
            command.Parameters.AddWithValue("@MatKhau", nhanVien.MatKhau);
            command.Parameters.AddWithValue("@MaChucVu", nhanVien.MaChucVu);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteNhanVien(int maNhanVien, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM NhanViens WHERE MaNhanVien = @MaNhanVien";
            command.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

            return await command.ExecuteNonQueryAsync();
        }
    }
}