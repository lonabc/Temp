using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfAppLogin
{
    /// <summary>
    /// HomePage.xaml 的交互逻辑
    /// </summary>
    public partial class HomePage : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public HomePage(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            InitializeComponent();
            Loaded += SplashWindow_Loaded;
        }

        private void SplashWindow_Loaded(object sender, RoutedEventArgs e)
        {
           

            var textFadeIn = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(1)
            };

            // 启动动画
          
            loadingText.BeginAnimation(OpacityProperty, textFadeIn);

            // 在后台加载必要资源并创建主窗口
            Task.Run(async () =>
            {
                // 模拟加载过程
                await Task.Delay(3000);

                // 在主线程中创建并显示主窗口
                await Dispatcher.InvokeAsync(() =>
                {
                    // 通过服务提供者获取MainWindow实例
                    var mainWindow = _serviceProvider.GetRequiredService<UserPage>();
                    mainWindow.Show();
                    this.Close();
                });
            });
        }
    }
}
