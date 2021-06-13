using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class AddAppointmentDTO : ViewModel
    {
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
    }
}
