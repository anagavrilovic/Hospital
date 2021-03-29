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

    public partial class IzmenaTermina : Window
    {
        private Appointment termin;
        public Appointment Termin { get => termin; set => termin = value; }
        private string imePrezimePacijent;
        public string ImePrezimePacijent { get => imePrezimePacijent; set => imePrezimePacijent = value; }
        private Doctor doktor = new Doctor();
        public Doctor Doktor { get => doktor; set => doktor = value; }
        public IzmenaTermina(Appointment appointment)
        {
            this.termin = appointment;
           // DoctorStorage storage = new DoctorStorage();
            MedicalRecordStorage mrs = new MedicalRecordStorage();
           // doktor = storage.GetOne(termin.IDDoctor);
            Patient patient = new Patient();
            MedicalRecord mr = new MedicalRecord();
            mr = mrs.GetByPatientID(termin.IDpatient);
            patient = mr.Patient;
            imePrezimePacijent = patient.FirstName +" " + patient.LastName;
            InitializeComponent();
            this.DataContext = this;
            switch (appointment.type)
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
            doctorBox.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            trajanjeTermina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            sala.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            switch (tipTermina.SelectedIndex)
            {
                case 0: termin.type = AppointmentType.examination; break;
                case 1: termin.type = AppointmentType.operation; break;
            }
            Datum.GetBindingExpression(DatePicker.TextProperty).UpdateSource();
            AppointmentStorage storage = new AppointmentStorage();
            storage.Delete(termin.IDAppointment);
            storage.Save(termin);
            this.Close();
        }
    }
}
