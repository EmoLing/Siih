using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Core.Models.Equipment;
using DynamicData;
using ReactiveUI;
using Shared.DTOs.Equipment;
using UIClient.Services;
using UIClient.ViewModels.Dialogs.Equipment;
using UIClient.Views.Dialogs.Equipment;

namespace UIClient.ViewModels.Equipment;
public class HardwaresViewModel : ViewModel
{
    private ObservableCollection<HardwareObject> _hardwares = [];
    private HardwareObject _selectedHardware;

    public HardwaresViewModel(MasterApiService apiService, MainWindowViewModel mainWindowViewModel)
        : base(apiService, mainWindowViewModel)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddHardware);
        DeleteCommand = ReactiveCommand.CreateFromTask(DeleteHardware);
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
        var hardwares = await ApiService.HardwaresApiService.GetHardwaresAsync();
        Hardwares = new ObservableCollection<HardwareObject>(hardwares);
    }

    private async Task AddHardware()
    {
        var dialog = new HardwareEditDialog();
        dialog.DataContext = new HardwareEditDialogViewModel(ApiService, null) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as HardwareEditDialogViewModel);

        var hardware = new HardwareObject()
        {
            Id = SelectedHardware.Id,
            Guid = SelectedHardware.Guid,
            Name = dialogData.Name,
            SerialNumber = dialogData.SerialNumber,
            Article = dialogData.Article,
            DateCreate = dialogData.DateCreate,
            Softwares = [.. dialogData.Softwares],
            ComplexHardware = SelectedHardware.ComplexHardware
        };

        try
        {
            var createdhardware = await ApiService.HardwaresApiService.AddHardwareAsync(hardware);

            if (createdhardware is not null)
            {
                Hardwares.Add(createdhardware);
                SelectedHardware = createdhardware;
            }
        }
        catch
        {
        }
    }

    private async Task DeleteHardware()
    {
        try
        {
            bool result = await ApiService.HardwaresApiService.DeleteHardwareAsync(SelectedHardware.Id);

            if (result)
            {
                Hardwares.Remove(SelectedHardware);
                SelectedHardware = Hardwares.FirstOrDefault();
            }
        }
        catch
        {
        }
    }

    private async Task EditHardware()
    {
        var dialog = new HardwareEditDialog();
        dialog.DataContext = new HardwareEditDialogViewModel(ApiService, SelectedHardware) { View = dialog };

        bool result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as HardwareEditDialogViewModel);

        var hardware = new HardwareObject()
        {
            Id = SelectedHardware.Id,
            Guid = SelectedHardware.Guid,
            Name = dialogData.Name,
            SerialNumber = dialogData.SerialNumber,
            Article = dialogData.Article,
            DateCreate = dialogData.DateCreate,
            Softwares = [.. dialogData.Softwares],
            ComplexHardware = SelectedHardware.ComplexHardware
        };

        try
        {
            await ApiService.HardwaresApiService.UpdateHardwareAsync(hardware);
            var updatedHardware = await ApiService.HardwaresApiService.GetHardwareAsync(hardware.Id);

            Hardwares.Replace(SelectedHardware, updatedHardware);
            var changedCollection = Hardwares.ToList();
            Hardwares = new ObservableCollection<HardwareObject>(changedCollection);
            SelectedHardware = updatedHardware;
        }
        catch
        {
        }
    }
}
