using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IDoctorRepository : IGenericRepository<Doctor>
    {
        List<Doctor> GetBySpecialty(DoctorSpecialty doctorSpecialty);
        void Update(Doctor doctorForUpdating);
        String GetIDByNameSurname(string nameSurname);
    }
}
