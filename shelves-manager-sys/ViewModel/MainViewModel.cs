using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using shelves_manager_sys.UserControls;
using System.Windows;

//主窗口ViewModel
namespace shelves_manager_sys.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public MainViewModel()
        {
            Page = new HomePage();
            SwitchPage = new RelayCommand<string>(t => switch_page(t));
        }
        public string adminName;
        public string AdminName
        {
            get => adminName;
            set
            {
                adminName = value;
                OnPropertyChanged();
            }
        }
        public string _title;
        public string Title { get => _title; set
            {
                _title = value;
                OnPropertyChanged();
            }}

        public RelayCommand<string> SwitchPage { get; set; }
        private object _page;
        public object Page
        {
            get => _page;
            set
            {
                _page = value;
                OnPropertyChanged();
            }
        }
        private void switch_page(string menu)
        {
            switch (menu)
            {
                case "Home":
                    Page = new HomePage();
                    Title = "智能货架管理系统 - 主页";
                    break;
                case "Shelves":
                    Page = new ShelvesManager();
                    Title = "智能货架管理系统 - 货架管理";
                    break;
                case "Tags":
                    Page = new TagsManager();
                    Title = "智能货架管理系统 - 标签管理";
                    break;
                case "Cargos":
                    Page = new CargosManager();
                    Title = "智能货架管理系统 - 物品编码";
                    break;
                case "Inventory":
                    Page = new InventoryManager();
                    Title = "智能货架管理系统 - 库存管理";
                    break;
                case "Search":
                    Page = new SearchManager();
                    Title = "智能货架管理系统 - 数据查询";
                    break;
                case "Exit":
                    MessageBoxResult dr = MessageBox.Show("确定要退出系统？这样下次将不会自动登录！", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
                    if (dr == MessageBoxResult.OK)
                    {
                        Application.Current.Shutdown();
                    }
                    break;
            }
        }
    }
}
