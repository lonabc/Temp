using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfSample;
using WpfAppLogin.Model;
using WpfAppLogin.VM;
using HandyControl.Controls;
using WpfAppLogin.Navigation;

namespace WpfAppLogin
{
    /// <summary>
    /// index.xaml 的交互逻辑
    /// </summary>
    /// 
    
    public partial class index 
    {
        private SocketWpf _socketClient;
        
        public index()
        {
            InitializeComponent();
            this.DataContext = new ViewModelDev(); //绑定ViewModel数据源
            _socketClient =new SocketWpf(); //wpf里socket初始化

            // 订阅 SizeChanged 事件
            //this.SizeChanged += Window_SizeChanged;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String message = "SocketTest";

            _socketClient.SendDataAsync(message); //触发发送
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // 根据窗口大小动态调整 Card 的宽度和高度
            double newWidth = e.NewSize.Width * 0.8; // 例如，宽度为窗口宽度的 80%
            double newHeight = e.NewSize.Height * 0.4; // 例如，高度为窗口高度的 20%
            myCard.Width = newWidth;
            myCard.Height = newHeight;
        }

        private void ChangFont(object sender, RoutedEventArgs e)
        {
            string result = ONorOFF.Text;
            if (result.Equals("ON"))
            {
                ONorOFF.Text = "OFF";
            }else
            {
                ONorOFF.Text = "ON";
            }
        }
        private void goToUserCenter(object sender, RoutedEventArgs e)
        {
            WindowNavigationService.NavigateTo<UserPage>(this);
        }
    }
}

