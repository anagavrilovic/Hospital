using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    class ExaminationService
    {
        private MedicalRecordService medicalRecordService = new MedicalRecordService();
        public Examination GetExaminationByID(String id)
        {
            MedicalRecord medicalRecord = medicalRecordService.GetByPatientId(MainWindow.IDnumber);
            foreach(Examination examination in medicalRecord.Examination)
            {
                if (examination.appointment.IDAppointment.Equals(id)) return examination;
            }
            return null;
        }
    }
}
