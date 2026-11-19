using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuaHang.Models.Entities
{
    [Table("SanPhams")]
    public class SanPham
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Mã vạch không được để trống")]
        [MaxLength(50, ErrorMessage = "Mã vạch tối đa 50 ký tự")]
        public string MaVach { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [MaxLength(200, ErrorMessage = "Tên sản phẩm tối đa 200 ký tự")]
        public string TenSP { get; set; } = string.Empty;

        [Range(0, double.MaxValue, ErrorMessage = "Giá nhập phải lớn hơn hoặc bằng 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaNhap { get; set; } = 0;

        [Required(ErrorMessage = "Giá bán không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá bán phải lớn hơn hoặc bằng 0")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal GiaBan { get; set; } = 0;

        [Range(0, 100, ErrorMessage = "Giảm giá phần trăm từ 0% đến 100%")]
        public int GiamGiaPhanTram { get; set; } = 0;

        [Range(0, int.MaxValue, ErrorMessage = "Số lượng tồn phải lớn hơn hoặc bằng 0")]
        public int SoLuongTon { get; set; } = 0;

        [Required(ErrorMessage = "Đơn vị tính không được để trống")]
        [MaxLength(50, ErrorMessage = "Đơn vị tính tối đa 50 ký tự")]
        public string DonViTinh { get; set; } = "Cái";

        public DateTime? HanSuDung { get; set; }

        [MaxLength(500)]
        public string? HinhAnh { get; set; }

        [Required(ErrorMessage = "Vui lòng chọn danh mục")]
        public int DanhMucId { get; set; }

        [ForeignKey("DanhMucId")]
        public virtual DanhMuc? DanhMuc { get; set; }

        /// <summary>
        /// true = Đang kinh doanh, false = Ngừng kinh doanh (phục vụ xóa/ẩn an toàn ở T4-04)
        /// </summary>
        public bool TrangThai { get; set; } = true;

        public DateTime NgayTao { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Giá bán thực tế sau khi tính chiết khấu / giảm giá
        /// </summary>
        [NotMapped]
        public decimal GiaSauGiam => GiamGiaPhanTram > 0 
            ? GiaBan * (100 - GiamGiaPhanTram) / 100m 
            : GiaBan;
    }
}
