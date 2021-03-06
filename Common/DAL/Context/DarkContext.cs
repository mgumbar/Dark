﻿using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using DAL;
using DAL.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Context
{
    public class DarkContext
    {
        private readonly IMongoDatabase _database = null;

        public DarkContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Log> Logs
        {
            get
            {
                return _database.GetCollection<Log>("log");
            }
        }
    }
}
