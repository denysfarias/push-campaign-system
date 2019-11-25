using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface ICampaignManager
    {
        ObjectWithNotification<IEnumerable<Campaign>> GetAll();

        CommandNotification Load(IEnumerable<Campaign> campaigns);
    }
}
