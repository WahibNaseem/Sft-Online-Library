using Microsoft.EntityFrameworkCore;
using SftLib.Data.Domain.Models;
using SftLib.Data.Persistance.Contexts;
using SftLib.Data.Persistance.Repositories;
using SftLibrary.Data.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Persistance.Repositories
{
    public class StatusRepository : BaseRepository, IStatusRepository
    {
        public StatusRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
        public async Task<IEnumerable<Status>> ListAsync()
        {
            return await _context.Statuses.ToListAsync();
        }
    }
}
