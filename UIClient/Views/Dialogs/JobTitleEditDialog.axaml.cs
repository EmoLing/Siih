using Avalonia.Controls;
using UIClient.Views.Dialogs;

namespace UIClient.Views;

public partial class JobTitleEditDialog : Window, IView, IClosable
{
    public JobTitleEditDialog()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}