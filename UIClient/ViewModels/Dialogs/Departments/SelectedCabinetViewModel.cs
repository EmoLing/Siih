using Shared.DTOs.Departments;

namespace UIClient.ViewModels.Dialogs.Departments;

public class SelectedCabinetViewModel
{
    public CabinetObject Cabinet { get; set; }
    public bool IsSelected { get; set; }
}
