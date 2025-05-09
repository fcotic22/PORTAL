using Data_Access_Layer.Repositories;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bussiness_Logic_Layer.Services
{
    public class NotificationService
    {
        public void AddNewNotification(Notification notification)
        {
            using(var repo = new NotificationRepository())
            {
                repo.Add(notification);
            }
        }

        public bool ContainsNotificationWithTitle(string title) 
        {
            using (var repo = new NotificationRepository())
            {
                if(repo.GetNotificationByTitle(title).FirstOrDefault() != null) return true;
                else return false;
            }
        }
    }
}
