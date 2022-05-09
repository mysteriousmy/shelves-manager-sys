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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace shelves_manager_sys.UserControls
{
    /// <summary>
    /// SearchManager.xaml 的交互逻辑
    /// </summary>
    public partial class SearchManager : UserControl
    {
        public SearchManager()
        {
            InitializeComponent();
            SearchViewModel inventoryView = new SearchViewModel();
            this.DataContext = inventoryView;
  
        }
    }
}
