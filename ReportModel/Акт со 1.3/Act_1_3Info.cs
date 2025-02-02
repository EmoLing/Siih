using DB.Models.Equipment;
using ReportModel.Акт_со_1._3;

namespace ReportModel;

public class Act_1_3Info(ComplexHardware complexHardware, List<(Hardware hardware, string incomingInspection, string functioningInspection)> errorPTS = null) : ReportInfo
{
    public override IReportType ReportType => IReportType.Act_so_1_3;

    public Act_1_3Table1Info Table1Info { get; private set; }

    public Act_1_3Table2Info Table2Info { get; private set; }

    public void Initialize() => GenerateTables();

    private void GenerateTables()
    {
        Table1Info = new Act_1_3Table1Info(complexHardware);
        Table1Info.Initialize();

        TablesInfo.Add(Table1Info);

        if (errorPTS is null)
            return;

        var table2Data = GetAct_1_3Table2Data(Table1Info, errorPTS);

        if (table2Data.Count == 0)
            return;

        Table2Info = new Act_1_3Table2Info(table2Data);
        Table2Info.Initialize();

        TablesInfo.Add(Table2Info);
    }

    private List<Act_1_3Table2Data> GetAct_1_3Table2Data(Act_1_3Table1Info act_1_3Table1Info, List<(Hardware hardware, string incomingInspection, string functioningInspection)> errorsPTS)
    {
        var data = new List<Act_1_3Table2Data>(errorPTS.Count);

        foreach (var (hardware, incomingInspection, functioningInspection) in errorsPTS)
        {
            var table1RI = act_1_3Table1Info.RowsInfo.OfType<Act_1_3Table1RowInfo>().FirstOrDefault(ri
                => ri.Name == hardware.Name && ri.SerialNumber == hardware.SerialNumber);

            if (table1RI is null)
                continue;

            data.Add(new Act_1_3Table2Data()
            {
                Number = table1RI.Number,
                HardwareName = table1RI.Name,
                SerialNumber = table1RI.SerialNumber,
                IncomingInspectionResults = incomingInspection,
                FunctioningInspectionResults = functioningInspection,
            });
        }

        return data;
    }
}
