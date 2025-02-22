namespace Shared.DTOs.Equipment;

public class HardwareObject : TransferObject
{
    public string SerialNumber { get; set; }
    public string Article { get; set; }
    public DateOnly DateCreate { get; set; }

    public ComplexHardwareObject ComplexHardware { get; set; }
    public List<SoftwareObject> Softwares { get; set; } = [];
}
