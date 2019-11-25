using Domain.PushNotificationProvider;
using System.IO;

namespace PushNotificationProvider
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

            return new NoProvider(_textWriter);
        }
    }
}
