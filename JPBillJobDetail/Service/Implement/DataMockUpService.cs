using JPBillJobDetail.Data.Entities;
using JPBillJobDetail.Models;
using JPBillJobDetail.Service.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JPBillJobDetail.Service.Implement
{
    public class DataMockUpService(Serilog.ILogger logger) : IDataMockUpService
    {
        private readonly Serilog.ILogger _logger = logger;

        public IEnumerable<JobGroup> GetJobGroupList()
        {
            List<JobGroup> Data =
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

            _logger.Information("Fetching mock JobGroup list: {@JobGroups}", Data);

            return Data;
        }

        public IEnumerable<TempProfile> GetTempProfileList()
        {
            List<TempProfile> Data = 
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

            _logger.Information("Fetching mock TempProfile list: {@TempProfiles}", Data);

            return Data;
        }

        public PagedListModel<BillJobDetailModel, BillJobFilterModel> GetMockBillJobDetails(BillJobFilterModel filter, int page, int pageSize)
        {
            var Data = GetAllMockBillJobDetails();

            var totalCount = Data.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            var skip = (page - 1) * pageSize;
            var items = Data.Skip(skip).Take(pageSize).ToList();

            Serilog.Log.Information("Fetching mock JobOrderDetails: {@JobOrderDetails}", items);

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

        public List<BillJobDetailModel> GetAllMockBillJobDetails()
        {
            List<BillJobDetailModel> Data =
            [
                new BillJobDetailModel
                {
                    CustCode = "CUST001",
                    OrderNo = "ORD1001",
                    ListNo = "1",
                    DocNo = "DOC123",
                    JobBarcode = "JB10001",
                    EmpCode = 1,
                    EmpName = "John Doe",
                    JobName = "Cutting",
                    Article = "A123",
                    TDesArt = "Stainless Steel",
                    Num = "100",
                    OkTtl = 95,
                    RtTtl = 3,
                    DmTtl = 1,
                    EpTtl = 1,
                    IdNo1 = "ID001",
                    Remark1 = "Checked",
                    IdNo2 = "ID002",
                    Remark2 = "Approved",
                    IdNo3 = "ID003",
                    Remark3 = "Packed",
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST002",
                    OrderNo = "ORD1002",
                    ListNo = "2",
                    DocNo = "DOC124",
                    JobBarcode = "JB10002",
                    EmpCode = 2,
                    EmpName = "Jane Smith",
                    JobName = "Welding",
                    Article = "B456",
                    TDesArt = "Aluminum",
                    Num = "50",
                    OkTtl = 48,
                    RtTtl = 1,
                    DmTtl = 0,
                    EpTtl = 1,
                    IdNo1 = "ID101",
                    Remark1 = "Ready",
                    IdNo2 = "ID102",
                    Remark2 = "Inspected",
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-1)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST003",
                    OrderNo = "ORD1003",
                    ListNo = "3",
                    DocNo = "DOC125",
                    JobBarcode = "JB10003",
                    EmpCode = 3,
                    EmpName = "Alice Brown",
                    JobName = "Assembly",
                    Article = "C789",
                    TDesArt = "Copper",
                    Num = "200",
                    OkTtl = 198,
                    RtTtl = 1,
                    DmTtl = 0,
                    EpTtl = 1,
                    IdNo1 = "ID201",
                    Remark1 = "Completed",
                    IdNo2 = "ID202",
                    Remark2 = "Verified",
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-2)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST004",
                    OrderNo = "ORD1004",
                    ListNo = "4",
                    DocNo = "DOC126",
                    JobBarcode = "JB10004",
                    EmpCode = 4,
                    EmpName = "Bob White",
                    JobName = "Finishing",
                    Article = "D012",
                    TDesArt = "Brass",
                    Num = "75",
                    OkTtl = 70,
                    RtTtl = 3,
                    DmTtl = 1,
                    EpTtl = 1,
                    IdNo1 = "ID301",
                    Remark1 = "Polished",
                    IdNo2 = "ID302",
                    Remark2 = "Inspected",
                    IdNo3 = "ID303",
                    Remark3 = "Packed",
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-3)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST005",
                    OrderNo = "ORD1005",
                    ListNo = "5",
                    DocNo = "DOC127",
                    JobBarcode = "JB10005",
                    EmpCode = 5,
                    EmpName = "Charlie Green",
                    JobName = "Inspection",
                    Article = "E345",
                    TDesArt = "Titanium",
                    Num = "120",
                    OkTtl = 120,
                    RtTtl = 0,
                    DmTtl = 0,
                    EpTtl = 0,
                    IdNo1 = "ID401",
                    Remark1 = "Passed",
                    IdNo2 = null,
                    Remark2 = null,
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-4)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST006",
                    OrderNo = "ORD1006",
                    ListNo = "6",
                    DocNo = "DOC128",
                    JobBarcode = "JB10006",
                    EmpCode = 6,
                    EmpName = "Diana Prince",
                    JobName = "Casting",
                    Article = "F678",
                    TDesArt = "Gold",
                    Num = "80",
                    OkTtl = 78,
                    RtTtl = 1,
                    DmTtl = 1,
                    EpTtl = 0,
                    IdNo1 = "ID501",
                    Remark1 = "Casted",
                    IdNo2 = "ID502",
                    Remark2 = "Inspected",
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-5)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST007",
                    OrderNo = "ORD1007",
                    ListNo = "7",
                    DocNo = "DOC129",
                    JobBarcode = "JB10007",
                    EmpCode = 7,
                    EmpName = "Eve Black",
                    JobName = "Plating",
                    Article = "G901",
                    TDesArt = "Silver",
                    Num = "60",
                    OkTtl = 59,
                    RtTtl = 0,
                    DmTtl = 1,
                    EpTtl = 0,
                    IdNo1 = "ID601",
                    Remark1 = "Plated",
                    IdNo2 = null,
                    Remark2 = null,
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-6)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST008",
                    OrderNo = "ORD1008",
                    ListNo = "8",
                    DocNo = "DOC130",
                    JobBarcode = "JB10008",
                    EmpCode = 8,
                    EmpName = "Frank Blue",
                    JobName = "Engraving",
                    Article = "H234",
                    TDesArt = "Steel",
                    Num = "110",
                    OkTtl = 108,
                    RtTtl = 1,
                    DmTtl = 1,
                    EpTtl = 0,
                    IdNo1 = "ID701",
                    Remark1 = "Engraved",
                    IdNo2 = "ID702",
                    Remark2 = "Checked",
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-7)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST009",
                    OrderNo = "ORD1009",
                    ListNo = "9",
                    DocNo = "DOC131",
                    JobBarcode = "JB10009",
                    EmpCode = 9,
                    EmpName = "Grace Red",
                    JobName = "Polishing",
                    Article = "I567",
                    TDesArt = "Bronze",
                    Num = "90",
                    OkTtl = 89,
                    RtTtl = 1,
                    DmTtl = 0,
                    EpTtl = 0,
                    IdNo1 = "ID801",
                    Remark1 = "Polished",
                    IdNo2 = null,
                    Remark2 = null,
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-8)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST010",
                    OrderNo = "ORD1010",
                    ListNo = "10",
                    DocNo = "DOC132",
                    JobBarcode = "JB10010",
                    EmpCode = 10,
                    EmpName = "Henry Violet",
                    JobName = "Quality Check",
                    Article = "J890",
                    TDesArt = "Nickel",
                    Num = "130",
                    OkTtl = 130,
                    RtTtl = 0,
                    DmTtl = 0,
                    EpTtl = 0,
                    IdNo1 = "ID901",
                    Remark1 = "Checked",
                    IdNo2 = null,
                    Remark2 = null,
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-9)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST011",
                    OrderNo = "ORD1011",
                    ListNo = "11",
                    DocNo = "DOC133",
                    JobBarcode = "JB10011",
                    EmpCode = 11,
                    EmpName = "Ivy Orange",
                    JobName = "Packing",
                    Article = "K123",
                    TDesArt = "Zinc",
                    Num = "140",
                    OkTtl = 139,
                    RtTtl = 1,
                    DmTtl = 0,
                    EpTtl = 0,
                    IdNo1 = "ID1001",
                    Remark1 = "Packed",
                    IdNo2 = null,
                    Remark2 = null,
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-10)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST012",
                    OrderNo = "ORD1012",
                    ListNo = "12",
                    DocNo = "DOC134",
                    JobBarcode = "JB10012",
                    EmpCode = 12,
                    EmpName = "Jack Silver",
                    JobName = "Dispatch",
                    Article = "L456",
                    TDesArt = "Lead",
                    Num = "150",
                    OkTtl = 150,
                    RtTtl = 0,
                    DmTtl = 0,
                    EpTtl = 0,
                    IdNo1 = "ID1101",
                    Remark1 = "Dispatched",
                    IdNo2 = null,
                    Remark2 = null,
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-11)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST013",
                    OrderNo = "ORD1013",
                    ListNo = "13",
                    DocNo = "DOC135",
                    JobBarcode = "JB10013",
                    EmpCode = 13,
                    EmpName = "Karen Gold",
                    JobName = "Receiving",
                    Article = "M789",
                    TDesArt = "Platinum",
                    Num = "160",
                    OkTtl = 159,
                    RtTtl = 1,
                    DmTtl = 0,
                    EpTtl = 0,
                    IdNo1 = "ID1201",
                    Remark1 = "Received",
                    IdNo2 = null,
                    Remark2 = null,
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-12)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST014",
                    OrderNo = "ORD1014",
                    ListNo = "14",
                    DocNo = "DOC136",
                    JobBarcode = "JB10014",
                    EmpCode = 14,
                    EmpName = "Leo Copper",
                    JobName = "Sorting",
                    Article = "N012",
                    TDesArt = "Copper",
                    Num = "170",
                    OkTtl = 170,
                    RtTtl = 0,
                    DmTtl = 0,
                    EpTtl = 0,
                    IdNo1 = "ID1301",
                    Remark1 = "Sorted",
                    IdNo2 = null,
                    Remark2 = null,
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-13)
                },
                new BillJobDetailModel
                {
                    CustCode = "CUST015",
                    OrderNo = "ORD1015",
                    ListNo = "15",
                    DocNo = "DOC137",
                    JobBarcode = "JB10015",
                    EmpCode = 15,
                    EmpName = "Mona Brass",
                    JobName = "Labeling",
                    Article = "O345",
                    TDesArt = "Brass",
                    Num = "180",
                    OkTtl = 179,
                    RtTtl = 1,
                    DmTtl = 0,
                    EpTtl = 0,
                    IdNo1 = "ID1401",
                    Remark1 = "Labeled",
                    IdNo2 = null,
                    Remark2 = null,
                    IdNo3 = null,
                    Remark3 = null,
                    IdNo4 = null,
                    Remark4 = null,
                    IdNo5 = null,
                    Remark5 = null,
                    IdNo6 = null,
                    Remark6 = null,
                    MDate = DateTime.Today.AddDays(-14)
                }
            ];

            _logger.Information("Fetching mock AllBillJobDetails: {@AllBillJobDetails}", Data);

            return Data;
        }
    }
}
