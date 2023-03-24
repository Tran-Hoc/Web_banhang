using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web_banhang.Data;

public partial class ThongKe
{
    [Key]
    public int Id { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime ThoiGian { get; set; }

    public long SoTruyCap { get; set; }
}
