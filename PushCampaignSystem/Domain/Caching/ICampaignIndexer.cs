using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using System.Threading.Tasks;

namespace Domain.Caching
{
    public interface ICampaignIndexer
    {
        Task<CommandNotification> IndexCampaignAsync(Campaign campaign);
    }
}
