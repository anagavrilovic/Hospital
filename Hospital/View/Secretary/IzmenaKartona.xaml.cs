using Hospital.Model;
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
    /// Interaction logic for IzmenaKartona.xaml
    /// </summary>
    public partial class IzmenaKartona : Page
    {
        private MedicalRecord record;

        private ObservableCollection<MedicalRecord> _pacijenti = new ObservableCollection<MedicalRecord>();
        public ObservableCollection<MedicalRecord> Pacijenti { get => _pacijenti; set => _pacijenti = value; }

        public MedicalRecord Record
        {
            get => record;
            set
            {
                record = value;
            }
        }
    
        public IzmenaKartona(MedicalRecord mr, ObservableCollection<MedicalRecord> p)
        {
            InitializeComponent();
            this.DataContext = this;
            this.record = mr;
            this.Pacijenti = p;

            switch (mr.Patient.MaritalStatus)
            {
                case MaritalStatus.neozenjen: BracnoStanje.SelectedItem = Neozenjen; break;
                case MaritalStatus.ozenjen: BracnoStanje.SelectedItem = Ozenjen; break;
                case MaritalStatus.udovac: BracnoStanje.SelectedItem = Udovac; break;
                case MaritalStatus.razveden: BracnoStanje.SelectedItem = Razveden; break;
            }

            switch (mr.Patient.Gender)
            {
                case Genders.male: Pol.SelectedItem = Muski; break;
                case Genders.female: Pol.SelectedItem = Zenski; break;
                case Genders.other: Pol.SelectedItem = Ostalo; break;
            }

            switch (mr.BloodType)
            {
                case BloodType.Aplus: KrvnaGrupa.SelectedItem = Aplus; break;
                case BloodType.Aneg: KrvnaGrupa.SelectedItem = Aneg; break;
                case BloodType.Bplus: KrvnaGrupa.SelectedItem = Bplus; break;
                case BloodType.Bneg: KrvnaGrupa.SelectedItem = Bneg; break;
                case BloodType.ABplus: KrvnaGrupa.SelectedItem = ABplus; break;
                case BloodType.ABneg: KrvnaGrupa.SelectedItem = ABneg; break;
                case BloodType.Oplus: KrvnaGrupa.SelectedItem = Oplus; break;
                case BloodType.Oneg: KrvnaGrupa.SelectedItem = Oneg; break;
            }

            if (mr.IsInsured)
            {
                DaButton.IsChecked = true;
            }
            else
            {
                NeButton.IsChecked = true;
            }
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            ImeText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            RoditeljText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            PrezimeText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Datum.GetBindingExpression(DatePicker.TextProperty).UpdateSource();
            AdresaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            BrojUliceText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            MestoText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            PostanskiBrojText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            OpstinaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            DrzavaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            KnjizicaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            LicnaText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            TelefonText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            JMBGText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            EmailText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            UsernameText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            KartonText.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            PasswordText.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            switch (BracnoStanje.SelectedIndex)
            {
                case 0: Record.Patient.MaritalStatus = MaritalStatus.neozenjen; break;
                case 1: Record.Patient.MaritalStatus = MaritalStatus.ozenjen; break;
                case 2: Record.Patient.MaritalStatus = MaritalStatus.udovac; break;
                case 3: Record.Patient.MaritalStatus = MaritalStatus.razveden; break;
            }

            switch (Pol.SelectedIndex)
            {
                case 0: Record.Patient.Gender = Genders.male; break;
                case 1: Record.Patient.Gender = Genders.female; break;
                case 2: Record.Patient.Gender = Genders.other; break;
            }

            switch (KrvnaGrupa.SelectedIndex)
            {
                case 0: Record.BloodType = BloodType.Aplus; break;
                case 1: Record.BloodType = BloodType.Aneg; break;
                case 2: Record.BloodType = BloodType.Bplus; break;
                case 3: Record.BloodType = BloodType.Bneg; break;
                case 4: Record.BloodType = BloodType.ABplus; break;
                case 5: Record.BloodType = BloodType.ABneg; break;
                case 6: Record.BloodType = BloodType.Oplus; break;
                case 7: Record.BloodType = BloodType.Oneg; break;
            }

            if ((bool)DaButton.IsChecked)
            {
                Record.IsInsured = true;
            }
            else
            {
                Record.IsInsured = false;
            }

            MedicalRecordStorage mrs = new MedicalRecordStorage();
            mrs.DoSerialization(Pacijenti);
            NavigationService.Navigate(new PrikazPacijenata());
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new PrikazPacijenata());
        }
    }
}
