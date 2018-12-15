using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    [BsonIgnoreExtraElements]
    class Worker
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("date_time")]
        [JsonProperty("date_time")]
        public DateTime DateTime { get; set; }
        [BsonElement("updated_at")]
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [BsonElement("payload")]
        //public Dictionary<string, string> Payload { get; set; }
        public string Payload { get; set; }
        [BsonElement("status")]
        public string Satus { get; set; }
        [BsonElement("job_name")]
        public string JobName { get; set; }
        [BsonElement("progress")]
        public float Progress { get; set; }
        [BsonElement("exception")]
        public string Exception { get; set; }
    }
}
