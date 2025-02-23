using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Threading;

namespace UIClient;

public class LoadingDialog : Window
{
    private TextBlock _loadingText;
    private ProgressBar _progressBar;

    public LoadingDialog()
    {
        Title = "Загрузка...";
        Width = 300;
        Height = 150;
        WindowStartupLocation = WindowStartupLocation.CenterScreen;

        // Создаем контейнер для элементов
        var stackPanel = new StackPanel
        {
            VerticalAlignment = VerticalAlignment.Center,
            HorizontalAlignment = HorizontalAlignment.Center,
            Spacing = 10
        };

        // Текст загрузки
        _loadingText = new TextBlock
        {
            Text = "Пожалуйста, подождите...",
            FontSize = 16,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        // Индикатор загрузки
        _progressBar = new ProgressBar
        {
            IsIndeterminate = true,
            Width = 200,
            Height = 10,
            HorizontalAlignment = HorizontalAlignment.Center
        };

        // Добавляем элементы в контейнер
        stackPanel.Children.Add(_loadingText);
        stackPanel.Children.Add(_progressBar);

        // Устанавливаем контейнер как содержимое окна
        Content = stackPanel;
    }

    // Метод для запуска диалога
    public static async Task ShowLoadingDialog(Func<Task> action, Window parentWindow)
    {
        var loadingDialog = new LoadingDialog();

        try
        {
            var task = loadingDialog.ShowDialog(parentWindow);
            await action();
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка в LoadingDialog: {ex.Message}");
            throw; // Повторно выбрасываем исключение
        }
        finally
        {
            loadingDialog.Close();
        }
    }
}
