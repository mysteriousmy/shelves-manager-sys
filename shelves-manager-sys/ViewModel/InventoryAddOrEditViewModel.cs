using Cargos_manager_sys.DAL;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Microsoft.Toolkit.Mvvm.Messaging;
using shelves_manager_sys.DAL;
using shelves_manager_sys.Entity;
using shelves_manager_sys.Entity.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace shelves_manager_sys.ViewModel
{
    class InventoryAddOrEditViewModel : ObservableObject
    {
        public PutCargos _currentEntity { get; set; }

        public List<Cargos> CargosList {get;set;}

        public List<Tags> TagsList {get;set;}

        public List<Shelves> ShevlesList { get;set;}
        public int _shelves;
        public int Shevles
        {
            get => _shelves;
            set
            {
                _shelves = value;
                OnPropertyChanged();
            }
        }
        public List<int> LayerList { get; set; }
        public int _layer;
        public int Layer 
        {
            get => _layer; 
            set
        {
                _layer = value;
                OnPropertyChanged();
         }}

        public PutCargos CurrentEntity
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

        public InventoryAddOrEditViewModel()
        {
            //SaveCommand = new RelayCommand(Save);
            initComboxData();
            SaveCommand = new RelayCommand(Save);
        }
        private void initComboxData()
        {
            using (ShelvesDbContext db = new ShelvesDbContext())
            {
                List<Cargos> cargos = db.cargos.ToList();
                List<Tags> tags = db.tags.ToList();
                List<Shelves> shelves = db.shelves.Where(x => x.isOnline == true).ToList();
                CargosList = cargos;
                TagsList = tags;
                ShevlesList = shelves;
            }
            List<int> layer= new List<int> { 1, 2, 3, 4 };
            LayerList = layer;
        }

        public static string MidStrEx(string sourse, string startstr, string endstr)
        {
            string result = string.Empty;
            int startindex, endindex;
            try
            {
                startindex = sourse.IndexOf(startstr);
                if (startindex == -1)
                    return result;
                string tmpstr = sourse.Substring(startindex + startstr.Length);
                endindex = tmpstr.IndexOf(endstr);
                if (endindex == -1)
                    return result;
                result = tmpstr.Remove(endindex);
            }
            catch (Exception ex)
            {
            }
            return result;
        }
        private void Save()
        {
            if (CurrentEntity.cargoId != 0 && CurrentEntity.tagId != 0 && Shevles != 0 && Layer != 0)
            {
                PutCargosDto putCargosDto = new PutCargosDto()
                {
                    cargoId = CurrentEntity.cargoId,
                    tagId = CurrentEntity.tagId,
                    shelveId = Shevles,
                    layer = Layer,
                };
                if (IsAdd)
                {
                    InventoryDAL.addPutCargos(putCargosDto);
                }
                else
                {
                    int id = CurrentEntity.putCargoId;
                    InventoryDAL.UpdatePutCargos(id, putCargosDto);
                }
            }
            else
            {
                MessageBox.Show("请选择数据");
            }
            WeakReferenceMessenger.Default.Send("close", "Token4");
            WeakReferenceMessenger.Default.Send("refresh", "Token4");
        }
    }
}
