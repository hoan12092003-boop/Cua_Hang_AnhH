using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    public static class VaiTroNguoiDung
    {
        public const string Admin = "Admin";
        public const string NhanVien = "NhanVien";
        public const string KhachHang = "KhachHang";
    }

    [Table("NguoiDungs")]
    public class NguoiDung
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Họ tên không được để trống")]
        [MaxLength(100, ErrorMessage = "Họ tên tối đa 100 ký tự")]
        public string HoTen { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [MaxLength(150, ErrorMessage = "Email tối đa 150 ký tự")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [MaxLength(255)]
        public string MatKhauHash { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        [MaxLength(20, ErrorMessage = "Số điện thoại tối đa 20 ký tự")]
        public string? SoDienThoai { get; set; }

        [MaxLength(255, ErrorMessage = "Địa chỉ tối đa 255 ký tự")]
        public string? DiaChi { get; set; }

        [Required]
        [MaxLength(20)]
        public string VaiTro { get; set; } = VaiTroNguoiDung.KhachHang;

        /// <summary>
        /// true = Đang hoạt động, false = Bị khóa
        /// </summary>
        public bool TrangThai { get; set; } = true;

        public DateTime NgayTao { get; set; } = DateTime.UtcNow;

        // Đơn hàng khách đã đặt
        [InverseProperty("KhachHang")]
        public virtual ICollection<DonHang> DonHangsMua { get; set; } = new List<DonHang>();

        // Đơn hàng do nhân viên này xử lý / thu ngân tại POS
        [InverseProperty("NhanVien")]
        public virtual ICollection<DonHang> DonHangsXuLy { get; set; } = new List<DonHang>();

        // Các phiếu nhập kho do người dùng này tạo
        [InverseProperty("NguoiTao")]
        public virtual ICollection<PhieuNhapKho> PhieuNhapKhos { get; set; } = new List<PhieuNhapKho>();
    }
}
