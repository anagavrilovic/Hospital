using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class AppointmentInfoDTO : ViewModel
    {
        private ObservableCollection<Appointment> appointments = new ObservableCollection<Appointment>();
        public ObservableCollection<Appointment> Appointments
        {
            get => appointments;
            set
            {
                appointments = value;
                OnPropertyChanged("Appointments");
            }
        }
        private double _durationInHours;
        public double DurationInHours
        {
            get => _durationInHours;
            set
            {
                _durationInHours = value;
                OnPropertyChanged("DurationInHours");
            }
        }
    }
}
