using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Accessibility;
using CommunityToolkit.Mvvm.Input;
using Wolah.IM.Network;
using Newtonsoft.Json.Linq;
using Wolah.IM.Helper;
using Wolah.IM.Model;
using Wolah.IM.Private;
using System.Windows;
using System.Windows.Threading;
using Wolah.IM.View;

namespace Wolah.IM.ViewModel
{
    public class LoginWindowViewModel:INotifyPropertyChanged
    {
        private string? _coverText;
        public string? CoverText
        {
            get => _coverText;
            set
            {
                _coverText = value;
                OnPropertyChanged();
            }
        }

        #region Command Declare
        public ICommand CloseWindowCommand { get; }
        public ICommand HideWindowCommand { get; }
        #endregion
        
        public LoginWindowViewModel()
        {
            CloseWindowCommand = new RelayCommand<object>(CloseWindow);
            HideWindowCommand = new RelayCommand<object>(HideWindow);
            TCPClient.CallLoginWindowEvent += TCPClientOnCallLoginWindowEvent;

            InitCurrentTime();
        }

        private void TCPClientOnCallLoginWindowEvent(object? sender, JObject e)
        {
            if (e["cmd"].ToObject<int>() == Commands.CmdLogin.ToInt())
            {
                if (e["res"].ToObject<string>() == "yes")
                {
                    Application.Current.Dispatcher.BeginInvoke(new Action(delegate
                    {
                        var window = Application.Current.MainWindow;
                        window?.Close();
                        var mainWindow = new MainWindow();
                        mainWindow.Show();
                    }));
                    TCPClient.CallLoginWindowEvent -= TCPClientOnCallLoginWindowEvent;
                }
                else
                {
                    var err = e["err"].ToObject<string>();
                    MessageBox.Show(err);
                }
            }
        }

        private void InitCurrentTime()
        {
            DispatcherTimer showTimer = new DispatcherTimer();
            showTimer.Tick += (sender, e) => { CoverText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); };
            showTimer.Interval = new TimeSpan(0,0,0,1);
            showTimer.Start();  
        }
        #region MVVM
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Window Action
        private void CloseWindow(object? obj)
        {
            if (obj is Window window)
            {
                JObject msg = new JObject();
                msg["cmd"] = Commands.CmdLogout.ToInt();
                //SendMsg(msg);
                window.Close();
                Application.Current.Shutdown();
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
