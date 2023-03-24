using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web_banhang.Data;

[Table("tb_Product")]
public partial class TbProduct
{
    [Key]
    public int Id { get; set; }

    [StringLength(250)]
    public string Title { get; set; } = null!;

    [StringLength(50)]
    public string? ProductCode { get; set; }

    public string? Description { get; set; }

    public string? Detail { get; set; }

    [StringLength(250)]
    public string? Image { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? PriceSale { get; set; }

    public int Quantity { get; set; }

    public bool IsHome { get; set; }

    public bool IsSale { get; set; }

    public bool IsFeature { get; set; }

    public bool IsHot { get; set; }

    public int ProductCategoryId { get; set; }

    [StringLength(250)]
    public string? SeoTitle { get; set; }

    [StringLength(500)]
    public string? SeoDescription { get; set; }

    [StringLength(250)]
    public string? SeoKeywords { get; set; }

    public string? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ModifiedDate { get; set; }

    public string? Modifiedby { get; set; }

    [StringLength(250)]
    public string? Alias { get; set; }

    public bool IsActive { get; set; }

    public int ViewCount { get; set; }

    [ForeignKey("ProductCategoryId")]
    [InverseProperty("TbProducts")]
    public virtual TbProductCategory ProductCategory { get; set; } = null!;

    [InverseProperty("Product")]
    public virtual ICollection<TbOrderDetail> TbOrderDetails { get; } = new List<TbOrderDetail>();

    [InverseProperty("Product")]
    public virtual ICollection<TbProductImage> TbProductImages { get; } = new List<TbProductImage>();
}
