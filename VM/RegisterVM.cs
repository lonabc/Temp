
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfAppLogin.Model;
using WpfAppLogin.RelayCommond;
using WpfAppLogin.Services;

namespace WpfAppLogin.VM
{
   public  class RegisterVM : INotifyPropertyChanged
    {
        #region 服务层数据绑定
        private RegisterModel _user;
        private RegisterServices _registerServices;
        #endregion

        public RegisterVM(RegisterModel user,RegisterServices registerServices)
        {
            _registerServices = registerServices;
            _user = user;
        }
        #region 设置属性监听
        public string name
        {
            get { return _user.name; }
            set { _user.name = value; RaisePropertyChanged("name"); }
        }
        public string password
        {
            get { return _user.password; }
            set { _user.password = value; RaisePropertyChanged("password"); }
        }
        public string email
        {
            get { return _user.email; }
            set { _user.email = value; RaisePropertyChanged("email"); }
        }
        
        public string verificationCode
        {
            get { return _user.verificationCode; }
            set { _user.verificationCode = value; RaisePropertyChanged("verificationCode"); }
        }



        #endregion
        public event PropertyChangedEventHandler? PropertyChanged;
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        bool CanRegisgter()
        {
            return true;
         }
       

        public async Task<string> register()
        {
           
            return await _registerServices.register(_user);
        }
    }
}
