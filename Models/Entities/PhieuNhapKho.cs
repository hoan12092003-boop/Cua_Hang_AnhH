using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table("PhieuNhapKhos")]
    public class PhieuNhapKho
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã phiếu nhập không được để trống")]
        [MaxLength(50, ErrorMessage = "Mã phiếu tối đa 50 ký tự")]
        public string MaPhieu { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên nhà cung cấp không được để trống")]
        [MaxLength(150, ErrorMessage = "Tên nhà cung cấp tối đa 150 ký tự")]
        public string NhaCungCap { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Tổng tiền nhập phải lớn hơn hoặc bằng 0")]
        public decimal TongTienNhap { get; set; } = 0;

        [Required(ErrorMessage = "Người tạo phiếu không được để trống")]
        public int NguoiTaoId { get; set; }

        [ForeignKey("NguoiTaoId")]
        public virtual NguoiDung? NguoiTao { get; set; }

        public DateTime NgayNhap { get; set; } = DateTime.UtcNow;

        [MaxLength(500, ErrorMessage = "Ghi chú tối đa 500 ký tự")]
        public string? GhiChu { get; set; }

        // Danh sách chi tiết các dòng sản phẩm nhập kho
        public virtual ICollection<ChiTietPhieuNhap> ChiTietPhieuNhaps { get; set; } = new List<ChiTietPhieuNhap>();
    }
}
