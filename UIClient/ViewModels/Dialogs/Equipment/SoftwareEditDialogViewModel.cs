using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Shared.DTOs.Equipment;
using UIClient.Services;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs.Equipment;

public class SoftwareEditDialogViewModel : ViewModel
{
    private string _name;
    private string _version;

    public SoftwareEditDialogViewModel(MasterApiService apiService, SoftwareObject softwareObject)
        : base(apiService)
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);

        if (softwareObject is not null)
        {
            Name = softwareObject.Name;
            Version = softwareObject.Version;
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string Version
    {
        get => _version;
        set => this.RaiseAndSetIfChanged(ref _version, value);
    }

    protected override Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }

    private void Save() => CloseDialog(true);

    private void Cancel() => CloseDialog(false);

    private void CloseDialog(bool result)
    {
        if (View is IClosable closable)
            closable.Close(result);
    }
}
