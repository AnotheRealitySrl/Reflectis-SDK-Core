namespace Reflectis.SDK.Diagnostics
{
    public class ExperienceJoinDTO : ExperienceDiagnosticDTO
    {
        [SettableField(isRequired = true)]
        public string context;

    }
}
