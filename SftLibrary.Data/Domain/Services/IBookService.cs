using SftLib.Data.Domain.Models;
using SftLibrary.Data.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Domain.Services
{
   public interface IBookService
    {
        Task<IEnumerable<Book>> ListAsync();
        Task<BookResponse> SaveAsync(Book book);
        Task<BookResponse> UpdateAsync(int id, Book book);
        Task<BookResponse> DeleteAsync(int id);

    }
}
