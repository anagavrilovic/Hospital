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
    class PatientAppointmentsViewModel
    {
        public ObservableCollection<Appointment> Appointments
        {
            get;
            set;
        }

        private AppointmentService appointmentService = new AppointmentService();

        public PatientAppointmentsViewModel()
        {
            Appointments = new ObservableCollection<Appointment>(appointmentService.GetByPatientID(MainWindow.IDnumber));
        }
    }
}
