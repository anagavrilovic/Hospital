﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for DetaljiPregleda.xaml
    /// </summary>
    public partial class DetaljiPregleda : Window,INotifyPropertyChanged
    {
        Appointment appointment = new Appointment();

        public event PropertyChangedEventHandler PropertyChanged;

        public Appointment Appointment
        {
            get { return appointment; }
            set
            {
                appointment = value;
                OnPropertyChanged();
            }
        }
        private string durationInMinutes;
        public string DurationInMinutes
        {
            get { return durationInMinutes; }
            set
            {
                durationInMinutes = value;
                OnPropertyChanged();
            }
        }

        public DetaljiPregleda(Appointment a)
        {
            this.Appointment = a;
            InitializeComponent();
            InitFields();
            this.DataContext = this;
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 1 / 2);
        }

        private void InitFields()
        {
           double dur= Appointment.DurationInHours * 60;
            durationInMinutes = dur.ToString() +" minuta";
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void MedicalRecordReview(object sender, RoutedEventArgs e)
        {
            MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
            DoktorKarton review = new DoktorKarton((medicalRecordStorage.GetByPatientID(Appointment.IDpatient)).MedicalRecordID);
            review.Owner = this;
            this.Hide();
            review.Show();
        }

        private void startExamination_Click(object sender, RoutedEventArgs e)
        {
            Doctor_Examination examination = new Doctor_Examination(appointment);
            examination.Show();
            examination.Owner=(Window.GetWindow(this) ).Owner;
            this.Close();

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Owner.Show();
            this.Close();
        }
    }
}
