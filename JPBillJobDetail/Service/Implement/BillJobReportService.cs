using ClosedXML.Excel;
using JPBillJobDetail.Models;
using JPBillJobDetail.Service.Interface;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace JPBillJobDetail.Service.Implement
{
    public class BillJobReportService : IBillJobReportService
    {
        public byte[] GenExcelBillJobReport(IEnumerable<BillJobDetailModel> data)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("BillJobDetail");

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

            var props = typeof(BillJobDetailModel).GetProperties();
            for (int i = 0; i < props.Length; i++)
            {
                string propName = props[i].Name;
                worksheet.Cell(1, i + 1).Value = headerMap.TryGetValue(propName, out string? value) ? value : propName;
            }

            var dataList = data.ToList();
            for (int row = 0; row < dataList.Count; row++)
            {
                for (int col = 0; col < props.Length; col++)
                {
                    var value = props[col].GetValue(dataList[row]);
                    worksheet.Cell(row + 2, col + 1).Value = value != null ? XLCellValue.FromObject(value) : XLCellValue.FromObject(string.Empty);
                }
            }

            worksheet.Columns().AdjustToContents();

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            return stream.ToArray();
        }

        public byte[] GenPDFBillJobReport(IEnumerable<BillJobDetailModel> data)
        {
            QuestPDF.Settings.License = LicenseType.Community;

            var document = Document.Create(container =>
            {
                var dataList = data.ToList();
                const int cardsPerPage = 4;

                // แบ่งข้อมูลเป็นกลุมๆ ตามจำนวนการ์ดต่อหน้า
                var chunkedData = dataList
                    .Select((item, index) => new { item, index })
                    .GroupBy(x => x.index / cardsPerPage)
                    .Select(g => g.Select(x => x.item).ToList())
                    .ToList();

                foreach (var pageData in chunkedData)
                {
                    container.Page(page =>
                    {
                        page.Size(PageSizes.A4.Portrait());
                        page.Margin(1.0f, Unit.Centimetre);
                        page.PageColor(Colors.White);
                        page.DefaultTextStyle(x => x.FontSize(10).FontFamily("Kanit").FontColor(Colors.Black));

                        // Header
                        page.Header()
                            .PaddingVertical(10)
                            .Row(row =>
                            {
                                row.RelativeItem();

                                row.RelativeItem()
                                   .AlignCenter()
                                   .Text("รายละเอียดงานที่ลงบิล")
                                   .Bold().FontSize(16);

                                row.RelativeItem()
                                   .AlignRight()
                                   .Text($"วันที่ออกรายงาน : {DateTime.Now:dd/MM/yyyy}")
                                   .FontSize(8);
                            });

                        // Content - แสดงการ์ดในหน้านี้
                        page.Content()
                            .PaddingTop(20)
                            .Column(column =>
                            {
                                foreach (var item in pageData)
                                {
                                    column.Item()
                                          .PaddingBottom(15) // ระยะห่างระหว่างการ์ด
                                          .Element(container => CreateFlexibleTable(container, item));
                                }
                            });

                        // Footer - แสดงหมายเลขหน้า
                        page.Footer()
                            .AlignCenter()
                            .Text($"หน้า {chunkedData.IndexOf(pageData) + 1} จาก {chunkedData.Count}")
                            .FontSize(8);
                    });
                }
            });

            return document.GeneratePdf();
        }

        private static void CreateFlexibleTable(IContainer container, BillJobDetailModel item)
        {
            container
                .Border(0.5f)
                .BorderColor(Colors.Black)
                .Table(table =>
                {
                    // กำหนดคอลัมน์หลัก - 64 คอลัมน์เพื่อความยืดหยุ่นสูงสุด
                    table.ColumnsDefinition(columns =>
                    {
                        for (int i = 0; i < 64; i++)
                        {
                            columns.RelativeColumn(1f);
                        }
                    });

                    // แถวที่ 1: OrderNo/CustCode, ListNo, DocNo, JobBarcode, mDate
                    CreateRowWithCustomWidths(table,
                    [
                        new CellData { Text = "ออเดอร์/ลูกค้า", IsHeader = true, Span = 8, Alignment = TextAlignment.Center },
                        new CellData { Text = $"{item.OrderNo} / {item.CustCode}", Span = 9 },
                        new CellData { Text = "ลำดับที่", IsHeader = true, Span = 4, Alignment = TextAlignment.Center },
                        new CellData { Text = item.ListNo?.ToString() ?? "-", Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = "เลขที่เอกสาร", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.DocNo ?? "-", Span = 8 },
                        new CellData { Text = "เลขที่บิลจ่ายงาน", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = item.JobBarcode ?? "-", Span = 8 },
                        new CellData { Text = "วันที่", IsHeader = true, Span = 4, Alignment = TextAlignment.Center },
                        new CellData { Text = item.MDate.ToString("dd/MM/yyyy") ?? "-", Span = 6, Alignment = TextAlignment.Center }
                    ]);

                    // แถวที่ 2: EmpCode, EmpName, JobName, Article, Num
                    CreateRowWithCustomWidths(table,
                    [
                        new CellData { Text = "รหัสช่าง", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.EmpCode != 0 ? item.EmpCode.ToString() : "-", Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "ชื่อข่าง", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.EmpName ?? "-", Span = 17 },
                        new CellData { Text = "ประเภทงาน", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.JobName ?? "-", Span = 8 },
                        new CellData { Text = "รหัสชิ้นงาน", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.Article ?? "-", Span = 8 },
                        new CellData { Text = "ครั้งที่", IsHeader = true, Span = 3, Alignment = TextAlignment.Center },
                        new CellData { Text = item.Num?.ToString() ?? "-", Span = 3, Alignment = TextAlignment.Center }
                    ]);

                    // แถวที่ 3: OkTtl + IdNo1, Remark1
                    CreateRowWithCustomWidths(table,
                    [
                        new CellData { Text = "จำนวนใช้ได้", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = FormatNumber(item.OkTtl), Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "รหัสอาการที่ 1", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = item.IdNo1 ?? "-", Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "อาการที่ 1", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.Remark1 ?? "-", Span = 37 }
                    ]);

                    // แถวที่ 4: RtTtl + IdNo2, Remark2
                    CreateRowWithCustomWidths(table,
                    [
                        new CellData { Text = "จำนวนคืนร้าน", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = FormatNumber(item.RtTtl), Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "รหัสอาการที่ 2", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = item.IdNo2 ?? "-", Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "อาการที่ 2", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.Remark2 ?? "-", Span = 37 }
                    ]);

                    // แถวที่ 5: DmTtl + IdNo3, Remark3
                    CreateRowWithCustomWidths(table,
                    [
                        new CellData { Text = "จำนวนงานเสีย", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = FormatNumber(item.DmTtl), Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "รหัสอาการที่ 3", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = item.IdNo3 ?? "-", Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "อาการที่ 3", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.Remark3 ?? "-", Span = 37 }
                    ]);

                    // แถวที่ 6: EpTtl + IdNo4, Remark4
                    CreateRowWithCustomWidths(table,
                    [
                        new CellData { Text = "จำนวนคืนช่าง", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = FormatNumber(item.EpTtl), Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "รหัสอาการที่ 4", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = item.IdNo4 ?? "-", Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "อาการที่ 4", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.Remark4 ?? "-", Span = 37 }
                    ]);

                    // แถวที่ 7: เซลล์แรกใช้ RowSpan(2)
                    table.Cell().Row(7).Column(1).RowSpan(2).ColumnSpan(11).Element(container => container
                        .Border(0.5f)
                        .BorderColor(Colors.Black)
                        .Padding(4)
                        .AlignMiddle()
                        .AlignCenter());

                    // แถวที่ 7: Empty + IdNo5, Remark5
                    CreateRowWithCustomWidths(table,
                    [
                        new CellData { Text = "รหัสอาการที่ 5", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = item.IdNo5 ?? "-", Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "อาการที่ 5", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.Remark5 ?? "-", Span = 37 }
                    ]);

                    // แถวที่ 8: Empty + IdNo6, Remark6
                    CreateRowWithCustomWidths(table,
                    [
                        new CellData { Text = "รหัสอาการที่ 6", IsHeader = true, Span = 6, Alignment = TextAlignment.Center },
                        new CellData { Text = item.IdNo6 ?? "-", Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = "อาการที่ 6", IsHeader = true, Span = 5, Alignment = TextAlignment.Center },
                        new CellData { Text = item.Remark6 ?? "-", Span = 37 }
                    ]);
                });
        }

        private static void CreateRowWithCustomWidths(TableDescriptor table, CellData[] cells)
        {
            foreach (var cell in cells)
            {
                if (cell.IsHeader)
                {
                    CreateFlexibleFieldCell(table, cell.Text, cell.Span, cell.Alignment);
                }
                else
                {
                    CreateFlexibleValueCell(table, cell.Text, cell.Span, cell.Alignment);
                }
            }
        }

        private static void CreateFlexibleFieldCell(TableDescriptor table, string text, int columnSpan = 1, TextAlignment alignment = TextAlignment.Center)
        {
            var cellContainer = table.Cell().ColumnSpan((uint)columnSpan).Element(container => container
                .Background(Colors.Grey.Lighten3)
                .Border(0.5f)
                .BorderColor(Colors.Black)
                .Padding(4)
                .AlignMiddle());

            switch (alignment)
            {
                case TextAlignment.Center:
                    cellContainer.AlignCenter().Text(text).Bold().FontSize(6);
                    break;
                case TextAlignment.Right:
                    cellContainer.AlignRight().Text(text).Bold().FontSize(6);
                    break;
                default:
                    cellContainer.AlignLeft().Text(text).Bold().FontSize(6);
                    break;
            }
        }

        private static void CreateFlexibleValueCell(TableDescriptor table, string text, int columnSpan = 1, TextAlignment alignment = TextAlignment.Left)
        {
            var cellContainer = table.Cell().ColumnSpan((uint)columnSpan).Element(container => container
                .Border(0.5f)
                .BorderColor(Colors.Black)
                .Padding(4)
                .AlignMiddle());

            switch (alignment)
            {
                case TextAlignment.Center:
                    cellContainer.AlignCenter().Text(text).FontSize(6);
                    break;
                case TextAlignment.Right:
                    cellContainer.AlignRight().Text(text).FontSize(6);
                    break;
                default:
                    cellContainer.AlignLeft().Text(text).FontSize(6);
                    break;
            }
        }

        private static string FormatNumber(decimal? value)
        {
            return value?.ToString("#,##0") ?? "-";
        }
    }
}
