using DB.Models.Equipment;

namespace UIClient.ViewModels.Dialogs.Equipment;

public class SelectedHardwareViewModel
{
    public Hardware Hardware { get; set; }
    public bool IsSelected { get; set; }
}
