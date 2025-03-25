namespace HeQuanTriDB.Models
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options){}
        public DbSet<MonAn> MonAns { get; set; } = null!;
        public DbSet<NhapKho> NhapKhos { get; set; } = null!;
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; } = null!;
        public DbSet<ChucVu> ChucVus { get; set; } = null!;
        public DbSet<HoaDon> HoaDons { get; set; } = null!;
        public DbSet<NhanVien> NhanViens { get; set; } = null!;
        public DbSet<NguyenLieu> NguyenLieus { get; set; } = null!;
        public DbSet<KhachHang> KhachHangs { get; set; } = null!;
        public DbSet<LuuTru> LuuTrus { get; set; } = null!;
        public DbSet<XuatKho> XuatKhos { get; set; } = null!;
        public DbSet<NhaCungCap> NhaCungCaps { get; set; } = null!;
        public DbSet<KhuyenMai> KhuyenMais { get; set; } = null!;
        

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(RelationalEventId.PendingModelChangesWarning));
        // }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ChiTietHoaDon>()
                .HasOne(x => x.HoaDon)
                .WithMany(x => x.ChiTietHoaDons)
                .HasForeignKey(x => x.MaHoaDon)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaHoaDon
            modelBuilder.Entity<ChiTietHoaDon>()
                .HasIndex(x => x.MaHoaDon);
            
            //HoaDon 1-n NhanVien
            modelBuilder.Entity<HoaDon>()
                .HasOne(x => x.NhanVien)
                .WithMany(x => x.HoaDons)
                .HasForeignKey(x => x.MaNhanVien)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaNhanVien
            modelBuilder.Entity<HoaDon>()
                .HasIndex(x => x.MaNhanVien);
            

            //HoaDon 1-n KhachHang
            modelBuilder.Entity<HoaDon>()
                .HasOne(x => x.KhachHang)
                .WithMany(x => x.HoaDons)
                .HasForeignKey(x => x.MaKhachHang)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaKhachHang
            modelBuilder.Entity<HoaDon>()
                .HasIndex(x => x.MaKhachHang);
            
            // KhuyenMai 1-n KhachHang
            modelBuilder.Entity<KhuyenMai>()
                .HasOne(x => x.KhachHang)
                .WithMany(x => x.KhuyenMais)
                .HasForeignKey(x => x.MaKhachHang)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaKhachHang
            modelBuilder.Entity<KhuyenMai>()
                .HasIndex(x => x.MaKhachHang);
            // ChucVu 1-n NhanVien


            modelBuilder.Entity<NhanVien>()
                .HasOne(x => x.ChucVu)
                .WithMany(x => x.NhanViens)
                .HasForeignKey(x => x.MaChucVu)
                .OnDelete(DeleteBehavior.NoAction);
                
            // index for MaChucVu
            modelBuilder.Entity<NhanVien>()
                .HasIndex(x => x.MaChucVu);

            // ChiTietHoaDon 1-n MonAn
            modelBuilder.Entity<ChiTietHoaDon>()
                .HasOne(x => x.MonAn)
                .WithMany(x => x.ChiTietHoaDons)
                .HasForeignKey(x => x.MaMonAn)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaMonAn
            modelBuilder.Entity<ChiTietHoaDon>()
                .HasIndex(x => x.MaMonAn);
            

            // NhapKho 1-n NhanVien
            modelBuilder.Entity<NhapKho>()
                .HasOne(x => x.NhanVien)
                .WithMany(x => x.NhapKhos)
                .HasForeignKey(x => x.MaNhanVien)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaNhanVien
            modelBuilder.Entity<NhapKho>()
                .HasIndex(x => x.MaNhanVien);

            // LuuTru 1-n NhanVien
            modelBuilder.Entity<LuuTru>()
                .HasOne(x => x.NhanVien)
                .WithMany(x => x.LuuTrus)
                .HasForeignKey(x => x.MaNhanVien)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaNhanVien
            modelBuilder.Entity<LuuTru>()
                .HasIndex(x => x.MaNhanVien);
            // XuatKho 1-n NhanVien
            modelBuilder.Entity<XuatKho>()
                .HasOne(x => x.NhanVien)
                .WithMany(x => x.XuatKhos)
                .HasForeignKey(x => x.MaNhanVien)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaNhanVien
            modelBuilder.Entity<XuatKho>()
                .HasIndex(x => x.MaNhanVien);
            
            // NhaCungCap 1-n NguyenLieu
            modelBuilder.Entity<NguyenLieu>()
                .HasOne(x => x.NhaCungCap)
                .WithMany(x => x.NguyenLieus)
                .HasForeignKey(x => x.MaNhaCungCap)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaNhaCungCap
            modelBuilder.Entity<NguyenLieu>()
                .HasIndex(x => x.MaNhaCungCap);
            // NhapKho 1-n NguyenLieu
            modelBuilder.Entity<NhapKho>()
                .HasOne(x => x.NguyenLieu)
                .WithMany(x => x.NhapKhos)
                .HasForeignKey(x => x.MaNguyenLieu)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaNguyenLieu
            modelBuilder.Entity<NhapKho>()
                .HasIndex(x => x.MaNguyenLieu);
            // LuuTru 1-n NguyenLieu
            modelBuilder.Entity<LuuTru>()
                .HasOne(x => x.NguyenLieu)
                .WithMany(x => x.LuuTrus)
                .HasForeignKey(x => x.MaNguyenLieu)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaNguyenLieu
            modelBuilder.Entity<LuuTru>()
                .HasIndex(x => x.MaNguyenLieu);
            // XuatKho 1-n NguyenLieu
            modelBuilder.Entity<XuatKho>()
                .HasOne(x => x.NguyenLieu)
                .WithMany(x => x.XuatKhos)
                .HasForeignKey(x => x.MaNguyenLieu)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaNguyenLieu
            modelBuilder.Entity<XuatKho>()
                .HasIndex(x => x.MaNguyenLieu);
            // LuuTru 1-n XuatKho
            modelBuilder.Entity<XuatKho>()
                .HasOne(x => x.LuuTru)
                .WithMany(x => x.XuatKhos)
                .HasForeignKey(x => x.MaLuuTru)
                .OnDelete(DeleteBehavior.NoAction);
            // index for MaLuuTru
            modelBuilder.Entity<XuatKho>()
                .HasIndex(x => x.MaLuuTru);
            

            
        }
        
    }
}