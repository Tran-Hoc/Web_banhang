using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Web_banhang.Data;

namespace Web_banhang.Models
{
    public class NewsVM
    {

        [Key]
        [DisplayName("ID")]
        public int? Id { get; set; }

        [DisplayName("Tên danh mục")]
        [Required(ErrorMessage = "Không được để trống")]
        [StringLength(150, ErrorMessage = "Không được quá 150 ký tự")]
        public string Title { get; set; } = null!;

        [DisplayName("Mô tả")]
        //[Required(ErrorMessage = "Không được để trống")]
        public string? Description { get; set; }

        [DisplayName("Chi tiết")]
        //[Required(ErrorMessage = "Không được để trống")]
        public string? Detail { get; set; }

        [DisplayName("Ảnh đại diện")]
        //[Required(ErrorMessage = "Không được để trống")]
        public IFormFile? FileImage { get; set; }

        [DisplayName("Ảnh đại diện")]
        public string? Image { get; set; }

        [DisplayName("Tiêu đề SEO")]
        [StringLength(150)]
        public string? SeoTitle { get; set; }

        [DisplayName("Mô tả SEO")]
        [StringLength(250)]
        public string? SeoDescription { get; set; }

        [DisplayName("Từ khoá SEO")]
        [StringLength(150)]
        public string? SeoKeywords { get; set; }

        [DisplayName("Thời gian tạo")]
        [Column(TypeName = "datetime")]
        public DateTime? CreatedDate { get; set; }

        [DisplayName("Hiển thị")]
        public bool IsActive { get; set; }

    }
}
