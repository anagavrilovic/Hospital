using Hospital.Controller.PatientControllers;
using Hospital.DTO.PatientDTO;
using Hospital.Model;
using Hospital.Services;
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
        private CalendarController calendarController = new CalendarController();
       private PatientSettingsService patientSettingsService = new PatientSettingsService();
       private AppointmentService appointmentService = new AppointmentService();
       private DateTime firstDayOfWeek;
       private Appointment appointmentForDeleting = null;
       private const int MAXIMUM_NUMBER_OF_TERMS_PER_DAY = 32; 

        public ObservableCollection<CalendarDTO> WeeklyTerms
        {
            get;
            set;
        }
        public PatientMakeAppointment()
        {
            InitializeComponent();
            this.DataContext = this;
            WeeklyTerms = new ObservableCollection<CalendarDTO>();
            firstDayOfWeek = DateTime.Now;
            dataGridApp.Focus();
            GenerateCalendar(firstDayOfWeek);
        }

        public PatientMakeAppointment(Appointment appointmentForDeleting) : this()
        {
            this.appointmentForDeleting = appointmentForDeleting;
        }

        private void KeyPress(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                Appointment appointment = GetSelectedAppointment();
                if (appointment == null || !appointmentService.IsChosenDateAcceptableForNewAppointment(appointment, appointmentForDeleting))
                {
                    MessageBox.Show("Nije moguce zakazati izabrani termin");
                    return;
                }
                if (MessageBox.Show("Da li ste sigurni da želite da zakažete odabrani termin?", "Potvrda", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) return;
                appointmentService.Save(appointment);
                if (appointmentForDeleting != null) appointmentService.Delete(appointmentForDeleting.IDAppointment);
                patientSettingsService.AddScheduling(DateTime.Now);
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
                dataGridApp.Focus();
            }

            if (e.Key == Key.RightCtrl)
            {
                int daysDifference = 7;
                RefreshCalendar(daysDifference);
                dataGridApp.Focus();
            }
        }

        private void NavigateBack()
        {
            PatientAppointments patientAppointments = new PatientAppointments();
            this.NavigationService.Navigate(patientAppointments);
        }

        private void GenerateCalendar(DateTime day)
        {
            DateTime MondayDay = SetDayToMonday(day);
            label.Content = "Termini za nedelju: "+MondayDay.ToShortDateString() + " - " + MondayDay.AddDays(6).ToShortDateString();
            for (int counter = 0, hour=6, minute=0; counter < MAXIMUM_NUMBER_OF_TERMS_PER_DAY; counter++)
            {
                if (counter % 2 == 1) minute = 30;
                CalendarDTO weeklyAppointmentsForTerm = calendarController.SetCalendarDTO(new CalendarDTO(hour, minute, MondayDay));
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
            firstDayOfWeek = firstDayOfWeek.AddDays(daysDifference);
            GenerateCalendar(firstDayOfWeek);
            dataGridApp.ItemsSource = null;
            dataGridApp.ItemsSource = WeeklyTerms;
        }

        private Appointment GetSelectedAppointment()
        {
            int currentColumnIndex = dataGridApp.CurrentCell.Column.DisplayIndex;
            int currentRowIndex = dataGridApp.Items.IndexOf(dataGridApp.CurrentItem);
            CalendarDTO patientCalendarDTO = WeeklyTerms.ElementAt(currentRowIndex);
            return calendarController.GetAppointment(patientCalendarDTO, currentColumnIndex);
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
