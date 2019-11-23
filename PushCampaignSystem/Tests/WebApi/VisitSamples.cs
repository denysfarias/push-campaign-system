using System.Collections.Generic;
using WebApi.Models;

namespace Tests.WebApi
{
    public static class VisitSamples
    {
        public static IEnumerable<Visit> Get()
        {
            return Samples.AsReadOnly();
        }

        private static readonly List<Visit> Samples = new List<Visit>()
            {
                new Visit()
                {
                    Id = 8623,
                    DeviceId = "dev92364",
                    PlaceId = 79
                },
                new Visit()
                {
                    Id = 2274,
                    DeviceId = "uid6244",
                    PlaceId = 10
                },
                new Visit()
                {
                    Id = 12,
                    DeviceId = "ale835",
                    PlaceId = 22
                },
                new Visit()
                {
                    Id = 632984,
                    DeviceId = "oery926",
                    PlaceId = 90
                }
            };
    }
}
