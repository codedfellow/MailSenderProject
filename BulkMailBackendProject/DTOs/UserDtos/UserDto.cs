using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs.UserDtos
{
    public record UserDto
    {
        public ObjectId Id { get; set; }
        public string? Email { get; set; }
        public DateTimeOffset? LastLoginDate { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? City { get; set; }
        public bool Confirmed { get; set; }
    }
}
