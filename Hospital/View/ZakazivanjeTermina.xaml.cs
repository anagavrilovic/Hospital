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
    public partial class ZakazivanjeTermina : Window
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
        private ObservableCollection<Termini> tabela = new ObservableCollection<Termini>();

        public ObservableCollection<Termini> Tabela
        {
            get { return tabela; }
            set { tabela = value; }
        }

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

        public ZakazivanjeTermina(Hospital.Model.Doctor d, MedicalRecord mr, int dan, string vreme, DateTime weekBegin, ObservableCollection<Termini> t)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Tabela = t;
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
            Appointment.DoctrosNameSurname = Doktor.FirstName + Doktor.LastName;
            Appointment.IDAppointment = Appointment.GetHashCode().ToString();

            switch (Tip.SelectedIndex)
            {
                case 0: Appointment.Type = AppointmentType.examination; break;
                case 1: Appointment.Type = AppointmentType.operation; break;
            }

            AppointmentStorage aps = new AppointmentStorage();
            bool ok = aps.SaveAndCheck(Appointment);

            if (!ok)
            {
                MessageBox.Show("Termin se poklapa sa nekim drugim terminom!");
                return;
            }

            DodajUKalendar(Appointment);
            this.Close();
        }

        private void DodajUKalendar(Appointment ap)
        {
            foreach(Termini t in Tabela)
            {
                if (t.Vreme.Equals(Time))
                {
                    double numberOfCells = ap.DurationInHours / 0.5;
                    switch (DanUNedelji)
                    {
                        case 1:
                            t.Ponedeljak = ap;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                Tabela[NadjiVreme(Time) + i].Ponedeljak.Type = ap.Type;
                            }
                            break;
                        case 2:
                            t.Utorak = ap;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                Tabela[NadjiVreme(Time) + i].Utorak.Type = ap.Type;
                            }
                            break;
                        case 3:
                            t.Sreda = ap;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                Tabela[NadjiVreme(Time) + i].Sreda.Type = ap.Type;
                            }
                            break;
                        case 4:
                            t.Cetvrtak = ap;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                Tabela[NadjiVreme(Time) + i].Cetvrtak.Type = ap.Type;
                            }
                            break;
                        case 5:
                            t.Petak = ap;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                Tabela[NadjiVreme(Time) + i].Petak.Type = ap.Type;
                            }
                            break;
                        case 6:
                            t.Subota = ap;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                Tabela[NadjiVreme(Time) + i].Subota.Type = ap.Type;
                            }
                            break;
                        case 7:
                            t.Nedelja = ap;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                Tabela[NadjiVreme(Time) + i].Nedelja.Type = ap.Type;
                            }
                            break;
                    }
                }
            }

        }

        private int NadjiVreme(String vreme)
        {

            DateTime start = new DateTime();
            TimeSpan ts = new TimeSpan(7, 0, 0);
            start = start.Date + ts;
            for (int i = 0; i < 48; i++)
            {

                if (start.ToString("HH:mm", CultureInfo.InvariantCulture).Equals(vreme))
                {
                    return i;
                }

                start = start.AddMinutes(30);
            }

            return -1;
        }

        private void OdustaniClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
