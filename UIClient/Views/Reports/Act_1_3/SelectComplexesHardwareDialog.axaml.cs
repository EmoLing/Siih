using Avalonia.Controls;
using UIClient.Views.Dialogs;

namespace UIClient.Views.Reports.Act_1_3;

public partial class SelectComplexesHardwareDialog : Window, IView, IClosable
{
    public SelectComplexesHardwareDialog()
    {
        InitializeComponent();
    }

    public void Close(bool result) => base.Close(result);
}