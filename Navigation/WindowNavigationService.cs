using Microsoft.Extensions.DependencyInjection;
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
        public static void NavigateTo<T>(Window currentWindow) where T : Window, new() //基本导航
        {
            var newWindow = new T();
            newWindow.Show();
            currentWindow.Close();
        }

        public static void NavigateTo<T>(Window currentWindow, IServiceProvider serviceProvider) //导航无需空参
            where T : Window
        {
            var newWindow = serviceProvider.GetRequiredService<T>();
            newWindow.Show();
            currentWindow.Close();
        }

        public static void NavigateTo<T>(Window currentWindow, Action<T> configure) //导航并配置新窗口
            where T : Window, new()
        {
            var newWindow = new T();
            configure(newWindow);
            newWindow.Show();
            currentWindow.Close();
        }

    }
}
