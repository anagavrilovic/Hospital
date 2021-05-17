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
        private ObservableCollection<HospitalTreatment> hospitalTreatments = new ObservableCollection<HospitalTreatment>();
        private HospitalTreatmentStorage hospitalTreatmentStorage = new HospitalTreatmentStorage();
        private RoomStorage roomStorage = new RoomStorage();
        private HospitalTreatment hospitalTreatment = new HospitalTreatment();
        private MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<HospitalTreatment> HospitalTreatments
        {
            get { return hospitalTreatments; }
            set { hospitalTreatments = value; }
        }
        public HospitalTreatment HospitalTreatment
        {
            get
            {
                return hospitalTreatment;
            }
            set
            {
                if (value != null)
                {
                    hospitalTreatment = value;
                    OnPropertyChanged("HospitalTreatment");
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
            InitProperties();
        }

        private void InitProperties()
        {
            DateTime startDate = DateTime.Today;
            calendar.DisplayDateStart = startDate;
        }

        private void FillTable()
        {
            foreach(HospitalTreatment hospitalTreatmentFromStorage in hospitalTreatmentStorage.GetAll())
            {
                hospitalTreatmentFromStorage.PatientsRecord = medicalRecordStorage.GetByPatientID(hospitalTreatmentFromStorage.PatientId);
                hospitalTreatmentFromStorage.Room = roomStorage.GetOne(hospitalTreatmentFromStorage.RoomId);
                hospitalTreatments.Add(hospitalTreatmentFromStorage);
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
            saveButton.IsEnabled = true;
            editButton.IsEnabled = false;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if(calendar.SelectedDate != null)
            {
                calendarPanel.Visibility = Visibility.Collapsed;
                saveButton.IsEnabled = false;
                editButton.IsEnabled = true;
                BindingExpression be = calendar.GetBindingExpression(Calendar.SelectedDateProperty);
                be.UpdateSource();
                SaveHospitalizedTreatment();
            }
        }

        private void SaveHospitalizedTreatment()
        {
            hospitalTreatmentStorage.DeleteByPatientId(hospitalTreatment.PatientId);
            hospitalTreatmentStorage.Save(hospitalTreatment);
        }
    }
}
