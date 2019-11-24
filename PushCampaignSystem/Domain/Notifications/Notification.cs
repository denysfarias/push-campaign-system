namespace Domain.Notifications
{
    /// <summary>
    /// Based on https://github.com/andrebaltieri/flunt
    /// </summary>
    public sealed class Notification
    {
        public Notification(string property, string message, Level level = Level.Error)
        {
            Property = property;
            Message = message;
            Level = level;
        }

        public string Property { get; private set; }
        public string Message { get; private set; }
        public Level Level { get; private set; }
    }
}
