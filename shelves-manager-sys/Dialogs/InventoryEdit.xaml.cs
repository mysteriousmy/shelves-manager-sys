using Microsoft.Toolkit.Mvvm.Messaging;
using System.Windows;

namespace shelves_manager_sys.Dialogs
{
    /// <summary>
    /// InventoryEdit.xaml 的交互逻辑
    /// </summary>
    public partial class InventoryEdit : Window
    {
        public InventoryEdit()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<string, string>(this, "Token4", (s, e) =>
            {
                if (e == "close")
                {
                    this.Close();
                }
            });
        }
    }
}
