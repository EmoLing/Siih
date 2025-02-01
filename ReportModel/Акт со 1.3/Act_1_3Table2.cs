using DB.Models.Equipment;

namespace ReportModel.Акт_со_1._3;

public class Act_1_3Table2(List<(Hardware hardware, string incomingInspection, string functioningInspection)> errorsPTS)
{
    public List<Act_1_3Table2Row> Rows { get; } = [];

    public void Initialize() => errorsPTS.ForEach(e =>
    {
        Rows.Add(new Act_1_3Table2Row()
        {
            Name = e.hardware.Name,
            SerialNumber = e.hardware.SerialNumber,
            FunctioningInspectionResults = e.functioningInspection,
            IncomingInspectionResults = e.incomingInspection,
        });
    });
}
