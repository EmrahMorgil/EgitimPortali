using EgitimPortali.Core.Enums;

namespace EgitimPortali.Application.Dtos
{
    public class DocumentationDto
    {
        public Guid CourseId { get; set; }
        public DocumentationType DocumentationType { get; set; }
        public string Content { get; set; } = null!;
    }
}
