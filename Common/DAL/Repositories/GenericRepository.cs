using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using DAL;
using DAL.Context;
using DAL.Models;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class
    {
        private readonly GenericContext<T> _context = null;
        private readonly string tableName;

        public GenericRepository(IOptions<Settings> settings)
        {
            _context = new GenericContext<T>(settings);
            this.tableName = typeof(T).ToString().ToLower().Split(".").Last();
        }


        public Task AddLog(Log item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            //IEnumerable<T> test = null;
            //return test;
            try
            {
                return await _context.Get(this.tableName).Find(_ => true).Limit(5000).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public IMongoCollection<BsonDocument> GetCollection()
        {
            return this._context.GetDatabase().GetCollection<BsonDocument>(this.tableName);
        }

        public Task<T> GetLog(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> Get(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAll()
        {
            throw new NotImplementedException();
        }

        public Task<bool> Remove(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(string id, string body)
        {
            throw new NotImplementedException();
        }
    }
}