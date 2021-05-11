using Hospital.View.Secretary;
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
    public partial class PrikazPacijenata : Page
    {
        MedicalRecordStorage mrs = new MedicalRecordStorage();

        private ObservableCollection<MedicalRecord> _pacijenti = new ObservableCollection<MedicalRecord>();
        public ObservableCollection<MedicalRecord> Pacijenti { get => _pacijenti; set => _pacijenti = value; }
        

        public PrikazPacijenata()
        {
            InitializeComponent();
            this.DataContext = this;   
            Pacijenti = mrs.GetAll();
        }    

        private void NoviClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new KreiranjeKartona());
        }
        
        private void IzmeniClick(object sender, RoutedEventArgs e)
        {
            if (PacijentiTable.SelectedItem == null)
            {
                InformationBox informationBox = new InformationBox("Selektujte pacijenta kojeg želite da izmenite!");
                informationBox.ShowDialog();
                return;
            }

            NavigationService.Navigate(new IzmenaKartona((MedicalRecord)PacijentiTable.SelectedItem, Pacijenti));
        }

        private void DetaljiClick(object sender, RoutedEventArgs e)
        {
            if (PacijentiTable.SelectedItem == null)
            {
                InformationBox informationBox = new InformationBox("Selektujte pacijenta čije informacije želite da pregledate!");
                informationBox.ShowDialog();
                return;
            }

            NavigationService.Navigate(new DetaljiKarton((MedicalRecord)PacijentiTable.SelectedItem));
        }

        private void BrisanjeClick(object sender, RoutedEventArgs e)
        {
            if (PacijentiTable.SelectedItem == null)
            {
                InformationBox informationBox = new InformationBox("Selektujte pacijenta kojeg želite da izbrišete!");
                informationBox.ShowDialog();
                return;
            }

            ConfirmBox confirmBox = new ConfirmBox("da želite da izbrišete pacijenta?");
            if ((bool)confirmBox.ShowDialog())
            {
                Pacijenti.Remove((MedicalRecord)PacijentiTable.SelectedItem);
                mrs.DoSerialization(Pacijenti);
            }
        }

        private void AlergeniClick(object sender, RoutedEventArgs e)
        {
            if (PacijentiTable.SelectedItem == null)
            {
                InformationBox informationBox = new InformationBox("Selektujte pacijenta čije alergene želite da izmenite!");
                informationBox.ShowDialog();
                return;
            }

            NavigationService.Navigate(new ModifikacijaAlergena((MedicalRecord)PacijentiTable.SelectedItem, Pacijenti));
        }
    }
}
