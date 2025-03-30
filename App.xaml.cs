using HarfBuzzSharp;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Configuration;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Caching;
using System.Windows;
using System.Windows.Controls;
using WpfAppLogin.Model;
using WpfAppLogin.VM;


namespace WpfAppLogin;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App :Application
{
    public static IServiceProvider ServiceProvider { get; private set; }


    protected override void OnStartup(StartupEventArgs e) // 重写启动事件
    {
        base.OnStartup(e);

        // 创建服务集合
        var services = new ServiceCollection();

        // 注册服务
        ConfigureServices(services);

        // 构建 ServiceProvider
        ServiceProvider = services.BuildServiceProvider();


        // 显示主窗口（通过 DI 解析）
        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();

    }


    private void ConfigureServices(IServiceCollection services)
    {
        // 注册窗口和页面

        services.AddTransient<MainWindow>();

        // 注册服务
        services.AddHttpClient("MyApi", client =>
        {
            client.BaseAddress = new Uri("http://localhost:5278");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddMemoryCache(); // Register memory cache service

        services.AddScoped<LoginModel>();
        services.AddScoped<LoginVm>(); // Registe
    }
}

