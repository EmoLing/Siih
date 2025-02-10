﻿using ReactiveUI;
using System.Reactive;
using UIClient.Services;

namespace UIClient.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    private readonly ApiService _apiService;
    private object _currentContent;

    public object CurrentContent
    {
        get => _currentContent;
        set => this.RaiseAndSetIfChanged(ref _currentContent, value);
    }

    public ReactiveCommand<Unit, Unit> ShowUsersCommand { get; }

    public MainWindowViewModel(ApiService apiService)
    {
        _apiService = apiService;

        ShowUsersCommand = ReactiveCommand.Create(ShowUsers);
    }

    private void ShowUsers()
    {
        CurrentContent = new UsersViewModel(_apiService);
    }

}
