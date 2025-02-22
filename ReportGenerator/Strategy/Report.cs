using NPOI.XWPF.UserModel;
using ReportModel;

namespace ReportGenerator.Strategy;

public abstract class Report(ReportInfo reportInfo) : IReport
{
    public virtual IEnumerable<TableInfo> GetTablesInfo() => [];
    public virtual void FillTableRow(XWPFTableRow row, RowInfo rowInfo) { }
}
