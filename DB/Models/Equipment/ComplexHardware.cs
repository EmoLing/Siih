using DB.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace DB.Models.Equipment;

public class ComplexHardware : DatabaseObject
{
    public string InventoryNumber { get; set; }
    public User User { get; set; }
    public ComplexHardwareType Type { get; set; }
    public List<Hardware> Hardwares { get; set; } = [];

    [NotMapped]
    public string ComplexHardwareTypeName => Type switch
    {
        ComplexHardwareType.UserArm => "АРМ пользователя публичного контура",
        ComplexHardwareType.DepartmentalArm => "АРМ пользователя ведомственного контура",
        ComplexHardwareType.Videoconferencing => "Видеоконференцсвязь",
        ComplexHardwareType.ServerComplex => "Серверный комплект",
        _ => "Другое",
    };
}
