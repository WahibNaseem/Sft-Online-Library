using SftLib.Data.Domain.Models;
using SftLib.Data.Domain.Repositories;
using SftLibrary.Data.Domain.Services;
using SftLibrary.Data.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public Task<BookResponse> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Book>> ListAsync()
        {
            return await _bookRepository.ListAsync();
        }

        public async Task<BookResponse> SaveAsync(Book book)
        {
            try
            {
               await _bookRepository.AddAsync(book);

                return new BookResponse();
            }
            catch(Exception ex)
            {
                return new BookResponse();
            }
        }

        public  Task<BookResponse> UpdateAsync(int id, Book book)
        {
            throw new NotImplementedException();
        }
    }
}
