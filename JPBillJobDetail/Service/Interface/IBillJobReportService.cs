using JPBillJobDetail.Models;

namespace JPBillJobDetail.Service.Interface
{
    public interface IBillJobReportService
    {
        byte[] GenExcelBillJobReport(IEnumerable<BillJobDetailModel> data);
        byte[] GenPDFBillJobReport(IEnumerable<BillJobDetailModel> data);
    }
}
