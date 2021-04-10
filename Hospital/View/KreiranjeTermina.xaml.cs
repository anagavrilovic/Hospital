﻿using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
  
    public partial class KreiranjeTermina : Window,INotifyPropertyChanged
    {
        private ObservableCollection<Doctor> doktori;
        private Appointment termin;
        public Appointment Termin { get => termin; set => termin = value; } 
        private ObservableCollection<Room> sobe;
        public ObservableCollection<Room> Sobe
        {
            get { return sobe; }
            set
            {
                sobe = value;
                OnPropertyChanged();
            }

        }
        private string vremeTermin;
        public string VremeTermin
        {
            get { return vremeTermin; }
            set
            {
                vremeTermin = value;
                OnPropertyChanged();
            }

        }
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set
            {
                date = value;
                OnPropertyChanged();
            }

        }
        private Doctor doktor;
        public Doctor Doktor
        {
            get { return doktor; }
            set
            {
                doktor = value;
                OnPropertyChanged();
            }

        }
        private DateTime time = DateTime.Today;
        private Patient pacijent;
        public Patient Pacijent
        {
            get { return pacijent; }
            set
            {
                pacijent = value;
                OnPropertyChanged();
            }

        }
        public ObservableCollection<Doctor> Doktori  
            {
                get{ return doktori;}
                set {
                doktori = value;
                OnPropertyChanged();
            }

            }
        

        public MakeApointment parentAppointment { get; set; }
        AppointmentStorage storage = new AppointmentStorage();
        DoctorStorage dStorage = new DoctorStorage();
        MedicalRecordStorage mStorage = new MedicalRecordStorage();
        RoomStorage rStorage = new RoomStorage();
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if(PropertyChanged != null)
            {
               this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public KreiranjeTermina(MakeApointment parentWindow)
        {
            InitializeComponent();
            Doktor = new Doctor();
            Pacijent = new Patient();
            Termin = new Appointment();
            this.DataContext = this;
            for (DateTime tm = time.AddHours(0); tm < time.AddHours(24); tm = tm.AddMinutes(30))
            {
                vreme.Items.Add(tm.ToShortTimeString());

            }
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            parentAppointment = parentWindow;
            Doktori = dStorage.GetAll();
            Sobe = rStorage.GetAll();

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (Pacijent == null || Date == null || VremeTermin == null || Doktor == null)
            {
                MessageBox.Show("Nisu uneti svi podace");
            }
            else
            {
                MessageBox.Show(Pacijent.FirstName);
                Termin.patientName = Pacijent.FirstName;
                Termin.IDpatient = Pacijent.PersonalID;
                Termin.patientSurname = Pacijent.LastName;
                Termin.DateTime = Date.Date.Add(DateTime.Parse(vremeTermin).TimeOfDay);
                Random random = new Random();
                Termin.IDDoctor = Doktor.PersonalID;
                Termin.DoctrosNameSurname = Doktor.FirstName + " " + Doktor.LastName;
                Termin.IDAppointment = random.Next(0, 10000).ToString();
                storage.Save(Termin);
                parentAppointment.dataGridPregledi.ItemsSource = storage.GetAll();
                this.Close();
            }

        }

        private void sale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Termin.Room= (Room)sale.SelectedItem;
        }


        private void dodajPacijenta(object sender, RoutedEventArgs e)
        {
            PacijentListBox pacijentLB = new PacijentListBox(this);
            pacijentLB.Owner = this;
            pacijentLB.ShowDialog();
        }

    }
}