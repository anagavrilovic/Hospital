using Hospital.Factory;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.ViewModels.Patient
{
    class PatientPassedAppointmentsViewModel
    {
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }

        private AppointmentService appointmentService = new AppointmentService();
        public PatientPassedAppointmentsViewModel()
        {
            Appointments = new ObservableCollection<Appointment>(appointmentService.GetPassedAppointmentsForPatient(MainWindow.IDnumber));
        }

        
    }
}
