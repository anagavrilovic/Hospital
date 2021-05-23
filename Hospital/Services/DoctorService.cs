using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        internal ObservableCollection<Doctor> GetAll()
        {
            return doctorRepository.GetAll();
        }

        internal Doctor GetByID(string doctorID)
        {
            return doctorRepository.GetByID(doctorID);
        }

        public ObservableCollection<Doctor> GetDoctorsBySpecialty(DoctorSpecialty doctorsSpecialty)
        {
            return doctorRepository.GetBySpecialty(doctorsSpecialty);
        }
    }
}
