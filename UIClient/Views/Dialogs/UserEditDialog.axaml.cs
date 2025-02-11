using Avalonia.Controls;
using UIClient.Views.Dialogs;

namespace UIClient.Views;

public partial class UserEditDialog : Window, IView, IClosable
{
    public UserEditDialog()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}