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
            this.DataContext = new ViewModelMove(); //绑定ViewModel数据源
            _socketClient =new SocketWpf(); //wpf里socket初始化
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            String message = "SocketTest";
            MessageBox.Show("方法以及执行");

            _socketClient.SendDataAsync(message); //触发发送
        }
    }
}

