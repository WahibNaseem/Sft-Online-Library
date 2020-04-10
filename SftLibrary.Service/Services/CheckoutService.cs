using Microsoft.EntityFrameworkCore.Internal;
using SftLib.Data.Domain.Repositories;
using SftLibrary.Data.Domain.Models;
using SftLibrary.Data.Domain.Repositories;
using SftLibrary.Data.Domain.Services;
using SftLibrary.Data.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SftLibrary.Service.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IBookService _bookService;
        private readonly ICheckoutHistoryService _checkoutHistoryService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutService(ICheckoutRepository checkoutRepository, IStatusRepository statusRepository, IBookService bookService, ICheckoutHistoryService checkoutHistoryService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _checkoutRepository = checkoutRepository;
            _statusRepository = statusRepository;
            _bookService = bookService;
            _checkoutHistoryService = checkoutHistoryService;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<CheckoutResponse> DeleteAsync(int id)
        {
            var existingCheckOut = await _checkoutRepository.FindByIdAsync(id);

            if (existingCheckOut == null)
                return new CheckoutResponse("Checkout Not Found");

            try
            {
                _checkoutRepository.Remove(existingCheckOut);
                await _unitOfWork.CompleteAsync();

                return new CheckoutResponse(existingCheckOut);
            }
            catch (Exception ex)
            {
                return new CheckoutResponse($"An error occurred when deleting the checkout:{ex.Message}");
            }
        }

        public async Task<CheckoutResponse> FindByIdAsync(int id)
        {
            var existingCheckout = await _checkoutRepository.FindByIdAsync(id);

            if (existingCheckout == null)
                return new CheckoutResponse("Checkout Not Found");

            return new CheckoutResponse(existingCheckout);

        }

        public async Task<IEnumerable<Checkout>> ListAsync()
        {
            var list = await _checkoutRepository.ListAsync();
            return list;
        }

        public async Task<CheckoutResponse> SaveAsync(Checkout checkout)
        {
            try
            {
                await _checkoutRepository.AddAsync(checkout);
                await _unitOfWork.CompleteAsync();

                return new CheckoutResponse(checkout);
            }
            catch (Exception ex)
            {
                return new CheckoutResponse($"An error occurred when saving the checkout:{ex.Message}");
            }
        }

        public async Task<CheckoutResponse> UpdateAsync(int id, Checkout checkout)
        {
            var existingCheckout = await _checkoutRepository.FindByIdAsync(id);
            if (existingCheckout == null)
                return new CheckoutResponse("Checkout Not Found");

            try
            {
                _checkoutRepository.Update(existingCheckout);
                await _unitOfWork.CompleteAsync();

                return new CheckoutResponse(existingCheckout);
            }
            catch (Exception ex)
            {
                return new CheckoutResponse($"An error occurred when updating the checkout:{ex.Message}");
            }

        }

        public async Task<BookResponse> CheckOutItem(int id, int bookId)
        {
            // Check if the book is already checked out
            if (IsCheckedOut(bookId))
                return new BookResponse("Book is already checked out");

            //Get the Book to checkout
            var existingBook = _bookService.ListAsync("").Result.FirstOrDefault(x => x.Id == bookId);
            if (existingBook == null)
                return new BookResponse("Failed to get book for checkout");

            existingBook.Status = _statusRepository.ListAsync().Result.FirstOrDefault(x => x.Name == "Checked Out");

            var bookResult = await _bookService.UpdateAsync(bookId, existingBook);
            if (!bookResult.Success)
                return new BookResponse($"Failed to Update status of book to checkout :{bookResult.Message}");

            var userForCheckOut = _userRepository.ListAsync().Result.FirstOrDefault(x => x.Id == id);
            if (userForCheckOut == null)
                return new BookResponse("Failed to get user for checkOut");

            var checkOut = new Checkout
            {
                Book = existingBook,
                User = userForCheckOut,
                Since = DateTime.Now,
                Until = DateTime.Now.AddDays(3)
            };

            var checkoutResult = await SaveAsync(checkOut);
            if (!checkoutResult.Success)
                return new BookResponse($"checkoutitem->  :{checkoutResult.Message}");

            var checkoutHistory = new CheckoutHistory
            {
                CheckedOut = DateTime.Now,
                Book = existingBook,
                User = userForCheckOut
            };

            var historyResult = await _checkoutHistoryService.SaveAsync(checkoutHistory);
            if(!historyResult.Success)
                return new BookResponse($"Failed to Save history of book to checkout  :{historyResult.Message}"); ;

            return new BookResponse(existingBook);
        }

        public async Task<BookResponse> CheckInItem(int bookId)
        {
            var existingBook = _bookService.ListAsync("").Result.FirstOrDefault(x => x.Id == bookId);
            if (existingBook == null)
                return new BookResponse("Failed to find book for checkin");


            // Get the the book to change the status
            existingBook.Status = _statusRepository.ListAsync().Result.FirstOrDefault(x => x.Name == "Available");
            var bookResult = await _bookService.UpdateAsync(bookId, existingBook);
            if (!bookResult.Success)
                return new BookResponse("Failed to Update status of book to checkout");


            //Remove any existing checkout on the item
            var checkout = _checkoutRepository.ListAsync().Result.FirstOrDefault(x => x.BookId == bookId);
            if (checkout != null)
                _checkoutRepository.Remove(checkout);

            //close any existing checkout history
            var history = _checkoutHistoryService.ListAsync().Result.FirstOrDefault(h => h.Book.Id == bookId && h.CheckedIn == null);
            if(history != null)
            {
                history.CheckedIn = DateTime.Now;
               await _checkoutHistoryService.UpdateAsync(history.Id, history);
            }

            return new BookResponse(existingBook);

        }


        private bool IsCheckedOut(int id)
        {
            var result = ListAsync().Result.Any(x => x.Book.Id == id);
            return result;
        }



    }
}
