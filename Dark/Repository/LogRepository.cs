using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dark.Context;
using Dark.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Dark.Repository
{
    public class LogRepository : ILogRepository
    {
        private readonly DarkContext _context = null;

        public LogRepository(IOptions<Settings> settings)
        {
            _context = new DarkContext(settings);
        }


        public Task AddLog(Log item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Log>> GetAllLogs()
        {
            try
            {
                return await _context.Logs.Find(_ => true).Limit(10).ToListAsync();
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        public Task<Log> GetLog(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Log>> GetLog(string bodyText, DateTime updatedFrom, long headerSizeLimit)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveAllLogs()
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveLog(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateLog(string id, string body)
        {
            throw new NotImplementedException();
        }
    }
}
