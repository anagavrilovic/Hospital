using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public partial class IzmenaTermina : Window, INotifyPropertyChanged
    {
        private Appointment termin;
        public Appointment Termin
        {
            get { return termin; }
            set
            {
                termin = value;
                OnPropertyChanged();
            }

        }
        private string imePrezimePacijent;
        public string ImePrezimePacijent
        {
            get { return imePrezimePacijent; }
            set
            {
                imePrezimePacijent = value;
                OnPropertyChanged();
            }

        }
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
        public ObservableCollection<Doctor> Doktori
        {
            get { return doktori; }
            set
            {
                doktori = value;
                OnPropertyChanged();
            }

        }
        private ObservableCollection<Doctor> doktori=new ObservableCollection<Doctor>();
        DoctorStorage dStorage = new DoctorStorage();
        RoomStorage rStorage = new RoomStorage();
        MakeApointment parentWindow = new MakeApointment();
        public event PropertyChangedEventHandler PropertyChanged;


        public IzmenaTermina(Appointment appointment, MakeApointment parentWindow)
        {
            this.parentWindow = parentWindow;
            this.termin = appointment;
            MedicalRecordStorage mrs = new MedicalRecordStorage();
            InitializeComponent();
            Doktori = dStorage.GetAll();
            Sobe = rStorage.GetAll();
            foreach(Room room in Sobe)
            {
                if (room.Id.Equals(Termin.Room.Id))
                {
                    sala.SelectedItem = room;
                }
            }
            foreach(Doctor doktor in Doktori)               
            {
                if (doktor.PersonalID.Equals(Termin.IDDoctor))
                {
                    doctorBox.SelectedItem = doktor;
                }
            }
            Patient patient = new Patient();
            MedicalRecord mr = new MedicalRecord();
            mr = mrs.GetByPatientID(termin.IDpatient);
            patient = mr.Patient;
            imePrezimePacijent = patient.FirstName +" " + patient.LastName;
            this.DataContext = this;
            switch (appointment.type)
            {
                case AppointmentType.examination: tipTermina.SelectedItem = pregled; break;
                case AppointmentType.operation: tipTermina.SelectedItem = operacija; break;
         
            }
        }


        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            trajanjeTermina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            switch (tipTermina.SelectedIndex)
            {
                case 0: termin.type = AppointmentType.examination; break;
                case 1: termin.type = AppointmentType.operation; break;
            }
            Datum.GetBindingExpression(DatePicker.TextProperty).UpdateSource();
            AppointmentStorage storage = new AppointmentStorage();
            storage.Delete(termin.IDAppointment);
            storage.Save(termin);
            parentWindow.dataGridPregledi.ItemsSource = storage.GetAll();
            this.Close();
        }

        private void doktorBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Doctor temp = (Doctor)doctorBox.SelectedItem;
            Termin.IDDoctor = temp.PersonalID;
        }

        private void sala_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Termin.Room = (Room)sala.SelectedItem;
        }
    }
}
