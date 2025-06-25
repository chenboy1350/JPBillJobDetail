using JPBillJobDetail.Data.Entities;
using JPBillJobDetail.Service.Interface;

namespace JPBillJobDetail.Service.Implement
{
    public class DataMockUpService : IDataMockUpService
    {
        public IEnumerable<JobGroup> GetJobGroupList()
        {
            return
            [
                new JobGroup
                {
                    JobNum = 1,
                    JobName = "Foundry",
                },
                new JobGroup
                {
                    JobNum = 2,
                    JobName = "Dress",
                },
                new JobGroup
                {
                    JobNum = 3,
                    JobName = "Polish",
                }
            ];
        }

        public IEnumerable<TempProfile> GetTempProfileList()
        {
            return
            [
                new TempProfile
                {
                    EmpCode = 1,
                    TitleName = "Mr.",
                    Name = "Profile 1",
                },
                new TempProfile
                {
                    EmpCode = 2,
                    TitleName = "Mr.",
                    Name = "Profile 2",
                },
                new TempProfile
                {
                    EmpCode = 3,
                    TitleName = "Mr.",
                    Name = "Profile 3",
                }
            ];
        }
    }
}
