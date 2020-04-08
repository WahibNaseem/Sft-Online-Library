using SftLibrary.Data.Domain.Models;
using SftLibrary.Data.Domain.Services.Communication;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Domain.Services
{
   public interface ICheckoutService
    {
        Task<IEnumerable<Checkout>> ListAsync();
        Task<CheckoutResponse> FindByIdAsync(int id);
        Task<CheckoutResponse> SaveAsync(Checkout checkout);
        Task<CheckoutResponse> UpdateAsync(int id, Checkout checkout);
        Task<CheckoutResponse> DeleteAsync(int id);
        Task<BookResponse> CheckOutItem(int id, int bookId);
        Task<BookResponse> CheckInItem(int bookId);

    }
}
