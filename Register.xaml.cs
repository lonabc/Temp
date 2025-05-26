using System;
using System.Collections.Generic;
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
using WpfAppLogin.Navigation;
using WpfAppLogin.VM;
using WpfAppLogin.VM.PageVmToTal;

namespace WpfAppLogin
{
    /// <summary>
    /// Register.xaml 的交互逻辑
    /// </summary>
    public partial class Register : Window
    {
        private RegisterVM _registerVm;
        public Register(RegisterVM registerVm)
        {
            InitializeComponent();
            _registerVm=registerVm;
            this.DataContext =_registerVm ;  //绑定数据源
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
        
         string code=  await _registerVm.register();
            if (code.Equals("200"))
            {

                WindowNavigationService.NavigateTo<MainWindow>(this, App.ServiceProvider);
            }
            else
            {
                MessageBox.Show("注册失败");
            }
        }
    }
}
