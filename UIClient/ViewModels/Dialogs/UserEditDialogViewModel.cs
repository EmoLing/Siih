using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Threading;
using ReactiveUI;
using Shared.DTOs.Departments;
using Shared.DTOs.Users;
using UIClient.Services;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs;

public class UserEditDialogViewModel : ViewModel
{
	private string _firstName;
    private string _lastName;
    private string _surname;
    private JobTitleObject _selectedJobTitle;
    private CabinetObject _selectedCabinet;

    private ObservableCollection<JobTitleObject> _jobTitles = [];
    private ObservableCollection<CabinetObject> _cabinets = [];

    public UserEditDialogViewModel(MasterApiService apiService)
        : base(apiService)
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }

    public string FirstName
    {
		get => _firstName;
        set => this.RaiseAndSetIfChanged(ref _firstName, value);
	}

    public string LastName
    {
        get => _lastName;
        set => this.RaiseAndSetIfChanged(ref _lastName, value);
    }

    public string Surname
    {
        get => _surname;
        set => this.RaiseAndSetIfChanged(ref _surname, value);
    }

    public JobTitleObject SelectedJobTitle
    {
        get => _selectedJobTitle;
        set => this.RaiseAndSetIfChanged(ref _selectedJobTitle, value);
    }

    public CabinetObject SelectedCabinet
    {
        get => _selectedCabinet;
        set => this.RaiseAndSetIfChanged(ref _selectedCabinet, value);
    }

    public ObservableCollection<JobTitleObject> JobTitles
    {
        get => _jobTitles;
        set => this.RaiseAndSetIfChanged(ref _jobTitles, value);
    }

    public ObservableCollection<CabinetObject> Cabinets
    {
        get => _cabinets;
        set => this.RaiseAndSetIfChanged(ref _cabinets, value);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var cabinets = await ApiService.CabinetsApiService.GetCabinetsAsync();
        var jobTitles = await ApiService.JobTitlesApiService.GetJobTitlesAsync();
        JobTitles = new ObservableCollection<JobTitleObject>(jobTitles);
        Cabinets = new ObservableCollection<CabinetObject>(cabinets);

        SelectedJobTitle = _selectedJobTitle ?? JobTitles.FirstOrDefault();
        SelectedCabinet = _selectedCabinet ?? Cabinets.FirstOrDefault();
        //await Dispatcher.UIThread.InvokeAsync(() =>
        //{

        //});
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
