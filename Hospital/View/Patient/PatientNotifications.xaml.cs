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
        private MedicalRecordService medicalRecordService = new MedicalRecordService();
        private NotificationService notificationService = new NotificationService();
        private String username;
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
            username = medicalRecordService.GetUsernameByIDPatient(MainWindow.IDnumber);
            List<NotificationsUsers> generalNotifications = notificationService.GetNotificationByUser(username);
            foreach (NotificationsUsers notification in generalNotifications)
            {
                    NotificationList.Add(new NotificationsUsersAdapter(notification));
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

                    UpdateTherapyNotification();
                }
                else if(dataGridApp.SelectedItem.GetType() == typeof(PatientNotesNotification))
                {
                    UpdateNoteNotification();
                }
                else
                {
                    UpdateGeneralNotification();
                }

            }

            if (e.Key == Key.Subtract)
            {
                if (dataGridApp.SelectedItem.GetType() == typeof(PatientTherapyMedicineNotification))
                {
                    PatientTherapyMedicineNotification selectedItem = (PatientTherapyMedicineNotification)dataGridApp.SelectedItem;
                    if (MessageBox.Show("Da li ste sigurni da želite da obrišete obaveštenje?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
                    NotificationList.Remove(selectedItem);
                    patientTherapyNotificationService.Delete(selectedItem.ID);
                    dataGridApp.Focus();
                }
                else if (dataGridApp.SelectedItem.GetType() == typeof(PatientNotesNotification))
                {
                    PatientNotesNotification selectedItem = (PatientNotesNotification)dataGridApp.SelectedItem;
                    if (MessageBox.Show("Da li ste sigurni da želite da obrišete obaveštenje?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
                    NotificationList.Remove(selectedItem);
                    patientNotesNotificationService.Delete(selectedItem.ID);
                    dataGridApp.Focus();
                }
                else
                {
                    NotificationsUsersAdapter selectedItem = (NotificationsUsersAdapter)dataGridApp.SelectedItem;
                    if (MessageBox.Show("Da li ste sigurni da želite da obrišete obaveštenje?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
                    NotificationList.Remove(selectedItem);
                    notificationService.DeleteUniqueNotificationsUsers(selectedItem.ID, username);
                    dataGridApp.Focus();
                }
            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.Navigate(new PatientMenu());
            }
        }

        private void UpdateTherapyNotification()
        {
            PatientTherapyMedicineNotification selectedItem = (PatientTherapyMedicineNotification)dataGridApp.SelectedItem;
            selectedItem.LastRead = DateTime.Now;
            selectedItem.Read = true;
            patientTherapyNotificationService.Update(selectedItem);
            PatientTherapy patientTherapy = new PatientTherapy(selectedItem);
            this.NavigationService.Navigate(patientTherapy);
        }

        private void UpdateNoteNotification()
        {
            PatientNotesNotification selectedItem = (PatientNotesNotification)dataGridApp.SelectedItem;
            selectedItem.LastRead = DateTime.Now;
            selectedItem.Read = true;
            patientNotesNotificationService.Update(selectedItem);
            this.NavigationService.Navigate(new PatientNoteNotification((PatientNotesNotification)dataGridApp.SelectedItem));
        }

        private void UpdateGeneralNotification()
        {
            NotificationsUsersAdapter selectedItem = (NotificationsUsersAdapter)dataGridApp.SelectedItem;
            NotificationsUsers notificationsUsers = notificationService.GetUniqueNotificationsUsers(selectedItem.ID, username);
            notificationsUsers.Read = true;
            notificationService.UpdateNotificationsUsers(notificationsUsers);
            this.NavigationService.Navigate(new PatientGeneralNotification(selectedItem));
        }
    }
}
