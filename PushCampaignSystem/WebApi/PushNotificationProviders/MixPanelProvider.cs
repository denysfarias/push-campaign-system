using System.IO;
using WebApi.Models;
using WebApi.PushCampaignService.Domain.PushNotificationProvider;

namespace WebApi.PushNotificationProviders
{
    public class MixPanelProvider : IPushNotificationProvider
    {
        private readonly TextWriter _textWriter;

        public MixPanelProvider(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public void PushNotification(PushNotificationPayload payload)
        {
            var visitDescription = PushNotificationProviderHelper.GetVisitDescription(payload);
            _textWriter.WriteLine(visitDescription);

            var deviceDescription = PushNotificationProviderHelper.GetDeviceDescription(payload);
            _textWriter.WriteLine(deviceDescription);

            _textWriter.WriteLine($"===> Mixpanel logging: Olar! {payload.Message}");

            _textWriter.WriteLine();
        }
    }
}
