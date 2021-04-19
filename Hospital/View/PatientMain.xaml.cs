using Hospital.Model;
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
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientMain.xaml
    /// </summary>
    public partial class PatientMain : Window
    {
        public PatientMain()
        {
            InitializeComponent();
            Boolean newNotification=false;
            PatientNotificationsStorage patientNotificationsStorage = new PatientNotificationsStorage();
            //  patientNotificationsStorage.SaveAll();
            ObservableCollection<PatientTherapyMedicineNotification> lista = patientNotificationsStorage.GetAll();
            foreach(PatientTherapyMedicineNotification ptm in lista)
            {
                if((ptm.FromDate.Date <= DateTime.Now.Date) && (ptm.ToDate.Date >= DateTime.Now.Date)){
                    if(ptm.LastRead.Date<= DateTime.Now.Date)
                    {
                        string[] times = ptm.Times.Split(' ');
                        for(int i = 0; i < times.Length-1; i += 2)
                        {
                            
                           TimeSpan ts = TimeSpan.Parse(times[i]);


                            DateTime dt = DateTime.Now.Date + ts;
                            if (times[i + 1].Equals("PM")) dt=dt.AddHours(12);
                            if ((ptm.LastRead < dt) && (dt <= DateTime.Now))
                            {
                                newNotification = true;
                                ptm.Read = false;
                                patientNotificationsStorage.Delete(ptm.ID);
                                patientNotificationsStorage.Save(ptm);
                            }
                        }
                    }
                }
            }
            if (newNotification)
            {
                MessageBox.Show("Imate novo obavestenje!");
            }
        }

        private void AppointmentList(object sender, RoutedEventArgs e)
        {
            PatientAppointmentList patientAppointmentList = new PatientAppointmentList();
            patientAppointmentList.Show();
        }

        private void MakeAnAppointment(object sender, RoutedEventArgs e)
        {
            PatientChooseDatesForAnAppointment patientChooseDatesForAnAppointment = new PatientChooseDatesForAnAppointment();
            patientChooseDatesForAnAppointment.Show();
        }

        private void PassedAppointmentList(object sender, RoutedEventArgs e)
        {
            PatientPassedAppointmentList patientPassedAppointmentList = new PatientPassedAppointmentList();
            patientPassedAppointmentList.Show();
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            PatientSettingsPage patientSettings = new PatientSettingsPage();
            patientSettings.Show();
        }

        private void Notifications(object sender, RoutedEventArgs e)
        {
            PatientNotifications patientNotifications = new PatientNotifications();
            patientNotifications.Show();
        }
    }
}
