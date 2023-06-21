using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using Wolah.IM.Helper;
using Wolah.IM.Network;
using Wolah.IM.Private;
using Wolah.IM.View;

namespace Wolah.IM.ViewModel.LoginViewModel;

public class LoginControlViewModel:INotifyPropertyChanged
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
    private string _receiveMessage = string.Empty;
    public string ReceiveMessage
    {
        get => _receiveMessage;
        set => _receiveMessage = value;
    }
    private string _loginMessage = string.Empty;
    public string LoginMessage
    {
        get => _loginMessage;
        set
        {
            _loginMessage = value; 
            if(_loginMessage != "登录成功")
            {
                LoginMessageColor = "#FF0000";
            }
            OnPropertyChanged();
        }
    }
    private string _loginMessageColor = "#000000";
    public string LoginMessageColor
    {
        get => _loginMessageColor;
        set
        {
            _loginMessageColor = value;
            OnPropertyChanged();
        }
    }

    public ICommand LoginCommand { get; } 

    public LoginControlViewModel()
    {
        tcpClient = new TCPClient();
        LoginCommand = new RelayCommand(Login);
        ServerSettingControlViewModel.UpdateServerEvent += UpdateServer;
        TCPClient.LoginControlEvent += TcpClientOnLoginControlEvent;
        
    }
    
    private async void Login()
    {
        #if DEBUG
        UserName = "10001";
        UserPassword = "123";
        #endif
        if(UserPassword == string.Empty || UserName == string.Empty)
        {
            LoginMessage = "用户名或密码不能为空";
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
                LoginMessage = "连接服务器失败";
                return;
            }
        }
        await tcpClient.SendDataAsync(chat);

        var recvTask =  tcpClient.StartReceivingAsync();
        // Wait for the server to respond in 5 seconds
        if (await Task.WhenAny(recvTask, Task.Delay(5000)) != recvTask)
        {
            LoginMessage = "登录超时";
        }
    }
    
    private void TcpClientOnLoginControlEvent(object? sender, JObject e)
    {
        if (e["cmd"].ToObject<int>() == Commands.CmdLogin.ToInt())
        {
            if (e["res"].ToObject<string>() == "yes")
            {
                LoginMessage = "登录成功";
                Application.Current.Dispatcher.BeginInvoke(new Action(delegate
                {
                    var window = Application.Current.MainWindow;
                    window?.Close();
                    var mainWindow = new MainWindow();
                    mainWindow.Show();
                }));
                TCPClient.LoginControlEvent -= TcpClientOnLoginControlEvent;
            }
            else
            {
                var err = e["err"].ToObject<string>();
                LoginMessage = err;
            }
        }
    }
    
    private void UpdateServer(object? sender, EventArgs e)
    {
        if(tcpClient.IsConnected())
            tcpClient.StopReceiving();
    }
    
    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}