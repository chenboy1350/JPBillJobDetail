using JPBillJobDetail.Data.Entities;

namespace JPBillJobDetail.Service.Interface
{
    public interface IBillJobService
    {
        IEnumerable<JobGroup> GetJobGroupList();
        IEnumerable<TempProfile> GetTempProfileList(int JobNum);
    }
}
