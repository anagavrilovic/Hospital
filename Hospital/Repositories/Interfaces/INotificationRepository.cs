using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface INotificationRepository : IGenericRepository<Notification>
    {
        ObservableCollection<Notification> GetAllNotificationsSortedDescending();
        void Update(Notification notification);
    }
}
