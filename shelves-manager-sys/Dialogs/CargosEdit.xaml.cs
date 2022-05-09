using Microsoft.Toolkit.Mvvm.Messaging;
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

namespace shelves_manager_sys.Dialogs
{
    /// <summary>
    /// ShelvesEdit.xaml 的交互逻辑
    /// </summary>
    public partial class CargosEdit : Window
    {
        public CargosEdit()
        {
            InitializeComponent();
            this.DataContext = new CargosAddOrEditViewModel();
            WeakReferenceMessenger.Default.Register<string, string>(this, "Token3", (s, e) =>
            {
                if(e == "close")
                {
                    this.Close();
                }
            });
        }

    }
}
