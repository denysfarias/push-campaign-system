using Domain.Notifications.DataTransferObjects;
using Domain.PushNotificationProvider.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.DataStore
{
    public interface ICampaignSearch
    {
        ObjectWithNotification<IEnumerable<PushCampaign>> FindMessagesForPlace(int placeId);
    }
}
