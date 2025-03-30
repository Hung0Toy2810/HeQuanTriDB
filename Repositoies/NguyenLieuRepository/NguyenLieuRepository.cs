using System.Data;
namespace HeQuanTriDB.Repositoies.NguyenLieuRepository
{
    public interface INguyenLieuRepository
    {
        string ConnectionString { get; }
        Task<List<NguyenLieu>> GetAllNguyenLieus();
        Task<NguyenLieu> GetNguyenLieuByID(int maNguyenLieu);
        Task<int> AddNguyenLieu(NguyenLieu nguyenLieu, SqlTransaction transaction);
        Task<int> UpdateNguyenLieu(NguyenLieu nguyenLieu, SqlTransaction transaction);
        Task<int> DeleteNguyenLieu(int maNguyenLieu, SqlTransaction transaction);
    }
}


namespace HeQuanTriDB.Repositoies.NguyenLieuRepository
{
    public class NguyenLieuRepository : INguyenLieuRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public NguyenLieuRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<NguyenLieu>> GetAllNguyenLieus()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NguyenLieus";

            using var reader = await command.ExecuteReaderAsync();
            var nguyenLieus = new List<NguyenLieu>();
            while (await reader.ReadAsync())
            {
                var nguyenLieu = new NguyenLieu
                {
                    MaNguyenLieu = reader.GetInt32(reader.GetOrdinal("MaNguyenLieu")),
                    TenNguyenLieu = reader.GetString(reader.GetOrdinal("TenNguyenLieu")),
                    DonViTinh = reader.GetString(reader.GetOrdinal("DonViTinh")),
                    Gia = reader.GetDouble(reader.GetOrdinal("Gia")),
                    MaNhaCungCap = reader.GetInt32(reader.GetOrdinal("MaNhaCungCap"))
                };
                nguyenLieus.Add(nguyenLieu);
            }
            return nguyenLieus;
        }

        public async Task<NguyenLieu> GetNguyenLieuByID(int maNguyenLieu)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM NguyenLieus WHERE MaNguyenLieu = @MaNguyenLieu";
            command.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new NguyenLieu
                {
                    MaNguyenLieu = reader.GetInt32(reader.GetOrdinal("MaNguyenLieu")),
                    TenNguyenLieu = reader.GetString(reader.GetOrdinal("TenNguyenLieu")),
                    DonViTinh = reader.GetString(reader.GetOrdinal("DonViTinh")),
                    Gia = reader.GetDouble(reader.GetOrdinal("Gia")),
                    MaNhaCungCap = reader.GetInt32(reader.GetOrdinal("MaNhaCungCap"))
                };
            }
            throw new KeyNotFoundException($"NguyenLieu with ID {maNguyenLieu} was not found.");
        }

        public async Task<int> AddNguyenLieu(NguyenLieu nguyenLieu, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO NguyenLieus (TenNguyenLieu, DonViTinh, Gia, MaNhaCungCap)
                VALUES (@TenNguyenLieu, @DonViTinh, @Gia, @MaNhaCungCap);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@TenNguyenLieu", nguyenLieu.TenNguyenLieu);
            command.Parameters.AddWithValue("@DonViTinh", nguyenLieu.DonViTinh);
            command.Parameters.AddWithValue("@Gia", nguyenLieu.Gia);
            command.Parameters.AddWithValue("@MaNhaCungCap", nguyenLieu.MaNhaCungCap);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }

        public async Task<int> UpdateNguyenLieu(NguyenLieu nguyenLieu, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE NguyenLieus
                SET TenNguyenLieu = @TenNguyenLieu, DonViTinh = @DonViTinh, Gia = @Gia, MaNhaCungCap = @MaNhaCungCap
                WHERE MaNguyenLieu = @MaNguyenLieu";
            command.Parameters.AddWithValue("@MaNguyenLieu", nguyenLieu.MaNguyenLieu);
            command.Parameters.AddWithValue("@TenNguyenLieu", nguyenLieu.TenNguyenLieu);
            command.Parameters.AddWithValue("@DonViTinh", nguyenLieu.DonViTinh);
            command.Parameters.AddWithValue("@Gia", nguyenLieu.Gia);
            command.Parameters.AddWithValue("@MaNhaCungCap", nguyenLieu.MaNhaCungCap);

            return await command.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteNguyenLieu(int maNguyenLieu, SqlTransaction transaction)
        {
            using var connection = transaction.Connection;
            if (connection.State != ConnectionState.Open)
            {
                await connection.OpenAsync();
            }

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM NguyenLieus WHERE MaNguyenLieu = @MaNguyenLieu";
            command.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);

            return await command.ExecuteNonQueryAsync();
        }
    }
}