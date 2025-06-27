using JPBillJobDetail.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using JPBillJobDetail.Models;
using System.Reflection;

namespace JPBillJobDetail.Service.Implement
{
    public class BillJobReportService(Serilog.ILogger logger) : IBillJobReportService
    {
        private readonly Serilog.ILogger _logger = logger;

        public byte[] GenBillJobReport(IEnumerable<BillJobDetailModel> data)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("BillJobDetail");

            // Map ชื่อ Property → ชื่อภาษาไทย
            var headerMap = new Dictionary<string, string>
            {
                ["CustCode"] = "ลูกค้า",
                ["OrderNo"] = "เลขที่ออเดอร์",
                ["ListNo"] = "ลำดับที่",
                ["DocNo"] = "เลขที่เอกสาร",
                ["JobBarcode"] = "เลขที่บิลจ่ายงาน",
                ["EmpCode"] = "รหัสช่าง",
                ["EmpName"] = "ชื่อข่าง",
                ["JobName"] = "ประเภทงาน",
                ["Article"] = "รหัสชิ้นงาน",
                ["TDesArt"] = "รายละเอียดชิ้นงาน",
                ["Num"] = "ครั้งที่",
                ["OkTtl"] = "จำนวนใช้ได้",
                ["RtTtl"] = "จำนวนคืนร้าน",
                ["DmTtl"] = "จำนวนงานเสีย",
                ["EpTtl"] = "จำนวนคืนช่าง",
                ["IdNo1"] = "รหัสอาการที่ 1",
                ["Remark1"] = "อาการที่ 1",
                ["IdNo2"] = "รหัสอาการที่ 2",
                ["Remark2"] = "อาการที่ 2",
                ["IdNo3"] = "รหัสอาการที่ 3",
                ["Remark3"] = "อาการที่ 3",
                ["IdNo4"] = "รหัสอาการที่ 4",
                ["Remark4"] = "อาการที่ 4",
                ["IdNo5"] = "รหัสอาการที่ 5",
                ["Remark5"] = "อาการที่ 5",
                ["IdNo6"] = "รหัสอาการที่ 6",
                ["Remark6"] = "อาการที่ 6",
                ["MDate"] = "วันที่"
            };

            // สร้าง Header แถวที่ 1
            var props = typeof(BillJobDetailModel).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                string propName = props[i].Name;
                worksheet.Cell(1, i + 1).Value = headerMap.TryGetValue(propName, out string? value) ? value : propName;
            }

            // ใส่ข้อมูลแถวถัดไป
            var dataList = data.ToList();
            for (int row = 0; row < dataList.Count; row++)
            {
                for (int col = 0; col < props.Length; col++)
                {
                    var value = props[col].GetValue(dataList[row]);
                    worksheet.Cell(row + 2, col + 1).Value = value != null ? XLCellValue.FromObject(value) : XLCellValue.FromObject(string.Empty);
                }
            }

            // ปรับความกว้างอัตโนมัติ
            worksheet.Columns().AdjustToContents();

            // บันทึกเป็นไฟล์
            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }
    }
}
