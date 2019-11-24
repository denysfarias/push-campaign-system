using Domain.PushNotificationProvider.Models;
using System.Collections.Generic;

namespace Domain.DataStore
{
    public interface ICampaignSearch
    {
        IEnumerable<PushCampaign> FindMessagesForPlace(int placeId);
    }
}
