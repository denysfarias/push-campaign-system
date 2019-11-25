using Domain.Notifications.DataTransferObjects;
using Domain.PushNotificationProvider;
using Domain.PushNotificationProvider.Models;
using System.IO;

namespace PushNotificationProvider
{
    public class LocalyticsProvider : IPushNotificationProvider
    {
        private readonly TextWriter _textWriter;

        public LocalyticsProvider(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public CommandNotification PushNotification(PushNotificationPayload payload)
        {
            var visitDescription = PushNotificationProviderHelper.GetVisitDescription(payload);
            _textWriter.WriteLine(visitDescription);

            var deviceDescription = PushNotificationProviderHelper.GetDeviceDescription(payload);
            _textWriter.WriteLine(deviceDescription);

            _textWriter.WriteLine($"===> Localytics logging: {{ \"message\": \"{payload.Message}\", device_id: \"{payload.DeviceId}\" }}");

            _textWriter.WriteLine();

            return new CommandNotification();
        }
    }
}
