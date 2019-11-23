using System;
using WebApi.Models;
using WebApi.PushCampaignService.Domain;

namespace WebApi.PushNotificationProviders
{
    public class MixPanelProvider : IPushNotificationProvider
    {
        public void PushNotification(PushNotificationPayload payload)
        {
            Console.WriteLine($"Olar! {payload.Message}");
        }
    }
}
