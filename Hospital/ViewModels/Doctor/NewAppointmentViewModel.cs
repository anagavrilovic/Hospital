using Hospital.Commands.DoctorCommands;
using Hospital.Controller;
using Hospital.Controller.DoctorControllers;
using Hospital.DTO.DoctorDTO;
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
        NavigationController navigationController;
        private NewAppointmentController controller;
        private AddAppointmentDTO dTO;
        public AddAppointmentDTO DTO
        {
            get { return dTO; }
            set
            {
                dTO = value;
                OnPropertyChanged("DTO");
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
            CreateCommands();
            InitProperties(doctorId, navigationController);
            DateAndTimeComponents();
        }

        private void CreateCommands()
        {
            AddPatientCommand = new RelayCommand(Execute_AddPatient, canExecuteMethod);
            CancelCommand = new RelayCommand(Execute_Cancel, canExecuteMethod);
            SaveCommand = new RelayCommand(Execute_Save, canExecuteMethod);
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
                DTO.SetTimeComboBox.Add(tm.ToShortTimeString());

            }
            for (int i = 0; i < 15; i++)
            {
                DTO.DurationForComboBox.Add(((i + 1) * 30));
            }
        }

        private void InitProperties(string doctorId, NavigationController navigationController)
        {
            DTO = new AddAppointmentDTO();
            controller = new NewAppointmentController(DTO);
            DTO.DurationForComboBox = new ObservableCollection<int>();
            DTO.SetTimeComboBox = new ObservableCollection<string>();
            this.navigationController = navigationController;
            DTO.Appointment = new Appointment();
            DTO.Appointment.Doctor = controller.GetDoctorById(doctorId);
            SetDoctorListAndRadioButtons();
        }

        private void SetDoctorListAndRadioButtons()
        {
            if (DTO.Appointment.Doctor.Specialty == (int)DoctorSpecialty.general)
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
            navigationController.NavigateToDoctorAppointments(DTO.Appointment.Doctor.PersonalID, navigationController);
        }
        private void SetRoomAndRoomType()
        {
            foreach (Room storageRoom in controller.GetAllRooms())
            {
                if (ExaminationIsChecked.Equals(true) && DTO.Appointment.Doctor.RoomID.Equals(storageRoom.Id))
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
                        if (EmergencyOperationIsChecked.Equals(false))
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
            DTO.Appointment.IDAppointment = controller.GenereteAppointmentId();
            double timeInMinutesDouble = double.Parse(DTO.DurationInMinutes);
            DTO.Appointment.DurationInHours = timeInMinutesDouble / 60;
        }
        private bool SomeFieldsEmpty()
        {
            if (DTO.DateOfAppointment == null || (OperationIsChecked.Equals(false) && ExaminationIsChecked.Equals(false)) ||
                DTO.DateOfAppointment == null || DTO.TimeOfAppointment == null || DTO.DurationForComboBox == null)
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
