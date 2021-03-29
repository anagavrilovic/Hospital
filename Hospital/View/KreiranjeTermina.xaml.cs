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
  
    public partial class KreiranjeTermina : Window
    {
        private Appointment termin;
        public Appointment Termin { get => termin; set => termin = value; } 
        public string ImeDoktor { get; set; }
        public string ImeSale { get; set; }
        public MakeApointment parentAppointment { get; set; }
        AppointmentStorage storage = new AppointmentStorage();
        DoctorStorage dStorage = new DoctorStorage();

        public KreiranjeTermina(MakeApointment parentWindow)
        {
            InitializeComponent();
            this.DataContext = this;
            Termin = new Appointment();
            parentAppointment = parentWindow;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            pacijentIme.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            pacijentPrezime.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Termin.Room = new Room();
            MedicalRecordStorage mStorage = new MedicalRecordStorage();
            foreach(MedicalRecord record in mStorage.GetAll())
            {
                if(record.Patient.FirstName.Equals(Termin.patientName) && record.Patient.LastName.Equals(Termin.patientSurname))
                {
                    Termin.IDpatient = record.Patient.PersonalID;
                }
            }
            doktorIme.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            trajanjeTermina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            sala.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            switch (tipTermina.SelectedIndex)
            {
                case 0: Termin.type = AppointmentType.examination; break;
                case 1: Termin.type = AppointmentType.operation; break;
            }
            Datum.GetBindingExpression(DatePicker.TextProperty).UpdateSource();
            Termin.IDAppointment = "300";
            sala.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Termin.Room.Name = ImeSale;
            storage.Save(Termin);
            parentAppointment.dataGridPregledi.ItemsSource = storage.GetAll();
            this.Close();
        }
    }
}
