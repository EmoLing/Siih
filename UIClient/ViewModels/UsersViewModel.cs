using DB.Models.Departments;
using DB.Models.Users;
using DynamicData;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using UIClient.Services;

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
        AddUserCommand = ReactiveCommand.Create(AddUser);
        DeleteUserCommand = ReactiveCommand.Create(DeleteUser);
        EditUserCommand = ReactiveCommand.Create(EditUser);
    }

    protected override async Task LoadDataAsync()
    {
        var users = await ApiService.GetUsersAsync();
        Users = new ObservableCollection<User>(users);
    }

    private void AddUser()
    {
        // Логика добавления пользователя
    }

    private void DeleteUser()
    {
        // Логика удаления пользователя
    }

    private void EditUser()
    {
        // Логика изменения пользователя
    }
}
