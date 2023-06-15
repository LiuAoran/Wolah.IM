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

        #region - 用于绑定ViewModel部分 -

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(PasswordControl), new PropertyMetadata(default(ICommand)));

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(PasswordControl), new PropertyMetadata(default(object)));

        public IInputElement CommandTarget { get; set; }

        #endregion

        public static readonly RoutedEvent PasswordChangedEvent =
        EventManager.RegisterRoutedEvent("PasswordChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PasswordControl));

        public event RoutedEventHandler PasswordChanged
        {
            add => AddHandler(PasswordChangedEvent, value);
            remove => RemoveHandler(PasswordChangedEvent, value);
        }

        private string? _password = string.Empty;
        public string? Password
        {
            get => _password;
            set {
                _password = value;
                OnPropertyChanged();

                if(this.Command != null)
                {
                    this.Command.Execute(CommandParameter);
                    RoutedEventArgs args = new RoutedEventArgs(PasswordControl.PasswordChangedEvent, this);
                    RaiseEvent(args);
                } 
            }
        }

        public PasswordControl()
        {
            InitializeComponent();
            Grid.DataContext = this;
        }

        
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Password == string.Empty)
            {
                DisplayPasswordChk.IsChecked = false;
            }
        }
    }
}
