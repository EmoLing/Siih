using System.Threading.Tasks;
using UIClient.Services;
using UIClient.Views;

namespace UIClient.ViewModels;

public abstract class ViewModel : ViewModelBase
{
    public ViewModel(ApiService apiService, MainWindowViewModel mainWindowViewModel = null)
    {
        ApiService = apiService;
        LoadDataAsync().ConfigureAwait(false);

        MainWindowViewModel = mainWindowViewModel;
    }

    protected ApiService ApiService { get; }
    internal IView View { get; init; }
    internal MainWindowViewModel MainWindowViewModel { get; }

    protected abstract Task LoadDataAsync();
}
