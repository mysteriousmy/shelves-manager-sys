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
using Tags_manager_sys.DAL;

namespace shelves_manager_sys.ViewModel
{
    class TagsAddOrEditViewModel : ObservableObject
    {
        private Tags _currentEntity;
        public Tags CurrentEntity
        {
            get { return _currentEntity; }
            set
            {
                if (_currentEntity != value)
                {
                    _currentEntity = value;

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

        public TagsAddOrEditViewModel()
        {
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            if(CurrentEntity.tagCode != null)
            {
                Tags tags = new()
                {
                    tagCode = CurrentEntity.tagCode,
                };
                if (IsAdd)
                {
                    TagsDAL.Insert(tags);
                }
                else
                {
                    tags.tgaId = CurrentEntity.tgaId;

                    TagsDAL.Update(tags);
                }
                WeakReferenceMessenger.Default.Send("close", "Token2");
                WeakReferenceMessenger.Default.Send("refresh", "Token2");
            }
            else
            {
                MessageBox.Show("请输入数据");
            }
         
        }
    }
}
