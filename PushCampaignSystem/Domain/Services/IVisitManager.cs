using Domain.DataStore.Entities;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IVisitManager
    {
        IEnumerable<Visit> GetAll();

        void Load(IEnumerable<Visit> visits);
    }
}
