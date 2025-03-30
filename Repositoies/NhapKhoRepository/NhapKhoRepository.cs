using System.Data;

namespace HeQuanTriDB.Repositoies.NhapKhoRepository
{
    public interface INhapKhoRepository
    {
        string ConnectionString { get; }
        Task<List<NhapKho>> GetAllNhapKhos();
        Task<NhapKho> GetNhapKhoByID(int maNhapKho);
        Task<int> AddNhapKho(NhapKho nhapKho, SqlTransaction transaction );
        Task<int> UpdateNhapKho(NhapKho nhapKho, SqlTransaction transaction );
        Task<int> DeleteNhapKho(int maNhapKho, SqlTransaction transaction);
    }
}


namespace HeQuanTriDB.Repositoies.NhapKhoRepository
{
    public class NhapKhoRepository : INhapKhoRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public NhapKhoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<NhapKho>> GetAllNhapKhos()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NhapKhos";

            using var reader = await command.ExecuteReaderAsync();
            var nhapKhos = new List<NhapKho>();
            while (await reader.ReadAsync())
            {
                var nhapKho = new NhapKho
                {
                    MaNhapKho = reader.GetInt32(reader.GetOrdinal("MaNhapKho")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaNguyenLieu = reader.GetInt32(reader.GetOrdinal("MaNguyenLieu")),
                    NgayNhap = reader.GetDateTime(reader.GetOrdinal("NgayNhap")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    TongTien = reader.GetDouble(reader.GetOrdinal("TongTien")),
                    SoNgayHetHan = reader.GetInt32(reader.GetOrdinal("SoNgayHetHan"))
                };
                nhapKhos.Add(nhapKho);
            }
            return nhapKhos;
        }

        public async Task<NhapKho> GetNhapKhoByID(int maNhapKho)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NhapKhos WHERE MaNhapKho = @MaNhapKho";
            command.Parameters.AddWithValue("@MaNhapKho", maNhapKho);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new NhapKho
                {
                    MaNhapKho = reader.GetInt32(reader.GetOrdinal("MaNhapKho")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaNguyenLieu = reader.GetInt32(reader.GetOrdinal("MaNguyenLieu")),
                    NgayNhap = reader.GetDateTime(reader.GetOrdinal("NgayNhap")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    TongTien = reader.GetDouble(reader.GetOrdinal("TongTien")),
                    SoNgayHetHan = reader.GetInt32(reader.GetOrdinal("SoNgayHetHan"))
                };
            }
            throw new KeyNotFoundException($"NhapKho with ID {maNhapKho} was not found.");
        }

        public async Task<int> AddNhapKho(NhapKho nhapKho, SqlTransaction transaction )
        {
            using var connection = transaction?.Connection ?? new SqlConnection(_connectionString);
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            command.CommandText = @"
                INSERT INTO NhapKhos (MaNhanVien, MaNguyenLieu, NgayNhap, SoLuong, TongTien, SoNgayHetHan)
                VALUES (@MaNhanVien, @MaNguyenLieu, @NgayNhap, @SoLuong, @TongTien, @SoNgayHetHan);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@MaNhanVien", nhapKho.MaNhanVien);
            command.Parameters.AddWithValue("@MaNguyenLieu", nhapKho.MaNguyenLieu);
            command.Parameters.AddWithValue("@NgayNhap", nhapKho.NgayNhap);
            command.Parameters.AddWithValue("@SoLuong", nhapKho.SoLuong);
            command.Parameters.AddWithValue("@TongTien", nhapKho.TongTien);
            command.Parameters.AddWithValue("@SoNgayHetHan", nhapKho.SoNgayHetHan);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        public async Task<int> UpdateNhapKho(NhapKho nhapKho, SqlTransaction transaction )
        {
            using var connection = transaction?.Connection ?? new SqlConnection(_connectionString);
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            command.CommandText = @"
                UPDATE NhapKhos
                SET MaNhanVien = @MaNhanVien, MaNguyenLieu = @MaNguyenLieu, NgayNhap = @NgayNhap, 
                    SoLuong = @SoLuong, TongTien = @TongTien, SoNgayHetHan = @SoNgayHetHan
                WHERE MaNhapKho = @MaNhapKho";
            command.Parameters.AddWithValue("@MaNhapKho", nhapKho.MaNhapKho);
            command.Parameters.AddWithValue("@MaNhanVien", nhapKho.MaNhanVien);
            command.Parameters.AddWithValue("@MaNguyenLieu", nhapKho.MaNguyenLieu);
            command.Parameters.AddWithValue("@NgayNhap", nhapKho.NgayNhap);
            command.Parameters.AddWithValue("@SoLuong", nhapKho.SoLuong);
            command.Parameters.AddWithValue("@TongTien", nhapKho.TongTien);
            command.Parameters.AddWithValue("@SoNgayHetHan", nhapKho.SoNgayHetHan);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteNhapKho(int maNhapKho, SqlTransaction transaction )
        {
            using var connection = transaction?.Connection ?? new SqlConnection(_connectionString);
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            if (transaction != null)
            {
                command.Transaction = transaction;
            }

            command.CommandText = "DELETE FROM NhapKhos WHERE MaNhapKho = @MaNhapKho";
            command.Parameters.AddWithValue("@MaNhapKho", maNhapKho);

            return await command.ExecuteNonQueryAsync();
        }
    }
}