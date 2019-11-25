using System.Collections.Generic;

namespace Domain.Notifications.DataTransferObjects
{
    public class CommandNotification : Notifiable
    {
        public CommandNotification() : base() { }

        public CommandNotification(string property, string message, Level level) : base(property, message, level) { }

        public CommandNotification(Notification notification) : base(notification) { }

        public CommandNotification(IReadOnlyCollection<Notification> notifications) : base(notifications) { }

        public CommandNotification(Notifiable item) : base(item) { }

        public CommandNotification(params Notifiable[] items) : base(items) { }
    }
}
