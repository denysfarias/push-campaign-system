using Domain.DataStore.Entities;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface ICampaignManager
    {
        IEnumerable<Campaign> GetAll();

        void Load(IEnumerable<Campaign> campaigns);
    }
}
