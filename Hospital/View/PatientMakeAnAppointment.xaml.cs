using System;
using System.Collections.Generic;
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
    /// Interaction logic for PatientMakeAnAppointment.xaml
    /// </summary>
    public partial class PatientMakeAnAppointment : Window
    {
        Boolean izmena;
        String appForDelete;
      
        public PatientMakeAnAppointment()
        {
            InitializeComponent();
            izmena = false;
        }
        public PatientMakeAnAppointment(String appForDelte)
        {
            InitializeComponent();
            this.appForDelete = appForDelte;
            izmena = true;
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Schedule(object sender, RoutedEventArgs e)
        {
            String datumVreme = date.Text.Trim()+" " + time.Text;
            DateTime datum;
            try
            {
                 datum = DateTime.Parse(datumVreme);
            }
            catch
            {
                MessageBox.Show("Nije dobro unesen datum/vreme.\nDatum:mm/dd/yyyy \nVreme:H:mm:ss AM(PM)");
                return;
            }
            Appointment appointment = new Appointment { DateTime = datum, type = AppointmentType.examination };
            
            if (checkbox1.IsChecked!=true)
            {
                MessageBox.Show("Morate odabrati doktora");
                return;
            }
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            appointment.IDDoctor = "123";
            appointment.DoctrosNameSurname = "Petar Petrovic";
            appointment.IDAppointment = appointmentStorage.GetNewID();
            MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
            appointment.IDpatient = medicalRecordStorage.GetOne(1).Patient.PersonalID;
            RoomStorage roomStorage = new RoomStorage();
            appointment.Room = roomStorage.GetOne(1);
            
            appointmentStorage.Save(appointment);
            this.Close();
            if (izmena)
            {
                appointmentStorage.Delete(appForDelete);
            }
        }
    }
}
