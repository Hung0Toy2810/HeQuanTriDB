namespace HeQuanTriDB.Models
{
    public class NhapKho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhapKho { get; set; }
        [Required]
        public int MaNhanVien { get; set; }
        [Required]
        public int MaNguyenLieu { get; set; }
        [Required]
        public DateTime NgayNhap { get; set; }
        [Required]
        public int SoLuong { get; set; }
        [Required]
        public double TongTien { get; set; }
        [Required]
        public int SoNgayHetHan { get; set; }
        [ForeignKey("MaNhanVien")]
        public NhanVien NhanVien { get; set; } = null!;
        [ForeignKey("MaNguyenLieu")]
        public NguyenLieu NguyenLieu { get; set; } = null!;
    }
}