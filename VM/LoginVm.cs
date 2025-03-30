using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppLogin.Model;

using WpfAppLogin.RelayCommond;

namespace WpfAppLogin.VM
{
    public class LoginVm : INotifyPropertyChanged
    {
        #region 服务层数据绑定
        private LoginModel _loginModel;

        private readonly HttpClient _httpClient;
   

        public LoginVm(IHttpClientFactory httpClientFactory,LoginModel loginModel)
        {
            _httpClient = httpClientFactory.CreateClient("MyApi");
            _loginModel = loginModel;
           
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

            try
            {
                _loginModel.id = "1";
                var json = JsonSerializer.Serialize(_loginModel);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var fullUrl = new Uri(_httpClient.BaseAddress, "HomeController/Login/Login").AbsoluteUri;
                Debug.WriteLine($"请求URL: {fullUrl}");
                Debug.WriteLine($"请求内容: {json}");

                var response = await _httpClient.PostAsync("HomeController/Login/Login", content);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                return "200";
                //       MessageBox.Show(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Trace.WriteLine($"请求失败: {ex.Message}");
                if (ex.StatusCode.HasValue)
                {
                    Trace.WriteLine($"状态码: {ex.StatusCode}");
                }
                return ex.StatusCode.ToString();
            }
         

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
