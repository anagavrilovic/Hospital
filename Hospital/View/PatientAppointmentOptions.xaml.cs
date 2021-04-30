﻿using Hospital.Model;
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
    /// Interaction logic for PatientAppointmentOptions.xaml
    /// </summary>
    public partial class PatientAppointmentOptions : Window
    {
        private Appointment app;
        private PatientSettingsStorage patientSettingsStorage = new PatientSettingsStorage();
        public PatientAppointmentOptions(Appointment app)
        {
            InitializeComponent();
            this.app = app;
        }

        private void Izmeni(object sender, RoutedEventArgs e)
        {
            DateTime currentTime = DateTime.Now;
            DoctorStorage doctorStorage = new DoctorStorage();
            Hospital.Model.Doctor d = doctorStorage.GetOne(app.IDDoctor);
            if (d.Specialty != DoctorSpecialty.general)
            {
                MessageBox.Show("Nije moguce pomeriti termin kod specijaliste");
            }
            else if (!patientSettingsStorage.isSchedulingAllowed())
            {
                MessageBox.Show("Previše puta ste zakazali / pomerili termin u kratkom vremenskom periodu.");
            }
            else if ((app.DateTime-currentTime).TotalDays >= 2)
            {
                PatientChangeAppointment patientChangeAppointment = new PatientChangeAppointment(app);
                patientChangeAppointment.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nije moguce otkazati termin tako kasno");
            }

            
        }

        private void Nazad(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Otkazi(object sender, RoutedEventArgs e)
        {
            AppointmentStorage aps = new AppointmentStorage();

            if ((app.DateTime - DateTime.Now).TotalDays >= 2)
            {
                aps.Delete(app.IDAppointment);
                this.Close();
            }
            else
            {
                MessageBox.Show("Nije moguce otkazati termin");
            }

            
            
        }
    }
}