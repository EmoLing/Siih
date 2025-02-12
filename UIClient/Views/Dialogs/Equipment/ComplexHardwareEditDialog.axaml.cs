using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using UIClient.Views.Dialogs;

namespace UIClient.Views.Dialogs.Equipment;

public partial class ComplexHardwareEditDialog : Window, IView, IClosable
{
    public ComplexHardwareEditDialog()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}