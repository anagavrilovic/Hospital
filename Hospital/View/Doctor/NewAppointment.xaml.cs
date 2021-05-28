using Hospital.Model;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Hospital.View.Doctor
{
    /// <summary>
    /// Interaction logic for NoviTermin.xaml
    /// </summary>
    public partial class NewAppointment : Window,INotifyPropertyChanged
    {
        private MedicalRecordService medicalRecordService;
        private RoomService roomService;
        private DoctorService doctorService;
        private AppointmentService appointmentService;
        private Appointment appointment;
        public Appointment Appointment {
            get { return appointment; }
            set
            {
                appointment = value;
                OnPropertyChanged();
            }
        }
        private Room room;
        public Room Room
        {
            get { return room; }
            set
            {
                room = value;
                OnPropertyChanged();
            }

        }
        private string timeOfAppointment;
        public string TimeOfAppointment
        {
            get { return timeOfAppointment; }
            set
            {
                timeOfAppointment = value;
                OnPropertyChanged();
            }

        }
        private DateTime dateOfAppointment;
        public DateTime DateOfAppointment
        {
            get { return dateOfAppointment; }
            set
            {
                dateOfAppointment = value;
                OnPropertyChanged();
            }

        }
        private string durationInMinutes;

        public event PropertyChangedEventHandler PropertyChanged;

        public string DurationInMinutes
        {
            get { return durationInMinutes; }
            set
            {
                durationInMinutes = value;
                OnPropertyChanged();
            }

        }
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if (PropertyChanged != null)
            {
                this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }
        public NewAppointment(string doctorId)
        {
            InitializeComponent();
            this.DataContext = this;
            InitProperties(doctorId);
            DateAndTimeComponents();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 1 / 2);
        }

        private void DateAndTimeComponents()
        {
            DateTime time = DateTime.Today;
            for (DateTime tm = time.AddHours(0); tm < time.AddHours(24); tm = tm.AddMinutes(30))
            {
                timeComboBox.Items.Add(tm.ToShortTimeString());

            }
            for (int i = 0; i < 15; i++)
            {
                durationInMinutesComboBox.Items.Add(((i + 1) * 30));
            }
            durationInMinutesComboBox.SelectedIndex = 1;
            timeComboBox.SelectedIndex = 7;
        }

        private void InitProperties(string doctorId)
        {
            roomService = new RoomService();
            appointmentService = new AppointmentService();
            medicalRecordService = new MedicalRecordService();
            doctorService = new DoctorService();
            Appointment = new Appointment();
            Appointment.Doctor = doctorService.GetDoctorById(doctorId);
            SetDoctorListAndRadioButtons();
        }

        private void SetDoctorListAndRadioButtons()
        {
            if (Appointment.Doctor.Specialty == (int)DoctorSpecialty.general)
            {
                rdbOperacija.Visibility = Visibility.Hidden;
                rdbPregled.IsChecked = true;
                emergencyOperationLabel.Visibility = Visibility.Collapsed;
                emergencyOperationCheckBox.Visibility = Visibility.Collapsed;
            }
        }

        private void CheckBoxEmergency_Changed(object sender, RoutedEventArgs e)
        {
            if (emergencyOperationCheckBox.IsChecked.Equals(true))
            {
                rdbPregled.IsEnabled = false;
                rdbOperacija.IsChecked = true;
                rdbOperacija.IsEnabled = true;
            }
            else
            {
                rdbPregled.IsEnabled = true;
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (!SomeFieldsEmpty())
            {
                FillAppointmentProperties();
                SetRoomAndRoomType();
                if (SaveIfNotOvelapping())
                {
                    UpdateParentPage();
                    this.Close();
                }
                else
                {
                    ErrorBox errorBox = new ErrorBox("Ovaj termin ima preklapanja");
                }
            }
        }
        private bool SaveIfNotOvelapping()
        {
            if (IsDoctorAvaliable() && IsPatientAvaliable())
            {
                appointmentService.Save(Appointment);
                return true;
            }
            else
                return false;
        }
        private bool IsDoctorAvaliable()
        {
            if (!appointmentService.IsDoctorAvaliableForAppointment(Appointment))
            {
                ErrorBox errorBox = new ErrorBox("Doktor je već zauzet u ovom terminu. Promenite trajanje ili odaberite drugi termin!");
                return false;
            }

            return true;
        }

        private bool IsPatientAvaliable()
        {
            if (!appointmentService.IsPatientAvaliableForAppointment(Appointment))
            {
                ErrorBox errorBox = new ErrorBox("Ovaj pacijent već ima zakazan pregled/operaciju u ovom terminu!");
                return false;
            }

            return true;
        }
        private void UpdateParentPage()
        {
            ObservableCollection<Appointment> list = new ObservableCollection<Appointment>();
            foreach (Appointment a in appointmentService.GetAll())
            {
                if (doctorService.GetDoctorById(a.IDDoctor).PersonalID.Equals(Appointment.Doctor.PersonalID))
                {
                    a.PatientsRecord = medicalRecordService.GetByPatientId(a.IDpatient);
                    a.Doctor = doctorService.GetDoctorById(a.IDDoctor);
                    list.Add(a);
                }
            }
        }
        private void SetRoomAndRoomType()
        {
            foreach (Room storageRoom in roomService.GetAll())
            {
                if (rdbPregled.IsChecked.Equals(true) && Appointment.Doctor.RoomID.Equals(storageRoom.Id))
                {
                    Appointment.Room = storageRoom;
                    Appointment.Type = AppointmentType.examination;
                    break;
                }
                else
                {
                    if (storageRoom.Type.Equals(RoomType.OPERACIONA_SALA))
                    {
                        Appointment.Room = storageRoom;
                        if (emergencyOperationCheckBox.IsChecked.Equals(false))
                            Appointment.Type = AppointmentType.operation;
                        else
                            Appointment.Type = AppointmentType.urgentExamination;
                        break;
                    }
                }
            }
        }
        private void FillAppointmentProperties()
        {
            Appointment.IDpatient = Appointment.PatientsRecord.Patient.PersonalID;
            Appointment.DateTime = DateOfAppointment.Date.Add(DateTime.Parse(timeOfAppointment).TimeOfDay);
            Appointment.IDDoctor = Appointment.Doctor.PersonalID;
            Appointment.IDAppointment = appointmentService.GenerateID();
            double timeInMinutesDouble = double.Parse(DurationInMinutes);
            Appointment.DurationInHours = timeInMinutesDouble / 60;
        }
        private bool SomeFieldsEmpty()
        {
            if (DateOfAppointment == null || (rdbOperacija.IsChecked.Equals(false) && rdbPregled.IsChecked.Equals(false)) ||
                datePicker.SelectedDate == null)
            {
                ErrorBox messageBox = new ErrorBox("Nisu uneti svi podaci");
                return true;
            }
            return false;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void addPatient_Click(object sender, RoutedEventArgs e)
        {
            AddPatientWindow patientListBox = new AddPatientWindow(this);
            patientListBox.Owner = this;
            patientListBox.Show();
        }
    }
}
