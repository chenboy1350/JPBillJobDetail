namespace JPBillJobDetail.Models
{
    public class BillJobDetailModel
    {
        public string? CustCode { get; set; } = string.Empty;
        public string? OrderNo { get; set; } = string.Empty;
        public string? ListNo { get; set; } = string.Empty;
        public string? DocNo { get; set; } = string.Empty;
        public string? JobBarcode { get; set; } = string.Empty;
        public int EmpCode { get; set; } = 0;
        public string? EmpName { get; set; } = string.Empty;
        public string? JobName { get; set; } = string.Empty;
        public string? Article { get; set; } = string.Empty;
        public string? TDesArt { get; set; } = string.Empty;
        public string? Num { get; set; } = string.Empty;
        public decimal OkTtl { get; set; } = 0m;
        public decimal RtTtl { get; set; } = 0m;
        public decimal DmTtl { get; set; } = 0m;
        public decimal EpTtl { get; set; } = 0m;
        public string? IdNo1 { get; set; } = string.Empty;
        public string? Remark1 { get; set; } = string.Empty;
        public string? IdNo2 { get; set; } = string.Empty;
        public string? Remark2 { get; set; } = string.Empty;
        public string? IdNo3 { get; set; } = string.Empty;
        public string? Remark3 { get; set; } = string.Empty;
        public string? IdNo4 { get; set; } = string.Empty;
        public string? Remark4 { get; set; } = string.Empty;
        public string? IdNo5 { get; set; } = string.Empty;
        public string? Remark5 { get; set; } = string.Empty;
        public string? IdNo6 { get; set; } = string.Empty;
        public string? Remark6 { get; set; } = string.Empty;
        public DateTime MDate { get; set; } = DateTime.MinValue;
    }
}
