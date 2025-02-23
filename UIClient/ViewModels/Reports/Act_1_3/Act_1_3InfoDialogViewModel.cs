using Core.Models.Equipment;
using DynamicData;
using ReactiveUI;
using ReportModel;
using ReportModel.Акт_со_1._3;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using System.Windows.Input;
using UIClient.Services;
using UIClient.Views.Reports.Act_1_3;

namespace UIClient.ViewModels.Reports.Act_1_3;

public class Act_1_3InfoDialogViewModel : ViewModel
{
    private ObservableCollection<ReportComplexHardwareViewModel> _complexesHardware;

    private ObservableCollection<ErrorPTS> _errorsPTS;

    public Act_1_3InfoDialogViewModel(MasterApiService apiService, MainWindowViewModel mainWindowViewModel)
        : base(apiService, mainWindowViewModel)
    {
        ComplexesHardware = [];
        _errorsPTS = [];

        CreateErrorsPTSCommand = ReactiveCommand.Create<ReportHardwareViewModel>(CreateErrorsPTS);
        RemoveErrorPTSCommand = ReactiveCommand.Create<ErrorPTS>(RemoveErrorPTS);

        GenerateReportCommand = ReactiveCommand.Create(GenerateReport);
        SelectComplexesHardwaresCommand = ReactiveCommand.Create(SelectComplexesHardwares);
        RemoveComplexesHardwaresCommand = ReactiveCommand.Create<ReportHardwareViewModel>(RemoveComplexesHardwares);

        MainWindowViewModel.SetTitle("Отчеты");
    }

    public ICommand GenerateReportCommand { get; }

    public ICommand SelectComplexesHardwaresCommand { get; }
    public ReactiveCommand<ReportHardwareViewModel, Unit> RemoveComplexesHardwaresCommand { get; }

    public ReactiveCommand<ReportHardwareViewModel, Unit> CreateErrorsPTSCommand { get; }
    public ReactiveCommand<ErrorPTS, Unit> RemoveErrorPTSCommand { get; }

    public string Title { get; set; }

    public ObservableCollection<ReportComplexHardwareViewModel> ComplexesHardware
    {
        get => _complexesHardware;
        set => this.RaiseAndSetIfChanged(ref _complexesHardware, value);
    }

    public ObservableCollection<ErrorPTS> ErrorsPTS
    {
        get => _errorsPTS;
        set => this.RaiseAndSetIfChanged(ref _errorsPTS, value);
    }

    protected override Task LoadDataAsync()
    {
        ComplexesHardware = [];
        ErrorsPTS = [];
        return Task.CompletedTask;
    }

    private void CreateErrorsPTS(ReportHardwareViewModel subItem)
    {
        // Логика создания объекта
    }

    private void RemoveErrorPTS(ErrorPTS subItem)
    {
        // Логика создания объекта
    }

    private async Task SelectComplexesHardwares()
    {
        var dialog = new SelectComplexesHardwareDialog();
        var currentComplexesHardware = ComplexesHardware?.Select(ch => ch.ComplexHardware).ToList();

        dialog.DataContext = new SelectComplexesHardwareDialogViewModel(ApiService, currentComplexesHardware) { View = dialog };

        await (dialog.DataContext as ViewModel)?.InitializeAsync();

        bool result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dataContext = (dialog.DataContext as SelectComplexesHardwareDialogViewModel);

        ComplexesHardware.AddRange(dataContext.SelectedComplexesHardware.Select(ch => new ReportComplexHardwareViewModel(ch)));
    }

    private void RemoveComplexesHardwares(ReportHardwareViewModel subItem)
    {

    }

    private void GenerateReport()
    {
        var complexes = ComplexesHardware.Select(ch => ch.ComplexHardware).ToList();
        var reportInfo = new Act_1_3Info(complexes, ErrorsPTS.ToList());
        reportInfo.Initialize();

        ReportGenerator.ReportGenerator.Start(reportInfo, Title);
    }
}
