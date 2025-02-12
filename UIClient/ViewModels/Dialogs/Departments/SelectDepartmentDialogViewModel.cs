using DB.Models.Departments;
using DB.Models.Equipment;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIClient.Services;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs.Departments;

public class SelectDepartmentDialogViewModel : ViewModel
{
    private readonly Department _oldDepartment;
    private Department _selectedDepartment;
    private ObservableCollection<Department> _departments;

    public SelectDepartmentDialogViewModel(ApiService apiService, Department oldDepartment)
        : base(apiService)
    {
        SelectCommand = ReactiveCommand.Create(SelectDepartment);
        CancelCommand = ReactiveCommand.Create(Cancel);
        _oldDepartment = oldDepartment;
    }

    public ICommand SelectCommand { get; }
    public ICommand CancelCommand { get; }

    public Department SelectedDepartment
    {
        get => _selectedDepartment;
        set => this.RaiseAndSetIfChanged(ref _selectedDepartment, value);
    }

    public ObservableCollection<Department> Departments
    {
        get => _departments;
        set => this.RaiseAndSetIfChanged(ref _departments, value);
    }

    protected override async Task LoadDataAsync()
    {
        var departments = await ApiService.GetDepartmentsAsync();
        Departments = new ObservableCollection<Department>(departments);

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
