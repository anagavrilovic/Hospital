using Hospital.Model;
using Hospital.Services;
using Hospital.Services.DoctorServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
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
  
    public partial class CreateAppointment : Window,INotifyPropertyChanged
    {
        private ObservableCollection<Hospital.Model.Doctor> doctors;
        private Appointment appointment=new Appointment();
        public Appointment Appointment { get => appointment; set => appointment = value; } 
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
        public ObservableCollection<Hospital.Model.Doctor> Doctors  
            {
                get{ return doctors;}
                set {
                doctors = value;
                OnPropertyChanged();
            }

            }
        private string durationInMinutes;
        public string DurationInMinutes
        {
            get { return durationInMinutes; }
            set
            {
                durationInMinutes = value;
                OnPropertyChanged();
            }

        }
        private CreateAppointmentService createAppointmentService;
        private RoomService roomService;
        private DoctorService doctorService;
        private MedicalRecordService medicalRecordService;
        private AppointmentService appointmentService;
        private int specialization;
        public AppointmentInfo parentWindow { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            if(PropertyChanged != null)
            {
               this.PropertyChanged.Invoke(this, new PropertyChangedEventArgs(name));
            }
        }

        public CreateAppointment(AppointmentInfo parentWindow,string patientID,int specialization)
        {
            InitializeComponent();
            this.DataContext = this;
            this.specialization = specialization;
            InitProperties(patientID);
            DateAndTimeComponents();
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 1 / 2);
            this.parentWindow = parentWindow;
        }

        private void InitProperties(string id)
        {
            createAppointmentService = new CreateAppointmentService();
            roomService = new RoomService();
            doctorService = new DoctorService();
            medicalRecordService = new MedicalRecordService();
            appointmentService = new AppointmentService();
            Appointment.PatientsRecord = (medicalRecordService.GetByPatientId(id));
            SetDoctorListAndRadioButtons();
        }

        private void SetDoctorListAndRadioButtons()
        {
            Doctors = new ObservableCollection<Model.Doctor>();
            if (specialization == (int)DoctorSpecialty.general)
            {
                rdbOperacija.Visibility = Visibility.Hidden;
                rdbPregled.IsChecked = true;
                Doctors = new ObservableCollection<Model.Doctor>(doctorService.GetAll());
                emergencyOperationLabel.Visibility = Visibility.Collapsed;
                emergencyOperationCheckBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (Model.Doctor doctor in doctorService.GetAll())
                {
                    if (specialization == (int)doctor.Specialty)
                    {
                        Doctors.Add(doctor);
                    }
                }
            }
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
                durationInMinutesComboBox.Items.Add(((i + 1) * 10));
            }
            durationInMinutesComboBox.SelectedIndex = 1;
            timeComboBox.SelectedIndex = 7;
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            parentWindow.addButton.IsEnabled = true;
            Close();
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
            ((AppointmentWindow)Window.GetWindow(this.Owner)).Examintaion.appointment = Appointment;
            ObservableCollection<Appointment> list = new ObservableCollection<Appointment>();
            list =createAppointmentService.SetParentAppointments(Appointment);
            parentWindow.dataGridPregledi.ItemsSource = list;
            parentWindow.dataGridPregledi.Items.Refresh();
            parentWindow.ComboBox.SelectedIndex=(int)(doctorService.GetDoctorById(Appointment.IDDoctor).Specialty);
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
                        if(emergencyOperationCheckBox.IsChecked.Equals(false))
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
            if (DateOfAppointment == null ||  (rdbOperacija.IsChecked.Equals(false) && rdbPregled.IsChecked.Equals(false)) || 
                doctorComboBox.SelectedIndex == -1 || datePicker.SelectedDate==null)
            {
                ErrorBox messageBox =new ErrorBox("Nisu uneti svi podaci");
                return true;
            }
            return false;
        }

        private void SelectedDoctorChanged(object sender, SelectionChangedEventArgs e)
        {
           if(((AppointmentWindow)Window.GetWindow(this.Owner)).LoggedInDoctor.PersonalID.Equals
                (((Model.Doctor)doctorComboBox.SelectedItem).PersonalID))
            {
                rdbOperacija.IsEnabled = true;
            }
            else
            {
                rdbOperacija.IsEnabled = false;
            }
        }

        private void CheckBoxEmergency_Changed(object sender, RoutedEventArgs e)
        {
            if (emergencyOperationCheckBox.IsChecked.Equals(true))
            {
                rdbPregled.IsEnabled = false;
                rdbOperacija.IsChecked = true;
                rdbOperacija.IsEnabled = true;
            }else 
            {
                rdbPregled.IsEnabled = true;
                if (!((AppointmentWindow)Window.GetWindow(this.Owner)).LoggedInDoctor.PersonalID.Equals
               (((Hospital.Model.Doctor)doctorComboBox.SelectedItem).PersonalID))
                {
                    rdbOperacija.IsEnabled = false;
                    rdbPregled.IsChecked = true; 
                }
            }
        }
    }
}
