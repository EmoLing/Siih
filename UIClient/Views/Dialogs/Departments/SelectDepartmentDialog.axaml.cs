using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UIClient.Views.Dialogs.Departments;

public partial class SelectDepartmentDialog : Window, IView, IClosable
{
    public SelectDepartmentDialog()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}