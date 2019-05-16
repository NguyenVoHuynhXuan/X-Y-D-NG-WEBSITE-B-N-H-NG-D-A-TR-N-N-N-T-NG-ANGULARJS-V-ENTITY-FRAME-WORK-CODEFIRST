using OfficeOpenXml;
using OfficeOpenXml.Table;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace uStora.Common
{
    public class ReportHelper
    {
        public static Task GenerateXls<T>(List<T> datasource, string filePath)
        {
            return Task.Run(() =>
            {
                using (ExcelPackage pck = new ExcelPackage(new FileInfo(filePath)))
                {
                    //Create the worksheet
                    ExcelWorksheet ws = pck.Workbook.Worksheets.Add(typeof(T).Name + "s");
                    ws.Cells["A1"].LoadFromCollection(datasource, true, TableStyles.Medium18);
                    ws.Cells.AutoFitColumns();
                    pck.Save();
                }
            });
        }
    }
}
