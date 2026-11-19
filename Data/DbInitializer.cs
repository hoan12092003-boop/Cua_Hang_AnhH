using CuaHang.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CuaHang.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            // Đảm bảo các bảng đã được migrate
            context.Database.Migrate();

            // 1. SEED NGUOI DUNG (3 tài khoản: Admin, Thu ngân POS, Khách hàng)
            if (!context.NguoiDungs.Any())
            {
                var users = new List<NguoiDung>
                {
                    new NguoiDung
                    {
                        HoTen = "Quản Trị Viên (Admin)",
                        Email = "admin@minimart.vn",
                        MatKhauHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"),
                        SoDienThoai = "0909000001",
                        DiaChi = "Hệ thống Quản trị Minimart",
                        VaiTro = VaiTroNguoiDung.Admin,
                        TrangThai = true,
                        NgayTao = DateTime.UtcNow
                    },
                    new NguoiDung
                    {
                        HoTen = "Nguyễn Văn Thu Ngân",
                        Email = "nhanvien@minimart.vn",
                        MatKhauHash = BCrypt.Net.BCrypt.HashPassword("Staff@123"),
                        SoDienThoai = "0909000002",
                        DiaChi = "Quầy Thu Ngân 01 - Minimart",
                        VaiTro = VaiTroNguoiDung.NhanVien,
                        TrangThai = true,
                        NgayTao = DateTime.UtcNow
                    },
                    new NguoiDung
                    {
                        HoTen = "Trần Thị Khách Hàng",
                        Email = "khachhang@gmail.com",
                        MatKhauHash = BCrypt.Net.BCrypt.HashPassword("User@123"),
                        SoDienThoai = "0909000003",
                        DiaChi = "123 Đường Nguyễn Trãi, Quận 1, TP. HCM",
                        VaiTro = VaiTroNguoiDung.KhachHang,
                        TrangThai = true,
                        NgayTao = DateTime.UtcNow
                    }
                };

                context.NguoiDungs.AddRange(users);
                context.SaveChanges();
            }

            // 2. SEED DANH MUC (6 danh mục tiện lợi)
            if (!context.DanhMucs.Any())
            {
                var categories = new List<DanhMuc>
                {
                    new DanhMuc { TenDanhMuc = "Đồ uống", MoTa = "Nước giải khát, nước ngọt, trà, nước tăng lực đóng chai/lon", HoatDong = true },
                    new DanhMuc { TenDanhMuc = "Đồ ăn nhanh", MoTa = "Bánh mì, cơm nắm, xúc xích, đồ ăn liền tiện lợi trong ngày", HoatDong = true },
                    new DanhMuc { TenDanhMuc = "Mì & Đồ khô", MoTa = "Mì ăn liền, phở khô, snack, bánh quy ăn vặt", HoatDong = true },
                    new DanhMuc { TenDanhMuc = "Gia vị", MoTa = "Nước mắm, nước tương, tương ớt, dầu ăn, hạt nêm", HoatDong = true },
                    new DanhMuc { TenDanhMuc = "Hóa mỹ phẩm", MoTa = "Dầu gội, sữa tắm, bột giặt, nước rửa chén, khăn ướt", HoatDong = true },
                    new DanhMuc { TenDanhMuc = "Sữa & Bơ", MoTa = "Sữa tươi, sữa chua, bơ, phô mai bổ sung dinh dưỡng", HoatDong = true }
                };

                context.DanhMucs.AddRange(categories);
                context.SaveChanges();
            }

            // 3. SEED SAN PHAM (24 sản phẩm mẫu, có hàng sắp hết & cận hạn)
            if (!context.SanPhams.Any())
            {
                var catDoUong = context.DanhMucs.First(c => c.TenDanhMuc == "Đồ uống");
                var catDoAnNhanh = context.DanhMucs.First(c => c.TenDanhMuc == "Đồ ăn nhanh");
                var catMiDoKho = context.DanhMucs.First(c => c.TenDanhMuc == "Mì & Đồ khô");
                var catGiaVi = context.DanhMucs.First(c => c.TenDanhMuc == "Gia vị");
                var catHoaMyPham = context.DanhMucs.First(c => c.TenDanhMuc == "Hóa mỹ phẩm");
                var catSuaBo = context.DanhMucs.First(c => c.TenDanhMuc == "Sữa & Bơ");

                var now = new DateTime(2026, 11, 19);

                var products = new List<SanPham>
                {
                    // --- NHÓM 1: ĐỒ UỐNG ---
                    new SanPham
                    {
                        MaVach = "8934567890101",
                        TenSP = "Nước ngọt Coca Cola 320ml (Lon)",
                        GiaNhap = 8500m,
                        GiaBan = 11000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 65,
                        DonViTinh = "Lon",
                        HanSuDung = now.AddMonths(10),
                        HinhAnh = "/images/products/coca-cola.jpg",
                        DanhMucId = catDoUong.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890102",
                        TenSP = "Trà xanh Không Độ 455ml (Chai)",
                        GiaNhap = 7500m,
                        GiaBan = 10000m,
                        GiamGiaPhanTram = 10,
                        SoLuongTon = 48,
                        DonViTinh = "Chai",
                        HanSuDung = now.AddMonths(8),
                        HinhAnh = "/images/products/tra-xanh.jpg",
                        DanhMucId = catDoUong.Id
                    },
                    new SanPham
                    {
                        // HÀNG SẮP HẾT HÀNG (< 10) để test cảnh báo T4-09
                        MaVach = "8934567890103",
                        TenSP = "Nước tăng lực Redbull Thái 250ml",
                        GiaNhap = 11500m,
                        GiaBan = 15000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 5,
                        DonViTinh = "Lon",
                        HanSuDung = now.AddMonths(12),
                        HinhAnh = "/images/products/redbull.jpg",
                        DanhMucId = catDoUong.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890104",
                        TenSP = "Nước khoáng thiên nhiên Aquafina 500ml",
                        GiaNhap = 4500m,
                        GiaBan = 6500m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 90,
                        DonViTinh = "Chai",
                        HanSuDung = now.AddMonths(18),
                        HinhAnh = "/images/products/aquafina.jpg",
                        DanhMucId = catDoUong.Id
                    },

                    // --- NHÓM 2: ĐỒ ĂN NHANH ---
                    new SanPham
                    {
                        // HÀNG CẬN HẠN (còn 6 ngày <= 15 ngày) để test cảnh báo T4-10
                        MaVach = "8934567890201",
                        TenSP = "Bánh mì tươi Sandwich bơ sữa Kinh Đô 200g",
                        GiaNhap = 14000m,
                        GiaBan = 19000m,
                        GiamGiaPhanTram = 15,
                        SoLuongTon = 20,
                        DonViTinh = "Gói",
                        HanSuDung = now.AddDays(6), // Sắp hết hạn
                        HinhAnh = "/images/products/sandwich.jpg",
                        DanhMucId = catDoAnNhanh.Id
                    },
                    new SanPham
                    {
                        // HÀNG VỪA SẮP HẾT VỪA CẬN HẠN (< 10 và còn 3 ngày)
                        MaVach = "8934567890202",
                        TenSP = "Cơm nắm Onigiri rong biển cá ngừ mayo",
                        GiaNhap = 12000m,
                        GiaBan = 16000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 3, // Sắp hết hàng
                        DonViTinh = "Cái",
                        HanSuDung = now.AddDays(3), // Cận hạn
                        HinhAnh = "/images/products/onigiri.jpg",
                        DanhMucId = catDoAnNhanh.Id
                    },
                    new SanPham
                    {
                        // HÀNG ĐÃ HẾT HẠN (-4 ngày) để test cảnh báo T4-10 (Đã quá hạn)
                        MaVach = "8934567890203",
                        TenSP = "Xúc xích tiệt trùng Heo Vissan 4 cây x 35g",
                        GiaNhap = 11000m,
                        GiaBan = 15000m,
                        GiamGiaPhanTram = 20,
                        SoLuongTon = 12,
                        DonViTinh = "Gói",
                        HanSuDung = now.AddDays(-4), // Đã hết hạn
                        HinhAnh = "/images/products/xuc-xich.jpg",
                        DanhMucId = catDoAnNhanh.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890204",
                        TenSP = "Bánh Bao nhân xá xíu trứng cút C.P 120g",
                        GiaNhap = 10000m,
                        GiaBan = 14000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 25,
                        DonViTinh = "Cái",
                        HanSuDung = now.AddDays(25),
                        HinhAnh = "/images/products/banh-bao.jpg",
                        DanhMucId = catDoAnNhanh.Id
                    },

                    // --- NHÓM 3: MÌ & ĐỒ KHÔ ---
                    new SanPham
                    {
                        MaVach = "8934567890301",
                        TenSP = "Mì Hảo Hảo Tôm chua cay 75g (Gói)",
                        GiaNhap = 3600m,
                        GiaBan = 4800m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 150,
                        DonViTinh = "Gói",
                        HanSuDung = now.AddMonths(5),
                        HinhAnh = "/images/products/hao-hao.jpg",
                        DanhMucId = catMiDoKho.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890302",
                        TenSP = "Mì xào tương đen Samyang Hàn Quốc 140g",
                        GiaNhap = 22000m,
                        GiaBan = 29000m,
                        GiamGiaPhanTram = 5,
                        SoLuongTon = 35,
                        DonViTinh = "Gói",
                        HanSuDung = now.AddMonths(7),
                        HinhAnh = "/images/products/samyang.jpg",
                        DanhMucId = catMiDoKho.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890303",
                        TenSP = "Snack khoai tây O'Star vị tự nhiên 63g",
                        GiaNhap = 10500m,
                        GiaBan = 14000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 40,
                        DonViTinh = "Gói",
                        HanSuDung = now.AddMonths(6),
                        HinhAnh = "/images/products/ostar.jpg",
                        DanhMucId = catMiDoKho.Id
                    },
                    new SanPham
                    {
                        // HÀNG SẮP HẾT HÀNG (< 10)
                        MaVach = "8934567890304",
                        TenSP = "Bánh quy AFC lúa mì hộp 200g",
                        GiaNhap = 23000m,
                        GiaBan = 30000m,
                        GiamGiaPhanTram = 10,
                        SoLuongTon = 8,
                        DonViTinh = "Hộp",
                        HanSuDung = now.AddMonths(9),
                        HinhAnh = "/images/products/afc.jpg",
                        DanhMucId = catMiDoKho.Id
                    },

                    // --- NHÓM 4: GIA VỊ ---
                    new SanPham
                    {
                        MaVach = "8934567890401",
                        TenSP = "Nước mắm Nam Ngư Đệ Nhị 900ml",
                        GiaNhap = 23000m,
                        GiaBan = 29500m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 45,
                        DonViTinh = "Chai",
                        HanSuDung = now.AddYears(1),
                        HinhAnh = "/images/products/nam-ngu.jpg",
                        DanhMucId = catGiaVi.Id
                    },
                    new SanPham
                    {
                        // HÀNG SẮP HẾT HÀNG (< 10)
                        MaVach = "8934567890402",
                        TenSP = "Nước tương Maggi Đậm Đặc 700ml",
                        GiaNhap = 21000m,
                        GiaBan = 27000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 7,
                        DonViTinh = "Chai",
                        HanSuDung = now.AddMonths(14),
                        HinhAnh = "/images/products/maggi.jpg",
                        DanhMucId = catGiaVi.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890403",
                        TenSP = "Tương ớt Chinsu cay nồng chai 250g",
                        GiaNhap = 10000m,
                        GiaBan = 13500m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 55,
                        DonViTinh = "Chai",
                        HanSuDung = now.AddMonths(10),
                        HinhAnh = "/images/products/chinsu.jpg",
                        DanhMucId = catGiaVi.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890404",
                        TenSP = "Dầu ăn thực vật Tường An Cooking Oil 1 Lít",
                        GiaNhap = 42000m,
                        GiaBan = 51000m,
                        GiamGiaPhanTram = 5,
                        SoLuongTon = 32,
                        DonViTinh = "Chai",
                        HanSuDung = now.AddYears(2),
                        HinhAnh = "/images/products/tuong-an.jpg",
                        DanhMucId = catGiaVi.Id
                    },

                    // --- NHÓM 5: HÓA MỸ PHẨM ---
                    new SanPham
                    {
                        // HÀNG SẮP HẾT HÀNG (< 10)
                        MaVach = "8934567890501",
                        TenSP = "Dầu gội sạch gàu Clear Men Cool Sport Bạc Hà 630g",
                        GiaNhap = 145000m,
                        GiaBan = 179000m,
                        GiamGiaPhanTram = 10,
                        SoLuongTon = 4,
                        DonViTinh = "Chai",
                        HanSuDung = now.AddYears(2),
                        HinhAnh = "/images/products/clear-men.jpg",
                        DanhMucId = catHoaMyPham.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890502",
                        TenSP = "Sữa tắm dưỡng ẩm Lifebuoy Than hoạt tính 800g",
                        GiaNhap = 130000m,
                        GiaBan = 165000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 28,
                        DonViTinh = "Chai",
                        HanSuDung = now.AddYears(2),
                        HinhAnh = "/images/products/lifebuoy.jpg",
                        DanhMucId = catHoaMyPham.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890503",
                        TenSP = "Nước rửa chén Sunlight Chanh 750g (Túi)",
                        GiaNhap = 23000m,
                        GiaBan = 29000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 60,
                        DonViTinh = "Túi",
                        HanSuDung = now.AddYears(2),
                        HinhAnh = "/images/products/sunlight.jpg",
                        DanhMucId = catHoaMyPham.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890504",
                        TenSP = "Khăn ướt không mùi dịu nhẹ Mamamy 80 tờ",
                        GiaNhap = 27000m,
                        GiaBan = 36000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 50,
                        DonViTinh = "Gói",
                        HanSuDung = now.AddYears(1),
                        HinhAnh = "/images/products/mamamy.jpg",
                        DanhMucId = catHoaMyPham.Id
                    },

                    // --- NHÓM 6: SỮA & BƠ ---
                    new SanPham
                    {
                        // HÀNG CẬN HẠN (còn 11 ngày <= 15 ngày) để test cảnh báo T4-10
                        MaVach = "8934567890601",
                        TenSP = "Sữa tươi tiệt trùng Vinamilk Có Đường 1 Lít",
                        GiaNhap = 27000m,
                        GiaBan = 34500m,
                        GiamGiaPhanTram = 10,
                        SoLuongTon = 24,
                        DonViTinh = "Hộp",
                        HanSuDung = now.AddDays(11), // Cận hạn
                        HinhAnh = "/images/products/vinamilk-1l.jpg",
                        DanhMucId = catSuaBo.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890602",
                        TenSP = "Lốc 4 hộp Sữa chua ăn Vinamilk Nha Đam 100g",
                        GiaNhap = 22000m,
                        GiaBan = 28000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 40,
                        DonViTinh = "Lốc",
                        HanSuDung = now.AddDays(25),
                        HinhAnh = "/images/products/sua-chua.jpg",
                        DanhMucId = catSuaBo.Id
                    },
                    new SanPham
                    {
                        MaVach = "8934567890603",
                        TenSP = "Phô mai con bò cười La Vache Quiri Hộp 8 miếng 112g",
                        GiaNhap = 31000m,
                        GiaBan = 39000m,
                        GiamGiaPhanTram = 0,
                        SoLuongTon = 35,
                        DonViTinh = "Hộp",
                        HanSuDung = now.AddMonths(5),
                        HinhAnh = "/images/products/pho-mai.jpg",
                        DanhMucId = catSuaBo.Id
                    },
                    new SanPham
                    {
                        // HÀNG SẮP HẾT HÀNG (< 10)
                        MaVach = "8934567890604",
                        TenSP = "Bơ lạt tự nhiên TH true BUTTER 100g",
                        GiaNhap = 33000m,
                        GiaBan = 42000m,
                        GiamGiaPhanTram = 5,
                        SoLuongTon = 6, // Sắp hết
                        DonViTinh = "Gói",
                        HanSuDung = now.AddMonths(4),
                        HinhAnh = "/images/products/bo-lat.jpg",
                        DanhMucId = catSuaBo.Id
                    }
                };

                context.SanPhams.AddRange(products);
                context.SaveChanges();
            }
        }
    }
}
