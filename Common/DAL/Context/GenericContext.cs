using DAL.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Context
{
    public class GenericContext<T>
            where T : class
    {
        private readonly IMongoDatabase _database = null;

        public GenericContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<T> Get(string tableName)
        {
            //return _database.GetCollection<T>(T.GetType().ToString());
            return _database.GetCollection<T>(tableName);
        }
    }
}
