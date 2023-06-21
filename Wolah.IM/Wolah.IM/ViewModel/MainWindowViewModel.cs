using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using Wolah.IM.Helper;

namespace Wolah.IM.ViewModel;

public class MainWindowViewModel
{
    public ICommand CloseWindowCommand { get; }
    public ICommand HideWindowCommand { get; }
    public ICommand MaximizeWindowCommand { get; }
    public MainWindowViewModel()
    {
        CloseWindowCommand = new RelayCommand<object>(CloseWindow);
        HideWindowCommand = new RelayCommand<object>(HideWindow);
        MaximizeWindowCommand = new RelayCommand<object>(MaximizeWindow);
    }
    
    
    #region Window Action
    private void CloseWindow(object? obj)
    {
        Application.Current.Shutdown();
    }
    private void HideWindow(object? obj)
    {
        SystemCommands.MinimizeWindow(Application.Current.MainWindow);
    }
    private void MaximizeWindow(object? obj)
    {
        if (Application.Current.MainWindow?.WindowState == WindowState.Normal)
        {
            SystemCommands.MaximizeWindow(Application.Current.MainWindow);
        }
        else
        {
            SystemCommands.RestoreWindow(Application.Current.MainWindow);
        }
    }
    #endregion
}