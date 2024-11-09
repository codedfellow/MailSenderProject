using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessAndEntities.Entities
{
    public class BaseModel
    {
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("modified_at")]
        public DateTime? ModifiedAt { get; set; }
        [BsonElement("deleted_at")]
        public DateTime? DeletedAt { get; set; }
        [BsonElement("deleted")]
        public bool Deleted { get; set; }
        [BsonElement("created_by")]
        public int CreatedBy { get; set; }
        [BsonElement("modified_by")]
        public int? ModifiedBy { get; set; }
    }
}
