using System.Collections.Generic;
using System.Linq;

namespace Domain.Notifications
{
    /// <summary>
    /// Based on https://github.com/andrebaltieri/flunt
    /// </summary>
    public abstract class Notifiable
    {
        private readonly List<Notification> _notifications;

        protected Notifiable() => _notifications = new List<Notification>();

        protected Notifiable(string property, string message, Level level) => _notifications = new List<Notification>() { new Notification(property, message, level) };

        protected Notifiable(Notification notification)
        {
            if (notification == null)
            {
                _notifications = new List<Notification>();
            }
            else
            {
                _notifications = new List<Notification>() { notification };
            }
        }

        protected Notifiable(IReadOnlyCollection<Notification> notifications)
        {
            if (notifications == null)
            {
                _notifications = new List<Notification>();
            }
            else
            {
                _notifications = new List<Notification>(notifications.Where(n => n != null));
            }
        }

        protected Notifiable(Notifiable item)
        {
            if (item.Notifications == null)
            {
                _notifications = new List<Notification>();
            }
            else
            {
                _notifications = new List<Notification>(item.Notifications.Where(n => n != null));
            }
        }

        protected Notifiable(params Notifiable[] items)
        {
            _notifications = new List<Notification>(items.SelectMany(item => item.Notifications ?? new List<Notification>()).Where(n => n != null));
        }


        public IReadOnlyCollection<Notification> Notifications => _notifications;

        public void AddNotification(string property, string message, Level level)
        {
            _notifications.Add(new Notification(property, message));
        }

        public void AddNotification(Notification notification)
        {
            if (notification != null)
            {
                _notifications.Add(notification);
            }
        }

        public void AddNotifications(IReadOnlyCollection<Notification> notifications)
        {
            if (notifications != null)
            {
                _notifications.AddRange(notifications.Where(n => n != null));
            }
        }

        public void AddNotifications(Notifiable item)
        {
            if (item.Notifications != null)
            {
                AddNotifications(item.Notifications.Where(n => n != null).ToList().AsReadOnly());
            }
        }

        public void AddNotifications(params Notifiable[] items)
        {
            foreach (var item in items.Where(n => n != null))
                AddNotifications(item);
        }

        public bool IsInvalid => HasError;
        public bool IsValid => !IsInvalid;
        public bool HasError => _notifications.Any(n => n.Level == Level.Error);
        public bool HasWarning => _notifications.Any(n => n.Level == Level.Warning);
        public bool HasInformation => _notifications.Any(n => n.Level == Level.Information);
    }
}
