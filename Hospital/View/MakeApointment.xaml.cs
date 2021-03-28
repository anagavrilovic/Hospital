using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for MakeApointment.xaml
    /// </summary>
    public partial class MakeApointment : Window
    {
        public ObservableCollection<Appointment> appointments
        {
            get;
            set;
        }

        public MakeApointment()
        {
            InitializeComponent();
            Appointment appointment = new Appointment();
            Doctor user = new Doctor();
            user.Address = "Mihajla Pupina";
            user.City = "Subotica";
            user.Country = "Srbija";
            //user.DateOfBirth = DateTime.ParseExact("23-04-1999 ", "dd-MM-yyyy",
              //                        CultureInfo.InvariantCulture);
            user.Email = "ljubovicstefan@gmail.com";
            user.FirstName = "Stefan";
            user.Gender = Genders.male;
            user.LastName = "Ljubovic";
            user.Username = "Stefan99";
            user.Password = "stefan123";
            user.CardID = "53253435";
            user.MaritalStatus = MaritalStatus.neozenjen;
            user.Township = "Subotica";
            user.PersonalID = "2304999820057";
            appointment.doctor = new List<Doctor>();
            appointment.doctor.Add(user);
            string termin = "3/9/2021";
            DateTime vreme = appointment.DateTime;
            DateTime.TryParse(termin, out vreme);
            appointment.dateTime = vreme;
            Patient patient = new Patient();
            patient.Address = "Strazilovska";
            patient.City = "Novi Sad";
            patient.Country = "Srbija";
           // patient.DateOfBirth = DateTime.ParseExact("15-03-2003 ", "dd-MM-yyyy",
              //                         CultureInfo.InvariantCulture);
            patient.Email = "peraperic@gmail.com";
            patient.FirstName = "Petar";
            patient.Gender = Genders.male;
            patient.LastName = "Petrovic";
            patient.Username = "Petar03";
            patient.Password = "petar123";
            patient.CardID = "3533522535";
            patient.MaritalStatus = MaritalStatus.neozenjen;
            patient.Township = "Novi Sad";
            patient.PersonalID = "12321332142";
            appointment.Patient = patient;
            appointment.Type = AppointmentType.examination;
            this.DataContext = this; 
            appointments = new ObservableCollection<Appointment>();
            appointments.Add(appointment);
            dataGridPregledi.Loaded += setMinWidths;
            Room room=new Room();
            room.Id = 532553252;
            room.IsAvaliable = true;
            room.Name = "Sala za preglede";
            room.Type = RoomType.SALA_ZA_PREGLEDE;
            room.Floor = 3;
            appointment.room = room;

            
        }

        private void setMinWidths(object sender, RoutedEventArgs e)
        {
            foreach (var column in dataGridPregledi.Columns)
            {
                column.MinWidth = column.ActualWidth;
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            appointments.Remove((Appointment)dataGridPregledi.SelectedItem);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            IzmenaTermina termin = new IzmenaTermina((Appointment)dataGridPregledi.SelectedItem);
            termin.Show();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            KreiranjeTermina termin = new KreiranjeTermina();
            termin.Owner=this;
            termin.Show();
        }
    }
}
