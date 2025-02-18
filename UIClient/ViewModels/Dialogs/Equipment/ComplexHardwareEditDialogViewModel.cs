﻿using DB.Models.Equipment;
using DB.Models.Users;
using DynamicData;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using UIClient.Services;
using UIClient.Views.Dialogs;
using UIClient.Views.Dialogs.Equipment;

namespace UIClient.ViewModels.Dialogs.Equipment;

public class ComplexHardwareEditDialogViewModel : ViewModel
{
    private string _name;
    private User _user;
    private string _inventoryNumber;
    private ComplexHardwareType _type;
    private ObservableCollection<Hardware> _hardwares = [];

    public ComplexHardwareEditDialogViewModel(ApiService apiService, ComplexHardware complexHardware)
        : base(apiService)
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
        AddHardwareCommand = ReactiveCommand.Create(AddHardware);
        CreateHardwareCommand = ReactiveCommand.Create(CreateHardware);

        if (complexHardware is not null)
        {
            Name = complexHardware.Name;
            Type = complexHardware.Type;
            InventoryNumber = complexHardware.InventoryNumber;
            User = complexHardware.User;
            Hardwares = new ObservableCollection<Hardware>(complexHardware.Hardwares);
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand AddHardwareCommand { get; }
    public ICommand CreateHardwareCommand { get; }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public User User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }

    public string InventoryNumber
    {
        get => _inventoryNumber;
        set => this.RaiseAndSetIfChanged(ref _inventoryNumber, value);
    }

    public ComplexHardwareType Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }

    public ObservableCollection<Hardware> Hardwares
    {
        get => _hardwares;
        set => this.RaiseAndSetIfChanged(ref _hardwares, value);
    }

    protected override Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }

    private void Save() => CloseDialog(true);

    private void Cancel() => CloseDialog(false);

    private async Task AddHardware()
    {
        var dialog = new SelectHardwaresDialog();
        dialog.DataContext = new SelectHardwaresDialogViewModel(ApiService, [.. Hardwares]) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dataContext = (dialog.DataContext as SelectHardwaresDialogViewModel);
        Hardwares.AddRange(dataContext.SelectedHardwares);
    }

    private void CreateHardware()
    {
    }

    private void CloseDialog(bool result)
    {
        if (View is IClosable closable)
            closable.Close(result);
    }
}
