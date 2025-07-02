using JPBillJobDetail.Models;
using JPBillJobDetail.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Threading.Tasks;

namespace JPBillJobDetail.Controllers
{
    public class HomeController(IBillJobService billJobService, IDataMockUpService dataMockUp, IBillJobReportService billJobReportService, IOptions<AppSettingModel> options) : Controller
    {
        private readonly IBillJobService _billJobService = billJobService;
        private readonly IDataMockUpService _dataMockUp = dataMockUp;
        private readonly IBillJobReportService _billJobReportService = billJobReportService;
        private readonly AppSettingModel _appSettings = options.Value;

        public IActionResult Index()
        {
            ViewBag.CurrentPage = 1;
            ViewBag.TotalPages = 0;
            ViewBag.PageSize = 10;
            ViewBag.TotalItems = 0;

            if (_appSettings.UseDemo)
            {
                ViewBag.JobGroupList = _dataMockUp.GetJobGroupList();
                ViewBag.JobBillConditionList = _dataMockUp.GetJobBillConditionList();
            }
            else
            {
                ViewBag.JobGroupList = _billJobService.GetJobGroupList();
                ViewBag.JobBillConditionList = _billJobService.GetBillConditionList();
            }

            return View(new PagedListModel<BillJobDetailModel, BillJobFilterModel>());
        }

        [HttpGet]
        public IActionResult TempProfile(int JobNum)
        {
            if (_appSettings.UseDemo)
            {
                return Ok(_dataMockUp.GetTempProfileList());
            }
            else
            {
                return Ok(_billJobService.GetTempProfileList(JobNum));
            }
        }

        [HttpGet]
        public async Task<IActionResult> BillJobDetail(int JobNum = 0,int Jobtype = 0, int EmpCode = 0, string BillCondion = "", DateTime? DtStart = null, DateTime? DtEnd = null, int Page = 1, int PageSize = 10)
        {
            if (Page < 1) Page = 1;
            if (PageSize < 5) PageSize = 5;
            if (PageSize > 100) PageSize = 100;

            BillJobFilterModel filter = new()
            {
                JobNum = JobNum,
                Jobtype = Jobtype,
                EmpCode = EmpCode,
                BillCondition = BillCondion,
                DtStart = DtStart,
                DtEnd = DtEnd
            };

            if (_appSettings.UseDemo)
            {
                PagedListModel<BillJobDetailModel, BillJobFilterModel> model = _dataMockUp.GetMockBillJobDetails(filter, Page, PageSize);

                ViewBag.CurrentPage = Page;
                ViewBag.TotalPages = model.Data.TotalPages;
                ViewBag.PageSize = PageSize;
                ViewBag.TotalItems = model.Data.TotalCount;

                return Json(new
                {
                    success = true,
                    html = await RenderPartialViewAsync("_BillJobPartial", model.Data.Items),
                    pagination = await RenderPartialViewAsync("_PaginationPartial", model),
                    totalCount = model.Data.TotalCount,
                    currentPage = Page,
                    totalPages = model.Data.TotalPages,
                    PageSize
                });
            }
            else
            {
                PagedListModel<BillJobDetailModel, BillJobFilterModel> model = await _billJobService.GetBillJobDetailListAsync(filter, Page, PageSize);

                ViewBag.CurrentPage = Page;
                ViewBag.TotalPages = model.Data.TotalPages;
                ViewBag.PageSize = PageSize;
                ViewBag.TotalItems = model.Data.TotalCount;

                return Json(new
                {
                    success = true,
                    html = await RenderPartialViewAsync("_BillJobPartial", model.Data.Items),
                    pagination = await RenderPartialViewAsync("_PaginationPartial", model),
                    totalCount = model.Data.TotalCount,
                    currentPage = Page,
                    totalPages = model.Data.TotalPages,
                    PageSize
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> BillJobReportExcel(int JobNum = 0, int Jobtype = 0, int EmpCode = 0, string BillCondion = "", DateTime? DtStart = null, DateTime? DtEnd = null)
        {
            BillJobFilterModel filter = new()
            {
                JobNum = JobNum,
                Jobtype = Jobtype,
                EmpCode = EmpCode,
                BillCondition = BillCondion,
                DtStart = DtStart,
                DtEnd = DtEnd
            };

            if (_appSettings.UseDemo)
            {
                var data = _dataMockUp.GetAllMockBillJobDetails();
                return File(_billJobReportService.GenExcelBillJobReport(data), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BillJobReport.xlsx");
            }
            else
            {
                var data = await _billJobService.GetAllBillJobDetailAsync(filter);
                if (data != null && data.Any())
                {
                    return File(_billJobReportService.GenExcelBillJobReport(data), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BillJobReport.xlsx");
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> BillJobReportPDF(int JobNum = 0, int Jobtype = 0, int EmpCode = 0, string BillCondion = "", DateTime? DtStart = null, DateTime? DtEnd = null)
        {
            BillJobFilterModel filter = new()
            {
                JobNum = JobNum,
                Jobtype = Jobtype,
                EmpCode = EmpCode,
                BillCondition = BillCondion,
                DtStart = DtStart,
                DtEnd = DtEnd
            };

            if (_appSettings.UseDemo)
            {
                var data = _dataMockUp.GetAllMockBillJobDetails();

                var pdfBytes = _billJobReportService.GenPDFBillJobReport(data);
                var contentDisposition = $"inline; filename=BillJob_{DateTime.Now:yyyyMMdd}.pdf";
                Response.Headers.Append("Content-Disposition", contentDisposition);

                return File(pdfBytes, "application/pdf");
            }
            else
            {
                var data = await _billJobService.GetAllBillJobDetailAsync(filter);
                if (data != null && data.Any())
                {
                    var pdfBytes = _billJobReportService.GenPDFBillJobReport(data);
                    var contentDisposition = $"inline; filename=BillJob_{DateTime.Now:yyyyMMdd}.pdf";
                    Response.Headers.Append("Content-Disposition", contentDisposition);

                    return File(pdfBytes, "application/pdf");
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        private async Task<string> RenderPartialViewAsync(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.ActionDescriptor.ActionName;

            ViewData.Model = model;

            using var writer = new StringWriter();
            Microsoft.AspNetCore.Mvc.ViewEngines.ICompositeViewEngine? viewEngine = HttpContext.RequestServices.GetService(typeof(Microsoft.AspNetCore.Mvc.ViewEngines.ICompositeViewEngine)) as Microsoft.AspNetCore.Mvc.ViewEngines.ICompositeViewEngine ?? throw new InvalidOperationException("View engine not found.");
            var viewResult = viewEngine.FindView(ControllerContext, viewName, false);

            if (viewResult.View == null)
            {
                throw new InvalidOperationException($"View '{viewName}' not found.");
            }

            var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, writer, new Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelperOptions());

            await viewResult.View.RenderAsync(viewContext);
            return writer.GetStringBuilder().ToString();
        }
    }
}
