using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using ReportModel;

namespace ReportGenerator.Strategy;

public abstract class Report(ReportInfo reportInfo) : IReport
{
    public virtual IEnumerable<TableInfo> GetTablesInfo() => [];
    public virtual void FillTableRow(XWPFTableRow row, RowInfo rowInfo) { }
    public virtual void MergeRows(XWPFTableRow row, RowInfo rowInfo, ST_Merge sT_Merge) { }
    public virtual void FillTableRowAfterMerge(XWPFTableRow row, RowInfo startRowInfo, RowInfo endRowInfo) { }
}
