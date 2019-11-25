using Caching;
using Domain.DataStore.Entities;
using Domain.MessageQueue;
using Domain.Services;
using MessageQueue;
using MessageQueue.Configurations;
using PushNotificationProvider;
using System;
using System.Linq;

namespace PushCampaignWorker
{
    class Program
    {
        private static readonly string CONSOLE_TITLE = "PUSH CAMPAIGN WORKER";

        private static ICampaignPusher _campaignPusher;        

        static void Main(string[] args)
        {
            using (var setCache = new SetCache())
            using (var messageQueueReader = new MessageQueueReader<Visit>(new PushCampaignConfiguration(), item => DateTime.Now.ToString("u")))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Title = CONSOLE_TITLE;
                Console.WriteLine($" [---------------------------------{CONSOLE_TITLE}---------------------------------]");
                Console.WriteLine(" [*] Waiting for messages.");

                var pushNotificationProviderFactory = new PushNotificationProviderFactory(Console.Out);
                _campaignPusher = new CampaignPusher(setCache, pushNotificationProviderFactory);

                var commandResult = messageQueueReader.StartReading(HandleData);

                if (commandResult.IsInvalid)
                {
                    Console.WriteLine(" Error opening connection to read queue.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }

        public static async void HandleData(Visit visit, AckHandler ackHandler, NackHandler nackHandler)
        {
            Console.WriteLine($" [-] Vist {visit.Id} RECEIVED at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");

            var pushingResult = await _campaignPusher.PushCampaign(visit);

            if (pushingResult.Notifications.Any())
            {
                foreach (var notification in pushingResult.Notifications)
                {
                    Console.WriteLine($" [-] {notification.Message} at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");
                }

                if (pushingResult.IsInvalid)
                {
                    nackHandler(requeue: false);
                    return;
                }
            }

            Console.WriteLine($" [x] Visit {visit.Id} DONE pushing campaign at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");

            ackHandler();
        }
    }
}
