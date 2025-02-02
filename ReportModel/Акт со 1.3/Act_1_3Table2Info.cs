using Common;

namespace ReportModel.Акт_со_1._3;

public class Act_1_3Table2Info(List<Act_1_3Table2Data> data) : TableInfo
{
    protected override int StartRow => 2;

    public override string TableName => TableNames.Act_1_3Table2;

    public void Initialize() => data.ForEach(d =>
    {
        RowsInfo.Add(new Act_1_3Table2RowInfo()
        {
            Number = d.Number,
            Name = d.HardwareName,
            SerialNumber = d.SerialNumber,
            FunctioningInspectionResults = d.FunctioningInspectionResults,
            IncomingInspectionResults = d.IncomingInspectionResults,
        });
    });
}
