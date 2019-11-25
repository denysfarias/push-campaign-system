using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using System.Threading.Tasks;

namespace Domain.Services
{
    public interface ICampaignPusher
    {
        Task<CommandNotification> PushCampaign(Visit visit);
    }
}
