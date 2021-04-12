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
    /// Interaction logic for Kalendar.xaml
    /// </summary>
    public partial class Kalendar : Window
    {
        private ObservableCollection<Termini> tabela = new ObservableCollection<Termini>();
        private ObservableCollection<Doctor> doctors = new ObservableCollection<Doctor>();
        private DateTime date = DateTime.Today;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }


        public ObservableCollection<Termini> Tabela
        {
            get { return tabela; }
            set { tabela = value; }
        }

        public ObservableCollection<Doctor> Doctors
        {
            get { return doctors; }
            set { doctors = value; }
        }


        public Kalendar()
        {
            InitializeComponent();
            this.DataContext = this;
            UcitajLekare();
            PopuniKalendar();
        }

        private void PopuniKalendar()
        {
            Tabela.Clear();

            DateTime start = new DateTime();
            TimeSpan ts = new TimeSpan(7, 0, 0);
            start = start.Date + ts;
            for (int i = 0; i < 48; i++)
            {
                Termini termin = new Termini();
                termin.Vreme = start.ToString("HH.mm", CultureInfo.InvariantCulture);
                start = start.AddMinutes(30);
                tabela.Add(termin);
            } 
        }

        private void PopuniTermine(Doctor doctor)
        {
            if(doctor == null)
            {
                return;
            }

            DateTime weekBegin = GetWeekBegin(Date);
            DateTime weekEnd = weekBegin.AddDays(6);

            AppointmentStorage storage = new AppointmentStorage();
            ObservableCollection<Appointment> appointments = storage.GetAll();

            foreach (Appointment app in appointments)
            {
                if (app.DateTime.Date >= weekBegin.Date && app.DateTime.Date <= weekEnd.Date && app.IDDoctor.Equals(doctor.PersonalID))
                {
                    DayOfWeek dan = app.DateTime.DayOfWeek;

                    double numberOfCells = app.DurationInHours / 0.5;

                    switch (dan)
                    {
                        case DayOfWeek.Monday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH.mm"))].Ponedeljak = app;
                            for(int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH.mm")) + i].Ponedeljak.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Tuesday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH.mm"))].Utorak = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH.mm")) + i].Utorak.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Wednesday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH.mm"))].Sreda = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH.mm")) + i].Sreda.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Thursday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH.mm"))].Cetvrtak = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH.mm")) + i].Cetvrtak.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Friday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH.mm"))].Petak = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH.mm")) + i].Petak.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Saturday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH.mm"))].Subota = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH.mm")) + i].Subota.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Sunday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH.mm"))].Nedelja = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH.mm")) + i].Nedelja.Type = app.Type;
                            }
                            break;
                    }

                }
            }
        }

        private void UcitajLekare()
        {
            DoctorStorage ds = new DoctorStorage();
            Doctors = ds.GetAll();
        }

        private int NadjiVreme(String vreme)
        {

            DateTime start = new DateTime();
            TimeSpan ts = new TimeSpan(7, 0, 0);
            start = start.Date + ts;
            for (int i = 0; i < 48; i++)
            {
                
                if(start.ToString("HH.mm", CultureInfo.InvariantCulture).Equals(vreme))
                {
                    return i;
                }

                start = start.AddMinutes(30);
            }

            return -1;
        }

        public DateTime GetWeekBegin(DateTime dt)
        {
            int diff = (7 + (dt.DayOfWeek - DayOfWeek.Monday)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        private void OsveziKalendar()
        {
            PopuniKalendar();
            PopuniTermine((Doctor)DoctorComboBox.SelectedItem);
        }

        private void ComboBoxDoctorEvent(object sender, SelectionChangedEventArgs e)
        {
            OsveziKalendar();
        }

        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            OsveziKalendar();
        }

        private void TrenutnaNedeljaClick(object sender, RoutedEventArgs e)
        {
            Date = DateTime.Today;
            OsveziKalendar();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            Date = Date.AddDays(-7);
            OsveziKalendar();
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            Date = Date.AddDays(7);
            OsveziKalendar();
        }
    }
}
