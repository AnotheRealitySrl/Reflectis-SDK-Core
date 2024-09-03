namespace Reflectis.SDK.Diagnostics
{
    public abstract class DetailExperienceDiagnosticDTO
    {
        [SettableFieldExperience]
        public string uniqueSessionId;
        [SettableFieldExperience]
        public string uniqueNetworkSessionId;
    }
}
