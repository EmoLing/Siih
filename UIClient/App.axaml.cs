using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using UIClient.Services;
using UIClient.ViewModels;
using UIClient.ViewModels.Departments;
using UIClient.ViewModels.Equipment;
using UIClient.ViewModels.Reports.Act_1_3;
using UIClient.Views;

namespace UIClient;
public partial class App : Application
{
    public IServiceProvider Services { get; private set; }

    public static Window Owner => Application.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop ? desktop.MainWindow : null;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var services = new ServiceCollection();
        services.AddSingleton<HttpClient>(sp => new HttpClient() { BaseAddress = new System.Uri("https://localhost:7208") });
        services.AddSingleton<ApiService>();
        services.AddTransient<MainWindowViewModel>();

        services.AddTransient<UsersViewModel>();
        services.AddTransient<JobTitlesViewModel>();

        services.AddTransient<SoftwaresViewModel>();
        services.AddTransient<HardwaresViewModel>();
        services.AddTransient<ComplexesHardwareViewModel>();

        services.AddTransient<CabinetsViewModel>();
        services.AddTransient<DepartmentsViewModel>();

        services.AddTransient<Act_1_3InfoDialogViewModel>();

        var serviceProvider = services.BuildServiceProvider();

        Services = serviceProvider;

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = serviceProvider.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}