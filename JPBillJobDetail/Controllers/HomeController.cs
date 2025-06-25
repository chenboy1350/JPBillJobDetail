using JPBillJobDetail.Models;
using JPBillJobDetail.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace JPBillJobDetail.Controllers
{
    public class HomeController(IBillJobService billJobService, IDataMockUpService dataMockUp, IOptions<AppSettingModel> options) : Controller
    {
        private readonly IBillJobService _billJobService = billJobService;
        private readonly IDataMockUpService _dataMockUp = dataMockUp;
        private readonly AppSettingModel _appSettings = options.Value;

        public IActionResult Index()
        {
            if (_appSettings.UseDemo)
            {
                ViewBag.JobGroupList = _dataMockUp.GetJobGroupList();
            }
            else
            {
                ViewBag.JobGroupList = _billJobService.GetJobGroupList();
            }

            return View();
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
