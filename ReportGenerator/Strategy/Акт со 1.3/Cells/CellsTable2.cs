namespace ReportGenerator.Strategy.Act_so_1_3;

internal readonly struct CellsTable2
{
    private CellsTable2(int value)
    {
        Value = value;
    }

    private int Value { get; }

    public static implicit operator CellsTable2(int value) => new(value);

    public static implicit operator int(CellsTable2 cell) => cell.Value;

    public static readonly CellsTable2 Number = 0;
    public static readonly CellsTable2 Name = 1;
    public static readonly CellsTable2 SerialNumber = 2;
    public static readonly CellsTable2 IncomingInspectionResults = 3;
    public static readonly CellsTable2 FunctioningInspectionResults = 4;
}
