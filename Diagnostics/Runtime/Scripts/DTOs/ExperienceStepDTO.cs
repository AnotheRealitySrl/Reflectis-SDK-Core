namespace Reflectis.SDK.Diagnostics
{
    public abstract class ExperienceStepDTO : ExperienceDiagnosticDTO
    {
        [SettableField(isRequired = true)]
        public string stepId;
    }
}
