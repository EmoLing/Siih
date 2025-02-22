using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Shared.DTOs.Equipment;
using UIClient.Services;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs.Equipment;

public class SelectHardwaresDialogViewModel : ViewModel
{
    private ObservableCollection<SelectedHardwareViewModel> _hardwares = [];
    private List<HardwareObject> _earlySelectedHardwares = [];

    public SelectHardwaresDialogViewModel(MasterApiService apiService, List<HardwareObject> selectedHardwares)
        : base(apiService)
    {
        AddHardwaresCommand = ReactiveCommand.Create(AddHardwares);
        CancelCommand = ReactiveCommand.Create(Cancel);
        _earlySelectedHardwares = selectedHardwares;
    }

    public ICommand CancelCommand { get; }
    public ICommand AddHardwaresCommand { get; }

    public ObservableCollection<SelectedHardwareViewModel> Hardwares
    {
        get => _hardwares;
        set => this.RaiseAndSetIfChanged(ref _hardwares, value);
    }

    public List<HardwareObject> SelectedHardwares { get; private set; } = [];

    protected override async Task LoadDataAsync()
    {
        var hardwares = await ApiService.HardwaresApiService.GetHardwaresAsync();
        var availableHardwares = hardwares.Where(h => h.ComplexHardware is null && !_earlySelectedHardwares.Contains(h));

        Hardwares = new ObservableCollection<SelectedHardwareViewModel>(availableHardwares
            .Select(h => new SelectedHardwareViewModel() { Hardware = h }));
    }

    private void AddHardwares()
    {
        SelectedHardwares = Hardwares.Where(h => h.IsSelected).Select(h => h.Hardware).ToList();
        CloseDialog(true);
    }

    private void Cancel() => CloseDialog(false);

    private void CloseDialog(bool result) => (View as IClosable)?.Close(result);
}
