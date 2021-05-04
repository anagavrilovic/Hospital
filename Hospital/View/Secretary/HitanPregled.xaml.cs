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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Secretary
{
    public partial class HitanPregled : Page
    {
        // Properties
        public ObservableCollection<string> Duration { get; set; }
        public MedicalRecord SelectedPatient { get; set; }
        public MedicalRecord NewGuestPatient { get; set; }
        public ObservableCollection<MedicalRecord> Patients { get; set; }
        public ICollectionView PatientsCollection { get; set; }
        public Appointment NewAppointment { get; set; }
        public DoctorSpecialty DoctorsSpecialty { get; set; }

        // Storage class properties
        public MedicalRecordStorage MedicalRecordStorage { get; set; }
        public AppointmentStorage AppointmentStorage { get; set; }


        // Methods
        public HitanPregled()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeEmptyObjects();
            ShowDurationInComboBox();
            LoadPatients();
        }

        private void InitializeEmptyObjects()
        {
            Duration = new ObservableCollection<string>();
            SelectedPatient = new MedicalRecord();
            NewGuestPatient = new MedicalRecord();
            NewAppointment = new Appointment();
            Patients = new ObservableCollection<MedicalRecord>();
            MedicalRecordStorage = new MedicalRecordStorage();
        }

        private void ShowDurationInComboBox()
        {
            double durationForSelection = 0;
            while (durationForSelection <= 20)
            {
                Duration.Add(durationForSelection.ToString());
                durationForSelection += 0.5;
            }
        }

        private void LoadPatients()
        {
            Patients = MedicalRecordStorage.GetAll();
            PatientsCollection = CollectionViewSource.GetDefaultView(Patients);
            PatientsCollection.Filter = CustomFilterPatients;
        }

        private bool CustomFilterPatients(object obj)
        {
            if (string.IsNullOrEmpty(TextBoxPatientsFilter.Text))
                return true;
            else
                return ((obj.ToString()).IndexOf(TextBoxPatientsFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void PatientsFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxPatients.ItemsSource).Refresh();
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            if (!IsPatientChosen())
                return;

            if (NewGuestPatient.Patient.IsGuest)
                RegisterGuestPatient();

            SetPatientForNewAppointment();
            SetAppointmentDetails();

            ScheduleUrgentAppointment();
        }

        private bool IsPatientChosen()
        {
            if(ListBoxPatients.SelectedItem == null && !NewGuestPatient.Patient.IsGuest)
            {
                MessageBox.Show("Niste izabrali nijednog pacijenta!");
                return false;
            }

            return true;
        }

        private void RegisterGuestPatient()
        {
            NewGuestPatient.MedicalRecordID = NewGuestPatient.GetHashCode().ToString();
            MedicalRecordStorage.Save(NewGuestPatient);
        }

        private void SetPatientForNewAppointment()
        {
            MedicalRecord newPatient;
            if (NewGuestPatient.Patient.IsGuest)
                newPatient = NewGuestPatient;
            else
                newPatient = SelectedPatient;            

            NewAppointment.IDpatient = newPatient.Patient.PersonalID;
            NewAppointment.PatientName = newPatient.Patient.FirstName;
            NewAppointment.PatientSurname = newPatient.Patient.LastName;
        }

        private void SetAppointmentDetails()
        {
            if (AppointmentType.SelectedIndex == 0)
                NewAppointment.Type = Hospital.AppointmentType.examination;
            else
                NewAppointment.type = Hospital.AppointmentType.operation;

            NewAppointment.DateTime = DateTime.Today;
            NewAppointment.IDAppointment = new AppointmentStorage().GetNewID();
        }

        private void ScheduleUrgentAppointment()
        {
            
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HitanPregled());
        }
    }
}
