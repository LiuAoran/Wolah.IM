using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Wolah.IM.CustomControl;
using Wolah.IM.View.LoginView;
using Wolah.IM.ViewModel;

namespace Wolah.IM.View
{
    /// <summary>
    /// LoginWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoginWindow : Window
    {
        private RegisterControl registerControl = new RegisterControl();
        private LoginControl loginControl = new LoginControl();

        public LoginWindow()
        {
            InitializeComponent();
            
            loginControl.Loaded+= LoginControlOnLoaded;
            loginControl.Unloaded+= LoginControlOnUnloaded;
            registerControl.Loaded+= RegisterControlOnLoaded;
            registerControl.Unloaded+= RegisterControlOnUnloaded;

            LonginContentBorder.Child = loginControl;
        }

        private void LoginGd_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void LoginControlOnLoaded(object sender, RoutedEventArgs e)
        {
            loginControl.GoRegisterEvent += GoRegisterEvent;
        }
        
        private void LoginControlOnUnloaded(object sender, RoutedEventArgs e)
        {
            loginControl.GoRegisterEvent -= GoRegisterEvent;
        }
        
        private void RegisterControlOnLoaded(object sender, RoutedEventArgs e)
        {

            registerControl.GoLoginEvent += GoLoginEvent;
        }
        
        private void RegisterControlOnUnloaded(object sender, RoutedEventArgs e)
        {
            registerControl.GoLoginEvent -= GoLoginEvent;
        }
        
        private void GoLoginEvent(object? sender, EventArgs e)
        {
            GoLoginAnimation();
        }
        private void GoRegisterEvent(object? sender, EventArgs e)
        {
            GoRegisterAnimation();
        }
        //Go Login Animation:RegisterControl Slide out to right and then LoginControl Slide in from left
        private void GoLoginAnimation()
        {
            var registerControlAnimation = new ThicknessAnimation
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(Width * 0.4, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };

            var loginControlAnimation = new ThicknessAnimation
            {
                From = new Thickness(-Width, 0, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            
            var registerControlOpacityAnimation = new DoubleAnimation
            {
                From = 0.9,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            
            var loginControlOpacityAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.5)
            };

            registerControlOpacityAnimation.Completed += (s, e) =>
            {
                LonginContentBorder.Child = loginControl;
                loginControl.BeginAnimation(MarginProperty, loginControlAnimation);
                loginControl.BeginAnimation(OpacityProperty, loginControlOpacityAnimation);
            };
            registerControl.BeginAnimation(MarginProperty, registerControlAnimation);
            registerControl.BeginAnimation(OpacityProperty, registerControlOpacityAnimation);
        }
        private void GoRegisterAnimation()
        {
            var loginControlAnimation = new ThicknessAnimation
            {
                From = new Thickness(0, 0, 0, 0),
                To = new Thickness(-Width * 0.4, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            
            var registerControlAnimation = new ThicknessAnimation
            {
                From = new Thickness(Width, 0, 0, 0),
                To = new Thickness(0, 0, 0, 0),
                Duration = TimeSpan.FromSeconds(0.5),
                EasingFunction = new CubicEase { EasingMode = EasingMode.EaseInOut }
            };
            
            var loginControlOpacityAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.3)
            };
            
            var registerControlOpacityAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = TimeSpan.FromSeconds(0.5)
            };
            loginControlOpacityAnimation.Completed += (s, e) =>
            {
                LonginContentBorder.Child = registerControl;
                registerControl.BeginAnimation(MarginProperty, registerControlAnimation);
                registerControl.BeginAnimation(OpacityProperty, registerControlOpacityAnimation);
            };
            loginControl.BeginAnimation(MarginProperty, loginControlAnimation);
            loginControl.BeginAnimation(OpacityProperty, loginControlOpacityAnimation);
            
        }
    }
}
