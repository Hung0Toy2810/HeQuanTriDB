using HeQuanTriDB.Models.dto;

namespace HeQuanTriDB.services
{
    public interface IOrderService
    {
        Task<int> Order(int maNhanVien, int maKhachHang, List<OrderDTO> orderItems);

    }
    

}