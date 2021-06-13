using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class NewAppointmentDTO : ViewModel
    {
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

    }
}
