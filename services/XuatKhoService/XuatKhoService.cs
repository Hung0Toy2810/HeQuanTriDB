using HeQuanTriDB.Models;
using HeQuanTriDB.Repositories.XuatKhoRepository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HeQuanTriDB.Services
{
    public interface IXuatKhoService
    {
        Task<List<XuatKho>> XuatKhoNguyenLieuAsync(int maNguyenLieu, int soLuongCanXuat, int maNhanVien, string nguyenNhanXuatKho);
    }

    public class XuatKhoService : IXuatKhoService
    {
        private readonly IXuatKhoRepository _xuatKhoRepository;

        public XuatKhoService(IXuatKhoRepository xuatKhoRepository)
        {
            _xuatKhoRepository = xuatKhoRepository ?? throw new ArgumentNullException(nameof(xuatKhoRepository));
        }

        public async Task<List<XuatKho>> XuatKhoNguyenLieuAsync(int maNguyenLieu, int soLuongCanXuat, int maNhanVien, string nguyenNhanXuatKho)
        {
            if (soLuongCanXuat <= 0)
                throw new ArgumentException("Số lượng cần xuất phải lớn hơn 0.");

            List<XuatKho> danhSachXuatKho = new List<XuatKho>();
            int soLuongConLai = soLuongCanXuat;

            if (string.IsNullOrEmpty(_xuatKhoRepository.ConnectionString))
                throw new InvalidOperationException("Chuỗi kết nối không được cấu hình.");

            using var connection = new SqlConnection(_xuatKhoRepository.ConnectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

            try
            {
                while (soLuongConLai > 0)
                {
                    // Lấy lô hàng gần hết hạn nhất
                    var luuTru = await _xuatKhoRepository.GetLuuTruGanHetHanAsync(maNguyenLieu, connection, transaction);
                    if (luuTru == null)
                    {
                        throw new InvalidOperationException("Không đủ nguyên liệu trong kho để xuất!");
                    }

                    // Tạo bản ghi xuất kho
                    var xuatKho = new XuatKho
                    {
                        MaNhanVien = maNhanVien,
                        MaNguyenLieu = maNguyenLieu,
                        NgayXuat = DateTime.UtcNow,
                        NguyenNhanXuatKho = nguyenNhanXuatKho,
                        MaLuuTru = luuTru.MaLuuTru
                    };

                    // Xử lý số lượng
                    if (soLuongConLai >= luuTru.SoLuong)
                    {
                        xuatKho.SoLuong = luuTru.SoLuong;
                        soLuongConLai -= luuTru.SoLuong;
                        luuTru.SoLuong = 0;
                    }
                    else
                    {
                        xuatKho.SoLuong = soLuongConLai;
                        luuTru.SoLuong -= soLuongConLai;
                        soLuongConLai = 0;
                    }

                    // Lưu xuất kho
                    xuatKho.MaXuatKho = await _xuatKhoRepository.CreateXuatKhoAsync(xuatKho, connection, transaction);
                    danhSachXuatKho.Add(xuatKho);

                    // Cập nhật hoặc xóa lô trữ
                    if (luuTru.SoLuong == 0)
                    {
                        await _xuatKhoRepository.DeleteLuuTruAsync(luuTru.MaLuuTru, connection, transaction);
                    }
                    else
                    {
                        await _xuatKhoRepository.UpdateLuuTruAsync(luuTru, connection, transaction);
                    }
                }

                await transaction.CommitAsync();
                return danhSachXuatKho;
            }
            catch (Exception ex)
            {
                try
                {
                    await transaction.RollbackAsync();
                }
                catch (Exception rollbackEx)
                {
                    throw new Exception($"Lỗi khi xuất kho: {ex.Message}. Rollback thất bại: {rollbackEx.Message}", ex);
                }
                throw new Exception($"Lỗi khi xuất kho: {ex.Message}", ex);
            }
        }
    }
}