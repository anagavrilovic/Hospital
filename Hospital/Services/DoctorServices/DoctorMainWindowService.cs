using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.Services.DoctorServices
{
    public class DoctorMainWindowService
    {
        private IHospitalTreatmentRepository treatmentRepository;
        private IDoctorRepository doctorRepository;
        public void checkHospitalTreatmentDates()
        {
            treatmentRepository = new HospitalTreatmentFileRepository();
            foreach (HospitalTreatment hospitalTreatment in treatmentRepository.GetAll())
            {
                if (hospitalTreatment.EndOfTreatment.Date < DateTime.Today)
                    treatmentRepository.Delete(hospitalTreatment.PatientId);
            }
        }
        public Doctor GetDoctorById(string doctorId)
        {
            doctorRepository = new DoctorFileRepository();
            return doctorRepository.GetByID(doctorId);
        }
    }
}
