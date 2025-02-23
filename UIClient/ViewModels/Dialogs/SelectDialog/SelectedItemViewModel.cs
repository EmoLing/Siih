using ReactiveUI;
using Shared.DTOs;

namespace UIClient.ViewModels.Dialogs.SelectDialog;

public class SelectedItemViewModel : ViewModelBase
{
    private bool _selected;
    public TransferObject TransferObject { get; set; }
    public bool IsSelected
    {
        get => _selected;
        set => this.RaiseAndSetIfChanged(ref _selected, value);
    }
}
