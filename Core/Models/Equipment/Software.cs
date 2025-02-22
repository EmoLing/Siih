namespace Core.Models.Equipment;

public class Software : DatabaseObject
{
    public string Version { get; set; }

    public List<Hardware> Hardwares { get; set; } = [];
}
