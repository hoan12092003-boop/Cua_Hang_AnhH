using Microsoft.EntityFrameworkCore;

namespace CuaHang.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Cac Fluent API cấu hình bảng sẽ được bổ sung ở Task T1-05 -> T1-08
        }
    }
}
