using Avalonia.Controls;

namespace UIClient.Views.Dialogs.SelectDialog;

public partial class SelectItemsTemplate : Window, IView, IClosable
{
    public SelectItemsTemplate()
    {
        InitializeComponent();
        DataGrid = this.FindControl<DataGrid>("TemplateDataGrid");
    }

    public DataGrid DataGrid { get; private set; }

    public void Close(bool result) => base.Close(result);
}