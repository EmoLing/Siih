using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using ReportModel;

namespace ReportGenerator.Strategy;

public interface IReport
{
    IEnumerable<TableInfo> GetTablesInfo();

    void FillTableRow(XWPFTableRow row, RowInfo rowInfo);

    void MergeRows(XWPFTableRow row, RowInfo rowInfo, ST_Merge sT_Merge);

    void FillTableRowAfterMerge(XWPFTableRow row, RowInfo startRowInfo, RowInfo endRowInfo) { }
}
