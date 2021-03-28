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

    public partial class IzmenaTermina : Window
    {
        private Appointment termin;
        public Appointment Termin { get => termin; set => termin = value; }
        private string imePrezimePacijent;
        public string ImePrezimePacijent { get => imePrezimePacijent; set => imePrezimePacijent = value; }
        private Doctor doktor = new Doctor();
        public Doctor Doktor { get => doktor; set => doktor = value; }
        private string imePrezimeDoktor;
        public string ImePrezimeDoktor { get => imePrezimeDoktor; set => imePrezimeDoktor = value; }
        public IzmenaTermina(Appointment appointment)
        {
            this.termin = appointment;
            imePrezimePacijent = appointment.Patient.FirstName +" " + appointment.Patient.LastName;
            doktor = appointment.Doctor.First();
            imePrezimeDoktor = doktor.FirstName +" " + doktor.LastName;
            InitializeComponent();
            this.DataContext = this;
            switch (appointment.Type)
            {
                case AppointmentType.examination: tipTermina.SelectedItem = pregled; break;
                case AppointmentType.operation: tipTermina.SelectedItem = operacija; break;
         
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {

            trajanjeTermina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            sala.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            switch (tipTermina.SelectedIndex)
            {
                case 0: termin.Type = AppointmentType.examination; break;
                case 1: termin.Type = AppointmentType.operation; break;
            }
            Datum.GetBindingExpression(DatePicker.TextProperty).UpdateSource();
            this.Close();
        }
    }
}
