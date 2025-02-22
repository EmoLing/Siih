using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using ReactiveUI;
using Shared.DTOs.Users;
using UIClient.Services;
using UIClient.ViewModels.Dialogs;
using UIClient.Views;

namespace UIClient.ViewModels;

public class UsersViewModel : ViewModel
{
    private ObservableCollection<UserObject> _users = [];
    private UserObject _selectedUser;

    public UsersViewModel(ApiService apiService)
    : base(apiService)
    {
        AddUserCommand = ReactiveCommand.CreateFromTask(AddUser);
        DeleteUserCommand = ReactiveCommand.Create(DeleteUser);
        EditUserCommand = ReactiveCommand.CreateFromTask(EditUser);
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

    public ReactiveCommand<Unit, Unit> AddUserCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteUserCommand { get; }
    public ReactiveCommand<Unit, Unit> EditUserCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var users = await ApiService.GetUsersAsync();
        Users = new ObservableCollection<UserObject>(users);
    }

    private async Task AddUser()
    {
        var dialog = new UserEditDialog();
        dialog.DataContext = new UserEditDialogViewModel(ApiService) { View = dialog };

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
            var createdUser = await ApiService.AddUserAsync(newUser);

            if (createdUser is not null)
            {
                Users.Add(createdUser);
                SelectedUser = createdUser;
            }
        }
        catch
        {
        }
    }

    private void DeleteUser()
    {
        // Логика удаления пользователя
    }

    private async Task EditUser()
    {
        if (SelectedUser is null)
            return;

        var dialog = new UserEditDialog();

        dialog.DataContext = new UserEditDialogViewModel(ApiService)
        {
            FirstName = SelectedUser.FirstName,
            LastName = SelectedUser.LastName,
            Surname = SelectedUser.Surname,
            SelectedCabinet = SelectedUser.Cabinet,
            SelectedJobTitle = SelectedUser.JobTitle,
            View = dialog,
        };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as UserEditDialogViewModel);

        var originalUser = new UserObject
        {
            Id = SelectedUser.Id,
            Name = SelectedUser.Name,
            Surname = SelectedUser.Surname,
            JobTitle = SelectedUser.JobTitle,
            Cabinet = SelectedUser.Cabinet
        };

        var editUser = new UserObject()
        {
            FirstName = dialogData.FirstName,
            LastName = dialogData.LastName,
            Surname = dialogData.Surname,
            Cabinet = dialogData.SelectedCabinet,
            JobTitle = dialogData.SelectedJobTitle,
        };

        try
        {
            var updatedUser = await ApiService.UpdateUserAsync(editUser);

            if (updatedUser is not null)
            {
                Users.Replace(SelectedUser, updatedUser);
                SelectedUser = updatedUser;
            }
        }
        catch
        {
        }
    }
}
