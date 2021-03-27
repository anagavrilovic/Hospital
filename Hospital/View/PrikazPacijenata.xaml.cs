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

        public PrikazPacijenata(ObservableCollection<MedicalRecord> p)
        {
            InitializeComponent();
            this.DataContext = this;
            Pacijenti = p;
            PopuniListu();
        }    

        private void KreirajKarton(object sender, RoutedEventArgs e)
        {
            KreiranjeKartona kk = new KreiranjeKartona(Pacijenti);
            kk.Show();
        }

        private void PopuniListu()
        {
            MedicalRecord md1 = new MedicalRecord();
            md1.Patient.FirstName = "Ana";
            md1.Patient.LastName = "Gavrilovic";
            md1.Patient.PersonalID = "2309999777021";
            md1.MedicalRecordID = 1;

            MedicalRecord md2 = new MedicalRecord();
            md2.Patient.FirstName = "Marija";
            md2.Patient.LastName = "Kljestan";
            md2.Patient.PersonalID = "15151515151515";
            md2.MedicalRecordID = 2;

            Pacijenti.Add(md1);
            Pacijenti.Add(md2);
        }

        private void IzbrisiKarton(object sender, RoutedEventArgs e)
        {
            Pacijenti.Remove((MedicalRecord) PacijentiTable.SelectedItem);
        }

        private void IzmeniKarton(object sender, RoutedEventArgs e)
        {

        }
    }
}
