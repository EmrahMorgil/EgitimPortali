using EgitimPortali.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Application.Dtos
{
    public class CourseDto
    {
        public Guid Id { get; set; }
        public List<DocumentationDto> Documentations { get; set; } = null!;
        public UserDto Instructor { get; set; } = null!;
        public EducationType EducationType { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int Capacity { get; set; }
        public double Price { get; set; }
        public int Time { get; set; }
        public string IntroductionPhoto { get; set; } = null!;
        public string IntroductionVideo { get; set; } = null!;
        public CourseStatus CourseStatus { get; set; }
    }
}
