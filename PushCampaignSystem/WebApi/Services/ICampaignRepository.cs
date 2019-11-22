using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ICampaignRepository
    {
        Campaign FindById(int id);

        IEnumerable<Campaign> FindAll();

        Campaign Create(Campaign campaign);
    }
}
