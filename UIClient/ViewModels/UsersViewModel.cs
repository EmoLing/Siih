using DB.Models.Departments;
using DB.Models.Users;
using DynamicData;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using UIClient.Services;
using UIClient.ViewModels.Dialogs;
using UIClient.Views;

namespace UIClient.ViewModels;

public class UsersViewModel : ViewModel
{
    private ObservableCollection<User> _users = [];

    public ObservableCollection<User> Users
    {
        get => _users;
        set => this.RaiseAndSetIfChanged(ref _users, value);
    }

    public ReactiveCommand<Unit, Unit> AddUserCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteUserCommand { get; }
    public ReactiveCommand<Unit, Unit> EditUserCommand { get; }

    public UsersViewModel(ApiService apiService)
        : base(apiService)
    {
        AddUserCommand = ReactiveCommand.CreateFromTask(AddUser);
        DeleteUserCommand = ReactiveCommand.Create(DeleteUser);
        EditUserCommand = ReactiveCommand.CreateFromTask(EditUser);
    }

    protected override async Task LoadDataAsync()
    {
        var users = await ApiService.GetUsersAsync();
        Users = new ObservableCollection<User>(users);
    }

    private async Task AddUser()
    {
        var dialog = new UserEditDialog() { DataContext = new UserEditDialogViewModel(ApiService) };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as UserEditDialogViewModel);

        var newUser = new User()
        {
            FirstName = dialogData.FirstName,
            LastName = dialogData.LastName,
            Surname = dialogData.Surname,
            Cabinet = dialogData.SelectedCabinet,
            JobTitle = dialogData.SelectedJobTitle,
        };

        await ApiService.AddUserAsync(newUser);
    }

    private void DeleteUser()
    {
        // Логика удаления пользователя
    }

    private Task EditUser()
    {
        return Task.CompletedTask;
        // Логика изменения пользователя
    }
}
