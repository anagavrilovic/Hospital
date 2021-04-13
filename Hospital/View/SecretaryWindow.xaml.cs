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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for SecretaryWindow.xaml
    /// </summary>
    public partial class SecretaryWindow : Window
    {
        public SecretaryWindow()
        {
            InitializeComponent();
        }

        private void PrikazPacijenata(object sender, RoutedEventArgs e)
        {
            Content = new PrikazPacijenata();
        }

        private void KreirajKarton(object sender, RoutedEventArgs e)
        {
            var kk = new KreiranjeKartona(null);
            kk.Show();
        }

        private void KalendarClick(object sender, RoutedEventArgs e)
        {
            var kalendar = new Kalendar();
            kalendar.Show();
        }

        private void ObavestenjaClick(object sender, RoutedEventArgs e)
        {
            var obavestenja = new ObavestenjaSekretar();
            obavestenja.Show();
        }
    }
}
