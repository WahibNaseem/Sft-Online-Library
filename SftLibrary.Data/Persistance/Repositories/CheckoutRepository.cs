using Microsoft.EntityFrameworkCore;
using SftLib.Data.Persistance.Contexts;
using SftLib.Data.Persistance.Repositories;
using SftLibrary.Data.Domain.Models;
using SftLibrary.Data.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SftLibrary.Data.Persistance.Repositories
{
    public class CheckoutRepository : BaseRepository, ICheckoutRepository
    {
        public CheckoutRepository(AppDbContext appDbContext) : base(appDbContext)
        {

        }
        public async Task AddAsync(Checkout checkout)
        {
            await _context.AddAsync(checkout);
        }

        public async Task<Checkout> FindByIdAsync(int id)
        {
            return await _context.CheckOuts.Include(x =>x.Book).FirstOrDefaultAsync(x =>x.Id==id);
        }

        public async Task<IEnumerable<Checkout>> ListAsync()
        {
            return await _context.CheckOuts.Include(x =>x.Book).ToListAsync();
        }

        public void Remove(Checkout checkout)
        {
            _context.CheckOuts.Remove(checkout);
        }

        public void Update(Checkout checkout)
        {
            _context.CheckOuts.Update(checkout);
        }
    }
}
