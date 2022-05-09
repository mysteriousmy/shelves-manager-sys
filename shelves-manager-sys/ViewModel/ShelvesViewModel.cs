using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using shelves_manager_sys.DAL;
using shelves_manager_sys.Dialogs;
using shelves_manager_sys.Entity;
using System.Collections.Generic;
using System.Windows;

//货架ViewModel
namespace shelves_manager_sys.ViewModel
{
    internal class ShelvesViewModel : ObservableObject
    {
        public ShelvesViewModel()
        {
            getShelvesPageAsync("1");
            CurrentPage = 1;
            GetShelvesByPage = new RelayCommand<string>(t => getShelvesPageAsync(t));
            DeleteCommand = new RelayCommand<string>(t => deleteCommand(t));
            StartCommand = new RelayCommand<string> (t => startCommand(t));
            StopCommand = new RelayCommand<string>(t => stopCommand(t));
            DoPageChange = new RelayCommand<string>(t => doPageChange(t));
            AddShevel = new RelayCommand(addShevel);
            UpdateShevel = new RelayCommand<string>(t => updateShevel(t));
        }
        public RelayCommand<string> GetShelvesByPage { get; set; }
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> StopCommand { get; set; }
        public RelayCommand<string> StartCommand { get; set; }
        public RelayCommand<string> DoPageChange { get; set; }
        public RelayCommand AddShevel { get; set; }
        public RelayCommand<string> UpdateShevel { get; set; }
        public List<Shelves> _shelves;
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
        public List<Shelves> Shelves
        {
            get => _shelves;
            set
            {
                _shelves = value;
                OnPropertyChanged();
            }
        }
        public void addShevel()
        {
            ShelvesEdit shelvesEdit = new();
            shelvesEdit.Show();
            var addOrEditViewModel = (shelvesEdit.DataContext as ShelvesAddOrEditViewModel);
            addOrEditViewModel.CurrentEntity = new();
            addOrEditViewModel.IsAdd = true;
            getShelvesPageAsync(CurrentPage.ToString());
        }
        public  void updateShevel(string id)
        {
            ShelvesEdit shelvesEdit = new();
            shelvesEdit.Show();
            var addOrEditViewModel = (shelvesEdit.DataContext as ShelvesAddOrEditViewModel);
            addOrEditViewModel.CurrentEntity = ShelvesDAL.findyById(int.Parse(id));
            addOrEditViewModel.IsAdd = false;
            getShelvesPageAsync(CurrentPage.ToString());
        }
        public async void getShelvesPageAsync(string pageindex)
        {
            int index = int.Parse(pageindex);
            var item = await ShelvesDAL.GetShelvesByPage(index, 7);
            Shelves = item.ShelveList;
            PageCount = item.count;
        }
        private void doPageChange(string flag)
        {
            switch (flag)
            {
                case "Up":
                    if(CurrentPage == 1)
                    {
                        MessageBox.Show("已经是第一页了，无法继续翻动");
                    }
                    else
                    {
                        CurrentPage = CurrentPage - 1;
                    }
                    break;
                case "Next":
                    if(CurrentPage == PageCount)
                    {
                        MessageBox.Show("已经是最后一页了，无法继续翻动");
                    }
                    else
                    {
                        CurrentPage = CurrentPage + 1;
                    }
                    break;
            }
            getShelvesPageAsync(CurrentPage.ToString());
        }
        private void startCommand(string id)
        {
            MessageBoxResult dr = MessageBox.Show("是否在启用该货架？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                ShelvesDAL.StartOrStopShelves(int.Parse(id), true);
                getShelvesPageAsync(CurrentPage.ToString());
            }
        }
        private void stopCommand(string id)
        {
            MessageBoxResult dr = MessageBox.Show("是否在停用该货架？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                ShelvesDAL.StartOrStopShelves(int.Parse(id), false);
                getShelvesPageAsync(CurrentPage.ToString());
            }
        }
        private void deleteCommand(string id)
        {
            MessageBoxResult dr = MessageBox.Show("是否在删除该货架？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
             {
                int ids = int.Parse(id);
                ShelvesDAL.DeleteDataById(ids);
                getShelvesPageAsync(CurrentPage.ToString());
            }
            
        }
    }
}
