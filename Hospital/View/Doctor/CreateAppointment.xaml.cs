using Hospital.DTO.DoctorDTO;
using Hospital.Model;
using Hospital.Services;
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
        private CreateAppointmentController controller;
        private CreateAppointmentDTO dTO;
        public CreateAppointmentDTO DTO
        {
            get { return dTO; }
            set
            {
                dTO = value;
                OnPropertyChanged("DTO");
            }

        }
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
            DTO = new CreateAppointmentDTO();
            controller = new CreateAppointmentController(DTO);
            DTO.Appointment.PatientsRecord = (controller.GetMedicalRecordByPatientId(id));
            SetDoctorListAndRadioButtons();
        }

        private void SetDoctorListAndRadioButtons()
        {
            DTO.Doctors = new ObservableCollection<Model.Doctor>();
            if (specialization == (int)DoctorSpecialty.general)
            {
                rdbOperacija.Visibility = Visibility.Hidden;
                rdbPregled.IsChecked = true;
                DTO.Doctors = new ObservableCollection<Model.Doctor>(controller.GetAllDoctors());
                emergencyOperationLabel.Visibility = Visibility.Collapsed;
                emergencyOperationCheckBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                foreach (Model.Doctor doctor in controller.GetAllDoctors())
                {
                    if (specialization == (int)doctor.Specialty)
                    {
                        DTO.Doctors.Add(doctor);
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
                controller.SaveAppointment();
                return true;
            }   
            else
                return false;
        }

        private bool IsDoctorAvaliable()
        {
            if (!controller.IsDoctorAvaliableForAppointment())
            {
                ErrorBox errorBox = new ErrorBox("Doktor je već zauzet u ovom terminu. Promenite trajanje ili odaberite drugi termin!");
                return false;
            }

            return true;
        }

        private bool IsPatientAvaliable()
        {
            if (!controller.IsPatientAvaliableForAppointment())
            {
                ErrorBox errorBox = new ErrorBox("Ovaj pacijent već ima zakazan pregled/operaciju u ovom terminu!");
                return false;
            }

            return true;
        }

        private void UpdateParentPage()
        {
            ObservableCollection<Appointment> list = new ObservableCollection<Appointment>();
            list =new ObservableCollection<Appointment>(controller.SetParentAppointments());
            parentWindow.dataGridPregledi.ItemsSource = list;
            parentWindow.dataGridPregledi.Items.Refresh();
            parentWindow.ComboBox.SelectedIndex=(int)(controller.GetDoctorById(DTO.Appointment.IDDoctor).Specialty);
        }

        private void SetRoomAndRoomType()
        {
            foreach (Room storageRoom in controller.GetAllRooms())
            {
                if (rdbPregled.IsChecked.Equals(true) && DTO.Appointment.Doctor.RoomID.Equals(storageRoom.Id))
                {
                    DTO.Appointment.Room = storageRoom;
                    DTO.Appointment.Type = AppointmentType.examination;
                    break;
                }
                else
                {
                    if (storageRoom.Type.Equals(RoomType.OPERACIONA_SALA))
                    {
                        DTO.Appointment.Room = storageRoom;
                        if(emergencyOperationCheckBox.IsChecked.Equals(false))
                            DTO.Appointment.Type = AppointmentType.operation;
                        else
                            DTO.Appointment.Type = AppointmentType.urgentExamination;
                        break;
                    }
                }
            }
        }


        private void FillAppointmentProperties()
        {
            DTO.Appointment.IDpatient = DTO.Appointment.PatientsRecord.Patient.PersonalID;
            DTO.Appointment.DateTime = DTO.DateOfAppointment.Date.Add(DateTime.Parse(DTO.TimeOfAppointment).TimeOfDay);
            DTO.Appointment.IDDoctor = DTO.Appointment.Doctor.PersonalID;
            DTO.Appointment.IDAppointment = controller.GenerateAppointmentId();
            double timeInMinutesDouble = double.Parse(DTO.DurationInMinutes);
            DTO.Appointment.DurationInHours = timeInMinutesDouble / 60;
        }

        private bool SomeFieldsEmpty()
        {
            if (DTO.DateOfAppointment == null ||  (rdbOperacija.IsChecked.Equals(false) && rdbPregled.IsChecked.Equals(false)) || 
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
                if (doctorComboBox.SelectedIndex != -1)
                {
                    if (!((AppointmentWindow)Window.GetWindow(this.Owner)).LoggedInDoctor.PersonalID.Equals
                   (((Model.Doctor)doctorComboBox.SelectedItem).PersonalID))
                    {
                        rdbOperacija.IsEnabled = false;
                        rdbPregled.IsChecked = true;
                    }
                }
            }
        }
    }
}
