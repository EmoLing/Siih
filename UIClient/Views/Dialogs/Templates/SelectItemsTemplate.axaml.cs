using Avalonia.Controls;

namespace UIClient.Views.Dialogs.SelectDialog;

public partial class SelectItemsTemplate : Window, IView, IClosable
{
    public SelectItemsTemplate()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}