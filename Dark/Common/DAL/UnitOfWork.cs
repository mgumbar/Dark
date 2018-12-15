using DAL.Context;
using DAL.Models;
using DAL.Repositories;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private DarkContext context;
        private GenericRepository<Log> log;
        private GenericRepository<Workflow> amon;
        private GenericRepository<Worker> courseRepository;
        private IOptions<Settings> settings;

        public UnitOfWork(IOptions<Settings> settings)
        {
            this.context = new DarkContext(settings);
            this.settings = settings;
        }

        public DarkContext Context
        {
            get
            {
                return this.context;
            }
        }

        public IGenericRepository<Log> Log
        {
            get
            {

                if (this.log == null)
                {
                    this.log = new GenericRepository<Log>(settings);
                }
                return this.log;
            }
        }

        public IGenericRepository<Workflow> Amon
        {
            get
            {

                if (this.amon == null)
                {
                    this.amon = new GenericRepository<Workflow>(settings);
                }
                return this.amon;
            }
        }

        public void Save()
        {
            //context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    //context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}