using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Minimize(object sender, RoutedEventArgs e)
        {
            foreach(Window w in Application.Current.Windows)
            {
                if (w.Visibility == Visibility.Visible)
                {
                    w.WindowState = WindowState.Minimized;
                }
            }
        }
        private void CloseApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
