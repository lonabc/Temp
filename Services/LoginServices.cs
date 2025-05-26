using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WpfAppLogin.Model;
using WpfAppLogin.Tools;

namespace WpfAppLogin.Services
{
    public   class LoginServices
    {
        private readonly HttpClient _httpClient;
        private readonly CacheToolsMy cacheToolsMy;

        public LoginServices(IHttpClientFactory httpClientFactory,CacheToolsMy cacheTools)
        {
           _httpClient=httpClientFactory.CreateClient("MyApi");
            cacheToolsMy = cacheTools;
        }

        public async Task<string> login(LoginModel loginModel)
        {
            loginModel.id = "1"; //设置ID为1
            try
            {
                var json = JsonSerializer.Serialize(loginModel); //将对象序列化为JSON字符串
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/LoginUser", content); //发送POST请求
              
                response.EnsureSuccessStatusCode(); //确保响应状态为200
                var responseBody = await response.Content.ReadAsStringAsync(); //读取响应内容
                string responseCode=responseBody.ToString();
                if (responseCode.Contains("登录失败"))
                {
                    return "500"; //登录失败
                }

                MessageBox.Show(responseCode);
                String key = loginModel.name + " " + loginModel.password;
                cacheToolsMy.setCachedData(key, responseCode);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseCode); //设置请求头

                return "200"; //登录成功
            }
            catch (HttpRequestException ex)
            {
               MessageBox.Show($"请求失败: {ex.Message}");
                if (ex.StatusCode.HasValue)
                {
                    Trace.WriteLine($"状态码: {ex.StatusCode}");
                }
                return ex.StatusCode.ToString();
            }

        }

        
    }
}
