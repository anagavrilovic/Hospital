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

namespace Hospital.View.Secretary
{
    /// <summary>
    /// Interaction logic for HitanPregledPomeranje.xaml
    /// </summary>
    public partial class HitanPregledPomeranje : Page
    {
        // Properties
        public Appointment NewUrgentAppointment { get; set; }
        public DoctorSpecialty DoctorsSpecialty { get; set; }
        public ObservableCollection<OptionForRescheduling> Options { get; set; }
        public OptionForRescheduling SelectedOption { get; set; }

        // Storage class properties
        public AppointmentStorage AppointmentStorage { get; set; }
        public MedicalRecordStorage MedicalRecordStorage { get; set; }
        public DoctorStorage DoctorStorage { get; set; }
        public RoomStorage RoomStorage { get; set; }


        // Methods
        public HitanPregledPomeranje(Appointment newUrgentAppointment, DoctorSpecialty doctorsSpecialty)
        {
            InitializeComponent();
            this.DataContext = this;
            this.NewUrgentAppointment = newUrgentAppointment;
            this.DoctorsSpecialty = doctorsSpecialty;
            InitializeEmptyObjects();
            FindAllOptions();
        }

        private void InitializeEmptyObjects()
        {
            Options = new ObservableCollection<OptionForRescheduling>();
            AppointmentStorage = new AppointmentStorage();
            MedicalRecordStorage = new MedicalRecordStorage();
            DoctorStorage = new DoctorStorage();
            RoomStorage = new RoomStorage();
        }

        private void FindAllOptions()
        {
            ObservableCollection<Model.Doctor> possibleDoctors = DoctorStorage.GetDoctorsBySpecialty(DoctorsSpecialty);

            foreach (Model.Doctor doctor in possibleDoctors)
            {
                SetDateTimeForNewAppointment();
                SetDoctorForNewUrgentAppointment(doctor);

                for (int i = 0; i < 3; i++)
                {
                    ObservableCollection<Appointment> overlappingAppointments = AppointmentStorage.GetOverlappingAppointments(NewUrgentAppointment);
                    if (HasAppointmentStarted(overlappingAppointments))
                    {
                        NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(30);
                        continue;
                    }

                    OptionForRescheduling optionForRescheduling = new OptionForRescheduling();
                    foreach (Appointment appointment in overlappingAppointments)
                    {
                        MoveAppointment moveAppointment = new MoveAppointment(appointment);
                        optionForRescheduling.Option.Add(moveAppointment);
                    }

                    InsertIfOptionDoesntExist(optionForRescheduling);

                    NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(30);
                }
            }

            SortOptions();
        }

        private void SetDateTimeForNewAppointment()
        {
            var currentDateTime = DateTime.Now;
            NewUrgentAppointment.DateTime = new DateTime(currentDateTime.Year, currentDateTime.Month, currentDateTime.Day, currentDateTime.Hour,
                currentDateTime.Minute, currentDateTime.Second, currentDateTime.Kind);
            NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddSeconds(-NewUrgentAppointment.DateTime.Second);

            if (NewUrgentAppointment.DateTime.Minute > 0 && NewUrgentAppointment.DateTime.Minute < 30)
                NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(30 - NewUrgentAppointment.DateTime.Minute);
            else if (NewUrgentAppointment.DateTime.Minute > 30 && NewUrgentAppointment.DateTime.Minute <= 59)
                NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(60 - NewUrgentAppointment.DateTime.Minute);
        }

        private void SetDoctorForNewUrgentAppointment(Model.Doctor doctor)
        {
            NewUrgentAppointment.IDDoctor = doctor.PersonalID;
            NewUrgentAppointment.DoctrosNameSurname = doctor.FirstName + " " + doctor.LastName;
        }

        private void InsertIfOptionDoesntExist(OptionForRescheduling optionForRescheduling)
        {
            if (optionForRescheduling.Option.Count() == 0)
                return;

            foreach (OptionForRescheduling option in Options)
                if (option.IsEqual(optionForRescheduling))
                    return;

            foreach (MoveAppointment moveAppointment in optionForRescheduling.Option)
            {
                if (moveAppointment.Appointment.Type.Equals(AppointmentType.urgentExamination) || moveAppointment.Appointment.Type.Equals(AppointmentType.urgentOperation))
                    return;
            }

            optionForRescheduling.SetTimeForRescheduling(NewUrgentAppointment);
            Options.Add(optionForRescheduling);
        }

        private bool HasAppointmentStarted(ObservableCollection<Appointment> overlappingAppointments)
        {
            foreach (Appointment appointment in overlappingAppointments)
            {
                if (appointment.DateTime < DateTime.Now)
                    return true;
            }

            return false;
        }

        private void SortOptions()
        {
            List<OptionForRescheduling> sortedList = Options.OrderBy(o => o.NewUrgentAppointmentTime).ToList();
            Options.Clear();
            Options = new ObservableCollection<OptionForRescheduling>(sortedList);
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            if (SelectedOption == null)
                MessageBox.Show("Selektujte opciju za pomeranje već postojećih termina!");

            SetTimeForNewUrgentAppointment(SelectedOption.NewUrgentAppointmentTime);
            SetDoctorForNewUrgentAppointment(SelectedOption.Option[0].Doctor);

            AppointmentStorage.RescheduleAppointments(SelectedOption.Option);
            SetRoomForNewUrgentAppointment();

            AppointmentStorage.Save(NewUrgentAppointment);
            AppointmentStorage.NotifyDoctor(NewUrgentAppointment);

            var urgentAppointmentDetails = new HitanPregledDetalji(NewUrgentAppointment);
            urgentAppointmentDetails.Show();

            NavigationService.Navigate(new HitanPregled());
        }

        private void SetTimeForNewUrgentAppointment(DateTime dateTime)
        {
            NewUrgentAppointment.DateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Kind);
        }

        private void SetRoomForNewUrgentAppointment()
        {
            ObservableCollection<Room> avaliableRooms = RoomStorage.GetAvaliableRooms(NewUrgentAppointment);
            NewUrgentAppointment.Room = avaliableRooms[0];
        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HitanPregled());
        }
    }
}
