using Shared.DTOs.Equipment;

namespace UIClient.ViewModels.Dialogs.Equipment;

public class SelectedHardwareViewModel
{
    public HardwareObject Hardware { get; set; }
    public bool IsSelected { get; set; }
}
