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
using Cargos_manager_sys.DAL;

namespace shelves_manager_sys.ViewModel
{
    class CargosAddOrEditViewModel : ObservableObject
    {
        private Cargos _currentEntity;
        public Cargos CurrentEntity
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

        public CargosAddOrEditViewModel()
        {
            SaveCommand = new RelayCommand(Save);
        }

        private void Save()
        {
            if(CurrentEntity.cargoName != null)
            {
                Cargos cargos = new()
                {
                    cargoId = CurrentEntity.cargoId,
                    cargoName = CurrentEntity.cargoName,
                };
                if (IsAdd)
                {
                    CargosDAL.Insert(cargos);
                }
                else
                {
                    cargos.cargoId = CurrentEntity.cargoId;

                    CargosDAL.Update(cargos);
                }
                WeakReferenceMessenger.Default.Send("close", "Token3");
                WeakReferenceMessenger.Default.Send("refresh", "Token3");
            }
            else
            {
                MessageBox.Show("请输入数据");
            }
         
        }
    }
}
