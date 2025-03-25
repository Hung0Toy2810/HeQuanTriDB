namespace HeQuanTriDB.Models
{
    public class ChucVu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaChucVu { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenChucVu { get; set; } = string.Empty;
        public ICollection<NhanVien> NhanViens { get; set; } = new List<NhanVien>();
    }
}