using Microsoft.Toolkit.Mvvm.Messaging;
using shelves_manager_sys.Entity;
using shelves_manager_sys.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace shelves_manager_sys
{
    /// <summary>
    /// 主窗口的交互逻辑代码
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel viewModel = new MainViewModel();
            this.DataContext = viewModel;
            home.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
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
