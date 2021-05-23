using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        void DeleteByPatientID(string patientID);
        bool IsDoctorAvaliableForAppointment(Appointment newAppointment);
        bool IsPatientAvaliableForAppointment(Appointment newAppointment);
        String GetNewID();
    }
}
