using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Factory
{
   public class PatientSettingsFileFactory : IPatientSettingsRepositoryFactory
    {
        public IPatientSettingsRepository CreatePatientSettingsRepository()
        {
            return new PatientSettingsFileRepository();
        }
    }
}
