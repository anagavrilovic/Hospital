using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for PatientMakeAppointment.xaml
    /// </summary>
    public partial class PatientMakeAppointment : Page
    {
        PatientSettingsStorage patientSettingsStorage = new PatientSettingsStorage();
        DoctorStorage doctorStorage = new DoctorStorage();
        AppointmentStorage appointmentStorage = new AppointmentStorage();
        MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
        RoomStorage roomStorage = new RoomStorage();
        DateTime FirstDayOfWeek;
        Appointment AppointmentForDeleting = null;
        public ObservableCollection<PatientCalendarDTO> Lista
        {
            get;
            set;
        }
        public PatientMakeAppointment()
        {
            InitializeComponent();
            this.DataContext = this;

            FirstDayOfWeek = DateTime.Now;
            dataGridApp.Focus();
            LetsMakeCalendar(FirstDayOfWeek);
        }

        public PatientMakeAppointment(Appointment AppointmentForDeleting)
        {
            InitializeComponent();
            this.DataContext = this;

            this.AppointmentForDeleting = AppointmentForDeleting;
            FirstDayOfWeek = DateTime.Now;
            dataGridApp.Focus();
            LetsMakeCalendar(FirstDayOfWeek);
        }

        private void myTestKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                
                int currentColumnIndex=dataGridApp.CurrentCell.Column.DisplayIndex;
                int currentRowIndex = dataGridApp.Items.IndexOf(dataGridApp.CurrentItem);
                PatientCalendarDTO patientCalendarDTO = Lista.ElementAt(currentRowIndex);
                if (String.IsNullOrEmpty(patientCalendarDTO.Days[currentColumnIndex-1]))
                {
                    return;
                }

                

                Appointment appointment = new Appointment(patientCalendarDTO.Dates[currentColumnIndex-1],AppointmentType.examination, medicalRecordStorage.GetByPatientID(MainWindow.IDnumber).Patient.FirstName, medicalRecordStorage.GetByPatientID(MainWindow.IDnumber).Patient.LastName,MainWindow.IDnumber,patientCalendarDTO.Doctors[currentColumnIndex-1],appointmentStorage.GetNewID(), patientCalendarDTO.Days[currentColumnIndex-1].Substring(3),roomStorage.GetOne(doctorStorage.GetOne(patientCalendarDTO.Doctors[currentColumnIndex - 1]).RoomID));

                if (AppointmentForDeleting != null)
                {
                    if (!IsReschedulingAllowed(appointment))
                    {
                        MessageBox.Show("Nije moguce toliko pomeriti termin");
                        return;
                    }
                }
                if ((appointment.DateTime - DateTime.Now).TotalDays < 2)
                {
                    MessageBox.Show("Nije moguće zakazati termin tako kasno.");
                    return;
                }

                appointmentStorage.Save(appointment);
                if (AppointmentForDeleting != null) appointmentStorage.Delete(AppointmentForDeleting.IDAppointment);
                patientSettingsStorage.AddScheduling(DateTime.Now);
                this.NavigationService.GoBack();
            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.GoBack();
            }

            if(e.Key == Key.LeftCtrl)
            {
                Lista.Clear();
                FirstDayOfWeek = FirstDayOfWeek.AddDays(-7);
                LetsMakeCalendar(FirstDayOfWeek);
                dataGridApp.ItemsSource = null;
                dataGridApp.ItemsSource = Lista;
            }

            if (e.Key == Key.RightCtrl)
            {
                Lista.Clear();
                FirstDayOfWeek = FirstDayOfWeek.AddDays(7);
                LetsMakeCalendar(FirstDayOfWeek);
                dataGridApp.ItemsSource = null;
                dataGridApp.ItemsSource = Lista;
            }
        }

        private void LetsMakeCalendar(DateTime dt)
        {
            DateTime novoVreme;
            int dan = (int)dt.DayOfWeek;
            switch (dan)
            {
                case 2:
                    novoVreme = dt.AddDays(-1);
                    break;
                case 3:
                    novoVreme = dt.AddDays(-2);
                    break;
                case 4:
                    novoVreme = dt.AddDays(-3);
                    break;
                case 5:
                    novoVreme = dt.AddDays(-4);
                    break;
                case 6:
                    novoVreme = dt.AddDays(-5);
                    break;
                case 0:
                    novoVreme = dt.AddDays(-6);
                    break;
                default:
                    novoVreme = dt;
                    break;
            }
            label.Content = "Termini za nedelju: "+novoVreme.ToShortDateString() + " - " + novoVreme.AddDays(6).ToShortDateString();
            Lista = new ObservableCollection<PatientCalendarDTO>();
            int i = 8;
            int j = 0;
            for (int x = 0; x < 25; x++)
            {
                if (x % 2 == 1) j = 30;
                PatientCalendarDTO zaKalendar = new PatientCalendarDTO(i, j, novoVreme);
                Lista.Add(zaKalendar);
                if (x % 2 == 1)
                {
                    j = 0;
                    i++;
                }

            }
        }

        private Boolean IsReschedulingAllowed(Appointment app)
        {
            if (Math.Abs((app.DateTime - AppointmentForDeleting.DateTime).TotalDays) > 10) return false;
            return true;
           
        }

       

    }
}
