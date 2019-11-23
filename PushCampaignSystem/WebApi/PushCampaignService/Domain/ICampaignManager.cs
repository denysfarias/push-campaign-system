using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.PushCampaignService.Domain
{
    public interface ICampaignManager
    {
        IEnumerable<Campaign> GetAll();

        void Load(IEnumerable<Campaign> campaigns);
    }
}
