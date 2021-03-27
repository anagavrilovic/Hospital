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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PrikazPacijenata.xaml
    /// </summary>
    public partial class PrikazPacijenata : UserControl
    {
        private ObservableCollection<MedicalRecord> _pacijenti = new ObservableCollection<MedicalRecord>();
        public ObservableCollection<MedicalRecord> Pacijenti { get => _pacijenti; set => _pacijenti = value; }
        
        MedicalRecordStorage mrs = new MedicalRecordStorage();

        public PrikazPacijenata()
        {
            InitializeComponent();
            this.DataContext = this;   
            Pacijenti = mrs.GetAll();
        }    

        private void KreirajKarton(object sender, RoutedEventArgs e)
        {
            KreiranjeKartona kk = new KreiranjeKartona(Pacijenti);
            kk.Show();
        }

        private void IzbrisiKarton(object sender, RoutedEventArgs e)
        {
            Pacijenti.Remove((MedicalRecord) PacijentiTable.SelectedItem);
            mrs.DoSerialization(Pacijenti);
        }

        private void IzmeniKarton(object sender, RoutedEventArgs e)
        {
            var ik = new IzmenaKartona((MedicalRecord) PacijentiTable.SelectedItem, Pacijenti);
            ik.Show();
        }
    }
}
