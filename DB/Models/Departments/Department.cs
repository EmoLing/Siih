using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Departments;

public class Department : DatabaseObject
{
    public List<Cabinet> Cabinets { get; set; }
}
