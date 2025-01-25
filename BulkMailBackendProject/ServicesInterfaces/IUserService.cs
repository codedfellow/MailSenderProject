using DTOs.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesInterfaces
{
    public interface IUserService
    {
        Task<bool> RegisterUser(UserRegistrationDto model);
        Task<string> Login(UserLoginDto model);
        
    }
}
