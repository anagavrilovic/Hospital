using Hospital.Model;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class PatientSettingsFileRepository : IPatientSettingsRepository
    {
        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<PatientSettings> GetAll()
        {
            throw new NotImplementedException();
        }

        public PatientSettings GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(PatientSettings parameter)
        {
            throw new NotImplementedException();
        }

        public void Serialize(ObservableCollection<PatientSettings> parameter)
        {
            throw new NotImplementedException();
        }
    }
}
