using Hospital.Controller.DoctorControllers;
using Hospital.DTO.DoctorDTO;
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
        public event PropertyChangedEventHandler PropertyChanged;
        private AppointmentDetailsController controller;
        private AppointmentDetailsDTO dTO;
        public AppointmentDetailsDTO DTO
        {
            get { return dTO; }
            set
            {
                dTO = value;
                OnPropertyChanged("DTO");
            }
        }
        public AppointmentDetails(Appointment a)
        {
            InitializeComponent();
            InitFields(a);
            this.DataContext = this;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 1 / 2);
        }

        private void InitFields(Appointment a)
        {
            DTO = new AppointmentDetailsDTO();
            controller = new AppointmentDetailsController(DTO);
            this.DTO.Appointment = a;
            double dur= DTO.Appointment.DurationInHours * 60;
            DTO.DurationInMinutes = dur.ToString() +" minuta";
            DTO.Appointment.PatientsRecord= controller.GetMedicalRecordByPatientId();
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
            MedicalRecordWindow review = new MedicalRecordWindow((controller.GetMedicalRecordByPatientId()).MedicalRecordID);
            review.Owner = this;
            this.Hide();
            review.Show();
        }

        private void startExamination_Click(object sender, RoutedEventArgs e)
        {
            AppointmentWindow examination = new AppointmentWindow(DTO.Appointment);
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
