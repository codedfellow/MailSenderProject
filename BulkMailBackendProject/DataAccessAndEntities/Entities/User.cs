using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.EntityFrameworkCore;

namespace DataAccessAndEntities.Entities
{
    [Collection("User")]
    public class User : BaseModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("email")]
        public string Email { get; set; }
        [BsonElement("password")]
        public string Password { get; set; }
        [BsonElement("last_login_date")]
        public DateTimeOffset? LastLoginDate { get; set; }
        [BsonElement("first_name")]
        public string? FirstName { get; set; }
        [BsonElement("middle_name")]
        public string? MiddleName { get; set; }
        [BsonElement("last_name")]
        public string? LastName { get; set; }
        [BsonElement("address")]
        public string? Address { get; set; }
        [BsonElement("dob")]
        public DateTime? DateOfBirth { get; set; }
        [BsonElement("city")]
        public string? City { get; set; }
        [BsonElement("confirmed")]
        public bool Confirmed { get; set; }
    }
}
