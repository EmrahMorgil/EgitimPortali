using EgitimPortali.Core.Entities;
using EgitimPortali.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Application.Dtos
{
    public class StudentManagementDto
    {
        public Guid Id { get; set; }
        public UserDto User { get; set; } = null!;
        public CourseDto Course { get; set; } = null!;
        public CourseStatus CourseStatus { get; set; }
    }
}
