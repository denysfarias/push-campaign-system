using Caching.Configurations;
using Domain.Caching;
using Domain.Notifications.DataTransferObjects;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Caching
{
    public class SetCache : ISetCache, IDisposable
    {
        private readonly ConnectionMultiplexer _redis;

        public SetCache()
        {
            _redis = ConnectionMultiplexer.Connect(GeneralData.SERVER);
        }

        public async Task<CommandNotification> AddOrAppendAsync(string key, string value)
        {
            try
            {
                var db = _redis.GetDatabase();
                var added = await db.SetAddAsync(key, value);
                return new CommandNotification();
            }
            catch (Exception ex)
            {
                return new CommandNotification(property: key, message: $"Error appending value \"{value}\" to key \"{key}\"", level: Domain.Notifications.Level.Error);
            }
        }

        public async Task<ObjectWithNotification<IEnumerable<string>>> GetAllAsync(string key)
        {
            try
            {
                var db = _redis.GetDatabase();

                var values = await db.SetMembersAsync(key);

                var realValues = values.Select(value => (string)value).ToList();

                return new ObjectWithNotification<IEnumerable<string>>(realValues);
            }
            catch (Exception ex)
            {
                return new ObjectWithNotification<IEnumerable<string>>(@object: null, property: key, message: $"Error getting values from key \"{key}\"", level: Domain.Notifications.Level.Error);
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _redis.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~Cache()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
