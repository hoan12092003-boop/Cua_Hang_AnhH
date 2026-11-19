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

            // Cấu hình bảng SanPham (Tiêu chí T1-06)
            modelBuilder.Entity<SanPham>(entity =>
            {
                // Ràng buộc Unique MaVach
                entity.HasIndex(p => p.MaVach)
                      .IsUnique()
                      .HasDatabaseName("IX_SanPhams_MaVach");

                // Ràng buộc Khóa ngoại với DanhMuc (Không cho xóa danh mục nếu còn sản phẩm)
                entity.HasOne(p => p.DanhMuc)
                      .WithMany(c => c.SanPhams)
                      .HasForeignKey(p => p.DanhMucId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Ràng buộc Check Constraints: GiaBan >= 0, GiaNhap >= 0, SoLuongTon >= 0, GiamGiaPhanTram 0-100
                entity.ToTable("SanPhams", t =>
                {
                    t.HasCheckConstraint("CK_SanPhams_GiaBan", "[GiaBan] >= 0");
                    t.HasCheckConstraint("CK_SanPhams_GiaNhap", "[GiaNhap] >= 0");
                    t.HasCheckConstraint("CK_SanPhams_SoLuongTon", "[SoLuongTon] >= 0");
                    t.HasCheckConstraint("CK_SanPhams_GiamGiaPhanTram", "[GiamGiaPhanTram] >= 0 AND [GiamGiaPhanTram] <= 100");
                });

                entity.Property(p => p.GiaNhap)
                      .HasDefaultValue(0m);

                entity.Property(p => p.GiaBan)
                      .HasDefaultValue(0m);

                entity.Property(p => p.GiamGiaPhanTram)
                      .HasDefaultValue(0);

                entity.Property(p => p.SoLuongTon)
                      .HasDefaultValue(0);

                entity.Property(p => p.DonViTinh)
                      .HasDefaultValue("Cái");

                entity.Property(p => p.TrangThai)
                      .HasDefaultValue(true);

                entity.Property(p => p.NgayTao)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
