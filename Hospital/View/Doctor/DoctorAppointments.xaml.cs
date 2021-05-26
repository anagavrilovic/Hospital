using Hospital.Model;
using Hospital.Services;
using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// <summary>
    /// Interaction logic for KalendarTermini.xaml
    /// </summary>
    public partial class DoctorAppointments : Page, INotifyPropertyChanged
    {
        private AppointmentService AppointmentService = new AppointmentService();
        private MedicalRecordService medicalRecordService = new MedicalRecordService();
        private DoctorService doctorService = new DoctorService();
        private Model.Doctor doctor = new Model.Doctor();
        public Model.Doctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public DoctorAppointments(string IDnumber)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            Doctor=doctorService.GetDoctorById(IDnumber);
            Appointments =new ObservableCollection<Appointment>(AppointmentService.InitAppointments(IDnumber));
        }


        private void dataGridAppointments_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dataGridAppointments.SelectedItem != null)
            {
                AppointmentDetails review = new AppointmentDetails((Appointment)dataGridAppointments.SelectedItem);
                review.Owner = Window.GetWindow(this);
                review.Show();
                Window.GetWindow(this).Hide();
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridAppointments.SelectedIndex != -1)
            {
                Appointment selectedAppointment = (Appointment)dataGridAppointments.SelectedItem;
                AppointmentService.DeleteAppointment(selectedAppointment.IDAppointment);
                MedicalRecord medicalRecord=medicalRecordService.GetByPatientId(selectedAppointment.IDpatient);
                if (medicalRecord != null)
                {
                    medicalRecordService.DeleteAppointmentFromExamination(selectedAppointment.IDAppointment, medicalRecord);
                }
                appointments.Remove((Appointment)dataGridAppointments.SelectedItem);
                dataGridAppointments.ItemsSource = appointments;
                dataGridAppointments.Items.Refresh();
            }
        }

        private void addAppointment_Click(object sender, RoutedEventArgs e)
        {
            NewAppointment newAppointment =new NewAppointment(this, doctor.PersonalID);
            newAppointment.Show();
        }
    }
}
