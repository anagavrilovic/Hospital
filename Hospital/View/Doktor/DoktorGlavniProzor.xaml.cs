using Hospital.Model;
using Hospital.View.Doktor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for DoktorGlavniProzor.xaml
    /// </summary>
    public partial class DoktorGlavniProzor : Window, INotifyPropertyChanged
    {
    
        private string doctorId;

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DoktorGlavniProzor(string doctorId)
        { 
            this.doctorId = doctorId;
            InitializeComponent();
            this.DataContext = this;
            InitProperties();
        }

        private void InitProperties()
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            Main.Content =new DoktorGlavnaStranica(doctorId);
        }

        private void Logo(object sender, RoutedEventArgs e)
        {
            Main.Content = new DoktorGlavnaStranica (doctorId);
        }

        private void SignOut(object sender, RoutedEventArgs e)
        {
            MainWindow m = new MainWindow();
            Application.Current.MainWindow = m;
            m.Show();
            this.Close();
        }

        private void Announcment(object sender, RoutedEventArgs e)
        {
            //obavestenje.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/announcment.png", UriKind.Absolute));
            Main.Content = new DoktorObavestenja(doctorId);
        }
    }
}
