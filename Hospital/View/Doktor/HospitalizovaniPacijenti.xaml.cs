using Hospital.Model;
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

namespace Hospital.View.Doktor
{
    public partial class HospitalizovaniPacijenti : Page,INotifyPropertyChanged
    {
        private ObservableCollection<HospitalizedPatient> hospitalizedPatients = new ObservableCollection<HospitalizedPatient>();
        private HospitalTreatmentStorage hospitalTreatmentStorage = new HospitalTreatmentStorage();
        private RoomStorage roomStorage = new RoomStorage();
        private HospitalizedPatient hospitalizedPatient = new HospitalizedPatient();
        private MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<HospitalizedPatient> HospitalizedPatients
        {
            get { return hospitalizedPatients; }
            set { hospitalizedPatients = value; }
        }
        public HospitalizedPatient HospitalizedPatient
        {
            get
            {
                return hospitalizedPatient;
            }
            set
            {
                if (value != hospitalizedPatient)
                {
                    hospitalizedPatient = value;
                    OnPropertyChanged("hospitalizedPatient");
                }
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public HospitalizovaniPacijenti()
        {
            InitializeComponent();
            this.DataContext = this;
            FillTable();
            DateTime startDate = DateTime.Today;
            calendar.DisplayDateStart = startDate;
        }

        private void FillTable()
        {
            foreach (HospitalTreatment hospitalTreatment in hospitalTreatmentStorage.GetAll())
            {
                HospitalizedPatient hospitalizedPatient = new HospitalizedPatient();
                hospitalizedPatient.Patient=((medicalRecordStorage.GetByPatientID(hospitalTreatment.PatientId)).Patient);
                hospitalizedPatient.Room = (roomStorage.GetOne(hospitalTreatment.RoomId));
                hospitalizedPatient.EndOfTreatment = hospitalTreatment.EndOfTreatment;
                hospitalizedPatient.StartOfTreatment = hospitalTreatment.StartOfTreatment;
                hospitalizedPatients.Add(hospitalizedPatient);
            }
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calendarPanel.Visibility.Equals(Visibility.Collapsed))
            {
                patientInfo.Visibility = Visibility.Visible;
                BindingExpression be = dataGrid.GetBindingExpression(DataGrid.SelectedValueProperty);
                be.UpdateSource();
            }
        }

        private void edit_Click(object sender, RoutedEventArgs e)
        {
            calendarPanel.Visibility = Visibility.Visible;
            sacuvaj.IsEnabled = true;
            izmeni.IsEnabled = false;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if(calendar.SelectedDate != null)
            {
                calendarPanel.Visibility = Visibility.Collapsed;
                sacuvaj.IsEnabled = false;
                izmeni.IsEnabled = true;
                BindingExpression be = calendar.GetBindingExpression(Calendar.SelectedDateProperty);
                be.UpdateSource();
                SaveHospitalizedTreatment();
            }
        }

        private void SaveHospitalizedTreatment()
        {
            HospitalTreatment hospitalTreatment = new HospitalTreatment();
            hospitalTreatment.EndOfTreatment = hospitalizedPatient.EndOfTreatment;
            hospitalTreatment.StartOfTreatment = hospitalizedPatient.StartOfTreatment;
            hospitalTreatment.PatientId = hospitalizedPatient.Patient.PersonalID;
            hospitalTreatment.RoomId = hospitalizedPatient.Room.Id;
            hospitalTreatmentStorage.DeleteByPatientId(hospitalizedPatient.Patient.PersonalID);
            hospitalTreatmentStorage.Save(hospitalTreatment);
        }
    }
}
