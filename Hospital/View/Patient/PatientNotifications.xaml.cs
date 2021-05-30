using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientNotifications.xaml
    /// </summary>
    public partial class PatientNotifications : Page
    {
        private PatientNotesNotificationService patientNotesNotificationService = new PatientNotesNotificationService();
        public ObservableCollection<PatientNotification> Lista
        {
            get;
            set;
        }
        public PatientNotifications()
        {
            InitializeComponent();
            this.DataContext = this;
            //MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
            // MedicalRecord medicalRecord = medicalRecordStorage.GetByPatientID(MainWindow.IDnumber);
            PatientNotificationsStorage patientNotificationsStorage = new PatientNotificationsStorage();
            Lista = new ObservableCollection<PatientNotification>(patientNotificationsStorage.GetAll());
            ObservableCollection<PatientNotification> auxiliaryList = new ObservableCollection<PatientNotification>(patientNotesNotificationService.GetByPatientID());
            foreach(PatientNotification pt in auxiliaryList)
            {
                Lista.Add(pt);
            }
            dataGridApp.SelectedIndex = 0;

            dataGridApp.Focus();
            /*    foreach(Examination e in medicalRecord.Examination)
                {

                    foreach(Medicine med in e.therapy.Medicine)
                    {
                        PatientTherapyMedicineNotification patientTherapyMedicineNotification = new PatientTherapyMedicineNotification();
                        patientTherapyMedicineNotification.Name = e.therapy.name+": "+med.Name;
                        for(int i = 0; i < med.TimesPerDay; i++)
                        {
                            TimeSpan ts = new TimeSpan(i*3 + 10, 0, 0);
                            DateTime dateTime = DateTime.Now.Date + ts;
                            patientTherapyMedicineNotification.updateTimes(dateTime);
                        }
                        patientTherapyMedicineNotification.Read = false;
                        DateTime date1 = e.appointment.DateTime;
                        DateTime date2 = date1.AddDays(med.DurationInDays);
                        patientTherapyMedicineNotification.FromDate = date1;
                        patientTherapyMedicineNotification.ToDate = date2;
                        patientTherapyMedicineNotification.updateDuration();
                        if ((date1.Date <= DateTime.Now) && (date2.Date>=DateTime.Now))
                        {
                            patientTherapyMedicineNotification.Active = true;
                        }
                        else
                        {
                            patientTherapyMedicineNotification.Active = false;
                        }
                        Lista.Add(patientTherapyMedicineNotification);
                    }
                }*/
        }

        private void myTestKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                if (dataGridApp.SelectedItem.GetType() == typeof(PatientTherapyMedicineNotification))
                {
                    PatientTherapyMedicineNotification selectedItem = (PatientTherapyMedicineNotification)dataGridApp.SelectedItem;
                    selectedItem.LastRead = DateTime.Now;
                    selectedItem.Read = true;
                    PatientNotificationsStorage patientNotificationsStorage = new PatientNotificationsStorage();
                    patientNotificationsStorage.Delete(selectedItem.ID);
                    patientNotificationsStorage.Save(selectedItem);
                    PatientTherapy patientTherapy = new PatientTherapy(selectedItem);
                    this.NavigationService.Navigate(patientTherapy);
                }
                else
                {
                    PatientsNote patientsNote = new PatientsNote((PatientNotesNotification)dataGridApp.SelectedItem);
                    this.NavigationService.Navigate(patientsNote);
                }

            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.GoBack();
            }
        }
    }
}
