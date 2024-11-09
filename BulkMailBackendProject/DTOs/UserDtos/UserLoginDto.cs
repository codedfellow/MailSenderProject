using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.UserDtos
{
    public record UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
