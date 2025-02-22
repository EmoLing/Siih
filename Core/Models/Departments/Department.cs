namespace Core.Models.Departments;

public class Department : DatabaseObject
{
    public List<Cabinet> Cabinets { get; set; }
}
