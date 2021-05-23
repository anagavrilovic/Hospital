using Hospital.Model;
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
        private DoctorStorage doctorStorage = new DoctorStorage();
        private AppointmentStorage appointmentStorage = new AppointmentStorage();
        private MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
        private Hospital.Model.Doctor doctor = new Hospital.Model.Doctor();
        public Hospital.Model.Doctor Doctor
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
            Doctor=doctorStorage.GetOne(IDnumber);
            InitAppointments();
        }

        private void InitAppointments()
        {
            foreach(Appointment a in appointmentStorage.GetAll())
            {
                if (a.IDDoctor.Equals(Doctor.PersonalID))
                {
                    a.Doctor = doctorStorage.GetOne(a.IDDoctor);
                    a.PatientsRecord = medicalRecordStorage.GetByPatientID(a.IDpatient);
                    Appointments.Add(a);
                }
            }
            
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
                appointmentStorage.Delete(((Appointment)dataGridAppointments.SelectedItem).IDAppointment);
                MedicalRecord medicalRecord=medicalRecordStorage.GetByPatientID(((Appointment)dataGridAppointments.SelectedItem).IDpatient);
                if (medicalRecord != null)
                {
                    foreach (Examination examination in medicalRecord.Examination)
                    {
                        if (examination.appointment != null)
                        {
                            if (examination.appointment.IDAppointment.Equals(((Appointment)dataGridAppointments.SelectedItem).IDAppointment))
                            {
                                medicalRecord.RemoveExamination(examination);
                                break;
                            }
                        }
                    }
                }
                medicalRecordStorage.EditRecord(medicalRecord);
                appointments.Remove((Appointment)dataGridAppointments.SelectedItem);
            }
        }

        private void addAppointment_Click(object sender, RoutedEventArgs e)
        {
            NewAppointment newAppointment =new NewAppointment(this, doctor.PersonalID);
            newAppointment.Show();
        }
    }
}
