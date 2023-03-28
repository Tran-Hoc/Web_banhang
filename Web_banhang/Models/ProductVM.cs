using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web_banhang.Models
{
    public class ProductVM
    {
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        [DisplayName("Tên sản phẩm")]
        [Required(ErrorMessage = "Không được để trống")]
        public string Title { get; set; } = null!;

        [StringLength(50)]
        [DisplayName("Tên mã sản phẩm")]
        [Required(ErrorMessage = "Không được để trống")]
        public string? ProductCode { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; }

        [DisplayName("Chi tiết")]
        public string? Detail { get; set; }

        [StringLength(250)]
        [DisplayName("Ảnh đại diện")]
        public string? Image { get; set; }

        [NotMapped]
        [StringLength(250)]
        [DisplayName("Ảnh chi tiết")]
        public string[]? ListImage { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Giá")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [DisplayName("Giá khuyến mãi")]
        public decimal? PriceSale { get; set; }
        [DisplayName("Số lượng")]
        public int Quantity { get; set; }

        [DisplayName("Hiển thị")]
        public bool IsActive { get; set; }

        [DisplayName("Khuyến mãi")]
        public bool IsSale { get; set; }

        [DisplayName("Nổi bật")]
        public bool IsFeature { get; set; }

        [DisplayName("Hot")]
        public bool IsHot { get; set; }

        [StringLength(250)]
        public string? SeoTitle { get; set; }

        [StringLength(500)]
        public string? SeoDescription { get; set; }

        [StringLength(250)]
        public string? SeoKeywords { get; set; }

    }
}
