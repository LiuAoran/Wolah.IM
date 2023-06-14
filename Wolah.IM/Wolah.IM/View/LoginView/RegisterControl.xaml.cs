using System;
using System.Reflection.Metadata;
using System.Windows;
using System.Windows.Controls;

namespace Wolah.IM.View.LoginView;

public partial class RegisterControl : UserControl
{
    public event EventHandler GoLoginEvent;
    
    public RegisterControl()
    {
        InitializeComponent();
    }

    private void GoLoginBtn_OnClick(object sender, RoutedEventArgs e)
    {
        GoLoginEvent?.Invoke(this, EventArgs.Empty);
    }
    
}