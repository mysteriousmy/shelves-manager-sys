using Microsoft.Toolkit.Mvvm.Messaging;
using shelves_manager_sys.Entity;
using shelves_manager_sys.ViewModel;
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
using System.Windows.Shapes;

namespace shelves_manager_sys
{
    /// <summary>
    /// 登录页面的交互逻辑代码
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            LoginViewModel viewModel = new LoginViewModel();
            this.DataContext = viewModel;
            WeakReferenceMessenger.Default.Register<Admins, string>(this, "UserToken", (s, e) =>
            {

                    MainWindow mainWindow = new MainWindow();
                    mainWindow.Show();
                     var mainViewModel = (mainWindow.DataContext as MainViewModel);
                    mainViewModel.AdminName = e.adminName;
                    Window window = Window.GetWindow(this);//关闭父窗体
                    window.Close();
            });
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void window_close(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void window_narrow(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}
