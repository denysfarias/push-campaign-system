using System.Collections.Generic;

namespace Domain.Notifications.DataTransferObjects
{
    public class ValueWithNotification<TValue> : Notifiable where TValue : struct
    {
        private readonly TValue? _value;

        public TValue Value { get { return _value ?? default(TValue); } }

        public ValueWithNotification(TValue? value) => _value = value;

        public ValueWithNotification(TValue? value, string property, string message, Level level) : base(property, message, level) => _value = value;

        public ValueWithNotification(TValue? value, Notification notification) : base(notification) => _value = value;

        public ValueWithNotification(TValue? value, IReadOnlyCollection<Notification> notifications) : base(notifications) => _value = value;

        public ValueWithNotification(TValue? value, Notifiable item) : base(item) => _value = value;

        public ValueWithNotification(TValue? value, params Notifiable[] items) : base(items) => _value = value;
    }
}
