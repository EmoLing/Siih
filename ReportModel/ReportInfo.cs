namespace ReportModel;

public abstract class ReportInfo
{
    public List<TableInfo> TablesInfo { get; } = [];

    public virtual ReportType ReportType { get; }
}
