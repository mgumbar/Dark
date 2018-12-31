using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    public class BaseModel
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("Id", NullValueHandling = NullValueHandling.Ignore)]
        public ObjectId Id { get; set; }
    }
}
