using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IPatientNotificationsRepository : IGenericRepository<PatientTherapyMedicineNotification>
    {
        List<PatientTherapyMedicineNotification> GetByPatientID();
        void Update(PatientTherapyMedicineNotification patientTherapyMedicineNotification);
        String GetNewID();
    }
}
