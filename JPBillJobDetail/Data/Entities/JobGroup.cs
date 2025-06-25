using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JPBillJobDetail.Data.Entities;

[Keyless]
[Table("JobGroup", Schema = "dbo")]
public partial class JobGroup
{
    public int JobNum { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string JobName { get; set; } = null!;

    public bool Foundry { get; set; }

    public bool Dress { get; set; }

    public bool Polish { get; set; }

    public bool Bury { get; set; }

    public bool Bathe { get; set; }

    public bool Complete { get; set; }

    public bool Lee { get; set; }

    public int Jobtype { get; set; }

    [Column(TypeName = "decimal(10, 2)")]
    public decimal? JobItem { get; set; }

    public int JobDayUse { get; set; }
}
