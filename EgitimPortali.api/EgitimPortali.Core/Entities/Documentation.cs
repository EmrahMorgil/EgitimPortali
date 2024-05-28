using EgitimPortali.Core.Common;
using EgitimPortali.Core.Enums;

namespace EgitimPortali.Core.Entities
{
    public class Documentation : BaseEntity
    {
        public Guid CourseId { get; set; }
        public DocumentationType DocumentationType { get; set; }
        public string Content { get; set; } = null!;
    }
}
