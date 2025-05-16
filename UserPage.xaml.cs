using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfAppLogin.Model;
using WpfAppLogin.VM;

namespace WpfAppLogin
{
    /// <summary>
    /// UserPage.xaml 的交互逻辑
    /// </summary>
    public partial class UserPage : Window
    {
        private SocketWpf _socketClient;
        public UserPage( )
        {
            InitializeComponent();
            UserModelPageVm _userPageModelVM = new UserModelPageVm(this);
            //      this.Closed += UserPage_Closed;
            _socketClient = new SocketWpf(); //wpf里socket初始化
            this.DataContext = _userPageModelVM;//绑定数据源
        }
        //private void UserPage_Closed(object sender, EventArgs e)
        //{
        //    Environment.Exit(0);
        //}

        private void CardImage_MouseEnter(object sender, MouseEventArgs e)
        {
            var image = sender as Image;
       //     MessageBox.Show("Mouse Entered!"); // 测试是否触发

            // 直接应用 Hover 效果
            image.Effect = new DropShadowEffect()
            {
                Color = Color.FromRgb(0x25, 0x63, 0xEB), // #FF2563EB
                ShadowDepth = 7,
                BlurRadius = 15,
                Opacity = 0.7,
                Direction = 320
            };
            image.Opacity = 1; // 调整透明度
        }

        private void CardImage_MouseLeave(object sender, MouseEventArgs e)
        {
            var image = sender as Image;
        //    MessageBox.Show("Mouse leave!");
            // 恢复默认效果
            image.Effect = new DropShadowEffect
            {
                Color = Color.FromRgb(0xD2,0xD5,0xE2), // #ADB5BD
                ShadowDepth = 5,
                BlurRadius = 10,
                Opacity = 0.5,
                Direction = 320
            };
            image.Opacity = 0.9; // 恢复透明度
        }

    }
}
