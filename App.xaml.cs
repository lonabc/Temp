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
using System.Windows.Threading;
using WpfAppLogin.Model;
using WpfAppLogin.Page;
using WpfAppLogin.Services;
using WpfAppLogin.Tools;
using WpfAppLogin.Tools.ToolsContext;
using WpfAppLogin.VM;
using WpfAppLogin.VM.PageVmToTal;


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

        // 处理 UI 线程未捕获异常
        DispatcherUnhandledException += App_DispatcherUnhandledException;

        // 处理非 UI 线程未捕获异常（Task 异常）
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;


        // 创建服务集合
        var services = new ServiceCollection();

        // 注册服务
        ConfigureServices(services);

        // 构建 ServiceProvider
        ServiceProvider = services.BuildServiceProvider();


        // 显示主窗口（通过 DI 解析）
        var mainWindow = ServiceProvider.GetRequiredService<HomePage>();
        mainWindow.Show();

    }


    private void ConfigureServices(IServiceCollection services)
    {
        // 注册窗口和页面

        services.AddTransient<MainWindow>();
        services.AddTransient<index>();
        services.AddTransient<HomePage>();
        services.AddTransient<UserPage>();
        services.AddTransient<Register>();

        // 注册服务
        services.AddHttpClient("MyApi", client => //MyApi用于标识配置，为多服务访问做准备
        {
            client.BaseAddress = new Uri("http://localhost:5091");
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        });

        services.AddMemoryCache(); // Register memory cache service
       

        #region 注册scoped服务
        services.AddScoped<LoginModel>();
        services.AddScoped<RegisterModel>();
        services.AddScoped<LoginVm>(); // Registe
        services.AddScoped<RegisterVM>();
        services.AddScoped<UserPageModel>();
        services.AddScoped<UserModelPageVm>(); // Register UserPageVm
        services.AddScoped<LoginServices>(); // Register LoginServices
        services.AddScoped<RegisterServices>();
        services.AddScoped<User>(); // Register User
        services.AddScoped<MainWindowPageVM>();
        services.AddScoped<CacheToolsMy>();
    
        #endregion


    }

    private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // 记录日志或显示错误信息
        MessageBox.Show($"UI 线程发生未处理异常: {e.Exception.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        // 标记异常已处理，防止应用程序崩溃
        e.Handled = true;
    }

    private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
    {
        // 记录日志或显示错误信息
        MessageBox.Show($"后台任务发生未处理异常: {e.Exception.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        // 标记异常已观察，防止应用程序崩溃
        e.SetObserved();
    }
}

