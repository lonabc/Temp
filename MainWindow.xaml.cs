using Microsoft.CSharp.RuntimeBinder;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WpfAppLogin.Navigation;
using WpfAppLogin.Page;
using WpfAppLogin.VM;
using WpfAppLogin.VM.PageVmToTal;

namespace WpfAppLogin;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //ModelTest modelTest;
    private readonly MainWindowPageVM _mainPageVM;
    public MainWindow(MainWindowPageVM mainPageVM)
    {
        InitializeComponent();
        _mainPageVM= mainPageVM;
        DataContext = _mainPageVM;
       
    }
    

    private async void  BtnLogin_Click(object sender, RoutedEventArgs e)
    {
       
        // string code=await _loginVm.login();
        string code = await _mainPageVM.LoginVm.login();
        if (code.Equals("200"))
        {
            WindowNavigationService.NavigateTo<UserPage>(this,App.ServiceProvider);
        }
        else
        { 
            MessageBox.Show("登入失败");
        }
    }
    private void Hyperlink_Click(object sender, RoutedEventArgs e)
    {
        WindowNavigationService.NavigateTo<Register>(this, App.ServiceProvider);
    }
}

