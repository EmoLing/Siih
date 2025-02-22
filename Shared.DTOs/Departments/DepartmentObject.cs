namespace Shared.DTOs.Departments;

public class DepartmentObject : TransferObject
{
    public List<CabinetObject> Cabinets { get; set; }
}
