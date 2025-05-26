using GalaSoft.MvvmLight.Command;
using Microsoft.Extensions.DependencyInjection;
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
using WpfAppLogin.Navigation;
using WpfSample;



namespace WpfAppLogin.VM
{
    public class UserModelPageVm : INotifyPropertyChanged
    {
        private readonly Window _currentWindow;
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

        public UserModelPageVm(Window currentWindow)
        {
            _currentWindow = currentWindow;
            Menus = new ObservableCollection<UserPageModel>
            {
                new UserPageModel
                {
                    Header = "用户中心",
                    Command = new RelayCommand(()=>{MessageBox.Show("已跳转到对应页面"); }),
                    IsSelected = false // 默认不选中
                },
                new UserPageModel
                {
                    Header = "检测中心",
                    Command =  new RelayCommand(() => {
                        
                        WindowNavigationService.NavigateTo<index>(_currentWindow);
                    }) ,
                    IsSelected = true // 默认选中
                },
                new UserPageModel
                {
                    Header = "退出登录",
                    Command = new RelayCommand(() => MessageBox.Show("out login clicked!")),
                     IsSelected = false // 默认不选中
                },

            };

            listViewModels = new ObservableCollection<ListViewModel>
            {
                new ListViewModel
                {
                    index = "good",
                    name = "空气污染",
                    Remark=DateTime.Now+"",
                },
                new ListViewModel
                {
                   index = "good",
                    name = "当前温度",
                    Remark=DateTime.Now+"",
                },
                new ListViewModel
                {
                   index = "good",
                    name = "光照强度",
                    Remark=DateTime.Now+"",
                },
            };
            UserPageLiveCharts = new UserPageLiveCharts();
        }

     
    }
}
