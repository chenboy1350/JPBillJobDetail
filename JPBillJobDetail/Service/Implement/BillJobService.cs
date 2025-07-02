using JPBillJobDetail.Data;
using JPBillJobDetail.Data.Entities;
using JPBillJobDetail.Models;
using JPBillJobDetail.Service.Interface;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace JPBillJobDetail.Service.Implement
{
    public class BillJobService(JPDbContext DbContext, Serilog.ILogger logger) : IBillJobService
    {
        private readonly JPDbContext _DbContext = DbContext;
        private readonly Serilog.ILogger _logger = logger;

        public IEnumerable<JobGroup> GetJobGroupList()
        {
            try
            {
                var result = _DbContext.JobGroup.Select(x => new JobGroup
                {
                    JobNum = x.JobNum,
                    JobName = x.JobName,
                    Jobtype = x.Jobtype,
                });

                _logger.Information("Fetched JobGroup list: {@JobGroups}", result.ToList());

                return [.. result];
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching JobGroup list");
                return [];
            }
        }

        public IEnumerable<JobBillCondition> GetBillConditionList()
        {
            try
            {
                var result = _DbContext.JobBillCondition.Select(x => new JobBillCondition
                {
                    IdNo = x.IdNo,
                    Detail = x.Detail,
                });

                _logger.Information("Fetched BillCondition list: {@BillCondition}", result.ToList());

                return [.. result];
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching BillCondition list");
                return [];
            }
        }

        public IEnumerable<TempProfile> GetTempProfileList(int JobNum)
        {
            IEnumerable<TempProfile> result = [];

            try
            {
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

                _logger.Information("Fetched TempProfile list for JobNum {JobNum}: {@TempProfiles}", JobNum, result.ToList());

                return [.. result];

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error fetching TempProfile list for JobNum: {JobNum}", JobNum);
                return [];
            }
        }

        public async Task<PagedListModel<BillJobDetailModel, BillJobFilterModel>> GetBillJobDetailListAsync(BillJobFilterModel filter, int page, int pageSize)
        {
            try
            {
                var query = await GetAllBillJobDetailAsync(filter);
                var totalCount = query.Count();
                var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
                var skip = (page - 1) * pageSize;
                var items = query.Skip(skip).Take(pageSize).ToList();

                _logger.Information("Fetched BillJobDetail list with filter: {@Filter}, page: {Page}, pageSize: {PageSize}, totalCount: {TotalCount}, totalPages: {TotalPages}, data: {@items}", filter, page, pageSize, totalCount, totalPages, items);

                return new PagedListModel<BillJobDetailModel, BillJobFilterModel>
                {
                    Data = new PaginationResult<BillJobDetailModel, BillJobFilterModel>
                    {
                        Items = items,
                        Filter = filter ?? new BillJobFilterModel(),
                        CurrentPage = page,
                        PageSize = pageSize,
                        TotalCount = totalCount,
                        TotalPages = totalPages,
                    }
                };
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in GetBillJobDetailListAsync with filter: {@Filter}, page: {Page}, pageSize: {PageSize}", filter, page, pageSize);
                throw;
            }
        }

        public async Task<IEnumerable<BillJobDetailModel>> GetAllBillJobDetailAsync(BillJobFilterModel filter)
        {
            try
            {
                bool hasJobNum = filter.JobNum != 0;
                bool hasJobtype = filter.Jobtype != 0;
                bool hasEmpCode = filter.EmpCode != 0;
                bool hasBillCondition = !string.IsNullOrEmpty(filter.BillCondition);
                bool hasDtStart = filter.DtStart.HasValue && filter.DtStart != DateTime.MinValue;
                bool hasDtEnd = filter.DtEnd.HasValue && filter.DtEnd != DateTime.MinValue;

                var query =
                    from a in _DbContext.JobDetail
                    join b in _DbContext.TempProfile on a.EmpCode equals b.EmpCode into bJoin
                    from b in bJoin.DefaultIfEmpty()

                    join c in _DbContext.JobHead on new { a.DocNo, a.EmpCode } equals new { c.DocNo, c.EmpCode } into cJoin
                    from c in cJoin.DefaultIfEmpty()

                    join d in _DbContext.Cprofile on a.Article equals d.Article into dJoin
                    from d in dJoin.DefaultIfEmpty()

                    join e in _DbContext.JobBill on new { a.DocNo, a.EmpCode, a.JobBarcode } equals new { e.DocNo, e.EmpCode, e.JobBarcode } into eJoin
                    from e in eJoin.DefaultIfEmpty()

                    join f in _DbContext.JobMprint on new { e.JobBarcode, e.Num } equals new { f.JobBarcode, f.Num } into fJoin
                    from f in fJoin.DefaultIfEmpty()

                    where c.BillCancel != true && e.Num != null

                    orderby c.MDate, a.DocNo, a.JobBarcode, a.EmpCode

                    select new { a, b, c, d, e, f };

                if (hasJobNum && hasJobtype)
                {
                    query = query.Where(x => x.c.JobNum == filter.JobNum && x.c.JobType == filter.Jobtype);
                }

                if (hasEmpCode)
                {
                    query = query.Where(x => x.a.EmpCode == filter.EmpCode);
                }

                if (hasBillCondition)
                {
                    query = query.Where(x =>
                        x.f.IdNo1 == filter.BillCondition ||
                        x.f.IdNo2 == filter.BillCondition ||
                        x.f.IdNo3 == filter.BillCondition ||
                        x.f.IdNo4 == filter.BillCondition ||
                        x.f.IdNo5 == filter.BillCondition ||
                        x.f.IdNo6 == filter.BillCondition );
                }

                if (hasDtStart && hasDtEnd)
                {
                    query = query.Where(x => x.c.MDate.Date >= filter.DtStart!.Value.Date && x.c.MDate.Date <= filter.DtEnd!.Value.Date);
                }
                else if (hasDtStart)
                {
                    query = query.Where(x => x.c.MDate.Date >= filter.DtStart!.Value.Date);
                }
                else if (hasDtEnd)
                {
                    query = query.Where(x => x.c.MDate.Date <= filter.DtEnd!.Value.Date);
                }

                var result = await query.Select(x => new BillJobDetailModel
                {
                    CustCode = x.a.CustCode.Trim(),
                    OrderNo = x.a.OrderNo.Trim(),
                    ListNo = x.a.ListNo.Trim(),
                    DocNo = x.a.DocNo.Trim(),
                    JobBarcode = x.a.JobBarcode.Trim(),
                    EmpCode = x.a.EmpCode,
                    EmpName = x.c.EmpName.Trim(),
                    JobName = x.c.JobName.Trim(),
                    Article = x.a.Article.Trim(),
                    TDesArt = x.d.TdesArt.Trim(),
                    Num = x.e.Num.Trim(),
                    OkTtl = x.e.OkTtl,
                    RtTtl = x.e.RtTtl,
                    DmTtl = x.e.DmTtl,
                    EpTtl = x.e.EpTtl,
                    IdNo1 = x.f.IdNo1.Trim(),
                    Remark1 = x.f.Remark1.Trim(),
                    IdNo2 = x.f.IdNo2.Trim(),
                    Remark2 = x.f.Remark2.Trim(),
                    IdNo3 = x.f.IdNo3.Trim(),
                    Remark3 = x.f.Remark3.Trim(),
                    IdNo4 = x.f.IdNo4.Trim(),
                    Remark4 = x.f.Remark4.Trim(),
                    IdNo5 = x.f.IdNo5.Trim(),
                    Remark5 = x.f.Remark5.Trim(),
                    IdNo6 = x.f.IdNo6.Trim(),
                    Remark6 = x.f.Remark6.Trim(),
                    MDate = x.c.MDate
                }).ToListAsync();

                _logger.Information("Fetched BillJobDetail list with filter: {@Filter}, count: {Count}", filter, result.Count);

                return result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error in GetAllBillJobDetailAsync with filter: {@Filter}", filter);
                throw;
            }
        }
    }
}
