namespace ReportModel;

public abstract class TableInfo
{
    protected abstract int StartRow { get; }

    public abstract string TableName { get; }

    public virtual int StartIndexRow => StartRow;

    public List<RowInfo> RowsInfo { get; } = [];
}
