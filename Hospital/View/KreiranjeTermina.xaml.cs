using Hospital.Model;
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
            this.DataContext = this;
            Termin = new Appointment();
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
            Boolean wrongPatient = true;
            MedicalRecordStorage mStorage = new MedicalRecordStorage();
            foreach (MedicalRecord record in mStorage.GetAll())
            {
                if (record.Patient.FirstName.Equals(Termin.patientName) && record.Patient.LastName.Equals(Termin.patientSurname))
                {
                    Termin.IDpatient = record.Patient.PersonalID;
                    wrongPatient = false;
                }
            }
            if (wrongPatient)
            {
                
                    MessageBox.Show("Uneli ste nepostojeceg pacijenta");
             }
            else { 
                pacijentIme.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                pacijentPrezime.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                trajanjeTermina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                switch (tipTermina.SelectedIndex)
                {
                    case 0: Termin.type = AppointmentType.examination; break;
                    case 1: Termin.type = AppointmentType.operation; break;
                }
                Datum.GetBindingExpression(DatePicker.TextProperty).UpdateSource();
                Random random = new Random();
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

        private void doktorIme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Doctor temp = (Doctor)doktorIme.SelectedItem;
            Termin.IDDoctor = temp.PersonalID;
        }
    }
}
