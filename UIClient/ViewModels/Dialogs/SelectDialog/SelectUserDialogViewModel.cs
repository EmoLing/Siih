using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using UIClient.Services;

namespace UIClient.ViewModels.Dialogs.SelectDialog;

public class SelectUserDialogViewModel(MasterApiService apiService, bool supportCheckBoxSelected) : SelectItemsViewModel(apiService, supportCheckBoxSelected)
{
    protected override DataGridColumn[] GetCustomDataGridColumns()
        => [
                new DataGridTextColumn { Header = "Фамилия", Binding = new Binding("TransferObject.LastName") },
                new DataGridTextColumn { Header = "Имя", Binding = new Binding("TransferObject.FirstName") },
                new DataGridTextColumn { Header = "Отчество", Binding = new Binding("TransferObject.Surname") },
                new DataGridTextColumn { Header = "Кабинет", Binding = new Binding("TransferObject.Cabinet.Name") }
           ];
    
}
