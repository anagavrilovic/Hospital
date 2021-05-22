using System;
using System.Collections.Generic;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for KartonDoktorStranica.xaml
    /// </summary>
    public partial class MedicalRecordPage : Page, INotifyPropertyChanged
    {
        private static int HOSPITAL_TREATMENT_TAB=6;
        private MedicalRecord medicalRecord;
        private MedicalRecordStorage medicalRecordStorage;
        private Appointment appointment=new Appointment();
        public MedicalRecord MedicalRecord
        {
            get { return medicalRecord; }
            set
            {
                medicalRecord = value;
            }
        }

        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public MedicalRecordPage(string id,Appointment pregled)
        {
            InitializeComponent();
            this.DataContext = this;
            InitProperties(id, pregled);
        }

        private void InitProperties(string id, Appointment pregled)
        {
            medicalRecordStorage = new MedicalRecordStorage();
            MedicalRecord = medicalRecordStorage.GetOne(id);
            this.appointment = pregled;
            saveButton.Visibility = Visibility.Collapsed;
            hospitalTreatmentButton.Visibility = Visibility.Collapsed;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        private void Save_Click(object sender, RoutedEventArgs e)
        {
            DoctorMainWindow doctorMainWindow = new DoctorMainWindow((((AppointmentWindow)Window.GetWindow(this)).LoggedInDoctor).PersonalID);
            doctorMainWindow.Show();
            Application.Current.MainWindow = doctorMainWindow;
            doctorMainWindow.Main.Navigate(new DoctorAppointments((((AppointmentWindow)Window.GetWindow(this)).LoggedInDoctor).PersonalID));
            Window.GetWindow(this).Close();
            SaveExamination();
            DeleteAppointmentOfExamination();
        }

        private void DeleteAppointmentOfExamination()
        {
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            appointmentStorage.Delete(appointment.IDAppointment);
        }

        private void SaveExamination()
        {
            MedicalRecord.AddExamination(((AppointmentWindow)Window.GetWindow(this)).Examintaion);
            medicalRecordStorage.EditRecord(MedicalRecord);
        }

        private void hospitalTreatmentButton_Click(object sender, RoutedEventArgs e)
        {
            ((AppointmentWindow)Window.GetWindow(this)).tab.SelectedIndex = HOSPITAL_TREATMENT_TAB;
            ((AppointmentWindow)Window.GetWindow(this)).hospitalTreatmentTab.IsEnabled = true;
            ((AppointmentWindow)Window.GetWindow(this)).TreatmentLabel.Foreground = Brushes.White;
        }
    }
}
