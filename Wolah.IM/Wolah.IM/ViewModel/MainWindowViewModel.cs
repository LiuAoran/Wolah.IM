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

    public class MainWindowViewModel
    {
        public ICommand CloseWindowCommand { get; }
        public ICommand HideWindowCommand { get; }
        public ICommand ResizeWindowCommand { get; }
        public MainWindowViewModel()
        {
            CloseWindowCommand = new RelayCommand(CloseWindow);
            HideWindowCommand = new RelayCommand(HideWindow);
            ResizeWindowCommand = new RelayCommand(ResizeWindow);
        }


        #region Window Action
        private void CloseWindow()
        {
                Application.Current.Shutdown();
        }

        private void HideWindow()
        {
            SystemCommands.MinimizeWindow(Application.Current.MainWindow);
        }
        private void ResizeWindow()
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
