using DB.Models.Equipment;
using DB.Models.Users;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using UIClient.Services;
using UIClient.ViewModels.Dialogs;
using UIClient.Views;

namespace UIClient.ViewModels.Equipment;

public class SoftwaresViewModel : ViewModel
{
    private ObservableCollection<Software> _softwares = [];
    private Software _selectedSoftware;

    public ObservableCollection<Software> Softwares
    {
        get => _softwares;
        set => this.RaiseAndSetIfChanged(ref _softwares, value);
    }

    public Software SelectedSoftware
    {
        get => _selectedSoftware;
        set => this.RaiseAndSetIfChanged(ref _selectedSoftware, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public SoftwaresViewModel(ApiService apiService)
        : base(apiService)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddSoftware);
        DeleteCommand = ReactiveCommand.Create(DeleteSoftware);
        EditCommand = ReactiveCommand.CreateFromTask(EditSoftware);
    }

    protected override async Task LoadDataAsync()
    {
        var softwares = await ApiService.GetSoftwaresAsync();
        Softwares = new ObservableCollection<Software>(softwares);
    }

    private Task AddSoftware()
    {
        return Task.CompletedTask;
    }

    private void DeleteSoftware()
    {
        // Логика удаления пользователя
    }

    private Task EditSoftware()
    {
        return Task.CompletedTask;
    }
}
