using ReactiveUI;
using Shared.DTOs.Equipment;

namespace UIClient.ViewModels.Reports.Act_1_3;

public class ReportHardwareViewModel : ViewModelBase
{
    private bool _isInErrorPTS;

    public HardwareObject Hardware { get; set; }

    public bool IsInErrorPTS
    {
        get => _isInErrorPTS;
        set => this.RaiseAndSetIfChanged(ref _isInErrorPTS, value);
    }
}
