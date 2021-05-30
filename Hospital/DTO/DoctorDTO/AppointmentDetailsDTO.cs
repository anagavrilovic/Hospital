using Hospital.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.DTO.DoctorDTO
{
    public class AppointmentDetailsDTO : ViewModel
    {
        Appointment appointment = new Appointment();

        public Appointment Appointment
        {
            get { return appointment; }
            set
            {
                appointment = value;
                OnPropertyChanged("Appointment");
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
