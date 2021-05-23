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
        private HospitalTreatmentService hospitalTreatmentService;

        public void checkHospitalTreatmentDates()
        {
            hospitalTreatmentService = new HospitalTreatmentService();
            foreach (HospitalTreatment hospitalTreatment in hospitalTreatmentService.GetAll())
            {
                if (hospitalTreatment.EndOfTreatment.Date < DateTime.Today)
                    hospitalTreatmentService.Delete(hospitalTreatment.PatientId);
            }
        }
    }
}
