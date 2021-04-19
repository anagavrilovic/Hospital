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
        private Room soba;
        public Room Soba
        {
            get { return soba; }
            set
            {
                soba = value;
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
        private string trajanjeMinuti;
        public string TrajanjeMinuti
        {
            get { return trajanjeMinuti; }
            set
            {
                trajanjeMinuti = value;
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

        public KreiranjeTermina(MakeApointment parentWindow,string id)
        {
            InitializeComponent();
            this.DataContext = this;
            Pacijent = new Patient();
            Pacijent = (mStorage.GetByPatientID(id)).Patient;
            Termin = new Appointment();
            Doktor = new Doctor();
            for (DateTime tm = time.AddHours(0); tm < time.AddHours(24); tm = tm.AddMinutes(30))
            {
                vreme.Items.Add(tm.ToShortTimeString());

            }
            for(int i = 0; i < 15; i++)
            {
                trajanje.Items.Add(((i + 1) * 10));
            }
            trajanje.SelectedIndex = 1;
            vreme.SelectedIndex = 7;
            
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            parentAppointment = parentWindow;
            Doktori = dStorage.GetAll();
            

        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            parentAppointment.dodaj.IsEnabled = true;
            Close();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (Pacijent == null || Date == null || VremeTermin == null || Doktor == null || (rdbOperacija.IsChecked.Equals(false) && rdbPregled.IsChecked.Equals(false)))
            {
                
                MessageBox.Show("Nisu uneti svi podace");
            }
            else
            {
                Termin.patientName = Pacijent.FirstName;
                Termin.IDpatient = Pacijent.PersonalID;
                Termin.patientSurname = Pacijent.LastName;
                Termin.DateTime = Date.Date.Add(DateTime.Parse(vremeTermin).TimeOfDay);
                DateTime apStart = Date.Date.Add(DateTime.Parse(vremeTermin).TimeOfDay);
                double v=double.Parse(TrajanjeMinuti);
                Termin.DurationInHours=v / 60;
                DateTime apEnd = apStart.AddMinutes(v);    
                Termin.IDDoctor = Doktor.PersonalID;
                foreach(Appointment a in storage.GetAll())
                {
                    if (a.IDDoctor.Equals(Doktor.PersonalID))
                    {
                        double d = a.DurationInHours / 60;
                        bool overlap = a.DateTime < apStart && a.DateTime.AddMinutes(d) < apEnd;
                        if (overlap)
                        {
                            MessageBox.Show("Preklapaju se termini");
                            return;
                        }
                    }
                }
                Termin.DoctrosNameSurname = Doktor.FirstName + " " + Doktor.LastName;
                Termin.IDAppointment = storage.GetNewID();
                foreach(Room r in rStorage.GetAll())
                {
                    if(rdbPregled.IsChecked.Equals(true) && Doktor.RoomID.Equals(r.Id))
                    {
                        Termin.Room = r;
                        Termin.Type = AppointmentType.examination;
                        break;
                    }
                    else
                    {
                        if (r.Type.Equals(RoomType.OPERACIONA_SALA))
                        {
                            Termin.Room = r;
                            Termin.Type = AppointmentType.operation;
                            break;
                        }
                    }
                }
                ((Doctor_Examination)Window.GetWindow(this.Owner)).Pregled.appointment = Termin;
                ObservableCollection<Appointment> list = new ObservableCollection<Appointment>();
                storage.Save(Termin);
                foreach (Appointment a in storage.GetAll())
                {

                    if (a.IDDoctor.Equals(Termin.IDDoctor))
                    {
                        list.Add(a);
                    }
                }
                parentAppointment.dataGridPregledi.ItemsSource = list;
                this.Close();
            }

        }
    }
}
