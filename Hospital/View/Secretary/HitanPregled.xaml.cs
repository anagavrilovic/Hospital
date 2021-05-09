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
        public Appointment NewUrgentAppointment { get; set; }
        public DoctorSpecialty DoctorsSpecialty { get; set; }

        // Storage class properties
        public MedicalRecordStorage MedicalRecordStorage { get; set; }
        public AppointmentStorage AppointmentStorage { get; set; }
        public Model.DoctorStorage DoctorStorage { get; set; }


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
            NewUrgentAppointment = new Appointment();
            Patients = new ObservableCollection<MedicalRecord>();
            MedicalRecordStorage = new MedicalRecordStorage();
            AppointmentStorage = new AppointmentStorage();
            DoctorStorage = new Model.DoctorStorage();
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

            SetPatientForNewUrgentAppointment();
            SetAppointmentDetails();

            ScheduleUrgentAppointment();
        }

        private bool IsPatientChosen()
        {
            if (ListBoxPatients.SelectedItem == null && !NewGuestPatient.Patient.IsGuest)
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

        private void SetPatientForNewUrgentAppointment()
        {
            MedicalRecord newPatient;
            if (NewGuestPatient.Patient.IsGuest)
                newPatient = NewGuestPatient;
            else
                newPatient = SelectedPatient;

            NewUrgentAppointment.IDpatient = newPatient.Patient.PersonalID;
            NewUrgentAppointment.PatientName = newPatient.Patient.FirstName;
            NewUrgentAppointment.PatientSurname = newPatient.Patient.LastName;
        }

        private void SetAppointmentDetails()
        {
            if (AppointmentType.SelectedIndex == 0)
                NewUrgentAppointment.Type = Hospital.AppointmentType.urgentExamination;
            else
                NewUrgentAppointment.type = Hospital.AppointmentType.urgentOperation;

            NewUrgentAppointment.IDAppointment = new AppointmentStorage().GetNewID();
        }

        private void ScheduleUrgentAppointment()
        {
            if (IsScheduledWithoutReschedulingAppointments())
            {
                SetRoomForNewUrgentAppointment();
                var urgentAppointmentWindow = new HitanPregledDetalji(NewUrgentAppointment);
                urgentAppointmentWindow.Show();
                NavigationService.Navigate(new HitanPregled());
            }
            else
            {
                NavigationService.Navigate(new HitanPregledPomeranje(NewUrgentAppointment, DoctorsSpecialty));
            }

        }

        private bool IsScheduledWithoutReschedulingAppointments()
        {
            SetDateTimeForNewAppointment();
            ObservableCollection<Model.Doctor> possibleDoctors = DoctorStorage.GetDoctorsBySpecialty(DoctorsSpecialty);

            for (int i = 0; i < 3; i++)
            {
                foreach (Model.Doctor doctor in possibleDoctors)
                {
                    SetDoctorForNewAppointment(doctor);
                    if (AppointmentStorage.IsDoctorAvaliableForAppointment(NewUrgentAppointment))
                    {
                        AppointmentStorage.Save(NewUrgentAppointment);
                        return true;
                    }
                        
                }
                NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(30);
            }
            return false;
        }

        private void SetRoomForNewUrgentAppointment()
        {
            ObservableCollection<Room> avaliableRooms = new RoomStorage().GetAvaliableRooms(NewUrgentAppointment);
            NewUrgentAppointment.Room = avaliableRooms[0];
        }

        private void SetDateTimeForNewAppointment()
        {
            var currentDateTime = DateTime.Now;
            NewUrgentAppointment.DateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour,
                currentDateTime.Minute, currentDateTime.Second, currentDateTime.Kind);
            NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddSeconds(-NewUrgentAppointment.DateTime.Second);

            if (NewUrgentAppointment.DateTime.Minute >= 0 && NewUrgentAppointment.DateTime.Minute <= 15)
                NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(-NewUrgentAppointment.DateTime.Minute);
            else if (NewUrgentAppointment.DateTime.Minute > 15 && NewUrgentAppointment.DateTime.Minute < 30)
                NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(30 - NewUrgentAppointment.DateTime.Minute);
            else if (NewUrgentAppointment.DateTime.Minute >= 30 && NewUrgentAppointment.DateTime.Minute <= 45)
                NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(-NewUrgentAppointment.DateTime.Minute + 30);
            else
                NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(60 - NewUrgentAppointment.DateTime.Minute);
        }

        private void SetDoctorForNewAppointment(Model.Doctor doctor)
        {
            NewUrgentAppointment.IDDoctor = doctor.PersonalID;
            NewUrgentAppointment.DoctrosNameSurname = doctor.FirstName + " " + doctor.LastName;
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HitanPregled());
        }
    }
}
