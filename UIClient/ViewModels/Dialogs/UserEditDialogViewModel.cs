using DB.Models.Departments;
using DB.Models.Users;
using ReactiveUI;
using System.Collections.Generic;
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

    public List<JobTitle> JobTitles { get; private set; }
    public List<Cabinet> Cabinets { get; private set; }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var cabinets = await ApiService.GetCabinetsAsync();
        var jobTitles = await ApiService.GetJobTitlesAsync();

        JobTitles = jobTitles;
        Cabinets = cabinets;
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
        if (this is IClosable closable)
            closable.Close(result);
    }
}
