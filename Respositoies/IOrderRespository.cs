namespace HeQuanTriDB.repositories
{
    public interface IOrderRepository
    {
        string ConnectionString { get; }
        Task<int> AddOrder(HoaDon hoaDon, SqlConnection connection, SqlTransaction transaction);
        Task<int> AddOrderDetail(ChiTietHoaDon chiTietHoaDon, SqlConnection connection, SqlTransaction transaction);
    }
}