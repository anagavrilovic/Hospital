using Hospital.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void getMenager(object sender, RoutedEventArgs e)
        {
            MenagerWindow mw = new MenagerWindow();
            mw.Owner = Application.Current.MainWindow;
            mw.Show();
        }

        private void getSecretary(object sender, RoutedEventArgs e)
        {
           
        }

        private void getDoctor(object sender, RoutedEventArgs e)
        {

        }

        private void getPatient(object sender, RoutedEventArgs e)
        {

        }

        void Window_Closing(object sender, CancelEventArgs e)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + "rooms.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, RoomStorage.rooms);
            }
        }
    }
}
