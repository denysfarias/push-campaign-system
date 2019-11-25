using Domain.Notifications.DataTransferObjects;
using Domain.PushNotificationProvider.Models;

namespace Domain.PushNotificationProvider
{
    public interface IPushNotificationProvider
    {
        CommandNotification PushNotification(PushNotificationPayload payload);
    }
}
