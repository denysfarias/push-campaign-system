using System.Collections.Generic;

namespace Domain.Notifications.DataTransferObjects
{
    /// <summary>
    /// Return object with notifications.
    /// </summary>
    /// <typeparam name="TObject"></typeparam>
    public class ObjectWithNotification<TObject> : Notifiable where TObject : class
    {
        public TObject Object { get; private set; }

        public ObjectWithNotification(TObject @object) => Object = @object;

        public ObjectWithNotification(TObject @object, string property, string message, Level level) : base(property, message, level) => Object = @object;

        public ObjectWithNotification(TObject @object, Notification notification) : base(notification) => Object = @object;

        public ObjectWithNotification(TObject @object, IReadOnlyCollection<Notification> notifications) : base(notifications) => Object = @object;

        public ObjectWithNotification(TObject @object, Notifiable item) : base(item) => Object = @object;

        public ObjectWithNotification(TObject @object, params Notifiable[] items) : base(items) => Object = @object;
    }
}
