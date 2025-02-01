using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportModel.Акт_со_1._3;

public class Act_1_3Table1Row : Act_1_3TableRow
{
    public string Article { get; set; }
    public required string DateCreate { get; set; }
    public required string InstallPlace { get; set; }
    public string SoftwareName { get; set; }
    public string SoftwareVersion { get; set; }
    public required string User { get; set; }
}
