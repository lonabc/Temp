using Microsoft.CSharp.RuntimeBinder;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfAppLogin.Navigation;
using WpfAppLogin.VM;

namespace WpfAppLogin;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //ModelTest modelTest;
    private readonly LoginVm _loginVm;


    public MainWindow(LoginVm loginVm)
    {
        InitializeComponent();
        _loginVm = loginVm;
        DataContext = _loginVm;
       
    }
    

    private async void  BtnLogin_Click(object sender, RoutedEventArgs e)
    {
       
         string code=await _loginVm.login();
        if (code.Equals("200"))
        {
            WindowNavigationService.NavigateTo<index>(this);
        }
        else
        { 
            MessageBox.Show("登入失败");
        }
    }
   


}

