using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<IEnumerable<T>> GetAll();

        IMongoCollection<BsonDocument> GetCollection();

        //Task<T> GetLog(string id);

        //// query after multiple parameters
        //Task<IEnumerable<T>> Get(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        //// add new note document
        //Task Add(T item);

        //// remove a single document / note
        //Task<bool> Remove(string id);

        //// update just a single document / note
        //Task<bool> Update(string id, string body);

        //// demo interface - full document update
        ////Task<bool> UpdateNoteDocument(string id, string body);

        //// should be used with high cautious, only in relation with demo setup
        //Task<bool> RemoveAll();
    }
}
