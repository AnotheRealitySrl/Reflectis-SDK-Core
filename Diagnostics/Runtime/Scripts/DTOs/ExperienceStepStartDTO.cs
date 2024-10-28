namespace Reflectis.SDK.Diagnostics
{
    public class ExperienceStepStartDTO : ExperienceStepDTO
    {
        [SettableField(isRequired = true)]
        public string description;
    }
}
