using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WpfAppLogin.Model;

namespace WpfAppLogin.Services
{
    public   class LoginServices
    {
        private readonly HttpClient _httpClient;

        public LoginServices(IHttpClientFactory httpClientFactory)
        {
           _httpClient=httpClientFactory.CreateClient("MyApi");
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
                return "200"; //登录成功
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
    }
}
