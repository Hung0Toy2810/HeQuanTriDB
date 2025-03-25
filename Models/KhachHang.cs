namespace HeQuanTriDB.Models
{
    public class KhachHang
    {
        [Key]
        //auto increment
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKhachHang { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenKhachHang { get; set; } = string.Empty;
        public string? DiaChi { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string SoDienThoai { get; set; } = string.Empty;
        public ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
        public ICollection<KhuyenMai> KhuyenMais { get; set; } = new List<KhuyenMai>();
    }
}