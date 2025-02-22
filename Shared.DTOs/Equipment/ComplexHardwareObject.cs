using Shared.DTOs.Users;

namespace Shared.DTOs.Equipment;

public class ComplexHardwareObject : TransferObject
{
    public string InventoryNumber { get; set; }
    public UserObject User { get; set; }
    public ComplexHardwareType Type { get; set; }
    public List<HardwareObject> Hardwares { get; set; } = [];

    public string ComplexHardwareTypeName => Type switch
    {
        ComplexHardwareType.UserArm => "АРМ пользователя публичного контура",
        ComplexHardwareType.DepartmentalArm => "АРМ пользователя ведомственного контура",
        ComplexHardwareType.Videoconferencing => "Видеоконференцсвязь",
        ComplexHardwareType.ServerComplex => "Серверный комплект",
        _ => "Другое",
    };
}
