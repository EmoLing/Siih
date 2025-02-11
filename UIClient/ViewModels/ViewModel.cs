using System.Threading.Tasks;
using UIClient.Services;
using UIClient.Views;

namespace UIClient.ViewModels;

public abstract class ViewModel : ViewModelBase
{
    public ViewModel(ApiService apiService)
    {
        ApiService = apiService;
        LoadDataAsync().ConfigureAwait(false);
    }

    protected ApiService ApiService { get; }
    internal IView View { get; init; }

    protected abstract Task LoadDataAsync();
}
