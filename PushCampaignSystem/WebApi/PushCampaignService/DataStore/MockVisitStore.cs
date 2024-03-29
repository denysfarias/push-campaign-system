﻿using Domain.DataStore;
using Domain.DataStore.Entities;
using Domain.Notifications.DataTransferObjects;
using System.Collections.Generic;
using System.Linq;

namespace WebApi.PushCampaignService.DataStore
{
    public class MockVisitStore : IDataStore<Visit>
    {
        private readonly List<Visit> _visits;

        public MockVisitStore()
        {
            _visits = new List<Visit>();
        }

        public MockVisitStore(IEnumerable<Visit> visits)
        {
            _visits = visits.ToList();
        }

        public CommandNotification Load(IEnumerable<Visit> data)
        {
            var hasDuplicate = _visits.Any(stored => data.Any(toStore => stored.Id == toStore.Id));

            if (hasDuplicate)
                return new CommandNotification(property: string.Empty, message: "Remove duplicate visit to proceed.", level: Domain.Notifications.Level.Error);

            _visits.AddRange(data);
            return new CommandNotification();
        }

        public ObjectWithNotification<IEnumerable<Visit>> FindAll()
        {
            var result = _visits.AsReadOnly();
            return new ObjectWithNotification<IEnumerable<Visit>>(result);
        }

        public CommandNotification Reset()
        {
            _visits.Clear();
            return new CommandNotification();
        }
    }
}
