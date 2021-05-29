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

namespace Hospital.View.Secretary
{
    public partial class HitanPregledPomeranje : Page
    {
        public Appointment NewUrgentAppointment { get; set; }
        public DoctorSpecialty DoctorsSpecialty { get; set; }
        public ObservableCollection<OptionForRescheduling> Options { get; set; }
        public OptionForRescheduling SelectedOption { get; set; }

        public AppointmentStorage AppointmentStorage { get; set; }
        public MedicalRecordStorage MedicalRecordStorage { get; set; }
        public DoctorStorage DoctorStorage { get; set; }
        public RoomStorage RoomStorage { get; set; }

        public AppointmentService AppointmentService { get; set; }
        public MedicalRecordService MedicalRecordService { get; set; }
        public DoctorService DoctorService { get; set; }
        public RoomService RoomService { get; set; }


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
            AppointmentService = new AppointmentService();
            MedicalRecordService = new MedicalRecordService();
            DoctorService = new DoctorService();
            RoomService = new RoomService();

            AppointmentStorage = new AppointmentStorage();
            MedicalRecordStorage = new MedicalRecordStorage();
            DoctorStorage = new DoctorStorage();
            RoomStorage = new RoomStorage();
        }

        private void FindAllOptions()
        {
            Options = new ObservableCollection<OptionForRescheduling>(AppointmentService.
                FindAllOptionsForRescheduling(NewUrgentAppointment, DoctorsSpecialty));
        }

        private void SetDoctorForNewUrgentAppointment(Model.Doctor doctor)
        {
            NewUrgentAppointment.IDDoctor = doctor.PersonalID;
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {
            if (SelectedOption == null)
            {
                InformationBox informationBox = new InformationBox("Selektujte opciju za pomeranje već postojećih termina!");
                informationBox.ShowDialog();
                return;
            }

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
