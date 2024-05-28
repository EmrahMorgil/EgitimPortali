using EgitimPortali.Core.Common;
using EgitimPortali.Core.Enums;


namespace EgitimPortali.Core.Entities
{
    public class Course : BaseEntity
    {
        public Guid InstructorId { get; set; }
        public EducationType EducationType { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public double Price { get; set; }
        public int Time { get; set; }
        public string IntroductionPhoto { get; set; } = null!;
        public string IntroductionVideo { get; set; } = null!;
    }
}
