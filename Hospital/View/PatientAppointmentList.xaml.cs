﻿using System;
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
    /// Interaction logic for PatientAppointmentList.xaml
    /// </summary>
    public partial class PatientAppointmentList : Window
    {

        public ObservableCollection<Appointment> Lista
        {
            get;
            set;
        }
        public PatientAppointmentList()
        {
            InitializeComponent();
            this.DataContext = this;
            AppointmentStorage app = new AppointmentStorage();
            Lista = app.GetByPatient(MainWindow.IDnumber);

            dataGridApp.SelectedIndex = 0;

            dataGridApp.Focus();
        }

        private void myTestKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Appointment selectedItem = (Appointment)dataGridApp.SelectedItem;
                PatientAppointmentOptions patientAppointmentOptions = new PatientAppointmentOptions(selectedItem);
                patientAppointmentOptions.Show();
                this.Close();
            }

            if (e.Key == Key.Escape)
            {
                this.Close();
            }
        }

    
    }
}