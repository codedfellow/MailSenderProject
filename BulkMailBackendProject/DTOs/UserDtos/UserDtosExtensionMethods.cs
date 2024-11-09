using DataAccessAndEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.UserDtos
{
    public static class UserDtosExtensionMethods
    {
        public static UserDto ConvertToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Email = user.Email,
                LastLoginDate = user.LastLoginDate,
                FirstName = user.FirstName,
                MiddleName = user.MiddleName,
                LastName = user.LastName,
                Address = user.Address,
                DateOfBirth = user.DateOfBirth,
                City = user.City,
                Confirmed = user.Confirmed,
            };
        }

        public static List<UserDto> ConvertToUserDtoList(this List<User> userList)
        {
            List<UserDto> userDtos = new List<UserDto>();

            for (int i = 0; i < userList.Count; i++)
            {
                User currentUser = userList[i];
                var dtoObj = currentUser.ConvertToUserDto();
                userDtos.Add(dtoObj);
            }

            return userDtos;
        }
    }
}
