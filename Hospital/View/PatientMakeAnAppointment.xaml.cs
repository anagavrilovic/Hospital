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
        public PatientMakeAnAppointment()
        {
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Schedule(object sender, RoutedEventArgs e)
        {
            String datumVreme = date.Text + time.Text;
            DateTime datum = DateTime.Parse(datumVreme);
            Appointment appointment = new Appointment { DateTime = datum, type = AppointmentType.examination };
            appointment.IDDoctor = "123";
            MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
            appointment.IDpatient = medicalRecordStorage.GetOne(1).Patient.PersonalID;
            RoomStorage roomStorage = new RoomStorage();
            appointment.Room = roomStorage.GetOne(1);
            AppointmentStorage appointmentStorage = new AppointmentStorage();
            appointmentStorage.Save(appointment);
        }
    }
}
