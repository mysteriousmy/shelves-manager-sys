using shelves_manager_sys.Entity.Context;
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
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : UserControl
    {
        public HomePage()
        {
            InitializeComponent();
            ShelvesDbContext db = new ShelvesDbContext();
            int shev = db.shelves.Count(x => x.isOnline);
            int put = db.putCargos.Count();
            int outS = db.outCargos.Count();
            ShevNum.Number = shev.ToString();
            PutNum.Number = put.ToString();
            OutNum.Number = outS.ToString();
        }
    }
}
