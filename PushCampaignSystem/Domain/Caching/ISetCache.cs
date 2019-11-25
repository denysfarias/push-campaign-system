using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Caching
{
    public interface ISetCache
    {
        Task<ObjectWithNotification<IEnumerable<string>>> GetAll(string key);
        
        Task<CommandNotification> AddOrAppend(string key, string value);
    }
}
