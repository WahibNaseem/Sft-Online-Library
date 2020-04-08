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
        private readonly IBookService _bookService;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutService(ICheckoutRepository checkoutRepository, IBookService bookService, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _checkoutRepository = checkoutRepository;
            _bookService = bookService;
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
            return await _checkoutRepository.ListAsync();
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

            //Update existingCheckout entitycode here
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
            //Already book checkout logic
            //  if (IsCheckedOut(bookId)) return;

            //Get the Book to checkout
            var existingBook = _bookService.ListAsync("").Result.FirstOrDefault(x => x.Id == bookId);
            if (existingBook == null)
                return new BookResponse("Failed to get book for checkout");

            //Temp Give the check out status id , refactore this code 
            existingBook.StatusId = -1;

            await _bookService.UpdateAsync(bookId, existingBook);

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

            var result = await SaveAsync(checkOut);
            if (result.Success)
                return new BookResponse(existingBook);

            return new BookResponse($"checkoutitem->  :{result.Message}");



        }
        private bool IsCheckedOut(int id)
        {
            return ListAsync().Result.Any(x => x.Id == id);

        }



    }
}
