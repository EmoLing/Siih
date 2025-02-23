using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ReactiveUI;
using Shared.DTOs;
using UIClient.Views;
using UIClient.Views.Dialogs;

namespace UIClient.ViewModels.Dialogs.SelectDialog;

public class SelectItemsViewModel : ViewModelBase
{
    private ObservableCollection<SelectedItemViewModel> _items = [];
    private SelectedItemViewModel _focusedObject;

    public SelectItemsViewModel()
    {
        AddCommand = ReactiveCommand.Create(AddItems);
        CancelCommand = ReactiveCommand.Create(Cancel);
    }

    internal IView View { get; init; }
    public ICommand CancelCommand { get; }
    public ICommand AddCommand { get; }
    public bool SupportCheckBoxSelected { get; set; } = true;

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

}
