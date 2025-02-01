using NPOI.XWPF.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportGenerator.Strategy;

public interface IReport
{
    IEnumerable<object> GetData();

    void FillTableRow(XWPFTableRow row, object data);
}
