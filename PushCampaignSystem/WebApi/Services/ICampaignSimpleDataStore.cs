﻿using System.Collections.Generic;
using WebApi.Models;

namespace WebApi.Services
{
    public interface ICampaignSimpleDataStore
    {
        IEnumerable<Campaign> FindAll();

        void Load(IEnumerable<Campaign> campaigns);

        void Reset();
    }
}