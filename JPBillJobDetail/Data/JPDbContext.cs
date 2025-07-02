using System;
using System.Collections.Generic;
using JPBillJobDetail.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace JPBillJobDetail.Data;

public partial class JPDbContext : DbContext
{
    public JPDbContext(DbContextOptions<JPDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CpriceSale> CpriceSale { get; set; }

    public virtual DbSet<Cprofile> Cprofile { get; set; }

    public virtual DbSet<JobBill> JobBill { get; set; }

    public virtual DbSet<JobBillCondition> JobBillCondition { get; set; }

    public virtual DbSet<JobDetail> JobDetail { get; set; }

    public virtual DbSet<JobGroup> JobGroup { get; set; }

    public virtual DbSet<JobHead> JobHead { get; set; }

    public virtual DbSet<JobMprint> JobMprint { get; set; }

    public virtual DbSet<TempProfile> TempProfile { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CpriceSale>(entity =>
        {
            entity.HasKey(e => e.Barcode)
                .IsClustered(false)
                .HasFillFactor(90);

            entity.ToTable("CPriceSale", "dbo", tb => tb.HasTrigger("CpriceSale_Trigger"));

            entity.HasIndex(e => e.Article, "CPriceSale4")
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => e.Barcode, "IX_CPriceSale")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.EpoxyColor, e.FnCode }, "IX_CPriceSale_1").HasFillFactor(90);

            entity.HasIndex(e => e.LinkBar, "IX_CPriceSale_2").HasFillFactor(90);

            entity.Property(e => e.ArtCode).HasDefaultValue("");
            entity.Property(e => e.ChkFinish).HasDefaultValue(1);
            entity.Property(e => e.ComCode).HasDefaultValue("0");
            entity.Property(e => e.ComputerName).HasDefaultValue("");
            entity.Property(e => e.DisCode).HasDefaultValue("0");
            entity.Property(e => e.EpoxyColor).HasDefaultValue("");
            entity.Property(e => e.FactoryCode).HasDefaultValue("0");
            entity.Property(e => e.FactorycodeOld).HasDefaultValue("");
            entity.Property(e => e.FnCode).HasDefaultValue("");
            entity.Property(e => e.FngemCode).HasDefaultValue("");
            entity.Property(e => e.LinkBar).HasDefaultValue("");
            entity.Property(e => e.ListGem).HasDefaultValue("");
            entity.Property(e => e.ListMat).HasDefaultValue("");
            entity.Property(e => e.Picture).HasDefaultValue("");
            entity.Property(e => e.PictureC).HasDefaultValue("");
            entity.Property(e => e.PictureL).HasDefaultValue("");
            entity.Property(e => e.PictureM).HasDefaultValue("");
            entity.Property(e => e.PictureR).HasDefaultValue("");
            entity.Property(e => e.PictureS).HasDefaultValue("");
            entity.Property(e => e.ProductType).HasDefaultValue(1);
            entity.Property(e => e.Remark).HasDefaultValue("");
            entity.Property(e => e.RingSize).HasDefaultValue("");
            entity.Property(e => e.TdesFn).HasDefaultValue("");
            entity.Property(e => e.UserName).HasDefaultValue("");

            entity.HasOne(d => d.ArticleNavigation).WithMany(p => p.CpriceSale)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CPriceSale_CProfile");
        });

        modelBuilder.Entity<Cprofile>(entity =>
        {
            entity.HasKey(e => e.Article).HasFillFactor(90);

            entity.ToTable("CProfile", "dbo", tb =>
                {
                    tb.HasTrigger("Cprofile_Del");
                    tb.HasTrigger("trg_CProfile_Insert");
                });

            entity.HasIndex(e => e.Article, "IX_CProfile")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.Article, e.SupArticle }, "IX_CProfile_1")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.ArtCode, e.List }, "IX_CProfile_2")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.CreaDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.GemType).HasDefaultValue(4);
            entity.Property(e => e.Idpic).HasDefaultValue("");
            entity.Property(e => e.LinkArticle)
                .HasDefaultValue("")
                .HasComment("รหัสงานใหม่ (Z)");
            entity.Property(e => e.LinkArticle1)
                .HasDefaultValue("")
                .HasComment("รหัสงาน(เปลี่ยนรหัสใหม่)");
            entity.Property(e => e.MarkCenter).HasDefaultValue("");
            entity.Property(e => e.PendantType).HasDefaultValue("");
            entity.Property(e => e.PictureScale).HasDefaultValue("");
            entity.Property(e => e.SupArticle).HasDefaultValue("");
        });

        modelBuilder.Entity<JobBill>(entity =>
        {
            entity.HasKey(e => e.Billnumber).HasFillFactor(90);

            entity.HasIndex(e => e.Billnumber, "IX_JobBill")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.JobBarcode, e.Num }, "IX_JobBill_1")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.Billnumber, e.JobBarcode, e.Num }, "IX_JobBill_2")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => e.JobBarcode, "IX_JobBill_3").HasFillFactor(90);

            entity.HasIndex(e => new { e.EmpCode, e.JobBarcode, e.OkTtl, e.OkWg, e.EpTtl, e.EpWg, e.RtTtl, e.RtWg, e.DmTtl, e.DmWg }, "JobBill9").HasFillFactor(90);

            entity.Property(e => e.ArtCode).HasDefaultValue("");
            entity.Property(e => e.Article).HasDefaultValue("");
            entity.Property(e => e.Barcode).HasDefaultValue("");
            entity.Property(e => e.CloseLast).HasComment("จบงานช่าง ส่งงานครั้งสุดท้าย");
            entity.Property(e => e.DocNo).HasDefaultValue("");
            entity.Property(e => e.EpQ3).HasComment("");
            entity.Property(e => e.FnCode).HasDefaultValue("");
            entity.Property(e => e.JobBarcode).HasDefaultValue("");
            entity.Property(e => e.ListNo).HasDefaultValue("");
            entity.Property(e => e.Lotno).HasDefaultValue("");
            entity.Property(e => e.MDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Num).HasComment("เลขลำดับที่ ของ JobBarcode เช่น '01','02'");
            entity.Property(e => e.PackDoc).HasDefaultValue("");
            entity.Property(e => e.SendMeltDoc).HasDefaultValue("");
            entity.Property(e => e.SendStockDoc).HasDefaultValue("");
            entity.Property(e => e.Silver).HasComment("เศษเนื้อเงิน");
            entity.Property(e => e.UserName).HasDefaultValue("");

            entity.HasOne(d => d.JobDetail).WithMany(p => p.JobBill)
                .HasPrincipalKey(p => new { p.JobBarcode, p.Barcode })
                .HasForeignKey(d => new { d.JobBarcode, d.Barcode })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_JobBill_JobDetail");
        });

        modelBuilder.Entity<JobBillCondition>(entity =>
        {
            entity.HasKey(e => e.IdNo)
                .HasName("PK_JobCondiMast")
                .HasFillFactor(90);

            entity.Property(e => e.IdNo).HasDefaultValue("");
            entity.Property(e => e.Detail).HasDefaultValue("");
            entity.Property(e => e.Detail1).HasDefaultValue("");
            entity.Property(e => e.MDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<JobDetail>(entity =>
        {
            entity.HasKey(e => new { e.JobBarcode, e.DocNo, e.EmpCode })
                .IsClustered(false)
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.JobBarcode, e.Barcode }, "IX_JobDetail")
                .IsUnique()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.DocNo, e.EmpCode }, "IX_JobDetail_1").HasFillFactor(90);

            entity.HasIndex(e => e.JobBarcode, "IX_JobDetail_2")
                .IsUnique()
                .IsClustered()
                .HasFillFactor(90);

            entity.HasIndex(e => new { e.JobBarcode, e.Barcode, e.CustCode }, "IX_JobDetail_3")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.JobBarcode).HasDefaultValue("");
            entity.Property(e => e.AccPrice).HasComment("ราคาค่าแรงช่างคิดบัญชี");
            entity.Property(e => e.AdjustWg).HasDefaultValue(0m);
            entity.Property(e => e.ArtCode).HasDefaultValue("");
            entity.Property(e => e.Article).HasDefaultValue("");
            entity.Property(e => e.Barcode).HasDefaultValue("");
            entity.Property(e => e.BodyWg).HasDefaultValue(0m);
            entity.Property(e => e.BodyWg2).HasDefaultValue(0m);
            entity.Property(e => e.ChkGem).HasComment("เช็คว่ามีพลอยติดตัวเรือนไปหรือไม่");
            entity.Property(e => e.ChkMaterial).HasComment("เช็คค่าวัตถุดิบ(ปักก้าน)ให้ช่าง ");
            entity.Property(e => e.CustCode).HasDefaultValue("");
            entity.Property(e => e.DateClose).HasComment("วันที่ปิดรายการ");
            entity.Property(e => e.Description).HasDefaultValue("");
            entity.Property(e => e.Dmpercent).HasComment("ค่าซิเนื้อเงิน คิดเป็น %");
            entity.Property(e => e.FnCode).HasDefaultValue("");
            entity.Property(e => e.Grade).HasDefaultValue("");
            entity.Property(e => e.GroupNo).HasDefaultValue("");
            entity.Property(e => e.GroupSetNo).HasDefaultValue("");
            entity.Property(e => e.JobClose).HasComment("ปิดช่าง 1=ปิดช่าง");
            entity.Property(e => e.JobPriceEdit).HasComment("รายการที่แก้ไขค่าแรง =1 ");
            entity.Property(e => e.JobPriceOld).HasComment("ราคาค่าแรงก่อนแก้ไข");
            entity.Property(e => e.ListNo).HasDefaultValue("");
            entity.Property(e => e.LotNo).HasDefaultValue("");
            entity.Property(e => e.MarkJob).HasDefaultValue("");
            entity.Property(e => e.MatItem).HasDefaultValue("");
            entity.Property(e => e.OrderNo).HasDefaultValue("");
            entity.Property(e => e.Remark1).HasDefaultValue("");
            entity.Property(e => e.Remark2).HasDefaultValue("");
            entity.Property(e => e.TtlwgOld).HasDefaultValue(0.00m);
            entity.Property(e => e.Unit).HasDefaultValue("");
            entity.Property(e => e.UserClose)
                .HasDefaultValue("")
                .HasComment("ชื่อผู้ทำรายการปิดช่าง");
            entity.Property(e => e.UserName).HasDefaultValue("");
        });

        modelBuilder.Entity<JobGroup>(entity =>
        {
            entity.Property(e => e.JobItem).HasDefaultValue(0m);
            entity.Property(e => e.JobNum).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<JobHead>(entity =>
        {
            entity.HasKey(e => new { e.DocNo, e.EmpCode }).HasFillFactor(90);

            entity.HasIndex(e => new { e.DocNo, e.EmpCode }, "IX_JobHead")
                .IsUnique()
                .HasFillFactor(90);

            entity.Property(e => e.ChkGem).HasComment("เช็คราคาค่าแรงรวมค่าพลอย");
            entity.Property(e => e.ChkSilver).HasComment("เช็คราคาค่าแรงรวมค่าเนื้อเงิน");
            entity.Property(e => e.DueDate).HasDefaultValueSql("(getdate() + 3)");
            entity.Property(e => e.JobDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.PrintBy).HasDefaultValueSql("(0)");
            entity.Property(e => e.Runid).ValueGeneratedOnAdd();
            entity.Property(e => e.TypeOther).HasDefaultValue("");
        });

        modelBuilder.Entity<JobMprint>(entity =>
        {
            entity.HasKey(e => new { e.JobBarcode, e.PrintItem, e.Num }).HasFillFactor(90);

            entity.Property(e => e.JobBarcode).HasDefaultValue("");
            entity.Property(e => e.Num).HasDefaultValue("");
            entity.Property(e => e.IdNo1).HasDefaultValue("");
            entity.Property(e => e.IdNo2).HasDefaultValue("");
            entity.Property(e => e.IdNo3).HasDefaultValue("");
            entity.Property(e => e.IdNo4).HasDefaultValue("");
            entity.Property(e => e.IdNo5).HasDefaultValue("");
            entity.Property(e => e.IdNo6).HasDefaultValue("");
            entity.Property(e => e.MDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Remark1).HasDefaultValue("");
            entity.Property(e => e.Remark2).HasDefaultValue("");
            entity.Property(e => e.Remark3).HasDefaultValue("");
            entity.Property(e => e.Remark4).HasDefaultValue("");
            entity.Property(e => e.Remark5).HasDefaultValue("");
            entity.Property(e => e.Remark6).HasDefaultValue("");
            entity.Property(e => e.UserName).HasDefaultValue("");
        });

        modelBuilder.Entity<TempProfile>(entity =>
        {
            entity.HasKey(e => e.EmpCode).HasFillFactor(90);

            entity.ToTable("TEmpProfile", "dbo", tb => tb.HasTrigger("TEmpProfile_Trigger"));

            entity.Property(e => e.EmpCode).ValueGeneratedNever();
            entity.Property(e => e.Btype).HasDefaultValue("000000000");
            entity.Property(e => e.DempType).HasDefaultValue("");
            entity.Property(e => e.Detail).HasDefaultValue("");
            entity.Property(e => e.EmpLink).ValueGeneratedOnAdd();
            entity.Property(e => e.Mdate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Remark).HasDefaultValue("");
            entity.Property(e => e.RunDoc).HasDefaultValue(0);
            entity.Property(e => e.TitleName).HasDefaultValue("");
            entity.Property(e => e.Username).HasDefaultValue("");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
