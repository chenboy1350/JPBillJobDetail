namespace JPBillJobDetail.Models
{
    public class JobOrderDetail
    {
        public string? CustCode { get; set; }         
        public string? OrderNo { get; set; }          
        public int ListNo { get; set; }              
        public string? DocNo { get; set; }            
        public string? JobBarcode { get; set; }       
        public string? EmpCode { get; set; }          
        public string? EmpName { get; set; }          
        public string? JobName { get; set; }          
        public string? Article { get; set; }          
        public string? TDesArt { get; set; }          
        public int Num { get; set; }                 
        public int OkTtl { get; set; }               
        public int RtTtl { get; set; }               
        public int DmTtl { get; set; }               
        public int EpTtl { get; set; }               
        public string? IdNo1 { get; set; }            
        public string? Remark1 { get; set; }          
        public string? IdNo2 { get; set; }            
        public string? Remark2 { get; set; }          
        public string? IdNo3 { get; set; }            
        public string? Remark3 { get; set; }          
        public string? IdNo4 { get; set; }            
        public string? Remark4 { get; set; }          
        public string? IdNo5 { get; set; }            
        public string? Remark5 { get; set; }          
        public string? IdNo6 { get; set; }            
        public string? Remark6 { get; set; }          
        public DateTime MDate { get; set; }          
    }

    public class JobFilterModel
    {
        public int JobType { get; set; } = 2;
        public int JobNum { get; set; } = 3;
        public bool? BillCancel { get; set; } = false;
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
    }
}
