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
        public async Task<ActionResult> Index()
        {
            return View(await _AmonRepository.GetAll());
        }

        // GET: Workflow/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Workflow/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Workflow/Create
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

        // GET: Workflow/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Workflow/Edit/5
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

        // GET: Workflow/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
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