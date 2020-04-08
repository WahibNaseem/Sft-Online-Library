using SftLib.Data.Domain.Models;
using SftLibrary.Data.Domain.Repositories;
using SftLibrary.Data.Domain.Services;
using SftLibrary.Data.Domain.Services.Communication;
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
        public async Task<UserResponse> FindByIdAsync(int id)
        {
            var existingUser = await _repo.FindByIdAsync(id);
            if (existingUser == null)
                return new UserResponse("User Not Found!");

            return new UserResponse(existingUser);
        }

        public async Task<IEnumerable<User>> ListAsync()
        {
            return await _repo.ListAsync();
        }
    }
}
