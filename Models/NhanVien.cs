using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HeQuanTriDB.Models
{
    public class NhanVien
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MaNhanVien { get; set; }

        [Required]
        public string TenNhanVien { get; set; } = string.Empty;

        [Required]
        public string DiaChi { get; set; } = string.Empty;

        [Required]
        public string SoDienThoai { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string MatKhau { get; set; } = string.Empty;
        [Required]
        public int MaChucVu { get; set; }

        [ForeignKey("MaChucVu")]
        public ChucVu ChucVu { get; set; } = null!;

        public ICollection<HoaDon> HoaDons { get; set; } = new List<HoaDon>();
        public ICollection<NhapKho> NhapKhos { get; set; } = new List<NhapKho>();
        public ICollection<LuuTru> LuuTrus { get; set; } = new List<LuuTru>();
        public ICollection<XuatKho> XuatKhos { get; set; } = new List<XuatKho>();
    }
}