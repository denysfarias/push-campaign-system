using Domain.Notifications.DataTransferObjects;
using Domain.PushNotificationProvider;
using Domain.PushNotificationProvider.Models;
using System.IO;

namespace PushNotificationProvider
{
    public class MixPanelProvider : IPushNotificationProvider
    {
        private readonly TextWriter _textWriter;

        public MixPanelProvider(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public CommandNotification PushNotification(PushNotificationPayload payload)
        {
            var visitDescription = PushNotificationProviderHelper.GetVisitDescription(payload);
            _textWriter.WriteLine(visitDescription);

            var deviceDescription = PushNotificationProviderHelper.GetDeviceDescription(payload);
            _textWriter.WriteLine(deviceDescription);

            _textWriter.WriteLine($"===> Mixpanel logging: Olar! {payload.Message}");

            _textWriter.WriteLine();

            return new CommandNotification();
        }
    }
}
