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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình bảng NguoiDung
            modelBuilder.Entity<NguoiDung>(entity =>
            {
                // Ràng buộc Unique Email (Tiêu chí T1-05)
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

            // Cấu hình bảng DanhMuc
            modelBuilder.Entity<DanhMuc>(entity =>
            {
                // Ràng buộc Unique TenDanhMuc (Tiêu chí T1-05)
                entity.HasIndex(c => c.TenDanhMuc)
                      .IsUnique()
                      .HasDatabaseName("IX_DanhMucs_TenDanhMuc");

                entity.Property(c => c.HoatDong)
                      .HasDefaultValue(true);

                entity.Property(c => c.NgayTao)
                      .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
        }
    }
}
