
using System.ComponentModel;
using System.Diagnostics;

using System.Net.Http;

using System.Text;
using System.Text.Json;

using System.Windows;

using WpfAppLogin.Model;
using WpfAppLogin.Services;



namespace WpfAppLogin.VM
{
    public class LoginVm : INotifyPropertyChanged
    {
        #region 服务层数据绑定
        private LoginModel _loginModel;
   
        private readonly LoginServices _loginServices;

        public LoginVm(LoginModel loginModel,LoginServices loginServices)
        {
         
            _loginModel = loginModel;
            _loginServices = loginServices;

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

        public async Task<string> login()
        {
          
            return await _loginServices.login(_loginModel);
        }

        bool CanLoginExecute()
        {
            return true;
         }
        //public ICommand LoginActoin
        //{
        //    get
        //    {
        //        return new Commond(login,CanLoginExecute);
        //    }
        //}
    }
}
