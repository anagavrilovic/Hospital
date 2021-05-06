using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for ZakazivanjeTermina.xaml
    /// </summary>
    public partial class ZakazivanjeTermina : Page
    {
        private Hospital.Model.Doctor doktor = new Hospital.Model.Doctor();
        private MedicalRecord pacijent = new MedicalRecord();
        private string time;
        private int danUNedelji;
        private string datumString;
        private DateTime datum;
        private Appointment appointment = new Appointment();
        private ObservableCollection<Room> rooms = new ObservableCollection<Room>();
        private ObservableCollection<string> trajanje = new ObservableCollection<string>();

        public ObservableCollection<string> Trajanje
        {
            get { return trajanje; }
            set { trajanje = value; }
        }

        public ObservableCollection<Room> Rooms
        {
            get { return rooms; }
            set { rooms = value; }
        }


        public Appointment Appointment
        {
            get { return appointment; }
            set { appointment = value; }
        }


        public DateTime Datum
        {
            get { return datum; }
            set { datum = value; }
        }


        public string DatumString
        {
            get { return datumString; }
            set { datumString = value; }
        }

        public string Time
        {
            get { return time; }
            set { time = value; }
        }

        public int DanUNedelji
        {
            get { return danUNedelji; }
            set { danUNedelji = value; }
        }

        public MedicalRecord Pacijent
        {
            get { return pacijent; }
            set { pacijent = value; }
        }

        public Hospital.Model.Doctor Doktor
        {
            get { return doktor; }
            set { doktor = value; }
        }

        public ZakazivanjeTermina(Hospital.Model.Doctor d, MedicalRecord mr, int dan, string vreme, DateTime weekBegin)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Doktor = d;
            this.Pacijent = mr;
            this.Time = vreme;
            this.DanUNedelji = dan;
            OdrediDatumVreme(weekBegin);
            UcitajSobe();
            PrikaziTrajanje();
        }

        private void OdrediDatumVreme(DateTime weekBegin)
        {
            Datum = weekBegin.AddDays(DanUNedelji - 1);

            string[] vreme = Time.Split(':');
            TimeSpan ts = new TimeSpan(Int32.Parse(vreme[0]), Int32.Parse(vreme[1]), 0);
            Datum = Datum.Date + ts;

            DatumString = Datum.ToString("dd.MM.yyyy.");
        }

        private void UcitajSobe()
        {
            RoomStorage rs = new RoomStorage();
            Rooms = rs.GetAll();
        }

        private void PrikaziTrajanje()
        {
            double t = 0;
            while(t <= 20)
            {
                Trajanje.Add(t.ToString());
                t += 0.5;
            }
        }

        private void PotvrdiClick(object sender, RoutedEventArgs e)
        {
            Appointment.DateTime = Datum;
            Appointment.PatientName = Pacijent.Patient.FirstName;
            Appointment.PatientSurname = Pacijent.Patient.LastName;
            Appointment.IDpatient = Pacijent.Patient.PersonalID;
            Appointment.IDDoctor = Doktor.PersonalID;
            Appointment.IDAppointment = Appointment.GetHashCode().ToString();

            StringBuilder sb = new StringBuilder(Doktor.FirstName);
            sb.Append(" ");
            sb.Append(Doktor.LastName);
            Appointment.DoctrosNameSurname = sb.ToString();

            switch (Tip.SelectedIndex)
            {
                case 0: Appointment.Type = AppointmentType.examination; break;
                case 1: Appointment.Type = AppointmentType.operation; break;
            }

            AppointmentStorage aps = new AppointmentStorage();
            bool ok = aps.SaveIfNotOvelapping(Appointment);

            if (!ok)
            {
                MessageBox.Show("Termin se poklapa sa nekim drugim terminom!");
                return;
            }

            NavigationService.Navigate(new Kalendar(Doktor));

        }

        private void OdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Kalendar(Doktor));
        }
    }
}
