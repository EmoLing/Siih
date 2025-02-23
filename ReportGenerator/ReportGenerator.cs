using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using ReportGenerator.Strategy;
using ReportModel;
using System.Diagnostics;

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
            //var mergeRows = new List<MergeRows>();
            //var mergedRowInfos = tableInfo.RowsInfo.Where(ri => ri.IsStartMerge || ri.IsEndMerge).ToList();

            //for (int i = 0; i < mergedRowInfos.Count; i += 2)
            //{
            //    mergeRows.Add(new MergeRows()
            //    {
            //        PairStart = (null, mergedRowInfos[i]),
            //        PairEnd = (null, mergedRowInfos[i + 1])
            //    });
            //}

            (XWPFTableRow row, RowInfo rowInfo) startRow = (null, null);
            foreach (var rowInfo in tableInfo.RowsInfo)
            {
                var row = table.GetRow(startRowIndex);

                if (row is null)
                {
                    row = table.CreateRow();
                    row.AddNewTableCell(); // При создании строки создает на одну ячейку меньше, нужно добавлять
                }

                report.FillTableRow(row, rowInfo);

                if (rowInfo.HasMerge)
                {
                    if (rowInfo.IsStartMerge)
                    {
                        startRow = (row, rowInfo);
                        report.MergeRows(row, rowInfo, ST_Merge.restart);
                    }
                    else
                        report.MergeRows(row, rowInfo, ST_Merge.@continue);

                    if (rowInfo.IsEndMerge)
                        report.FillTableRowAfterMerge(startRow.row, startRow.rowInfo, rowInfo);
                }

                startRowIndex++;
            }

            //StartMergeRows(mergeRows);
        }
    }

    private static void StartMergeRows(List<MergeRows> mergeRows)
    {
        if (mergeRows.Count == 0)
            return;

        foreach (var mergeRow in mergeRows)
        {
            int numberStart = mergeRow.PairEnd.rowInfo.Number;
            int numberEnd = mergeRow.PairEnd.rowInfo.Number;
            int dif = numberEnd - numberStart;

            mergeRow.PairStart.row.MergeCells(0, dif);
        }
    }

    private static string GetReportTemplatePath(ReportType reportType) => reportType switch
    {
        ReportType.Act_so_1_3 => "act_1.3.docx",
        _ => String.Empty,
    };

    private class MergeRows
    {
        public (XWPFTableRow row, RowInfo rowInfo) PairStart { get; set; }
        public (XWPFTableRow row, RowInfo rowInfo) PairEnd { get; set; }
    }
}