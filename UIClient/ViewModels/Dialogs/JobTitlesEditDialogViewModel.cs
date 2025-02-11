using ReactiveUI;
using System.Threading.Tasks;
using System.Windows.Input;
using UIClient.Services;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs;

public class JobTitlesEditDialogViewModel : ViewModel
{
    private string _name;

    public JobTitlesEditDialogViewModel(ApiService apiService)
        : base(apiService)
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    protected override Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }

    private void Save()
    {
        CloseDialog(true);
    }

    private void Cancel()
    {
        CloseDialog(false);
    }

    private void CloseDialog(bool result)
    {
        if (View is IClosable closable)
            closable.Close(result);
    }
}
