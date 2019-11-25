using Caching;
using Domain.Caching;
using Domain.DataStore.Entities;
using Domain.MessageQueue;
using MessageQueue;
using MessageQueue.Configurations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IndexCampaignWorker
{
    class Program
    {
        private static readonly string CONSOLE_TITLE = "INDEX CAMPAIGN WORKER";

        private static ICampaignIndexer _campaignIndexer;

        static async Task Main(string[] args)
        {
            using (var setCache = new SetCache())
            using (var messageQueueReader = new MessageQueueReader<Campaign>(new IndexCampaignConfiguration(), item => DateTime.Now.ToString("u")))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Title = CONSOLE_TITLE;
                Console.WriteLine($" [--- {CONSOLE_TITLE} ---]");
                Console.WriteLine(" [*] Waiting for messages.");

                _campaignIndexer = new CampaignIndexer(setCache);

                var commandResult = await messageQueueReader.StartReadingAsync(HandleData);

                if (commandResult.IsInvalid)
                {
                    Console.WriteLine(" Error opening connection to read queue.");
                    Console.ReadLine();
                    return;
                }

                Console.WriteLine(" Press [enter] any time to exit.");
                Console.ReadLine();
            }
        }

        public static async Task HandleData(Campaign campaign, AckHandler ackHandler, NackHandler nackHandler)
        {
            Console.WriteLine($" [-] Campaign {campaign.Id} RECEIVED at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");

            var indexingResult = await _campaignIndexer.IndexCampaignAsync(campaign);

            if (indexingResult.Notifications.Any())
            {
                foreach (var notification in indexingResult.Notifications)
                {
                    Console.WriteLine($" [-] {notification.Message} at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");
                }

                if (indexingResult.IsInvalid)
                {
                    nackHandler(requeue: false);
                    return;
                }

            }

            Console.WriteLine($" [x] Campaign {campaign.Id} DONE indexing at {DateTime.Now.ToShortDateString()} {DateTime.Now.ToShortTimeString()}.");
            
            ackHandler();
        }
    }
}
