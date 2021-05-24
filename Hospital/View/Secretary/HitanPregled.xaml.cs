using Hospital.Services;
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
        private const double minDurationForAppointment = 0.5;
        private const double maxDurationForAppointment = 24;
        private const double timeSlot = 0.5;

        public Appointment NewUrgentAppointment { get; set; }

        public DoctorSpecialty DoctorsSpecialty { get; set; }
        public ObservableCollection<string> PossibleDuration { get; set; }
        public MedicalRecord SelectedPatient { get; set; }
        public ObservableCollection<MedicalRecord> Patients { get; set; }
        public ICollectionView PatientsCollection { get; set; }
        public MedicalRecord NewGuestPatient { get; set; }

        public MedicalRecordService MedicalRecordService { get; set; }
        public AppointmentService AppointmentService { get; set; }
        public DoctorService DoctorService { get; set; }


        public HitanPregled()
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeEmptyObjects();
            ShowPossibleDuration();
            LoadPatients();
        }

        private void InitializeEmptyObjects() 
        {
            SelectedPatient = new MedicalRecord();
            NewGuestPatient = new MedicalRecord();
            NewUrgentAppointment = new Appointment();
            Patients = new ObservableCollection<MedicalRecord>();
            MedicalRecordService = new MedicalRecordService();
            AppointmentService = new AppointmentService();
            DoctorService = new DoctorService();
        }

        private void ShowPossibleDuration()
        {
            PossibleDuration = new ObservableCollection<string>();
            double durationForSelection = minDurationForAppointment;
            while (durationForSelection <= maxDurationForAppointment)
            {
                PossibleDuration.Add(durationForSelection.ToString());
                durationForSelection += timeSlot;
            }
        }

        private void LoadPatients()
        {
            Patients = new ObservableCollection<MedicalRecord>(MedicalRecordService.GetAllRecords());

            PatientsCollection = CollectionViewSource.GetDefaultView(Patients);
            PatientsCollection.Filter = CustomFilterPatients;
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
            MedicalRecordService.RegisterNewRecord(NewGuestPatient);
        }

        private void SetPatientForNewUrgentAppointment()
        {
            if (NewGuestPatient.Patient.IsGuest)
                NewUrgentAppointment.PatientsRecord = NewGuestPatient;
            else
                NewUrgentAppointment.PatientsRecord = SelectedPatient;

            NewUrgentAppointment.IDpatient = NewUrgentAppointment.PatientsRecord.Patient.PersonalID;
        }

        private void SetAppointmentDetails()
        {
            NewUrgentAppointment.Type = (Hospital.AppointmentType)AppointmentType.SelectedIndex + 2;
            NewUrgentAppointment.IDAppointment = AppointmentService.GenerateID();
        }

        private void ScheduleUrgentAppointment()
        {
            if (AppointmentService.IsScheduledWithoutReschedulingAppointments(NewUrgentAppointment, DoctorsSpecialty))
            {
                var urgentAppointmentWindow = new HitanPregledDetalji(NewUrgentAppointment);
                urgentAppointmentWindow.Show();
                NavigationService.Navigate(new HitanPregled());
            }
            else
                NavigationService.Navigate(new HitanPregledPomeranje(NewUrgentAppointment, DoctorsSpecialty));

        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HitanPregled());
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
    }
}
