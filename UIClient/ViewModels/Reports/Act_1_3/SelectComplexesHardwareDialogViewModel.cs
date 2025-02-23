using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Shared.DTOs.Equipment;
using UIClient.Services;
using UIClient.Views;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Reports.Act_1_3;

public class SelectComplexesHardwareDialogViewModel : ViewModel
{
    private ObservableCollection<SelectedComplexHardwareViewModel> _complexesHardware = [];
    private List<ComplexHardwareObject> _earlySelectedComplexesHardware;

    public SelectComplexesHardwareDialogViewModel(MasterApiService apiService, List<ComplexHardwareObject> selectedComplexesHardware)
        : base(apiService)
    {
        AddComplexesHardwareCommand = ReactiveCommand.Create(AddComplexesHardware);
        CancelCommand = ReactiveCommand.Create(Cancel);
        _earlySelectedComplexesHardware = selectedComplexesHardware ?? [];
    }

    internal IView View { get; init; }
    public ICommand CancelCommand { get; }
    public ICommand AddComplexesHardwareCommand { get; }

    public ObservableCollection<SelectedComplexHardwareViewModel> ComplexesHardware
    {
        get => _complexesHardware;
        set => this.RaiseAndSetIfChanged(ref _complexesHardware, value);
    }

    public List<ComplexHardwareObject> SelectedComplexesHardware { get; private set; } = [];

    protected override async Task LoadDataAsync()
    {
        var complexes = await ApiService.ComplexesHardwareApiService.GetComplexesHardwareAsync();
        var availableHardwares = complexes.Where(c => !_earlySelectedComplexesHardware.Contains(c));

        ComplexesHardware = new ObservableCollection<SelectedComplexHardwareViewModel>(availableHardwares
            .Select(ch => new SelectedComplexHardwareViewModel() { ComplexHardware = ch }));
    }

    private void AddComplexesHardware()
    {
        SelectedComplexesHardware = ComplexesHardware.Where(h => h.IsSelected).Select(ch => ch.ComplexHardware).ToList();
        CloseDialog(true);
    }

    private void Cancel() => CloseDialog(false);

    private void CloseDialog(bool result) => (View as IClosable)?.Close(result);
}