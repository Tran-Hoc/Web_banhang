using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web_banhang.Data;

[Table("tb_News")]
public partial class TbNews
{
    [Key]
    [DisplayName("ID")]
    public int Id { get; set; }

    [DisplayName("Tên danh mục")]
    [Required(ErrorMessage = "Không được để trống")]
    [StringLength(150, ErrorMessage ="Không được quá 150 ký tự") ]
    public string Title { get; set; } = null!;

    [DisplayName("Mô tả")]
    public string? Description { get; set; }

    [DisplayName("Chi tiết")]
    
    public string? Detail { get; set; }

    [DisplayName("Ảnh đại diện")]
    public string? Image { get; set; }

    public int CategoryId { get; set; }

    [DisplayName("Tiêu đề SEO")]

    [StringLength(150)]
    public string? SeoTitle { get; set; }

    [DisplayName("Mô tả SEO")]
    [StringLength(250)]
    public string? SeoDescription { get; set; }

    [DisplayName("Từ khoá SEO")]
    [StringLength(150)]
    public string? SeoKeywords { get; set; }

    [DisplayName("Được tạo bởi")]
    public string? CreatedBy { get; set; }

    [DisplayName("Thời gian tạo")]
    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [DisplayName("Thời gian chỉnh sửa")]
    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    [DisplayName("Chỉnh sửa bởi")]
    public string? Modifiedby { get; set; }

    public string? Alias { get; set; }

    public bool IsActive { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("TbNews")]
    public virtual TbCategory Category { get; set; } = null!;
}
