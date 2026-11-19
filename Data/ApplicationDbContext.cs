using CuaHang.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuaHang.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<NguoiDung> NguoiDungs { get; set; } = null!;
        public DbSet<DanhMuc> DanhMucs { get; set; } = null!;
        public DbSet<SanPham> SanPhams { get; set; } = null!;
        public DbSet<DonHang> DonHangs { get; set; } = null!;
        public DbSet<ChiTietDonHang> ChiTietDonHangs { get; set; } = null!;
        public DbSet<PhieuNhapKho> PhieuNhapKhos { get; set; } = null!;
        public DbSet<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình bảng NguoiDung (T1-05)
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                entity.HasIndex(u => u.Email)
                      .IsUnique()
                      .HasDatabaseName("IX_NguoiDungs_Email");

                entity.Property(u => u.VaiTro)
                      .HasDefaultValue(VaiTroNguoiDung.KhachHang);

                entity.Property(u => u.TrangThai)
                      .HasDefaultValue(true);

                entity.Property(u => u.NgayTao)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Cấu hình bảng DanhMuc (T1-05)
            modelBuilder.Entity<DanhMuc>(entity =>
            {
                entity.HasIndex(c => c.TenDanhMuc)
                      .IsUnique()
                      .HasDatabaseName("IX_DanhMucs_TenDanhMuc");

                entity.Property(c => c.HoatDong)
                      .HasDefaultValue(true);

                entity.Property(c => c.NgayTao)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Cấu hình bảng SanPham (T1-06)
            modelBuilder.Entity<SanPham>(entity =>
            {
                entity.HasIndex(p => p.MaVach)
                      .IsUnique()
                      .HasDatabaseName("IX_SanPhams_MaVach");

                entity.HasOne(p => p.DanhMuc)
                      .WithMany(c => c.SanPhams)
                      .HasForeignKey(p => p.DanhMucId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("SanPhams", t =>
                {
                    t.HasCheckConstraint("CK_SanPhams_GiaBan", "[GiaBan] >= 0");
                    t.HasCheckConstraint("CK_SanPhams_GiaNhap", "[GiaNhap] >= 0");
                    t.HasCheckConstraint("CK_SanPhams_SoLuongTon", "[SoLuongTon] >= 0");
                    t.HasCheckConstraint("CK_SanPhams_GiamGiaPhanTram", "[GiamGiaPhanTram] >= 0 AND [GiamGiaPhanTram] <= 100");
                });

                entity.Property(p => p.GiaNhap).HasDefaultValue(0m);
                entity.Property(p => p.GiaBan).HasDefaultValue(0m);
                entity.Property(p => p.GiamGiaPhanTram).HasDefaultValue(0);
                entity.Property(p => p.SoLuongTon).HasDefaultValue(0);
                entity.Property(p => p.DonViTinh).HasDefaultValue("Cái");
                entity.Property(p => p.TrangThai).HasDefaultValue(true);
                entity.Property(p => p.NgayTao).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Cấu hình bảng DonHang (T1-07)
            modelBuilder.Entity<DonHang>(entity =>
            {
                entity.HasIndex(o => o.MaDon)
                      .IsUnique()
                      .HasDatabaseName("IX_DonHangs_MaDon");

                entity.HasOne(o => o.KhachHang)
                      .WithMany(u => u.DonHangsMua)
                      .HasForeignKey(o => o.KhachHangId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(o => o.NhanVien)
                      .WithMany(u => u.DonHangsXuLy)
                      .HasForeignKey(o => o.NhanVienId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("DonHangs", t =>
                {
                    t.HasCheckConstraint("CK_DonHangs_TongTien", "[TongTien] >= 0");
                    t.HasCheckConstraint("CK_DonHangs_TienKhachDua", "[TienKhachDua] >= 0");
                    t.HasCheckConstraint("CK_DonHangs_TienThua", "[TienThua] >= 0");
                });

                entity.Property(o => o.KenhBan).HasDefaultValue(KenhBanDonHang.Online);
                entity.Property(o => o.TrangThai).HasDefaultValue(TrangThaiDonHang.ChoXacNhan);
                entity.Property(o => o.PhuongThucTT).HasDefaultValue(PhuongThucThanhToan.COD);
                entity.Property(o => o.TongTien).HasDefaultValue(0m);
                entity.Property(o => o.TienKhachDua).HasDefaultValue(0m);
                entity.Property(o => o.TienThua).HasDefaultValue(0m);
                entity.Property(o => o.NgayTao).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Cấu hình bảng ChiTietDonHang (T1-07)
            modelBuilder.Entity<ChiTietDonHang>(entity =>
            {
                entity.HasOne(d => d.DonHang)
                      .WithMany(o => o.ChiTietDonHangs)
                      .HasForeignKey(d => d.DonHangId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(d => d.SanPham)
                      .WithMany(p => p.ChiTietDonHangs)
                      .HasForeignKey(d => d.SanPhamId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("ChiTietDonHangs", t =>
                {
                    t.HasCheckConstraint("CK_ChiTietDonHangs_SoLuong", "[SoLuong] >= 1");
                    t.HasCheckConstraint("CK_ChiTietDonHangs_DonGia", "[DonGia] >= 0");
                    t.HasCheckConstraint("CK_ChiTietDonHangs_ThanhTien", "[ThanhTien] >= 0");
                });

                entity.Property(d => d.SoLuong).HasDefaultValue(1);
                entity.Property(d => d.DonGia).HasDefaultValue(0m);
                entity.Property(d => d.ThanhTien).HasDefaultValue(0m);
            });

            // Cấu hình bảng PhieuNhapKho (Tiêu chí T1-08)
            modelBuilder.Entity<PhieuNhapKho>(entity =>
            {
                // Mã phiếu nhập duy nhất để truy vết từng lô/phiếu
                entity.HasIndex(p => p.MaPhieu)
                      .IsUnique()
                      .HasDatabaseName("IX_PhieuNhapKhos_MaPhieu");

                // Khóa ngoại liên kết người tạo phiếu (NguoiDung)
                entity.HasOne(p => p.NguoiTao)
                      .WithMany(u => u.PhieuNhapKhos)
                      .HasForeignKey(p => p.NguoiTaoId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("PhieuNhapKhos", t =>
                {
                    t.HasCheckConstraint("CK_PhieuNhapKhos_TongTienNhap", "[TongTienNhap] >= 0");
                });

                entity.Property(p => p.TongTienNhap).HasDefaultValue(0m);
                entity.Property(p => p.NgayNhap).HasDefaultValueSql("CURRENT_TIMESTAMP");
            });

            // Cấu hình bảng ChiTietPhieuNhap (Tiêu chí T1-08)
            modelBuilder.Entity<ChiTietPhieuNhap>(entity =>
            {
                // Khóa ngoại liên kết PhieuNhapKho (Xóa phiếu thì cascade xóa chi tiết phiếu)
                entity.HasOne(c => c.PhieuNhap)
                      .WithMany(p => p.ChiTietPhieuNhaps)
                      .HasForeignKey(c => c.PhieuNhapId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Khóa ngoại liên kết SanPham (Chặn xóa sản phẩm khi đã có lịch sử nhập kho)
                entity.HasOne(c => c.SanPham)
                      .WithMany(p => p.ChiTietPhieuNhaps)
                      .HasForeignKey(c => c.SanPhamId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.ToTable("ChiTietPhieuNhaps", t =>
                {
                    t.HasCheckConstraint("CK_ChiTietPhieuNhaps_SoLuong", "[SoLuong] >= 1");
                    t.HasCheckConstraint("CK_ChiTietPhieuNhaps_GiaNhap", "[GiaNhap] >= 0");
                    t.HasCheckConstraint("CK_ChiTietPhieuNhaps_ThanhTien", "[ThanhTien] >= 0");
                });

                entity.Property(c => c.SoLuong).HasDefaultValue(1);
                entity.Property(c => c.GiaNhap).HasDefaultValue(0m);
                entity.Property(c => c.ThanhTien).HasDefaultValue(0m);
            });
        }
    }
}
