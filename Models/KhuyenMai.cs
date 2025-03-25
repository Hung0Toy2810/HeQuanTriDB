namespace HeQuanTriDB.Models
{
    public class KhuyenMai
    {
        [Key]
        //auto increment
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaKhuyenMai { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenKhuyenMai { get; set; } = string.Empty;
        [Required]
        public int MaKhachHang { get; set; }
        [Required]
        public bool DaDung { get; set; }
        [Required]
        public DateTime NgayHetHan { get; set; }
        [ForeignKey("MaKhachHang")]
        public KhachHang KhachHang { get; set; } = null!;
    }
}