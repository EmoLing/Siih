using ReactiveUI;
using System.Reactive;
using UIClient.Services;
using UIClient.ViewModels.Departments;
using UIClient.ViewModels.Equipment;
using UIClient.ViewModels.Reports.Act_1_3;

namespace UIClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly string defaultTitle = "SIIH";
    private readonly MasterApiService _apiService;
    private object _currentContent;
    private string _title;

    public MainWindowViewModel(MasterApiService apiService)
    {
        _apiService = apiService;

        ShowUsersCommand = ReactiveCommand.Create(ShowUsers);
        ShowJobTitlesCommand = ReactiveCommand.Create(ShowJobTitles);
        ShowSoftwaresCommand = ReactiveCommand.Create(ShowSoftwares);
        ShowHardwaresCommand = ReactiveCommand.Create(ShowHardwares);
        ShowComplexesHardwareCommand = ReactiveCommand.Create(ShowComplexesHardware);
        ShowCabinetsCommand = ReactiveCommand.Create(ShowCabinets);
        ShowDepartmentsCommand = ReactiveCommand.Create(ShowDepartments);
        ShowReportDialogCommand = ReactiveCommand.Create(ShowReportDialog);
    }

    public object CurrentContent
    {
        get => _currentContent;
        set => this.RaiseAndSetIfChanged(ref _currentContent, value);
    }

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public ReactiveCommand<Unit, Unit> ShowUsersCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowJobTitlesCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowSoftwaresCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowHardwaresCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowComplexesHardwareCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowCabinetsCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowDepartmentsCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowReportDialogCommand { get; }

    public void SetTitle(string title) => Title = $"{defaultTitle} - {title}";

    private void ShowUsers()
    {
        CurrentContent = new UsersViewModel(_apiService, this) {};
    }

    private void ShowJobTitles()
    {
        CurrentContent = new JobTitlesViewModel(_apiService, this) {};
    }

    private void ShowSoftwares()
    {
        CurrentContent = new SoftwaresViewModel(_apiService, this) {};
    }

    private void ShowHardwares()
    {
        CurrentContent = new HardwaresViewModel(_apiService, this) {};
    }

    private void ShowComplexesHardware()
    {
        CurrentContent = new ComplexesHardwareViewModel(_apiService, this) {};
    }

    private void ShowCabinets()
    {
        CurrentContent = new CabinetsViewModel(_apiService, this) {};
    }

    private void ShowDepartments()
    {
        CurrentContent = new DepartmentsViewModel(_apiService, this) {};
    }

    private void ShowReportDialog()
    {
        CurrentContent = new Act_1_3InfoDialogViewModel(_apiService, this) {};
    }
}
