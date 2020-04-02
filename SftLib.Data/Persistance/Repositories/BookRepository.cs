using Microsoft.EntityFrameworkCore;
using SftLib.Data.Domain.Models;
using SftLib.Data.Persistance.Contexts;
using SftLib.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLib.Data.Persistance.Repositories
{
    public class BookRepository :BaseRepository, IBookRepository
    {
        public BookRepository(AppDbContext appDbcontext): base(appDbcontext) { }
       
        public async Task AddAsync(Book book)
        {
            await _context.Books.AddAsync(book);
        }

        public async Task<Book> FindByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> ListAsync()
        {
            return await _context.Books.Include(x => x.Status).ToListAsync();
        }

        public void Remove(Book book)
        {
            _context.Books.Remove(book);
        }

        public void Update(Book book)
        {
            _context.Books.Update(book);
        }
    }
}
