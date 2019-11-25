using Domain.Notifications.DataTransferObjects;

namespace Domain.PushNotificationProvider
{
    public interface IPushNotificationProviderFactory
    {
        ObjectWithNotification<IPushNotificationProvider> Create(string providerName);
    }
}
