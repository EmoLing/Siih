﻿using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Shared.DTOs.Departments;
using UIClient.Services;
using UIClient.Views.Dialogs;
using UIClient.Views.Dialogs.Departments;

namespace UIClient.ViewModels.Dialogs.Departments;

internal class CabinetEditDialogViewModel : ViewModel
{
    private string _name;
    private DepartmentObject _department;

    public CabinetEditDialogViewModel(MasterApiService apiService)
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

    public DepartmentObject Department
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