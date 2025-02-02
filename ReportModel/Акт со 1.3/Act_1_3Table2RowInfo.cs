using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportModel.Акт_со_1._3;

public class Act_1_3Table2RowInfo : Act_1_3TableRowInfo
{
    /// <summary>Результаты входного контроля с описанием нарушений</summary>
    public string IncomingInspectionResults { get; set; }

    /// <summary>Результаты контроля функционирования с описанием нарушений</summary>
    public string FunctioningInspectionResults { get; set; }
}
