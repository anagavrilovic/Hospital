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
        private ObservableCollection<MedicalRecord> _pacijenti = new ObservableCollection<MedicalRecord>();
        public ObservableCollection<MedicalRecord> Pacijenti { get => _pacijenti; set => _pacijenti = value; }

        public SecretaryWindow()
        {
            InitializeComponent();
        }

        private void PrikazPacijenata(object sender, RoutedEventArgs e)
        {
            Content = new PrikazPacijenata(Pacijenti);
        }

        private void KreirajKarton(object sender, RoutedEventArgs e)
        {
            var kk = new KreiranjeKartona(Pacijenti);
            kk.Show();
        }
    }
}
