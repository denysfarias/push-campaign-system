namespace Domain.PushNotificationProvider
{
    public interface IPushNotificationProviderFactory
    {
        IPushNotificationProvider Create(string providerName);
    }
}
