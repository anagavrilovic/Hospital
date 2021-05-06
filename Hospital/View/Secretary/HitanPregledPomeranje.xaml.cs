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

        // Storage class properties
        public AppointmentStorage AppointmentStorage { get; set; }
        public MedicalRecordStorage MedicalRecordStorage { get; set; }
        public DoctorStorage DoctorStorage { get; set; }


        // Methods
        public HitanPregledPomeranje(Appointment newUrgentAppointment, DoctorSpecialty doctorsSpecialty)
        {
            InitializeComponent();
            this.DataContext = this;
            this.NewUrgentAppointment = newUrgentAppointment;
            this.DoctorsSpecialty = doctorsSpecialty;
            InitializeEmptyObjects();
            FindAllOptions();

            /*MoveAppointment ma = new MoveAppointment(AppointmentStorage.GetByID("1"));
            OptionForRescheduling ofr = new OptionForRescheduling();
            ofr.Option.Add(ma);
            Options.Add(ofr);*/
        }

        private void InitializeEmptyObjects()
        {
            Options = new ObservableCollection<OptionForRescheduling>();
            AppointmentStorage = new AppointmentStorage();
            MedicalRecordStorage = new MedicalRecordStorage();
            DoctorStorage = new DoctorStorage();
        }

        private void FindAllOptions()
        {
            ObservableCollection<Model.Doctor> possibleDoctors = DoctorStorage.GetDoctorsBySpecialty(DoctorsSpecialty);

            foreach(Model.Doctor doctor in possibleDoctors)
            {
                SetDateTimeForNewAppointment();
                SetDoctorForNewAppointment(doctor);

                for(int i = 0; i < 3; i++)
                {
                    ObservableCollection<Appointment> overlappingAppointments = AppointmentStorage.GetOverlappingAppointments(NewUrgentAppointment);
                    if (HasAppointmentStarted(overlappingAppointments))
                    {
                        NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(30);
                        continue;
                    }

                    OptionForRescheduling optionForRescheduling = new OptionForRescheduling();
                    foreach(Appointment appointment in overlappingAppointments)
                    {
                        MoveAppointment moveAppointment = new MoveAppointment(appointment);
                        optionForRescheduling.Option.Add(moveAppointment);
                    }

                    InsertIfOptionDoesntExist(optionForRescheduling);

                    NewUrgentAppointment.DateTime = NewUrgentAppointment.DateTime.AddMinutes(30);
                }
            }
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

        private void SetDoctorForNewAppointment(Model.Doctor doctor)
        {
            NewUrgentAppointment.IDDoctor = doctor.PersonalID;
            NewUrgentAppointment.DoctrosNameSurname = doctor.FirstName + " " + doctor.LastName;
        }

        private void InsertIfOptionDoesntExist(OptionForRescheduling optionForRescheduling)
        {
            if (optionForRescheduling.Option.Count() == 0)
                return;

            foreach(OptionForRescheduling option in Options)
            {
                if (option.IsEqual(optionForRescheduling))
                    return;
            }

            Options.Add(optionForRescheduling);
        }

        private bool HasAppointmentStarted(ObservableCollection<Appointment> overlappingAppointments)
        {
            foreach(Appointment appointment in overlappingAppointments)
            {
                if (appointment.DateTime < DateTime.Now)
                    return true;
            }

            return false;
        }

        private void BtnPotvrdiClick(object sender, RoutedEventArgs e)
        {

        }

        private void BtnOdustaniClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HitanPregled());
        }
    }
}
