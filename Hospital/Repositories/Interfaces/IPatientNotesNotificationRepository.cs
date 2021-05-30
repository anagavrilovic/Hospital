using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IPatientNotesNotificationRepository:IGenericRepository<PatientNotesNotification>
    {
        String GetNewID();
        List<PatientNotesNotification> GetByPatientID();
    }
}
