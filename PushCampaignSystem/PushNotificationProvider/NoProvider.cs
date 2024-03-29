﻿using Domain.Notifications.DataTransferObjects;
using Domain.PushNotificationProvider;
using Domain.PushNotificationProvider.Models;
using System.IO;

namespace PushNotificationProvider
{
    public class NoProvider : IPushNotificationProvider
    {
        private readonly TextWriter _textWriter;

        public NoProvider(TextWriter textWriter)
        {
            _textWriter = textWriter;
        }

        public CommandNotification PushNotification(PushNotificationPayload payload)
        {
            var preffix = PushNotificationProviderHelper.GetVisitDescription(payload);
            _textWriter.WriteLine(preffix);

            _textWriter.WriteLine("===> No campaign with matching target");

            _textWriter.WriteLine();

            return new CommandNotification();
        }
    }
}
