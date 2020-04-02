using SftLib.Data.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SftLib.Data.Persistance.Repositories
{
    public class BaseRepository
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }
    }
}
