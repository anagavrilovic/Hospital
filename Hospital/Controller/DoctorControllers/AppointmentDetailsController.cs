using Hospital.DTO.DoctorDTO;
using Hospital.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Controller.DoctorControllers
{
    public class AppointmentDetailsController
    {
        private MedicalRecordService medicalRecordService;
        private AppointmentDetailsDTO DTO;
        public AppointmentDetailsController(AppointmentDetailsDTO DTO)
        {
            this.DTO = DTO;
            medicalRecordService = new MedicalRecordService();
        }

        public MedicalRecord GetMedicalRecordByPatientId()
        {
            return medicalRecordService.GetByPatientId(DTO.Appointment.IDpatient);
        }
    }
}
