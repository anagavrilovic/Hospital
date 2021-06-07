using Hospital.Model;
using Hospital.Services;
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

        public Appointment NewAppointment { get; set; }
        public ObservableCollection<Room> AvaliableRooms { get; set; }
        public ObservableCollection<string> PossibleDuration { get; set; }

        public AppointmentService AppointmentService { get; set; }
        public RoomService RoomService { get; set; }


        public ZakazivanjeTermina(Hospital.Model.Doctor selectedDoctor, MedicalRecord selectedPatient, DateTime dateTimeForNewAppointment)
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeAllProperties();
            ShowPossibleDuration();
            InitializeNewAppointment(selectedDoctor, selectedPatient, dateTimeForNewAppointment);
        }

        private void InitializeAllProperties()
        {
            NewAppointment = new Appointment();
            AvaliableRooms = new ObservableCollection<Room>();
            PossibleDuration = new ObservableCollection<string>();
            AppointmentService = new AppointmentService();
            RoomService = new RoomService();
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

        private void InitializeNewAppointment(Model.Doctor selectedDoctor, MedicalRecord selectedPatient, DateTime dateTimeForNewAppointment)
        {
            SetPatientForNewAppointment(selectedPatient);
            SetDoctorForNewAppointment(selectedDoctor);
            NewAppointment.DateTime = dateTimeForNewAppointment;
            NewAppointment.IDAppointment = AppointmentService.GetNewID();
        }

        private void SetPatientForNewAppointment(MedicalRecord selectedPatient)
        {
            NewAppointment.PatientsRecord = selectedPatient;
            NewAppointment.IDpatient = selectedPatient.Patient.PersonalID;
        }

        private void SetDoctorForNewAppointment(Model.Doctor selectedDoctor)
        {
            NewAppointment.Doctor = selectedDoctor;
            NewAppointment.IDDoctor = selectedDoctor.PersonalID;
        }

        private void PotvrdiClick(object sender, RoutedEventArgs e)
        {
            if(ComboBoxTrajanje.SelectedIndex == -1 || ComboBoxType.SelectedIndex == -1 || ComboBoxRoom.SelectedIndex == -1)
            {
                InformationBox informationBox = new InformationBox("Popunite sve informacije o novom pregledu!");
                informationBox.ShowDialog();
                return;
            }

            string message = AppointmentService.ScheduleAppointment(NewAppointment);

            if (!message.Equals(""))
            {
                InformationBox informationBox = new InformationBox(message);
                informationBox.ShowDialog();
                return;
            }

            NavigationService.Navigate(new Kalendar(NewAppointment.Doctor));
        }

        private void OdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Kalendar(NewAppointment.Doctor));
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
            AvaliableRooms = new ObservableCollection<Room>(RoomService.GetAvaliableRoomsForNewAppointment(NewAppointment));
            ComboBoxRoom.ItemsSource = AvaliableRooms;
        }
        
    }
}
