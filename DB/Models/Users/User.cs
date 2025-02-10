﻿using DB.Models.Departments;
using DB.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models.Users;

public class User : DatabaseObject
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Surname { get; set; }
    public Cabinet Cabinet { get; set; }
    public JobTitle JobTitle { get; set; }
    public List<ComplexHardware> ComplexHardwares { get; set; } = [];

    public override string ToString() => $"{LastName} {FirstName?.FirstOrDefault()}.{LastName?.FirstOrDefault()}.";
    
}
