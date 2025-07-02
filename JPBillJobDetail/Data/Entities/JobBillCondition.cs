using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JPBillJobDetail.Data.Entities;

[Table("JobBillCondition", Schema = "dbo")]
public partial class JobBillCondition
{
    [Key]
    [StringLength(3)]
    [Unicode(false)]
    public string IdNo { get; set; } = null!;

    [Column(TypeName = "ntext")]
    public string Detail { get; set; } = null!;

    [Column("mDate", TypeName = "datetime")]
    public DateTime MDate { get; set; }

    [Column("detail1")]
    [StringLength(150)]
    [Unicode(false)]
    public string Detail1 { get; set; } = null!;
}
