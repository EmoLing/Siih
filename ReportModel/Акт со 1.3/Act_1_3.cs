using DB.Models.Equipment;
using ReportModel.Акт_со_1._3;

namespace ReportModel;

public class Act_1_3(ComplexHardware complexHardware, List<(Hardware hardware, string incomingInspection, string functioningInspection)> errorPTS = null)
{
    public Act_1_3Table1 Table1 { get; private set; }

    public Act_1_3Table2 Table2 { get; private set; }

    public void Initialize() => GenerateTables();

    private void GenerateTables()
    {
        Table1 = new Act_1_3Table1(complexHardware);
        Table1.Initialize();

        if (errorPTS is null)
            return;

        Table2 = new Act_1_3Table2(errorPTS);
        Table2.Initialize();
    }
}
