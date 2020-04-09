using SftLibrary.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Domain.Repositories
{
   public interface ICheckoutHistoryRepository
    {
        Task<IEnumerable<CheckoutHistory>> ListAsync();
        Task<CheckoutHistory> FindByIdAsync(int id);
        Task AddAsync(CheckoutHistory checkoutHistory);
        void Update(CheckoutHistory checkoutHistory);
    }
}
