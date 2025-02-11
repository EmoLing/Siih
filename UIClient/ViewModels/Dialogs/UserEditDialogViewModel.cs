using DB.Models.Departments;
using DB.Models.Users;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UIClient.Services;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs;

public class UserEditDialogViewModel : ViewModel
{
	private string _firstName;
    private string _lastName;
    private string _surname;
    private JobTitle _selectedJobTitle;
    private Cabinet _selectedCabinet;

    private ObservableCollection<JobTitle> _jobTitles = [];
    private ObservableCollection<Cabinet> _cabinets = [];

    public UserEditDialogViewModel(ApiService apiService)
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

    public JobTitle SelectedJobTitle
    {
        get => _selectedJobTitle;
        set => this.RaiseAndSetIfChanged(ref _selectedJobTitle, value);
    }

    public Cabinet SelectedCabinet
    {
        get => _selectedCabinet;
        set => this.RaiseAndSetIfChanged(ref _selectedCabinet, value);
    }

    public ObservableCollection<JobTitle> JobTitles
    {
        get => _jobTitles;
        set => this.RaiseAndSetIfChanged(ref _jobTitles, value);
    }

    public ObservableCollection<Cabinet> Cabinets
    {
        get => _cabinets;
        set => this.RaiseAndSetIfChanged(ref _cabinets, value);
    }


    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var cabinets = await ApiService.GetCabinetsAsync();
        var jobTitles = await ApiService.GetJobTitlesAsync();

        JobTitles = new ObservableCollection<JobTitle>(jobTitles);
        Cabinets = new ObservableCollection<Cabinet>(cabinets);

        SelectedJobTitle = _selectedJobTitle ?? JobTitles.FirstOrDefault();
        SelectedCabinet = _selectedCabinet ?? Cabinets.FirstOrDefault();
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
