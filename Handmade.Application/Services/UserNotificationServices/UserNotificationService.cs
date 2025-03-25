using Handmade.Application.Contracts;
using Handmade.DTOs.SharedDTOs;
using Handmade.DTOs.UserNotifications;
using Handmade.Models;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handmade.Application.Services.UserNotificationServices
{
    public class UserNotificationService(IUserNotificationRepository userNotificationRepository) : IUserNotificationService
    {
        private readonly IUserNotificationRepository _userNotificationRepository = userNotificationRepository;
        public async Task<ResultView<List<GetUserNotificationsDTO>>> GetUserNotificationsAsync(int userId)
        {
            ResultView<List<GetUserNotificationsDTO>> result = new();
            try
            {
                List<UserNotification> userNotifications = [.. (await _userNotificationRepository.GetAllAsync()).Where(un => un.UserId == userId)];
                result.Data = userNotifications.Adapt<List<GetUserNotificationsDTO>>();
                result.IsSuccess = true;
                result.Msg = "User Notifications fetched successfully.";
            }
            catch (Exception ex)
            {
                result.Msg = $"Unexpected error occured while fetching user notifications, {ex.Message}";
            }
            return result;
        }

        public async Task<ResultView<bool>> MarkNotificationReadAsync(int notificationId)
        {
            ResultView<bool> result = new();
            try
            {
                UserNotification? findUserNotification = (await _userNotificationRepository.GetAllAsync()).FirstOrDefault(un => un.Id == notificationId);
                if (findUserNotification == null)
                {
                    result.Msg = "Notification not found";
                    return result;
                }
                findUserNotification.IsDeleted = true;
                int saveStatus = await _userNotificationRepository.SaveChangesAsync();
                result.Data = true && saveStatus > 0;
                result.Msg = saveStatus > 0 ? "Notification marked as read successfully." : "Notification wasn't marked read.";
                result.IsSuccess = true && saveStatus > 0;
            }
            catch (Exception ex)
            {
                result.Msg = $"Unexpected error occured while marking notification as read, {ex.Message}";
            }
            return result;
        }

        public async Task<ResultView<List<GetUserNotificationsDTO>>> MarkAllUserNotificationsReadAsync(int userId)
        {
            ResultView<List<GetUserNotificationsDTO>> result = new();
            try
            {
                List<UserNotification> userNotifications = [.. (await _userNotificationRepository.GetAllAsync()).Where(un => un.UserId == userId)];
                foreach (var notification in userNotifications)
                {
                    notification.IsDeleted = true;
                }
                await _userNotificationRepository.SaveChangesAsync();
                result.Data = userNotifications.Adapt<List<GetUserNotificationsDTO>>();
                result.IsSuccess = true;
                result.Msg = "All User Notifications marked as read successfully.";
            }
            catch (Exception ex)
            {
                result.Msg = $"Unexpected error occured while marking all user notifications as read, {ex.Message}";
            }
            return result;
        }

        public async Task<ResultView<List<GetUserNotificationsDTO>>> DeleteUserNotificationAsync(int notificationId)
        {
            ResultView<List<GetUserNotificationsDTO>> result = new();
            try
            {
                UserNotification? findUserNotification = (await _userNotificationRepository.GetAllAsync()).FirstOrDefault(un => un.Id == notificationId);
                if (findUserNotification == null)
                {
                    result.Msg = "Notification not found";
                    return result;
                }
                await _userNotificationRepository.DeleteAsync(findUserNotification);
                int saveStatus = await _userNotificationRepository.SaveChangesAsync();
                if (saveStatus > 0)
                {
                    List<UserNotification> userNotifications = [.. (await _userNotificationRepository.GetAllAsync()).Where(un => un.UserId == findUserNotification.UserId)];
                    result.Data = userNotifications.Adapt<List<GetUserNotificationsDTO>>();
                    result.IsSuccess = true;
                    result.Msg = "Notification deleted successfully.";
                }
                result.Msg = saveStatus > 0 ? "Notification deleted successfully." : "Notification wasn't deleted.";
            }
            catch (Exception ex)
            {
                result.Msg = $"Unexpected error occured while deleting notification, {ex.Message}";
            }
            return result;
        }
    }
}
