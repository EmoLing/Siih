using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Core.Models.Equipment;
using DynamicData;
using ReactiveUI;
using Shared.DTOs.Departments;
using Shared.DTOs.Equipment;
using UIClient.Services;
using UIClient.ViewModels.Dialogs.Departments;
using UIClient.Views.Dialogs.Departments;

namespace UIClient.ViewModels.Departments;

public class CabinetsViewModel : ViewModel
{
    private ObservableCollection<CabinetObject> _cabinets = [];
    private CabinetObject _selectedCabinet;

    public CabinetsViewModel(MasterApiService apiService, MainWindowViewModel mainWindowViewModel)
        : base(apiService, mainWindowViewModel)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddCabinet);
        DeleteCommand = ReactiveCommand.CreateFromTask(DeleteCabinet);
        EditCommand = ReactiveCommand.CreateFromTask(EditCabinet);

        MainWindowViewModel.SetTitle("Кабинеты");
    }

    public ObservableCollection<CabinetObject> Cabinets
    {
        get => _cabinets;
        set => this.RaiseAndSetIfChanged(ref _cabinets, value);
    }

    public CabinetObject SelectedCabinet
    {
        get => _selectedCabinet;
        set => this.RaiseAndSetIfChanged(ref _selectedCabinet, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var hardwares = await ApiService.CabinetsApiService.GetCabinetsAsync();
        Cabinets = new ObservableCollection<CabinetObject>(hardwares);
    }

    private async Task AddCabinet()
    {
        var dialog = new CabinetEditDialog();
        dialog.DataContext = new CabinetEditDialogViewModel(ApiService, null) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as CabinetEditDialogViewModel);

        var cabinet = new CabinetObject()
        {
            Name = dialogData.Name,
            Department = dialogData.Department,
        };

        try
        {
            var createdCabinet = await ApiService.CabinetsApiService.AddCabinetAsync(cabinet);

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

    private async Task DeleteCabinet()
    {
        try
        {
            bool result = await ApiService.CabinetsApiService.DeleteCabinetAsync(SelectedCabinet.Id);

            if (result)
            {
                Cabinets.Remove(SelectedCabinet);
                SelectedCabinet = Cabinets.FirstOrDefault();
            }
        }
        catch
        {
        }
    }

    private async Task EditCabinet()
    {
        var dialog = new CabinetEditDialog();
        dialog.DataContext = new CabinetEditDialogViewModel(ApiService, SelectedCabinet) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as CabinetEditDialogViewModel);

        var cabinet = new CabinetObject()
        {
            Id = SelectedCabinet.Id,
            Guid = SelectedCabinet.Guid,
            Name = dialogData.Name,
            Department = dialogData.Department,
        };

        try
        {
            await ApiService.CabinetsApiService.UpdateCabinetAsync(cabinet);

            Cabinets.Replace(SelectedCabinet, cabinet);
            var changedCollection = Cabinets.ToList();
            Cabinets = new ObservableCollection<CabinetObject>(changedCollection);
            SelectedCabinet = cabinet;
        }
        catch
        {
        }
    }
}
