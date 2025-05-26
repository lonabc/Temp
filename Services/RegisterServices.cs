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
    public class RegisterServices
    {
        private HttpClient _httpClient;
        public  RegisterServices(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient("MyApi");
        }

        public async Task<string> register(RegisterModel user)
        {
            try
            {
                if (!user.password.Equals(user.verificationCode))
                {
                    MessageBox.Show("两次输入密码不一致，请重新输入");
                    return "500";
                }
             
                var json = JsonSerializer.Serialize(user); //将对象序列化为JSON字符串
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await _httpClient.PostAsync("/RegisterUser", content); //发送POST请求
               
                response.EnsureSuccessStatusCode(); //确保响应状态为200
                var responseBody = await response.Content.ReadAsStringAsync(); //读取响应内容
                string responseCode = responseBody.ToString();
              
                if (responseCode.Contains("注册失败"))
                {
                    return "500"; //注册失败
                }
                MessageBox.Show(responseCode);
                return "200"; //注册成功
            }
            catch (HttpRequestException ex)
            {
              MessageBox.Show("请求错误: " + ex.Message);
                if (ex.StatusCode.HasValue)
                {
                    Trace.WriteLine($"状态码: {ex.StatusCode}");
                }
                return ex.StatusCode.ToString();
            }
        }
    }
}
