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
        private readonly IMongoCollection<T> _collection = null;
        private readonly string tableName;

        public GenericRepository(IOptions<Settings> settings)
        {
            this._context = new GenericContext<T>(settings);
            this.tableName = typeof(T).ToString().ToLower().Split(".").Last();
            this._collection = this._context.Get(this.tableName);
        }


        public async Task Add(T item)
        {
            try
            {
                await this._collection.InsertOneAsync(item);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            //IEnumerable<T> test = null;
            //return test;
            try
            {
                return await this._collection.Find(_ => true).Limit(50).ToListAsync();
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

        public async Task<T> Get(long id)
        {
            try
            {
                return await this._collection
                                .Find(note => "id" == id.ToString())
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<T> Get(string id)
        {
            try
            {
                ObjectId internalId = GetInternalId(id);
                var filterId = String.Format("_id:ObjectId('{0}')", id);
                var filter = String.Format(@"{{{0}}}",
                                             filterId);

                return await this._collection
                                .Find(filter)
                                .FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<IEnumerable<T>> Get(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> RemoveAll()
        {
            try
            {
                DeleteResult actionResult
                    = await this._collection
                    .DeleteManyAsync(new BsonDocument());

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Remove(string id)
        {
            try
            {
                DeleteResult actionResult
                    = await this._collection.DeleteOneAsync(this.GetFilterForId(id));

                return actionResult.IsAcknowledged
                    && actionResult.DeletedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Update(string id, UpdateDefinition<T> update)
        {
            var filter = Builders<T>.Filter.Eq(s => "Id", id);
            //var update = Builders<Note>.Update
            //                .Set(s => s.Body, body)
            //                .CurrentDate(s => s.UpdatedOn);

            try
            {
                UpdateResult actionResult
                    = await this._collection.UpdateOneAsync(filter, update);

                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public async Task<bool> Update(string id, T item)
        {
            try
            {
                
                ReplaceOneResult actionResult
                    = await this._collection
                                    .ReplaceOneAsync(this.GetFilterForId(id)
                                            , item
                                            , new UpdateOptions { IsUpsert = true });
                return actionResult.IsAcknowledged
                    && actionResult.ModifiedCount > 0;
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public ObjectId GetInternalId(string id)
        {
            ObjectId internalId;
            if (!ObjectId.TryParse(id, out internalId))
                internalId = ObjectId.Empty;

            return internalId;
        }

        private string GetFilterForId(string id)
        {
            var filterId = String.Format("_id:ObjectId('{0}')", id);
            return String.Format(@"{{{0}}}", filterId);

        }
    }
}