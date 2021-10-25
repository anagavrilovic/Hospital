using Hospital.Factory;
using Hospital.Services;
using Hospital.View.Secretary;
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
    public partial class ChoosePatient : Window, INotifyPropertyChanged
    {
        private MedicalRecord patientInCalendar;
        public MedicalRecord PatientInCalendar
        {
            get { return patientInCalendar; }
            set { patientInCalendar = value; OnPropertyChanged("PatientInCalendar"); }
        }

        private MedicalRecord selectedPatient;
        public MedicalRecord SelectedPatient
        {
            get { return selectedPatient; }
            set { selectedPatient = value; OnPropertyChanged("SelectedPatient"); }
        }

        public ObservableCollection<MedicalRecord> Patients { get; set; }
        public ObservableCollection<Appointment> PatientsAppointments { get; set; }
        public ICollectionView PatientsCollection { get; set; }
        public ICollectionView PatientsAppointmentsCollection { get; set; }

        public MedicalRecordService MedicalRecordService { get; set; }
        public AppointmentService AppointmentService { get; set; }


        public ChoosePatient(MedicalRecord patient)
        {
            InitializeComponent();
            InitializeAllPropeties();
            LoadPatients();

            this.DataContext = this;
            PatientInCalendar = patient;
        }

        private void InitializeAllPropeties()
        {
            Patients = new ObservableCollection<MedicalRecord>();
            PatientsAppointments = new ObservableCollection<Appointment>();
            MedicalRecordService = new MedicalRecordService();
            AppointmentService = new AppointmentService();
        }

        private void LoadPatients()
        {
            Patients = new ObservableCollection<MedicalRecord>(MedicalRecordService.GetAllRecords());

            PatientsCollection = CollectionViewSource.GetDefaultView(Patients);
            PatientsCollection.Filter = CustomFilterPatients;
        }

        private bool CustomFilterPatients(object obj)
        {
            if (string.IsNullOrEmpty(PacijentiFilter.Text))
                return true;
            else
                return ((obj.ToString()).IndexOf(PacijentiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void PatientsFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(ListBoxPacijenti.ItemsSource).Refresh();
        }

        private void BtnExitClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PotvrdiClick(object sender, RoutedEventArgs e)
        {
            if(SelectedPatient == null)
            {
                InformationBox informationBox = new InformationBox("Selektujte pacijenta!");
                informationBox.Show();
                return;
            }

            PatientInCalendar.DeepCopy(SelectedPatient);
            this.Close();
        }

        private void OdustaniClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ListBoxAllPatients_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SelectedPatient == null)
                return;

            GetSelectedPatientsAppointments();
                
            PatientsAppointmentsCollection = CollectionViewSource.GetDefaultView(PatientsAppointments);
            PatientsAppointmentsCollection.Filter = CustomFilterAppointments;
        }

        private void GetSelectedPatientsAppointments()
        {
            PatientsAppointments.Clear();
            ObservableCollection<Appointment> app = new ObservableCollection<Appointment>(AppointmentService.GetAll());
            foreach (Appointment ap in app)
            {
                if (SelectedPatient.Patient.PersonalID.Equals(ap.IDpatient))
                {
                    PatientsAppointments.Add(ap);
                }
            }
        }

        private bool CustomFilterAppointments(object obj)
        {
            if (string.IsNullOrEmpty(TerminiFilter.Text))
                return true;
            else
                return ((obj as Appointment).DateTime.ToString("dd.MM.yyyy., HH:mm").IndexOf(TerminiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (obj.ToString().IndexOf(TerminiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       ("pregled".IndexOf(TerminiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                                    ((obj as Appointment).Type.Equals(AppointmentType.examination) || (obj as Appointment).Type.Equals(AppointmentType.urgentExamination)) ||
                       ("operacija".IndexOf(TerminiFilter.Text, StringComparison.OrdinalIgnoreCase) >= 0) &&
                                    ((obj as Appointment).Type.Equals(AppointmentType.operation) || (obj as Appointment).Type.Equals(AppointmentType.urgentOperation));
        }

        private void AppointmentsFilterTextChanged(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(TabelaTermina.ItemsSource).Refresh();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
