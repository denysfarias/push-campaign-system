using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.PushCampaignService.Domain
{
    public interface IVisitManager
    {
        IEnumerable<Visit> GetAll();

        void Load(IEnumerable<Visit> visits);
    }
}
