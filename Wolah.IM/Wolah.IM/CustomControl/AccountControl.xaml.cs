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

namespace Wolah.IM.CustomControl
{
    /// <summary>
    /// Interaction logic for AccountControl.xaml
    /// </summary>
    public partial class AccountControl : UserControl
    {
        public static readonly DependencyProperty WatermarkProperty = 
            DependencyProperty.Register("Watermark", typeof(string), typeof(AccountControl), new PropertyMetadata(null));
        public string Watermark
        {
            get { return (string)GetValue(WatermarkProperty); }
            set { SetValue(WatermarkProperty, value); }
        }

        public static readonly DependencyProperty TextProperty =
    DependencyProperty.Register("Text", typeof(string), typeof(AccountControl), new PropertyMetadata(null));
        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public AccountControl()
        {
            InitializeComponent();
        }
    }
}
