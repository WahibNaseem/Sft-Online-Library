using SftLibrary.Data.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SftLibrary.Data.Domain.Repositories
{
   public interface ICheckoutRepository
    {
        Task<IEnumerable<Checkout>> ListAsync();
        Task<Checkout> FindByIdAsync(int id);
        Task AddAsync(Checkout checkout);
        void Update(Checkout checkout);
        void Remove(Checkout checkout);
    }
}
