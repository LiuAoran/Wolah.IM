using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Wolah.IM.Helper;

namespace Wolah.IM.ViewModel
{ 
namespace Wolah.IM.ViewModel;

public class MainWindowViewModel
{
    public ICommand CloseWindowCommand { get; }
        public ICommand ResizeWindowCommand { get; }
    public ICommand HideWindowCommand { get; }
    public ICommand MaximizeWindowCommand { get; }
    public MainWindowViewModel()
    {
        CloseWindowCommand = new RelayCommand<object>(CloseWindow);
            ResizeWindowCommand = new RelayCommand<object>(ResizeWindow);
        HideWindowCommand = new RelayCommand<object>(HideWindow);
        MaximizeWindowCommand = new RelayCommand<object>(MaximizeWindow);
    }
    
    
    #region Window Action
    private void CloseWindow(object? obj)
    {
            if (obj is Window window)
            {
                window.Close();
        Application.Current.Shutdown();
    }
        }

        private void ResizeWindow(object? obj)
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
}
