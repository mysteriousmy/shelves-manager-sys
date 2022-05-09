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
    class TagsViewModel : ObservableObject
    {
        public RelayCommand<string> DeleteCommand { get; set; }
        public RelayCommand<string> DoPageChange { get; set; }
        public RelayCommand AddTags { get; set; }
        public RelayCommand<string> UpdateTags { get; set; }
        public TagsViewModel()
        {
            getTagsPageAsync("1");
            CurrentPage = 1;
            DeleteCommand = new RelayCommand<string>(t => deleteCommand(t));
            DoPageChange = new RelayCommand<string>(t => doPageChange(t));
            AddTags = new RelayCommand(addTags);
            UpdateTags = new RelayCommand<string>(t => updateTags(t));
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
        public List<Tags> _tags;
        public List<Tags> Tags
        {
            get => _tags;
            set
            {
                _tags = value;
                OnPropertyChanged();
            }
        }
        public async void getTagsPageAsync(string pageindex)
        {
            int index = int.Parse(pageindex);
            var item = await TagsDAL.GetTagsByPage(index, 7);
            Tags = item.TagList;
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
            getTagsPageAsync(CurrentPage.ToString());
        }
        public void addTags()
        {
            TagsEdit tagsEdit = new();
            tagsEdit.Show();
            var addOrEditViewModel = (tagsEdit.DataContext as TagsAddOrEditViewModel);
            addOrEditViewModel.CurrentEntity = new();
            addOrEditViewModel.IsAdd = true;
            getTagsPageAsync(CurrentPage.ToString());
        }
        public void updateTags(string id)
        {
            TagsEdit tagsEdit = new();
            tagsEdit.Show();
            var addOrEditViewModel = (tagsEdit.DataContext as TagsAddOrEditViewModel);
            addOrEditViewModel.CurrentEntity =TagsDAL.findyById(int.Parse(id));
            addOrEditViewModel.IsAdd = false;
            getTagsPageAsync(CurrentPage.ToString());
        }
        private void deleteCommand(string id)
        {
            MessageBoxResult dr = MessageBox.Show("是否在删除该标签？", "提示", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (dr == MessageBoxResult.OK)
            {
                int ids = int.Parse(id);
                TagsDAL.DeleteDataById(ids);
                getTagsPageAsync(CurrentPage.ToString());
            }

        }
    }
    
}
