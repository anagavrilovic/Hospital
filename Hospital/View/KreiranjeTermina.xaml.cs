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
                trajanjeTermina.GetBindingExpression(TextBox.TextProperty).UpdateSource();
                Termin.IDpatient = Pacijent.PersonalID;
                Termin.patientName = Pacijent.FirstName;
                Termin.patientSurname = Pacijent.LastName;
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

        private void sale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Termin.Room= (Room)sale.SelectedItem;
        }

        private void doktorIme_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Doctor temp = (Doctor)doktorIme.SelectedItem;
            Termin.IDDoctor = temp.PersonalID;
        }

        private void dodajPacijenta(object sender, RoutedEventArgs e)
        {
            PacijentListBox pacijentLB = new PacijentListBox(this);
            pacijentLB.ShowDialog();
        }
    }
}
