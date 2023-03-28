using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Web_banhang.Models
{
    public class ProdCategoryVM
    {
        [Key]
        public int Id { get; set; }

        [StringLength(150)]
        [DisplayName("Tiêu đề")]
        [Required(ErrorMessage ="Không được bỏ trống")]
        public string Title { get; set; }

        [DisplayName("Mô tả")]
        public string? Description { get; set; }

        [StringLength(250)]
        [DisplayName("Hình ảnh")]
        [Required(ErrorMessage = "Không được bỏ trống")]
        public string Icon { get; set; }

        [DisplayName("Tiêu đề SEO")]
        [StringLength(150)]
        public string? SeoTitle { get; set; }

        [DisplayName("Mô tả SEO")]
        [StringLength(250)]
        public string? SeoDescription { get; set; }

        [DisplayName("Từ khoá SEO")]
        [StringLength(150)]
        public string? SeoKeywords { get; set; }

        [StringLength(150)]
        public string? Alias { get; set; }

    }
}
