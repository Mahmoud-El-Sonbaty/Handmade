using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.DTOs.UserNotifications
{
    public class GetUserNotificationsDTO
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string? Message { get; set; }
        public bool IsRead { get; set; }
    }
}
