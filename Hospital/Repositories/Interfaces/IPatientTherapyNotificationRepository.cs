﻿using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
   public interface IPatientTherapyNotificationRepository : IGenericRepository<PatientTherapyMedicineNotification>
    {
        List<PatientTherapyMedicineNotification> GetByPatientID(String patientId);
        void Update(PatientTherapyMedicineNotification patientTherapyMedicineNotification);
        String GetNewID();
    }
}
