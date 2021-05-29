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
    public class DoctorAppointmentsViewModel : ViewModel
    {
        NavigationController navigationController;
        private NewAppointmentDTO dTO;
        public NewAppointmentDTO DTO
        {
            get { return dTO; }
            set
            {
                dTO = value;
                OnPropertyChanged("DTO");
            }
        }
        private DoctorAppointmentsController controller; 
        private RelayCommand newCommand;
        public RelayCommand NewCommand
        {
            get { return newCommand; }
            set
            {
                newCommand = value;
            }
        }
        private RelayCommand deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
            set
            {
                deleteCommand = value;
            }
        }
        private RelayCommand doubleClickCommand;
        public RelayCommand DoubleClickCommand
        {
            get { return doubleClickCommand; }
            set
            {
                doubleClickCommand = value;
            }
        }
        public DoctorAppointmentsViewModel(string IDnumber, NavigationController navigationController)
        {
            DTO = new NewAppointmentDTO();
            controller = new DoctorAppointmentsController(DTO);
            this.navigationController = navigationController;
            SetCommands();
            DTO.Doctor = controller.GetDoctorById(IDnumber);
            DTO.Appointments = new ObservableCollection<Appointment>(controller.InitAppointments());
        }

        private void SetCommands()
        {
            DoubleClickCommand = new RelayCommand(dataGridAppointments_MouseDoubleClick, CanExecute_Command);
            NewCommand = new RelayCommand(Execute_AddAppointment, CanExecute_Command);
            DeleteCommand = new RelayCommand(Execute_Delete, CanExecute_Command);
        }

        private void dataGridAppointments_MouseDoubleClick(object sender)
        {
            if (DTO.SelectedAppointment != null)
            {
                AppointmentDetails review = new AppointmentDetails(DTO.SelectedAppointment);
                review.Owner = Window.GetWindow(App.Current.MainWindow);
                review.Show();
                Window.GetWindow(App.Current.MainWindow).Hide();
            }
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
        private void Execute_Delete(object sender)
        {
            if (DTO.SelectedAppointment != null)
            {
                Appointment selectedAppointment = DTO.SelectedAppointment;
                controller.DeleteAppointment();
                MedicalRecord medicalRecord = controller.GetByPatientId();
                if (medicalRecord != null)
                {
                    controller.DeleteAppointmentFromExamination(medicalRecord);
                }
                DTO.Appointments.Remove(DTO.SelectedAppointment);
            }
        }

        private void Execute_AddAppointment(object sender)
        {
            NewAppointment newAppointment = new NewAppointment(DTO.Doctor.PersonalID,navigationController);
            newAppointment.Show();
        }
    }
}
