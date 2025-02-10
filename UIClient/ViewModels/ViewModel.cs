using System.Threading.Tasks;
using UIClient.Services;

namespace UIClient.ViewModels;

public abstract class ViewModel : ViewModelBase
{
    public ViewModel(ApiService apiService)
    {
        ApiService = apiService;
        LoadDataAsync().ConfigureAwait(false);
    }

    protected ApiService ApiService { get; }

    protected abstract Task LoadDataAsync();
}
