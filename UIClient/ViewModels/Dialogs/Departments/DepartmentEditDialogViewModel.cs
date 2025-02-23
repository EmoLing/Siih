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

public class DepartmentEditDialogViewModel : ViewModel
{
    private string _name;
    private ObservableCollection<CabinetObject> _cabinets = [];
    private CabinetObject _selectedCabinet;
    private bool _isEnableRemove;

    public DepartmentEditDialogViewModel(MasterApiService apiService, DepartmentObject department)
        : base(apiService)
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
        AddCabinetsCommand = ReactiveCommand.Create(AddCabinets);
        RemoveCabinetCommand = ReactiveCommand.Create(RemoveCabinet);

        if (department is not null)
        {
            Name = department.Name;
            Cabinets = new ObservableCollection<CabinetObject>(department.Cabinets);
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand AddCabinetsCommand { get; }
    public ICommand RemoveCabinetCommand { get; }

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

    public CabinetObject SelectedCabinet
    {
        get => _selectedCabinet;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedCabinet, value);
            IsEnableRemove = value is not null;
        }
    }

    public bool IsEnableRemove
    {
        get => _isEnableRemove;
        set => this.RaiseAndSetIfChanged(ref _isEnableRemove, value);
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

    private async Task AddCabinets()
    {
        var dialog = new SelectCabinetsDialog();
        dialog.DataContext = new SelectCabinetsDialogViewModel(ApiService, [.. Cabinets]) { View = dialog };
        await (dialog.DataContext as ViewModel)?.InitializeAsync();

        bool result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dataContext = (dialog.DataContext as SelectCabinetsDialogViewModel);
        Cabinets.AddRange(dataContext.SelectedCabinets);
    }

    public void RemoveCabinet()
    {
        if (SelectedCabinet is null)
            return;

        Cabinets.Remove(SelectedCabinet);
    }

    private void CloseDialog(bool result)
    {
        if (View is IClosable closable)
            closable.Close(result);
    }
}