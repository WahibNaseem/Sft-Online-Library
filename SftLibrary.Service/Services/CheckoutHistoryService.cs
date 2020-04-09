using SftLibrary.Data.Domain.Models;
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
    public class CheckoutHistoryService : ICheckoutHistoryService
    {
        private readonly ICheckoutHistoryRepository _checkoutHistoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CheckoutHistoryService(ICheckoutHistoryRepository checkoutHistoryRepository, IUnitOfWork unitOfWork)
        {
            _checkoutHistoryRepository = checkoutHistoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CheckoutHistoryResponse> FindByIdAsync(int id)
        {
            var existingHistory = await _checkoutHistoryRepository.FindByIdAsync(id);

            if (existingHistory == null)
                return new CheckoutHistoryResponse("Book Not Found!");

            return new CheckoutHistoryResponse(existingHistory);
        }

        public Task<IEnumerable<CheckoutHistory>> ListAsync()
        {
            return _checkoutHistoryRepository.ListAsync();
        }

        public async Task<CheckoutHistoryResponse> SaveAsync(CheckoutHistory checkoutHistory)
        {
            try
            {
                await _checkoutHistoryRepository.AddAsync(checkoutHistory);
                await _unitOfWork.CompleteAsync();

                return new CheckoutHistoryResponse(checkoutHistory);

            }
            catch (Exception ex)
            {
                return new CheckoutHistoryResponse($"An error occurred when saving the checkoutHistory: {ex.Message}");
            }
        }

        public async Task<CheckoutHistoryResponse> UpdateAsync(int id, CheckoutHistory checkoutHistory)
        {
            var existingHistory = await _checkoutHistoryRepository.FindByIdAsync(id);
            if (existingHistory == null)
                return new CheckoutHistoryResponse("CheckoutHistory not Found");

            try
            {
                _checkoutHistoryRepository.Update(existingHistory);
                await _unitOfWork.CompleteAsync();

                return new CheckoutHistoryResponse(existingHistory);

            }
            catch (Exception ex)
            {
                return new CheckoutHistoryResponse($"An error occurred when updating the checkoutHistory: {ex.Message}");
            }
        }

        public async Task<IEnumerable<CheckoutHistory>> FindByBookId(int id)
        {
            var historyList = await _checkoutHistoryRepository.ListAsync();
            if (historyList.Count() > 0)
                historyList = historyList.Where(x => x.Book.Id == id);

            return historyList;
        }
    }
}
