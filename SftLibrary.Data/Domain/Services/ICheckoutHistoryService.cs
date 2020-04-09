using SftLibrary.Data.Domain.Models;
using SftLibrary.Data.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Domain.Services
{
   public interface ICheckoutHistoryService
    {
        Task<IEnumerable<CheckoutHistory>> ListAsync();
        Task<CheckoutHistoryResponse> FindByIdAsync(int id);
        Task<CheckoutHistoryResponse> SaveAsync(CheckoutHistory checkoutHistory);
        Task<CheckoutHistoryResponse> UpdateAsync(int id, CheckoutHistory checkoutHistory);

        Task<IEnumerable<CheckoutHistory>> FindByBookId(int id);


    }
}
