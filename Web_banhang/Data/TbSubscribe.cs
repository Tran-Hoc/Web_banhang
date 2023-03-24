﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web_banhang.Data;

[Table("tb_Subscribe")]
public partial class TbSubscribe
{
    [Key]
    public int Id { get; set; }

    public string Email { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime CreatedDate { get; set; }
}
