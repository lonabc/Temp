using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfAppLogin.VM;

namespace WpfAppLogin;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //ModelTest modelTest;
    LoginVm loginVm;
    public MainWindow()
    {
        InitializeComponent();
         loginVm = new LoginVm();
         this.DataContext =loginVm; 
    }
    

    private void BtnLogin_Click(object sender, RoutedEventArgs e)
    {
        
       loginVm.login();
        MessageBox.Show("Login Success"+loginVm.name+loginVm.password);
    }
}

