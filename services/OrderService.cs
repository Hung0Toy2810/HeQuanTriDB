namespace HeQuanTriDB.services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<int> Order(int maNhanVien, int maKhachHang, List<OrderDTO> orderItems)
        {
            if (orderItems == null || !orderItems.Any())
                throw new ArgumentException("Danh sách món ăn không được trống.");

            double tongTien = orderItems.Sum(item => item.Gia * item.SoLuong);

            var hoaDon = new HoaDon
            {
                MaNhanVien = maNhanVien,
                MaKhachHang = maKhachHang,
                NgayLap = DateTime.UtcNow,
                TongTien = tongTien
            };

            if (string.IsNullOrEmpty(_orderRepository.ConnectionString))
                throw new InvalidOperationException("Chuỗi kết nối không được cấu hình.");

            using var connection = new SqlConnection(_orderRepository.ConnectionString);
            await connection.OpenAsync();
            using var transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

            try
            {
                int maHoaDon = await _orderRepository.AddOrder(hoaDon, transaction);

                foreach (var item in orderItems)
                {
                    if (item.SoLuong > item.SoLuongHienCo)
                        throw new InvalidOperationException($"Số lượng yêu cầu ({item.SoLuong}) vượt quá số lượng hiện có ({item.SoLuongHienCo}) cho món {item.TenMonAn}.");

                    var chiTietHoaDon = new ChiTietHoaDon
                    {
                        MaHoaDon = maHoaDon,
                        MaMonAn = item.MaMonAn,
                        SoLuong = item.SoLuong,
                        ThanhTien = item.Gia * item.SoLuong
                    };
                    await _orderRepository.AddOrderDetail(chiTietHoaDon, transaction);
                }

                await transaction.CommitAsync();
                return maHoaDon;
            }
            catch (Exception ex)
            {
                try
                {
                    await transaction.RollbackAsync();
                }
                catch (Exception rollbackEx)
                {
                    throw new Exception($"Lỗi khi tạo đơn hàng: {ex.Message}. Rollback thất bại: {rollbackEx.Message}", ex);
                }
                throw new Exception($"Lỗi khi tạo đơn hàng: {ex.Message}", ex);
            }
        }
    }
}