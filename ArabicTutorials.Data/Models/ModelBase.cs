using System;
using MongoDB.Bson.Serialization.Attributes;

namespace ArabicTutorials.Data.Models
{
    public abstract class ModelBase
    {
        [BsonId]
        public string Id { get; set; }

        public User CreatedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ModifiedAt { get; set; }
    }
}
