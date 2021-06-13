using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
  public  interface IHospitalTreatmentRepository : IGenericRepository<HospitalTreatment>
    {
        void EditHospitalTreatment(HospitalTreatment hospitalTreatment);
    }
}
