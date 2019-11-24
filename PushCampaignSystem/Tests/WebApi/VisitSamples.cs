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
                    id = 8623,
                    device_id = "dev92364",
                    place_id = 79
                },
                new Visit()
                {
                    id = 2274,
                    device_id = "uid6244",
                    place_id = 10
                },
                new Visit()
                {
                    id = 12,
                    device_id = "ale835",
                    place_id = 22
                },
                new Visit()
                {
                    id = 632984,
                    device_id = "oery926",
                    place_id = 90
                }
            };
    }
}
