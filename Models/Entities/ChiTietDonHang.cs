using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table("ChiTietDonHangs")]
    public class ChiTietDonHang
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int DonHangId { get; set; }
        [ForeignKey("DonHangId")]
        public virtual DonHang? DonHang { get; set; }

        [Required]
        public int SanPhamId { get; set; }
        [ForeignKey("SanPhamId")]
        public virtual SanPham? SanPham { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Số lượng mua tối thiểu là 1")]
        public int SoLuong { get; set; } = 1;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal DonGia { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal ThanhTien { get; set; } = 0;
    }
}
