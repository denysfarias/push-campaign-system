using WebApi.Models;

namespace WebApi.PushCampaignService.Domain.PushNotificationProvider
{
    public interface IPushNotificationProvider
    {
        void PushNotification(PushNotificationPayload payload);
    }
}
