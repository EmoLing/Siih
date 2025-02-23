using Avalonia.Controls;

namespace UIClient.Views.Dialogs.Equipment;

public partial class SoftwareEditDialog : Window, IView, IClosable
{
    public SoftwareEditDialog()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}