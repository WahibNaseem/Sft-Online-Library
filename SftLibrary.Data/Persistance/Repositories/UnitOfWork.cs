using SftLib.Data.Persistance.Contexts;
using SftLibrary.Data.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _appDbContext;

        public UnitOfWork(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public async Task CompleteAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
