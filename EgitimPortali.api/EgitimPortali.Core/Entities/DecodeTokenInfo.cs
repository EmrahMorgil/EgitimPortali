using EgitimPortali.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EgitimPortali.Core.Entities
{
    public class DecodedTokenInfo
    {
        public Guid UserId { get; set; }
        public string Email { get; set; } = null!;
        public Role Role { get; set; }
    }
}
