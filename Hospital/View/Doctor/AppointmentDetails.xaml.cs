using Hospital.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DetaljiPregleda.xaml
    /// </summary>
    public partial class AppointmentDetails : Window,INotifyPropertyChanged
    {
        private MedicalRecordService medicalRecordService;
        Appointment appointment = new Appointment();
        public event PropertyChangedEventHandler PropertyChanged;

        public Appointment Appointment
        {
            get { return appointment; }
            set
            {
                appointment = value;
                OnPropertyChanged();
            }
        }
        private string durationInMinutes;
        public string DurationInMinutes
        {
            get { return durationInMinutes; }
            set
            {
                durationInMinutes = value;
                OnPropertyChanged();
            }
        }

        public AppointmentDetails(Appointment a)
        {
            this.Appointment = a;
            InitializeComponent();
            InitFields();
            this.DataContext = this;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 1 / 2);
        }

        private void InitFields()
        {
            medicalRecordService = new MedicalRecordService();
           double dur= Appointment.DurationInHours * 60;
            durationInMinutes = dur.ToString() +" minuta";
           appointment.PatientsRecord= medicalRecordService.GetByPatientId(Appointment.IDpatient);
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void MedicalRecordReview(object sender, RoutedEventArgs e)
        {
            MedicalRecordWindow review = new MedicalRecordWindow((medicalRecordService.GetByPatientId(Appointment.IDpatient)).MedicalRecordID);
            review.Owner = this;
            this.Hide();
            review.Show();
        }

        private void startExamination_Click(object sender, RoutedEventArgs e)
        {
            AppointmentWindow examination = new AppointmentWindow(appointment);
            examination.Show();
            Application.Current.MainWindow = examination;
            this.Close();
            examination.Owner = Window.GetWindow(this.Owner);
            Window.GetWindow(this.Owner).Hide();

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
