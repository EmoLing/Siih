using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Departments;

public class Cabinet : DatabaseObject
{
    public Department Department { get; set; }
}
