using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Shared.DTOs.Equipment;
using UIClient.Services;
using UIClient.ViewModels.Dialogs.Equipment;
using UIClient.Views.Dialogs.Equipment;

namespace UIClient.ViewModels.Equipment;

public class ComplexesHardwareViewModel : ViewModel
{
    private ObservableCollection<ComplexHardwareObject> _complexesHardware = [];
    private ComplexHardwareObject _selectedComplexHardware;

    public ComplexesHardwareViewModel(MasterApiService apiService, MainWindowViewModel mainWindowViewModel)
        : base(apiService, mainWindowViewModel)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddComplexHardware);
        DeleteCommand = ReactiveCommand.Create(DeleteComplexHardware);
        EditCommand = ReactiveCommand.CreateFromTask(EditComplexHardware);

        MainWindowViewModel.SetTitle("Комплекс");
    }

    public ObservableCollection<ComplexHardwareObject> ComplexesHardware
    {
        get => _complexesHardware;
        set => this.RaiseAndSetIfChanged(ref _complexesHardware, value);
    }

    public ComplexHardwareObject SelectedComplexHardware
    {
        get => _selectedComplexHardware;
        set => this.RaiseAndSetIfChanged(ref _selectedComplexHardware, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var complexesHardware = await ApiService.ComplexesHardwareApiService.GetComplexesHardwareAsync();
        ComplexesHardware = new ObservableCollection<ComplexHardwareObject>(complexesHardware);
    }

    private async Task AddComplexHardware()
    {
        var dialog = new ComplexHardwareEditDialog();
        dialog.DataContext = new ComplexHardwareEditDialogViewModel(ApiService, null) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as ComplexHardwareEditDialogViewModel);

        var complexHardware = new ComplexHardwareObject()
        {
            Name = dialogData.Name,
            InventoryNumber = dialogData.InventoryNumber,
            Type = dialogData.Type,
            User = dialogData.User,
            Hardwares = [.. dialogData.Hardwares],
        };

        try
        {
            var createdComplexHardware = await ApiService.ComplexesHardwareApiService.AddComplexHardwareAsync(complexHardware);

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
