using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using UIClient.ViewModels.Equipment;

namespace UIClient.Views.Equipment;

public partial class ComplexesHardwareView : UserControl
{
    public ComplexesHardwareView()
    {
        InitializeComponent();
        DataContext = ((App)Application.Current).Services.GetRequiredService<ComplexesHardwareViewModel>();
    }
}