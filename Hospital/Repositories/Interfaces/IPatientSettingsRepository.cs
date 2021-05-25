using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IPatientSettingsRepository : IGenericRepository<PatientSettings>
    {
        void Update(PatientSettings patientSettings);

        void FirstSave(String id);
    }
}
