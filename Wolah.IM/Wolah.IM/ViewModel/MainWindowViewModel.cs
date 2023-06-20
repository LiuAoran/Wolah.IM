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
    public class MainWindowViewModel
    {
        public ICommand CloseWindowCommand { get; }
        public ICommand ResizeWindowCommand { get; }
        public ICommand HideWindowCommand { get; }

        public MainWindowViewModel()
        {
            CloseWindowCommand = new RelayCommand<object>(CloseWindow);
            ResizeWindowCommand = new RelayCommand<object>(ResizeWindow);
            HideWindowCommand = new RelayCommand<object>(HideWindow);
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
        {
            if (obj is Window window)
            {
                if (window.WindowState == WindowState.Maximized)
                {
                    window.WindowState = WindowState.Normal;
                }
                else
                {
                    window.WindowState = WindowState.Maximized;
                }
            }
        }

        private void HideWindow(object? obj)
        {
            if (obj is Window window)
            {
                window.WindowState = WindowState.Minimized;
            }
        }


        #endregion
    }
}
