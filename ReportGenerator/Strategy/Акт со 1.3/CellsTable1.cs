namespace ReportGenerator.Strategy.Act_so_1_3;

internal readonly struct CellTable1
{
    private CellTable1(int value)
    {
        Value = value;
    }

    private int Value { get; }

    public static implicit operator CellTable1(int value) => new(value);

    public static implicit operator int(CellTable1 cell) => cell.Value;

    public static readonly CellTable1 Number = 0;
    public static readonly CellTable1 Name = 1;
    public static readonly CellTable1 SerialNumber = 2;
    public static readonly CellTable1 Article = 3;
    public static readonly CellTable1 CreateDate = 4;
    public static readonly CellTable1 InstallPlace = 5;
    public static readonly CellTable1 SoftwareName = 6;
    public static readonly CellTable1 SoftwareVersion = 7;
    public static readonly CellTable1 User = 8;
}
