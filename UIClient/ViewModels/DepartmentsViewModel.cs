using DB.Models.Departments;
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

namespace UIClient.ViewModels;

public class DepartmentsViewModel : ViewModel
{
    private ObservableCollection<Department> _departments = [];
    private Department _selectedDepartment;

    public ObservableCollection<Department> Departments
    {
        get => _departments;
        set => this.RaiseAndSetIfChanged(ref _departments, value);
    }

    public Department SelectedDepartment
    {
        get => _selectedDepartment;
        set => this.RaiseAndSetIfChanged(ref _selectedDepartment, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public DepartmentsViewModel(ApiService apiService)
        : base(apiService)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddDepartment);
        DeleteCommand = ReactiveCommand.Create(DeleteDepartment);
        EditCommand = ReactiveCommand.CreateFromTask(EditDepartment);
    }

    protected override async Task LoadDataAsync()
    {
        var departments = await ApiService.GetDepartmentsAsync();
        Departments = new ObservableCollection<Department>(departments);
    }

    private async Task AddDepartment()
    {
        var dialog = new JobTitleEditDialog();
        dialog.DataContext = new JobTitlesEditDialogViewModel(ApiService) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as JobTitlesEditDialogViewModel);

        var newJobTitle = new JobTitle() { Name = dialogData.Name, };

        try
        {
            var createdUser = await ApiService.AddJobTitleAsync(newJobTitle);

            if (createdUser is not null)
            {
                //JobTitles.Add(createdUser);
                //SelectedJobTitle = createdUser;
            }
        }
        catch
        {
        }
    }

    private void DeleteDepartment()
    {
        // Логика удаления пользователя
    }

    private Task EditDepartment()
    {
        return Task.CompletedTask;
        //    if (SelectedJobTitle is null)
        //        return;

        //    var dialog = new UserEditDialog();

        //    dialog.DataContext = new UserEditDialogViewModel(ApiService)
        //    {
        //        FirstName = SelectedJobTitle.FirstName,
        //        LastName = SelectedJobTitle.LastName,
        //        Surname = SelectedJobTitle.Surname,
        //        SelectedCabinet = SelectedJobTitle.Cabinet,
        //        SelectedJobTitle = SelectedJobTitle.JobTitle,
        //        View = dialog,
        //    };

        //    var result = await dialog.ShowDialog<bool>(App.Owner);

        //    if (!result)
        //        return;

        //    var dialogData = (dialog.DataContext as UserEditDialogViewModel);

        //    var originalUser = new User
        //    {
        //        Id = SelectedJobTitle.Id,
        //        Name = SelectedJobTitle.Name,
        //        Surname = SelectedJobTitle.Surname,
        //        JobTitle = SelectedJobTitle.JobTitle,
        //        Cabinet = SelectedJobTitle.Cabinet
        //    };

        //    var editUser = new User()
        //    {
        //        FirstName = dialogData.FirstName,
        //        LastName = dialogData.LastName,
        //        Surname = dialogData.Surname,
        //        Cabinet = dialogData.SelectedCabinet,
        //        JobTitle = dialogData.SelectedJobTitle,
        //    };

        //    try
        //    {
        //        var updatedUser = await ApiService.UpdateUserAsync(originalUser, editUser);

        //        if (updatedUser is not null)
        //        {
        //            Users.Add(updatedUser);
        //            SelectedJobTitle = updatedUser;
        //        }
        //    }
        //    catch
        //    {
        //    }
    }
}
