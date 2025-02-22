using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Shared.DTOs.Equipment;
using UIClient.Services;

namespace UIClient.ViewModels.Equipment;
public class HardwaresViewModel : ViewModel
{
    private ObservableCollection<HardwareObject> _hardwares = [];
    private HardwareObject _selectedHardware;

    public HardwaresViewModel(ApiService apiService, MainWindowViewModel mainWindowViewModel)
        : base(apiService, mainWindowViewModel)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddHardware);
        DeleteCommand = ReactiveCommand.Create(DeleteHardware);
        EditCommand = ReactiveCommand.CreateFromTask(EditHardware);

        MainWindowViewModel.SetTitle("Оборудование");
    }

    public ObservableCollection<HardwareObject> Hardwares
    {
        get => _hardwares;
        set => this.RaiseAndSetIfChanged(ref _hardwares, value);
    }

    public HardwareObject SelectedHardware
    {
        get => _selectedHardware;
        set => this.RaiseAndSetIfChanged(ref _selectedHardware, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var hardwares = await ApiService.GetHardwaresAsync();
        Hardwares = new ObservableCollection<HardwareObject>(hardwares);
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
