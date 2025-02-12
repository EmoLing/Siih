using DB.Models.Departments;
using DB.Models.Equipment;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using UIClient.Services;
using UIClient.ViewModels.Dialogs.Departments;
using UIClient.ViewModels.Dialogs.Equipment;
using UIClient.Views.Dialogs.Departments;
using UIClient.Views.Dialogs.Equipment;

namespace UIClient.ViewModels.Departments;

public class CabinetsViewModel : ViewModel
{
    private ObservableCollection<Cabinet> _cabinets = [];
    private Cabinet _selectedCabinet;

    public ObservableCollection<Cabinet> Cabinets
    {
        get => _cabinets;
        set => this.RaiseAndSetIfChanged(ref _cabinets, value);
    }

    public Cabinet SelectedCabinet
    {
        get => _selectedCabinet;
        set => this.RaiseAndSetIfChanged(ref _selectedCabinet, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public CabinetsViewModel(ApiService apiService)
        : base(apiService)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddCabinet);
        DeleteCommand = ReactiveCommand.Create(DeleteCabinet);
        EditCommand = ReactiveCommand.CreateFromTask(EditCabinet);
    }

    protected override async Task LoadDataAsync()
    {
        var hardwares = await ApiService.GetCabinetsAsync();
        Cabinets = new ObservableCollection<Cabinet>(hardwares);
    }

    private async Task AddCabinet()
    {
        var dialog = new CabinetEditDialog();
        dialog.DataContext = new CabinetEditDialogViewModel(ApiService) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as CabinetEditDialogViewModel);

        var cabinet = new Cabinet()
        {
            Name = dialogData.Name,
            Department = dialogData.Department,
        };

        try
        {
            var createdCabinet = await ApiService.AddCabinetAsync(cabinet);

            if (createdCabinet is not null)
            {
                Cabinets.Add(createdCabinet);
                SelectedCabinet = createdCabinet;
            }
        }
        catch
        {
        }
    }

    private void DeleteCabinet()
    {
        // Логика удаления пользователя
    }

    private Task EditCabinet()
    {
        return Task.CompletedTask;
    }
}
