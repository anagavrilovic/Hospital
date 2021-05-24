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
       private PatientSettingsStorage patientSettingsStorage = new PatientSettingsStorage();
       private DoctorStorage doctorStorage = new DoctorStorage();
       private AppointmentStorage appointmentStorage = new AppointmentStorage();
       private MedicalRecordStorage medicalRecordStorage = new MedicalRecordStorage();
       private RoomStorage roomStorage = new RoomStorage();
       private DateTime FirstDayOfWeek;
       private Appointment AppointmentForDeleting = null;
       private const int MAXIMUM_DAYS_DIFFERENCE= 10;
       private const int MINIMUM_DAYS_DIFFERENCE = 2;
       private const int MAXIMUM_NUMBER_OF_TERMS_PER_DAY = 24; 

        public ObservableCollection<PatientCalendarDTO> WeeklyTerms
        {
            get;
            set;
        }
        public PatientMakeAppointment()
        {
            InitializeComponent();
            this.DataContext = this;
            WeeklyTerms = new ObservableCollection<PatientCalendarDTO>();
            FirstDayOfWeek = DateTime.Now;
            dataGridApp.Focus();
            GenerateCalendar(FirstDayOfWeek);
        }

        public PatientMakeAppointment(Appointment AppointmentForDeleting) : this()
        {
            this.AppointmentForDeleting = AppointmentForDeleting;
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Appointment appointment = GetSelectedAppointment();
                if (appointment == null || !IsChosenDateAcceptable(appointment)) return;
                appointmentStorage.Save(appointment);
                if (AppointmentForDeleting != null) appointmentStorage.Delete(AppointmentForDeleting.IDAppointment);
                patientSettingsStorage.AddScheduling(DateTime.Now);
                NavigateBack();
            }

            if (e.Key == Key.Escape)
            {
                this.NavigationService.GoBack();
            }

            if(e.Key == Key.LeftCtrl)
            {
                int daysDifference = -7;
                RefreshCalendar(daysDifference);
            }

            if (e.Key == Key.RightCtrl)
            {
                int daysDifference = 7;
                RefreshCalendar(daysDifference);
            }
        }

        private void NavigateBack()
        {
            PatientAppointments patientAppointments = new PatientAppointments();
            patientAppointments.Refresh();
            this.NavigationService.Navigate(patientAppointments);
        }

        private void GenerateCalendar(DateTime day)
        {
            DateTime MondayDay = SetDayToMonday(day);
            label.Content = "Termini za nedelju: "+MondayDay.ToShortDateString() + " - " + MondayDay.AddDays(6).ToShortDateString();
            for (int counter = 0, hour=8, minute=0; counter < MAXIMUM_NUMBER_OF_TERMS_PER_DAY; counter++)
            {
                if (counter % 2 == 1) minute = 30;
                PatientCalendarDTO weeklyAppointmentsForTerm = new PatientCalendarDTO(hour, minute, MondayDay);
                WeeklyTerms.Add(weeklyAppointmentsForTerm);
                if (counter % 2 == 1)
                {
                    minute = 0;
                    hour++;
                }

            }
        }

        private void RefreshCalendar(int daysDifference)
        {
            WeeklyTerms.Clear();
            FirstDayOfWeek = FirstDayOfWeek.AddDays(daysDifference);
            GenerateCalendar(FirstDayOfWeek);
            dataGridApp.ItemsSource = null;
            dataGridApp.ItemsSource = WeeklyTerms;
        }

        private Appointment GetSelectedAppointment()
        {
            int currentColumnIndex = dataGridApp.CurrentCell.Column.DisplayIndex;
            int currentRowIndex = dataGridApp.Items.IndexOf(dataGridApp.CurrentItem);
            PatientCalendarDTO patientCalendarDTO = WeeklyTerms.ElementAt(currentRowIndex);
            if (String.IsNullOrEmpty(patientCalendarDTO.Doctors[currentColumnIndex - 1]))
            {
                return null;
            }

            //return new Appointment(patientCalendarDTO.Dates[currentColumnIndex - 1], AppointmentType.examination, medicalRecordStorage.GetByPatientID(MainWindow.IDnumber).Patient.FirstName, medicalRecordStorage.GetByPatientID(MainWindow.IDnumber).Patient.LastName, MainWindow.IDnumber, patientCalendarDTO.DoctorsID[currentColumnIndex - 1], appointmentStorage.GetNewID(), patientCalendarDTO.Doctors[currentColumnIndex - 1].Substring(3), roomStorage.GetOne(doctorStorage.GetOne(patientCalendarDTO.DoctorsID[currentColumnIndex - 1]).RoomID));
            return new Appointment();
        }

        private Boolean IsChosenDateAcceptable(Appointment appointment)
        {
            if (AppointmentForDeleting != null)
            {
                if (!IsDateAcceptableForRescheduling(appointment)) return false;
            }
            if (!IsDateAcceptableForScheduling(appointment)) return false;

            return true;
        }

        private Boolean IsDateAcceptableForRescheduling(Appointment appointment)
        {
            if ((Math.Abs((appointment.DateTime - AppointmentForDeleting.DateTime).TotalDays) > MAXIMUM_DAYS_DIFFERENCE))
            {
                MessageBox.Show("Nije moguce toliko pomeriti termin");
                return false;
            }
            return true;
        }

        private Boolean IsDateAcceptableForScheduling(Appointment appointment)
        {
            if ((appointment.DateTime - DateTime.Now).TotalDays < MINIMUM_DAYS_DIFFERENCE)
            {
                MessageBox.Show("Nije moguće zakazati termin tako kasno.");
                return false;
            }
            return true;
        }

        private DateTime SetDayToMonday(DateTime day)
        {
            int auxiliaryDay = (int)day.DayOfWeek;
            DateTime MondayDate;
            switch (auxiliaryDay)
            {
                case 2:
                    MondayDate = day.AddDays(-1);
                    break;
                case 3:
                    MondayDate = day.AddDays(-2);
                    break;
                case 4:
                    MondayDate = day.AddDays(-3);
                    break;
                case 5:
                    MondayDate = day.AddDays(-4);
                    break;
                case 6:
                    MondayDate = day.AddDays(-5);
                    break;
                case 0:
                    MondayDate = day.AddDays(-6);
                    break;
                default:
                    MondayDate = day;
                    break;
            }
            return MondayDate;
        }

    }
}
