using EgitimPortali.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Application.Dtos
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Role { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
    }
}
