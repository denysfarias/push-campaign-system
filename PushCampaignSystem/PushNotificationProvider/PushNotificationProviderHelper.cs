﻿using Domain.PushNotificationProvider.Models;

namespace PushNotificationProvider
{
    public static class PushNotificationProviderHelper
    {
        public static string GetVisitDescription(PushNotificationPayload pushNotificationPayload)
        {
            return $"=> Push sent regarding visit {pushNotificationPayload.VisitId}";
        }

        public static string GetDeviceDescription(PushNotificationPayload pushNotificationPayload)
        {
            return $"===> Device ID: \"{pushNotificationPayload.DeviceId}\"";
        }
    }
}
