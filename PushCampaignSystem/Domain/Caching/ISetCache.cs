using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Caching
{
    public interface ISetCache
    {
        Task<ObjectWithNotification<IEnumerable<string>>> GetAllAsync(string key);
        
        Task<CommandNotification> AddOrAppendAsync(string key, string value);
    }
}
