using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class DoctorService
    {
        private IDoctorRepository doctorRepository;

        public DoctorService()
        {
            doctorRepository = new DoctorFileRepository();
        }
        public Doctor GetDoctorById(string id)
        {
            return doctorRepository.GetByID(id);
        }
    }
}
