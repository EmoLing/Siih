using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Shared.DTOs.Users;
using UIClient.Services;
using UIClient.ViewModels.Dialogs;
using UIClient.Views;

namespace UIClient.ViewModels;

public class JobTitlesViewModel : ViewModel
{
    private ObservableCollection<JobTitleObject> _jobTitles = [];
    private JobTitleObject _selectedJobTitle;

    public ObservableCollection<JobTitleObject> JobTitles
    {
        get => _jobTitles;
        set => this.RaiseAndSetIfChanged(ref _jobTitles, value);
    }

    public JobTitleObject SelectedJobTitle
    {
        get => _selectedJobTitle;
        set => this.RaiseAndSetIfChanged(ref _selectedJobTitle, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    public JobTitlesViewModel(ApiService apiService)
        : base(apiService)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddJobTitle);
        DeleteCommand = ReactiveCommand.Create(DeleteJobTitle);
        EditCommand = ReactiveCommand.CreateFromTask(EditJobTitle);
    }

    protected override async Task LoadDataAsync()
    {
        var jobTitles = await ApiService.GetJobTitlesAsync();
        JobTitles = new ObservableCollection<JobTitleObject>(jobTitles);
    }

    private async Task AddJobTitle()
    {
        var dialog = new JobTitleEditDialog();
        dialog.DataContext = new JobTitlesEditDialogViewModel(ApiService) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as JobTitlesEditDialogViewModel);

        var newJobTitle = new JobTitleObject() { Name = dialogData.Name, };

        try
        {
            var createdUser = await ApiService.AddJobTitleAsync(newJobTitle);

            if (createdUser is not null)
            {
                JobTitles.Add(createdUser);
                SelectedJobTitle = createdUser;
            }
        }
        catch
        {
        }
    }

    private void DeleteJobTitle()
    {
        // Логика удаления пользователя
    }

    private Task EditJobTitle()
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
