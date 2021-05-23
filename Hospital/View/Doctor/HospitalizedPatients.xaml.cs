using Hospital.Model;
using Hospital.Services.DoctorServices;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Hospital.View.Doctor
{
    public partial class HospitalizedPatients : Page,INotifyPropertyChanged
    {
        private HospitalizedPatientsService service=new HospitalizedPatientsService();
        private ObservableCollection<HospitalTreatment> hospitalTreatments = new ObservableCollection<HospitalTreatment>();
        private HospitalTreatment hospitalTreatment = new HospitalTreatment();

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
        public HospitalizedPatients()
        {
            InitializeComponent();
            this.DataContext = this;
            InitProperties();
        }

        private void InitProperties()
        {
            DateTime startDate = DateTime.Today;
            calendar.DisplayDateStart = startDate;
            hospitalTreatments = service.SetTreatments();
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
            service.EditHospitalTreatment(HospitalTreatment);
        }
    }
}
