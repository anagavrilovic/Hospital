using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller.DoctorControllers
{
    public class DoctorMainWindowController
    {
        private DoctorService doctorService;
        private HospitalTreatmentService hospitalTreatmentService;

        public DoctorMainWindowController()
        {
            doctorService = new DoctorService();
            hospitalTreatmentService = new HospitalTreatmentService();
        }

        public void CheckHospitalTreatmentDates()
        {
            hospitalTreatmentService.checkHospitalTreatmentDates();
        }

        public Model.Doctor GetDoctorById(string id)
        {
            return doctorService.GetDoctorById(id);
        }
    }
}
