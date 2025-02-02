namespace ReportGenerator.Strategy.Act_so_1_3;

internal readonly struct CellsTable1
{
    private CellsTable1(int value)
    {
        Value = value;
    }

    private int Value { get; }

    public static implicit operator CellsTable1(int value) => new(value);

    public static implicit operator int(CellsTable1 cell) => cell.Value;

    public static readonly CellsTable1 Number = 0;
    public static readonly CellsTable1 Name = 1;
    public static readonly CellsTable1 SerialNumber = 2;
    public static readonly CellsTable1 Article = 3;
    public static readonly CellsTable1 CreateDate = 4;
    public static readonly CellsTable1 InstallPlace = 5;
    public static readonly CellsTable1 SoftwareName = 6;
    public static readonly CellsTable1 SoftwareVersion = 7;
    public static readonly CellsTable1 User = 8;
}
