namespace Reflectis.SDK.Diagnostics
{
    public class ExperienceStepCompleteDTO : ExperienceStepDTO
    {
        [SettableField]
        public int? score;
        [SettableField]
        public int? maxScore;
        [SettableField]
        public int? passingScore;
        [SettableField(isRequired = true)]
        public EExperienceOutcome outcome;
        [SettableField(isRequired = true)]
        public EExperienceScoringType scoringType;
        [SettableField]
        public string notes;
    }
}
