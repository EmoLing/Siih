using DB.Models.Equipment;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using UIClient.Services;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Reports.Act_1_3;

public class SelectComplexesHardwareDialogViewModel : ViewModel
{
    private ObservableCollection<SelectedComplexHardwareViewModel> _complexesHardware = [];
    private List<ComplexHardware> _earlySelectedComplexesHardware = [];

    public SelectComplexesHardwareDialogViewModel(ApiService apiService, List<ComplexHardware> selectedComplexesHardware)
        : base(apiService)
    {
        AddComplexesHardwareCommand = ReactiveCommand.Create(AddComplexesHardware);
        CancelCommand = ReactiveCommand.Create(Cancel);
        _earlySelectedComplexesHardware = selectedComplexesHardware;
    }

    public ICommand CancelCommand { get; }
    public ICommand AddComplexesHardwareCommand { get; }

    public ObservableCollection<SelectedComplexHardwareViewModel> ComplexesHardware
    {
        get => _complexesHardware;
        set => this.RaiseAndSetIfChanged(ref _complexesHardware, value);
    }

    public List<ComplexHardware> SelectedComplexesHardware { get; private set; } = [];

    protected override async Task LoadDataAsync()
    {
        var complexes = await ApiService.GetComplexesHardwareAsync();
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