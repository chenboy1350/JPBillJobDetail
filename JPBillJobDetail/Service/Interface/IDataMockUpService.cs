using JPBillJobDetail.Data.Entities;
using JPBillJobDetail.Models;
using System.Collections.Generic;

namespace JPBillJobDetail.Service.Interface
{
    public interface IDataMockUpService
    {
        IEnumerable<JobGroup> GetJobGroupList();
        IEnumerable<TempProfile> GetTempProfileList();
        PagedListModel<BillJobDetailModel, BillJobFilterModel> GetMockBillJobDetails(BillJobFilterModel filter, int page, int pageSize);
        List<BillJobDetailModel> GetAllMockBillJobDetails();
        IEnumerable<JobBillCondition> GetJobBillConditionList();
    }
}
