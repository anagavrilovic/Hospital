using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IMedicalRecordRepository : IGenericRepository<MedicalRecord>
    {
        void UpdatePatientsInformation(MedicalRecord medicalRecord);
        List<int> GetExistingIDs();
        MedicalRecord GetByUsername(string username);
        MedicalRecord GetByPatientID(string id);
    }
}
