using Shared.DTOs.Equipment;

namespace ReportModel.Акт_со_1._3;

public class ErrorPTS
{
    public HardwareObject Hardware { get; set; }

    public string IncomingInspection { get; set; } = String.Empty;

    public string FunctioningInspection { get; set; } = String.Empty;
}
