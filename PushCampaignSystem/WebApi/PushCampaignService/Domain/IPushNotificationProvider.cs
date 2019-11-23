using WebApi.Models;

namespace WebApi.PushCampaignService.Domain
{
    public interface IPushNotificationProvider
    {
        void PushNotification(PushNotificationPayload payload);
    }
}
