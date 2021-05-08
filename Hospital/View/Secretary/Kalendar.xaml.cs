using Hospital.Model;
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
    public partial class Kalendar : Page
    {
        public const int numberOfTimeSlotsPerDay = 48;

        // Properties
        public ObservableCollection<WeeklyCalendarRow> WeeklyCalendar { get; set; }
        public ObservableCollection<Model.Doctor> AllDoctors { get; set; }
        public DateTime ChosenDate { get; set; }
        public DateTime WeekBegin { get; set; }
        public DateTime WeekEnd { get; set; }
        public MedicalRecord SelectedPatientForNewAppointment { get; set; }
        public Model.Doctor SelectedDoctorForNewAppointment { get; set; }

        // Storage class properties
        public AppointmentStorage AppointmentStorage { get; set; }


        // Methods
        public Kalendar(Model.Doctor doctorWhoseAppointmentsWillInitialyBeShown)
        {
            InitializeComponent();
            this.DataContext = this;
            InitializeAllProperties();
            LoadAllDoctors();
            SetSelectedDoctorIfNeeded(doctorWhoseAppointmentsWillInitialyBeShown);
            RefreshWeeklyCalendar();
        }

        private void InitializeAllProperties()
        {
            this.WeeklyCalendar = new ObservableCollection<WeeklyCalendarRow>();
            this.AllDoctors = new ObservableCollection<Model.Doctor>();
            this.ChosenDate = DateTime.Today;
            this.SelectedPatientForNewAppointment = new MedicalRecord();
            this.AppointmentStorage = new AppointmentStorage();
        }

        private void LoadAllDoctors()
        {
            DoctorStorage doctorStorage = new DoctorStorage();
            AllDoctors = doctorStorage.GetAll();
        }

        private void SetSelectedDoctorIfNeeded(Model.Doctor doctorWhoseAppointmentsWillInitialyBeShown)
        {
            if (doctorWhoseAppointmentsWillInitialyBeShown != null)
            {
                foreach (Model.Doctor doctor in AllDoctors)
                {
                    if (doctor.IsEqualWith(doctorWhoseAppointmentsWillInitialyBeShown))
                    {
                        SelectedDoctorForNewAppointment = doctor;
                    }
                }
            }
        }

        private void RefreshWeeklyCalendar()
        {
            WeeklyCalendar.Clear();

            FillWeeklyCalendarWithEmptyTimeSlots(GetStartTimeForWeeklyCalendar());

            if (SelectedDoctorForNewAppointment != null)
                ShowSelectedDoctorsAppointments();
        }

        private DateTime GetStartTimeForWeeklyCalendar()
        {
            TimeSpan startTime = new TimeSpan(0, 0, 0);
            return new DateTime().Date + startTime;
        }

        private void FillWeeklyCalendarWithEmptyTimeSlots(DateTime timeForRow)
        {
            for (int i = 0; i < numberOfTimeSlotsPerDay; i++)
            {
                WeeklyCalendarRow newRow = new WeeklyCalendarRow();
                newRow.TimeInRow = timeForRow.ToString("HH:mm", CultureInfo.InvariantCulture);
                WeeklyCalendar.Add(newRow);
                
                timeForRow = timeForRow.AddMinutes(30);
            }
        }

        private void ShowSelectedDoctorsAppointments()
        {
            ObservableCollection<Appointment> selectedDoctorsAppointments = AppointmentStorage.GetAppointmentsByDoctor(SelectedDoctorForNewAppointment.PersonalID);
            ObservableCollection<Appointment> selectedDoctorsAppointmentsInChosenWeek = GetSelectedDoctorsAppointmentsInChosenWeek(selectedDoctorsAppointments);

            LoadSelectedDoctorsAppointmentsIntoWeeklyCalendar(selectedDoctorsAppointmentsInChosenWeek);
        }

        private ObservableCollection<Appointment> GetSelectedDoctorsAppointmentsInChosenWeek(ObservableCollection<Appointment> selectedDoctorsAppointments)
        {
            SetFirstAndLastDateOfWeekInWhichChosenDateIs(ChosenDate);

            ObservableCollection<Appointment> selectedDoctorsAppointmentsInChosenWeek = new ObservableCollection<Appointment>();
            foreach (Appointment appointment in selectedDoctorsAppointments)
                if (IsAppointmentInChosenWeek(appointment))
                    selectedDoctorsAppointmentsInChosenWeek.Add(appointment);

            return selectedDoctorsAppointmentsInChosenWeek;
        }

        private bool IsAppointmentInChosenWeek(Appointment appointment)
        { 
            return appointment.DateTime.Date >= WeekBegin.Date && appointment.DateTime.Date <= WeekEnd.Date;
        }

        private void LoadSelectedDoctorsAppointmentsIntoWeeklyCalendar(ObservableCollection<Appointment> selectedDoctorsAppointmentsInChosenWeek)
        {
            foreach (Appointment appointment in selectedDoctorsAppointmentsInChosenWeek)
            {
                DayOfWeek appointmentsDayOfWeek = appointment.GetAppointmentsDayOfWeek();

                double numberOfCells = appointment.DurationInHours / 0.5;

                switch (appointmentsDayOfWeek)
                {
                    case DayOfWeek.Monday:
                        WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm"))].Monday = appointment;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm")) + i].Monday.Type = appointment.Type;
                        }
                        break;

                    case DayOfWeek.Tuesday:
                        WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm"))].Tuesday = appointment;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm")) + i].Tuesday.Type = appointment.Type;
                        }
                        break;

                    case DayOfWeek.Wednesday:
                        WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm"))].Wednesday = appointment;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm")) + i].Wednesday.Type = appointment.Type;
                        }
                        break;

                    case DayOfWeek.Thursday:
                        WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm"))].Thursday = appointment;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm")) + i].Thursday.Type = appointment.Type;
                        }
                        break;

                    case DayOfWeek.Friday:
                        WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm"))].Friday = appointment;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm")) + i].Friday.Type = appointment.Type;
                        }
                        break;

                    case DayOfWeek.Saturday:
                        WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm"))].Saturday = appointment;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm")) + i].Saturday.Type = appointment.Type;
                        }
                        break;

                    case DayOfWeek.Sunday:
                        WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm"))].Sunday = appointment;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(appointment.DateTime.ToString("HH:mm")) + i].Sunday.Type = appointment.Type;
                        }
                        break;
                }
            }
        }

        private int FindTimeRow(String time)
        {
            DateTime startTime = GetStartTimeForWeeklyCalendar();

            for (int i = 0; i < numberOfTimeSlotsPerDay; i++)
            {
                if (startTime.ToString("HH:mm", CultureInfo.InvariantCulture).Equals(time))
                    return i;

                startTime = startTime.AddMinutes(30);
            }

            return -1;
        }

        public void SetFirstAndLastDateOfWeekInWhichChosenDateIs(DateTime chosenDate)
        {
            int difference = (7 + (chosenDate.DayOfWeek - DayOfWeek.Monday)) % 7;
            WeekBegin = chosenDate.AddDays(-1 * difference).Date;
            WeekEnd = WeekBegin.AddDays(6);
        }

        private void ComboBoxDoctorEvent(object sender, SelectionChangedEventArgs e)
        {
            RefreshWeeklyCalendar();
        }

        private void DateChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshWeeklyCalendar();
        }

        private void CurrentWeekClick(object sender, RoutedEventArgs e)
        {
            ChosenDate = DateTime.Today;
            RefreshWeeklyCalendar();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
            ChosenDate = ChosenDate.AddDays(-7);
            RefreshWeeklyCalendar();
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {
            ChosenDate = ChosenDate.AddDays(7);
            RefreshWeeklyCalendar();
        }

        private void ChoosePatientClick(object sender, RoutedEventArgs e)
        {
            OdaberiPacijenta choosePatientWindow = new OdaberiPacijenta(SelectedPatientForNewAppointment);
            choosePatientWindow.Show();
        }

        private void ScheduleAppointmentClick(object sender, RoutedEventArgs e)
        {
            if (KalendarDataGrid.SelectedCells.Count == 0)
            {
                MessageBox.Show("Odaberite termin koji želite da zakažete.");
                return;
            }

            int indexColumn = KalendarDataGrid.SelectedCells[0].Column.DisplayIndex;
            string time = ((WeeklyCalendarRow)KalendarDataGrid.SelectedCells[0].Item).TimeInRow;
            DateTime dateTimeForNewAppointment = SetDateTimeForNewAppointment(indexColumn, time);

            NavigationService.Navigate(new ZakazivanjeTermina(SelectedDoctorForNewAppointment, SelectedPatientForNewAppointment, dateTimeForNewAppointment));
        }

        private DateTime SetDateTimeForNewAppointment(int dayOfWeek, string time)
        {
            DateTime date = WeekBegin.AddDays(dayOfWeek - 1);

            string[] vreme = time.Split(':');
            TimeSpan ts = new TimeSpan(Int32.Parse(vreme[0]), Int32.Parse(vreme[1]), 0);
            date = date.Date + ts;

            return date;
        }

        private void CancelAppointmentClick(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da otkažete termin?",
                        "Potvrda", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                int columnIndex = KalendarDataGrid.SelectedCells[0].Column.DisplayIndex;
                var time = ((WeeklyCalendarRow)KalendarDataGrid.SelectedCells[0].Item).TimeInRow;

                double numberOfCells;

                switch (columnIndex)
                {
                    case 1:
                        AppointmentStorage.Delete(WeeklyCalendar[FindTimeRow(time)].Monday.IDAppointment);
                        numberOfCells = WeeklyCalendar[FindTimeRow(time)].Monday.DurationInHours / 0.5;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(time) + i].Monday.Type = AppointmentType.none;
                        }
                        break;
                    case 2:
                        AppointmentStorage.Delete(WeeklyCalendar[FindTimeRow(time)].Tuesday.IDAppointment);
                        numberOfCells = WeeklyCalendar[FindTimeRow(time)].Tuesday.DurationInHours / 0.5;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(time) + i].Tuesday.Type = AppointmentType.none;
                        }
                        break;
                    case 3:
                        AppointmentStorage.Delete(WeeklyCalendar[FindTimeRow(time)].Wednesday.IDAppointment);
                        numberOfCells = WeeklyCalendar[FindTimeRow(time)].Wednesday.DurationInHours / 0.5;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(time) + i].Wednesday.Type = AppointmentType.none;
                        }
                        break;
                    case 4:
                        AppointmentStorage.Delete(WeeklyCalendar[FindTimeRow(time)].Thursday.IDAppointment);
                        numberOfCells = WeeklyCalendar[FindTimeRow(time)].Thursday.DurationInHours / 0.5;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(time) + i].Thursday.Type = AppointmentType.none;
                        }
                        break;
                    case 5:
                        AppointmentStorage.Delete(WeeklyCalendar[FindTimeRow(time)].Friday.IDAppointment);
                        numberOfCells = WeeklyCalendar[FindTimeRow(time)].Friday.DurationInHours / 0.5;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(time) + i].Friday.Type = AppointmentType.none;
                        }
                        break;
                    case 6:
                        AppointmentStorage.Delete(WeeklyCalendar[FindTimeRow(time)].Saturday.IDAppointment);
                        numberOfCells = WeeklyCalendar[FindTimeRow(time)].Saturday.DurationInHours / 0.5;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(time) + i].Saturday.Type = AppointmentType.none;
                        }
                        break;
                    case 7:
                        AppointmentStorage.Delete(WeeklyCalendar[FindTimeRow(time)].Sunday.IDAppointment);
                        numberOfCells = WeeklyCalendar[FindTimeRow(time)].Sunday.DurationInHours / 0.5;
                        for (int i = 1; i < numberOfCells; i++)
                        {
                            WeeklyCalendar[FindTimeRow(time) + i].Sunday.Type = AppointmentType.none;
                        }
                        break;
                }

                RefreshWeeklyCalendar();
            }
        }
    }
}
