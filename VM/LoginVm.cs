using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppLogin.Model;
using WpfAppLogin.RelayCommond;

namespace WpfAppLogin.VM
{
    class LoginVm:INotifyPropertyChanged
    {
        #region 服务层数据绑定
        private LoginModel _loginModel;

        public LoginVm()
        {
            _loginModel = new LoginModel();
        }
      

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string name
        {
            get { return _loginModel.name; }
            set { _loginModel.name = value; RaisePropertyChanged("name"); }
        }
        public string password
        {
            get { return _loginModel.password; }
            set { _loginModel.password = value; RaisePropertyChanged("password"); }
        }
        #endregion

        public void login()
        {
            Console.WriteLine(name+""+password);
            if (_loginModel.name == "admin" && _loginModel.password == "admin")
            {
                index index = new index();
                index.Show();
                MessageBox.Show(name+""+password);
            }
            else
            {
                Console.WriteLine("Login Failed");
                
            }
        }

        bool CanLoginExecute()
        {
            return true;
         }
        public ICommand LoginActoin
        {
            get
            {
                return new Commond(login,CanLoginExecute);
            }
        }
    }
}
