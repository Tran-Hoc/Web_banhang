using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Web_banhang.Data;

[Table("tb_SystemSetting")]
public partial class TbSystemSetting
{
    [Key]
    [StringLength(50)]
    public string SettingKey { get; set; } = null!;

    [StringLength(4000)]
    public string? SettingValue { get; set; }

    [StringLength(4000)]
    public string? SettingDescription { get; set; }
}
