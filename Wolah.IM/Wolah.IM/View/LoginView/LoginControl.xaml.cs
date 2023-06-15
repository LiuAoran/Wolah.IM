using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wolah.IM.View.LoginView
{
    /// <summary>
    /// Interaction logic for LoginControl.xaml
    /// </summary>
    public partial class LoginControl : UserControl
    {
        public event EventHandler GoRegisterEvent;
        public LoginControl()
        {
            InitializeComponent();
        }

        private void GoRegisterBtn_OnClick(object sender, RoutedEventArgs e)
        {
            GoRegisterEvent?.Invoke(this, EventArgs.Empty);
        }

        private void PasswordControl_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }
    }
}
