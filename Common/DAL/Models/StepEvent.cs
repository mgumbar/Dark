﻿using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    //[BsonDiscriminator("events")]
    [BsonIgnoreExtraElements]
    public class StepEvent
    {
        [BsonElement("message")]
        public string Message { get; set; }
        [BsonElement("created_at")]
        public DateTime CreatedAt { get; set; }
        [BsonElement("status")]
        public string Status { get; set; }
        [BsonElement("event_type")]
        public string EventType { get; set; }
        [BsonElement("client_key")]
        public string ClientKey { get; set; }
        [BsonElement("source")]
        public string Source { get; set; }

        public StepEvent() { }

        //[BsonConstructorAttribute("events")]
        //public StepEvent(string message, string createdAt, string status, string eventType)
        //{
        //    this.Message = message;
        //    this.CreatedAt = createdAt;
        //    this.Status = status;
        //    this.EventType = eventType;
        //}

    }
}
