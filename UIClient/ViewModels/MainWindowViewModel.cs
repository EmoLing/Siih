using Avalonia.Controls;
using Avalonia.Threading;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;
using UIClient.Services;
using UIClient.ViewModels.Departments;
using UIClient.ViewModels.Equipment;
using UIClient.ViewModels.Reports.Act_1_3;

namespace UIClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly string defaultTitle = "SIIH";
    private readonly MasterApiService _apiService;
    private ViewModel _currentContent;
    private string _title;

    public MainWindowViewModel(MasterApiService apiService)
    {
        _apiService = apiService;

        ShowUsersCommand = ReactiveCommand.CreateFromTask(ShowUsers);
        ShowJobTitlesCommand = ReactiveCommand.CreateFromTask(ShowJobTitles);
        ShowSoftwaresCommand = ReactiveCommand.CreateFromTask(ShowSoftwares);
        ShowHardwaresCommand = ReactiveCommand.CreateFromTask(ShowHardwares);
        ShowComplexesHardwareCommand = ReactiveCommand.CreateFromTask(ShowComplexesHardware);
        ShowCabinetsCommand = ReactiveCommand.CreateFromTask(ShowCabinets);
        ShowDepartmentsCommand = ReactiveCommand.CreateFromTask(ShowDepartments);
        ShowReportDialogCommand = ReactiveCommand.CreateFromTask(ShowReportDialog);
    }

    public ViewModel CurrentContent
    {
        get => _currentContent;
        set => this.RaiseAndSetIfChanged(ref _currentContent, value);
    }

    public string Title
    {
        get => _title;
        set => this.RaiseAndSetIfChanged(ref _title, value);
    }

    public Window Window { get; set; }

    public ReactiveCommand<Unit, Unit> ShowUsersCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowJobTitlesCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowSoftwaresCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowHardwaresCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowComplexesHardwareCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowCabinetsCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowDepartmentsCommand { get; }
    public ReactiveCommand<Unit, Unit> ShowReportDialogCommand { get; }

    public void SetTitle(string title) => Title = $"{defaultTitle} - {title}";

    private async Task ShowUsers()
    {
        CurrentContent = new UsersViewModel(_apiService, this);
        await CurrentContent.InitializeAsync();
    }

    private async Task ShowJobTitles()
    {
        CurrentContent = new JobTitlesViewModel(_apiService, this) {};
        await CurrentContent.InitializeAsync();
    }

    private async Task ShowSoftwares()
    {
        CurrentContent = new SoftwaresViewModel(_apiService, this) {};
        await CurrentContent.InitializeAsync();
    }

    private async Task ShowHardwares()
    {
        CurrentContent = new HardwaresViewModel(_apiService, this) {};
        await CurrentContent.InitializeAsync();
    }

    private async Task ShowComplexesHardware()
    {
        CurrentContent = new ComplexesHardwareViewModel(_apiService, this) {};
        await CurrentContent.InitializeAsync();
    }

    private async Task ShowCabinets()
    {
        CurrentContent = new CabinetsViewModel(_apiService, this) {};
        await CurrentContent.InitializeAsync();
    }

    private async Task ShowDepartments()
    {
        CurrentContent = new DepartmentsViewModel(_apiService, this) {};
        await CurrentContent.InitializeAsync();
    }

    private async Task ShowReportDialog()
    {
        CurrentContent = new Act_1_3InfoDialogViewModel(_apiService, this) {};
        await CurrentContent.InitializeAsync();
    }
}
