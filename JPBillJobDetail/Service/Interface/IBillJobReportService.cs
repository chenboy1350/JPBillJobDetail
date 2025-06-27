using JPBillJobDetail.Models;

namespace JPBillJobDetail.Service.Interface
{
    public interface IBillJobReportService
    {
        byte[] GenBillJobReport(IEnumerable<BillJobDetailModel> data);
    }
}
