using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace UIClient.Views.Dialogs.Equipment;

public partial class SelectHardwaresDialog : Window, IView, IClosable
{
    public SelectHardwaresDialog()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}