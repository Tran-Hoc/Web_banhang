using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web_banhang.Data;

[Table("tb_ProductImage")]
public partial class TbProductImage
{
    [Key]
    public int Id { get; set; }

    public int ProductId { get; set; }

    public string? Image { get; set; }

    public bool IsDefault { get; set; }

    [ForeignKey("ProductId")]
    [InverseProperty("TbProductImages")]
    public virtual TbProduct Product { get; set; } = null!;
}
