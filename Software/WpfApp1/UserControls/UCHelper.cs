using Entities.Models;
using Notifications.Wpf.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation_Layer.UserControls
{
    public class UCHelper
    {
        private static NotificationManager notificationManager;

        public UCHelper() 
        {
            notificationManager = new NotificationManager();
        }

        public static void DisplayNotification(string title, string message, NotificationType type) 
        {
            var notificationContent = new NotificationContent
            {
                Title = title,
                Message = message,
                Type = type
            };
            notificationManager.ShowAsync(notificationContent);   
        }


    }
}
