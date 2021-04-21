﻿using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
        private ObservableCollection<Hospital.Model.Doctor> doctors = new ObservableCollection<Hospital.Model.Doctor>();
        private DateTime date = DateTime.Today;
        private MedicalRecord patient = new MedicalRecord();

        private DateTime weekBegin;

        public DateTime WeekBegin
        {
            get { return weekBegin; }
            set { weekBegin = value; }
        }


        public MedicalRecord Patient
        {
            get { return patient; }
            set { patient = value; }
        }

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

        public ObservableCollection<Hospital.Model.Doctor> Doctors
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
                termin.Vreme = start.ToString("HH:mm", CultureInfo.InvariantCulture);
                start = start.AddMinutes(30);
                tabela.Add(termin);
            } 
        }

        private void PopuniTermine(Hospital.Model.Doctor doctor)
        {
            if(doctor == null)
            {
                return;
            }

            WeekBegin = GetWeekBegin(Date);
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
                            tabela[NadjiVreme(app.DateTime.ToString("HH:mm"))].Ponedeljak = app;
                            for(int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH:mm")) + i].Ponedeljak.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Tuesday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH:mm"))].Utorak = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH:mm")) + i].Utorak.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Wednesday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH:mm"))].Sreda = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH:mm")) + i].Sreda.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Thursday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH:mm"))].Cetvrtak = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH:mm")) + i].Cetvrtak.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Friday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH:mm"))].Petak = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH:mm")) + i].Petak.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Saturday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH:mm"))].Subota = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH:mm")) + i].Subota.Type = app.Type;
                            }
                            break;

                        case DayOfWeek.Sunday:
                            tabela[NadjiVreme(app.DateTime.ToString("HH:mm"))].Nedelja = app;
                            for (int i = 1; i < numberOfCells; i++)
                            {
                                tabela[NadjiVreme(app.DateTime.ToString("HH:mm")) + i].Nedelja.Type = app.Type;
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
                
                if(start.ToString("HH:mm", CultureInfo.InvariantCulture).Equals(vreme))
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
            PopuniTermine((Hospital.Model.Doctor)DoctorComboBox.SelectedItem);
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

        private void OdaberiPacijentaClick(object sender, RoutedEventArgs e)
        {
            var op = new OdaberiPacijenta(Patient);
            op.Show();
        }

        private void ZakaziClick(object sender, RoutedEventArgs e)
        {
            if(KalendarDataGrid.SelectedCells.Count == 0)
            {
                return;
            }

            int indexColumn = KalendarDataGrid.SelectedCells[0].Column.DisplayIndex;
            var vreme = ((Termini)KalendarDataGrid.SelectedCells[0].Item).Vreme;
            
            var zt = new ZakazivanjeTermina((Hospital.Model.Doctor)DoctorComboBox.SelectedItem, Patient, indexColumn, vreme, WeekBegin, Tabela);
            zt.Show();

        }

        private void OtkaziClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                        "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int indexColumn = KalendarDataGrid.SelectedCells[0].Column.DisplayIndex;
                var vreme = ((Termini)KalendarDataGrid.SelectedCells[0].Item).Vreme;

                AppointmentStorage aps = new AppointmentStorage();
                double trajanje;

                switch (indexColumn)
                {
                    case 1:
                        aps.Delete(Tabela[NadjiVreme(vreme)].Ponedeljak.IDAppointment);
                        trajanje = Tabela[NadjiVreme(vreme)].Ponedeljak.DurationInHours / 0.5;
                        for(int i = 1; i < trajanje; i++)
                        {
                            Tabela[NadjiVreme(vreme) + i].Ponedeljak.Type = AppointmentType.none;
                        }
                        break;
                    case 2:
                        aps.Delete(Tabela[NadjiVreme(vreme)].Utorak.IDAppointment);
                        trajanje = Tabela[NadjiVreme(vreme)].Utorak.DurationInHours / 0.5;
                        for (int i = 1; i < trajanje; i++)
                        {
                            Tabela[NadjiVreme(vreme) + i].Utorak.Type = AppointmentType.none;
                        }
                        break;
                    case 3:
                        aps.Delete(Tabela[NadjiVreme(vreme)].Sreda.IDAppointment);
                        trajanje = Tabela[NadjiVreme(vreme)].Sreda.DurationInHours / 0.5;
                        for (int i = 1; i < trajanje; i++)
                        {
                            Tabela[NadjiVreme(vreme) + i].Sreda.Type = AppointmentType.none;
                        }
                        break;
                    case 4:
                        aps.Delete(Tabela[NadjiVreme(vreme)].Cetvrtak.IDAppointment);
                        trajanje = Tabela[NadjiVreme(vreme)].Cetvrtak.DurationInHours / 0.5;
                        for (int i = 1; i < trajanje; i++)
                        {
                            Tabela[NadjiVreme(vreme) + i].Cetvrtak.Type = AppointmentType.none;
                        }
                        break;
                    case 5:
                        aps.Delete(Tabela[NadjiVreme(vreme)].Petak.IDAppointment);
                        trajanje = Tabela[NadjiVreme(vreme)].Petak.DurationInHours / 0.5;
                        for (int i = 1; i < trajanje; i++)
                        {
                            Tabela[NadjiVreme(vreme) + i].Petak.Type = AppointmentType.none;
                        }
                        break;
                    case 6:
                        aps.Delete(Tabela[NadjiVreme(vreme)].Subota.IDAppointment);
                        trajanje = Tabela[NadjiVreme(vreme)].Subota.DurationInHours / 0.5;
                        for (int i = 1; i < trajanje; i++)
                        {
                            Tabela[NadjiVreme(vreme) + i].Subota.Type = AppointmentType.none;
                        }
                        break;
                    case 7:
                        aps.Delete(Tabela[NadjiVreme(vreme)].Nedelja.IDAppointment);
                        trajanje = Tabela[NadjiVreme(vreme)].Nedelja.DurationInHours / 0.5;
                        for (int i = 1; i < trajanje; i++)
                        {
                            Tabela[NadjiVreme(vreme) + i].Nedelja.Type = AppointmentType.none;
                        }
                        break;
                }

                OsveziKalendar();
            }
            else
            {
                
            }
        }
    }
}
