using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Effects;
using WpfAppLogin.Model;
using WpfAppLogin.Model.PartModel;
using WpfSample;



namespace WpfAppLogin.VM
{
    public class UserModelPageVm : INotifyPropertyChanged
    {
        public ObservableCollection<UserPageModel> Menus { get; set; }
        public ObservableCollection<ListViewModel> listViewModels { get; set; }
        private UserPageLiveCharts _userPageLiveCharts;

        public UserPageLiveCharts UserPageLiveCharts { get { 
                return _userPageLiveCharts;
            }
            set
            {
                _userPageLiveCharts = value;
                OnPropertyChanged(nameof(UserPageLiveCharts));
            } }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

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

            listViewModels = new ObservableCollection<ListViewModel>
            {
                new ListViewModel
                {
                    index = "good",
                    name = "灯光控制",
                    Remark=DateTime.Now+"",
                },
                new ListViewModel
                {
                   index = "good",
                    name = "窗帘控制",
                    Remark=DateTime.Now+"",
                },
                new ListViewModel
                {
                   index = "good",
                    name = "空调控制",
                    Remark=DateTime.Now+"",
                },
            };
            UserPageLiveCharts = new UserPageLiveCharts();


        }

     
    }
}
