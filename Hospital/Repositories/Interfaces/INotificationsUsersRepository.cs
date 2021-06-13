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
        List<NotificationsUsers> GetNotificationRecipientsByIDNotification(string id);
        NotificationsUsers GetUniqueNotificationsUsers(String id, String username);
        void DeleteUniqueNotificationsUsers(String id, String username);
        void UpdateNotificationsUsers(NotificationsUsers notificationsUsers);
    }
}
