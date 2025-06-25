using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace JPBillJobDetail.Data.Entities;

[PrimaryKey("JobBarcode", "PrintItem", "Num")]
[Table("JobMPrint", Schema = "dbo")]
public partial class JobMprint
{
    [Key]
    [StringLength(14)]
    [Unicode(false)]
    public string JobBarcode { get; set; } = null!;

    [Key]
    [Column(TypeName = "decimal(18, 0)")]
    public decimal PrintItem { get; set; }

    [Key]
    [StringLength(10)]
    [Unicode(false)]
    public string Num { get; set; } = null!;

    [Column("OKTtl", TypeName = "decimal(18, 1)")]
    public decimal Okttl { get; set; }

    [Column("OKWg", TypeName = "decimal(18, 2)")]
    public decimal Okwg { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal EpTtl { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal EpWg { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal RtTtl { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal RtWg { get; set; }

    [Column(TypeName = "decimal(18, 1)")]
    public decimal DmTtl { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal DmWg { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Remark1 { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Remark2 { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Remark3 { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Remark4 { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Remark5 { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Remark6 { get; set; } = null!;

    public bool EditStatus { get; set; }

    public bool PrintStatus { get; set; }

    [Column("mDate", TypeName = "datetime")]
    public DateTime MDate { get; set; }

    [StringLength(20)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Silver { get; set; }

    public int ModelR { get; set; }

    [Column("ModelRD")]
    public int ModelRd { get; set; }

    [Column("ModelRM")]
    public int ModelRm { get; set; }

    [Column("ModelRN")]
    public int ModelRn { get; set; }

    public int ModelM { get; set; }

    [Column("ModelMD")]
    public int ModelMd { get; set; }

    [Column("ModelMM")]
    public int ModelMm { get; set; }

    [Column("ModelMN")]
    public int ModelMn { get; set; }

    [Column("matwg_return", TypeName = "decimal(18, 2)")]
    public decimal MatwgReturn { get; set; }

    [Column("Qty_Remark1", TypeName = "decimal(18, 2)")]
    public decimal QtyRemark1 { get; set; }

    [Column("Qty_Remark2", TypeName = "decimal(18, 2)")]
    public decimal QtyRemark2 { get; set; }

    [Column("Qty_Remark3", TypeName = "decimal(18, 2)")]
    public decimal QtyRemark3 { get; set; }

    [Column("Qty_Remark4", TypeName = "decimal(18, 2)")]
    public decimal QtyRemark4 { get; set; }

    [Column("Qty_Remark5", TypeName = "decimal(18, 2)")]
    public decimal QtyRemark5 { get; set; }

    [Column("Qty_Remark6", TypeName = "decimal(18, 2)")]
    public decimal QtyRemark6 { get; set; }

    [StringLength(3)]
    [Unicode(false)]
    public string IdNo1 { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string IdNo2 { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string IdNo3 { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string IdNo4 { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string IdNo5 { get; set; } = null!;

    [StringLength(3)]
    [Unicode(false)]
    public string IdNo6 { get; set; } = null!;
}
