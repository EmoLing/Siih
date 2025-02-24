using Core.Models.Equipment;
using DynamicData;
using ReactiveUI;
using ReportModel;
using ReportModel.Акт_со_1._3;
using Shared.DTOs.Equipment;
using System.Collections.Generic;
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
    private ReportComplexHardwareViewModel _selectedReportComplexHardware;
    private bool _isEnableSelectedReportComplexHardware;
    private ObservableCollection<ErrorPTS> _errorsPTS;

    public Act_1_3InfoDialogViewModel(MasterApiService apiService, MainWindowViewModel mainWindowViewModel)
        : base(apiService, mainWindowViewModel)
    {
        ComplexesHardware = [];
        _errorsPTS = [];

        CreateErrorsPTSCommand = ReactiveCommand.Create<ReportHardwareViewModel>(CreateErrorsPTS);
        RemoveErrorPTSCommand = ReactiveCommand.Create<ErrorPTS>(RemoveErrorPTS);

        GenerateReportCommand = ReactiveCommand.CreateFromTask(GenerateReport);
        SelectComplexesHardwaresCommand = ReactiveCommand.Create(SelectComplexesHardwares);
        RemoveComplexesHardwaresCommand = ReactiveCommand.Create(RemoveComplexesHardwares);

        MainWindowViewModel.SetTitle("Отчеты");
    }

    public ICommand GenerateReportCommand { get; }

    public ICommand SelectComplexesHardwaresCommand { get; }
    public ReactiveCommand<Unit, Unit> RemoveComplexesHardwaresCommand { get; }

    public ReactiveCommand<ReportHardwareViewModel, Unit> CreateErrorsPTSCommand { get; }
    public ReactiveCommand<ErrorPTS, Unit> RemoveErrorPTSCommand { get; }

    public string Title { get; set; }

    public ObservableCollection<ReportComplexHardwareViewModel> ComplexesHardware
    {
        get => _complexesHardware;
        set => this.RaiseAndSetIfChanged(ref _complexesHardware, value);
    }

    public ReportComplexHardwareViewModel SelectedReportComplexHardware
    {
        get => _selectedReportComplexHardware;
        set
        {
            this.RaiseAndSetIfChanged(ref _selectedReportComplexHardware, value);
            IsEnableSelectedReportComplexHardware = value is not null;
        }
    }

    public bool IsEnableSelectedReportComplexHardware
    {
        get => _isEnableSelectedReportComplexHardware;
        set => this.RaiseAndSetIfChanged(ref _isEnableSelectedReportComplexHardware, value);
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
        ErrorsPTS.Add(new ErrorPTS() { Hardware = subItem.Hardware });
        subItem.IsInErrorPTS = true;
    }

    private void RemoveErrorPTS(ErrorPTS subItem)
    {
        var reportComplex = ComplexesHardware.FirstOrDefault(ch => ch.Hardwares.Any(h => h.Hardware.Id == subItem.Hardware.Id));
        ErrorsPTS.Remove(subItem);
        var reportHardware = reportComplex.Hardwares.FirstOrDefault(h => h.Hardware.Id == subItem.Hardware.Id);
        reportHardware.IsInErrorPTS = false;
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

    private void RemoveComplexesHardwares()
    {
        var removedErrorPTS = ErrorsPTS.Where(e => SelectedReportComplexHardware.Hardwares.Any(h => h.Hardware.Id == e.Hardware.Id));
        ComplexesHardware.Remove(SelectedReportComplexHardware);

        foreach (var error in removedErrorPTS)
            ErrorsPTS.Remove(error);

        SelectedReportComplexHardware = ComplexesHardware.FirstOrDefault();
    }

    private async Task GenerateReport()
    {
        var complexes = ComplexesHardware.Select(ch => ch.ComplexHardware).ToList();
        var reloadedLists = new List<ComplexHardwareObject>(complexes.Count);

        foreach (var complex in complexes)
        {
            var reloadedComplex = await ApiService.ComplexesHardwareApiService.GetComplexHardwareAsync(complex.Id);
            reloadedLists.Add(reloadedComplex);
        }

        var reportInfo = new Act_1_3Info(reloadedLists, ErrorsPTS.ToList());
        reportInfo.Initialize();

        ReportGenerator.ReportGenerator.Start(reportInfo, Title);
    }
}
