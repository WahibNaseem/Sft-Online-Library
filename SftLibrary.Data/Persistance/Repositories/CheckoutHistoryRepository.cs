using Microsoft.EntityFrameworkCore;
using SftLib.Data.Persistance.Contexts;
using SftLib.Data.Persistance.Repositories;
using SftLibrary.Data.Domain.Models;
using SftLibrary.Data.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Persistance.Repositories
{
    public class CheckoutHistoryRepository : BaseRepository, ICheckoutHistoryRepository
    {
        public CheckoutHistoryRepository(AppDbContext appDbContext) : base(appDbContext) { }

        public async Task AddAsync(CheckoutHistory checkoutHistory)
        {
            await _context.CheckoutHistories.AddAsync(checkoutHistory);
        }

        public async Task<CheckoutHistory> FindByIdAsync(int id)
        {
            return await _context.CheckoutHistories.Include(h => h.Book).Include(x => x.User).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<CheckoutHistory>> ListAsync()
        {
            return await _context.CheckoutHistories.Include(h => h.Book).Include(x => x.User).ToListAsync();
        }

        public void Update(CheckoutHistory checkoutHistory)
        {
            _context.CheckoutHistories.Update(checkoutHistory);
        }
    }
}
