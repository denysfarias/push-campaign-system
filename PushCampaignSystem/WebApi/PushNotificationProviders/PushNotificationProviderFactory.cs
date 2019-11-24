using System;
using System.IO;
using WebApi.PushCampaignService.Domain.PushNotificationProvider;

namespace WebApi.PushNotificationProviders
{
    public class PushNotificationProviderFactory : IPushNotificationProviderFactory
    {
        private readonly TextWriter _textWriter;

        public PushNotificationProviderFactory(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public IPushNotificationProvider Create(string providerName)
        {
            if (providerName == "localytics")
                return new LocalyticsProvider(_textWriter);

            if (providerName == "mixpanel")
                return new MixPanelProvider(_textWriter);

            throw new NotImplementedException();
        }
    }
}
