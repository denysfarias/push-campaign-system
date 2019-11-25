using Caching;
using Domain.DataStore.Entities;
using Domain.MessageQueue;
using Domain.Services;
using MessageQueue;
using MessageQueue.Configurations;
using PushNotificationProvider;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PushCampaignWorker
{
    class Program
    {
        private const bool VERBOSE = false;

        private static readonly string CONSOLE_TITLE = "PUSH CAMPAIGN WORKER";

        private static ICampaignPusher _campaignPusher;        

        static async Task Main(string[] args)
        {
            using (var setCache = new SetCache())
            using (var messageQueueReader = new MessageQueueReader<Visit>(new PushCampaignConfiguration(), item => DateTime.Now.ToString("u")))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Title = CONSOLE_TITLE;
                Console.WriteLine($" [--- {CONSOLE_TITLE} ---]");
                if (VERBOSE)
                {
                    Console.WriteLine(" [*] Waiting for messages.");
                }

                var pushNotificationProviderFactory = new PushNotificationProviderFactory(Console.Out);
                _campaignPusher = new CampaignPusher(setCache, pushNotificationProviderFactory);

                var commandResult = await messageQueueReader.StartReadingAsync(HandleData);

                if (commandResult.IsInvalid)
                {
                    Console.WriteLine(" Error opening connection to read queue.");
                    Console.ReadLine();
                    return;
                }

                if (VERBOSE)
                {
                    Console.WriteLine(" Press [enter] any time to exit.");
                }
                Console.ReadLine();
            }
        }

        public static async Task HandleData(Visit visit, AckHandler ackHandler, NackHandler nackHandler)
        {
            if (VERBOSE)
            {
                Console.WriteLine($" [-] Vist {visit.Id} RECEIVED at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");
            }

            var pushingResult = await _campaignPusher.PushCampaignAsync(visit);

            if (pushingResult.Notifications.Any())
            {
                if (VERBOSE)
                {
                    foreach (var notification in pushingResult.Notifications)
                    {
                        Console.WriteLine($" [-] {notification.Message} at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");
                    }
                }

                if (pushingResult.IsInvalid)
                {
                    nackHandler(requeue: false);
                    return;
                }
            }

            if (VERBOSE)
            {
                Console.WriteLine($" [x] Visit {visit.Id} DONE pushing campaign at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");
            }

            ackHandler();
        }
    }
}
