namespace HeQuanTriDB.Models
{
    public class MonAn
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaMonAn { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenMonAn { get; set; } = string.Empty;
        [Required]
        public double Gia { get; set; }
        [Required]
        public int SoLuongHienCo { get; set; }
        public ICollection<ChiTietHoaDon> ChiTietHoaDons { get; set; } = new List<ChiTietHoaDon>();

    }
}