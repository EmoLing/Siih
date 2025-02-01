using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Equipment;

public class Hardware : DatabaseObject
{
    public string SerialNumber { get; set; }
    public string Article { get; set; }
    public DateOnly DateCreate { get; set; }

    public ComplexHardware ComplexHardware { get; set; }
    public List<Software> Softwares { get; set; } = [];
}
