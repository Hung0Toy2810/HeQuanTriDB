namespace HeQuanTriDB.Models
{
    public class LuuTru
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaLuuTru { get; set; }
        [Required]
        public int MaNhanVien { get; set; }
        [Required]
        public int MaNguyenLieu { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public DateTime NgayHetHan { get; set; }
        [ForeignKey("MaNhanVien")]
        public NhanVien NhanVien { get; set; } = null!;
        [ForeignKey("MaNguyenLieu")]
        public NguyenLieu NguyenLieu { get; set; } = null!;
        public ICollection<XuatKho> XuatKhos { get; set; } = new List<XuatKho>();
    }
}