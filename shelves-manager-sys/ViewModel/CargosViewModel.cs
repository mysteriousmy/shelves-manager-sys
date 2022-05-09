using Cargos_manager_sys.DAL;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using shelves_manager_sys.Dialogs;
using shelves_manager_sys.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Tags_manager_sys.DAL;

namespace shelves_manager_sys.ViewModel
{
    class CargosViewModel : ObservableObject
    {
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> DoPageChange { get; set; }
        public RelayCommand AddCargos { get; set; }
        public RelayCommand<string> UpdateCargos { get; set; }
        public CargosViewModel()
        {
            getCargosPageAsync("1");
            CurrentPage = 1;
            DeleteCommand = new RelayCommand<string>(t => deleteCommand(t));
            DoPageChange = new RelayCommand<string>(t => doPageChange(t));
            AddCargos = new RelayCommand(addCargos);
            UpdateCargos = new RelayCommand<string>(t => updateCargos(t));
        }
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
        public List<Cargos> _tags;
        public List<Cargos> Cargos
        {
            get => _tags;
            set
            {
                _tags = value;
                OnPropertyChanged();
            }
        }
        public async void getCargosPageAsync(string pageindex)
        {
            int index = int.Parse(pageindex);
            var item = await CargosDAL.GetCargosByPage(index, 7);
            Cargos = item.TagList;
            PageCount = item.count;
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
            getCargosPageAsync(CurrentPage.ToString());
        }
        public void addCargos()
        {
            CargosEdit tagsEdit = new();
            tagsEdit.Show();
            var addOrEditViewModel = (tagsEdit.DataContext as CargosAddOrEditViewModel);
            addOrEditViewModel.CurrentEntity = new();
            addOrEditViewModel.IsAdd = true;
            getCargosPageAsync(CurrentPage.ToString());
        }
        public void updateCargos(string id)
        {
            CargosEdit tagsEdit = new();
            tagsEdit.Show();
            var addOrEditViewModel = (tagsEdit.DataContext as CargosAddOrEditViewModel);
            addOrEditViewModel.CurrentEntity =CargosDAL.findyById(int.Parse(id));
            addOrEditViewModel.IsAdd = false;
            getCargosPageAsync(CurrentPage.ToString());
        }
        private void deleteCommand(string id)
        {
            MessageBoxResult dr = MessageBox.Show("是否在删除该货物？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                int ids = int.Parse(id);
                CargosDAL.DeleteDataById(ids);
                getCargosPageAsync(CurrentPage.ToString());
            }

        }
    }
    
}
