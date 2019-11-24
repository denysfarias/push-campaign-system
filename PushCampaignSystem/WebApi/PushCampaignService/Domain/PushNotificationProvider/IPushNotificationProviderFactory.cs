namespace WebApi.PushCampaignService.Domain.PushNotificationProvider
{
    public interface IPushNotificationProviderFactory
    {
        IPushNotificationProvider Create(string providerName);
    }
}
