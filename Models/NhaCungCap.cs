namespace HeQuanTriDB.Models
{
    public class NhaCungCap
    {
        [Key]
        //auto increment
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhaCungCap { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenNhaCungCap { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string DiaChi { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string SoDienThoai { get; set; } = string.Empty;
        [Required]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;
        public ICollection<NguyenLieu> NguyenLieus { get; set; } = new List<NguyenLieu>();
    }
}