using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Threading;
using DynamicData;
using ReactiveUI;
using Shared.DTOs.Users;
using UIClient.Services;
using UIClient.ViewModels.Dialogs;
using UIClient.Views;

namespace UIClient.ViewModels;

public class UsersViewModel : ViewModel
{
    private ObservableCollection<UserObject> _allUsers;
    private ObservableCollection<UserObject> _users;
    private UserObject _selectedUser;
    private string _filterText;

    public UsersViewModel(MasterApiService apiService, MainWindowViewModel mainWindowViewModel)
    : base(apiService, mainWindowViewModel)
    {
        AddUserCommand = ReactiveCommand.CreateFromTask(AddUser);
        DeleteUserCommand = ReactiveCommand.CreateFromTask(DeleteUser);
        EditUserCommand = ReactiveCommand.CreateFromTask(EditUser);

        Users = [];

        MainWindowViewModel.SetTitle("Пользователи");
    }

    public ObservableCollection<UserObject> Users
    {
        get => _users;
        set => this.RaiseAndSetIfChanged(ref _users, value);
    }

    public UserObject SelectedUser
    {
        get => _selectedUser;
        set => this.RaiseAndSetIfChanged(ref _selectedUser, value);
    }

    public string FilterText
    {
        get => _filterText;
        set
        {
            _filterText = value;
            this.RaisePropertyChanged(nameof(FilterText));
            FilterData();
        }
    }

    public ReactiveCommand<Unit, Unit> AddUserCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteUserCommand { get; }
    public ReactiveCommand<Unit, Unit> EditUserCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var users = await ApiService.UsersApiService.GetUsersAsync();
        _allUsers = new ObservableCollection<UserObject>(users);
        await Dispatcher.UIThread.InvokeAsync(() => users.ForEach(u => Users.Add(u)));
    }

    private void FilterData()
    {
        if (String.IsNullOrEmpty(FilterText))
        {
            Users = _allUsers;
        }
        else
        {
            var filtered = _users.Where(u => u.Cabinet?.Department?.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase) is true).ToList();
            Users = new ObservableCollection<UserObject>(filtered);
        }
    }

    private async Task AddUser()
    {
        var dialog = new UserEditDialog();
        var dialogVM = new UserEditDialogViewModel(ApiService) { View = dialog };

        await dialogVM.InitializeAsync();
        dialog.DataContext = dialogVM;

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as UserEditDialogViewModel);

        var newUser = new UserObject()
        {
            FirstName = dialogData.FirstName,
            LastName = dialogData.LastName,
            Surname = dialogData.Surname,
            Cabinet = dialogData.SelectedCabinet,
            JobTitle = dialogData.SelectedJobTitle,
        };

        try
        {
            var createdUser = await ApiService.UsersApiService.AddUserAsync(newUser);

            if (createdUser is not null)
            {
                _allUsers.Add(createdUser);
                Users.Add(createdUser);
                SelectedUser = createdUser;
            }
        }
        catch
        {
        }
    }

    private async Task DeleteUser()
    {
        try
        {
            bool result = await ApiService.UsersApiService.DeleteUserAsync(SelectedUser.Id);

            if (result)
            {
                _allUsers.Remove(SelectedUser);
                Users.Remove(SelectedUser);
                SelectedUser = Users.FirstOrDefault();
            }
        }
        catch
        {
        }
    }

    private async Task EditUser()
    {
        if (SelectedUser is null)
            return;

        var dialog = new UserEditDialog();

        var dialogVM = new UserEditDialogViewModel(ApiService)
        {
            FirstName = SelectedUser.FirstName,
            LastName = SelectedUser.LastName,
            Surname = SelectedUser.Surname,
            SelectedCabinet = SelectedUser.Cabinet,
            SelectedJobTitle = SelectedUser.JobTitle,
            View = dialog,
        };

        await dialogVM.InitializeAsync();
        dialog.DataContext = dialogVM;

        bool result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = dialog.DataContext as UserEditDialogViewModel;

        var editUser = new UserObject()
        {
            Id = SelectedUser.Id,
            Guid = SelectedUser.Guid,
            Name = SelectedUser.Name,
            FirstName = dialogData.FirstName,
            LastName = dialogData.LastName,
            Surname = dialogData.Surname,
            Cabinet = dialogData.SelectedCabinet,
            JobTitle = dialogData.SelectedJobTitle,
            ComplexHardwares = SelectedUser.ComplexHardwares
        };

        try
        {
            await ApiService.UsersApiService.UpdateUserAsync(editUser);

            _allUsers.Replace(SelectedUser, editUser);
            Users.Replace(SelectedUser, editUser);
            SelectedUser = editUser;
        }
        catch
        {
        }
    }
}
