using JPBillJobDetail.Data.Entities;

namespace JPBillJobDetail.Service.Interface
{
    public interface IDataMockUpService
    {
        IEnumerable<JobGroup> GetJobGroupList();
        IEnumerable<TempProfile> GetTempProfileList();
    }
}
