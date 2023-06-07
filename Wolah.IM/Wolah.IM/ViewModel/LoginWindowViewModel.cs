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

namespace Wolah.IM.ViewModel
{
    public class LoginWindowViewModel:INotifyPropertyChanged
    {
        private TCPClient tcpClient = new TCPClient(ServerSource.ServerIP, ServerSource.ServerPort);
        public string? UserName { get; set; }
        public string? UserPassword { get; set; }
        
        private string? _userSend;
        private string? _userRec;

        public string? UserSend
        {
            get => _userSend;
            set
            {
                _userSend = value;
                OnPropertyChanged();
            }
        }

        public string? UserRec
        {
            get => _userRec;
            set
            {
                _userRec = value;
                OnPropertyChanged();
            }
        }

        public ICommand SendMsgCommand { get;  }

        public LoginWindowViewModel()
        {
            SendMsgCommand = new RelayCommand(SendMsg);
            tcpClient.DataReceived -= TcpClient_DataReceived;
            tcpClient.DataReceived += TcpClient_DataReceived;
            tcpClient.RecEndEvent -= TcpClientRecEndEvent;
            tcpClient.RecEndEvent += TcpClientRecEndEvent;
            
            Task.Run(async () =>
            {
                await tcpClient.StartReceivingAsync();
            });
        }
        
        private void TcpClientRecEndEvent(object? sender, string e)
        {
            UserRec = e;
        }
        private void TcpClient_DataReceived(object? sender, JObject e)
        {
            // Handle the data received from the server
            Console.WriteLine($"Received data: {e}");
            // Update the UserRec property
            UserRec = e.ToString();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }

        public void SendMsg()
        {
            // Send a chat message to the server
            JObject chat = new JObject();
            chat["cmd"] = (int)commands.cmd_login;
            chat["account"] = UserSend;
            chat["password"] = UserSend;
            if(tcpClient.IsConnected())
                tcpClient.SendDataAsync(chat);
        }
    }
}
