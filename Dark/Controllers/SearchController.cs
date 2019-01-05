using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DAL.Repositories;
using Dark.DTOs;
using Dark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Dark.Controllers
{
    public class SearchController : Controller
    {
        private readonly IGenericRepository<Log> _logRepository;

        public SearchController(IUnitOfWork uow)
        {
            _logRepository = uow.Log;
        }

        // GET: Search
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _logRepository.GetAll());
        }

        //public async Task<IEnumerable<Log>> GetAll()
        //{
        //    return await _logRepository.GetAll();
        //}


        // GET: Search/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // POST: Workflow/Create
        [HttpPost("Log/")]
        public async Task Create([FromBody] Log log)
        {
            try
            {
                if (log == null)
                    return;
                Console.WriteLine(DateTime.UtcNow + ": Inserting log: " + log.ToJson().ToString());
                await this._logRepository.Add(log);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // POST: Search/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(SearchLogDTO search)
        {
            Console.WriteLine(DateTime.UtcNow + ": Executing query: " + search.ToJson().ToString());
            try
            {
                if (search?.Application?.ToLower() == "global")
                    search.Application = null;
                var applicationNameQuery = String.IsNullOrEmpty(search.Application) ? String.Format("application_name: {{ $ne:null }}") : String.Format("application_name: '{0}'", search.Application);
                var dataQuery = String.IsNullOrEmpty(search?.Data) ? String.Format("data: {{ $ne:null }}") : String.Format("data: RegExp('{0}')", search.Data);
                var logNameQuery = String.IsNullOrEmpty(search?.LogName) ? String.Format("logname: {{ $ne:null }}") : String.Format("logname: '{0}'", search.LogName);
                var filter = String.Format(@"{{{0}, 
                                           date_time: {{$gte: ISODate('{1}'), $lte: ISODate('{2}')}},
                                           {3},
                                           {4}
                                         }}",
                                             applicationNameQuery,
                                             search?.StartDate.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                                             search?.EndDate.AddHours(-1).ToString("yyyy-MM-ddTHH:mm:ssZ"),
                                             dataQuery,
                                             logNameQuery);

                //& builder.Eq("logname", logName);
                var documentArray = await this._logRepository.GetCollection().Find(filter).Limit(50).ToListAsync();
                var result = new List<Log>();
                foreach (var document in documentArray)
                {
                    var log = BsonSerializer.Deserialize<Log>(document);
                    result.Add(log);
                }
                return View(result);
            }
            catch (Exception e)
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Search/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Search/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Search/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}