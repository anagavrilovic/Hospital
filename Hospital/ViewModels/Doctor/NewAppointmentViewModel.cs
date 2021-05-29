using Hospital.Commands.DoctorCommands;
using Hospital.Controller;
using Hospital.Services;
using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.ViewModels.Doctor
{
    public class NewAppointmentViewModel : ViewModel
    {
        public Action CloseAction { get; set; }
        private RoomService roomService;
        private DoctorService doctorService;
        NavigationController navigationController;
        private AppointmentService appointmentService;
        private Appointment appointment;
        public Appointment Appointment
        {
            get { return appointment; }
            set
            {
                appointment = value;
                OnPropertyChanged("Appointment");
            }
        }
        private Room room;
        public Room Room
        {
            get { return room; }
            set
            {
                room = value;
                OnPropertyChanged("Room");
            }

        }
        private string timeOfAppointment;
        public string TimeOfAppointment
        {
            get { return timeOfAppointment; }
            set
            {
                timeOfAppointment = value;
                OnPropertyChanged("TimeOfAppointment");
            }

        }
        private DateTime dateOfAppointment;
        public DateTime DateOfAppointment
        {
            get { return dateOfAppointment; }
            set
            {
                dateOfAppointment = value;
                OnPropertyChanged("DateOfAppointment");
            }

        }
        private string durationInMinutes;


        public string DurationInMinutes
        {
            get { return durationInMinutes; }
            set
            {
                durationInMinutes = value;
                OnPropertyChanged("DurationInMinutes");
            }
        }
        private bool examinationIsChecked;
        public bool ExaminationIsChecked
        {
            get { return examinationIsChecked; }
            set
            {
                examinationIsChecked = value;
                OnPropertyChanged("ExaminationIsChecked");
            }
        }
        private bool operationIsChecked;
        public bool OperationIsChecked
        {
            get { return operationIsChecked; }
            set
            {
                operationIsChecked = value;
                OnPropertyChanged("OperationIsChecked");
            }
        }
        private Visibility operationVisibility;
        public Visibility OperationVisibility
        {
            get { return operationVisibility; }
            set
            {
                operationVisibility = value;
                OnPropertyChanged("OperationVisibility");
            }
        }
        private Visibility emergencyOperationLabel;
        public Visibility EmergencyOperationLabel
        {
            get { return emergencyOperationLabel; }
            set
            {
                emergencyOperationLabel = value;
                OnPropertyChanged("EmergencyOperationLabel");
            }
        }
        private Visibility emergencyOperationCheckBox;
        public Visibility EmergencyOperationCheckBox
        {
            get { return emergencyOperationCheckBox; }
            set
            {
                emergencyOperationCheckBox = value;
                OnPropertyChanged("EmergencyOperationCheckBox");
            }
        }
        private bool emergencyOperationIsChecked;
        public bool EmergencyOperationIsChecked
        {
            get { return emergencyOperationIsChecked; }
            set
            {
                emergencyOperationIsChecked = value;
                OnPropertyChanged("EmergencyOperationIsChecked");
                CheckBoxEmergency_Changed();
            }
        }

        private bool operationIsEnabled;
        public bool OperationIsEnabled
        {
            get { return operationIsEnabled; }
            set
            {
                operationIsEnabled = value;
                OnPropertyChanged("OperationIsEnabled");
            }
        }
        private ObservableCollection<string> setTimeComboBox;
        public ObservableCollection<string> SetTimeComboBox
        {
            get { return setTimeComboBox; }
            set
            {
                setTimeComboBox = value;
                OnPropertyChanged("SetTimeComboBox");
            }
        }
        private ObservableCollection<int> durationForComboBox;
        public ObservableCollection<int> DurationForComboBox
        {
            get { return durationForComboBox; }
            set
            {
                durationForComboBox = value;
                OnPropertyChanged("DurationForComboBox");
            }
        }
        private string patientLabel;
        public string PatientLabel
        {
            get { return patientLabel; }
            set
            {
                patientLabel = value;
                OnPropertyChanged("PatientLabel");
            }
        }
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
            }
        }
        private RelayCommand cancelCommand;
        public RelayCommand CancelCommand
        {
            get { return cancelCommand; }
            set
            {
                cancelCommand = value;
            }
        }
        private RelayCommand addPatientCommand;
        public RelayCommand AddPatientCommand
        {
            get { return addPatientCommand; }
            set
            {
                addPatientCommand = value;
            }
        }
        public NewAppointmentViewModel(string doctorId, NavigationController navigationController)
        {
            AddPatientCommand = new RelayCommand(Execute_AddPatient, canExecuteMethod);
            CancelCommand = new RelayCommand(Execute_Cancel, canExecuteMethod);
            SaveCommand = new RelayCommand(Execute_Save, canExecuteMethod);
            InitProperties(doctorId, navigationController);
            DateAndTimeComponents();
        }

        private bool canExecuteMethod(object parameter)
        {
            return true;
        }
        private void DateAndTimeComponents()
        {
            DateTime time = DateTime.Today;
            for (DateTime tm = time.AddHours(0); tm < time.AddHours(24); tm = tm.AddMinutes(30))
            {
                SetTimeComboBox.Add(tm.ToShortTimeString());

            }
            for (int i = 0; i < 15; i++)
            {
                DurationForComboBox.Add(((i + 1) * 30));
            }
        }

        private void InitProperties(string doctorId, NavigationController navigationController)
        {
            DurationForComboBox = new ObservableCollection<int>();
            SetTimeComboBox = new ObservableCollection<string>();
            this.navigationController = navigationController;
            roomService = new RoomService();
            appointmentService = new AppointmentService();
            doctorService = new DoctorService();
            Appointment = new Appointment();
            Appointment.Doctor = doctorService.GetDoctorById(doctorId);
            SetDoctorListAndRadioButtons();
        }

        private void SetDoctorListAndRadioButtons()
        {
            if (Appointment.Doctor.Specialty == (int)DoctorSpecialty.general)
            {
                OperationVisibility = Visibility.Hidden;
                ExaminationIsChecked = true;
                EmergencyOperationLabel = Visibility.Collapsed;
                EmergencyOperationCheckBox = Visibility.Collapsed;
            }
        }


        private void CheckBoxEmergency_Changed()
        {
            if (EmergencyOperationIsChecked.Equals(true))
            {
                ExaminationIsChecked = false;
                OperationIsChecked = true;
                OperationIsEnabled = true;
            }
            else
            {
                ExaminationIsChecked = true;
            }
        }

        private void Execute_Save(object sender)
        {
            if (!SomeFieldsEmpty())
            {
                FillAppointmentProperties();
                SetRoomAndRoomType();
                if (SaveIfNotOvelapping())
                {
                    UpdateParentPage();
                    CloseAction();
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
            navigationController.NavigateToDoctorAppointments(Appointment.Doctor.PersonalID, navigationController);
        }
        private void SetRoomAndRoomType()
        {
            foreach (Room storageRoom in roomService.GetAll())
            {
                if (ExaminationIsChecked.Equals(true) && Appointment.Doctor.RoomID.Equals(storageRoom.Id))
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
                        if (EmergencyOperationIsChecked.Equals(false))
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
            if (DateOfAppointment == null || (OperationIsChecked.Equals(false) && ExaminationIsChecked.Equals(false)) ||
                DateOfAppointment == null || TimeOfAppointment == null || DurationForComboBox == null)
            {
                ErrorBox messageBox = new ErrorBox("Nisu uneti svi podaci");
                return true;
            }
            return false;
        }

        private void Execute_Cancel(object sender)
        {
            CloseAction();
        }

        private void Execute_AddPatient(object sender)
        {
            AddPatientWindow patientListBox = new AddPatientWindow(this);
            //patientListBox.Owner = this;
            patientListBox.Show();
        }
    }
}
