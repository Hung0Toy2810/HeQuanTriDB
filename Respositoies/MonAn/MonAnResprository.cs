namespace HeQuanTriDB.repositories
{
    public class MonAnRespository : IMonAnRespository
    {
        private readonly string _connectionString;
        public string ConnectionString => _connectionString;

        public MonAnRespository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<MonAn>> GetAllMonAns()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM MonAns";

            using var reader = await command.ExecuteReaderAsync();
            var monAns = new List<MonAn>();
            while (await reader.ReadAsync())
            {
                var monAn = new MonAn
                {
                    MaMonAn = reader.GetInt32(reader.GetOrdinal("MaMonAn")),
                    TenMonAn = reader.GetString(reader.GetOrdinal("TenMonAn")),
                    Gia = reader.GetDouble(reader.GetOrdinal("Gia")),
                    SoLuongHienCo = reader.GetInt32(reader.GetOrdinal("SoLuongHienCo"))
                };
                monAns.Add(monAn);
            }
            return monAns;
        }
        public async Task<MonAn> GetMonAnByID(int maMonAn)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "SELECT * FROM MonAns WHERE MaMonAn = @MaMonAn";
            command.Parameters.AddWithValue("@MaMonAn", maMonAn);

            using var reader = await command.ExecuteReaderAsync();
            if (await reader.ReadAsync())
            {
                return new MonAn
                {
                    MaMonAn = reader.GetInt32(reader.GetOrdinal("MaMonAn")),
                    TenMonAn = reader.GetString(reader.GetOrdinal("TenMonAn")),
                    Gia = reader.GetDouble(reader.GetOrdinal("Gia")),
                    SoLuongHienCo = reader.GetInt32(reader.GetOrdinal("SoLuongHienCo"))
                };
            }
            throw new KeyNotFoundException($"MonAn with ID {maMonAn} was not found.");
        }
        public async Task<int> AddMonAn(MonAn monAn)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                INSERT INTO MonAns (TenMonAn, Gia, SoLuongHienCo)
                OUTPUT INSERTED.MaMonAn
                VALUES (@TenMonAn, @Gia, @SoLuongHienCo)";
            command.Parameters.AddWithValue("@TenMonAn", monAn.TenMonAn);
            command.Parameters.AddWithValue("@Gia", monAn.Gia);
            command.Parameters.AddWithValue("@SoLuongHienCo", monAn.SoLuongHienCo);

            var result = await command.ExecuteScalarAsync();
            return Convert.ToInt32(result);
        }
        public async Task<int> UpdateMonAn(MonAn monAn)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = @"
                UPDATE MonAns
                SET TenMonAn = @TenMonAn, Gia = @Gia, SoLuongHienCo = @SoLuongHienCo
                WHERE MaMonAn = @MaMonAn";
            command.Parameters.AddWithValue("@MaMonAn", monAn.MaMonAn);
            command.Parameters.AddWithValue("@TenMonAn", monAn.TenMonAn);
            command.Parameters.AddWithValue("@Gia", monAn.Gia);
            command.Parameters.AddWithValue("@SoLuongHienCo", monAn.SoLuongHienCo);

            return await command.ExecuteNonQueryAsync();
        }
        public async Task<int> DeleteMonAn(int maMonAn)
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = connection.CreateCommand();
            command.CommandText = "DELETE FROM MonAns WHERE MaMonAn = @MaMonAn";
            command.Parameters.AddWithValue("@MaMonAn", maMonAn);

            return await command.ExecuteNonQueryAsync();
        }
    }
}
