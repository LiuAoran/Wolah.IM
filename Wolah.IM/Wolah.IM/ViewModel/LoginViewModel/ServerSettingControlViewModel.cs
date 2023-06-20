using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using Wolah.IM.Network;
using Wolah.IM.Private;

namespace Wolah.IM.ViewModel.LoginViewModel;

public class ServerSettingControlViewModel : INotifyPropertyChanged
{
    private string? _serverIP;
    public string? ServerIP
    {
        get => _serverIP;
        set
        {
            _serverIP = value;
            OnPropertyChanged();
        }
    }
    
    private string? _serverPort;
    public string? ServerPort
    {
        get => _serverPort;
        set
        {
            _serverPort = value;
            OnPropertyChanged();
        }
    }
    
    private string? _serverKey;
    public string? ServerKey
    {
        get => _serverKey;
        set
        {
            _serverKey = value;
            OnPropertyChanged();
        }
    }
    
    private string? _responseText;
    public string? ResponseText
    {
        get => _responseText;
        set
        {
            _responseText = value;
            OnPropertyChanged();
        }
    }

    public static event EventHandler UpdateServerEvent;

    #region Command Declear

    public ICommand PingServerCommand { get;  }
    public ICommand ConfirmCommand { get;  }

    #endregion
    
    public ServerSettingControlViewModel()
    {
        PingServerCommand = new RelayCommand(PingServer);
        ConfirmCommand = new RelayCommand(Confirm);
    }
    
    private async void PingServer()
    {
        if (string.IsNullOrEmpty(ServerIP) || string.IsNullOrEmpty(ServerPort))
        {
            ResponseText = "IP或端口不能为空";
            return;
        }
        if (!int.TryParse(ServerPort, out var port))
        {
            ResponseText = "端口格式不正确";
            return;
        }
        if (await TCPClient.TestServerAsync(ServerIP, port))
        {
            ResponseText = "连接成功";
        }
        else
        {
            ResponseText = "连接失败";
        }
    }
    
    public void Confirm()
    {
        if (string.IsNullOrEmpty(ServerIP) || string.IsNullOrEmpty(ServerPort))
        {
            ResponseText = "IP或端口不能为空";
            return;
        }
        if (!int.TryParse(ServerPort, out var port))
        {
            ResponseText = "端口格式不正确";
            return;
        }
        try
        {
            ServerSource.ServerIP = IPAddress.Parse(ServerIP);
        }
        catch (Exception)
        {
            ResponseText = "IP格式不正确";
            return;
        }
        
        ServerSource.ServerKey = ServerKey;
        ResponseText = "保存成功";
        UpdateServerEvent?.Invoke(this, EventArgs.Empty);
    }

    public event PropertyChangedEventHandler? PropertyChanged;
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}