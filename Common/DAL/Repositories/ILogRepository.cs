using DAL;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public interface ILogRepository
    {
        Task<IEnumerable<Log>> GetAllLogs();

        Task<Log> GetLog(string id);

        // query after multiple parameters
        Task<IEnumerable<Log>> GetLog(string bodyText, DateTime updatedFrom, long headerSizeLimit);

        // add new note document
        Task AddLog(Log item);

        // remove a single document / note
        Task<bool> RemoveLog(string id);

        // update just a single document / note
        Task<bool> UpdateLog(string id, string body);

        // demo interface - full document update
        //Task<bool> UpdateNoteDocument(string id, string body);

        // should be used with high cautious, only in relation with demo setup
        Task<bool> RemoveAllLogs();
    }
}
