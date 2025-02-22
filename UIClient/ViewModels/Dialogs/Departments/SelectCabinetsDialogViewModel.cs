using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Shared.DTOs.Departments;
using UIClient.Services;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs.Departments;

public class SelectCabinetsDialogViewModel : ViewModel
{
    private ObservableCollection<SelectedCabinetViewModel> _cabinets = [];
    private readonly List<CabinetObject> _earlySelectedCabinets = [];

    public SelectCabinetsDialogViewModel(MasterApiService apiService, List<CabinetObject> selectedCabinets)
        : base(apiService)
    {
        AddCabinetsCommand = ReactiveCommand.Create(AddCabinets);
        CancelCommand = ReactiveCommand.Create(Cancel);
        _earlySelectedCabinets = selectedCabinets;
    }

    public ICommand CancelCommand { get; }
    public ICommand AddCabinetsCommand { get; }

    public ObservableCollection<SelectedCabinetViewModel> Cabinets
    {
        get => _cabinets;
        set => this.RaiseAndSetIfChanged(ref _cabinets, value);
    }

    public List<CabinetObject> SelectedCabinets { get; private set; } = [];

    protected override async Task LoadDataAsync()
    {
        var cabinets = await ApiService.CabinetsApiService.GetCabinetsAsync();
        var availableCabinets = cabinets.Where(c => c.Department is null && !_earlySelectedCabinets.Contains(c));

        Cabinets = new ObservableCollection<SelectedCabinetViewModel>(availableCabinets
            .Select(c => new SelectedCabinetViewModel() { Cabinet = c }));
    }

    private void AddCabinets()
    {
        SelectedCabinets = Cabinets.Where(c => c.IsSelected).Select(c => c.Cabinet).ToList();
        CloseDialog(true);
    }

    private void Cancel() => CloseDialog(false);

    private void CloseDialog(bool result) => (View as IClosable)?.Close(result);
}
