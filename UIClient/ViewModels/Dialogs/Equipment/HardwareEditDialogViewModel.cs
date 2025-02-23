using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using ReactiveUI;
using Shared.DTOs.Equipment;
using UIClient.Services;
using UIClient.ViewModels.Dialogs.SelectDialog;
using UIClient.Views.Dialogs;
using UIClient.Views.Dialogs.SelectDialog;

namespace UIClient.ViewModels.Dialogs.Equipment;
public class HardwareEditDialogViewModel : ViewModel
{
    private readonly int _id;
    private string _name;
    private string _serialNumber;
    private string _article;
    private DateOnly _dateCreate;
    private ObservableCollection<SoftwareObject> _softwares = [];
    private SoftwareObject _selectedSoftware;

    public HardwareEditDialogViewModel(MasterApiService apiService, HardwareObject hardware)
        : base(apiService)
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
        AddSoftwareCommand = ReactiveCommand.Create(AddSoftware);
        DeleteSoftwareCommand = ReactiveCommand.Create(DeleteSoftware);

        if (hardware is not null)
        {
            _id = hardware.Id;
            Name = hardware.Name;
            SerialNumber = hardware.SerialNumber;
            Article = hardware.Article;
            DateCreate = hardware.DateCreate;
            Softwares = new ObservableCollection<SoftwareObject>(hardware.Softwares);
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand AddSoftwareCommand { get; }
    public ICommand DeleteSoftwareCommand { get; }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public string SerialNumber
    {
        get => _serialNumber;
        set => this.RaiseAndSetIfChanged(ref _serialNumber, value);
    }

    public string Article
    {
        get => _article;
        set => this.RaiseAndSetIfChanged(ref _article, value);
    }

    public DateOnly DateCreate
    {
        get => _dateCreate;
        set => this.RaiseAndSetIfChanged(ref _dateCreate, value);
    }

    public ObservableCollection<SoftwareObject> Softwares
    {
        get => _softwares;
        set => this.RaiseAndSetIfChanged(ref _softwares, value);
    }

    public SoftwareObject SelectedSoftware
    {
        get => _selectedSoftware;
        set => this.RaiseAndSetIfChanged(ref _selectedSoftware, value);
    }

    protected override Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }

    private void Save() => CloseDialog(true);

    private void Cancel() => CloseDialog(false);

    private void CloseDialog(bool result)
    {
        if (View is IClosable closable)
            closable.Close(result);
    }

    private async Task AddSoftware()
    {
        var softwares = await ApiService.SoftwaresApiService.GetSoftwaresAsync();
        var filteredSoftwares = softwares.Where(s => s.Hardwares.All(h => h.Id != _id)).Select(s => new SelectedItemViewModel() { TransferObject = s });

        var dialog = new SelectItemsTemplate();
        var selectDialogVM = new SelectItemsViewModel
        {
            Items = new ObservableCollection<SelectedItemViewModel>(filteredSoftwares),
            View = dialog,
        };

        dialog.DataContext = selectDialogVM;

        bool result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dataContext = dialog.DataContext as SelectItemsViewModel;

        foreach (var selectedItem in dataContext.SelectedItems)
        {
            var software = await ApiService.SoftwaresApiService.GetSoftwareAsync(selectedItem.Id);
            Softwares.Add(software);
        }
    }

    private void DeleteSoftware()
    {
        if (SelectedSoftware is null)
            return;

        Softwares.Remove(SelectedSoftware);
    }
}
