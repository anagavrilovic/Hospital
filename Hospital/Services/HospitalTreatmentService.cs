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
    public class HospitalTreatmentService
    {
        IHospitalTreatmentRepository hospitalTreatmentRepository;
        public HospitalTreatmentService()
        {
            hospitalTreatmentRepository = new HospitalTreatmentFileRepository();
        }
        public void Delete(string id)
        {
           hospitalTreatmentRepository.Delete(id);
        }
        public ObservableCollection<HospitalTreatment> GetAll()
        {
            return hospitalTreatmentRepository.GetAll();
        }
        public void Update(HospitalTreatment hospitalTreatment)
        {
            hospitalTreatmentRepository.EditHospitalTreatment(hospitalTreatment);
        }
    }
}
