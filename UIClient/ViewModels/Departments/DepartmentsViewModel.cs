using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Shared.DTOs.Departments;
using UIClient.Services;
using UIClient.ViewModels.Dialogs.Departments;
using UIClient.Views.Dialogs.Departments;

namespace UIClient.ViewModels.Departments;

public class DepartmentsViewModel : ViewModel
{
    private ObservableCollection<DepartmentObject> _departments = [];
    private DepartmentObject _selectedDepartment;

    public ObservableCollection<DepartmentObject> Departments
    {
        get => _departments;
        set => this.RaiseAndSetIfChanged(ref _departments, value);
    }

    public DepartmentObject SelectedDepartment
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
        Departments = new ObservableCollection<DepartmentObject>(departments);
    }

    private async Task AddDepartment()
    {
        var dialog = new DepartmentEditDialog();
        dialog.DataContext = new DepartmentEditDialogViewModel(ApiService, null) { View = dialog };

        var result = await dialog.ShowDialog<bool>(App.Owner);

        if (!result)
            return;

        var dialogData = dialog.DataContext as DepartmentEditDialogViewModel;

        var department = new DepartmentObject()
        {
            Name = dialogData.Name,
            Cabinets = [.. dialogData.Cabinets]
        };

        try
        {
            var createdDepartment = await ApiService.AddDepartmentAsync(department);

            if (createdDepartment is not null)
            {
                Departments.Add(createdDepartment);
                SelectedDepartment = createdDepartment;
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
    }
}
