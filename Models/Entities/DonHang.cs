using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    public static class KenhBanDonHang
    {
        public const string Online = "Online";
        public const string POS = "POS";
    }

    public static class TrangThaiDonHang
    {
        public const string ChoXacNhan = "ChoXacNhan";   // Đơn online mới đặt
        public const string DaXacNhan = "DaXacNhan";     // Đã duyệt đơn
        public const string DangGiao = "DangGiao";       // Đang giao hàng
        public const string HoanThanh = "HoanThanh";     // Đã giao / POS thanh toán xong
        public const string DaHuy = "DaHuy";             // Đã hủy (hoàn tồn kho)
    }

    public static class PhuongThucThanhToan
    {
        public const string COD = "COD";                         // Tiền mặt khi nhận hàng (Online)
        public const string ChuyenKhoan = "ChuyenKhoan";         // Chuyển khoản QR code
        public const string TienMat = "TienMat";                 // Tiền mặt tại quầy (POS)
    }

    [Table("DonHangs")]
    public class DonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string MaDon { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string KenhBan { get; set; } = KenhBanDonHang.Online; // Online hoặc POS

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal TongTien { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal TienKhachDua { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal TienThua { get; set; } = 0;

        [Required]
        [MaxLength(30)]
        public string TrangThai { get; set; } = TrangThaiDonHang.ChoXacNhan;

        [Required]
        [MaxLength(30)]
        public string PhuongThucTT { get; set; } = PhuongThucThanhToan.COD;

        // Khách hàng (nullable nếu khách mua vãng lai tại POS)
        public int? KhachHangId { get; set; }
        [ForeignKey("KhachHangId")]
        public virtual NguoiDung? KhachHang { get; set; }

        // Nhân viên thu ngân / nhân viên xử lý đơn (nullable nếu khách tự đặt online)
        public int? NhanVienId { get; set; }
        [ForeignKey("NhanVienId")]
        public virtual NguoiDung? NhanVien { get; set; }

        // Thông tin giao hàng cho đơn Online
        [MaxLength(100)]
        public string? TenNguoiNhan { get; set; }

        [MaxLength(20)]
        public string? SoDienThoaiNhan { get; set; }

        [MaxLength(255)]
        public string? DiaChiGiaoHang { get; set; }

        [MaxLength(500)]
        public string? GhiChu { get; set; }

        public DateTime NgayTao { get; set; } = DateTime.UtcNow;

        // Danh sách sản phẩm trong đơn
        public virtual ICollection<ChiTietDonHang> ChiTietDonHangs { get; set; } = new List<ChiTietDonHang>();
    }
}
