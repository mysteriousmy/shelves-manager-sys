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

//仓库管理ViewModel
namespace shelves_manager_sys.ViewModel
{
    class InventoryViewModel : ObservableObject
    {
        public InventoryViewModel()
        {
            CurrentPage = 1;
            getInventoryPageAsync(CurrentPage.ToString());
            DoPageChange = new RelayCommand<string>(t => doPageChange(t));
            AddPutCargos = new RelayCommand(addPutCargos);
            UpdatePutCargos = new RelayCommand<string>(t => updatePutCargos(t));
            OutputCargos = new RelayCommand<string>(t => outputCargos(t));
            DeleteCommand = new RelayCommand<string>(t => deleteCommand(t));
            CheckTable = new RelayCommand<string>(t => checkTable(t));
        }
        public Boolean _nowTable = true;
        public Boolean NowTable
        {
            get => _nowTable;
            set
            {
                _nowTable = value;
                OnPropertyChanged();
            }
        }
        public RelayCommand<string> CheckTable { get; set; }
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> DoPageChange { get; set; }
        public RelayCommand<string> OutputCargos { get; set; }
        public RelayCommand AddPutCargos { get; set; }
        public RelayCommand<string> UpdatePutCargos { get; set; }

        public List<OutCargosList> _outCargoList;
        public List<OutCargosList> OutCargosList
        {
            get => _outCargoList;
            set
            {
                _outCargoList = value;
                OnPropertyChanged();
            }
        }
        public List<PutCargosList> _putCargoList;
        public List<PutCargosList> PutCargoList 
        { 
            get => _putCargoList; 
            set
        {
                _putCargoList = value;
                OnPropertyChanged();
        }}
        public int _currentPage;
        public long _pageCount;
        public long PageCount
        {
            get => _pageCount;
            set
            {
                _pageCount = value;
                OnPropertyChanged();
            }
        }
        public int CurrentPage
        {
            get => _currentPage;
            set
            {
                _currentPage = value;
                OnPropertyChanged();
            }
        }
        public void checkTable(string type)
        {
            switch (type)
            {
                case "put":
                    NowTable = true;
                    getInventoryPageAsync("1");
                    break;
                case "out":
                    NowTable = false;
                    getInventoryOutPageAsync("1");
                    break;
            }
        }
        public async void getInventoryPageAsync(string pageindex)
        {
            int index = int.Parse(pageindex);
            var item = await InventoryDAL.getBasePageQueryData(index, 7);
            PutCargoList = item.cargosLists;
            PageCount = item.count;
        }
        public async void getInventoryOutPageAsync(string pageindex)
        {
            int index = int.Parse(pageindex);
            var item = await InventoryDAL.getBasePageQueryOutData(index, 7);
            OutCargosList = item.cargosLists;
            PageCount = item.count;
            CurrentPage = 1;
        }
        public void addPutCargos()
        {
            InventoryEdit inventory = new();
            var addOrEditViewModel = (inventory.DataContext as InventoryAddOrEditViewModel);
            addOrEditViewModel.CurrentEntity = new();
            addOrEditViewModel.IsAdd = true;
            inventory.Show();
        }
        public void updatePutCargos(string id)
        {
            ShelvesDbContext db = new ShelvesDbContext();
            var re = db.putCargos.Find(int.Parse(id));
            InventoryEdit inventory = new();
            var addOrEditViewModel = (inventory.DataContext as InventoryAddOrEditViewModel);
            addOrEditViewModel.CurrentEntity = re;
            addOrEditViewModel.IsAdd = false;
            inventory.Show();
        }
        public void deleteCommand(string id)
        {
            MessageBoxResult dr = MessageBox.Show("确定要删除？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                ShelvesDbContext db = new ShelvesDbContext();
                var item = db.putCargos.First(x => x.putCargoId == int.Parse(id));
                var shev = db.shelvesCargos.First(x => x.cargoId == item.cargoId);
                PutCargosDto dto = new()
                {
                    cargoId = item.cargoId,
                    shelveId = shev.shelveId,
                    tagId = item.tagId,
                };
                InventoryDAL.DeletePutCargos(int.Parse(id), dto);
                getInventoryPageAsync(CurrentPage.ToString());
            }
            
        }
        public void outputCargos(string id)
        {
            MessageBoxResult dr = MessageBox.Show("确定要出库？这样会从入库中删除", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                ShelvesDbContext db = new ShelvesDbContext();
                var item = db.putCargos.First(x => x.putCargoId == int.Parse(id));
                var shev = db.shelvesCargos.First(x => x.cargoId == item.cargoId);
                PutCargosDto dto = new()
                {
                    cargoId = item.cargoId,
                    shelveId = shev.shelveId,
                    tagId = item.tagId,
                };
                InventoryDAL.OutPutCargos(int.Parse(id), dto);
                getInventoryPageAsync(CurrentPage.ToString());
            }
        }
        private void doPageChange(string flag)
        {

            switch (flag)
            {
                case "Up":
                    if (CurrentPage == 1)
                    {
                        MessageBox.Show("已经是第一页了，无法继续翻动");
                    }
                    else
                    {
                        CurrentPage = CurrentPage - 1;
                    }
                    break;
                case "Next":
                    if (CurrentPage == PageCount)
                    {
                        MessageBox.Show("已经是最后一页了，无法继续翻动");
                    }
                    else
                    {
                        CurrentPage = CurrentPage + 1;
                    }
                    break;
            }
            getInventoryPageAsync(CurrentPage.ToString());
        }
    }
}
