using Cargos_manager_sys.DAL;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using shelves_manager_sys.DAL;
using shelves_manager_sys.Dialogs;
using shelves_manager_sys.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
//搜索ViewModel
namespace shelves_manager_sys.ViewModel
{
    class SearchViewModel : ObservableObject
    {
        public SearchViewModel()
        {
            SearchResult = new RelayCommand(Search);
            MethodList = new List<string> { "查询货物在货架的位置","根据货架号查询货物", "根据货架名查询货物","根据标签编码查询货物"};
            CurrentMethod = "查询货物在货架的位置";
        }
        public string _currentMethod;
        public string CurrentMethod { get => _currentMethod; set
            {
                _currentMethod = value;
                OnPropertyChanged();
            }}
        public List<string> MethodList { get; set; }
        public string _searchWords;
        public string SearchWords { get => _searchWords; set
            {
                _searchWords = value;
                OnPropertyChanged();
            }}
        public List<MoreCargoInfo> _searchList;
        public List<MoreCargoInfo> SearchList { get => _searchList; set
            {
                _searchList = value;
                OnPropertyChanged();
            } }
        public RelayCommand SearchResult { get; set; }
        public void Search()
        {
            switch (CurrentMethod)
            {
                case "查询货物在货架的位置":
                    if (SearchWords == null)
                    {
                        MessageBox.Show("请输入货物关键词");
                    }
                    else
                    {
                        MessageBox.Show(SearchWords);
                        var item = CargosDAL.GetCargosMoreInfoByPage(SearchWords);
                        if (item == null || item.Count() == 0)
                        {
                            MessageBox.Show("没有搜到相关信息");
                        }
                        else
                        {
                            SearchList = item;
                        }
                    }
                    break;
                case "根据货架号查询货物":
                    if (SearchWords == null)
                    {
                        MessageBox.Show("请输入关键词");
                    }
                    else
                    {
                        MessageBox.Show(SearchWords);
                        var item = CargosDAL.GetCargosMoreInfoByShevNum(SearchWords);
                        if (item == null || item.Count() == 0)
                        {
                            MessageBox.Show("没有搜到相关信息");
                        }
                        else
                        {
                            SearchList = item;
                        }
                    }
                    break;
                case "根据货架名查询货物":
                    if (SearchWords == null)
                    {
                        MessageBox.Show("请输入关键词");
                    }
                    else
                    {
                        MessageBox.Show(SearchWords);
                        var item = CargosDAL.GetCargosMoreInfoByShevName(SearchWords);
                        if (item == null || item.Count() == 0)
                        {
                            MessageBox.Show("没有搜到相关信息");
                        }
                        else
                        {
                            SearchList = item;
                        }
                    }
                    break;
                case "根据标签编码查询货物":
                    if (SearchWords == null)
                    {
                        MessageBox.Show("请输入关键词");
                    }
                    else
                    {
                        MessageBox.Show(SearchWords);
                        var item = CargosDAL.GetCargosMoreInfoByTagCode(SearchWords);
                        if (item == null || item.Count() == 0)
                        {
                            MessageBox.Show("没有搜到相关信息");
                        }
                        else
                        {
                            SearchList = item;
                        }
                    }
                    break;
            }
         
        }
    }
}
