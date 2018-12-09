using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonIgnoreExtraElements]
    public class Workflow
    {
        [BsonId]
        public ObjectId _id { get; set; }
        [BsonElement("client_key")]
        public string ClientKey { get; set; }
        //public string Entity { get; set; }
        //public string Filename { get; set; }
        [BsonElement("sender")]
        public string Sender { get; set; }
        [BsonIgnoreIfNull]
        //[BsonSerializer(typeof(MongoDB.Bson.Serialization.Attributes.o))]
        //[BsonRepresentation(BsonType.String)]
        [BsonElement("steps")]
        public List<Step> Steps { get; set; }

        [BsonElement("payload")]
        public Dictionary<string, string> Payload { get; set; }

        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("updated_at")]
        public DateTime UpdatedAt { get; set; }

        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }

}