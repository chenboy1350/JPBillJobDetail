using JPBillJobDetail.Data;
using JPBillJobDetail.Data.Entities;
using JPBillJobDetail.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JPBillJobDetail.Service.Implement
{
    public class BillJobService(JPDbContext DbContext) : IBillJobService
    {
        private readonly JPDbContext _DbContext = DbContext;

        public IEnumerable<JobGroup> GetJobGroupList()
        {
            var result = _DbContext.JobGroup.Select(x => new JobGroup
            {
                JobNum = x.JobNum,
                JobName = x.JobName,
            });

            return [.. result];
        }

        public IEnumerable<TempProfile> GetTempProfileList(int JobNum)
        {
            IEnumerable<TempProfile> result = [];

            var jobGroup = _DbContext.JobGroup.FirstOrDefault(j => j.JobNum == JobNum);

            if (jobGroup != null)
            {
                result = [.. _DbContext.TempProfile
                    .Where(e =>
                        (!jobGroup.Foundry || e.Foundry) &&
                        (!jobGroup.Dress || e.Dress) &&
                        (!jobGroup.Polish || e.Polish) &&
                        (!jobGroup.Bury || e.Bury) &&
                        (!jobGroup.Bathe || e.Bathe) &&
                        (!jobGroup.Complete || e.Complete) &&
                        (!jobGroup.Lee || e.Lee)
                    )
                    .Select(x => new TempProfile
                    {
                        EmpCode = x.EmpCode,
                        TitleName = x.TitleName,
                        Name = x.Name,
                    })];
            }

            return [.. result];
        }
    }
}
