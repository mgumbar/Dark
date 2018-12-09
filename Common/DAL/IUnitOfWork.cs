using DAL.Context;
using DAL.Models;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public interface IUnitOfWork
    {
        DarkContext Context { get;  }

        IGenericRepository<Log> Log { get; }

        IGenericRepository<Workflow> Amon { get; }
    }
}
