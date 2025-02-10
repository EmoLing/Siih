using Common;
using NPOI.XWPF.UserModel;
using ReportModel;
using ReportModel.Акт_со_1._3;

namespace ReportGenerator.Strategy.Act_so_1_3;

public class Act_1_3_Report(ReportInfo act) : Report(act)
{
    private const int _tablesFontSize = 12;

    public override IEnumerable<TableInfo> GetTablesInfo() => act.TablesInfo;

    public override void FillTableRow(XWPFTableRow row, RowInfo rowInfo)
    {
        switch (rowInfo)
        {
            case RowInfo when rowInfo is Act_1_3Table1RowInfo table1RowInfo:
                FillTable1RowCore(row, table1RowInfo);
                return;
            case RowInfo when rowInfo is Act_1_3Table2RowInfo table2RowInfo:
                FillTable2RowCore(row,table2RowInfo);
                return;
            default:
                throw new ArgumentException(Messages.TypeNotFound);
        }
    }

    private void FillTable1RowCore(XWPFTableRow row, Act_1_3Table1RowInfo dataRow)
    {
        row.GetCell(CellsTable1.Number).FillCellWithStyle(dataRow.Number.ToString(), _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable1.Name).FillCellWithStyle(dataRow.Name, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable1.SerialNumber).FillCellWithStyle(dataRow.SerialNumber, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable1.Article).FillCellWithStyle(dataRow.Article, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable1.CreateDate).FillCellWithStyle(dataRow.DateCreate, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable1.InstallPlace).FillCellWithStyle(dataRow.InstallPlace, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable1.SoftwareName).FillCellWithStyle(dataRow.SoftwareName, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable1.SoftwareVersion).FillCellWithStyle(dataRow.SoftwareVersion, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable1.User).FillCellWithStyle(dataRow.User, _tablesFontSize, ParagraphAlignment.CENTER);
    }

    private void FillTable2RowCore(XWPFTableRow row, Act_1_3Table2RowInfo dataRow)
    {
        row.GetCell(CellsTable2.Number).FillCellWithStyle(dataRow.Number.ToString(), _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable2.Name).FillCellWithStyle(dataRow.Name, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable2.SerialNumber).FillCellWithStyle(dataRow.SerialNumber, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable2.IncomingInspectionResults).FillCellWithStyle(dataRow.IncomingInspectionResults, _tablesFontSize, ParagraphAlignment.CENTER);
        row.GetCell(CellsTable2.FunctioningInspectionResults).FillCellWithStyle(dataRow.FunctioningInspectionResults, _tablesFontSize, ParagraphAlignment.CENTER);
    }
}