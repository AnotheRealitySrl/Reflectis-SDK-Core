namespace Reflectis.SDK.Diagnostics
{
    public class ExperienceStepCompleteDTO : ExperienceStepDTO
    {
        [SettableFieldExperience]
        public int? score;
        [SettableFieldExperience]
        public int? maxScore;
        [SettableFieldExperience]
        public int? passingScore;
        [SettableFieldExperience]
        public EExperienceOutcome? outcome;
        [SettableFieldExperience]
        public EExperienceScoringType? scoringType;
        [SettableFieldExperience]
        public string notes;
    }
}
