using System.Threading.Tasks;
using Avalonia.Controls;
using UIClient.Services;
using UIClient.Views;

namespace UIClient.ViewModels;

public abstract class ViewModel : ViewModelBase
{
    public ViewModel(MasterApiService apiService, MainWindowViewModel mainWindowViewModel = null)
    {
        ApiService = apiService;
        MainWindowViewModel = mainWindowViewModel;
    }

    public async Task InitializeAsync()
    {
        await LoadingDialog.ShowLoadingDialog(LoadDataAsync, App.Owner);
    }

    protected MasterApiService ApiService { get; }
    internal IView View { get; init; }
    internal MainWindowViewModel MainWindowViewModel { get; }

    protected abstract Task LoadDataAsync();
}
