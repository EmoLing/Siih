using DB.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UIClient.ViewModels.Reports.Act_1_3;

public class ReportHardwareViewModel
{
    public Hardware Hardware { get; set; }
    public bool IsInErrorPTS { get; set; }
}
