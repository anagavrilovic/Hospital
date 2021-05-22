using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface INotificationsUsersRepository : IGenericRepository<NotificationsUsers>
    {
        void DeleteNotificationsUsersByNotificationID(string id);
        ObservableCollection<NotificationsUsers> GetNotificationRecipientsByIDNotification(string id);
    }
}
