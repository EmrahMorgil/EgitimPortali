using EgitimPortali.Core.Entities;
using EgitimPortali.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Application.Dtos
{
    public class UserCourseDto
    {
        public Guid Id { get; set; }
        public CourseDto Course { get; set; }
        public CourseStatus Status { get; set; }
    }
}
