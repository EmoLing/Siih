using DB.Models.Departments;

namespace UIClient.ViewModels.Dialogs.Departments;

public class SelectedCabinetViewModel
{
    public Cabinet Cabinet { get; set; }
    public bool IsSelected { get; set; }
}
