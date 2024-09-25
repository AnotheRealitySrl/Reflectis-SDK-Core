namespace Reflectis.SDK.Utilities
{
    public class Property
    {
        public string name;

        public object value;

        public Property(string name, object value)
        {
            this.name = name;
            this.value = value;
        }
    }
}