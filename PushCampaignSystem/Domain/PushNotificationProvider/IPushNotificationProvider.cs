using Domain.PushNotificationProvider.Models;

namespace Domain.PushNotificationProvider
{
    public interface IPushNotificationProvider
    {
        void PushNotification(PushNotificationPayload payload);
    }
}
