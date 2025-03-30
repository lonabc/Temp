using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfAppLogin.Navigation
{
    public class WindowNavigationService
    {
        public static void NavigateTo<T>(Window currentWindow) where T : Window, new()
        {
            var newWindow = new T();
            newWindow.Show();
            currentWindow.Close();
        }

        public static void NavigateTo<T>(Window currentWindow, Action<T> configure)
            where T : Window, new()
        {
            var newWindow = new T();
            configure(newWindow);
            newWindow.Show();
            currentWindow.Close();
        }

    }
}
