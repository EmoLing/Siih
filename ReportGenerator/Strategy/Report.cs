using NPOI.XWPF.UserModel;
using ReportModel;

namespace ReportGenerator.Strategy;

public abstract class Report(ReportInfo reportInfo) : IReport
{
    public virtual void FillTableRow(XWPFTableRow row, RowInfo rowInfo) { }

    public virtual IEnumerable<TableInfo> GetTablesInfo() => [];
}
