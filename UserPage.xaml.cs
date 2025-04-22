using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppLogin.Model;
using WpfAppLogin.VM;

namespace WpfAppLogin
{
    /// <summary>
    /// UserPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserPage : Window
    {

        private UserModelPageVm _userPageModelVM;
       
        public UserPage(UserModelPageVm userModelPageVm)
        {
            InitializeComponent();

            _userPageModelVM = userModelPageVm;
            this.Closed += UserPage_Closed;
            this.DataContext = _userPageModelVM;//绑定数据源
        }
        private void UserPage_Closed(object sender, EventArgs e)
        {
            

            Environment.Exit(0);
        }
    }
}
