using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table("ChiTietPhieuNhaps")]
    public class ChiTietPhieuNhap
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PhieuNhapId { get; set; }

        [ForeignKey("PhieuNhapId")]
        public virtual PhieuNhapKho? PhieuNhap { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn sản phẩm nhập")]
        public int SanPhamId { get; set; }

        [ForeignKey("SanPhamId")]
        public virtual SanPham? SanPham { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng nhập tối thiểu là 1")]
        public int SoLuong { get; set; } = 1;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá nhập phải lớn hơn hoặc bằng 0")]
        public decimal GiaNhap { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal ThanhTien { get; set; } = 0;
    }
}
