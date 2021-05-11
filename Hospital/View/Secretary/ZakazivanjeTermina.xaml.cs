using Hospital.Model;
using Hospital.View.Secretary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    public partial class ZakazivanjeTermina : Page
    {
        private const double minDurationForAppointment = 0.5;
        private const double maxDurationForAppointment = 24;
        private const double timeSlot = 0.5;

        public Model.Doctor Doctor { get; set; }
        public MedicalRecord Patient { get; set; }
        public Appointment NewAppointment { get; set; }
        public ObservableCollection<Room> AvaliableRooms { get; set; }
        public ObservableCollection<string> PossibleDuration { get; set; }

        public AppointmentStorage AppointmentStorage { get; set; }
        public RoomStorage RoomStorage { get; set; }


        public ZakazivanjeTermina(Hospital.Model.Doctor selectedDoctor, MedicalRecord selectedPatient, DateTime dateTimeForNewAppointment)
        {
            InitializeComponent();
            InitializeAllProperties();
            ShowPossibleDuration();

            this.DataContext = this;
            this.Doctor = selectedDoctor;
            this.Patient = selectedPatient;
            NewAppointment.DateTime = dateTimeForNewAppointment;
        }

        private void InitializeAllProperties()
        {
            NewAppointment = new Appointment();
            AvaliableRooms = new ObservableCollection<Room>();
            PossibleDuration = new ObservableCollection<string>();
            AppointmentStorage = new AppointmentStorage();
            RoomStorage = new RoomStorage();
        }

        private void ShowPossibleDuration()
        {
            double duration = minDurationForAppointment;
            while (duration <= maxDurationForAppointment)
            {
                PossibleDuration.Add(duration.ToString());
                duration += timeSlot;
            }
        }

        private void PotvrdiClick(object sender, RoutedEventArgs e)
        {
            InitializeNewAppointment();

            if (!IsDoctorAvaliable())
                return;

            if (!IsPatientAvaliable())
                return;

            AppointmentStorage.Save(NewAppointment);
            NavigationService.Navigate(new Kalendar(Doctor));
        }

        private void InitializeNewAppointment()
        {
            SetPatientForNewAppointment();
            SetDoctorForNewAppointment();
            SetIDForNewAppointment();
        }

        private void SetPatientForNewAppointment()
        {
            NewAppointment.IDpatient = Patient.Patient.PersonalID;
            NewAppointment.PatientName = Patient.Patient.FirstName;
            NewAppointment.PatientSurname = Patient.Patient.LastName; 
        }

        private void SetDoctorForNewAppointment()
        {
            NewAppointment.IDDoctor = Doctor.PersonalID;
            StringBuilder sb = new StringBuilder(Doctor.FirstName).Append(" ").Append(Doctor.LastName);
            NewAppointment.DoctrosNameSurname = sb.ToString();
        }

        private void SetIDForNewAppointment()
        {
            NewAppointment.IDAppointment = NewAppointment.GetHashCode().ToString();
        }

        private bool IsDoctorAvaliable()
        {
            if (!AppointmentStorage.IsDoctorAvaliableForAppointment(NewAppointment))
            {
                InformationBox informationBox = new InformationBox("Doktor je već zauzet u ovom terminu. Promenite trajanje ili odaberite drugi termin!");
                informationBox.ShowDialog();
                return false;
            }

            return true;
        }

        private bool IsPatientAvaliable()
        {
            if (!AppointmentStorage.IsPatientAvaliableForAppointment(NewAppointment))
            {
                InformationBox informationBox = new InformationBox("Ovaj pacijent već ima zakazan pregled/operaciju u ovom terminu!");
                informationBox.ShowDialog();
                return false;
            }

            return true;
        }

        private void OdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Kalendar(Doctor));
        }

        private void DurationSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadAvaliableRooms();
        }
            
        private void TypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            NewAppointment.Type = (AppointmentType)ComboBoxType.SelectedIndex;
            LoadAvaliableRooms();
        }

        private void LoadAvaliableRooms()
        {
            AvaliableRooms = RoomStorage.GetAvaliableRooms(NewAppointment);
            Room.ItemsSource = AvaliableRooms;
        }
        
    }
}
