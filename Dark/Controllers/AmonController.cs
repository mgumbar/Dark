using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using DAL.Models;
using DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dark.Controllers
{
    public class AmonController : Controller
    {
        private readonly IGenericRepository<Workflow> _AmonRepository;
        public AmonController(IUnitOfWork uow)
        {
            _AmonRepository = uow.Amon;
        }

        // GET: Workflow
        [HttpGet]
        public ActionResult Index()
        {
            return View();
            //return View(await _AmonRepository.GetAll());
        }

        // GET: Workflow/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        [HttpGet("Amon/{id}")]
        public async Task<Workflow> Details(string id)
        {
            return await this._AmonRepository.Get(id.ToString()) ?? new Workflow(); ;
        }

        // GET: Workflow/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workflow/Create
        [HttpPost("Amon/")]
        public async Task Create([FromBody] Workflow workflow)
        {
            try
            {
                // TODO: Add insert logic here

                await this._AmonRepository.Add(workflow);
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // GET: Workflow/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Workflow/Edit/5
        [HttpPut("Amon/{id}")]
        //[Produces("application/json")]
        public async Task<bool> Edit(string id, [FromBody] Workflow workflow)
        {
            try
            {
                // TODO: Add update logic here
                workflow.Id =  this._AmonRepository.GetInternalId(id);
                return await this._AmonRepository.Update(id.ToString(), workflow);
                //return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                // log or manage the exception
                throw ex;
            }
        }

        // GET: Workflow/Delete/5
        [HttpDelete("Amon/{id}")]
        public async Task<bool> Delete(string id)
        {
            return await this._AmonRepository.Remove(id);
        }

        // POST: Workflow/Delete/5
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