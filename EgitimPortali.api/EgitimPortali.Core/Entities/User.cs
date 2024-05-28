using EgitimPortali.Core.Common;
using EgitimPortali.Core.Enums;
using Microsoft.AspNetCore.Identity;

namespace EgitimPortali.Core.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
