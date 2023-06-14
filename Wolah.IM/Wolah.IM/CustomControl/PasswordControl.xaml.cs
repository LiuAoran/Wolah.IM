using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Wolah.IM.CustomControl
{
    /// <summary>
    /// Interaction logic for PasswordControl.xaml
    /// </summary>
    public partial class PasswordControl : UserControl,INotifyPropertyChanged
    {
        public static readonly DependencyProperty WatermarkProperty =
    DependencyProperty.Register("Watermark", typeof(string), typeof(PasswordControl), new PropertyMetadata(null));
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty PasswordTextProperty =
DependencyProperty.Register("PasswordText", typeof(string), typeof(PasswordControl), new PropertyMetadata(string.Empty));
        public string? PasswordText => Password;

        private string? _password = string.Empty;
        public string? Password
        {
            get => _password;
            set {
                _password = value;
                OnPropertyChanged();
            }
        }


        public PasswordControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            DisplayPasswordChk.IsChecked = false;
        }
    }
}
