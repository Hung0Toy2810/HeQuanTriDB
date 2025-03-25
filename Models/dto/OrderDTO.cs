namespace HeQuanTriDB.Models.dto
{
    public class OrderDTO
    {
        public int MaMonAn { get; set; }
        public string TenMonAn { get; set; } = string.Empty;
        public double Gia { get; set; }
        public int SoLuongHienCo { get; set; }
        public int SoLuong { get; set; }
    }
}