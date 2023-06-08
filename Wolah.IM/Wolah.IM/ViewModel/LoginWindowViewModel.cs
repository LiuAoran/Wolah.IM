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

namespace Wolah.IM.ViewModel
{
    public class LoginWindowViewModel:INotifyPropertyChanged
    {
        //private TCPClient tcpClient = new TCPClient(ServerSource.ServerIP, ServerSource.ServerPort);
        private TCPClient tcpClient;

        private string? _userName;
        public string? UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                OnPropertyChanged();
            }
        }

        private string? _userPassword;
        public string? UserPassword
        {
            get => _userPassword;
            set
            {
                _userPassword = value;
                OnPropertyChanged();
            }
        }

        private string? _userRec;
        public string? UserRec
        {
            get => _userRec;
            set
            {
                _userRec = value;
                OnPropertyChanged();
            }
        }

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
        public ICommand SendMsgCommand { get;  }
        public ICommand CloseWindowCommand { get; }
        public ICommand HideWindowCommand { get; }
        public ICommand LoginCommand { get; }
        #endregion
        
        public LoginWindowViewModel()
        {
            SendMsgCommand = new RelayCommand<JObject>(SendMsg!);
            CloseWindowCommand = new RelayCommand<object>(CloseWindow);
            HideWindowCommand = new RelayCommand<object>(HideWindow);
            LoginCommand = new RelayCommand(Login);

            InitCurrentTime();
            //InitTcpConnection();
        }

        private void InitCurrentTime()
        {
            DispatcherTimer showTimer = new DispatcherTimer();
            showTimer.Tick += (sender, e) => { CoverText = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"); };
            showTimer.Interval = new TimeSpan(0,0,0,1);
            showTimer.Start();  
        }

        private void InitTcpConnection()
        {
            tcpClient.CallLoginWindow -= TcpClientCallLoginWindow;
            tcpClient.CallLoginWindow += TcpClientCallLoginWindow;

            Task.Run(async () =>
            {
                await tcpClient.StartReceivingAsync();
            });
        }

        private void TcpClientCallLoginWindow(object? sender, JObject e)
        {
            // Handle the data received from the server
            Console.WriteLine($"Received data: {e}");
            // Update the UserRec property
            UserRec = e.ToString();
        } 

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void Login()
        {
            // Send a chat message to the server
            JObject chat = new JObject();
            chat["cmd"] = Commands.CmdLogin.ToInt();
            chat["account"] = UserName;
            chat["password"] = UserPassword;
            if(tcpClient.IsConnected())
                tcpClient.SendDataAsync(chat);
        }

        private void SendMsg(JObject j)
        {
            if (tcpClient.IsConnected())
                tcpClient.SendDataAsync(j);
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
