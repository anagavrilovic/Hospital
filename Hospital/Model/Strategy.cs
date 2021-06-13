using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    interface Strategy
    {
        void Update();

        void Delete();

        List<IPatientNotification> GetNotifications();
    }
}
