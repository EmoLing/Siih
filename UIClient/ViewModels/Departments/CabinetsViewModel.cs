using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Shared.DTOs.Departments;
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
        DeleteCommand = ReactiveCommand.Create(DeleteCabinet);
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
        dialog.DataContext = new CabinetEditDialogViewModel(ApiService) { View = dialog };

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

    private void DeleteCabinet()
    {
        // Логика удаления пользователя
    }

    private Task EditCabinet()
    {
        return Task.CompletedTask;
    }
}
