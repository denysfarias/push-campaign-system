using Domain.Notifications.DataTransferObjects;
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

        public ObjectWithNotification<IPushNotificationProvider> Create(string providerName)
        {
            IPushNotificationProvider result;

            if (providerName == "localytics")
                result = new LocalyticsProvider(_textWriter);
            else if (providerName == "mixpanel")
                result = new MixPanelProvider(_textWriter);
            else
                result = new NoProvider(_textWriter);

            return new ObjectWithNotification<IPushNotificationProvider>(result);
        }
    }
}
