using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using DynamicData;
using ReactiveUI;
using Shared.DTOs.Equipment;
using Shared.DTOs.Users;
using UIClient.Services;
using UIClient.ViewModels.Dialogs.SelectDialog;
using UIClient.Views.Dialogs;
using UIClient.Views.Dialogs.Equipment;
using UIClient.Views.Dialogs.SelectDialog;

namespace UIClient.ViewModels.Dialogs.Equipment;

public class ComplexHardwareEditDialogViewModel : ViewModel
{
    private string _name;
    private UserObject _user;
    private string _inventoryNumber;
    private ComplexHardwareType _type;
    private ObservableCollection<HardwareObject> _hardwares = [];

    public ComplexHardwareEditDialogViewModel(MasterApiService apiService, ComplexHardwareObject complexHardware)
        : base(apiService)
    {
        SaveCommand = ReactiveCommand.Create(Save);
        CancelCommand = ReactiveCommand.Create(Cancel);
        AddHardwareCommand = ReactiveCommand.Create(AddHardware);
        CreateHardwareCommand = ReactiveCommand.Create(CreateHardware);
        SelectUserCommand = ReactiveCommand.CreateFromTask(SelectUser);

        if (complexHardware is not null)
        {
            Name = complexHardware.Name;
            Type = complexHardware.Type;
            InventoryNumber = complexHardware.InventoryNumber;
            User = complexHardware.User;
            Hardwares = new ObservableCollection<HardwareObject>(complexHardware.Hardwares);
        }
    }

    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }
    public ICommand AddHardwareCommand { get; }
    public ICommand CreateHardwareCommand { get; }
    public ICommand SelectUserCommand { get; }

    public string Name
    {
        get => _name;
        set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public UserObject User
    {
        get => _user;
        set => this.RaiseAndSetIfChanged(ref _user, value);
    }

    public string InventoryNumber
    {
        get => _inventoryNumber;
        set => this.RaiseAndSetIfChanged(ref _inventoryNumber, value);
    }

    public ComplexHardwareType Type
    {
        get => _type;
        set => this.RaiseAndSetIfChanged(ref _type, value);
    }

    public ObservableCollection<HardwareObject> Hardwares
    {
        get => _hardwares;
        set => this.RaiseAndSetIfChanged(ref _hardwares, value);
    }

    protected override Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }

    private void Save() => CloseDialog(true);

    private void Cancel() => CloseDialog(false);

    private async Task AddHardware()
    {
        var dialog = new SelectHardwaresDialog();
        dialog.DataContext = new SelectHardwaresDialogViewModel(ApiService, [.. Hardwares]) { View = dialog };
        (dialog.DataContext as ViewModel)?.InitializeAsync();

        bool result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dataContext = (dialog.DataContext as SelectHardwaresDialogViewModel);
        Hardwares.AddRange(dataContext.SelectedHardwares);
    }

    private void CreateHardware()
    {
    }

    private async Task SelectUser()
    {
        var users = await ApiService.UsersApiService.GetUsersAsync();

        var filteredUsers = users.Where(u => u.ComplexHardwares is null || u.ComplexHardwares.Count == 0)
            .Select(u => new SelectedItemViewModel() { TransferObject = u }).ToList();

        var selectDialog = new SelectItemsTemplate();
        var selectDialogVM = new SelectUserDialogViewModel(ApiService, false)
        {
            Items = new ObservableCollection<SelectedItemViewModel>(filteredUsers),
            View = selectDialog,
            Caption = "Выбор пользователя",
            Title = "Выберите доступного пользователя"
        };

        selectDialog.DataGrid.Columns.AddRange(selectDialogVM.Columns);
        selectDialog.DataContext = selectDialogVM;

        bool result = await selectDialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var focusedItem = (selectDialog.DataContext as SelectItemsViewModel)?.FocusedObject?.TransferObject;

        if (focusedItem is null)
            return;

        var user = await ApiService.UsersApiService.GetUserAsync(focusedItem.Id);

        if (user is null)
            return;

        User = user;
    }

    private void CloseDialog(bool result)
    {
        if (View is IClosable closable)
            closable.Close(result);
    }
}
