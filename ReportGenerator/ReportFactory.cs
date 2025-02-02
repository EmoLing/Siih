using Common;
using ReportGenerator.Strategy;
using ReportGenerator.Strategy.Act_so_1_3;
using ReportModel;

namespace ReportGenerator;

internal class ReportFactory
{
    public static Report CreateReport(ReportInfo reportInfo) => reportInfo.ReportType switch
    {
        IReportType.Act_so_1_3 => new Act_1_3_Report(reportInfo),
        _ => throw new ArgumentException(Messages.TypeNotFound)
    };
}
