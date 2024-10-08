namespace Reflectis.SDK.Diagnostics
{
    public class AnalyticsDisplayableData
    {
        public string type;
        public string content;

        public AnalyticsDisplayableData(string type, string content)
        {
            this.type = type;
            this.content = content;
        }
    }
}
