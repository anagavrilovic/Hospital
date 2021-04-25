using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
    /// Interaction logic for OdaberiPacijenta.xaml
    /// </summary>
    public partial class OdaberiPacijenta : Window
    {
        private MedicalRecord patient = new MedicalRecord();
        private ObservableCollection<MedicalRecord> pacijenti = new ObservableCollection<MedicalRecord>();
        private ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();

        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set { appointments = value; }
        }

        public ObservableCollection<MedicalRecord> Pacijenti
        {
            get { return pacijenti; }
            set { pacijenti = value; }
        }

        public MedicalRecord Patient
        {
            get { return patient; }
            set { patient = value; }
        }

        private ICollectionView pacijentiCollection;

        public ICollectionView PacijentiCollection
        {
            get { return pacijentiCollection; }
            set { pacijentiCollection = value; }
        }

        private ICollectionView terminiCollection;

        public ICollectionView TerminiCollection
        {
            get { return terminiCollection; }
            set { terminiCollection = value; }
        }

        public OdaberiPacijenta(MedicalRecord medicalRecord)
        {
            InitializeComponent();
            this.DataContext = this;
            Patient = medicalRecord;
            UcitajPacijente();
        }

        private void UcitajPacijente()
        {
            MedicalRecordStorage mrs = new MedicalRecordStorage();
            Pacijenti = mrs.GetAll();

            PacijentiCollection = CollectionViewSource.GetDefaultView(Pacijenti);
            PacijentiCollection.Filter = CustomFilterPacijenti;
        }

        private bool CustomFilterPacijenti(object obj)
        {
            if (string.IsNullOrEmpty(PacijentiFilter.Text))
            {
                return true;
            }
            else
            {
                return ((obj.ToString()).IndexOf(PacijentiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void PacijentiFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxPacijenti.ItemsSource).Refresh();
        }

        private void BtnExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PotvrdiClick(object sender, RoutedEventArgs e)
        {
            PopuniPacijenta();
            this.Close();
        }

        private void PopuniPacijenta()
        {
            MedicalRecord mr = (MedicalRecord)ListBoxPacijenti.SelectedItem;
            Patient.Patient.FirstName = mr.Patient.FirstName;
            Patient.Patient.LastName = mr.Patient.LastName;
            Patient.Patient.Address.Street = mr.Patient.Address.Street;
            Patient.Patient.Address.StreetNumber = mr.Patient.Address.StreetNumber;
            Patient.Patient.CardID = mr.Patient.CardID;
            Patient.Patient.Address.City.CityName = mr.Patient.Address.City.CityName;
            Patient.Patient.Address.City.Country.CountryName = mr.Patient.Address.City.Country.CountryName;
            Patient.Patient.DateOfBirth = mr.Patient.DateOfBirth;
            Patient.Patient.Email = mr.Patient.Email;
            Patient.Patient.Gender = mr.Patient.Gender;
            Patient.Patient.IsGuest = mr.Patient.IsGuest;
            Patient.Patient.MaritalStatus = mr.Patient.MaritalStatus;
            Patient.Patient.Password = mr.Patient.Password;
            Patient.Patient.PersonalID = mr.Patient.PersonalID;
            Patient.Patient.PhoneNumber = mr.Patient.PhoneNumber;
            Patient.Patient.Address.City.Township = mr.Patient.Address.City.Township;
            Patient.Patient.Address.City.PostalCode = mr.Patient.Address.City.PostalCode;
            Patient.Patient.Username = mr.Patient.Username;
            Patient.Allergen = mr.Allergen;
            Patient.Examination = mr.Examination;
            Patient.HealthCardNumber = mr.HealthCardNumber;
            Patient.IsInsured = mr.IsInsured;
            Patient.MedicalRecordID = mr.MedicalRecordID;
            Patient.ParentName = mr.ParentName;
            Patient.BloodType = mr.BloodType;
        }

        private void OdustaniClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListBoxPacijenti_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Appointments.Clear();

            MedicalRecord mr = (MedicalRecord)ListBoxPacijenti.SelectedItem;
            if(mr != null)
            {
                AppointmentStorage appointmentStorage = new AppointmentStorage();
                ObservableCollection<Appointment> app = appointmentStorage.GetAll();

                foreach (Appointment ap in app)
                {
                    if (mr.Patient.PersonalID.Equals(ap.IDpatient))
                    {
                        Appointments.Add(ap);
                    }
                }
            }
                
            TerminiCollection = CollectionViewSource.GetDefaultView(Appointments);
            TerminiCollection.Filter = CustomFilterTermini;
        }

        private bool CustomFilterTermini(object obj)
        {
            if (string.IsNullOrEmpty(TerminiFilter.Text))
            {
                return true;
            }
            else
            {
                return ((obj.ToString()).IndexOf(TerminiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
            }
        }

        private void TerminiFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(TabelaTermina.ItemsSource).Refresh();
        }
    }
}
