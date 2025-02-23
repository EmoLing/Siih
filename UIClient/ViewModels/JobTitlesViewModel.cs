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

public class JobTitlesViewModel : ViewModel
{
    private ObservableCollection<JobTitleObject> _jobTitles;
    private JobTitleObject _selectedJobTitle;

    public JobTitlesViewModel(MasterApiService apiService, MainWindowViewModel mainWindowViewModel)
        : base(apiService, mainWindowViewModel)
    {
        JobTitles = [];

        AddCommand = ReactiveCommand.CreateFromTask(AddJobTitle);
        DeleteCommand = ReactiveCommand.CreateFromTask(DeleteJobTitle);
        EditCommand = ReactiveCommand.CreateFromTask(EditJobTitle);

        MainWindowViewModel.SetTitle("Должности");
    }

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

    protected override async Task LoadDataAsync()
    {
        var jobTitles = await ApiService.JobTitlesApiService.GetJobTitlesAsync();
        await Dispatcher.UIThread.InvokeAsync(() => jobTitles.ForEach(jt => JobTitles.Add(jt)));
    }

    private async Task AddJobTitle()
    {
        var dialog = new JobTitleEditDialog();
        dialog.DataContext = new JobTitlesEditDialogViewModel(ApiService) { View = dialog };

        await (dialog.DataContext as ViewModel)?.InitializeAsync();

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as JobTitlesEditDialogViewModel);

        var newJobTitle = new JobTitleObject() { Name = dialogData.Name, };

        try
        {
            var createdUser = await ApiService.JobTitlesApiService.AddJobTitleAsync(newJobTitle);

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

    private async Task DeleteJobTitle()
    {
        try
        {
            bool result = await ApiService.JobTitlesApiService.DeleteJobTitleAsync(SelectedJobTitle.Id);

            if (result)
            {
                JobTitles.Remove(SelectedJobTitle);
                SelectedJobTitle = JobTitles.FirstOrDefault();
            }
        }
        catch
        {
        }
    }

    private async Task EditJobTitle()
    {
        if (SelectedJobTitle is null)
            return;

        var dialog = new JobTitleEditDialog();

        dialog.DataContext = new JobTitlesEditDialogViewModel(ApiService)
        {
            Name = SelectedJobTitle.Name,
            View = dialog,
        };

        await (dialog.DataContext as ViewModel)?.InitializeAsync();

        bool result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = (dialog.DataContext as JobTitlesEditDialogViewModel);

        var editJobTitle = new JobTitleObject()
        {
            Id = SelectedJobTitle.Id,
            Guid = SelectedJobTitle.Guid,
            Name = dialogData.Name,
        };

        try
        {
            await ApiService.JobTitlesApiService.UpdateJobTitleAsync(editJobTitle);
            JobTitles.Replace(SelectedJobTitle, editJobTitle);
            SelectedJobTitle = editJobTitle;
        }
        catch
        {
        }
    }
}
