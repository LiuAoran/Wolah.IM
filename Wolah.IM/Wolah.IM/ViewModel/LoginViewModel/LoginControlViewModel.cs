using System;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using Wolah.IM.Helper;
using Wolah.IM.Network;
using Wolah.IM.Private;

namespace Wolah.IM.ViewModel.LoginViewModel;

public class LoginControlViewModel
{
    private TCPClient tcpClient;
    private string _userName = string.Empty;
    public string UserName
    {
        get => _userName;
        set => _userName = value;
    }
    private string _userPassword = string.Empty;
    public string UserPassword
    {
        get => _userPassword;
        set => _userPassword = value;
    }
    
    public ICommand LoginCommand { get; } 
    public ICommand PasswordChangedCommand { get; }

    public LoginControlViewModel()
    {
        tcpClient = new TCPClient();
        LoginCommand = new RelayCommand(Login);
        PasswordChangedCommand = new RelayCommand<string?>(PasswordChanged);
        ServerSettingControlViewModel.UpdateServerEvent += UpdateServer;
    }

    private void PasswordChanged(string? password)
    {
        
    }

    private async void Login()
    {
        if(UserPassword == string.Empty || UserName == string.Empty)
        {
            MessageBox.Show("用户名或密码不能为空");
            return;
        }
        // Send a chat message to the server
        JObject chat = new JObject();
        chat["cmd"] = Commands.CmdLogin.ToInt();
        chat["account"] = UserName;
        chat["password"] = UserPassword;

        if (!tcpClient.IsConnected())
        {
            await tcpClient.ConnectAsync(ServerSource.ServerIP, ServerSource.ServerPort);
            if (!tcpClient.IsConnected())
            {
                MessageBox.Show("连接服务器失败");
                return;
            }
        }
        await tcpClient.SendDataAsync(chat);
    }
    
    private void UpdateServer(object? sender, EventArgs e)
    {
        tcpClient.StopReceiving();
    }
}