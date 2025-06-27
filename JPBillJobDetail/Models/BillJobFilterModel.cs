namespace JPBillJobDetail.Models
{
    public class BillJobFilterModel
    {
        public int JobNum { get; set; }
        public int Jobtype { get; set; }
        public int EmpCode { get; set; }
        public DateTime? DtStart { get; set; }
        public DateTime? DtEnd { get; set; }
    }
}
