using Shared.DTOs.Departments;
using Shared.DTOs.Equipment;

namespace Shared.DTOs.Users;

public class UserObject : TransferObject
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Surname { get; set; }
    public CabinetObject Cabinet { get; set; }
    public JobTitleObject JobTitle { get; set; }
    public List<ComplexHardwareObject> ComplexHardwares { get; set; } = [];

    public override string ToString() => $"{LastName} {FirstName?.FirstOrDefault()}.{LastName?.FirstOrDefault()}.";

}
