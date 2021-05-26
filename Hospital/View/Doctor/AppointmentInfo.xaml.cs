using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace Hospital.View.Doctor
{

    public partial class AppointmentInfo : Page, INotifyPropertyChanged
    {
        private ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> Appointments
        {
            get => appointments;
            set
            {
                appointments = value;
            }
        }
        private double _durationInHours;
        public double DurationInHours 
        { 
            get => _durationInHours;
            set 
            {
                _durationInHours = value; 
            }
        }
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        private string patientId;
        public event PropertyChangedEventHandler PropertyChanged;
        private AppointmentService appointmentService;

        public AppointmentInfo(Model.Doctor doctor,string patientId)
        {
            InitializeComponent();
            this.DataContext = this;
            InitProperties(doctor, patientId);
            dataGridPregledi.Loaded += SetDataGridColumnWidth;
        }

        private void InitProperties(Model.Doctor doctor, string patientId)
        {
            appointmentService = new AppointmentService();
            this.patientId = patientId;
            ComboBox.ItemsSource = Enum.GetValues(typeof(DoctorSpecialty)).Cast<DoctorSpecialty>();
            ComboBox.SelectedIndex = (int)doctor.Specialty;
        }

        private void SetDataGridColumnWidth(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGridPregledi.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void AddAppointment(object sender, RoutedEventArgs e)
        {
            CreateAppointment appointmentWindow = new CreateAppointment(this,patientId,ComboBox.SelectedIndex);
            addButton.IsEnabled = false;
            appointmentWindow.Owner = Window.GetWindow(this);
            appointmentWindow.Show();
        }

        private void SetAppointmentsInDataGrid(object sender, SelectionChangedEventArgs e)
        {
           Appointments=new ObservableCollection<Appointment>(appointmentService.SetAppointmentDataGrid((DoctorSpecialty)ComboBox.SelectedItem));
            dataGridPregledi.ItemsSource = Appointments;
            dataGridPregledi.Items.Refresh();
        }
    }
}
