using Handmade.DTOs.SharedDTOs;
using Handmade.DTOs.UserNotifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.UserNotificationServices
{
    public interface IUserNotificationService
    {
        //public Task<ResultView<int>> CreateUserNotificationAsync(int userId, string message);
        public Task<ResultView<List<GetUserNotificationsDTO>>> GetUserNotificationsAsync(int userId);
        public Task<ResultView<bool>> MarkNotificationReadAsync(int notificationId);
        public Task<ResultView<List<GetUserNotificationsDTO>>> MarkAllUserNotificationsReadAsync(int userId);
        public Task<ResultView<List<GetUserNotificationsDTO>>> DeleteUserNotificationAsync(int notificationId);
    }
}
