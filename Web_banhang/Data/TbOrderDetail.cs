using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web_banhang.Data;

[Table("tb_OrderDetail")]
public partial class TbOrderDetail
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("TbOrderDetails")]
    public virtual TbOrder Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("TbOrderDetails")]
    public virtual TbProduct Product { get; set; } = null!;
}
