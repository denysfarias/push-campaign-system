using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.PushCampaignService.Domain.DataStore
{
    public interface ICampaignSearch
    {
        IEnumerable<PushCampaign> FindMessagesForPlace(int placeId);
    }
}
