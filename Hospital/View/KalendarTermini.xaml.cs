﻿using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for KalendarTermini.xaml
    /// </summary>
    public partial class KalendarTermini : Window, INotifyPropertyChanged
    {
        private DoctorStorage dStorage = new DoctorStorage();
        private AppointmentStorage aStorage = new AppointmentStorage();
        private Doctor doctor = new Doctor();
        public Doctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public KalendarTermini(string IDnumber)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            Doctor=dStorage.GetOne(IDnumber);
            InitAppointments();
        }

        private void InitAppointments()
        {
            foreach(Appointment a in aStorage.GetAll())
            {
                if (a.IDDoctor.Equals(Doctor.PersonalID))
                {
                    Appointments.Add(a);
                }
            }
            
        }

        private void dataGridPregledi_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            DetaljiPregleda d = new DetaljiPregleda((Appointment)dataGridPregledi.SelectedItem);
            d.Owner = this;
            d.Show();
            this.Hide();
        }
    }
}
