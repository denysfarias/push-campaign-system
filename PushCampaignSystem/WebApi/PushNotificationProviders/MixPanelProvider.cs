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
            _textWriter.WriteLine($"Olar! {payload.Message}");
        }
    }
}
