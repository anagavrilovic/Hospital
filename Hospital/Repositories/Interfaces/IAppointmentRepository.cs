using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        void DeletePatientsAppointments(string patientID);
        bool IsDoctorAvaliableForAppointment(Appointment newAppointment);
        bool IsPatientAvaliableForAppointment(Appointment newAppointment);
        String GetNewID();
        List<Appointment> GetByDoctorID(string doctorID);
        List<int> GetExistingIDs();
        List<Appointment> GetByPatientID(string patientID);
        Appointment SetDoctorForAppointment(Appointment appointment);
        List<Appointment> GetPassedAppointmentsForPatient(String id);
        List<Appointment> GetPassedAppointments();

    }
}
