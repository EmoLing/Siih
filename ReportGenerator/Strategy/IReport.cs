using NPOI.XWPF.UserModel;
using ReportModel;

namespace ReportGenerator.Strategy;

public interface IReport
{
    IEnumerable<TableInfo> GetTablesInfo();

    void FillTableRow(XWPFTableRow row, RowInfo rowInfo);
}
