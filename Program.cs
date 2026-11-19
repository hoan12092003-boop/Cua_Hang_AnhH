using CuaHang.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình ConnectionString cho SQLite ORM (EF Core)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? "Data Source=minimart.db";

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Kiểm tra kết nối cơ sở dữ liệu khi khởi động ứng dụng (Tiêu chí nghiệm thu T1-04)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        // Tu dong ap dung Migration tao va dong bo Schema DB (T1-09)
        context.Database.Migrate();
        app.Logger.LogInformation(">>> [DB CHECK] Ap dung Migration va ket noi SQLite thanh cong: {ConnectionString}", connectionString);
    }
    catch (Exception ex)
    {
        app.Logger.LogError(ex, ">>> [DB ERROR] Khong the ket noi den co so du lieu.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
