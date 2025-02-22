using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using Shared.DTOs.Departments;
using UIClient.Services;
using UIClient.Views.Dialogs;
using UIClient.Views.Dialogs.Departments;

namespace UIClient.ViewModels.Dialogs.Departments;

internal class DepartmentEditDialogViewModel : ViewModel
{
    private string _name;
    private ObservableCollection<CabinetObject> _cabinets = [];
    private readonly DepartmentObject _department;

    public DepartmentEditDialogViewModel(ApiService apiService, DepartmentObject department)
        : base(apiService)
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
        AddCabinetsCommand = ReactiveCommand.Create(AddCabinets);

        _department = department;
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public ICommand AddCabinetsCommand { get; }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public ObservableCollection<CabinetObject> Cabinets
    {
        get => _cabinets;
        set => this.RaiseAndSetIfChanged(ref _cabinets, value);
    }

    protected override async Task LoadDataAsync()
    {
        if (_department is null)
            return;

        var allCabinets = await ApiService.GetCabinetsAsync();
        Cabinets = new ObservableCollection<CabinetObject>(allCabinets.Where(c => c.Department is null || c.Department == _department));
    }

    private void Save()
    {
        CloseDialog(true);
    }

    private void Cancel()
    {
        CloseDialog(false);
    }

    private async Task AddCabinets()
    {
        var dialog = new SelectCabinetsDialog();
        dialog.DataContext = new SelectCabinetsDialogViewModel(ApiService, [.. Cabinets]) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dataContext = (dialog.DataContext as SelectCabinetsDialogViewModel);
        Cabinets.AddRange(dataContext.SelectedCabinets);
    }

    private void CloseDialog(bool result)
    {
        if (View is IClosable closable)
            closable.Close(result);
    }
}