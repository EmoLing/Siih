using Avalonia;
using Avalonia.ReactiveUI;
using Serilog;
using System;
using System.Diagnostics;

namespace UIClient;

internal sealed class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.File("logs/avalonia.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

        Trace.Listeners.Add(new SerilogTraceListener());

        try
        {
            Log.Information("Запуск приложения");
            BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Приложение завершилось с ошибкой");
        }
        finally
        {
            Log.CloseAndFlush();
        }
    }

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .UseReactiveUI();

    public class SerilogTraceListener : TraceListener
    {
        public override void Write(string message)
        {
            Log.Information(message);
        }

        public override void WriteLine(string message)
        {
            Log.Information(message);
        }
    }
}
