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
        void DeleteByPatientID(string patientID);
        ObservableCollection<Appointment> GetByDoctorID(string doctorID);
        List<int> GetExistingIDs();
        ObservableCollection<Appointment> GetByPatientID(string patientID);
    }
}
