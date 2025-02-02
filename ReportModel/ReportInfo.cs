using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportModel;

public abstract class ReportInfo
{
    public List<TableInfo> TablesInfo { get; } = [];

    public virtual IReportType ReportType { get; }
}
