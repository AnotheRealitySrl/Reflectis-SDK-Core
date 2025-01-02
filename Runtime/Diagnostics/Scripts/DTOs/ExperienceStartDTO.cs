namespace Reflectis.SDK.Diagnostics
{
    public class ExperienceStartDTO : ExperienceDiagnosticDTO
    {
        [SettableField(isRequired = true)]
        public string context;
    }
}
