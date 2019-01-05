using MongoDB.Bson.Serialization.Attributes;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    [BsonIgnoreExtraElements]
    public class Log
    {
        public Log(string data, int line, string path, string host, string applicationName, string status, string process)
        {
            this.Line = line;
            this.Data = data;
            this.DateTime = DateTime.UtcNow;
            this.Host = host;
            this.User = "SYS";
            this.Path = path;
            this.Logname = path.Substring(path.LastIndexOf('\\') + 1);
            this.Status = status;
            this.Process = process;
            this.ApplicationName = applicationName;
        }

        [JsonIgnore]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("line")]
        [JsonProperty("line")]
        public int Line { get; set; }
        [BsonElement("application_name")]
        [JsonProperty("application_name")]
        public string ApplicationName { get; set; }

        [StringLength(50, MinimumLength = 3)]
        [BsonElement("data")]
        [JsonProperty("data")]
        public string Data { get; set; }
        [BsonElement("date")]
        [JsonProperty("date")]
        public string Date { get; set; }
        [BsonElement("date_time")]
        [JsonProperty("date_time")]
        public DateTime DateTime { get; set; }
        [BsonElement("host")]
        [JsonProperty("host")]
        public string Host { get; set; }
        [BsonElement("logname")]
        [JsonProperty("logname")]
        public string Logname { get; set; }
        [BsonElement("user")]
        [JsonProperty("user")]
        public string User { get; set; }
        [BsonElement("time")]
        [JsonProperty("time")]
        public string Time { get; set; }
        [BsonElement("path")]
        [JsonProperty("path")]
        public string Path { get; set; }
        [BsonElement("request")]
        [JsonProperty("request")]
        public string Request { get; set; }
        [BsonElement("status")]
        [JsonProperty("status")]
        public string Status { get; set; }
        [BsonElement("reponse_size")]
        [JsonProperty("reponse_size")]
        public string ResponseSize { get; set; }
        [BsonElement("referrer")]
        [JsonProperty("referrer")]
        public string Referrer { get; set; }
        [BsonElement("user_agent")]
        [JsonProperty("user_agent")]
        public string UserAgent { get; set; }
        [BsonElement("process")]
        [JsonProperty("process")]
        public string Process { get; set; }

        public string LogNameShort()
        {
            int maxSize = 50;
            return this.Logname.Length > maxSize ? this.Logname.Substring(0, maxSize) + "..." : this.Logname;
        }
    }
}