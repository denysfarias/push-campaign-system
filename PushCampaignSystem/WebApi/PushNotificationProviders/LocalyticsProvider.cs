using System.IO;
using WebApi.Models;
using WebApi.PushCampaignService.Domain.PushNotificationProvider;

namespace WebApi.PushNotificationProviders
{
    public class LocalyticsProvider : IPushNotificationProvider
    {
        private readonly TextWriter _textWriter;

        public LocalyticsProvider(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public void PushNotification(PushNotificationPayload payload)
        {
            _textWriter.WriteLine($"{{ message: \"{payload.Message}\", device_id: {payload.DeviceId} }}");
        }
    }
}
