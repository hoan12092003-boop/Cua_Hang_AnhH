using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table("DanhMucs")]
    public class DanhMuc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được để trống")]
        [MaxLength(100, ErrorMessage = "Tên danh mục tối đa 100 ký tự")]
        public string TenDanhMuc { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string? MoTa { get; set; }

        /// <summary>
        /// true = Đang hoạt động / hiển thị, false = Ngưng hoạt động / ẩn
        /// </summary>
        public bool HoatDong { get; set; } = true;

        public DateTime NgayTao { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Danh sách sản phẩm thuộc danh mục này
        /// </summary>
        public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
    }
}
