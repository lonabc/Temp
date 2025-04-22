using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfAppLogin.Model;

namespace WpfAppLogin.VM
{
    public class UserModelPageVm
    {
        public ObservableCollection<UserPageModel> Menus { get; set; }

        public UserModelPageVm()
        {
            Menus = new ObservableCollection<UserPageModel>
            {
                new UserPageModel
                {
                    Header = "家具控制",
                    Command = new RelayCommand(() => MessageBox.Show("Open clicked!"))
                },
                new UserPageModel
                {
                    Header = "检测中心",
                    Command = new RelayCommand(() => MessageBox.Show("Save clicked!"))
                },
                new UserPageModel
                {
                    Header = "退出登录",
                    Command = new RelayCommand(() => MessageBox.Show("out login clicked!"))
                },
            };
        }
    }
}
