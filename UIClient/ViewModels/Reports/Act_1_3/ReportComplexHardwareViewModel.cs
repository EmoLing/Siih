using DB.Models.Equipment;
using System.Collections.ObjectModel;
using System.Linq;

namespace UIClient.ViewModels.Reports.Act_1_3;

public class ReportComplexHardwareViewModel
{
    public ReportComplexHardwareViewModel(ComplexHardware complexHardware)
    {
        ComplexHardware = complexHardware;
        var hardwares = complexHardware.Hardwares.Select(h => new ReportHardwareViewModel() { Hardware = h });
        Hardwares = new ObservableCollection<ReportHardwareViewModel>(hardwares);
    }

    public ComplexHardware ComplexHardware { get; set; }
    public ObservableCollection<ReportHardwareViewModel> Hardwares { get; set; }
}
