using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using ReactiveUI;
using Shared.DTOs;
using UIClient.Services;
using UIClient.Views;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs.SelectDialog;

public class SelectItemsViewModel : ViewModel
{
    private ObservableCollection<SelectedItemViewModel> _items = [];
    private SelectedItemViewModel _focusedObject;
    private bool _supportCheckBoxSelected;

    public SelectItemsViewModel(MasterApiService apiService, bool supportCheckBoxSelected = true)
        : base(apiService)
    {
        _supportCheckBoxSelected = supportCheckBoxSelected;
        Columns = [];

        AddCommand = ReactiveCommand.Create(AddItems);
        CancelCommand = ReactiveCommand.Create(Cancel);

        LoadColumns();
    }

    internal IView View { get; init; }
    public ICommand CancelCommand { get; }
    public ICommand AddCommand { get; }
    //public bool SupportCheckBoxSelected { get; set; } = true;

    public string Title { get; set; } = String.Empty;
    public string Caption { get; set; } = String.Empty;
    public List<DataGridColumn> Columns { get; }

    private void LoadColumns()
    {
        Columns.Add(new DataGridTemplateColumn
        {
            Header = "",
            IsVisible = _supportCheckBoxSelected,
            CellTemplate = new FuncDataTemplate<SelectedItemViewModel>((item, _) => new CheckBox
            {
                [!CheckBox.IsCheckedProperty] = new Binding("IsSelected", BindingMode.TwoWay),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center,
            })
        });

        Columns.AddRange(GetCustomDataGridColumns());
    }

    protected virtual DataGridColumn[] GetCustomDataGridColumns()
        => [new DataGridTextColumn { Header = "Наименование", Binding = new Binding("TransferObject.Name") }];
    
    public ObservableCollection<SelectedItemViewModel> Items
    {
        get => _items;
        set => this.RaiseAndSetIfChanged(ref _items, value);
    }

    public SelectedItemViewModel FocusedObject
    {
        get => _focusedObject;
        set => this.RaiseAndSetIfChanged(ref _focusedObject, value);
    }

    public List<TransferObject> SelectedItems { get; private set; } = [];

    public List<TransferObject> EarlySelectedItems { get; init; } = [];

    private void AddItems()
    {
        SelectedItems = Items.Where(h => h.IsSelected).Select(ch => ch.TransferObject).ToList();
        CloseDialog(true);
    }

    private void Cancel() => CloseDialog(false);

    private void CloseDialog(bool result) => (View as IClosable)?.Close(result);

    protected override Task LoadDataAsync()
    {
        return Task.CompletedTask;
    }
}
