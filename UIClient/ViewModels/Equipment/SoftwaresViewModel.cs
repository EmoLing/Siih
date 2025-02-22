using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using Shared.DTOs.Equipment;
using UIClient.Services;

namespace UIClient.ViewModels.Equipment;

public class SoftwaresViewModel : ViewModel
{
    private ObservableCollection<SoftwareObject> _softwares = [];
    private SoftwareObject _selectedSoftware;

    public SoftwaresViewModel(ApiService apiService, MainWindowViewModel mainWindowViewModel)
        : base(apiService, mainWindowViewModel)
    {
        AddCommand = ReactiveCommand.CreateFromTask(AddSoftware);
        DeleteCommand = ReactiveCommand.Create(DeleteSoftware);
        EditCommand = ReactiveCommand.CreateFromTask(EditSoftware);

        MainWindowViewModel.SetTitle("ПО");
    }

    public ObservableCollection<SoftwareObject> Softwares
    {
        get => _softwares;
        set => this.RaiseAndSetIfChanged(ref _softwares, value);
    }

    public SoftwareObject SelectedSoftware
    {
        get => _selectedSoftware;
        set => this.RaiseAndSetIfChanged(ref _selectedSoftware, value);
    }

    public ReactiveCommand<Unit, Unit> AddCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteCommand { get; }
    public ReactiveCommand<Unit, Unit> EditCommand { get; }

    protected override async Task LoadDataAsync()
    {
        var softwares = await ApiService.GetSoftwaresAsync();
        Softwares = new ObservableCollection<SoftwareObject>(softwares);
    }

    private Task AddSoftware()
    {
        return Task.CompletedTask;
    }

    private void DeleteSoftware()
    {
        // Логика удаления пользователя
    }

    private Task EditSoftware()
    {
        return Task.CompletedTask;
    }
}
