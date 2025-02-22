using System.Collections.ObjectModel;
using System.Linq;
using Shared.DTOs.Equipment;

namespace UIClient.ViewModels.Reports.Act_1_3;

public class ReportComplexHardwareViewModel
{
    public ReportComplexHardwareViewModel(ComplexHardwareObject complexHardware)
    {
        ComplexHardware = complexHardware;
        var hardwares = complexHardware.Hardwares.Select(h => new ReportHardwareViewModel() { Hardware = h });
        Hardwares = new ObservableCollection<ReportHardwareViewModel>(hardwares);
    }

    public ComplexHardwareObject ComplexHardware { get; set; }
    public ObservableCollection<ReportHardwareViewModel> Hardwares { get; set; }
}
