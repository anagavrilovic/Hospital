
using Hospital.Commands.Secretary;
using Hospital.Factory;
using Hospital.Repositories;
using Hospital.Services;
using Hospital.View.Secretary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Navigation;

namespace Hospital.ViewModels.Secretary
{
    public class UrgentExaminationViewModel : ViewModel
    {
        #region Properties

        private const double minDurationForAppointment = 0.5;
        private const double maxDurationForAppointment = 24;
        private const double timeSlot = 0.5;

        public Appointment NewUrgentAppointment { get; set; }

        public DoctorSpecialty DoctorsSpecialty { get; set; }
        public ObservableCollection<string> PossibleDuration { get; set; }
        public MedicalRecord SelectedPatient { get; set; }

        public int SelectedAppointment { get; set; }
        public ObservableCollection<MedicalRecord> Patients { get; set; }
        public ICollectionView PatientsCollection { get; set; }

        private MedicalRecord newGuestPatient;
        public MedicalRecord NewGuestPatient
        {
            get { return newGuestPatient; }
            set { newGuestPatient = value; OnPropertyChanged("NewGuestPatient"); }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set 
            { 
                searchText = value; 
                OnPropertyChanged("SearchText");
                CollectionViewSource.GetDefaultView(Patients).Refresh();
            }
        }

        public MedicalRecordService MedicalRecordService { get; set; }
        public AppointmentService AppointmentService { get; set; }
        public DoctorService DoctorService { get; set; }
        public NavigationService NavigationService { get; set; }

        #endregion

        #region Konstruktor
        public UrgentExaminationViewModel(NavigationService navigationService)
        {
            this.NavigationService = navigationService;
            ScheduleAppointmentCommand = new RelayCommand(Execute_ScheduleAppointmentCommand, CanExecuteCommands);
            CancelCommand = new RelayCommand(Execute_CancelCommand, CanExecuteCommands);
            InitializeEmptyProperties();
            ShowPossibleDuration();
            LoadPatients();
        }

        #endregion

        #region Metode

        private void InitializeEmptyProperties()
        {
            SelectedPatient = new MedicalRecord();
            NewGuestPatient = new MedicalRecord();
            NewUrgentAppointment = new Appointment();
            Patients = new ObservableCollection<MedicalRecord>();
            MedicalRecordService = new MedicalRecordService();
            AppointmentService = new AppointmentService();
            DoctorService = new DoctorService(new DoctorFileRepository());
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

        private bool CustomFilterPatients(object obj)
        {
            if (string.IsNullOrEmpty(SearchText))
                return true;
            else
                return ((obj.ToString()).IndexOf(SearchText, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private bool IsPatientChosen()
        {
            if (SelectedPatient == null && !NewGuestPatient.Patient.IsGuest)
            {
                InformationBox informationBox = new InformationBox("Niste izabrali nijednog pacijenta!");
                informationBox.Show();
                return false;
            }
            return true;
        }

        private void RegisterGuestPatient()
        {
            NewGuestPatient.MedicalRecordID = MedicalRecordService.GenerateID();
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
            NewUrgentAppointment.Type = (Hospital.AppointmentType)SelectedAppointment + 2;
            NewUrgentAppointment.IDAppointment = AppointmentService.GetNewID();
        }

        private void ScheduleUrgentAppointment()
        {
            if (AppointmentService.IsScheduledWithoutReschedulingAppointments(NewUrgentAppointment, DoctorsSpecialty))
            {
                var urgentAppointmentWindow = new UrgentExaminationDetails(NewUrgentAppointment);
                urgentAppointmentWindow.Show();
                NavigationService.Navigate(new UrgentExamination(new UrgentExaminationViewModel(this.NavigationService)));
            }
            else
                NavigationService.Navigate(new UrgentExaminationRescheduling(NewUrgentAppointment, DoctorsSpecialty));

        }

        #endregion

        #region Komande

        public RelayCommand ScheduleAppointmentCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }

        #endregion

        #region Akcije

        public void Execute_ScheduleAppointmentCommand(object obj)
        {
            if (!IsPatientChosen())
                return;

            if (NewGuestPatient.Patient.IsGuest)
                RegisterGuestPatient();

            SetPatientForNewUrgentAppointment();
            SetAppointmentDetails();

            ScheduleUrgentAppointment();
        }

        public bool CanExecuteCommands(object obj)
        {
            return true;
        }

        public void Execute_CancelCommand(object obj)
        {
            this.NavigationService.Navigate(new UrgentExamination(new UrgentExaminationViewModel(this.NavigationService)));
        }


        #endregion
    }
}
