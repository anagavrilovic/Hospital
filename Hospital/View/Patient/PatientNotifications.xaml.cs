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
        private PatientTherapyNotificationService patientTherapyNotificationService = new PatientTherapyNotificationService();
        public ObservableCollection<IPatientNotification> NotificationList
        {
            get;
            set;
        }
        public PatientNotifications()
        {
            InitializeComponent();
            this.DataContext = this;
            List<PatientTherapyMedicineNotification> therapyNotifications = patientTherapyNotificationService.GetByPatientID();
            therapyNotifications = patientTherapyNotificationService.SetNotifactionsActivity(therapyNotifications);
            NotificationList = new ObservableCollection<IPatientNotification>(therapyNotifications);
            ObservableCollection<IPatientNotification> auxiliaryList = new ObservableCollection<IPatientNotification>(patientNotesNotificationService.GetByPatientID());
            foreach(IPatientNotification pt in auxiliaryList)
            {
                NotificationList.Add(pt);
            }
            dataGridApp.SelectedIndex = 0;

            dataGridApp.Focus();
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
                    PatientTherapyNotificationService patientNotificationsService = new PatientTherapyNotificationService();
                    patientNotificationsService.Update(selectedItem);
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
