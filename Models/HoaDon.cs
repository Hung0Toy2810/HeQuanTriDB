namespace HeQuanTriDB.Models
{
    public class HoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaHoaDon { get; set; }
        [Required]
        public int MaNhanVien { get; set; }
        [Required]
        public int MaKhachHang { get; set; }
        [Required]
        public DateTime NgayLap { get; set; }
        [Required]
        public double TongTien { get; set; }
        [ForeignKey("MaNhanVien")]
        public NhanVien NhanVien { get; set; } = null!;
        [ForeignKey("MaKhachHang")]
        public KhachHang KhachHang { get; set; } = null!;
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();
    }
}