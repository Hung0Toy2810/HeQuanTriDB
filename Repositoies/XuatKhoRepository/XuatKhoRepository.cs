using HeQuanTriDB.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Threading.Tasks;
// IConfiguration
using Microsoft.Extensions.Configuration;

namespace HeQuanTriDB.Repositories.XuatKhoRepository
{
    public interface IXuatKhoRepository
    {
        string ConnectionString { get; }
        Task<LuuTru?> GetLuuTruGanHetHanAsync(int maNguyenLieu, SqlConnection connection, SqlTransaction transaction);
        Task<int> CreateXuatKhoAsync(XuatKho xuatKho, SqlConnection connection, SqlTransaction transaction);
        Task UpdateLuuTruAsync(LuuTru luuTru, SqlConnection connection, SqlTransaction transaction);
        Task DeleteLuuTruAsync(int maLuuTru, SqlConnection connection, SqlTransaction transaction);
    }

    public class XuatKhoRepository : IXuatKhoRepository
    {
        private readonly string _connectionString;

        public string ConnectionString => _connectionString;

        public XuatKhoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? throw new ArgumentNullException(nameof(configuration));
        }

        public async Task<LuuTru?> GetLuuTruGanHetHanAsync(int maNguyenLieu, SqlConnection connection, SqlTransaction transaction)
        {
            string sql = "SELECT TOP 1 * FROM LuuTrus WHERE MaNguyenLieu = @MaNguyenLieu AND SoLuong > 0 ORDER BY NgayHetHan ASC";
            using (var command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.AddWithValue("@MaNguyenLieu", maNguyenLieu);
                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        return new LuuTru
                        {
                            MaLuuTru = reader.GetInt32("MaLuuTru"),
                            MaNhanVien = reader.GetInt32("MaNhanVien"),
                            MaNguyenLieu = reader.GetInt32("MaNguyenLieu"),
                            SoLuong = reader.GetInt32("SoLuong"),
                            NgayHetHan = reader.GetDateTime("NgayHetHan")
                        };
                    }
                    return null; // Explicitly returning null for nullable type
                }
            }
        }

        public async Task<int> CreateXuatKhoAsync(XuatKho xuatKho, SqlConnection connection, SqlTransaction transaction)
        {
            string sql = @"INSERT INTO XuatKhos (MaNhanVien, MaNguyenLieu, SoLuong, NgayXuat, NguyenNhanXuatKho, MaLuuTru)
                           VALUES (@MaNhanVien, @MaNguyenLieu, @SoLuong, @NgayXuat, @NguyenNhanXuatKho, @MaLuuTru);
                           SELECT SCOPE_IDENTITY();";
            using (var command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.AddWithValue("@MaNhanVien", xuatKho.MaNhanVien);
                command.Parameters.AddWithValue("@MaNguyenLieu", xuatKho.MaNguyenLieu);
                command.Parameters.AddWithValue("@SoLuong", xuatKho.SoLuong);
                command.Parameters.AddWithValue("@NgayXuat", xuatKho.NgayXuat);
                command.Parameters.AddWithValue("@NguyenNhanXuatKho", xuatKho.NguyenNhanXuatKho);
                command.Parameters.AddWithValue("@MaLuuTru", xuatKho.MaLuuTru);
                return Convert.ToInt32(await command.ExecuteScalarAsync());
            }
        }

        public async Task UpdateLuuTruAsync(LuuTru luuTru, SqlConnection connection, SqlTransaction transaction)
        {
            string sql = "UPDATE LuuTrus SET SoLuong = @SoLuong WHERE MaLuuTru = @MaLuuTru";
            using (var command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.AddWithValue("@SoLuong", luuTru.SoLuong);
                command.Parameters.AddWithValue("@MaLuuTru", luuTru.MaLuuTru);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task DeleteLuuTruAsync(int maLuuTru, SqlConnection connection, SqlTransaction transaction)
        {
            string sql = "DELETE FROM LuuTrus WHERE MaLuuTru = @MaLuuTru";
            using (var command = new SqlCommand(sql, connection, transaction))
            {
                command.Parameters.AddWithValue("@MaLuuTru", maLuuTru);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}