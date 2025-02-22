using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Shared.DTOs.Departments;
using UIClient.Services;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs.Departments;

public class SelectDepartmentDialogViewModel : ViewModel
{
    private readonly DepartmentObject _oldDepartment;
    private DepartmentObject _selectedDepartment;
    private ObservableCollection<DepartmentObject> _departments;

    public SelectDepartmentDialogViewModel(ApiService apiService, DepartmentObject oldDepartment)
        : base(apiService)
    {
        SelectCommand = ReactiveCommand.Create(SelectDepartment);
        CancelCommand = ReactiveCommand.Create(Cancel);
        _oldDepartment = oldDepartment;
    }

    public ICommand SelectCommand { get; }
    public ICommand CancelCommand { get; }

    public DepartmentObject SelectedDepartment
    {
        get => _selectedDepartment;
        set => this.RaiseAndSetIfChanged(ref _selectedDepartment, value);
    }

    public ObservableCollection<DepartmentObject> Departments
    {
        get => _departments;
        set => this.RaiseAndSetIfChanged(ref _departments, value);
    }

    protected override async Task LoadDataAsync()
    {
        var departments = await ApiService.GetDepartmentsAsync();
        Departments = new ObservableCollection<DepartmentObject>(departments);

        if (_oldDepartment is not null)
            SelectedDepartment = _oldDepartment;
    }

    private void SelectDepartment()
    {
        CloseDialog(true);
    }

    private void Cancel() => CloseDialog(false);

    private void CloseDialog(bool result) => (View as IClosable)?.Close(result);
}
