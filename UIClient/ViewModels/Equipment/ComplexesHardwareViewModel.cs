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
using UIClient.ViewModels.Dialogs.Equipment;
using UIClient.Views;
using UIClient.Views.Dialogs.Equipment;

namespace UIClient.ViewModels.Equipment;

public class ComplexesHardwareViewModel : ViewModel
{
    private ObservableCollection<ComplexHardware> _complexesHardware = [];
    private ComplexHardware _selectedComplexHardware;

    public ObservableCollection<ComplexHardware> ComplexesHardware
    {
        get => _complexesHardware;
        set => this.RaiseAndSetIfChanged(ref _complexesHardware, value);
    }

    public ComplexHardware SelectedComplexHardware
    {
        get => _selectedComplexHardware;
        set => this.RaiseAndSetIfChanged(ref _selectedComplexHardware, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public ComplexesHardwareViewModel(ApiService apiService)
        : base(apiService)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddComplexHardware);
        DeleteCommand = ReactiveCommand.Create(DeleteComplexHardware);
        EditCommand = ReactiveCommand.CreateFromTask(EditComplexHardware);
    }

    protected override async Task LoadDataAsync()
    {
        var complexesHardware = await ApiService.GetComplexesHardwareAsync();
        ComplexesHardware = new ObservableCollection<ComplexHardware>(complexesHardware);
    }

    private async Task AddComplexHardware()
    {
        var dialog = new ComplexHardwareEditDialog();
        dialog.DataContext = new ComplexHardwareEditDialogViewModel(ApiService, null) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as ComplexHardwareEditDialogViewModel);

        var complexHardware = new ComplexHardware()
        {
            Name = dialogData.Name,
            InventoryNumber = dialogData.InventoryNumber,
            Type = dialogData.Type,
            User = dialogData.User,
            Hardwares = [.. dialogData.Hardwares],
        };

        try
        {
            var createdComplexHardware = await ApiService.AddComplexHardwareAsync(complexHardware);

            if (createdComplexHardware is not null)
            {
                ComplexesHardware.Add(createdComplexHardware);
                SelectedComplexHardware = createdComplexHardware;
            }
        }
        catch
        {
        }
    }

    private void DeleteComplexHardware()
    {
        // Логика удаления пользователя
    }

    private async Task EditComplexHardware()
    {
        var dialog = new ComplexHardwareEditDialog();
        dialog.DataContext = new ComplexHardwareEditDialogViewModel(ApiService, SelectedComplexHardware) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        //var dialogData = (dialog.DataContext as ComplexHardwareEditDialogViewModel);

        //var complexHardware = new ComplexHardware()
        //{
        //    Name = dialogData.Name,
        //    InventoryNumber = dialogData.InventoryNumber,
        //    Type = dialogData.Type,
        //    User = dialogData.User,
        //    Hardwares = [.. dialogData.Hardwares],
        //};

        //try
        //{
        //    var createdComplexHardware = await ApiService.AddComplexHardwareAsync(complexHardware);

        //    if (createdComplexHardware is not null)
        //    {
        //        ComplexesHardware.Add(createdComplexHardware);
        //        SelectedComplexHardware = createdComplexHardware;
        //    }
        //}
        //catch
        //{
        //}
    }
}
