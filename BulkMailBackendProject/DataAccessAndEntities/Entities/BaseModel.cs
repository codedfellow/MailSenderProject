using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

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
        public ObjectId? CreatedBy { get; set; }
        public ObjectId? ModifiedBy { get; set; }
    }
}
