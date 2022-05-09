using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using shelves_manager_sys.DAL;
using shelves_manager_sys.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

//货架修改窗口ViewModel
namespace shelves_manager_sys.ViewModel
{
    class ShelvesAddOrEditViewModel : ObservableObject
    {
        private Shelves _currentEntity;
        public Shelves CurrentEntity
        {
            get { return _currentEntity; }
            set
            {
                if (_currentEntity != value)
                {
                    _currentEntity = value;

                    IsChecked = _currentEntity.isOnline;

                    this.OnPropertyChanged();
                }
            }
        }

        private bool _isChecked;
        public bool IsChecked
        {
            get { return _isChecked; }
            set
            {
                if (_isChecked != value)
                {
                    _isChecked = value;
                    this.OnPropertyChanged();
                }
            }
        }

        private bool _isAdd = false;

        public bool IsAdd
        {
            get { return _isAdd; }
            set
            {
                if (_isAdd != value)
                {
                    _isAdd = value;
                    this.OnPropertyChanged();
                }
            }
        }
        public RelayCommand SaveCommand { get; set; }

        public ShelvesAddOrEditViewModel()
        {
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            if(CurrentEntity.shelveName != null && CurrentEntity.isOnline != null)
            {
                Shelves shelves = new()
                {
                    shelveName = CurrentEntity.shelveName,
                    isOnline = IsChecked
                };
                if (IsAdd)
                {
                    ShelvesDAL.Insert(shelves);
                }
                else
                {
                    shelves.shelveId = CurrentEntity.shelveId;
                    shelves.isOnline = IsChecked;
                    ShelvesDAL.Update(shelves);
                }
                WeakReferenceMessenger.Default.Send("close", "Token1");
                WeakReferenceMessenger.Default.Send("refresh", "Token1");
            }
            else
            {
                MessageBox.Show("请输入数据");
            }
         
        }
    }
}
