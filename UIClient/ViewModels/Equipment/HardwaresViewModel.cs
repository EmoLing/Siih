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

namespace UIClient.ViewModels.Equipment;
public class HardwaresViewModel : ViewModel
{
    private ObservableCollection<Hardware> _hardwares = [];
    private Hardware _selectedHardware;

    public ObservableCollection<Hardware> Hardwares
    {
        get => _hardwares;
        set => this.RaiseAndSetIfChanged(ref _hardwares, value);
    }

    public Hardware SelectedHardware
    {
        get => _selectedHardware;
        set => this.RaiseAndSetIfChanged(ref _selectedHardware, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public HardwaresViewModel(ApiService apiService)
        : base(apiService)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddHardware);
        DeleteCommand = ReactiveCommand.Create(DeleteHardware);
        EditCommand = ReactiveCommand.CreateFromTask(EditHardware);
    }

    protected override async Task LoadDataAsync()
    {
        var hardwares = await ApiService.GetHardwaresAsync();
        Hardwares = new ObservableCollection<Hardware>(hardwares);
    }

    private Task AddHardware()
    {
        return Task.CompletedTask;
    }

    private void DeleteHardware()
    {
        // Логика удаления пользователя
    }

    private Task EditHardware()
    {
        return Task.CompletedTask;
    }
}
