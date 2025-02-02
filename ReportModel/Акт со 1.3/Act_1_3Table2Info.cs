using DB.Models.Equipment;

namespace ReportModel.Акт_со_1._3;

public class Act_1_3Table2Info(List<Act_1_3Table2Data> data) : TableInfo
{
    protected override int StartRow => 2;

    public List<Act_1_3Table2RowInfo> Rows { get; } = [];

    public void Initialize() => data.ForEach(d =>
    {
        Rows.Add(new Act_1_3Table2RowInfo()
        {
            Number = d.Number,
            Name = d.HardwareName,
            SerialNumber = d.SerialNumber,
            FunctioningInspectionResults = d.FunctioningInspectionResults,
            IncomingInspectionResults = d.IncomingInspectionResults,
        });
    });
}
