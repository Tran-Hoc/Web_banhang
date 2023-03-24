using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web_banhang.Data;

[Table("tb_Category")]
public partial class TbCategory
{
    public TbCategory()
    {
        //this.TbNews = new HashSet<TbNews>();
    }
    [DisplayName("ID")]
    [Key]
    public int Id { get; set; }

    [DisplayName("Tên danh mục")]
    [Required(ErrorMessage = "Không được để trống")]
    [StringLength(150)]
    public string Title { get; set; } = null!;

    [DisplayName("Mô tả")]
    //[Required(ErrorMessage = "Không được để trống")]
    public string? Description { get; set; }

    [DisplayName("Tiêu đề SEO")]
    //[Required(ErrorMessage = "Không được để trống")]
    [StringLength(150)]
    public string? SeoTitle { get; set; }

    [DisplayName("Mô tả SEO")]
    //[Required(ErrorMessage = "Không được để trống")]
    [StringLength(250)]
    public string? SeoDescription { get; set; }

    [DisplayName("Từ khoá SEO")]
    //[Required(ErrorMessage = "Không được để trống")]
    [StringLength(150)]
    public string? SeoKeywords { get; set; }

    [DisplayName("Vị trí")]
    [Required(ErrorMessage = "Không được để trống")]
    
    public int Position { get; set; }

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

    [InverseProperty("Category")]
    public virtual ICollection<TbNews> TbNews { get; } = new List<TbNews>();

    [InverseProperty("Category")]
    public virtual ICollection<TbPost> TbPosts { get; } = new List<TbPost>();
}
