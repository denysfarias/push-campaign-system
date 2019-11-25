using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;

namespace Domain.Services
{
    public interface IVisitManager
    {
        ObjectWithNotification<IEnumerable<Visit>> GetAll();

        CommandNotification Load(IEnumerable<Visit> visits);
    }
}
