using System;
using WebApi.PushCampaignService.Domain.PushNotificationProvider;

namespace WebApi.PushNotificationProviders
{
    public class PushNotificationProviderFactory : IPushNotificationProviderFactory
    {
        public IPushNotificationProvider Create(string providerName)
        {
            if (providerName == "localytics")
                return new LocalyticsProvider();

            if (providerName == "mixpanel")
                return new MixPanelProvider();

            throw new NotImplementedException();
        }
    }
}
