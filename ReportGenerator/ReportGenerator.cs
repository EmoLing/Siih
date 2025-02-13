using NPOI.XWPF.UserModel;
using ReportGenerator.Strategy;
using ReportModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator;

public static class ReportGenerator
{
    public static void Start(ReportInfo reportInfo, string reportName)
    {
        string templatePath = GetReportTemplatePath(reportInfo.ReportType);
        using var document = new XWPFDocument(File.OpenRead(templatePath));

        var report = ReportFactory.CreateReport(reportInfo);
        FillTablesFromReport(report, document);
        
        string newPathFile = $"{Path.GetTempPath()}{reportName}.docx";

        document.SaveAs(newPathFile);
        Process.Start("explorer.exe", $"/select, \"{newPathFile}\"");
    }

    private static void FillTablesFromReport(IReport report, XWPFDocument document)
    {
        var tablesInfo = report.GetTablesInfo();

        foreach (var tableInfo in tablesInfo)
        {
            var table = document.FindTableByName(tableInfo.TableName);

            if (table is null)
                continue;

            if (table.NumberOfRows < 2)
                continue;

            int startRowIndex = tableInfo.StartIndexRow;

            foreach (var rowInfo in tableInfo.RowsInfo)
            {
                var row = table.GetRow(startRowIndex);

                if (row is null)
                {
                    row = table.CreateRow();
                    row.AddNewTableCell(); // При создании строки создает на одну ячейку меньше, нада добавлять
                }

                report.FillTableRow(row, rowInfo);

                startRowIndex++;
            }
        }
    }

    private static string GetReportTemplatePath(ReportType reportType) => reportType switch
    {
        ReportType.Act_so_1_3 => "act_1.3.docx",
        _ => String.Empty,
    };
}
