using DB.Models.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Equipment;

public class ComplexHardware : DatabaseObject
{
    public string InventoryNumber { get; set; }
    public User User { get; set; }
    public ComplexHardwareType Type { get; set; }
    public List<Hardware> Hardwares { get; set; } = [];
}
