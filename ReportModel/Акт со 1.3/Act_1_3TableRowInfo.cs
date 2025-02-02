using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportModel.Акт_со_1._3;

public abstract class Act_1_3TableRowInfo : RowInfo
{
    public required int Number { get; set; }
    public required string Name { get; set; }
    public required string SerialNumber { get; set; }
}
