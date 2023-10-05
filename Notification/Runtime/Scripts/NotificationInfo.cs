public class NotificationInfo
{
    private ENotificationType type;
    private string text;
    private int eventId;

    public ENotificationType Type { get => type; set => type = value; }
    public string Text { get => text; set => text = value; }
    public int EventId { get => eventId; set => eventId = value; }

    public NotificationInfo()
    {
    }

    public NotificationInfo(ENotificationType type, string text, int eventId)
    {
        Type = type;
        Text = text;
        EventId = eventId;
    }
}
