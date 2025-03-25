namespace HeQuanTriDB.Models
{
    public class NguyenLieu
    {
        [Key]
        //auto increment
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNguyenLieu { get; set; }
        [Required]
        [MaxLength(100)]
        public string TenNguyenLieu { get; set; } = string.Empty;
        [Required]
        [MaxLength(20)]
        public string DonViTinh { get; set; } = string.Empty;
        [Required]
        public double Gia { get; set; }
        [Required]
        public int MaNhaCungCap { get; set; }
        [ForeignKey("MaNhaCungCap")]
        public NhaCungCap NhaCungCap { get; set; } = null!;
        public ICollection<NhapKho> NhapKhos { get; set; } = new List<NhapKho>();
        public ICollection<LuuTru> LuuTrus { get; set; } = new List<LuuTru>();
        public ICollection<XuatKho> XuatKhos { get; set; } = new List<XuatKho>();
    }
}