using ReactiveUI;
using System.Reactive;
using UIClient.Services;
using UIClient.ViewModels.Equipment;

namespace UIClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ApiService _apiService;
    private object _currentContent;

    public object CurrentContent
    {
        get => _currentContent;
        set => this.RaiseAndSetIfChanged(ref _currentContent, value);
    }

    public ReactiveCommand<Unit, Unit> ShowUsersCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowJobTitlesCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowSoftwaresCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowHardwaresCommand { get; }

    public MainWindowViewModel(ApiService apiService)
    {
        _apiService = apiService;

        ShowUsersCommand = ReactiveCommand.Create(ShowUsers);
        ShowJobTitlesCommand = ReactiveCommand.Create(ShowJobTitles);
        ShowSoftwaresCommand = ReactiveCommand.Create(ShowSoftwares);
        ShowHardwaresCommand = ReactiveCommand.Create(ShowHardwares);
    }

    private void ShowUsers()
    {
        CurrentContent = new UsersViewModel(_apiService);
    }

    private void ShowJobTitles()
    {
        CurrentContent = new JobTitlesViewModel(_apiService);
    }

    private void ShowSoftwares()
    {
        CurrentContent = new SoftwaresViewModel(_apiService);
    }

    private void ShowHardwares()
    {
        CurrentContent = new HardwaresViewModel(_apiService);
    }
}
