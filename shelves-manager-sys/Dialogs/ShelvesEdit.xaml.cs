using Microsoft.Toolkit.Mvvm.Messaging;
using shelves_manager_sys.ViewModel;
using System.Windows;

namespace shelves_manager_sys.Dialogs
{
    /// <summary>
    /// ShelvesEdit.xaml 的交互逻辑
    /// </summary>
    public partial class ShelvesEdit : Window
    {
        public ShelvesEdit()
        {
            InitializeComponent();
            this.DataContext = new ShelvesAddOrEditViewModel();
            WeakReferenceMessenger.Default.Register<string, string>(this, "Token1", (s, e) =>
            {
                if(e == "close")
                {
                    this.Close();
                }
            });
        }
    }
}
