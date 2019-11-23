using System;
using WebApi.Models;
using WebApi.PushCampaignService.Domain.PushNotificationProvider;

namespace WebApi.PushNotificationProviders
{
    public class LocalyticsProvider : IPushNotificationProvider
    {
        public void PushNotification(PushNotificationPayload payload)
        {
            Console.WriteLine($"{{ message: \"{payload.Message}\", device_id: {payload.DeviceId} }}");
        }
    }
}
