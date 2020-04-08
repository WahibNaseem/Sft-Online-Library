using SftLib.Data.Domain.Models;
using SftLib.Data.Domain.Repositories;
using SftLibrary.Data.Domain.Repositories;
using SftLibrary.Data.Domain.Services;
using SftLibrary.Data.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Service.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BookService(IBookRepository bookRepository, IUnitOfWork unitOfWork)
        {
            _bookRepository = bookRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<BookResponse> DeleteAsync(int id)
        {
            var existingBook = await _bookRepository.FindByIdAsync(id);

            if (existingBook == null)
                return new BookResponse("Book Not Found!");

            try
            {
                _bookRepository.Remove(existingBook);
                await _unitOfWork.CompleteAsync();

                return new BookResponse(existingBook);

            }
            catch (Exception ex)
            {
                return new BookResponse($"An error occurred when deleting the book:{ex.Message}");
            }
        }
        public async Task<BookResponse> FindByIdAsync(int id)
        {
            var existingBook = await _bookRepository.FindByIdAsync(id);

            if (existingBook == null)
                return new BookResponse("Book Not Found!");

            return new BookResponse(existingBook);
        }

        public async Task<IEnumerable<Book>> ListAsync(string search)
        {
            if (string.IsNullOrEmpty(search))
            {
                var list = await _bookRepository.ListAsync();
                return list;
            }
            else
            {
                var books = await _bookRepository.ListAsync();
                var list = books.Where(x => x.Title.ToLower().Contains(search.ToLower()));

                return list;
            }
        }

        public async Task<BookResponse> SaveAsync(Book book)
        {
            try
            {
                await _bookRepository.AddAsync(book);
                await _unitOfWork.CompleteAsync();

                return new BookResponse(book);
            }
            catch (Exception ex)
            {
                return new BookResponse($"An error occurred when saving the book:{ex.Message}");
            }
        }

        public async Task<BookResponse> UpdateAsync(int id, Book book)
        {
            var existingBook = await _bookRepository.FindByIdAsync(id);

            if (existingBook == null)
                return new BookResponse("Book Not Found!");

            //Update existingBook entitycode here            
            existingBook.StatusId = book.StatusId;

            try
            {
                _bookRepository.Update(existingBook);
                await _unitOfWork.CompleteAsync();

                return new BookResponse(existingBook);
            }
            catch (Exception ex)
            {
                return new BookResponse($"An error occurred when updating the book:{ex.Message}");
            }
        }
    }
}
