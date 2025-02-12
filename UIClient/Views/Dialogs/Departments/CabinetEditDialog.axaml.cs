using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UIClient.Views;
using UIClient.Views.Dialogs;

namespace UIClient.Views.Dialogs.Departments;

public partial class CabinetEditDialog : Window, IView, IClosable
{
    public CabinetEditDialog()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}