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

namespace shelves_manager_sys.ViewModel
{
    class LoginViewModel : ObservableObject
    {
        public LoginViewModel()
        {
            LoginCommand = new RelayCommand(Login);
        }
        public RelayCommand LoginCommand { get; set; }
        private string adminName;
        private string adminPassword;
        private bool isAutoLogin;
        public bool IsAutoLogin
        {
            get => isAutoLogin;
            set
            {
                isAutoLogin = value;
                OnPropertyChanged();
            }
        }
        public string AdminName
        {
            get => adminName;
            set
            {
                adminName = value;
                OnPropertyChanged();
            }
        }
        public string AdminPassword
        {
            get => adminPassword;
            set
            {
                adminPassword = value;
                OnPropertyChanged();
            }
        }
        public void Login()
        {
            if(adminName == null || adminPassword == null)
            {
                MessageBox.Show("用户名或密码不能为空！");
            }
            else
            {
                Admins admin = AdminsDAL.LoginCheck(adminName, adminPassword);
                if (admin == null)
                {
                    MessageBox.Show(isAutoLogin.ToString());
                    MessageBox.Show("用户名或密码错误！");
                }
                else
                {
                    MessageBox.Show("登录成功！");
                    WeakReferenceMessenger.Default.Send(admin, "UserToken");
                }
            }
        }
    }
}
