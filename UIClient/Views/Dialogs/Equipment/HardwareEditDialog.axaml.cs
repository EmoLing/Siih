using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UIClient.Views.Dialogs.Equipment;

public partial class HardwareEditDialog : Window, IView, IClosable
{
    public HardwareEditDialog()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}