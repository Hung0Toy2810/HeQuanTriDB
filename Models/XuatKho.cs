namespace HeQuanTriDB.Models
{
    public class XuatKho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaXuatKho { get; set; }
        [Required]
        public int MaNhanVien { get; set; }
        [Required]
        public int MaNguyenLieu { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public DateTime NgayXuat { get; set; }
        [Required]
        public string NguyenNhanXuatKho { get; set; } = string.Empty;
        [ForeignKey("MaNhanVien")]
        public NhanVien NhanVien { get; set; } = null!;
        [ForeignKey("MaNguyenLieu")]
        public NguyenLieu NguyenLieu { get; set; } = null!;
        [Required]
        public int MaLuuTru { get; set; }
        [ForeignKey("MaLuuTru")]
        public LuuTru LuuTru { get; set; } = null!;
    }
}