using System;
using System.Windows;
using System.Windows.Controls;

namespace Wolah.IM.View.LoginView;

public partial class ServerSettingControl : UserControl
{
    public event EventHandler CancelServerSettingEvent;
    public event EventHandler ConfirmServerSettingEvent; 
    public ServerSettingControl()
    {
        InitializeComponent();
    }

    private void CancelBtn_OnClick(object sender, RoutedEventArgs e)
    {
        CancelServerSettingEvent?.Invoke(this, EventArgs.Empty);
    }

    private void ConfirmBtn_OnClick(object sender, RoutedEventArgs e)
    {
        ConfirmServerSettingEvent?.Invoke(this, EventArgs.Empty);
    }
}