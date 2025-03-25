namespace HeQuanTriDB.Models
{
    public class ChiTietHoaDon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChiTietHoaDon { get; set; }
        [Required]
        public int MaHoaDon { get; set; }
        [Required]
        public int MaMonAn { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public double ThanhTien { get; set; }
        [ForeignKey("MaHoaDon")]
        public  HoaDon HoaDon { get; set; } = null!;
        [ForeignKey("MaMonAn")]
        public  MonAn MonAn { get; set; } = null!;
    }
    
}