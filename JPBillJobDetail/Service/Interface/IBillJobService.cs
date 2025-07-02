using JPBillJobDetail.Data.Entities;
using JPBillJobDetail.Models;

namespace JPBillJobDetail.Service.Interface
{
    public interface IBillJobService
    {
        IEnumerable<JobGroup> GetJobGroupList();
        IEnumerable<JobBillCondition> GetBillConditionList();
        IEnumerable<TempProfile> GetTempProfileList(int JobNum);
        Task<PagedListModel<BillJobDetailModel, BillJobFilterModel>> GetBillJobDetailListAsync(BillJobFilterModel filter, int page, int pageSize);
        Task<IEnumerable<BillJobDetailModel>> GetAllBillJobDetailAsync(BillJobFilterModel filter);
    }
}
