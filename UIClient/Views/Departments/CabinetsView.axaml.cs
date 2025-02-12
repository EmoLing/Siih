using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using UIClient.ViewModels.Departments;

namespace UIClient.Views.Departments;

public partial class CabinetsView : UserControl
{
    public CabinetsView()
    {
        InitializeComponent();
        DataContext = ((App)Application.Current).Services.GetRequiredService<CabinetsViewModel>();
    }
}