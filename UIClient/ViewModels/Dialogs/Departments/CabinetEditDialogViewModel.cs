using DB.Models.Departments;
using DB.Models.Equipment;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UIClient.Services;
using UIClient.ViewModels.Dialogs.Equipment;
using UIClient.Views.Dialogs;
using UIClient.Views.Dialogs.Departments;
using UIClient.Views.Dialogs.Equipment;

namespace UIClient.ViewModels.Dialogs.Departments;

internal class CabinetEditDialogViewModel : ViewModel
{
    private string _name;
    private Department _department;

    public CabinetEditDialogViewModel(ApiService apiService)
        : base(apiService)
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
        SelectCabinet = ReactiveCommand.Create(SelectDepartment);
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand SelectCabinet { get; }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public Department Department
    {
        get => _department;
        set => this.RaiseAndSetIfChanged(ref _department, value);
    }

    protected override Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }

    private void Save()
    {
        CloseDialog(true);
    }

    private void Cancel()
    {
        CloseDialog(false);
    }

    private async Task SelectDepartment()
    {
        var dialog = new SelectDepartmentDialog();
        dialog.DataContext = new SelectDepartmentDialogViewModel(ApiService, Department) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dataContext = (dialog.DataContext as SelectDepartmentDialogViewModel);
        Department = dataContext.SelectedDepartment;
    }

    private void CloseDialog(bool result)
    {
        if (View is IClosable closable)
            closable.Close(result);
    }
}