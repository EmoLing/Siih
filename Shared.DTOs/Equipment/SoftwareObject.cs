namespace Shared.DTOs.Equipment;

public class SoftwareObject : TransferObject
{
    public string Version { get; set; }

    public List<HardwareObject> Hardwares { get; set; } = [];
}
