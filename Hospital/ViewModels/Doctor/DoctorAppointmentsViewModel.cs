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
    public class DoctorAppointmentsViewModel : ViewModel
    {
        private AppointmentService AppointmentService = new AppointmentService();
        private MedicalRecordService medicalRecordService = new MedicalRecordService();
        private DoctorService doctorService = new DoctorService();
        NavigationController navigationController;
        private Model.Doctor doctor = new Model.Doctor();
        public Model.Doctor Doctor
        {
            get { return doctor; }
            set
            {
                doctor = value;
                OnPropertyChanged("Doctor");
            }
        }
        private Appointment selectedAppointment;
        public Appointment SelectedAppointment
        {
            get { return selectedAppointment; }
            set
            {
                selectedAppointment = value;
                OnPropertyChanged("SelectedAppointment");
            }
        }
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
        private ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> Appointments
        {
            get { return appointments; }
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }
        public DoctorAppointmentsViewModel(string IDnumber, NavigationController navigationController)
        {
            this.navigationController = navigationController;
            DoubleClickCommand = new RelayCommand(dataGridAppointments_MouseDoubleClick, CanExecute_Command);
            NewCommand = new RelayCommand(Execute_AddAppointment, CanExecute_Command);
            DeleteCommand = new RelayCommand(Execute_Delete, CanExecute_Command);
            Doctor = doctorService.GetDoctorById(IDnumber);
            Appointments = new ObservableCollection<Appointment>(AppointmentService.InitAppointments(IDnumber));
        }

        private void dataGridAppointments_MouseDoubleClick(object sender)
        {
            if (SelectedAppointment != null)
            {
                AppointmentDetails review = new AppointmentDetails(SelectedAppointment);
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
            if (SelectedAppointment != null)
            {
                Appointment selectedAppointment = SelectedAppointment;
                AppointmentService.DeleteAppointment(selectedAppointment.IDAppointment);
                MedicalRecord medicalRecord = medicalRecordService.GetByPatientId(selectedAppointment.IDpatient);
                if (medicalRecord != null)
                {
                    medicalRecordService.DeleteAppointmentFromExamination(selectedAppointment.IDAppointment, medicalRecord);
                }
                appointments.Remove(SelectedAppointment);
            }
        }

        private void Execute_AddAppointment(object sender)
        {
            NewAppointment newAppointment = new NewAppointment(doctor.PersonalID,navigationController);
            newAppointment.Show();
        }
    }
}
