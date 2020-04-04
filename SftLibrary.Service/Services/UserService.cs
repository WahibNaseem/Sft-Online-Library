using SftLib.Data.Domain.Models;
using SftLibrary.Data.Domain.Repositories;
using SftLibrary.Data.Domain.Services;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace SftLibrary.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repo;

        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }
        public async Task<User> FindByIdAsync(int id)
        {
            return await _repo.FindByIdAsync(id);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _repo.ListAsync();
        }
    }
}
