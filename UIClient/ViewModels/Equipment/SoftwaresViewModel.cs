using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;
using Shared.DTOs.Equipment;
using UIClient.Services;
using UIClient.ViewModels.Dialogs.Equipment;
using UIClient.Views.Dialogs.Equipment;

namespace UIClient.ViewModels.Equipment;

public class SoftwaresViewModel : ViewModel
{
    private ObservableCollection<SoftwareObject> _softwares = [];
    private SoftwareObject _selectedSoftware;

    public SoftwaresViewModel(MasterApiService apiService, MainWindowViewModel mainWindowViewModel)
        : base(apiService, mainWindowViewModel)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddSoftware);
        DeleteCommand = ReactiveCommand.CreateFromTask(DeleteSoftware);
        EditCommand = ReactiveCommand.CreateFromTask(EditSoftware);

        MainWindowViewModel.SetTitle("ПО");
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

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var softwares = await ApiService.SoftwaresApiService.GetSoftwaresAsync();
        Softwares = new ObservableCollection<SoftwareObject>(softwares);
    }

    private async Task AddSoftware()
    {
        var dialog = new SoftwareEditDialog();
        dialog.DataContext = new SoftwareEditDialogViewModel(ApiService, null) { View = dialog };

        bool result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = dialog.DataContext as SoftwareEditDialogViewModel;

        var software = new SoftwareObject()
        {
            Name = dialogData.Name,
            Version = dialogData.Version,
        };

        try
        {
            await ApiService.SoftwaresApiService.AddSoftwareAsync(software);
            var createdSoftware = await ApiService.SoftwaresApiService.GetSoftwareAsync(software.Id);

            if (createdSoftware is not null)
            {
                Softwares.Add(createdSoftware);
                SelectedSoftware = createdSoftware;
            }
        }
        catch
        {
        }
    }

    private async Task DeleteSoftware()
    {
        try
        {
            bool result = await ApiService.SoftwaresApiService.DeleteSoftwareAsync(SelectedSoftware.Id);

            if (result)
            {
                Softwares.Remove(SelectedSoftware);
                SelectedSoftware = Softwares.FirstOrDefault();
            }
        }
        catch
        {
        }
    }

    private async Task EditSoftware()
    {
        var dialog = new SoftwareEditDialog();
        dialog.DataContext = new SoftwareEditDialogViewModel(ApiService, SelectedSoftware) { View = dialog };

        bool result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = dialog.DataContext as SoftwareEditDialogViewModel;

        var software = new SoftwareObject()
        {
            Id = SelectedSoftware.Id,
            Guid = SelectedSoftware.Guid,
            Name = dialogData.Name,
            Version = dialogData.Version,
            Hardwares = SelectedSoftware.Hardwares,
        };

        try
        {
            await ApiService.SoftwaresApiService.UpdateSoftwareAsync(software);
            var updatedSoftware = await ApiService.SoftwaresApiService.GetSoftwareAsync(software.Id);

            Softwares.Replace(SelectedSoftware, updatedSoftware);
            var changedCollection = Softwares.ToList();
            Softwares = new ObservableCollection<SoftwareObject>(changedCollection);
            SelectedSoftware = updatedSoftware;
        }
        catch
        {
        }
    }
}
