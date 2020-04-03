using SftLib.Data.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SftLib.Data.Domain.Repositories
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> ListAsync();
        Task<Book> FindByIdAsync(int id);
        Task AddAsync(Book book);
        void Update(Book book);
        void Remove(Book book);
        
    }
}
