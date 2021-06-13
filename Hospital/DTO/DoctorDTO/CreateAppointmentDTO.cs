using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class CreateAppointmentDTO : ViewModel
    {
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
        private ObservableCollection<Hospital.Model.Doctor> doctors;
        public ObservableCollection<Model.Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
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
        private Appointment appointment = new Appointment();
        public Appointment Appointment
        {
            get { return appointment; }
            set
            {
                appointment = value;
                OnPropertyChanged("Appointment");
            }

        }
    }
}
