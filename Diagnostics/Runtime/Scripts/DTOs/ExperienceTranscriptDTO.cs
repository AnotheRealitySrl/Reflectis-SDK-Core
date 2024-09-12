namespace Reflectis.SDK.Diagnostics
{
    public class ExperienceTranscriptDTO : ExperienceStepDTO
    {
        [SettableField(isRequired = true)]
        public string description;
    }
}
