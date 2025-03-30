namespace HeQuanTriDB.Respositoies.XuatKhoRepository
{
    //interface
    public interface IXuatKhoRepository
    {
        string ConnectionString { get; }
        Task<List<XuatKho>> GetAllXuatKhos();
        Task<XuatKho> GetXuatKhoByID(int maXuatKho);
        // with transaction
        Task<int> AddXuatKho(XuatKho xuatKho, SqlTransaction transaction);
        Task<int> UpdateXuatKho(XuatKho xuatKho, SqlTransaction transaction);
        Task<int> DeleteXuatKho(int maXuatKho, SqlTransaction transaction);
    }
}


namespace HeQuanTriDB.Respositoies.XuatKhoRepository
{
    public class XuatKhoRepository : IXuatKhoRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public XuatKhoRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<XuatKho>> GetAllXuatKhos()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM XuatKhos";

            using var reader = await command.ExecuteReaderAsync();
            var xuatKhos = new List<XuatKho>();
            while (await reader.ReadAsync())
            {
                var xuatKho = new XuatKho
                {
                    MaXuatKho = reader.GetInt32(reader.GetOrdinal("MaXuatKho")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaNguyenLieu = reader.GetInt32(reader.GetOrdinal("MaNguyenLieu")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    NgayXuat = reader.GetDateTime(reader.GetOrdinal("NgayXuat")),
                    NguyenNhanXuatKho = reader.GetString(reader.GetOrdinal("NguyenNhanXuatKho")),
                    MaLuuTru = reader.GetInt32(reader.GetOrdinal("MaLuuTru"))
                };
                xuatKhos.Add(xuatKho);
            }
            return xuatKhos;
        }

        public async Task<XuatKho> GetXuatKhoByID(int maXuatKho)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM XuatKhos WHERE MaXuatKho = @MaXuatKho";
            command.Parameters.AddWithValue("@MaXuatKho", maXuatKho);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new XuatKho
                {
                    MaXuatKho = reader.GetInt32(reader.GetOrdinal("MaXuatKho")),
                    MaNhanVien = reader.GetInt32(reader.GetOrdinal("MaNhanVien")),
                    MaNguyenLieu = reader.GetInt32(reader.GetOrdinal("MaNguyenLieu")),
                    SoLuong = reader.GetInt32(reader.GetOrdinal("SoLuong")),
                    NgayXuat = reader.GetDateTime(reader.GetOrdinal("NgayXuat")),
                    NguyenNhanXuatKho = reader.GetString(reader.GetOrdinal("NguyenNhanXuatKho")),
                    MaLuuTru = reader.GetInt32(reader.GetOrdinal("MaLuuTru"))
                };
            }
            throw new KeyNotFoundException($"XuatKho with ID {maXuatKho} was not found.");
        }

        public async Task<int> AddXuatKho(XuatKho xuatKho, SqlTransaction transaction)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                INSERT INTO XuatKhos (MaNhanVien, MaNguyenLieu, SoLuong, NgayXuat, NguyenNhanXuatKho, MaLuuTru)
                VALUES (@MaNhanVien, @MaNguyenLieu, @SoLuong, @NgayXuat, @NguyenNhanXuatKho, @MaLuuTru);
                SELECT SCOPE_IDENTITY();";
            command.Parameters.AddWithValue("@MaNhanVien", xuatKho.MaNhanVien);
            command.Parameters.AddWithValue("@MaNguyenLieu", xuatKho.MaNguyenLieu);
            command.Parameters.AddWithValue("@SoLuong", xuatKho.SoLuong);
            command.Parameters.AddWithValue("@NgayXuat", xuatKho.NgayXuat);
            command.Parameters.AddWithValue("@NguyenNhanXuatKho", xuatKho.NguyenNhanXuatKho);
            command.Parameters.AddWithValue("@MaLuuTru", xuatKho.MaLuuTru);

            return Convert.ToInt32(await command.ExecuteScalarAsync());
        }
        

        public async Task<int> UpdateXuatKho(XuatKho xuatKho, SqlTransaction transaction)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = @"
                UPDATE XuatKhos
                SET MaNhanVien = @MaNhanVien, MaNguyenLieu = @MaNguyenLieu, SoLuong = @SoLuong, 
                    NgayXuat = @NgayXuat, NguyenNhanXuatKho = @NguyenNhanXuatKho, MaLuuTru = @MaLuuTru
                WHERE MaXuatKho = @MaXuatKho";
            command.Parameters.AddWithValue("@MaXuatKho", xuatKho.MaXuatKho);
            command.Parameters.AddWithValue("@MaNhanVien", xuatKho.MaNhanVien);
            command.Parameters.AddWithValue("@MaNguyenLieu", xuatKho.MaNguyenLieu);
            command.Parameters.AddWithValue("@SoLuong", xuatKho.SoLuong);
            command.Parameters.AddWithValue("@NgayXuat", xuatKho.NgayXuat);
            command.Parameters.AddWithValue("@NguyenNhanXuatKho", xuatKho.NguyenNhanXuatKho);
            command.Parameters.AddWithValue("@MaLuuTru", xuatKho.MaLuuTru);

            return await command.ExecuteNonQueryAsync();
        }
        

        public async Task<int> DeleteXuatKho(int maXuatKho, SqlTransaction transaction)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.Transaction = transaction;
            command.CommandText = "DELETE FROM XuatKhos WHERE MaXuatKho = @MaXuatKho";
            command.Parameters.AddWithValue("@MaXuatKho", maXuatKho);

            return await command.ExecuteNonQueryAsync();
        }
    }
}