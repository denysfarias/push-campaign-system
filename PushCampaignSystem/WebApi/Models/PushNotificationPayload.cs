namespace WebApi.Models
{
    public class PushNotificationPayload
    {
        public int VisitId { get; set; }
        
        public string Message { get; set; }

        public string DeviceId { get; set; }
    }
}
